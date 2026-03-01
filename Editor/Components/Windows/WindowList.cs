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
        public WindowList()
        {
            ConfigReader reader = new ConfigReader();
            this.jobs = reader.PrepareJobs();

            this.UpdatePreviews();

            this.keys[ConsoleKey.UpArrow] = this.KeyUp;
            this.keys[ConsoleKey.DownArrow] = this.KeyDown;
        }

        private void UpdatePreviews()
        {
            List<Preview> previews = new List<Preview>();

            for (int i = 0; i < this.jobs.Count; i++)
            {
                JobPreview jobPrev = new JobPreview(this.jobs[i]);
                jobPrev.IsSelected += this.Selected;
                jobPrev.DeletePending += this.Delete;

                previews.Add(jobPrev);
            }

            AddPreview addPrev = new AddPreview();
            addPrev.NewJob += this.NewJob;
            previews.Add(addPrev);

            this.previews = previews;
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
            else this.previews[selectedPreview].HandleKey(keyInfo);
        }

        private void KeyUp()
        {
            this.selectedPreview = Math.Max(--this.selectedPreview, 0);
        }

        private void KeyDown()
        {
            this.selectedPreview = Math.Min(++this.selectedPreview, this.previews.Count - 1);
        }

        private void Selected(BackupJob job)
        {
            this.Application.CreateWindow(new WindowEdit(job));
        }


        // the last preview will always be the add new preview
        // cannot be deleted
        private void Delete(BackupJob job)
        {
            this.Application.CreateWindow(new WindowDelete(job));
        }

        private void NewJob()
        {
            this.Application.CreateWindow(new WindowAccept());
        }
    }
}
