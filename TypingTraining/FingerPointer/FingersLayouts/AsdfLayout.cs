using System.Collections.Generic;

namespace TypingTraining.FingerPointer.FingersLayouts
{
    public class AsdfLayout : IFingersLayout
    {
        public Dictionary<Fingers, string> FingersCharsets => _charsets;

        private Dictionary<Fingers, string> _charsets;

        public AsdfLayout()
        {
            _charsets = new Dictionary<Fingers, string>();

            _charsets.Add(Fingers.LeftThumb, " ");
            _charsets.Add(Fingers.LeftIndex, "frvbgt45VFRTGBамкепиАМКЕПИ%");
            _charsets.Add(Fingers.LeftMiddle, "dec3DECвсу3ВСУ");
            _charsets.Add(Fingers.LeftRing, "swx2SWXычц2ЫЧЦ");
            _charsets.Add(Fingers.LeftLittle, "azq1AZQ!фяйФЯЙёЁ");

            _charsets.Add(Fingers.RightThumb, " ");
            _charsets.Add(Fingers.RightIndex, "jmuhny67JMUHNYоьгрнтРНТОЬГ");
            _charsets.Add(Fingers.RightMiddle, "ki8KIлбшЛБШ");
            _charsets.Add(Fingers.RightRing, "lo9LOдющДЮЩ(");
            _charsets.Add(Fingers.RightLittle, "p0P-_=+)жзхъэЖЗХЪЭ");
        }
    }
}
