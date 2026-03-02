using System;
using System.Collections.Generic;
using System.Text;
using Editor.BackupData;
using Editor.Components.AbstractClasses;
using Editor.Components.Non_WindowComponents;
using Editor.Components.Windows.Editing;
using Editor.Model;

namespace Editor.Components.Windows
{
    public class WindowList : Window
    {
        private int selectedPreview = 0;
        private List<BackupJob> backupJobs;
        private List<Preview> previews = new List<Preview>();
        private Dictionary<ConsoleKey, Action> keys = new Dictionary<ConsoleKey, Action>();

        public WindowList(List<BackupJob> jobs)
        {
            this.backupJobs = jobs;

            this.UpdatePreviews();

            this.keys[ConsoleKey.UpArrow] = this.KeyUp;
            this.keys[ConsoleKey.DownArrow] = this.KeyDown;
            this.keys[ConsoleKey.Escape] = this.Leave;
        }

        public void UpdatePreviews()
        {
            List<Preview> previews = new List<Preview>();

            for (int i = 0; i < this.backupJobs.Count; i++)
            {
                JobPreview jobPrev = new JobPreview(this.backupJobs[i]);
                jobPrev.IsSelected += this.SelectJob;
                jobPrev.DeletePending += this.Delete;

                previews.Add(jobPrev);
            }

            AddPreview addPrev = new AddPreview();
            addPrev.NewJob += this.CreateNewJob;
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
        }

        /// KEY HANDLING
        public override void HandleKey(ConsoleKeyInfo keyInfo)
        {
            if (this.keys.ContainsKey(keyInfo.Key))
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

        /// WINDOWS CREATION 
        private void SelectJob(BackupJob job)
        {
            WindowEdit windowEdit = new WindowEdit(job, this.selectedPreview, (editedJob) =>
            {
                this.Application.EditJob(editedJob, this.selectedPreview);
            });

            this.Application.CreateWindow(windowEdit);
        }

        private void CreateNewJob()
        {
            WindowEdit windowEdit = new WindowEdit(new BackupJob(), this.Application.Jobs.Count, (newJob) =>
            {
                this.Application.AddJob(newJob);
                this.UpdatePreviews();
            });

            this.Application.CreateWindow(windowEdit);
        }

        private void Delete()
        {
            string deleteMessage = "Are you sure you want to delete this configuration?";

            WindowChoose choose = new WindowChoose(deleteMessage);

            choose.Confirm += () =>
            {
                this.Application.DeleteJob(this.selectedPreview);
                this.UpdatePreviews();
                this.Application.WindowsInUse.Pop();
            };

            choose.Cancel += () =>
            {
                this.Application.WindowsInUse.Pop();
            };

            this.Application.CreateWindow(choose);
        }

        private void Leave()
        {
            string leaveMessage = "Are you sure you want to leave?";

            WindowChoose choose = new WindowChoose(leaveMessage);

            choose.Confirm += () =>
            {
                this.Application.TurnOff();
            };

            choose.Cancel += () =>
            {
                this.Application.WindowsInUse.Pop();
            };

            this.Application.CreateWindow(choose);
        }
    }
}
