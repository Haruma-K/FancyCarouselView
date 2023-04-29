using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Demo.Scripts
{
    public class Demo : MonoBehaviour
    {
        [SerializeField] private DemoCarouselView _carouselView;
        [SerializeField] [Range(1, 3)] private int _bannerCount = 3;
        [SerializeField] private Button _setupButton;
        [SerializeField] private Button _cleanupButton;

        private bool _isSetup;

        private void Start()
        {
            _setupButton.onClick.AddListener(Setup);
            _cleanupButton.onClick.AddListener(Cleanup);

            Setup();
        }

        private void Setup()
        {
            if (_isSetup)
                return;

            var items = Enumerable.Range(0, _bannerCount)
                .Select(i =>
                {
                    var spriteResourceKey = $"tex_demo_banner_{i:D2}";
                    var text = $"Demo Banner {i:D2}";
                    return new DemoData(spriteResourceKey, text, () => Debug.Log($"Clicked: {text}"));
                })
                .ToArray();
            _carouselView.Setup(items);
            _isSetup = true;
        }

        private void Cleanup()
        {
            if (!_isSetup)
                return;

            _carouselView.Cleanup();
            _isSetup = false;
        }
    }
}
