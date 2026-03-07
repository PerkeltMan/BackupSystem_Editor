using Editor.BackupData;
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;
using JsonException = Newtonsoft.Json.JsonException;

namespace Editor.Model
{
    public class ConfigFileManipulation
    {
        private const string FILE = @"D:\\config2.json";

        public List<BackupJob> PrepareJobs()
        {
            List<BackupJob> result = new List<BackupJob>();

            try
            {
                using (StreamReader sr = new StreamReader(FILE))
                {
                    string allText = sr.ReadToEnd();

                    List<BackupJob>? jobs = JsonConvert.DeserializeObject<List<BackupJob>>(allText); //?? throw new Exception("there are no backup jobs in config");

                    if (jobs == null)
                    {
                        return result;
                    }

                    //Console.WriteLine("Loading jobs...");
                    //Console.WriteLine(File.ReadAllText(FILE));

                    //fills BackupJob list in BackupService
                    foreach (BackupJob job in jobs)
                    {
                        //better safe than sorry
                        if (job.Method == "full")
                        {
                            job.Retention.Size = 1;
                        }

                        result.Add(job);
                    }
                }
            }
            catch (FileNotFoundException)
            {
                throw new Exception("invalid json file or wrong config file path");
            }
            catch (JsonException ex)
            {
                throw new Exception($"Wrong JSON format: {ex.Message}");
            }

            return result;
        }

        public void SaveJobs(List<BackupJob> jobs)
        {
            try
            {
                string json = JsonConvert.SerializeObject(jobs, Formatting.Indented);
                File.WriteAllText(FILE, json);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to save backup jobs: {ex.Message}");
            }
        }
    }
}