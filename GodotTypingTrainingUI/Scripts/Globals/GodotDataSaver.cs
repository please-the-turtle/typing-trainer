using Godot;
using System;
using Newtonsoft.Json;

namespace GodotTypingTrainerUI.Scripts.Globals
{
    /// <summary>
    /// Saves data with godot File library.
    /// </summary>
    public class GodotDataSaver
    {
        public GodotDataSaver(string filePath)
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

        private string _filePath;

        public void SaveData(object data)
        {
            File file = new File();
            Error error = file.Open(_filePath, File.ModeFlags.Write);
            if (error != Error.Ok)
            {
                throw new Exception($"Failed to open file. Path: {_filePath}. Error: {error}");
            }

            string json = JsonConvert.SerializeObject(data);
            file.StoreLine(json);
            file.Close();
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
