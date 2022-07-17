using System;

namespace GodotTypingTrainerUI.Scripts.Globals
{
    internal class GlobalParameter
    {
        internal object Value
        {
            set => _value = value;
        }

        internal string ParameterName
        {
            get => _parameterName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException($"'{nameof(value)}' cannot be null or whitespace.", nameof(value));
                }

                _parameterName = value;
            }
        }
        private string _parameterName;

        private object _value;

        internal GlobalParameter(string parameterName, object value)
        {
            ParameterName = parameterName;
            _value = value;
        }

        internal T GetValue<T>()
        {
            return (T)_value;
        }
    }
}

