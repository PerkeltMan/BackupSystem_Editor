using System;
using System.Collections.Generic;
using System.Text;
using Editor.BackupData;
using Editor.BackupData.Job;
using Editor.Components;
using Editor.Components.Windows;

namespace Editor
{
    //┌┐─│└┘
    public class Application
    {
        private Stack<Window> windowsInUse = new Stack<Window>();
        private bool turnOff = false;
        public List<BackupJob> Jobs = new List<BackupJob>();

        public Application()
        {
            Console.CursorVisible = false;
            this.CreateWindow(new WindowList());
        }

        public void Run()
        {
            while (!turnOff)
            {
                this.windowsInUse.Peek().Draw();

                ConsoleKeyInfo info = Console.ReadKey();

                this.windowsInUse.Peek().HandleKey(info);

                Console.Clear();
            }
        }

        public void TurnOff()
        {
            this.turnOff = true;
        }

        public void CreateWindow(Window window)
        {
            window.Application = this;
            this.windowsInUse.Push(window);
        }
    }
}
