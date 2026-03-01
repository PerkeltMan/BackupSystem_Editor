using System;
using System.Collections.Generic;
using System.Text;

namespace Editor.Components.Windows
{
    public class WindowChangePath : Window
    {
        private string path;
        public WindowChangePath(string path)
        {
            this.path = path;
        }
        public override void Draw()
        {
            throw new NotImplementedException();
        }

        public override void HandleKey(ConsoleKeyInfo keyInfo)
        {
            throw new NotImplementedException();
        }
    }
}
