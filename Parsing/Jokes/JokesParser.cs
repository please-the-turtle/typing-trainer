using AngleSharp.Html.Dom;
using TypingTraining.TypingTexts;

namespace Parsing.Jokes
{
    public class JokesParser : IParser<TypingText[]>
    {
        private const string LanguageName = "RU";

        public TypingText[] Parse(IHtmlDocument document)
        {
            List<TypingText> texts = new();

            var items = document.QuerySelectorAll("div")
                .Where(item => item.ClassName != null && item.ClassName.Contains("post"));

            foreach (var item in items)
            {
                item.RemoveChild(item.LastElementChild!);
                string content = item.TextContent.Trim();
                TypingText text = TypingText.Create(content, LanguageName);
                texts.Add(text);
            }

            return texts.ToArray();
        }
    }
}
