using Editor.Components.Interfaces;

namespace Editor.Components.Components
{
    
    public class Button : IComponent
    {
        private string text;
        public event Action? Clicked;

        public Button(string text, Action clicked)
        {
            this.text = text;
            this.Clicked = clicked;
        }

        public void Draw()
        {
            Console.Write($" [ {this.text} ] ");
        }

        public void HandleKey(ConsoleKeyInfo info)
        {
            if (info.Key == ConsoleKey.Spacebar)
            {
                this.Clicked?.Invoke();
            }
            else return;
        }

        public override string ToString()
        {
            return this.text;
        }
    }
}
