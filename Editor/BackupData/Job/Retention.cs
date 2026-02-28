using System;
using System.Collections.Generic;
using System.Text;

namespace Editor.BackupData.Job
{
    public class Retention
    {
        public int Count { get; set; }
        public int Size { get; set; }
        public required BackupJob BackupJob { get; set; }
    }
}
