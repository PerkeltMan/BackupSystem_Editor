using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Editor.BackupData;
using Editor.Components.AbstractClasses;
using Editor.Components.Components;
using Editor.Components.Interfaces;
using Editor.Components.Non_WindowComponents;

namespace Editor.Components.Windows
{
    public class WindowEdit : WindowScroll
    {
        //private int jobID;
        private BackupJob job;

        public event Action<BackupJob>? SaveAction;

        public WindowEdit(BackupJob job, int jobID, Action<BackupJob>? saveAction)
        {
            this.job = job;
            //this.jobID = jobID;
            this.SaveAction = saveAction;

            this.Components.Add(new Textbox("Name", job.Name));
            this.Components.Add(new Textbox("Method", job.Method));
            this.Components.Add(new Textbox("Schedule", job.Timing));
            this.Components.Add(new Textbox("Count", job.Retention.Count.ToString()));
            this.Components.Add(new Textbox("Size", job.Retention.Size.ToString()));

            foreach (Textbox box in this.Components)
            {
                box.ValueChanged += (newValue) =>
                {
                    box.Value = newValue;
                    switch (box.Label)
                    {
                        case "Name":
                            this.job.Name = newValue;
                            break;
                        case "Method":
                            this.job.Method = newValue;
                            break;
                        case "Schedule":
                            this.job.Timing = newValue;
                            break;
                        case "Count":
                            if (int.TryParse(newValue, out int count))
                                this.job.Retention.Count = count;
                            break;
                        case "Size":
                            if (int.TryParse(newValue, out int size))
                                this.job.Retention.Size = size;
                            break;
                    }
                };
            }

            Textbox textboxName = new Textbox("Name", this.job.Name);
            textboxName.ValueChanged += (newValue) =>
            {
                this.job.Name = newValue;
            };
            this.Components.Add(textboxName);

            Button methodButton = new Button("Method");
            methodButton.Clicked += () =>
            {
                this.RequestCreateWindow(new WindowChoose("Choose a method"));             
            };

            Button sourceButton = new Button("Source");
            sourceButton.Clicked += () =>
            {
                //this.Application.CreateWindow(new WindowPathChanging(this.job.Sources));
            };
            this.Components.Add(sourceButton);

            Button targetButton = new Button("Destination");
            targetButton.Clicked += () =>
            {
                //this.Application.CreateWindow(new WindowPathChanging(this.job.Targets));
            };
            this.Components.Add(targetButton);

            Button btnSAve = new Button("Save");
            btnSAve.Clicked += () =>
            {
                WindowChoose confirm = new WindowChoose("Are you sure you want to save the changes?");
                confirm.Confirm += () =>
                {
                    this.SaveAction?.Invoke(this.job);
                    this.RequestShutWindow();
                    this.RequestShutWindow();
                };
                
                confirm.Cancel += () =>
                {
                    this.RequestShutWindow();
                };

                this.RequestCreateWindow(confirm);
            };
            this.Components.Add(btnSAve);

            Button btnCancel = new Button("Cancel");
            btnCancel.Clicked += () =>
            {
                this.RequestShutWindow();
            };
            this.Components.Add(btnCancel);
        }
    }
}
