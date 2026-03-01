using Editor.BackupData;
using Editor.Components.AbstractClasses;
using Editor.Components.Windows;

namespace Editor.Model
{
    //┌┐─│└┘
    public class Application
    {
        public Stack<Window> WindowsInUse = new Stack<Window>();
        public List<BackupJob> Jobs = new List<BackupJob>();
        private bool turnOff = false;
        private ConfigFileManipulation configManipulator;
        public Application()
        {
            this.configManipulator = new ConfigFileManipulation();
            this.Jobs = configManipulator.PrepareJobs();

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
            configManipulator.SaveJobs(this.Jobs);
        }

        public void EditJob(BackupJob job, int index)
        {
            this.Jobs[index] = job;
            configManipulator.SaveJobs(this.Jobs);
        }

        public void AddJob(BackupJob job)
        {
            job.ID = this.Jobs.Count;
            this.Jobs.Add(job);
            configManipulator.SaveJobs(this.Jobs);
        }

        public void CreateWindow(Window window)
        {
            window.Application = this;
            this.WindowsInUse.Push(window);
        }
    }
}
