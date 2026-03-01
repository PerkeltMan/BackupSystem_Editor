namespace Editor.BackupData
{
    public class Retention
    {
        public int Count { get; set; } = 0;
        public int Size { get; set; } = 0;
        [Newtonsoft.Json.JsonIgnore]
        public required BackupJob BackupJob { get; set; }
    }
}
