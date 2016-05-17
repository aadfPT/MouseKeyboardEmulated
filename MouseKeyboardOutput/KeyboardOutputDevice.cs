using System;
using ODIF;

namespace MouseKeyboardOutput
{
    internal class KeyboardOutputDevice
    {
        public Button Ctrl { get; set; } = new Button("Ctrl", DataFlowDirection.Input);
        public Button Shift { get; set; } = new Button("Shift", DataFlowDirection.Input);
        public Button Enter { get; set; } = new Button("Enter", DataFlowDirection.Input);
        public Button Backspace { get; set; } = new Button("Backspace", DataFlowDirection.Input);
        public Button Esc { get; set; } = new Button("Esc", DataFlowDirection.Input);
        public Button AltEnter { get; set; } = new Button("Alt & Enter", DataFlowDirection.Input);
        public Button OnScreenKeyboard { get; set; } = new Button("Show/Hide On-Screen Keyboard", DataFlowDirection.Input);
    }
}