using System;
using System.Collections.Generic;
using System.Text;

namespace Editor.BackupData.Job
{
    public class BackupJob
    {
        public int ID { get; set; }
        public required string Name { get; set; }
        public List<string> Sources = new List<string>();
        public List<string> Targets = new List<string>();
        public required string Method { get; set; }
        public required string Timing { get; set; }
        public required Retention Retention {  get; set; }

        public BackupJob(Retention retention)
        {
            this.Retention = retention;
            this.Retention.BackupJob = this;
        }
    }
}
