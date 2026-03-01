using System;
using System.Collections.Generic;
using System.Text;
using Editor.Components.Interfaces;
using Editor.Components.Windows.Editing;

namespace Editor.Components.Windows
{
    public class WindowPaths : IComponent
    {
        Dictionary<ConsoleKey, Action> keys = new Dictionary<ConsoleKey, Action>();
        private string label;
        private List<string> values = new List<string>();
        private bool isIn = false;
        private int selectedIndex = 0;

        public event Action<WindowPathChanging>? ChangePathRequested;
        public event Action? ExitRequested;
        public event Action? EntranceRequested;

        public WindowPaths(string label, List<string> values)
        {
            this.label = label;
            this.values = values;

            this.keys[ConsoleKey.UpArrow] = this.KeyUp;
            this.keys[ConsoleKey.DownArrow] = this.KeyDown;
            this.keys[ConsoleKey.Spacebar] = this.Select;
            this.keys[ConsoleKey.Escape] = this.Escape;
        }

        public void Draw()
        {
            Console.WriteLine(this.label);

            for (int i = 0; i < values.Count; i++)
            {
                if (isIn && i == selectedIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }

                Console.WriteLine($"[    {values[i]}");
                Console.ResetColor();
            }
        }

        public void HandleKey(ConsoleKeyInfo info)
        {
            if (!isIn)
            {
                if (info.Key == ConsoleKey.Spacebar)
                {
                    isIn = true;
                    this.EntranceRequested?.Invoke();
                }
            }
            else
            {
                if (this.keys.ContainsKey(info.Key))
                {
                    this.keys[info.Key]();
                }
            }
        }

        private void KeyUp()
        {
            selectedIndex = Math.Max(0, selectedIndex - 1);
        }

        private void KeyDown()
        {
            selectedIndex = Math.Min(values.Count - 1, selectedIndex + 1);
        }

        private void Escape()
        {
            isIn = false;
            this.ExitRequested?.Invoke();
        }

        private void Select()
        {
            this.ChangePathRequested?.Invoke(new WindowPathChanging(this.values[selectedIndex]));
        }
    }
}
