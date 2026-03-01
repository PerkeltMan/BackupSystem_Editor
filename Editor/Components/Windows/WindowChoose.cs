using System;
using System.Collections.Generic;
using System.Text;
using Editor.BackupData.Job;
using Editor.Components.Components;

namespace Editor.Components.Windows
{
    public class WindowChoose : Window
    {
        private BackupJob job;
        private List<Button> buttons = new List<Button>();
        public WindowChoose(BackupJob job)
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
