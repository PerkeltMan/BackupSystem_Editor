using System;
using System.Collections.Generic;
using System.Text;
using Editor.BackupData;
using Editor.Components.AbstractClasses;

namespace Editor.Components.Non_WindowComponents
{
    public class JobPreview : Preview
    {
        private BackupJob job;
        Dictionary<ConsoleKey, Action> keys = new();

        public event Action<BackupJob>? GotSelected;
        public event Action? DeleteRequested;

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
            this.GotSelected?.Invoke(this.job);
        }

        private void Delete()
        {
            this.DeleteRequested?.Invoke();
        }
    }
}
