using Editor.BackupData;
using Editor.Components.AbstractClasses;
using Editor.Components.Components;

namespace Editor.Components.Windows
{
    public class WindowEdit : WindowScroll
    {
        //private int jobID;
        private BackupJob job = new BackupJob();
        private int jobID;

        public event Action<BackupJob>? SaveAction;

        public WindowEdit(BackupJob job, int jobID, Action<BackupJob>? saveAction)
        {
            this.job = new BackupJob
            {
                Sources = job.Sources,
                Targets = job.Targets, 
                Name = job.Name,
                Method = job.Method,
                Timing = job.Timing,
                Retention = new Retention
                {
                    Size = job.Retention.Size,
                    Count = job.Retention.Count
                }
            };

            this.jobID = jobID;
            this.SaveAction = saveAction;

            this.Components.Add(new Textbox("Name", this.job.Name, (editedName) => this.job.Name = editedName));

            this.Components.Add(new Button("Method: " + this.job.Method , () =>
            {
                var options = new (string label, Action action)[]
                {
                    ("Full", () => this.SetMethod("Full")),
                    ("Incremental", () => this.SetMethod("Incremental")),
                    ("Differential", () => this.SetMethod("Differential"))
                };

                this.RequestCreateWindow(new WindowChoose("Select backup method", options));
            }
            ));

            this.Components.Add(new Textbox("Timing", this.job.Timing, (timing) => this.job.Timing = timing));

            RetentionBoxes("Package size", this.job.Retention.Size, newValue => this.job.Retention.Size = newValue);
            RetentionBoxes("Package count", this.job.Retention.Count, newValue => this.job.Retention.Count = newValue);

            this.Components.Add(new Button("Sources", () => this.RequestCreateWindow(new WindowPathEditor(this.job.Sources, (sources) => this.job.Sources = sources))));
            this.Components.Add(new Button("Targets", () => this.RequestCreateWindow(new WindowPathEditor(this.job.Sources, (targets) => this.job.Targets = targets))));

            this.Components.Add(new Button("Save", () =>
            {
                this.RequestCreateWindow(new WindowChoose("Are you sure you want to save these changes?",
                ("Yes", () => { this.SaveAction?.Invoke(this.job); this.RequestShutWindow(); this.RequestShutWindow(); }),
                ("No", () => this.RequestShutWindow())
                ));
            }
            ));

            this.Components.Add(new Button("Cancel", () => this.RequestShutWindow()));
        }

        private void SetMethod(string method)
        {
            this.job.Method = method;
            this.RequestShutWindow();
        }

        private void RetentionBoxes(string label, int value, Action<int> func)
        {
            this.Components.Add(new Textbox(label, value.ToString(), (newValue) =>
            {
                if (string.IsNullOrEmpty(newValue) || newValue == "0" || this.job.Method == "full")
                    newValue = "1";

                func(int.Parse(newValue));
            }));
        }
    }
}
