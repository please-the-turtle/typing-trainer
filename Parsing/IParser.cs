using AngleSharp.Html.Dom;

namespace Parsing
{
    public interface IParser<T> where T : class
    {
        T Parse(IHtmlDocument document);
    }
}