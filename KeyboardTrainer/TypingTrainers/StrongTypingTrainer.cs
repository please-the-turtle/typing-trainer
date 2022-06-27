using TypingTraining.TypingTexts;

namespace TypingTraining.TypingTrainers
{
    public class StrongTypingTrainer : TypingTrainer
    {
        public StrongTypingTrainer(ITypingTextsProvider textsProvider) : base(textsProvider) { }

        public override bool CheckInputChar(char input)
        {
            if (!TrainingPaused)
            {
                if (input != CurrentTypingText.Content[TypingCursorPosition])
                {
                    MissesNumber++;
                    return false;
                }

                TypingCursorPosition++;
                if (TypingCursorPosition >= CurrentTypingText.Content.Length)
                {
                    CompleteTraining();
                }

                return true;
            }

            return false;
        }
    }
}
