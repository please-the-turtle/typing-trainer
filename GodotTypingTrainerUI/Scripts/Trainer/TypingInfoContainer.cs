using Godot;

namespace GodotTypingTrainerUI.Scripts.Trainer
{
    public class TypingInfoContainer : VBoxContainer
    {
        private Label _speedLabel;
        private Label _accuracyLabel;
        private Label _missesLabel;

        public override void _Ready()
        {
            _speedLabel = GetNode<Label>("SpeedLabel");
            _accuracyLabel = GetNode<Label>("AccuracyLabel");
            _missesLabel = GetNode<Label>("MissesLabel");
        }

        public void ResetInfo()
        {
            UpdateSpeedInfo(0f);
            UpdateAccuracyInfo(1f);
            UpdateMissesInfo(0);
        }

        public void UpdateSpeedInfo(float speed)
        {
            _speedLabel.Text = $"Speed: {(int)speed} ch/min";
        }

        public void UpdateAccuracyInfo(float accuracy)
        {
            int accuracyInPercents = (int)(accuracy * 100);
            _accuracyLabel.Text = $"Accuracy: {accuracyInPercents}%";
        }

        public void UpdateMissesInfo(int missesNumber)
        {
            _missesLabel.Text = $"Misses: {missesNumber}";
        }
    }
}