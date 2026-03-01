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
        public Stack<Window> WindowsInUse = new Stack<Window>();
        public List<BackupJob> Jobs = new List<BackupJob>();
        private bool turnOff = false;

        public Application()
        {
            ConfigReader configReader = new ConfigReader();
            this.Jobs = configReader.PrepareJobs();

            this.CreateWindow(new WindowList(this.Jobs));
            Console.CursorVisible = false;
        }

        public void Run()
        {
            while (!turnOff)
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

        public void DeleteJob(int index)
        {
            this.Jobs.RemoveAt(index);
        }

        public void CreateWindow(Window window)
        {
            window.Application = this;
            this.WindowsInUse.Push(window);
        }
    }
}
