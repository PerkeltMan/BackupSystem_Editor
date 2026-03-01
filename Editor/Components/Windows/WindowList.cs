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
        private List<BackupJob> backupJobs;
        private int selectedPreview = 0;
        private List<Preview> previews = new List<Preview>();
        private Dictionary<ConsoleKey, Action> keys = new Dictionary<ConsoleKey, Action>();
        private Application application;

        public WindowList(List<BackupJob> jobs, Application application)
        {
            this.application = application;
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
                jobPrev.IsSelected += this.Selected;
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

        /// WINDOWS CREATION
        private void Selected(BackupJob job)
        {
            WindowEdit windowEdit = new WindowEdit(job);

            windowEdit.JobEdited += (BackupJob job) =>
            {
                this.application.EditJob(job, this.selectedPreview);
                this.application.WindowsInUse.Pop();
            };

            windowEdit.EditCanceled += () =>
            {
                this.application.WindowsInUse.Pop();
            };

            windowEdit.ChangePathRequested += (WindowChangePath changePath) =>
            {
                this.application.CreateWindow(changePath);
            };

            this.application.CreateWindow(new WindowEdit(job));
        }

        private void Delete()
        {
            string deleteMessage = "Are you sure you want to delete this configuration?";

            WindowChoose choose = new WindowChoose(deleteMessage);

            choose.Confirm += () =>
            {
                this.application.DeleteJob(this.selectedPreview);
                this.UpdatePreviews();   
                this.application.WindowsInUse.Pop();
            };

            choose.Cancel += () =>
            {
                this.application.WindowsInUse.Pop();
            };

            this.application.CreateWindow(choose);
        }

        private void CreateNewJob()
        {
            this.application.CreateWindow(new WindowAdd());
        }

        private void Leave()
        {
            string leaveMessage = "Are you sure you want to leave?";

            WindowChoose choose = new WindowChoose(leaveMessage);

            choose.Confirm += () =>
            {
                this.application.TurnOff();
            };

            choose.Cancel += () =>
            {
                this.application.WindowsInUse.Pop();
            };

            this.application.CreateWindow(choose);
        }
    }
}
