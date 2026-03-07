using Editor.BackupData;
using Editor.Components.AbstractClasses;
using Editor.Components.Non_WindowComponents;
using Editor.Model;

namespace Editor.Components.Windows
{
    public class WindowList : WindowScroll
    {
        private List<BackupJob> backupJobs;
        private ConfigFileManipulation configFileManipulator = new();

        private event Action turnOff;

        public WindowList(Action turnOff)
        {
            this.turnOff = turnOff;

            this.backupJobs = configFileManipulator.PrepareJobs();

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
                this.backupJobs[this.SelectedComponent] = editedJob;
                this.SaveChanges();
            });

            this.RequestCreateWindow(windowEdit);
        }

        private void CreateNewJob()
        {
            WindowEdit windowEdit = new WindowEdit(new BackupJob(), this.backupJobs.Count, (newJob) =>
            {
                this.backupJobs.Add(newJob);
                this.SaveChanges();
            });

            this.RequestCreateWindow(windowEdit);
        }

        private void Delete()
        {
            string deleteMessage = "Are you sure you want to delete this configuration?";

            WindowChoose choose = new WindowChoose(deleteMessage);

            choose.Confirm += () =>
            {
                this.backupJobs.RemoveAt(this.SelectedComponent);
                this.SaveChanges();
                this.RequestShutWindow();
            };

            choose.Cancel += () =>
            {
                this.RequestShutWindow();
            };

            this.RequestCreateWindow(choose);
        }

        private void Leave()
        {
            string leaveMessage = "Are you sure you want to leave?";

            WindowChoose choose = new WindowChoose(leaveMessage);

            choose.Confirm += () =>
            {
                this.turnOff.Invoke();
            };

            choose.Cancel += () =>
            {
                this.RequestShutWindow();
            };

            this.RequestCreateWindow(choose);
        }

        private void SaveChanges()
        {
            this.configFileManipulator.SaveJobs(this.backupJobs);
            this.UpdatePreviews();
        }
    }
}
