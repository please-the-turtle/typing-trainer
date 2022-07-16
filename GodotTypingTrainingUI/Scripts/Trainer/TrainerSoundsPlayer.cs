using Godot;

namespace GodotTypingTrainerUI.Scripts.Trainer
{
    public class TrainerSoundsPlayer : Node
    {
        private AudioStreamPlayer _missPlayer;
        private AudioStreamPlayer _completedPlayer;

        public override void _Ready()
        {
            _missPlayer = GetNode<AudioStreamPlayer>("MissPlayer");
            _completedPlayer = GetNode<AudioStreamPlayer>("CompletedPlayer");
        } 

        public void PlayMissSound()
        {
            _missPlayer.Play();
        }

        public void PlayCompletedSound()
        {
            _completedPlayer.Play();
        }
    }
}