using AngleSharp.Html.Parser;

namespace Parsing
{
    public class ParserWorker<T> where T : class
    {
        public event Action<object, T>? OnNewData;
        public event Action<object>? OnCompleted;

        private IParser<T> _parser = null!;
        private IParserSettings _parserSettings = null!;

        private bool _isActive;

        private HtmlLoader _loader = null!;

        public IParser<T> Parser
        {
            get
            {
                return _parser;
            }
            private set
            {
                _parser = value;
            }
        }

        public IParserSettings Settings
        {
            get
            {
                return _parserSettings;
            }
            private set
            {
                _parserSettings = value;
                _loader = new HtmlLoader(value);
            }
        }

        public bool IsActive
        {
            get
            {
                return _isActive;
            }
        }

        public ParserWorker(IParser<T> parser, IParserSettings parserSettings)
        {
            Parser = parser ?? throw new ArgumentNullException(nameof(parser));
            Settings = parserSettings ?? throw new ArgumentNullException(nameof(parserSettings));
        }

        public void Start()
        {
            _isActive = true;
            Worker();
        }

        public void Abort()
        {
            _isActive = false;
        }

        private async void Worker()
        {
            for (int i = _parserSettings.StartPoint; i <= _parserSettings.EndPoint; i++)
            {
                if (!_isActive)
                {
                    OnCompleted?.Invoke(this);
                    return;
                }

                var source = await _loader.GetSourceByPageId(i);
                var domParser = new HtmlParser();

                var document = await domParser.ParseDocumentAsync(source!);

                var result = _parser.Parse(document);

                OnNewData?.Invoke(this, result);
            }

            OnCompleted?.Invoke(this);
            _isActive = false;
        }
    }
}
