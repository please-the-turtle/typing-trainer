using Godot;
using System.Threading.Tasks;

namespace GodotTypingTrainerUI.Scripts
{
    public class Global : Node
    {
        public Node CurrentScene { get; private set; }
        public ApplicationSettings ApplicationSettings { get; private set; }

        private Global() { }

        public override void _Ready()
        {
            Viewport root = GetTree().Root;
            CurrentScene = root.GetChild(root.GetChildCount() - 1);
            ApplicationSettings = LoadApplicationSettings();
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
                await Task.Run(() => { saver.SaveData(ApplicationSettings); });
            }
        }

        private ApplicationSettings LoadApplicationSettings()
        {
            ApplicationSettings settings;
            File settingsFile = new File();

            if (!settingsFile.FileExists(ApplicationSettings.SettingsPath))
            {
                var saver = new GodotDataSaver(ApplicationSettings.SettingsPath);
                settings = new ApplicationSettings();
                saver.SaveData(settings);
            }

            var loader = new GodotDataLoader<ApplicationSettings>(ApplicationSettings.SettingsPath);
            if (!loader.TryLoadData(out settings) || settings is null)
            {
                settings = new ApplicationSettings();
            }

            return settings;
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