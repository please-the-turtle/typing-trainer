using Godot;
using GodotTypingTrainerUI.Scripts.Extentions;

namespace GodotTypingTrainerUI.Scripts.Trainer
{
    public class CompletedPanel : Panel
    {
        private Label _speedLabel;
        private Label _totalSpeedLabel;
        private Label _accuracyLabel;
        private Label _totalAccuracyLabel;

        private AnimationPlayer _animationPlayer;

        public override void _Ready()
        {
            _speedLabel = GetNode<Label>("InfoContainer/SpeedLabel");
            _totalSpeedLabel = GetNode<Label>("InfoContainer/TotalSpeedLabel");
            _accuracyLabel = GetNode<Label>("InfoContainer/AccuracyLabel");
            _totalAccuracyLabel = GetNode<Label>("InfoContainer/TotalAccuracyLabel");

            _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");

            Hide();
        }

        public void Open(float speed, float accuracy)
        {
            UpdateSpeedInfo(speed);
            UpdateTotalSpeedInfo();
            UpdateAccuracyInfo(accuracy);
            UpdateTotalAccuracyInfo();

           _animationPlayer.Play("Open");
        }

        public void Close()
        {
            _animationPlayer.Play("Close");
        }

        private void UpdateSpeedInfo(float speed)
        {
            _speedLabel.Text = $"Speed: {(int)speed} ch/min";
        }

        private void UpdateTotalSpeedInfo()
        {
            float totalSpeed = this.GetGlobal().UserStatistics.TotalSpeed;
            _totalSpeedLabel.Text = $"Total speed: {(int)totalSpeed} ch/min";
        }

        private void UpdateAccuracyInfo(float accuracy)
        {
            int accuracyInPercents = (int)(accuracy * 100);
            _accuracyLabel.Text = $"Accuracy: {accuracyInPercents}%";
        }

        private void UpdateTotalAccuracyInfo()
        {
            float totalAccuracy = this.GetGlobal().UserStatistics.TotalAccuracy;
            int accuracyInPercents = (int)(totalAccuracy * 100);
            _totalAccuracyLabel.Text = $"Total accuracy: {accuracyInPercents}%";
        }

        private void GotoMenu()
        {
            this.GetGlobal().GotoScene("res://Scenes/Menu.tscn");
        }
    }
}