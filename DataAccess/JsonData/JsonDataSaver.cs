using System;
using System.IO;
using System.Text.Json;

namespace DataAccess.JsonData
{
    public class JsonDataSaver : IDataSaver
    {
        public JsonDataSaver(string jsonFilePath)
        {
            JsonFilePath = jsonFilePath;
        }

        public string JsonFilePath
        {
            get => _filePath;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException($"'{nameof(value)}' cannot be null or whitespace.",
                        nameof(value));
                }

                _filePath = value;
            }
        }

        private string _filePath = null!;

        public void SaveData(object data)
        {
            string json = JsonSerializer.Serialize(data);
            File.WriteAllText(JsonFilePath, json);
        }

        public bool TrySaveData(object data)
        {
            try
            {
                SaveData(data);
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
