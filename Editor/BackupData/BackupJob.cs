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
        public required string Name { get; set; }

        [JsonProperty("Sources")]
        public List<string> Sources = new List<string>();

        [JsonProperty("Targets")]
        public List<string> Targets = new List<string>();

        [JsonProperty("Method")]
        public required string Method { get; set; }

        [JsonProperty("Timing")]
        public required string Timing { get; set; }

        [JsonProperty("Retention")]
        public required Retention Retention {  get; set; }

        public BackupJob(Retention retention)
        {
            this.Retention = retention;
            this.Retention.BackupJob = this;
        }
    }
}
