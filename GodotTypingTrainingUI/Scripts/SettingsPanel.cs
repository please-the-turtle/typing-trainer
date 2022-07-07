using Godot;

namespace GodotTypingTrainerUI.Scripts
{
    public class SettingsPanel : Panel
    {
        [Signal]
        delegate void TypingTextsUpdated();

        private Button _updateTextsButton;

        public override void _Ready()
        {
            Close();

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
            _updateTextsButton.Text = "Update texts";
            _updateTextsButton.Disabled = false;

            EmitSignal(nameof(TypingTextsUpdated));
        }

        private void OpenTextsFolder()
        {
            string userPath = OS.GetUserDataDir();
            string textsPath = this.GetGlobal().ApplicationSettings.TextsPath;
            string absoluteTextsPath = userPath + textsPath.Replace("user://", "/");

            OS.ShellOpen(absoluteTextsPath);
        }
    }
}


