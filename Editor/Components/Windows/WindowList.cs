using System;
using System.Collections.Generic;
using System.Text;
using Editor.BackupData;
using Editor.BackupData.Job;
using Editor.Components.Non_WindowComponents;

namespace Editor.Components.Windows
{
    public class WindowList : Window
    {
        private List<BackupJob> jobs;
        private List<Preview> previews = new List<Preview>();
        private int selectedPreview = 0;
        private Dictionary<ConsoleKey, Action> keys = new Dictionary<ConsoleKey, Action>();
        public event Action<int>? Selected;
        public WindowList()
        {
            ConfigReader reader = new ConfigReader();
            this.jobs = reader.PrepareJobs();

            foreach (BackupJob job in this.jobs)
            {
                this.previews.Add(new Preview(job.Name));
            }

            this.previews.Add(new Preview("add"));

            this.keys[ConsoleKey.Spacebar] = this.Select;
            this.keys[ConsoleKey.UpArrow] = this.KeyUp;
            this.keys[ConsoleKey.DownArrow] = this.KeyDown;
            this.keys[ConsoleKey.Q] = this.Delete;
        }

        public override void Draw()
        {
            for (int i = 0; i < this.previews.Count; i++)
            {
                if (i == this.selectedPreview)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }

                this.previews[i].Draw();
                Console.ResetColor();
            }

            /*foreach (Preview prev in this.previews)
            {
                if (this.selectedPreview == this.previews.IndexOf(prev))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }

                prev.Draw();
                Console.ResetColor();
            }*/
        }

        public override void HandleKey(ConsoleKeyInfo keyInfo)
        {
            if (keys.ContainsKey(keyInfo.Key))
            {
                this.keys[keyInfo.Key].Invoke();
            }
        }

        private void Select()
        {
            this.Selected?.Invoke(this.selectedPreview);
        }

        private void KeyUp()
        {
            this.selectedPreview = Math.Max(--this.selectedPreview, 0);
        }

        private void KeyDown()
        {
            this.selectedPreview = Math.Min(++this.selectedPreview, this.previews.Count - 1);
        }

        // the last preview will always be the add new preview
        // cannot be deleted
        private void Delete()
        {
            if (this.selectedPreview == this.previews.Count - 1)
            {
                return;
            }

            this.previews.RemoveAt(this.selectedPreview);
        }
    }
}
