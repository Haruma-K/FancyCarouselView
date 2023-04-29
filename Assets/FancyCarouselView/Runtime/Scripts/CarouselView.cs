using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EasingCore;
using FancyScrollView;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace FancyCarouselView.Runtime.Scripts
{
    /// <summary>
    ///     Base class for the carousel view.
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    /// <typeparam name="TCell"></typeparam>
    [RequireComponent(typeof(CarouselScroller))]
    [RequireComponent(typeof(Image))]
    [DisallowMultipleComponent]
    public abstract class CarouselView<TData, TCell> : FancyScrollView<TData, CarouselContext<TData, TCell>>,
        ICarouselView<TData, TCell>, IBeginDragHandler, IDragHandler,
        IEndDragHandler where TCell : CarouselCell<TData, TCell>
    {
        private const float FlickThreshold = 3.0f;

        [SerializeField] private Scroller _scroller;
        [SerializeField] private TCell _cellPrefab;
        [SerializeField] private Vector2 _cellSize = new Vector2(100, 100);
        [SerializeField] private float _cellSpacing = 30.0f;
        [SerializeField] private float _snapAnimationDuration = 0.2f;
        [SerializeField] private Ease _snapAnimationType = Ease.OutQuad;
        [SerializeField] private bool _autoScrollingEnabled;
        [SerializeField] private float _autoScrollingIntervalSec = 3.0f;
        [SerializeField] private bool _inverseAutoScrollingDirection;
        [SerializeField] private CarouselProgressView _progressView;
        [SerializeField] private bool _progressViewInteraction;
        private int _activeCellIndex = -1;
        private Coroutine _autoScrollCoroutine;
        private bool _scrollerDraggableCache;
        private Coroutine _scrollCoroutine;

        protected override GameObject CellPrefab => _cellPrefab.gameObject;

        public Scroller Scroller
        {
            get => _scroller;
            set => _scroller = value;
        }

        public TCell CellPrefabInternal
        {
            get => _cellPrefab;
            set => _cellPrefab = value;
        }

        public Vector2 CellSize
        {
            get => _cellSize;
            set => _cellSize = value;
        }

        public float CellSpacing
        {
            get => _cellSpacing;
            set => _cellSpacing = value;
        }

        public float SnapAnimationDuration
        {
            get => _snapAnimationDuration;
            set => _snapAnimationDuration = value;
        }

        public Ease SnapAnimationType
        {
            get => _snapAnimationType;
            set => _snapAnimationType = value;
        }

        public bool AutoScrollingEnabled
        {
            get => _autoScrollingEnabled;
            set => _autoScrollingEnabled = value;
        }

        public float AutoScrollingIntervalSec
        {
            get => _autoScrollingIntervalSec;
            set => _autoScrollingIntervalSec = value;
        }

        public bool InverseAutoScrollingDirection
        {
            get => _inverseAutoScrollingDirection;
            set => _inverseAutoScrollingDirection = value;
        }

        public CarouselProgressView ProgressView
        {
            get => _progressView;
            set => _progressView = value;
        }

        public bool ProgressViewInteraction
        {
            get => _progressViewInteraction;
            set => _progressViewInteraction = value;
        }

        private int ActiveCellPosition => Mathf.RoundToInt(_scroller.Position);

        private void OnEnable()
        {
            if (_progressView != null) ActiveCellChanged += _progressView.SetActiveIndex;
        }

        private void OnDisable()
        {
            if (_progressView != null) ActiveCellChanged -= _progressView.SetActiveIndex;
        }

        void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
        {
            // If the scroller draggable is set to false, do nothing.
            if (!_scroller.Draggable)
                return;

            // On mobile device, by using two fingers, it is possible to trigger this method twice before OnEndDrag.
            // So to ignore second finger, do nothing if _draggingPinterId has already been set.
            if (_draggingPinterId != -1)
                return;

            _scrollerDraggableCache = _scroller.Draggable;
            _draggingPinterId = eventData.pointerId;

            var isInvalidDragging = false;
            isInvalidDragging |= _scroller.ScrollDirection == ScrollDirection.Vertical &&
                                 Math.Abs(eventData.delta.x) > Math.Abs(eventData.delta.y);
            isInvalidDragging |= _scroller.ScrollDirection == ScrollDirection.Horizontal &&
                                 Math.Abs(eventData.delta.x) < Math.Abs(eventData.delta.y);
            if (isInvalidDragging)
            {
                // If the drag direction is invalid, prevent the scroller from responding to the drag event.
                // This flag will be reset to _scrollerDraggableCache in OnEndDrag.
                _scroller.Draggable = false;
                return;
            }

            if (IsScrolling)
                StopScrolling();

            if (IsAutoScrolling)
                StopAutoScrolling();
        }

        public ActiveCellChangedDelegate ActiveCellChanged { get; set; }

        public CarouselDataChangedDelegate DataChanged { get; set; }

        public CarouselCellInstantiatedDelegate<TData, TCell> CarouselCellInstantiated { get; set; }

        public CarouselCellRefreshedDelegate<TData, TCell> CarouselCellRefreshed { get; set; }

        public CarouselCellVisibilityChangedDelegate<TData, TCell> CarouselCellVisibilityChanged { get; set; }

        public int DataCount { get; private set; }

        public bool IsScrolling => _scrollCoroutine != null;

        public bool IsAutoScrolling => _autoScrollCoroutine != null;

        public bool IsDragging => _draggingPinterId != -1;

        private int _draggingPinterId = -1;

        public bool Draggable => _scroller.Draggable;

        public int ActiveCellIndex => (int)GetCircularPosition(ActiveCellPosition);

        public ScrollDirection ScrollDirection => _scroller.ScrollDirection;

        void ICarouselView<TData, TCell>.Setup(IList<TData> dataList)
        {
            Setup((IReadOnlyList<TData>)dataList);
        }

        public void Setup(IReadOnlyList<TData> dataList)
        {
            DataCount = dataList.Count;

            // FancyScrollView only accepts IList, so if it can be converted to IList, convert it, and if it cannot be converted, create a new List.
            var list = dataList as IList<TData> ?? dataList.ToList();

            UpdateContents(list);
            _scroller.SetTotalCount(DataCount);
            DataChanged?.Invoke();
            if (_progressView != null)
                _progressView.Setup(DataCount);
        }

        public void Cleanup()
        {
            Setup(Array.Empty<TData>());
        }

        public void ScrollToBefore(float duration, Ease easeType, Action onComplete = null)
        {
            var position = ActiveCellPosition - 1;
            if (!loop) position = Mathf.Max(0, position);
            ScrollTo(position, duration, easeType, onComplete);
        }

        public void ScrollToAfter(float duration, Ease easeType, Action onComplete = null)
        {
            var position = ActiveCellPosition + 1;
            if (!loop) position = Mathf.Min(DataCount - 1, position);
            ScrollTo(position, duration, easeType, onComplete);
        }

        public void ScrollTo(float position, float duration, Ease easeType, Action onComplete = null)
        {
            if (duration <= 0f)
            {
                onComplete?.Invoke();
                return;
            }

            if (IsScrolling)
                StopScrolling();

            var animationRoutine = ScrollRoutine(position, duration, easeType,
                () =>
                {
                    _scrollCoroutine = null;
                    onComplete?.Invoke();
                });
            _scrollCoroutine = StartCoroutine(animationRoutine);
        }

        void IDragHandler.OnDrag(PointerEventData eventData)
        {
        }

        void IEndDragHandler.OnEndDrag(PointerEventData eventData)
        {
            // If the scroller draggable is false before OnBeginDrag, do nothing.
            if (!_scrollerDraggableCache)
                return;

            // If the pointer id is not the same as the one when the drag started, do nothing.
            // This is possible when using two fingers on mobile devices.
            if (_draggingPinterId != eventData.pointerId)
                return;

            var pos1 = Mathf.FloorToInt(_scroller.Position);
            var pos2 = Mathf.CeilToInt(_scroller.Position);
            if (!loop)
            {
                pos1 = Mathf.Max(0, pos1);
                pos2 = Mathf.Min(DataCount - 1, pos2);
            }

            float position;
            var deltaX = ScrollDirection == ScrollDirection.Horizontal ? eventData.delta.x : eventData.delta.y;
            if (deltaX < -FlickThreshold)
                position = ScrollDirection == ScrollDirection.Horizontal ? pos2 : pos1;
            else if (deltaX > FlickThreshold)
                position = ScrollDirection == ScrollDirection.Horizontal ? pos1 : pos2;
            else
                position = ActiveCellPosition;

            ScrollTo(position, _snapAnimationDuration, _snapAnimationType);
            if (_autoScrollingEnabled)
                StartAutoScrolling();

            _scroller.Draggable = _scrollerDraggableCache;
            _draggingPinterId = -1;
        }

        private void OnProgressViewElementClicked(int index)
        {
            if (!_progressViewInteraction)
                return;

            if (IsAutoScrolling)
                StopAutoScrolling();

            ScrollTo(index, _snapAnimationDuration, _snapAnimationType, () =>
            {
                if (_autoScrollingEnabled)
                    StartAutoScrolling();
            });
        }

        private void StartAutoScrolling()
        {
            if (IsAutoScrolling)
                StopAutoScrolling();

            _autoScrollCoroutine = StartCoroutine(AutoScrollRoutine());
        }

        private IEnumerator AutoScrollRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(_autoScrollingIntervalSec);
                var beforeScrollPosition = ActiveCellPosition - 1;
                var afterScrollPosition = ActiveCellPosition + 1;
                if (!loop)
                {
                    beforeScrollPosition = Mathf.Max(0, beforeScrollPosition);
                    afterScrollPosition = Mathf.Min(DataCount - 1, afterScrollPosition);
                }

                var position = _inverseAutoScrollingDirection ? beforeScrollPosition : afterScrollPosition;
                yield return ScrollRoutine(position, _snapAnimationDuration, _snapAnimationType);
            }
        }

        private IEnumerator ScrollRoutine(float position, float duration, Ease easeType, Action onComplete = null)
        {
            return AnimationUtils.CreateFloatAnimationRoutine(_scroller.Position, position, duration, easeType,
                x => { _scroller.Position = x; },
                () =>
                {
                    _scroller.Position = GetCircularPosition(position);
                    onComplete?.Invoke();
                });
        }

        protected override void Initialize()
        {
            if (cellContainer == null) cellContainer = transform;

            var rectTrans = (RectTransform)cellContainer.transform;
            var carouselSize = _scroller.ScrollDirection == ScrollDirection.Horizontal
                ? rectTrans.rect.width
                : rectTrans.rect.height;
            var intervalPerPx = 0.5f / carouselSize;
            var cellSize = _scroller.ScrollDirection == ScrollDirection.Horizontal ? _cellSize.x : _cellSize.y;
            var zeroSpacingInterval = 0.5f - (carouselSize - cellSize) * intervalPerPx;
            cellInterval = zeroSpacingInterval + intervalPerPx * _cellSpacing;

            Context.CarouselSize = carouselSize;
            Context.CellSize = _cellSize;
            Context.ScrollDirection = _scroller.ScrollDirection;
            Context.CarouselCellInstantiated += CarouselCellInstantiated;
            Context.CarouselCellVisibilityChanged += CarouselCellVisibilityChanged;
            Context.CarouselCellRefreshedDelegate += CarouselCellRefreshed;

            _activeCellIndex = ActiveCellIndex;
            ActiveCellChanged?.Invoke(ActiveCellIndex);
            _scroller.OnValueChanged(OnScrolled);

            if (_autoScrollingEnabled) StartAutoScrolling();

            if (_progressView != null && _progressView is ClickableCarouselProgressView clickableView)
                clickableView.ElementClicked += OnProgressViewElementClicked;
        }

        private void OnScrolled(float position)
        {
            UpdatePosition(position);

            if (_activeCellIndex != ActiveCellIndex)
            {
                _activeCellIndex = ActiveCellIndex;
                ActiveCellChanged?.Invoke(ActiveCellIndex);
            }
        }

        private void StopScrolling()
        {
            if (!IsScrolling) return;

            StopCoroutine(_scrollCoroutine);
            _scrollCoroutine = null;
        }

        private void StopAutoScrolling()
        {
            if (!IsAutoScrolling) return;

            StopCoroutine(_autoScrollCoroutine);
            _autoScrollCoroutine = null;
        }

        private float GetCircularPosition(float position)
        {
            var size = DataCount;
            return size < 1 ? 0 : position < 0 ? size - 1 + (position + 1) % size : position % size;
        }
#if UNITY_EDITOR

        private void Reset()
        {
            loop = true;
            GetComponent<Image>().color = Color.clear;
            if (cellContainer == null) cellContainer = transform;
        }

        private void OnDrawGizmos()
        {
            if (cellContainer != null && _scroller != null) DrawCellRect();
        }

        private void DrawCellRect()
        {
            var isHorizontal = ScrollDirection == ScrollDirection.Horizontal;
            var cellContainerTrans = cellContainer.transform;
            var cellRect = new Rect
            {
                size = _cellSize,
                center = Vector2.zero
            };
            var color = Color.white;
            color.a = 0.25f;
            var outlineColor = Color.clear;
            var pos = cellContainerTrans.position;
            var q = Quaternion.identity;
            var s = cellContainerTrans.lossyScale;
            var handlesMatrix = Handles.matrix;
            Handles.matrix = Matrix4x4.TRS(pos, q, s);
            Handles.DrawSolidRectangleWithOutline(cellRect, color, outlineColor);
            Handles.matrix = handlesMatrix;
        }

#endif
    }
}
