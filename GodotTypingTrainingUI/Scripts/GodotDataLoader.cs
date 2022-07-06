using Godot;
using Newtonsoft.Json;
using System;

namespace GodotTypingTrainerUI.Scripts
{
    /// <summary>
    /// Loads data with godot File library.
    /// </summary>
    /// <typeparam name="T">Type of the loaded data.</typeparam>
    public class GodotDataLoader<T>
    {
        public GodotDataLoader(string filePath)
        {
            JsonFilePath = filePath;
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
            T obj = default;
            File file = new File();

            Error error = file.Open(_filePath, File.ModeFlags.Read);

            if (error != Error.Ok)
            {
                throw new Exception($"Failed to open file. Path: {_filePath}. Error: {error}");
            }

            string json = file.GetAsText();
            obj = JsonConvert.DeserializeObject<T>(json);
            file.Close();

            return obj;
        }

        public bool TryLoadData(out T loadedData)
        {
            try
            {
                loadedData = LoadData();
            }
            catch
            {
                loadedData = default;
                return false;
            }

            return true;
        }
    }
}
