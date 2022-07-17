using Godot;
using GodotTypingTrainerUI.Scripts.Extentions;
using GodotTypingTrainerUI.Scripts.Globals;
using TypingTraining.TypingTexts;
using TypingTraining.TypingTrainers;
using System.Threading.Tasks;

namespace GodotTypingTrainerUI.Scripts.Menu
{
    public class MenuScene : Control
    {
        private LoadingScene _loading;
        private SettingsPanel _settingsPanel;
        private TextsOptionButton _textsOptionButton;
        private VBoxContainer _controlsContainer;

        public override void _Ready()
        {
            _loading = GetNode<LoadingScene>("Loading");
            _settingsPanel = GetNode<SettingsPanel>("SettingsPanel");
            _textsOptionButton = GetNode<TextsOptionButton>("ControlsContainer/TextsOptionButton");
            _controlsContainer = GetNode<VBoxContainer>("ControlsContainer");
        }

        private async void StartTrainingAsync()
        {
            _loading.Start();
            SetChildrenDisabled(_controlsContainer);

            Global global = this.GetGlobal();
            global.ApplicationSettings.LastTypingTextsIndex = _textsOptionButton.Selected;
            await global.SaveApplicationSettingsAsync();

            TypingTrainer trainer = await GetTypingTrainerAsync();
            global.SetGlobalParameter("trainer", trainer);

            global.GotoScene("res://Scenes/Trainer.tscn");
        }

        private void SetChildrenDisabled(Node controlsContainer)
        {
            foreach(var item in controlsContainer.GetChildren())
            {
                if (item is BaseButton button)
                {
                    button.Disabled = true;
                }
            }
        }

        private async Task<TypingTrainer> GetTypingTrainerAsync()
        {
            TypingTrainer trainer = await Task.Run(() => GetTypingTrainer());

            return trainer;
        }

        private TypingTrainer GetTypingTrainer()
        {
            string textsFilePath = GetLastTypingTextPath();
            GodotDataLoader<TypingText[]> loader = new GodotDataLoader<TypingText[]>(textsFilePath);
            TypingText[] texts = loader.LoadData();
            RandomTypingTextProvider provider = new RandomTypingTextProvider(texts);

            return new StrongTypingTrainer(provider);
        }

        private string GetLastTypingTextPath()
        {
            Global global = this.GetGlobal();
            string textsPath = global.ApplicationSettings.TextsPath;
            int lastTextIndex = global.ApplicationSettings.LastTypingTextsIndex;
            string textsExtension = global.ApplicationSettings.TypingTextsExtention;
            string lastTextFullName = _textsOptionButton.GetItemText(lastTextIndex) + textsExtension;

            return textsPath + '/' + lastTextFullName;
        }

        private void ShowSettings()
        {
            _settingsPanel.Open();
        }

        private async void OnSettingsPanel_hide()
        {
            await this.GetGlobal().SaveApplicationSettingsAsync();
        }
    }
}