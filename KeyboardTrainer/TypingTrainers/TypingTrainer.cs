using System.Diagnostics;
using TypingTraining.TypingTexts;

namespace TypingTraining.TypingTrainers
{
    /// <summary>
    /// Trainer of the keyboard typing.
    /// </summary>
    public class TypingTrainer
    {
        public event Action<TypingTrainer>? TypingTextChanged;

        public event Action<TypingTrainer>? TypingCursorPositionChanged;

        public event Action<TypingTrainer>? MissesNumberChanged;

        public event Action<TypingTrainer>? TypingCompleted;

        private ITypingTextsProvider _textsProvider;

        private TypingText _currentText = null!;
        private int _cursorPosition;
        private int _missesNumber;
        private bool _isPaused = true;
        private Stopwatch _typingTimer;

        private TypingTrainer(ITypingTextsProvider textsProvider)
        {
            _textsProvider = textsProvider;
            _typingTimer = new Stopwatch();
        }

        /// <summary>
        /// Creates new instance of TypingTrainer.
        /// </summary>
        /// <param name="textsProvider">Provider of the texts for typing.</param>
        /// <returns>New instance of TypingTrainer.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TypingTrainer Create(ITypingTextsProvider textsProvider)
        {
            if (textsProvider is null)
            {
                throw new ArgumentNullException(nameof(textsProvider));
            }

            TypingTrainer trainer = new(textsProvider);
            trainer.ChangeTypingText();

            return trainer;
        }

        /// <summary>
        /// Gets text that should enter user.
        /// </summary>
        public TypingText CurrentTypingText
        {
            get => _currentText;
            private set
            {
                _currentText = value;
                TypingTextChanged?.Invoke(this);
            }
        }

        /// <summary>
        /// Gets cursor position. Separates text into entered and not entered.
        /// </summary>
        public int TypingCursorPosition
        {
            get => _cursorPosition;
            private set
            {
                _cursorPosition = value;
                TypingCursorPositionChanged?.Invoke(this);
            }
        }

        /// <summary>
        /// Number of incorrect button presses.
        /// </summary>
        public int MissesNumber
        {
            get => _missesNumber;
            private set
            {
                _missesNumber = value;
                MissesNumberChanged?.Invoke(this);
            }
        }

        public float TypingSpeed
        {
            get => (float)(TypingCursorPosition / _typingTimer.Elapsed.TotalMinutes);
        }

        public float TypingAccuracy
        {
            get => (float)MissesNumber / (MissesNumber + TypingCursorPosition);
        }

        public bool TrainingPaused
        {
            get => _isPaused;
        }

        /// <summary>
        /// Starts the training.
        /// </summary>
        public void Start()
        {
            _isPaused = false;
            _typingTimer.Start();
        }

        public void Pause()
        {
            _isPaused = true;
            _typingTimer.Stop();
        }

        /// <summary>
        /// Сhecks for a character that the user has entered from the keyboard.
        /// If entered a valid character, moves typing cursor.
        /// </summary>
        /// <param name="input">Entered character.</param>
        /// <returns>
        /// True if user has entered a valid character and training not paused. False overwise.
        /// </returns>
        public bool CheckInputChar(char input)
        {
            if (!_isPaused)
            {
                if (input != CurrentTypingText.Content[TypingCursorPosition])
                {
                    MissesNumber++;
                    return false;
                }

                TypingCursorPosition++;
                if (TypingCursorPosition >= CurrentTypingText.Content.Length)
                {
                    TypingCompleted?.Invoke(this);
                    ChangeTypingText();
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// Sets new typing text.
        /// </summary>
        public void ChangeTypingText()
        {
            CurrentTypingText = _textsProvider.GetNextText();
            ResetTraining();
        }

        /// <summary>
        /// Resets training progress.
        /// </summary>
        public void ResetTraining()
        {
            Pause();
            TypingCursorPosition = 0;
            MissesNumber = 0;
            _typingTimer.Reset();
        }
    }
}
