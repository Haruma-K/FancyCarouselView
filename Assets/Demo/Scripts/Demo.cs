using System.Linq;
using UnityEngine;

namespace Demo.Scripts
{
    public class Demo : MonoBehaviour
    {
        [SerializeField] private DemoCarouselView _carouselView;
        [SerializeField] [Range(1, 3)] private int _bannerCount = 3;

        private void Start()
        {
            var items = Enumerable.Range(0, _bannerCount)
                .Select(i =>
                {
                    var spriteResourceKey = $"tex_demo_banner_{i:D2}";
                    var text = $"Demo Banner {i:D2}";
                    return new DemoData(spriteResourceKey, text, () => Debug.Log($"Clicked: {text}"));
                })
                .ToArray();
            _carouselView.Setup(items);
        }
    }
}
