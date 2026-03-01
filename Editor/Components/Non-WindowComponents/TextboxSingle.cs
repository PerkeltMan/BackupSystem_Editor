using System;
using System.Collections.Generic;
using System.Text;

namespace Editor.Components.Components
{
    public class TextboxSingle : Textbox
    {
        private string value;
        private string label;
        private Dictionary<ConsoleKey, Action> keys = new Dictionary<ConsoleKey, Action>();

        public event Action<string>? ValueChanged;

        public TextboxSingle(string label, string value)
        {
            this.value = value;
            this.label = label;

            this.keys[ConsoleKey.Backspace] = this.Backspace;
            this.keys[ConsoleKey.Enter] = this.Enter;
        }

        public override void Draw()
        {
            Console.WriteLine(this.label);
            Console.WriteLine("[ " + this.value);
        }

        public override void HandleKey(ConsoleKeyInfo info)
        {
            if (this.keys.ContainsKey(info.Key))
            {
                this.keys[info.Key]();
            }
            else if (!char.IsControl(info.KeyChar))
            {
                this.value += info.KeyChar;
                this.ValueChanged?.Invoke(this.value);
            }
        }

        private void Backspace()
        {
            if (this.value.Length > 0)
            {
                this.value = this.value.Substring(0, this.value.Length - 1);
                this.ValueChanged?.Invoke(this.value);
            }
        }

        private void Enter()
        {
            // Do nothing for now
        }
    }
}
