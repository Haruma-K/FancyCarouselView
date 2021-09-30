using System.Linq;
using UnityEngine;

namespace Demo.Scripts
{
    public class Demo : MonoBehaviour
    {
        [SerializeField] private DemoCarouselView _carouselView = default;
        [SerializeField, Range(1, 3)] private int _bannerCount = 3;

        private void Awake()
        {
            _carouselView.ActiveCellChanged += OnActiveCellChanged;
            _carouselView.CarouselCellInstantiated += OnCellInstantiated;
            _carouselView.CarouselCellRefreshed += OnCellWillRefreshed;
            _carouselView.CarouselCellVisibilityChanged += OnCellVisibilityChanged;
        }

        private void Start()
        {
            var items = Enumerable.Range(0, _bannerCount)
                .Select(i =>
                {
                    var spriteResourceKey = $"tex_demo_banner_{i:D2}";
                    var text = $"Demo Banner {i:D2}";
                    return new DemoData(spriteResourceKey, text);
                })
                .ToArray();
            

            _carouselView.Setup(items);
        }

        private void OnDestroy()
        {
            _carouselView.ActiveCellChanged -= OnActiveCellChanged;
            _carouselView.CarouselCellInstantiated -= OnCellInstantiated;
            _carouselView.CarouselCellRefreshed -= OnCellWillRefreshed;
            _carouselView.CarouselCellVisibilityChanged -= OnCellVisibilityChanged;
        }

        private void OnActiveCellChanged(int cellIndex)
        {
        }

        private void OnCellInstantiated(DemoCarouselCell cell)
        {
        }

        private void OnCellWillRefreshed(DemoCarouselCell cell)
        {
            //Debug.Log($"OnCarouselCellContentUpdated: {cell.Index} / {cell.gameObject.GetHashCode()}");
        }

        private void OnCellVisibilityChanged(DemoCarouselCell cell, bool visibility)
        {
            //Debug.Log($"OnCarouselCellVisibilityChanged: {cell.Index} ({visibility})");
        }
    }
}