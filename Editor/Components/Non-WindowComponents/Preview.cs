using System;
using System.Collections.Generic;
using System.Text;

namespace Editor.Components.Non_WindowComponents
{
    public abstract class Preview : IComponent
    {
        public abstract void Draw();


        public abstract void HandleKey(ConsoleKeyInfo info);
    }
}
