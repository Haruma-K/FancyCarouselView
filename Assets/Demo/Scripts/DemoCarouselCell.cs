using FancyCarouselView.Runtime.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Demo.Scripts
{
    public class DemoCarouselCell : CarouselCell<DemoData, DemoCarouselCell>
    {
        [SerializeField] private Image _image = default;
        [SerializeField] private Text _text = default;

        protected override void Refresh(DemoData data)
        {
            _image.sprite = Resources.Load<Sprite>(data.SpriteResourceKey);
            _text.text = data.Text;
        }
    }
}