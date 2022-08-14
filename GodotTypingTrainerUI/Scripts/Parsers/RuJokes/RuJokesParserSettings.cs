using Parsing;

namespace GodotTypingTrainerUI.Scripts.Parsers.RuJokes
{
    public class RuJokesParserSettings : IParserSettings
    {
        public string BaseUrl => "https://baneks.ru";

        public string Prefix => "{CurrentId}";

        public int StartPoint => 100;

        public int EndPoint => 200;
    }
}