using Godot;

namespace GodotTypingTrainerUI.Scripts
{
    public class SettingsPanel : Panel
    {
        private Button _updateTextsButton;

        public override void _Ready()
        {
            _updateTextsButton = GetNode<Button>("GridContainer/VBoxContainer/UpdateTextsButton");
        }

        public void Open()
        {
            Show();
        }

        public void Close()
        {
            Hide();
        }

        private void OnUpdateTextsButton_pressed()
        {
            _updateTextsButton.Disabled = true;
            _updateTextsButton.Text = "Texts updating...";
            TypingTextsUploader uploader = new();
            string uploadPath = this.GetGlobal().ApplicationSettings.TextsPath;
            string textsExtension = this.GetGlobal().ApplicationSettings.TypingTextsExtention;
            uploader.Upload(uploadPath, textsExtension);
        }
    }
}


