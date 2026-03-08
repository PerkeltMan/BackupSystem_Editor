using Editor.BackupData;
using Editor.Components.AbstractClasses;
using Editor.Components.Components;
using Editor.Components.Non_WindowComponents;
using Editor.Model;

namespace Editor.Components.Windows
{
    public class WindowList : Window
    {
        private List<BackupJob> backupJobs = new();
        private ConfigFileManipulation configFileManipulator = new();

        private event Action TurnOff;

        public WindowList(Action turnOff)
        {
            this.TurnOff = turnOff;

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

            Button addJob = new Button("add job", () => this.CreateNewJob());

            this.Components.Add(addJob);
        }

        /// WINDOWS CREATION 
        private void SelectJob(BackupJob job)
        {
            WindowEdit windowEdit = new WindowEdit(job, (editedJob) =>
            {
                this.backupJobs[this.SelectedComponent] = editedJob;
                this.SaveChanges();
            });

            this.RequestCreateWindow(windowEdit);
        }

        private void CreateNewJob()
        {
            WindowEdit windowEdit = new WindowEdit(new BackupJob(), (newJob) =>
            {
                this.backupJobs.Add(newJob);
                this.SaveChanges();
            });

            this.RequestCreateWindow(windowEdit);
        }

        private void Delete()
        {
            string deleteMessage = "Are you sure you want to delete this configuration?";

            
            WindowChoose choose = new WindowChoose(deleteMessage,
                ("Yes", () => { 
                    this.backupJobs.RemoveAt(this.SelectedComponent); 
                    this.SaveChanges();  
                    this.RequestShutWindow(); 
                }),
                ("No", () => this.RequestShutWindow())
                );

            this.RequestCreateWindow(choose);
        }

        private void Leave()
        {
            string leaveMessage = "Are you sure you want to leave?";

            WindowChoose leaveWindow = new WindowChoose(leaveMessage,
                ("Yes", () => this.TurnOff.Invoke()),
                ("No", () => this.RequestShutWindow())
                );

            this.RequestCreateWindow(leaveWindow);
        }

        private void SaveChanges()
        {
            this.configFileManipulator.SaveJobs(this.backupJobs);
            this.UpdatePreviews();
        }
    }
}
