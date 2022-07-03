using System;
using System.IO;
using System.Text.Json;

namespace DataAccess.JsonData
{
    public class JsonDataLoader<T> : IDataLoader<T>
    {
        public JsonDataLoader(string jsonFilePath)
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

        public T LoadData()
        {
            string json = File.ReadAllText(JsonFilePath);
            T obj = JsonSerializer.Deserialize<T>(json)!;
            return obj;
        }

        public bool TryLoadData(out T loadedData)
        {
            loadedData = default!;

            try
            {
                loadedData = LoadData();
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
