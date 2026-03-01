using System;
using System.Collections.Generic;
using System.Text;

namespace Editor.Components.Components
{
    public class Textbox : IComponent
    {
        private string label;
        private string value;

        public event Action<string>? ValueChanged;

        public Textbox(string label, string value)
        {
            this.label = label;
            this.value = value;
        }

        public void Draw()
        {
            
        }

        public void HandleKey(ConsoleKeyInfo info)
        {
            throw new NotImplementedException();
        }
    }
}
