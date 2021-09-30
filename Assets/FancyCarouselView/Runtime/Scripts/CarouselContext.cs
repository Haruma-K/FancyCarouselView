using FancyScrollView;
using UnityEngine;

namespace FancyCarouselView.Runtime.Scripts
{
    /// <summary>
    ///     Data container shared between <see cref="CarouselView{TData,TCell}" /> and <see cref="CarouselCell{TData,TCell}" />
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    /// <typeparam name="TCell"></typeparam>
    public sealed class CarouselContext<TData, TCell> where TCell : CarouselCell<TData, TCell>
    {
        /// <summary>
        ///     Delegate called when the carousel cell is instantiated.
        /// </summary>
        public CarouselCellInstantiatedDelegate<TData, TCell> CarouselCellInstantiated;

        /// <summary>
        ///     Delegate called when the carousel cell is refreshed.
        /// </summary>
        public CarouselCellRefreshedDelegate<TData, TCell> CarouselCellRefreshedDelegate;

        /// <summary>
        ///     Delegate called when the visibility of the carousel cell is changed.
        /// </summary>
        public CarouselCellVisibilityChangedDelegate<TData, TCell> CarouselCellVisibilityChanged;

        /// <summary>
        ///     Size of the carousel view.
        /// </summary>
        public float CarouselSize { get; set; }

        /// <summary>
        ///     Size of the carousel cell.
        /// </summary>
        public Vector2 CellSize { get; set; }

        /// <summary>
        ///     Scroll direction.
        /// </summary>
        public ScrollDirection ScrollDirection { get; set; }
    }
}