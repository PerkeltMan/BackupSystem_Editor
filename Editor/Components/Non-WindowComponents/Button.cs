using System;
using System.Collections.Generic;
using System.Text;
using Editor.Components.Interfaces;

namespace Editor.Components.Components
{
    public class Button : IComponent
    {
        public string Text;
        public event Action? Clicked;
        public Button(string text)
        {
            this.Text = text;
        }

        public void Draw()
        {
            Console.WriteLine($"[ {this.Text} ]");
        }

        public void HandleKey(ConsoleKeyInfo info)
        {
            if (info.Key == ConsoleKey.Spacebar)
            {
                this.Clicked?.Invoke();
            }
        }
    }
}
