using System;
using System.Collections.Generic;
using System.Text;
using Editor.Components.Interfaces;
using Editor.Model;

namespace Editor.Components.AbstractClasses
{
    public abstract class Window
    {
        public Application Application { get; set; } = new Application();
        public int SelectedComponent { get; set; } = 0;
        public Dictionary<ConsoleKey, Action> Keys { get; set; } = new Dictionary<ConsoleKey, Action>();
        public List<IComponent> Components { get; set; } = new List<IComponent>();

        protected Window()
        {
            this.Keys[ConsoleKey.UpArrow] = this.KeyUp;
            this.Keys[ConsoleKey.DownArrow] = this.KeyDown;
        }

        public void HandleKey(ConsoleKeyInfo keyInfo)
        {
            if (this.Keys.ContainsKey(keyInfo.Key))
            {
                this.Keys[keyInfo.Key].Invoke();
            }
            else
            {
                this.Components[this.SelectedComponent].HandleKey(keyInfo);
            }
        }

        public virtual void Draw()
        {
            for (int i = 0; i < this.Components.Count; i++)
            {
                if (i == this.SelectedComponent)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }

                this.Components[i].Draw();
                Console.ResetColor();
            }
        }

        private void KeyUp()
        {
            this.SelectedComponent = Math.Max(--this.SelectedComponent, 0);
        }

        private void KeyDown()
        {
            this.SelectedComponent = Math.Min(++this.SelectedComponent, this.Components.Count - 1);
        }
    }
}
