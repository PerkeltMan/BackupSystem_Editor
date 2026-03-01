using System;
using System.Collections.Generic;
using System.Text;

namespace Editor.Components
{
    public abstract class Textbox : IComponent
    {
        public abstract void Draw();
        public abstract void HandleKey(ConsoleKeyInfo info);
    }
}
