using Godot;
using GodotTypingTrainerUI.Scripts.Extentions;

namespace GodotTypingTrainerUI.Scripts.Trainer
{
    public class PausePanel : Panel
    {
        public override void _Ready()
        {
            Hide();
        }

        public void Open()
        {
            Show();
        }

        public void Close()
        {
            Hide();
        }

        private void GotoMenu()
        {
            Close();
            this.GetGlobal().GotoScene("res://Scenes/Menu.tscn");
        }
    }
}