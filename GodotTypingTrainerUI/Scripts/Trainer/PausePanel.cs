using Godot;
using GodotTypingTrainerUI.Scripts.Extentions;

namespace GodotTypingTrainerUI.Scripts.Trainer
{
    public class PausePanel : Panel
    {
        private AnimationPlayer _animationPlayer;

        public override void _Ready()
        {
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
        
        private void GotoMenu()
        {
            this.GetGlobal().GotoScene("res://Scenes/Menu.tscn");
        }
    }
}