using System;
using TypingTraining.FingerPointer.FingersLayouts;

namespace TypingTraining.FingerPointer.Pointers
{
    public class FingerPointer : IFingerPointer
    {
        private readonly IFingersLayout _fingersLayout;

        public FingerPointer(IFingersLayout fingersLayout)
        {
            _fingersLayout = fingersLayout ?? throw new ArgumentNullException(nameof(fingersLayout));
        }

        public Fingers GetFinger(char typedCharacter)
        {
            var charsets = _fingersLayout.FingersCharsets;
            foreach (var charset in charsets)
            {
                if (charset.Value.Contains(typedCharacter.ToString()))
                {
                    return charset.Key;
                }
            }

            return Fingers.None;
        }
    }
}
