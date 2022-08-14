using AngleSharp.Html.Dom;
using Parsing;
using TypingTraining.TypingTexts;

namespace ParsingConsoleUI.Parsers.Jokes
{
    public class JokesParser : IParser<TypingText[]>
    {
        private string _languageName = "RU";

        public TypingText[] Parse(IHtmlDocument document)
        {
            List<TypingText> texts = new();

            var items = document.QuerySelectorAll("div")
                .Where(item => item.ClassName != null && item.ClassName.Contains("post"));

            foreach (var item in items)
            {
                item.RemoveChild(item.LastElementChild!);
                string content = item.TextContent.Trim();
                TypingText text = TypingText.Create(content, _languageName);
                texts.Add(text);
            }

            return texts.ToArray();
        }
    }
}
