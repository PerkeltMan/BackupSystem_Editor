using System;
using System.Collections.Generic;
using System.Text;
using Editor.BackupData.Job;
using Editor.Components.Components;
using Editor.Components.Non_WindowComponents;

namespace Editor.Components.Windows
{
    public class WindowEdit : Window
    {
        private Dictionary<ConsoleKey, Action> keys = new Dictionary<ConsoleKey, Action>();
        private BackupJob job;
        private List<IComponent> components = new List<IComponent>();
        private int selectedComponent = 0;

        public event Action<BackupJob>? JobEdited;
        public event Action? EditCanceled;
        public event Action<WindowChangePath>? ChangePathRequested;
        public event Action<WindowPaths>? IntoPathsEntrance;

        public WindowEdit(BackupJob job)
        {
            this.job = job;   
            
            this.UpdateComponentList();

            this.keys.Add(ConsoleKey.UpArrow, this.KeyUp);
            this.keys.Add(ConsoleKey.DownArrow, this.KeyDown);
        }

        public override void Draw()
        {
            Console.WriteLine(job.ID);

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

        private void UpdateComponentList()
        {
            this.components.Clear();

            this.components.Add(new TextboxSingle("Name", this.job.Name));
            this.components.Add(new WindowPaths("Source", this.job.Sources));
            
            this.components.Add(new WindowPaths("Destination", this.job.Targets));
            this.components.Add(new TextboxSingle("Type", this.job.Method));
            this.components.Add(new TextboxSingle("Schedule", this.job.Timing));
            this.components.Add(new TextboxSingle("Count", this.job.Retention.Count.ToString()));
            this.components.Add(new TextboxSingle("Size", this.job.Retention.Size.ToString()));
            this.components.Add(new Button("Save"));
            this.components.Add(new Button("Cancel"));
        }
    }
}
