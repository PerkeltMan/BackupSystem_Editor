using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;
using Editor.BackupData;
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;
using JsonException = Newtonsoft.Json.JsonException;

namespace Editor.Model
{
    public class ConfigFileManipulation
    {
        private const string FILE = @"D:\\config.json";

        public List<BackupJob> PrepareJobs()
        {
            List<BackupJob> result = new List<BackupJob>();

            try
            {
                using (StreamReader sr = new StreamReader(FILE))
                {
                    string allText = sr.ReadToEnd();

                    List<BackupJob>? jobs = JsonConvert.DeserializeObject<List<BackupJob>>(allText);

                    if (jobs == null)
                    {
                        throw new Exception("there are no backup jobs in config");
                    }

                    //fills BackupJob list in BackupService
                    foreach (BackupJob job in jobs)
                    {
                        if (job.Sources == null || job.Sources.Count == 0 || job.Targets == null || job.Targets.Count == 0)
                        {
                            throw new Exception($"Backup job {jobs.IndexOf(job) + 1} is missing at least one source or at least one destination");
                        }

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