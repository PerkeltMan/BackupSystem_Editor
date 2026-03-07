using Editor.Components.Interfaces;

namespace Editor.Components.Components
{
    public class Textbox : IComponent
    {
        public string Value;
        public string Label;
        private Dictionary<ConsoleKey, Action> keys = new();

        public event Action<string>? ValueChanged;

        public Textbox(string label, string value, Action<string> valueChanged)
        {
            this.Value = value;
            this.Label = label;
            this.ValueChanged = valueChanged;

            this.keys[ConsoleKey.Backspace] = this.Backspace;
            this.keys[ConsoleKey.Enter] = this.Enter;
        }

        public void Draw()
        {
            Console.WriteLine(this.Label);
            Console.WriteLine("    " + this.Value);
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
