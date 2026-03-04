using Editor.BackupData;
using Editor.Components.AbstractClasses;
using Editor.Components.Non_WindowComponents;

namespace Editor.Components.Windows
{
    public class WindowList : WindowScroll
    {
        private List<BackupJob> backupJobs;

        public WindowList(List<BackupJob> jobs)
        {
            this.backupJobs = jobs;

            this.UpdatePreviews();

            this.Keys[ConsoleKey.Escape] = this.Leave;
        }

        public void UpdatePreviews()
        {
            this.Components.Clear();

            for (int i = 0; i < this.backupJobs.Count; i++)
            {
                JobPreview jobPrev = new JobPreview(this.backupJobs[i]);
                jobPrev.GotSelected += this.SelectJob;
                jobPrev.DeleteRequested += this.Delete;

                this.Components.Add(jobPrev);
            }

            AddPreview addPrev = new AddPreview();
            addPrev.NewJob += this.CreateNewJob;

            this.Components.Add(addPrev);
        }

        /// WINDOWS CREATION 
        private void SelectJob(BackupJob job)
        {
            WindowEdit windowEdit = new WindowEdit(job, this.SelectedComponent, (editedJob) =>
            {
                this.Application.EditJob(editedJob, this.SelectedComponent);
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
                this.Application.DeleteJob(this.SelectedComponent);
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
