using System;
using System.Collections.Generic;
using System.Text;
using Editor.Components;
using Editor.Components.Windows;

namespace Editor
{
    public class Application
    {
        private Stack<AWindow> windowsInUse = new Stack<AWindow>();
        private bool turnOff = false;

        public Application()
        {
            AWindow window = new WindowChoose();
            window.Application = this;
        }

        public void Run()
        {
            while (!turnOff)
            {
                ConsoleKeyInfo info = Console.ReadKey();

                this.windowsInUse.Peek().HandleKey(info);
            }
        }

        public void TurnOff()
        {
            this.turnOff = true;
        }

        public void WindowSwitch(Window window)
        {

        }
    }
}
