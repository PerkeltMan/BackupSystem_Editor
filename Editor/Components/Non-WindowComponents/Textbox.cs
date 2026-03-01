using System;
using System.Collections.Generic;
using System.Text;
using Editor.Components.AbstractClasses;

namespace Editor.Components.Components
{
    public class TextboxSingle
    {
        public string Value;
        private string label;
        private Dictionary<ConsoleKey, Action> keys = new Dictionary<ConsoleKey, Action>();

        public event Action<string>? ValueChanged;

        public TextboxSingle(string label, string value)
        {
            this.Value = value;
            this.label = label;

            this.keys[ConsoleKey.Backspace] = this.Backspace;
            this.keys[ConsoleKey.Enter] = this.Enter;
        }

        public void Draw()
        {
            Console.WriteLine(this.label);
            Console.WriteLine("[ " + this.Value);
        }

        public void HandleKey(ConsoleKeyInfo info)
        {
            if (this.keys.ContainsKey(info.Key))
            {
                this.keys[info.Key]();
            }
            else if (!char.IsControl(info.KeyChar))
            {
                this.Value += info.KeyChar;
                this.ValueChanged?.Invoke(this.Value);
            }
        }

        private void Backspace()
        {
            if (this.Value.Length > 0)
            {
                this.Value = this.Value.Substring(0, this.Value.Length - 1);
                this.ValueChanged?.Invoke(this.Value);
            }
        }

        private void Enter()
        {
            // Do nothing for now
        }
    }
}
