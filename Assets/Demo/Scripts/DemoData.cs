using System;

namespace Demo.Scripts
{
    public class DemoData
    {
        public string SpriteResourceKey { get; }
        public string Text { get; }
        public Action Clicked { get; }

        public DemoData(string spriteResourceKey, string text, Action clicked)
        {
            SpriteResourceKey = spriteResourceKey;
            Text = text;
            Clicked = clicked;
        }
    }
}
