using System;
using System.Collections.Generic;
using System.Text;
using Editor.Components;

namespace Editor
{
    public class Application
    {
        private Stack<AWindow> windowsInUse = new Stack<AWindow>();
        private bool turnOff = false;

        public Application()
        {
            this.windowsInUse.Push();
        }

        public void Run()
        {
            while (!turnOff)
            {

            }
        }
    }
}
