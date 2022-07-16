using Godot;
using GodotTypingTrainerUI.Scripts.Extentions;

namespace GodotTypingTrainerUI.Scripts.Menu
{
    public class SettingsPanel : Panel
    {
        [Signal]
        delegate void TypingTextsUpdated();

        private Button _updateTextsButton;

        private AnimationPlayer _animationPlayer;

        public override void _Ready()
        {
            _updateTextsButton = GetNode<Button>("ScrollContainer/VBoxContainer/UpdateTextsButton");
            _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");

            Hide();
        }

        public void Open()
        {
            _animationPlayer.Play("Open");
        }

        public void Close()
        {
            _animationPlayer.Play("Close");
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


