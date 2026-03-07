using Editor.Components.AbstractClasses;
using Editor.Components.Windows;

namespace Editor.Model
{
    public class Application
    {
        private bool turnOff = false;
        public Stack<Window> WindowsInUse = new();

        public Application()
        {
            this.CreateWindow(new WindowList( () =>
            {
                this.TurnOff();
            }));

            Console.CursorVisible = false;
        }

        public void Run()
        {
            while (!this.turnOff)
            {
                this.WindowsInUse.Peek().Draw();

                ConsoleKeyInfo info = Console.ReadKey();

                this.WindowsInUse.Peek().HandleKey(info);

                Console.Clear();
            }
        }

        public void TurnOff()
        {
            this.turnOff = true;
        }

        public void ShutWindow()
        {
            this.WindowsInUse.Pop();
        }

        public void CreateWindow(Window window)
        {
            window.CreateWindowHandler += this.CreateWindow;
            window.ShutWindowHandler += this.ShutWindow;
            this.WindowsInUse.Push(window);
        }
    }
}
