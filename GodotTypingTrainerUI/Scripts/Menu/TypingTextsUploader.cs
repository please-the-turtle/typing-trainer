using Parsing;
using System.Collections.Generic;
using TypingTraining.TypingTexts;
using GodotTypingTrainerUI.Scripts.Globals;
using GodotTypingTrainerUI.Scripts.Parsers.RuJokes;

namespace GodotTypingTrainerUI.Scripts.Menu
{
    public class TypingTextsUploader
    {
        private List<TypingTextParsingSetup> _textParsingSetups;

        public TypingTextsUploader()
        {
            _textParsingSetups = ConfigureParsingSetups();
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

            foreach (var setup in _textParsingSetups)
            {
                parserWorker = new(setup.Parser, setup.ParserSettings);
                parsedTexts.Clear();
                parserWorker.OnNewData += (o, t) => parsedTexts.AddRange(t);
                string filePath = $"{uploadPath}/{setup.FileName}{filesExtension}";
                parserWorker.OnCompleted += (o) => SaveParsedTexts(filePath, parsedTexts);
                parserWorker.Start();
            }
        }

        private void SaveParsedTexts(string filePath, List<TypingText> parsedTexts)
        {
            if (parsedTexts is not null && parsedTexts.Count != 0)
            {
                GodotDataSaver saver = new(filePath);
                var b = saver.TrySaveData(parsedTexts);
            }
        }

        // Godot 3 don't supports tls1.3. 
        // All https pages with tls1.3 won't parse!
        // The solution of this problem is postponed until the transition to Godot 4 mono.
        private List<TypingTextParsingSetup> ConfigureParsingSetups()
        {
            List<TypingTextParsingSetup> parsers = new();

            TypingTextParsingSetup jokes = new(
                "[RU] Cringe jokes",
                new RuJokesParser(),
                new RuJokesParserSettings());
            parsers.Add(jokes);

            return parsers;
        }

    }
}
