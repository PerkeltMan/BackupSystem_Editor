namespace Editor.BackupData
{
    public class Retention
    {
        public int Count { get; set; }
        public int Size { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        public required BackupJob BackupJob { get; set; }
    }
}
