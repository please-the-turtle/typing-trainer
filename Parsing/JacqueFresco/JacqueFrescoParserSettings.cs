namespace Parsing.JacqueFresco
{
    public class JacqueFrescoParserSettings : IParserSettings
    {
        public string BaseUrl => "https://ru.citaty.net/avtory";

        public string Prefix => "zhak-fresko/?page={CurrentId}";

        public int StartPoint => 2;

        public int EndPoint => 3;
    }
}
