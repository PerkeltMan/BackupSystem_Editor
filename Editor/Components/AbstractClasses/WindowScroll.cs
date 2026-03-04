using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor.Components.AbstractClasses
{
    public abstract class WindowScroll : Window
    {
        protected WindowScroll()
        {
            this.Keys[ConsoleKey.UpArrow] = this.KeyUp;
            this.Keys[ConsoleKey.DownArrow] = this.KeyDown;
        }

        private void KeyUp()
        {
            this.SelectedComponent = Math.Max(--this.SelectedComponent, 0);
        }

        private void KeyDown()
        {
            this.SelectedComponent = Math.Min(++this.SelectedComponent, this.Components.Count - 1);
        }
    }
}
