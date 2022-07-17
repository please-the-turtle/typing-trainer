using Godot;
using GodotTypingTrainerUI.Scripts.Extentions;

namespace GodotTypingTrainerUI.Scripts.Menu
{
    public class StatisticsPanel : Panel
    {
        private Label _speedLabel;
        private Label _accuracyLabel;

        private AnimationPlayer _animationPlayer;

        public override void _Ready()
        {
            _speedLabel = GetNode<Label>("ScrollContainer/VBoxContainer/SpeedLabel");
            _accuracyLabel = GetNode<Label>("ScrollContainer/VBoxContainer/AccuracyLabel");

            _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");

            Hide();
        }

        public void Open()
        {
            UpdateSpeedLabel();
            UpdateAccuracyLabel();

            _animationPlayer.Play("Open");
        }

        public void Close()
        {
            _animationPlayer.Play("Close");
        }

        private void UpdateSpeedLabel()
        {
            float speed = this.GetGlobal().UserStatistics.TotalSpeed;
            _speedLabel.Text = $"AVG speed: {(int)speed} ch/min";
        }

        private void UpdateAccuracyLabel()
        {
            float accuracy = this.GetGlobal().UserStatistics.TotalAccuracy;
            int accuracyInPercents = (int)(accuracy * 100);
            _accuracyLabel.Text = $"AVG accuracy: {(int)accuracyInPercents}%";
        }
    }
}