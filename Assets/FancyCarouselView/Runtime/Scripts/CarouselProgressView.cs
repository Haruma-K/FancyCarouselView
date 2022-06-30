using System;
using UnityEngine;

namespace FancyCarouselView.Runtime.Scripts
{
    /// <summary>
    ///     View that shows the progress of the carousel view.
    /// </summary>
    public abstract class CarouselProgressView : MonoBehaviour
    {
        /// <summary>
        ///     Set up.
        /// </summary>
        /// <param name="elementCount"></param>
        public abstract void Setup(int elementCount);

        /// <summary>
        ///     Set the active data index.
        /// </summary>
        /// <param name="elementIndex"></param>
        public abstract void SetActiveIndex(int elementIndex);
    }

    public abstract class ClickableCarouselProgressView : CarouselProgressView
    {
        /// <summary>
        ///     Event called when the element of the carousel is clicked.
        /// </summary>
        public abstract event Action<int> ElementClicked;
    }
}
