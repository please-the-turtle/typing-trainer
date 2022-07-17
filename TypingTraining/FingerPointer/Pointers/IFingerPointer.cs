namespace TypingTraining.FingerPointer.Pointers
{
    /// <summary>
    /// Specifies the fingers with which to enter text on the keyboard.
    /// </summary>
    public interface IFingerPointer
    {
        /// <summary>
        /// Indicates the finger with which a key with a specific character should be pressed.
        /// </summary>
        /// <param name="typedCharacter"></param>
        /// <returns>Finger with which a key with a specific character.</returns>
        Fingers GetFinger(char typedCharacter);
    }
}
