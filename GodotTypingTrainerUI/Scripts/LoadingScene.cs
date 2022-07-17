using Godot;

namespace GodotTypingTrainerUI.Scripts
{
    public class LoadingScene : Control
    {
        private AnimationPlayer _animationPlayer;

        public override void _Ready()
        {
            Visible = false;
            _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
            _animationPlayer.CurrentAnimation = "LoadingAnimation";
        }

        public void Start()
        {
            Visible = true;
            _animationPlayer.Play();
        }

        public void Stop()
        {
            Visible = false;
            _animationPlayer.Stop();
        }
    }
}