using Newtonsoft.Json;

namespace GodotTypingTrainerUI.Scripts.Globals
{
    [JsonObject(MemberSerialization.OptIn)]
    public class UserStatistics
    {
        [JsonProperty("totalSpeed")]
        public float TotalSpeed { get; set; } = 0f;

        [JsonProperty("totalAccuracy")]
        public float TotalAccuracy { get; set; } = 0f;

        [JsonProperty("writtenTextsNumber")]
        public int WrittenTextsNumber {get; set; } = 0;
    }
}