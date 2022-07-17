using System.Collections.Generic;

namespace TypingTraining.FingerPointer.FingersLayouts
{
    /// <summary>
    /// Describes the position of the fingers on the keyboard.
    /// </summary>
    public interface IFingersLayout
    {
        /// <summary>
        /// Provides character sets to be entered with each finger.
        /// </summary>
        Dictionary<Fingers, string> FingersCharsets { get; }
    }
}
