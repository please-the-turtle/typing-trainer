using Godot;
using TypingTraining.FingerPointer;
using TypingTraining.FingerPointer.FingersLayouts;
using TypingTraining.FingerPointer.Pointers;

namespace GodotTypingTrainerUI.Scripts.Trainer
{
    public class Hands : Control
    {
        private IFingerPointer _fingerPointer;

        private AnimationPlayer _animationPlayer;
        private Label _fingerPrompt;
        private FingerPromptPositions _fingerPromptPositions;

        public override void _Ready()
        {
            IFingersLayout fingersLayout = new AsdfLayout();
            _fingerPointer = new FingerPointer(fingersLayout);

            _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
            _fingerPrompt = GetNode<Label>("FingerPrompt");
            _fingerPromptPositions = GetNode<FingerPromptPositions>("FingerPromptPositions");
        }

        public void PlayMissAnimation()
        {
            if (IsNoAnimationPlaying())
            {
                _animationPlayer.Play("Miss");
            }
        }

        public void UpdateFingerPrompt(char typedCharacter)
        {
            Fingers finger = _fingerPointer.GetFinger(typedCharacter);

            if (finger == Fingers.None)
            {
                _fingerPrompt.Hide();
            }
            else
            {
                _fingerPrompt.Text = typedCharacter.ToString();
                Vector2 position = _fingerPromptPositions.GetFingerPromptPosition(finger);
                position -= _fingerPrompt.RectPivotOffset; 
                _fingerPrompt.SetPosition(position);
                _fingerPrompt.Show();
            }
        }

        private bool IsNoAnimationPlaying()
        {
            return string.IsNullOrEmpty(_animationPlayer.CurrentAnimation);
        }
    }
}