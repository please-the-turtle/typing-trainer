using AngleSharp.Html.Dom;
using Parsing;
using TypingTraining.TypingTexts;

namespace ParsingConsoleUI.Parsers.JacqueFresco
{
    public class JacqueFrescoParser : IParser<TypingText[]>
    {
        private string _languageName = "RU";

        public TypingText[] Parse(IHtmlDocument document)
        {
            List<TypingText> texts = new();

            var items = document.QuerySelectorAll("p")
                .Where(item => item.ClassName != null && item.ClassName.Contains("blockquote-text"));

            foreach (var item in items)
            {
                string content = item.TextContent.Trim();
                TypingText text = TypingText.Create(content, _languageName);
                texts.Add(text);
            }

            return texts.ToArray();
        }
    }
}
