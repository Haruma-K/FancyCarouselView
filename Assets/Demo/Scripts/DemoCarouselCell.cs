using FancyCarouselView.Runtime.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Demo.Scripts
{
    public class DemoCarouselCell : CarouselCell<DemoData, DemoCarouselCell>
    {
        [SerializeField] private Image _image;
        [SerializeField] private Text _text;
        [SerializeField] private Button _button;

        private DemoData _data;

        protected override void Refresh(DemoData data)
        {
            _data = data;
            _image.sprite = Resources.Load<Sprite>(data.SpriteResourceKey);
            _text.text = data.Text;
        }

        protected override void OnVisibilityChanged(bool visibility)
        {
            if (visibility)
                _button.onClick.AddListener(OnClick);
            else
                _button.onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            _data?.Clicked?.Invoke();
        }
    }
}
