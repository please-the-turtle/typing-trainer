using Parsing;
using TypingTraining.TypingTexts;

namespace GodotTypingTrainerUI.Scripts.Menu
{
    public class TypingTextParsingSetup
    {
        public TypingTextParsingSetup(string fileName, IParser<TypingText[]> parser, IParserSettings parserSettings)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new System.ArgumentException($"'{nameof(fileName)}' cannot be null or whitespace.", nameof(fileName));
            }

            FileName = fileName;
            Parser = parser ?? throw new System.ArgumentNullException(nameof(parser));
            ParserSettings = parserSettings ?? throw new System.ArgumentNullException(nameof(parserSettings));
        }

        public string FileName { get; }
        public IParser<TypingText[]> Parser { get; }
        public IParserSettings ParserSettings { get; }
    }
}
