using System;
using System.Collections.Generic;
using EasingCore;
using FancyScrollView;

namespace FancyCarouselView.Runtime.Scripts
{
    /// <summary>
    ///     Interface of the carousel view (Non Generic).
    /// </summary>
    public interface ICarouselView
    {
        /// <summary>
        ///     Delegate called when the active cell is changed.
        /// </summary>
        ActiveCellChangedDelegate ActiveCellChanged { get; set; }

        /// <summary>
        ///     Delegate called when the data is updated.
        /// </summary>
        CarouselDataChangedDelegate DataChanged { get; set; }

        /// <summary>
        ///     Total count of the data.
        /// </summary>
        int DataCount { get; }

        /// <summary>
        ///     Return true if the carousel view is scrolling.
        /// </summary>
        bool IsScrolling { get; }

        /// <summary>
        ///     Return true if the carousel view is auto scrolling.
        /// </summary>
        bool IsAutoScrolling { get; }

        /// <summary>
        ///     Return true when the carousel view is being dragged.
        /// </summary>
        bool IsDragging { get; }

        /// <summary>
        ///     Draggable or not.
        /// </summary>
        bool Draggable { get; }

        /// <summary>
        ///     Index of the active cell.
        /// </summary>
        int ActiveCellIndex { get; }

        /// <summary>
        ///     Scroll direction.
        /// </summary>
        ScrollDirection ScrollDirection { get; }

        /// <summary>
        ///     Scroll to the before position.
        /// </summary>
        /// <param name="duration"></param>
        /// <param name="easeType"></param>
        /// <param name="onComplete"></param>
        void ScrollToBefore(float duration, Ease easeType, Action onComplete = null);

        /// <summary>
        ///     Scroll to the after position.
        /// </summary>
        /// <param name="duration"></param>
        /// <param name="easeType"></param>
        /// <param name="onComplete"></param>
        void ScrollToAfter(float duration, Ease easeType, Action onComplete = null);

        /// <summary>
        ///     Scroll to the specified position.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="duration"></param>
        /// <param name="easeType"></param>
        /// <param name="onComplete"></param>
        void ScrollTo(float position, float duration, Ease easeType, Action onComplete = null);
    }

    /// <summary>
    ///     Interface of the carousel view.
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    /// <typeparam name="TCell"></typeparam>
    public interface ICarouselView<TData, TCell> : ICarouselView where TCell : CarouselCell<TData, TCell>
    {
        /// <summary>
        ///     Delegate called when the cell is instantiated.
        /// </summary>
        CarouselCellInstantiatedDelegate<TData, TCell> CarouselCellInstantiated { get; set; }

        /// <summary>
        ///     Delegate called when the cell is refreshed.
        /// </summary>
        CarouselCellRefreshedDelegate<TData, TCell> CarouselCellRefreshed { get; set; }

        /// <summary>
        ///     Delegate called when the visibility of the cell is changed.
        /// </summary>
        CarouselCellVisibilityChangedDelegate<TData, TCell> CarouselCellVisibilityChanged { get; set; }

        /// <summary>
        ///     Set up the carousel view.
        /// </summary>
        /// <param name="dataList"></param>
        void Setup(IList<TData> dataList);
    }
}