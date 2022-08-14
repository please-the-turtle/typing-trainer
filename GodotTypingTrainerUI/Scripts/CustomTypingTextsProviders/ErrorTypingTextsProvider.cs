using TypingTraining.TypingTexts;

namespace GodotTypingTrainerUI.Scripts.CustomTypingTextsProviders
{
    public class ErrorTypingTextsProvider : ITypingTextsProvider
    {
        private string _errorTextContent = 
            @"Failed to load training text. Try updating the texts in settings or adding your own texts files to the folder for texts.";
        private string _errorTextLanguageName = "EN";

        private TypingText _errorText;

        public ErrorTypingTextsProvider()
        {
            _errorText = TypingText.Create(_errorTextContent, _errorTextLanguageName);
        }

        public TypingText GetNextText()
        {

            return _errorText;
        }
    }
}