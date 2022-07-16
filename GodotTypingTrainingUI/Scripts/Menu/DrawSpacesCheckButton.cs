using Godot;
using GodotTypingTrainerUI.Scripts.Extentions;

namespace GodotTypingTrainingUI.Scripts.Menu
{
    public class DrawSpacesCheckButton : CheckButton
    {
        public override void _Ready()
        {
            Pressed = this.GetGlobal().ApplicationSettings.DrawSpaces;
        }

        private void OnPressed()
        {
            this.GetGlobal().ApplicationSettings.DrawSpaces = Pressed;
        }
    }
}