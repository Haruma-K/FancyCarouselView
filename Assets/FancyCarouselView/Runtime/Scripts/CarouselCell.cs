using FancyScrollView;
using UnityEngine;

namespace FancyCarouselView.Runtime.Scripts
{
    /// <summary>
    ///     Base class for the cell of the <see cref="CarouselView{TData,TCell}" />.
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    /// <typeparam name="TCell"></typeparam>
    public abstract class CarouselCell<TData, TCell> : FancyCell<TData, CarouselContext<TData, TCell>>
        where TCell : CarouselCell<TData, TCell>
    {
        /// <summary>
        ///     Refresh the cell based on the <see cref="itemData" />.
        /// </summary>
        /// <param name="itemData"></param>
        protected abstract void Refresh(TData itemData);

        /// <summary>
        ///     This method will be called after the visibility of the cell is changed.
        /// </summary>
        /// <param name="visibility"></param>
        protected virtual void OnVisibilityChanged(bool visibility)
        {
        }

        /// <summary>
        ///     <para>This method will be called after the cell is initialized.</para>
        ///     <para>Override this method if you want to write the process when the cell is instantiated.</para>
        /// </summary>
        protected virtual void OnInitialized()
        {
        }

        /// <summary>
        ///     <para>This method will be called after the position of the cell is changed.</para>
        ///     <para>Override this method if you want to customize the movement of the cell.</para>
        /// </summary>
        /// <param name="position"></param>
        protected virtual void OnPositionUpdated(float position)
        {
        }

        public override void UpdateContent(TData itemData)
        {
            Refresh(itemData);
            Context.CarouselCellRefreshedDelegate?.Invoke((TCell)this);
        }

        public override void SetVisible(bool visibility)
        {
            if (gameObject.activeSelf == visibility)
            {
                return;
            }

            gameObject.SetActive(visibility);

            OnVisibilityChanged(visibility);

            Context.CarouselCellVisibilityChanged?.Invoke((TCell)this, visibility);
        }

        public override void Initialize()
        {
            var rectTrans = (RectTransform)transform;
            var anchorMin = rectTrans.anchorMin;
            var anchorMax = rectTrans.anchorMax;
            var pivot = rectTrans.pivot;
            rectTrans.anchorMin = Vector2.one * 0.5f;
            rectTrans.anchorMax = Vector2.one * 0.5f;
            rectTrans.pivot = Vector2.one * 0.5f;
            rectTrans.sizeDelta = Context.CellSize;
            rectTrans.anchorMin = anchorMin;
            rectTrans.anchorMax = anchorMax;
            rectTrans.pivot = pivot;

            OnInitialized();

            Context.CarouselCellInstantiated?.Invoke((TCell)this);
        }

        public override void UpdatePosition(float position)
        {
            var pos = transform.localPosition;
            if (Context.ScrollDirection == ScrollDirection.Horizontal)
            {
                pos.x = Mathf.Lerp(-Context.CarouselSize, Context.CarouselSize, position);
            }
            else
            {
                pos.y = Mathf.Lerp(Context.CarouselSize, -Context.CarouselSize, position);
            }

            transform.localPosition = pos;

            OnPositionUpdated(position);
        }
    }
}