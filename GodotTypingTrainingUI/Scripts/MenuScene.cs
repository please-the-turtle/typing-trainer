using Godot;

namespace GodotTypingTrainerUI.Scripts
{
    public class MenuScene : Control
    {
        private LoadingScene _loading;
        private SettingsPanel _settingsPanel;
        private TextsOptionButton _textsOptionButton;

        public override void _Ready()
        {
            _loading = GetNode<LoadingScene>("Loading");
            _settingsPanel = GetNode<SettingsPanel>("SettingsPanel");
            _textsOptionButton = GetNode<TextsOptionButton>("ControlsPanel/TextsOptionButton");
        }

        private async void StartTrainingAsync()
        {
            _loading.Start();
            //_startButton.Disabled = true;

            var global = this.GetGlobal();
            global.ApplicationSettings.LastTypingTextsIndex = _textsOptionButton.Selected;
            await global.SaveApplicationSettingsAsync();
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