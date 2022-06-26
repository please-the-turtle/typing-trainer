namespace Parsing
{
    public interface IParserSettings
    {
        /// <summary>
        /// Base URL address of the page for data parsing.
        /// </summary>
        string BaseUrl { get; }

        /// <summary>
        /// Prefix of the page for data parsing.
        /// <br/>The {CurrentId} part in the string will be replaced with a point number. 
        /// <br/>Example: "/Page{CurrentId}/"
        /// </summary>
        string Prefix { get; }

        /// <summary>
        /// Specifies the start page for data parsing.
        /// </summary>
        int StartPoint { get; }

        /// <summary>
        /// Specifies the final page index for parsing.
        /// </summary>
        int EndPoint { get; }
    }
}
