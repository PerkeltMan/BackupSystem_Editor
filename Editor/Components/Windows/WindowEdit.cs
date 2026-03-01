using System;
using System.Collections.Generic;
using System.Text;
using Editor.BackupData.Job;

namespace Editor.Components.Windows
{
    public class WindowEdit : Window
    {
        private BackupJob job;
        public WindowEdit(BackupJob job)
        {
            this.job = job;    
        }

        public override void Draw()
        {
            throw new NotImplementedException();
        }

        public override void HandleKey(ConsoleKeyInfo keyInfo)
        {
            throw new NotImplementedException();
        }
    }
}
