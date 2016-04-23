using System;
using ODIF;

namespace MouseKeyboardOutput
{
    internal class KeyboardOutputDevice
    {
        public InputChannelTypes.Button Ctrl { get; set; } = new InputChannelTypes.Button("Ctrl", String.Empty);
        public InputChannelTypes.Button Shift { get; set; } = new InputChannelTypes.Button("Shift", String.Empty);
        public InputChannelTypes.Button Enter { get; set; } = new InputChannelTypes.Button("Enter", String.Empty);
        public InputChannelTypes.Button Backspace { get; set; } = new InputChannelTypes.Button("Backspace", String.Empty);
        public InputChannelTypes.Button Esc { get; set; } = new InputChannelTypes.Button("Esc", String.Empty);
        public InputChannelTypes.Button AltEnter { get; set; } = new InputChannelTypes.Button("Alt & Enter", String.Empty);
        public InputChannelTypes.Button OnScreenKeyboard { get; set; } = new InputChannelTypes.Button("Show/Hide On-Screen Keyboard", String.Empty);
    }
}