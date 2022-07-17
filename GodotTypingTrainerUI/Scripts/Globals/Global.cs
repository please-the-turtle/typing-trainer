using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GodotTypingTrainerUI.Scripts.Globals
{
    public class Global : Node
    {
        public Node CurrentScene { get; private set; }
        public ApplicationSettings ApplicationSettings { get; private set; }
        public UserStatistics UserStatistics { get; private set; }

        private List<GlobalParameter> _parameters = new();

        private Global() { }

        public override void _Ready()
        {
            Viewport root = GetTree().Root;
            CurrentScene = root.GetChild(root.GetChildCount() - 1);
            ApplicationSettings = LoadOrCreateNew<ApplicationSettings>(ApplicationSettings.SettingsPath);
            UserStatistics = LoadOrCreateNew<UserStatistics>(ApplicationSettings.UserStatisticsPath);

            int masterAudioBusIdx = AudioServer.GetBusIndex("Master");
            AudioServer.SetBusMute(masterAudioBusIdx, !ApplicationSettings.IsSoundsEnabled);
        }

        public void SetGlobalParameter(string parameterName, object value)
        {
            GlobalParameter parameter;

            if (_parameters.Exists(p => p.ParameterName == parameterName))
            {
                parameter = GetGlobalParameter(parameterName);
                parameter.Value = value;
            }
            else
            {
                parameter = new(parameterName, value);
                _parameters.Add(parameter);
            }
        }

        public T GetGlobalParameterValue<T>(string parameterName)
        {
            if (string.IsNullOrWhiteSpace(parameterName))
            {
                throw new ArgumentException($"'{nameof(parameterName)}' cannot be null or whitespace.", nameof(parameterName));
            }

            var parameter = GetGlobalParameter(parameterName);

            if (parameter is not null)
            {
                return parameter.GetValue<T>();
            }

            return default;
        }

        public void RemoveGlobalParameter(string parameterName)
        {
            var parameter = GetGlobalParameter(parameterName);
            _parameters.Remove(parameter);
        }

        public void GotoScene(string path)
        {
            CallDeferred(nameof(DeferredGotoScene), path);
        }

        public async Task SaveApplicationSettingsAsync()
        {
            if (ApplicationSettings.IsSettingsChanged)
            {
                var saver = new GodotDataSaver(ApplicationSettings.SettingsPath);
                await Task.Run(() => saver.TrySaveData(ApplicationSettings));
            }
        }

        public async Task SaveUserStatisticsAsync()
        {
            var saver = new GodotDataSaver(ApplicationSettings.UserStatisticsPath);
            await Task.Run(() => saver.TrySaveData(UserStatistics));
        }

        private GlobalParameter GetGlobalParameter(string parameterName)
        {
            return _parameters.FirstOrDefault(p => p.ParameterName == parameterName);
        }

        private T LoadOrCreateNew<T>(string filePath) where T : new()
        {
            T item;
            File statisticsFile = new File();

            if (!statisticsFile.FileExists(filePath))
            {
                var saver = new GodotDataSaver(filePath);
                item = new T();
                saver.SaveData(item);
            }

            var loader = new GodotDataLoader<T>(filePath);
            if (!loader.TryLoadData(out item) || item is null)
            {
                item = new T();
            }

            return item;
        }

        private void DeferredGotoScene(string path)
        {
            CurrentScene.Free();
            PackedScene nextScene = (PackedScene)GD.Load(path);
            CurrentScene = nextScene.Instance();
            GetTree().Root.AddChild(CurrentScene);
            GetTree().CurrentScene = CurrentScene;
        }
    }
}