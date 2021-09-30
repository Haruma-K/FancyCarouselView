using UnityEngine;
using UnityEngine.UI;

namespace FancyCarouselView.Runtime.Scripts
{
    /// <summary>
    ///     Element of the <see cref="DotCarouselProgressView" />.
    /// </summary>
    public sealed class DotCarouselProgressElement : MonoBehaviour
    {
        [SerializeField] private Image _image = default;
        [SerializeField] private Color _activeColor = Color.white;
        [SerializeField] private Color _deactiveColor = Color.grey;

        /// <summary>
        ///     Active or not.
        /// </summary>
        public bool IsActive { get; private set; }

        /// <summary>
        ///     Set whether or not it is active.
        /// </summary>
        /// <param name="isActive"></param>
        public void SetActive(bool isActive)
        {
            _image.color = isActive ? _activeColor : _deactiveColor;
            IsActive = isActive;
        }
    }
}