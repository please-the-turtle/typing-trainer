namespace Parsing.Jokes
{
    public class JokesParserSettings : IParserSettings
    {
        public string BaseUrl => "https://vse-shutochki.ru";

        public string Prefix => "anekdoty/{CurrentId}";

        public int StartPoint => 2;

        public int EndPoint => 3;
    }
}
