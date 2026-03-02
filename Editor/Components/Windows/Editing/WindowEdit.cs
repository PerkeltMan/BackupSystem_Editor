using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Editor.BackupData;
using Editor.Components.AbstractClasses;
using Editor.Components.Components;
using Editor.Components.Interfaces;
using Editor.Components.Non_WindowComponents;

namespace Editor.Components.Windows.Editing
{
    public class WindowEdit : Window
    {
        private int jobID;
        private int selectedComponent = 0;
        private BackupJob job;
        private Dictionary<ConsoleKey, Action> keys = new Dictionary<ConsoleKey, Action>();
        private List<IComponent> components = new List<IComponent>();

        public event Action<BackupJob>? SaveAction;

        public WindowEdit(BackupJob job, int jobID, Action<BackupJob>? saveAction)
        {
            this.job = job;
            this.jobID = jobID;
            this.SaveAction = saveAction;

            List<Textbox> textboxes = new List<Textbox>();
            textboxes.Add(new Textbox("Name", job.Name));
            textboxes.Add(new Textbox("Method", job.Method));
            textboxes.Add(new Textbox("Schedule", job.Timing));
            textboxes.Add(new Textbox("Count", job.Retention.Count.ToString()));
            textboxes.Add(new Textbox("Size", job.Retention.Size.ToString()));

            foreach (Textbox textbox in textboxes)
            {
                Textbox captured = textbox;

                captured.ValueChanged += (newValue) =>
                {
                    captured.Value = newValue;

                    switch (captured.Label)
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

                this.components.Add((IComponent)captured);
            }

            Button sourceButton = new Button("Source");
            sourceButton.Clicked += () =>
            {
                this.Application.CreateWindow(new WindowPathChanging(this.job.Sources));
            };
            this.components.Add(sourceButton);

            Button targetButton = new Button("Destination");
            targetButton.Clicked += () =>
            {
                this.Application.CreateWindow(new WindowPathChanging(this.job.Targets));
            };
            this.components.Add(targetButton);

            Button btnSAve = new Button("Save");
            btnSAve.Clicked += () =>
            {
                WindowChoose confirm = new WindowChoose("Are you sure you want to save the changes?");
                confirm.Confirm += () =>
                {
                    this.SaveAction?.Invoke(this.job);
                    this.Application.WindowsInUse.Pop();
                    this.Application.WindowsInUse.Pop();
                };
                
                confirm.Cancel += () =>
                {
                    this.Application.WindowsInUse.Pop();
                };

                this.Application.CreateWindow(confirm);
            };
            this.components.Add(btnSAve);

            Button btnCancel = new Button("Cancel");
            btnCancel.Clicked += () =>
            {
                this.Application.WindowsInUse.Pop();
            };
            this.components.Add(btnCancel);

            this.keys.Add(ConsoleKey.UpArrow, this.KeyUp);
            this.keys.Add(ConsoleKey.DownArrow, this.KeyDown);
        }


        public override void Draw()
        {
            for (int i = 0; i < this.components.Count; i++)
            {
                if (i == this.selectedComponent)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }

                this.components[i].Draw();
                Console.ResetColor();
            }
        }

        public override void HandleKey(ConsoleKeyInfo keyInfo)
        {
            if (this.keys.ContainsKey(keyInfo.Key))
            {
                this.keys[keyInfo.Key]();
            }
            else
            {
                this.components[this.selectedComponent].HandleKey(keyInfo);
            }
        }

        private void KeyUp()
        {
            this.selectedComponent = Math.Max(--this.selectedComponent, 0);
        }

        private void KeyDown()
        {
            this.selectedComponent = Math.Min(++this.selectedComponent, this.components.Count - 1);
        }
    }
}
