using Godot;

namespace GodotTypingTrainerUI.Scripts
{
    public class SoundCheckButton : CheckButton
    {
        public override void _Ready()
        {
            Pressed = this.GetGlobal().ApplicationSettings.IsSoundsEnabled;
        }

        private void OnPressed()
        {
            this.GetGlobal().ApplicationSettings.IsSoundsEnabled = Pressed;
        }
    }
}
