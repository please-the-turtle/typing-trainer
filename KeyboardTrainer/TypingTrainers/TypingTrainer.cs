using System;
using System.Diagnostics;
using TypingTraining.TypingTexts;

namespace TypingTraining.TypingTrainers
{
    /// <summary>
    /// Trainer of the keyboard typing.
    /// </summary>
    public abstract class TypingTrainer
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

        public TypingTrainer(ITypingTextsProvider textsProvider)
        {
            if (textsProvider is null)
            {
                throw new ArgumentNullException(nameof(textsProvider));
            }

            _textsProvider = textsProvider;
            _typingTimer = new Stopwatch();

            ChangeTypingText();
        }

        /// <summary>
        /// Gets text that should enter user.
        /// </summary>
        public TypingText CurrentTypingText
        {
            get => _currentText;
            protected set
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
            protected set
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
            protected set
            {
                _missesNumber = value;
                MissesNumberChanged?.Invoke(this);
            }
        }

        /// <summary>
        /// Typing speed in correct chars per minute.
        /// </summary>
        public float TypingSpeed
        {
            get
            {
                double elapsedMinutes = _typingTimer.Elapsed.TotalMinutes;

                if (elapsedMinutes == 0)
                {
                    return 0;
                }

                return (float)(TypingCursorPosition / elapsedMinutes);
            }
        }

        /// <summary>
        /// Number of correct typed chars divided by all typed chars.
        /// </summary>
        public float TypingAccuracy
        {
            get
            {
                int allTypedChars = MissesNumber + TypingCursorPosition;
                
                if (allTypedChars == 0)
                {
                    return 0;
                }

                return (float)TypingCursorPosition / (allTypedChars);
            }
        }

        public bool IsTrainingPaused
        {
            get => _isPaused;
        }

        /// <summary>
        /// Starts the training.
        /// </summary>
        public virtual void Start()
        {
            _isPaused = false;
            _typingTimer.Start();
        }

        public virtual void Pause()
        {
            _isPaused = true;
            _typingTimer.Stop();
        }

        protected virtual void CompleteTraining()
        {
            TypingCompleted?.Invoke(this);
            ChangeTypingText();
        }

        /// <summary>
        /// Сhecks for a character that the user has entered from the keyboard.
        /// </summary>
        /// <param name="input">Entered character.</param>
        /// <returns>
        /// True if user has entered a valid character and training not paused. False overwise.
        /// </returns>
        public abstract bool CheckInputChar(char input);

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
        public virtual void ResetTraining()
        {
            Pause();
            TypingCursorPosition = 0;
            MissesNumber = 0;
            _typingTimer.Reset();
        }
    }
}
