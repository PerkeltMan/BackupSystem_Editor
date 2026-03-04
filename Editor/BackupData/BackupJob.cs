using Newtonsoft.Json;

namespace Editor.BackupData
{
    public class BackupJob
    {
        public int ID { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("Sources")]
        public List<string> Sources = new();

        [JsonProperty("Targets")]
        public List<string> Targets = new();
        [JsonProperty("Method")]
        public string Method { get; set; } = string.Empty;

        [JsonProperty("Timing")]
        public string Timing { get; set; } = string.Empty;

        [JsonProperty("Retention")]
        public Retention Retention { get; set; } = new();
    }
}
