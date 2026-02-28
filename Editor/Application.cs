using System;
using System.Collections.Generic;
using System.Text;
using Editor.BackupData;
using Editor.BackupData.Job;
using Editor.Components;
using Editor.Components.Windows;

namespace Editor
{
    public class Application
    {
        private Stack<Window> windowsInUse = new Stack<Window>();
        private bool turnOff = false;
        public List<BackupJob> Jobs = new List<BackupJob>();

        public Application()
        { 
            ConfigReader reader = new ConfigReader();
            this.Jobs = reader.PrepareJobs();

            this.CreateWindow(new WindowChoose());
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

        public void CreateWindow(Window window)
        {
            window.Application = this;
            this.windowsInUse.Push(window);
        }
    }
}
