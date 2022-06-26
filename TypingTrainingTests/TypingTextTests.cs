using TypingTraining.TypingTexts;

namespace TypingTrainingTests
{
    [TestFixture]
    public class TypingTextTests
    {
        [TestCase("a", "EN")]
        [TestCase("its random test text for content string", "EN")]
        [TestCase("текст набранный кириллицей", "RU")]
        [TestCase("a aaa aaaa", "FR")]
        public void TestCreate_ValidInputData_Success(string content, string languageName)
        {
            TypingText text = TypingText.Create(content, languageName);

            Assert.NotNull(text);
        }

        [TestCase("", "EN")]
        [TestCase("    ", "EN")]
        [TestCase(" ", "RU")]
        [TestCase(null, "FR")]
        public void TestCreate_NotValidContentData_Exception(string content, string languageName)
        {
            Assert.Throws<ArgumentException>(() => TypingText.Create(content, languageName));
        }

        [TestCase("a", "RUS")]
        [TestCase("a", "LONG STRING")]
        [TestCase("its random test text for content string", "E")]
        [TestCase("текст набранный кириллицей", "")]
        [TestCase("текст набранный кириллицей", " ")]
        [TestCase("текст набранный кириллицей", "  ")]
        [TestCase("a aaa aaaa", null)]
        public void TestCreate_NotValidLanguageNameData_Exception(string content, string languageName)
        {
            Assert.Throws<ArgumentException>(() => TypingText.Create(content, languageName));
        }
    }
}
