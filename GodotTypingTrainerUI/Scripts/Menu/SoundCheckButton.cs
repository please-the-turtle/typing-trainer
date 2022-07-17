using Godot;
using GodotTypingTrainerUI.Scripts.Extentions;

namespace GodotTypingTrainerUI.Scripts.Menu
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
            int masterAudioBusIdx = AudioServer.GetBusIndex("Master");
            AudioServer.SetBusMute(masterAudioBusIdx, !Pressed);
        }
    }
}
