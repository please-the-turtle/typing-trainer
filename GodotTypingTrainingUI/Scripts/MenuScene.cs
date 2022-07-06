using Godot;

namespace GodotTypingTrainerUI.Scripts
{
    public class MenuScene : Control
    {
        private TextsOptionButton _textsOptionButton;
        private LoadingScene _loading;
        private SettingsPanel _settingsPanel;

        public override void _Ready()
        {
            _loading = GetNode<LoadingScene>("Loading");
            _settingsPanel = GetNode<SettingsPanel>("SettingsPanel");
        }

        private void StartTraining()
        {
            _loading.Start();
            //_startButton.Disabled = true;
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