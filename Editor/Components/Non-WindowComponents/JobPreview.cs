using System;
using System.Collections.Generic;
using System.Text;
using Editor.BackupData.Job;

namespace Editor.Components.Non_WindowComponents
{
    public class JobPreview : Preview
    {
        Dictionary<ConsoleKey, Action> keys = new Dictionary<ConsoleKey, Action>();
        public event Action<BackupJob>? IsSelected;
        public event Action? DeletePending;
        private BackupJob job;

        public JobPreview(BackupJob job)
        {
            this.job = job;

            this.keys[ConsoleKey.Spacebar] = this.Selected;
            this.keys[ConsoleKey.Q] = this.Delete;
        }

        public override void Draw()
        {
            Console.Write($"""
                ┌{"".PadRight(Console.WindowWidth - 2, '─')}┐
                {"│".PadRight(2, ' ')}{this.job.Name}{"│".PadLeft(Console.WindowWidth - 2 - this.job.Name.Length)}
                └{"".PadRight(Console.WindowWidth - 2, '─')}┘
                """
                );
        }

        public override void HandleKey(ConsoleKeyInfo info)
        {
            if (this.keys.ContainsKey(info.Key))
            {
                this.keys[info.Key].Invoke();
            }
        }

        private void Selected()
        {
            this.IsSelected?.Invoke(job);
        }

        private void Delete()
        {
            this.DeletePending?.Invoke();
        }
    }
}
