using System;
using ODIF;

namespace MouseKeyboardOutput
{
    internal class KeyboardOutputDevice
    {
        public Button Ctrl { get; set; } = new Button("Ctrl", DataFlowDirection.Output);
        public Button Shift { get; set; } = new Button("Shift", DataFlowDirection.Output);
        public Button Enter { get; set; } = new Button("Enter", DataFlowDirection.Output);
        public Button Backspace { get; set; } = new Button("Backspace", DataFlowDirection.Output);
        public Button Esc { get; set; } = new Button("Esc", DataFlowDirection.Output);
        public Button AltEnter { get; set; } = new Button("Alt & Enter", DataFlowDirection.Output);
        public Button OnScreenKeyboard { get; set; } = new Button("Show/Hide On-Screen Keyboard", DataFlowDirection.Output);
    }
}