using Editor.BackupData;
using Editor.Components.AbstractClasses;
using Editor.Components.Windows;

namespace Editor.Model
{
    public class Application
    {
        private bool turnOff = false;
        private ConfigFileManipulation configManipulator;
        public Stack<Window> WindowsInUse = new();
        public List<BackupJob> Jobs = new();

        public Application()
        {
            this.configManipulator = new();
            this.Jobs = configManipulator.PrepareJobs();

            this.CreateWindow(new WindowList(this.Jobs));
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

        public void DeleteJob(int index)
        {
            this.Jobs.RemoveAt(index);
            this.configManipulator.SaveJobs(this.Jobs);
        }

        public void EditJob(BackupJob job, int index)
        {
            this.Jobs[index] = job;
            this.configManipulator.SaveJobs(this.Jobs);
        }

        public void AddJob(BackupJob job)
        {
            job.ID = this.Jobs.Count;
            this.Jobs.Add(job);
            this.configManipulator.SaveJobs(this.Jobs);
        }

        public void CreateWindow(Window window)
        {
            window.Application = this;
            this.WindowsInUse.Push(window);
        }
    }
}
