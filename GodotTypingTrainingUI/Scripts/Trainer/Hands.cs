using Godot;

namespace GodotTypingTrainerUI.Scripts.Trainer
{
    public class Hands : Control
    {
        private AnimationPlayer _animationPlayer;

        public override void _Ready()
        {
            _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        }

        public void PlayMissAnimation()
        {
            if (IsNoAnimationPlaying())
            _animationPlayer.Play("Miss");
        }

        private bool IsNoAnimationPlaying()
        {
            return string.IsNullOrEmpty(_animationPlayer.CurrentAnimation);
        }
    }
}