using System.Net.Security;
using Editor.Components.Interfaces;
using Editor.Model;

namespace Editor.Components.AbstractClasses
{
    public abstract class Window
    {
        public int SelectedComponent { get; set; } = 0;
        public Dictionary<ConsoleKey, Action> Keys { get; set; } = new();
        //public Dictionary<(int x, int y) IComponent> Components = new();
        public IComponent[,] ComponentDrawing { get; set; } = new IComponent[,] { };
        public List<IComponent> Components { get; set; } = new();

        public event Action<Window>? CreateWindowHandler;
        public event Action? ShutWindowHandler;

        protected Window()
        {
            //this.Keys[ConsoleKey.UpArrow] = this.KeyUp;
            //this.Keys[ConsoleKey.RightArrow] = this.;
            
        }

        public void HandleKey(ConsoleKeyInfo keyInfo)
        {
            if (this.Keys.ContainsKey(keyInfo.Key))
            {
                this.Keys[keyInfo.Key].Invoke();
            }
            else
            {
                if (this.Components.Count > 0)
                    this.Components[this.SelectedComponent].HandleKey(keyInfo);
            }
        }

        // 3 may seem random, but 
        public void FillLocations()
        {
            int width = 3;

            for (int i = 0; i < Components.Count; i++)
            {
                int x = i % width;
                int y = i / width;

                this.ComponentDrawing[x, y] = ComponentDrawing[x, y];
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

        protected void RequestCreateWindow(Window window)
        {
            this.CreateWindowHandler?.Invoke(window);
        }

        protected void RequestShutWindow()
        {
            this.ShutWindowHandler?.Invoke();
        }

        public void KeyUp()
        {
            this.SelectedComponent = Math.Max(--this.SelectedComponent, 0);
        }

        public void KeyDown()
        {
            this.SelectedComponent = Math.Min(++this.SelectedComponent, this.Components.Count - 1);
        }
    }
}

