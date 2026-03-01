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
        private BackupJob job;
        private int jobID;
        private int selectedComponent = 0;

        private List<IComponent> components = new List<IComponent>();

        public WindowEdit(BackupJob job, int jobID)
        {
            this.job = job;
            this.jobID = jobID;

            List<TextboxSingle> textboxes = new List<TextboxSingle>();
            textboxes.Add(new TextboxSingle("Name", job.Name));
            textboxes.Add(new TextboxSingle("Method", job.Method));
            textboxes.Add(new TextboxSingle("Schedule", job.Timing));
            textboxes.Add(new TextboxSingle("Count", job.Retention.Count.ToString()));
            textboxes.Add(new TextboxSingle("Size", job.Retention.Size.ToString()));

            foreach (TextboxSingle textbox in textboxes)
            {
                TextboxSingle captured = textbox;

                captured.ValueChanged += (newValue) =>
                {
                    captured.Value = newValue;
                };

                this.components.Add((IComponent)captured);
            }

            Button sourceButton = new Button("Source");
            sourceButton.Clicked += () =>
            {
                this.Application.CreateWindow(new WindowPathChanging(this.job.Sources));
            };  

        }



        public override void Draw()
        {
            throw new NotImplementedException();
        }

        public override void HandleKey(ConsoleKeyInfo keyInfo)
        {
            throw new NotImplementedException();
        }

        public void BtnSave_Clicked()
        {
            this.Application.EditJob(this.job, this.jobID);
            this.Application.WindowsInUse.Pop();
        }

        public void BtnCancel_Clicked() 
        {
            this.Application.WindowsInUse.Pop();
        }

        //private Dictionary<ConsoleKey, Action> keys = new Dictionary<ConsoleKey, Action>();
        //private BackupJob job;
        //private List<IComponent> components = new List<IComponent>();
        //private List<WindowPaths> windowsPaths = new List<WindowPaths>();
        //private int selectedComponent = 0;
        //private int jobID;

        //public event Action<BackupJob>? JobEdited;
        //public event Action? EditCanceled;
        //public event Action<WindowPathChanging>? ChangePathRequested;
        //public event Action<WindowPaths>? IntoPathsEntrance;

        //public WindowEdit(BackupJob job, int jobID)
        //{
        //    this.job = job;   
        //    this.jobID = jobID;

        //    this.components.Add(new TextboxSingle("Name", job.Name));
        //    this.components.Add(new TextboxSingle("Method", job.Method));
        //    this.components.Add(new TextboxSingle("Schedule", job.Timing));
        //    this.components.Add(new TextboxSingle("Count", job.Retention.Count.ToString()));
        //    this.components.Add(new TextboxSingle("Size", job.Retention.Size.ToString()));

        //    foreach (IComponent component in this.components)
        //    {
        //        component. += this.Change;
        //    }

        //    this.UpdateComponentList();

        //    this.keys.Add(ConsoleKey.UpArrow, this.KeyUp);
        //    this.keys.Add(ConsoleKey.DownArrow, this.KeyDown);
        //}

        //public override void Draw()
        //{
        //    Console.WriteLine(job.ID);

        //    for (int i = 0; i < this.components.Count; i++)
        //    {
        //        if (i == this.selectedComponent)
        //        {
        //            Console.ForegroundColor = ConsoleColor.Green;
        //        }

        //        this.components[i].Draw();
        //        Console.ResetColor();
        //    }
        //}

        //public override void HandleKey(ConsoleKeyInfo keyInfo)
        //{
        //    if (this.keys.ContainsKey(keyInfo.Key))
        //    {
        //        this.keys[keyInfo.Key]();
        //    }
        //    else
        //    {
        //        this.components[this.selectedComponent].HandleKey(keyInfo);
        //    }
        //}

        //private void KeyUp()
        //{
        //    this.selectedComponent = Math.Max(--this.selectedComponent, 0);
        //}

        //private void KeyDown()
        //{
        //    this.selectedComponent = Math.Min(++this.selectedComponent, this.components.Count - 1);
        //}

        //public void Save()
        //{
        //    this.Application.EditJob(this.job, this.jobID);
        //}

        //public void Cancel()
        //{
        //    this.Application.WindowsInUse.Pop();
        //}

        //public void Change(value)
        //{
        //    this.components[selectedComponent] = (IComponent)value;
        //}

        //private void UpdateComponentList()
        //{
        //    this.components.Clear();

        //    this.components.Add((new TextboxSingle("Name", this.job.Name)));
        //    this.components.Add(new WindowPaths("Source", this.job.Sources));
        //    this.components.Add(new WindowPaths("Destination", this.job.Targets));
        //    this.components.Add((IComponent)new Components.TextboxSingle("Type", this.job.Method));
        //    this.components.Add((IComponent)new Components.TextboxSingle("Schedule", this.job.Timing));
        //    this.components.Add((IComponent)new Components.TextboxSingle("Count", this.job.Retention.Count.ToString()));
        //    this.components.Add((IComponent)new Components.TextboxSingle("Size", this.job.Retention.Size.ToString()));
        //    this.components.Add(new Button("Save"));
        //    this.components.Add(new Button("Cancel"));
        //}
    }
}
