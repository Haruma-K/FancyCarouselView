namespace FancyCarouselView.Runtime.Scripts
{
    /// <summary>
    ///     Delegate called when the active cell is changed.
    /// </summary>
    public delegate void ActiveCellChangedDelegate(int cellIndex);

    /// <summary>
    ///     Delegate called when the data of the carousel view is updated.
    /// </summary>
    public delegate void CarouselDataChangedDelegate();

    /// <summary>
    ///     Delegate called when the carousel cell is instantiated.
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    /// <typeparam name="TCell"></typeparam>
    public delegate void CarouselCellInstantiatedDelegate<TData, TCell>(TCell cell)
        where TCell : CarouselCell<TData, TCell>;

    /// <summary>
    ///     Delegate called when the visibility of the carousel cell is changed.
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    /// <typeparam name="TCell"></typeparam>
    public delegate void CarouselCellVisibilityChangedDelegate<TData, TCell>(TCell cell, bool visibility)
        where TCell : CarouselCell<TData, TCell>;

    /// <summary>
    ///     Delegate called shen the carousel cell is refreshed.
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    /// <typeparam name="TCell"></typeparam>
    public delegate void CarouselCellRefreshedDelegate<TData, TCell>(TCell cell, TData data)
        where TCell : CarouselCell<TData, TCell>;
}
