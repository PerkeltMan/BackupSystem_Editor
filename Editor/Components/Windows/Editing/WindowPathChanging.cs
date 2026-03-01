using System;
using System.Collections.Generic;
using System.Text;
using Editor.Components.AbstractClasses;

namespace Editor.Components.Windows.Editing
{
    public class WindowPathChanging : Window
    {
        private List<string> paths;
        public WindowPathChanging(List<string> paths)
        {
            this.paths = paths;
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
