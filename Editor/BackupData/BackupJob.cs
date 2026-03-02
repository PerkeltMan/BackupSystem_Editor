using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Editor.BackupData
{
    public class BackupJob
    {
        public int ID { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("Sources")]
        public List<string> Sources = new List<string>();

        [JsonProperty("Targets")]
        public List<string> Targets = new List<string>();

        [JsonProperty("Method")]
        public string Method { get; set; } = string.Empty;

        [JsonProperty("Timing")]
        public string Timing { get; set; } = string.Empty;

        [JsonProperty("Retention")]
        public Retention Retention { get; set; }

        public BackupJob(Retention retention)
        {
            this.Retention = retention;
            this.Retention.BackupJob = this;
        }

        public BackupJob()
        {
            Retention retention = new Retention() { Size = 0, Count = 0, BackupJob = this };
            this.Retention = retention;
        }
    }
}
