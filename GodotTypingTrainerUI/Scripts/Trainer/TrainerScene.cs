﻿using System;
using Godot;
using TypingTraining.TypingTrainers;
using GodotTypingTrainerUI.Scripts.Extentions;

namespace GodotTypingTrainerUI.Scripts.Trainer
{
    public class TrainerScene : Node
    {
        private TypingTrainer _trainer;
        private int _scrollingCharsNumber = 60;
        private int _charactersToScroll;

        private TextEdit _typingTextView;
        private PausePanel _pausePanel;
        private CompletedPanel _completedPanel;
        private TypingInfoContainer _infoContainer;
        private Label _helperLabel;
        private Hands _hands;

        private TrainerSoundsPlayer _soundsPlayer;

        public override void _Ready()
        {
            var global = this.GetGlobal();
            _trainer = global.GetGlobalParameterValue<TypingTrainer>("trainer");

            if (_trainer is null)
            {
                throw new ApplicationException("You must add TypingTrainer parameter in GlobalParameters first. " +
                    "Use Global.SetGlobalParameter(\"trainer\", TypingTrainer) before going to TrainerScene.");
            }

            _trainer.TypingTextChanged += OnTypingTextChanged;
            _trainer.TypingCursorPositionChanged += OnTypingCursorPositionChanged;
            _trainer.MissesNumberChanged += OnMissesNumberChanged;
            _trainer.TypingCompleted += OnTypingCompleted;

            _typingTextView = GetNode<TextEdit>("TextPanel/TypingTextView");
            _typingTextView.Text = _trainer.CurrentTypingText.Content;
            _typingTextView.DrawSpaces = global.ApplicationSettings.DrawSpaces;

            _pausePanel = GetNode<PausePanel>("PausePanel");
            _completedPanel = GetNode<CompletedPanel>("CompletedPanel");
            _infoContainer = GetNode<TypingInfoContainer>("TypingInfoContainer");
            _helperLabel = GetNode<Label>("HelperLabel");

            _hands = GetNode<Hands>("Hands");
            char nextChar = GetNextTypeChar();
            _hands.UpdateFingerPrompt(nextChar);

            _soundsPlayer = GetNode<TrainerSoundsPlayer>("SoundsPlayer");

            _charactersToScroll = _scrollingCharsNumber;
        }

        public override void _UnhandledInput(InputEvent @event)
        {
            if (@event is InputEventKey eventKey && IsTypingAvailable())
            {
                var unicode = eventKey.Unicode;
                var bytes = BitConverter.GetBytes(unicode);
                var inputChar = BitConverter.ToChar(bytes, 0);

                if (char.IsLetterOrDigit(inputChar) ||
                    char.IsSeparator(inputChar) ||
                    char.IsSymbol(inputChar) ||
                    char.IsPunctuation(inputChar))
                {
                    HandleInputChar(inputChar);
                }
            }
        }

        public override void _ExitTree()
        {
            this.GetGlobal().RemoveGlobalParameter("trainer");
            base._ExitTree();
        }

        private bool IsTypingAvailable()
        {
            return !_pausePanel.Visible && !_completedPanel.Visible;
        }

        private void PauseTraining()
        {
            _helperLabel.Visible = true;
            _trainer.Pause();
            _pausePanel.Open();
        }

        private void ContinueTraining()
        {
            _pausePanel.Close();
            _trainer.Start();
        }

        private void ChangeTypingText()
        {
            _trainer.ChangeTypingText();
            _pausePanel.Close();
        }

        private void OnMissesNumberChanged(object sender)
        {
            _infoContainer.UpdateMissesInfo(_trainer.MissesNumber);
        }

        private async void OnTypingCompleted(object sender)
        {
            UpdateTotalAccuracyStatistics();
            UpdateTotalSpeedStatistics();
            this.GetGlobal().UserStatistics.WrittenTextsNumber++;

            _soundsPlayer.PlayCompletedSound();

            _completedPanel.Open(_trainer.TypingSpeed, _trainer.TypingAccuracy);

            await this.GetGlobal().SaveUserStatisticsAsync();
        }

        private void UpdateTotalAccuracyStatistics()
        {
            var statistics = this.GetGlobal().UserStatistics;
            float oldTotalAccuracy = statistics.TotalAccuracy;
            float newTotalAccuracy = _trainer.TypingAccuracy;
            int writtenTextsNumber = statistics.WrittenTextsNumber;

            newTotalAccuracy = (oldTotalAccuracy * writtenTextsNumber + _trainer.TypingAccuracy) / (writtenTextsNumber + 1);

            statistics.TotalAccuracy = newTotalAccuracy;
        }

        private void UpdateTotalSpeedStatistics()
        {
            var statistics = this.GetGlobal().UserStatistics;
            float oldTotalSpeed = statistics.TotalSpeed;
            float newTotalSpeed = _trainer.TypingSpeed;
            int writtenTextsNumber = statistics.WrittenTextsNumber;

            newTotalSpeed = (oldTotalSpeed * writtenTextsNumber + _trainer.TypingSpeed) / (writtenTextsNumber + 1);

            statistics.TotalSpeed = newTotalSpeed;
        }

        private void HandleInputChar(char inputChar)
        {
            if (_trainer.IsTrainingPaused)
            {
                _trainer.Start();
                _helperLabel.Hide();
            }

            if (_trainer.CheckInputChar(inputChar))
            {
                _charactersToScroll--;
                if (_charactersToScroll == 0)
                {
                    _typingTextView.ScrollVertical++;
                    _charactersToScroll = _scrollingCharsNumber;
                }
            }
            else
            {
                _soundsPlayer.PlayMissSound();
                _hands.PlayMissAnimation();
            }

            _infoContainer.UpdateSpeedInfo(_trainer.TypingSpeed);
            _infoContainer.UpdateAccuracyInfo(_trainer.TypingAccuracy);
        }

        private void OnTypingCursorPositionChanged(object sender)
        {
            _typingTextView.Select(0, 0, 0, _trainer.TypingCursorPosition);

            char nextChar = GetNextTypeChar();
            _hands.UpdateFingerPrompt(nextChar);
        }

        private char GetNextTypeChar()
        {
            if (_trainer.TypingCursorPosition >= _trainer.CurrentTypingText.Content.Length)
            {
                return '\0';
            }

            return _trainer.CurrentTypingText.Content[_trainer.TypingCursorPosition];
        }

        private void OnTypingTextChanged(object sender)
        {
            _typingTextView.Text = _trainer.CurrentTypingText.Content;
            _charactersToScroll = _scrollingCharsNumber;

            _helperLabel.Show();
            _infoContainer.ResetInfo();
        }
    }
}
