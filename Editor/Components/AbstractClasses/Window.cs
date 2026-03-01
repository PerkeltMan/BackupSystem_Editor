using System;
using System.Collections.Generic;
using System.Text;
using Editor.Model;

namespace Editor.Components.AbstractClasses
{
    public abstract class Window
    {
        public Application Application { get; set; }
        public abstract void HandleKey(ConsoleKeyInfo keyInfo);
        public abstract void Draw();
    }
}
