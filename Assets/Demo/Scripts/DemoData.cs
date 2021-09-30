namespace Demo.Scripts
{
    public class DemoData
    {
        public string SpriteResourceKey { get; }
        public string Text { get; }

        public DemoData(string spriteResourceKey, string text)
        {
            SpriteResourceKey = spriteResourceKey;
            Text = text;
        }
    }
}
