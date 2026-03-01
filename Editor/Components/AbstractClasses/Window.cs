using System;
using System.Collections.Generic;
using System.Text;

namespace Editor.Components.AbstractClasses
{
    public abstract class Window
    {
        public abstract void HandleKey(ConsoleKeyInfo keyInfo);
        public abstract void Draw();
    }
}
