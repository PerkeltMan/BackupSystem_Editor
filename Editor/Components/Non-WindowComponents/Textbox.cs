using System;
using System.Collections.Generic;
using System.Text;

namespace Editor.Components.Components
{
    public class Textbox : IComponent
    {
        private string label;
        private string value;
        private Dictionary<ConsoleKey, Action> keys = new Dictionary<ConsoleKey, Action>();

        public event Action<string>? ValueChanged;

        public Textbox(string label, string value)
        {
            this.label = label;
            this.value = value;

            this.keys[ConsoleKey.Backspace] = this.Backspace;
            this.keys[ConsoleKey.Enter] = this.Enter;
        }

        public void Draw()
        {
            Console.WriteLine(this.label);
            Console.WriteLine("[ " + this.value + " ]");
        }

        public void HandleKey(ConsoleKeyInfo info)
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
