﻿using System;
using Newtonsoft.Json;

namespace GodotTypingTrainerUI.Scripts
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ApplicationSettings
    {
        public const string SettingsPath = "user://settings.save";

        public string TypingTextsExtention => ".type";

        public bool IsSoundsEnabled
        {
            get => _isSoundsEnabled;
            set
            {
                if (_isSoundsEnabled != value)
                {
                    _isSettingsChanged = true;
                }

                _isSoundsEnabled = value;
            }
        }

        public string TextsPath
        {
            get => _textsPath;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Texts path string must be not null or empty.", nameof(value));
                }

                if (_textsPath != value)
                {
                    _isSettingsChanged = true;
                }

                _textsPath = value;
            }
        }

        public int LastTypingTextsIndex 
        {
            get => _lastTypingTextsIndex;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("LastTypingTexts greater than zero.", nameof(value));
                }

                if (_lastTypingTextsIndex != value)
                {
                    _isSettingsChanged = true;
                }

                _lastTypingTextsIndex = value;
            }
        }

        public bool IsSettingsChanged => _isSettingsChanged;

        [JsonProperty("lastTypingTexts")]
        private int _lastTypingTextsIndex;

        [JsonProperty("soundsEnabled")]
        private bool _isSoundsEnabled = true;

        [JsonProperty("textsPath")]
        private string _textsPath = "user://texts";

        private bool _isSettingsChanged = false;
    }
}