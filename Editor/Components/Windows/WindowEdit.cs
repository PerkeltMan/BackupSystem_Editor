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
        private int jobID;

        public event Action<BackupJob>? SaveAction;

        public WindowEdit(BackupJob job, int jobID, Action<BackupJob>? saveAction)
        {
            this.job = job;
            this.jobID = jobID;
            this.SaveAction = saveAction;


        }
    }
}
