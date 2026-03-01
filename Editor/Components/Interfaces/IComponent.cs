using System;
using System.Collections.Generic;
using System.Text;

namespace Editor.Components.Interfaces
{
    public interface IComponent
    {
        public void HandleKey(ConsoleKeyInfo info);
        public void Draw();
    }
}
