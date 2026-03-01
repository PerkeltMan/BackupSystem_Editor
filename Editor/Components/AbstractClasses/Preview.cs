using System;
using System.Collections.Generic;
using System.Text;
using Editor.Components.Interfaces;

namespace Editor.Components.AbstractClasses
{
    public abstract class Preview : IComponent
    {
        public abstract void Draw();


        public abstract void HandleKey(ConsoleKeyInfo info);
    }
}
