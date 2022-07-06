using Parsing;
using Parsing.Jokes;
using System.Collections.Generic;
using TypingTraining.TypingTexts;

namespace GodotTypingTrainerUI.Scripts
{
    public class TypingTextsUploader
    {
        private Dictionary<string, IParser<TypingText[]>> _textsParsers;

        public TypingTextsUploader()
        {
            _textsParsers = ConfigureTextParsers();
        }

        /// <summary>
        /// Uploads new texts for typing training from outside resources.
        /// </summary>
        /// <param name="uploadPath">Path for saving uploaded texts.</param>
        /// <param name="filesExtension">Extension of the saved files with uploaded data.</param>>
        public void Upload(string uploadPath, string filesExtension)
        {
            ParserWorker<TypingText[]> parserWorker;
            List<TypingText> parsedTexts = new();

            foreach (var parser in _textsParsers)
            {
                parserWorker = new(parser.Value);
                parsedTexts.Clear();
                parserWorker.OnNewData += (o, t) => parsedTexts.AddRange(t);
                string filePath = $"{uploadPath}/{parser.Key}{filesExtension}";
                parserWorker.OnCompleted += (o) => SaveParsedTexts(filePath, parsedTexts);
                parserWorker.Start();
            }
        }

        private void SaveParsedTexts(string filePath, List<TypingText> parsedTexts)
        {
            if (parsedTexts is not null && parsedTexts.Count != 0)
            {
                GodotDataSaver saver = new(filePath);
                saver.TrySaveData(parsedTexts);
            }
        }

        private Dictionary<string, IParser<TypingText[]>> ConfigureTextParsers()
        {
            Dictionary<string, IParser<TypingText[]>> parsers = new();

            JokesParser jokesParser = new();
            parsers.Add("[RU] Jokes (not fun)", jokesParser);

            return parsers;
        }

    }
}
