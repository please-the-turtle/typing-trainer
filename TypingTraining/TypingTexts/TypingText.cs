using Newtonsoft.Json;
using System;

namespace TypingTraining.TypingTexts
{
    public class TypingText
    {
        [JsonConstructor]
        private TypingText(string content, string languageName)
        {
            Content = content;
            LanguageName = languageName;
        }

        public static TypingText Create(string content, string languageName)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                throw new ArgumentException("Content must be not null or white space.", 
                    nameof(content));
            }

            if (string.IsNullOrWhiteSpace(languageName))
            {
                throw new ArgumentException("Language name must be not null or white space.", 
                    nameof(languageName));
            }

            if (languageName.Length != 2)
            {
                throw new ArgumentException("Language name must match the two letter ISO langugage name.", 
                    nameof(languageName));
            }

            return new TypingText(content, languageName);
        }

        public string Content { get; private set; }

        /// <summary>
        /// Information about language of the text. Must match the letter ISO langugage name.
        /// </summary>
        public string LanguageName { get; private set; }
    }
}
