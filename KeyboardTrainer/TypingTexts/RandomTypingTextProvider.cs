using System;

namespace TypingTraining.TypingTexts
{
    public class RandomTypingTextProvider : ITypingTextsProvider
    {
        private readonly TypingText[] _texts;
        private readonly Random _random;

        public RandomTypingTextProvider(TypingText[] texts)
        {
            _texts = texts ?? throw new ArgumentNullException(nameof(texts));
            _random = new Random();
        }

        public TypingText GetNextText()
        {
            int index = _random.Next(_texts.Length);
            return _texts[index];
        }
    }
}
