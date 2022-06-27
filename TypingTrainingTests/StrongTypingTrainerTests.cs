using TypingTraining.TypingTrainers;
using TypingTraining.TypingTexts;
using Moq;

namespace TypingTrainingTests
{
    [TestFixture]
    public class StrongTypingTrainerTests
    {
        public static IEnumerable<TestCaseData> GetValidTypingTextsProviderTestCaseData()
        {
            Mock<ITypingTextsProvider> textsProviderMock = new();

            yield return new TestCaseData(textsProviderMock.Object);
        }

        [TestCaseSource(nameof(GetValidTypingTextsProviderTestCaseData))]
        public void TestCreate_ValidTypingTextsProvider_Success(ITypingTextsProvider textsProvider)
        {
            StrongTypingTrainer trainer = new(textsProvider);

            Assert.NotNull(trainer);
        }

        [Test]
        public void TestCreate_TypingTextsProviderIsNull_Exception()
        {
            Assert.Throws<ArgumentNullException>(() => new StrongTypingTrainer(null!));
        }

        public static IEnumerable<TestCaseData> GetValidTypingTrainerTestCaseData()
        {
            var textsProviderMock = new Mock<ITypingTextsProvider>();
            textsProviderMock.Setup(a => a.GetNextText()).Returns(TypingText.Create("te", "EN"));
            StrongTypingTrainer trainer = new(textsProviderMock.Object);

            yield return new TestCaseData(trainer);
        }

        [TestCaseSource(nameof(GetValidTypingTrainerTestCaseData))]
        public void TestCheckInputChar_TrainingIsPaused_ReturnFalse(TypingTrainer trainer)
        {
            bool checkResult = trainer.CheckInputChar('t');

            Assert.IsFalse(checkResult);
        }

        [TestCaseSource(nameof(GetValidTypingTrainerTestCaseData))]
        public void TestCheckInputChar_CorrectInuptChar_ReturnsTrue(TypingTrainer trainer)
        {
            trainer.Start();
            bool checkResult = trainer.CheckInputChar('t');
            trainer.Pause();

            Assert.IsTrue(checkResult);
        }

        [TestCaseSource(nameof(GetValidTypingTrainerTestCaseData))]
        public void TestCheckInputChar_NotCorrectInuptChar_ReturnsFalse(TypingTrainer trainer)
        {
            trainer.Start();
            bool checkResult = trainer.CheckInputChar('-');
            trainer.Pause();

            Assert.IsFalse(checkResult);
        }

        [TestCaseSource(nameof(GetValidTypingTrainerTestCaseData))]
        public void TestTypingTextChanged_ValidTypingTrainer_EventRaised(TypingTrainer trainer)
        {
            bool wasCalled = false;
            trainer.TypingTextChanged += (t) => { wasCalled = true; };

            trainer.ChangeTypingText();

            Assert.True(wasCalled);
        }

        [TestCaseSource(nameof(GetValidTypingTrainerTestCaseData))]
        public void TestTypingCursorPositionChanged_ValidTypingTrainer_EventRaised(TypingTrainer trainer)
        {
            bool wasCalled = false;
            trainer.TypingCursorPositionChanged += (t) => { wasCalled = true; };

            trainer.Start();
            trainer.CheckInputChar('t');

            Assert.True(wasCalled);
        }

        [TestCaseSource(nameof(GetValidTypingTrainerTestCaseData))]
        public void TestMissesNumberChanged_ValidTypingTrainer_EventRaised(TypingTrainer trainer)
        {
            bool wasCalled = false;
            trainer.MissesNumberChanged += (t) => { wasCalled = true; };

            trainer.Start();
            trainer.CheckInputChar('-');

            Assert.True(wasCalled);
        }

        [TestCaseSource(nameof(GetValidTypingTrainerTestCaseData))]
        public void TestTypingCompleted_ValidTypingTrainer_EventRaised(TypingTrainer trainer)
        {
            bool wasCalled = false;
            trainer.TypingCompleted += (t) => { wasCalled = true; };

            trainer.Start();
            trainer.CheckInputChar('t');
            trainer.CheckInputChar('e');

            Assert.True(wasCalled);
        }

        [TestCaseSource(nameof(GetValidTypingTrainerTestCaseData))]
        public void TestCurrentTypingTextGetter_ValidTypingTrainer_CorrectCurrentTypingText(TypingTrainer trainer)
        {
            string expectedContent = "te";
            string expectedLanguageName = "EN";
            TypingText actual = trainer.CurrentTypingText;

            Assert.That(actual.Content, Is.EqualTo(expectedContent));
            Assert.That(actual.LanguageName, Is.EqualTo(expectedLanguageName));
        }

        [TestCaseSource(nameof(GetValidTypingTrainerTestCaseData))]
        public void TestTypingCursorPositionGetter_ValidTypingTrainer_CorrectTypingCursorPosition(TypingTrainer trainer)
        {
            int expected = 1;

            trainer.Start();
            trainer.CheckInputChar('t');
            trainer.Pause();
            int actual = trainer.TypingCursorPosition;

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCaseSource(nameof(GetValidTypingTrainerTestCaseData))]
        public void TestMissesNumberGetter_ValidTypingTrainer_CorrectMissesNumber(TypingTrainer trainer)
        {
            int expected = 2;

            trainer.Start();
            trainer.CheckInputChar('e');
            trainer.CheckInputChar('t');
            trainer.CheckInputChar('-');
            trainer.Pause();
            int actual = trainer.MissesNumber;

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCaseSource(nameof(GetValidTypingTrainerTestCaseData))]
        public void TestTypingSpeed_ValidTypingTrainer_CorrectTypingSpeed(TypingTrainer trainer)
        {
            // speed = 1char / (50ms + 50ms + runtime error)

            trainer.Start();
            Thread.Sleep(50);
            trainer.CheckInputChar('t');
            Thread.Sleep(50);
            float speed = trainer.TypingSpeed;
            trainer.Pause();

            Assert.Positive(speed);
            Assert.LessOrEqual(speed, 600);
        }

        [TestCaseSource(nameof(GetValidTypingTrainerTestCaseData))]
        public void TestTypingAccuracy_ValidTypingTrainer_CorrectTypingAccuracy(TypingTrainer trainer)
        {
            // accuracy = 1 not correct char / 2 typed chars

            float actual = .5f;

            trainer.Start();
            trainer.CheckInputChar('t');
            trainer.CheckInputChar('-');
            float expected = trainer.TypingAccuracy;
            trainer.Pause();

            Assert.That(expected, Is.EqualTo(actual));
        }

        [TestCaseSource(nameof(GetValidTypingTrainerTestCaseData))]
        public void TestTrainingPaused_ValidTypingTrainer_CorrectTrainingPaused(TypingTrainer trainer)
        {
            Assert.IsTrue(trainer.TrainingPaused);
            trainer.Start();
            Assert.IsFalse(trainer.TrainingPaused);
            trainer.Pause();
            Assert.IsTrue(trainer.TrainingPaused);
        }
    }
}