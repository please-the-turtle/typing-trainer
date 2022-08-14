using System.Collections.Generic;
using System.Linq;
using AngleSharp.Html.Dom;
using Parsing;
using TypingTraining.TypingTexts;

namespace GodotTypingTrainerUI.Scripts.Parsers.RuJokes
{
    public class RuJokesParser : IParser<TypingText[]>
    {
        private string _languageName = "RU";

        public TypingText[] Parse(IHtmlDocument document)
        {
            List<TypingText> texts = new();

            var items = document.QuerySelectorAll("p");

            foreach (var item in items)
            {
                string content = item.TextContent.Trim();
                content = content.Replace('—', '-');
                TypingText text = TypingText.Create(content, _languageName);
                texts.Add(text);
            }

            return texts.ToArray();
        }
    }
}
