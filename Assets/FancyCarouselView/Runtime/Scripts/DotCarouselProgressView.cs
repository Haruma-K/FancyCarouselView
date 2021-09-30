using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FancyCarouselView.Runtime.Scripts
{
    /// <summary>
    ///     View that represents the progress of the carousel view with dots.
    /// </summary>
    public sealed class DotCarouselProgressView : CarouselProgressView
    {
        [SerializeField] private DotCarouselProgressElement progressElementPrefab = default;
        private int _activeIndex = -1;

        private List<DotCarouselProgressElement> _progressElementInstances = new List<DotCarouselProgressElement>();

        /// <summary>
        ///     Set up.
        /// </summary>
        /// <param name="elementCount"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public override void Setup(int elementCount)
        {
            if (GetComponent<HorizontalLayoutGroup>() == null && GetComponent<VerticalLayoutGroup>() == null)
            {
                throw new InvalidOperationException(
                    $"{nameof(DotCarouselProgressView)} requires {nameof(HorizontalLayoutGroup)} or {nameof(VerticalLayoutGroup)}. Make sure it is attached.");
            }

            _progressElementInstances = new List<DotCarouselProgressElement>(elementCount);
            for (var i = 0; i < elementCount; i++)
            {
                var instance = Instantiate(progressElementPrefab, transform);
                instance.SetActive(false);
                _progressElementInstances.Add(instance);
            }

            if (_activeIndex != -1)
            {
                SetActiveIndex(_activeIndex);
            }
        }

        /// <summary>
        ///     Set the active data index.
        /// </summary>
        /// <param name="elementIndex"></param>
        public override void SetActiveIndex(int elementIndex)
        {
            if (_activeIndex != -1 && _progressElementInstances.Count - 1 >= _activeIndex)
            {
                var instance = _progressElementInstances[_activeIndex];
                instance.SetActive(false);
            }

            if (_progressElementInstances.Count - 1 >= elementIndex)
            {
                var instance = _progressElementInstances[elementIndex];
                instance.SetActive(true);
            }

            _activeIndex = elementIndex;
        }
    }
}