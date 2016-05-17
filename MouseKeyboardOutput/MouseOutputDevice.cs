using System;
using ODIF;

namespace MouseKeyboardOutput
{
    internal class MouseOutputDevice
    {
        public JoyAxis CursorX { get; set; } = new JoyAxis("CursorX", DataFlowDirection.Input);

        public JoyAxis CursorY { get; set; } = new JoyAxis("CursorY", DataFlowDirection.Input);

        public JoyAxis DeltaX { get; set; } = new JoyAxis("DeltaX", DataFlowDirection.Input);

        public JoyAxis DeltaY { get; set; } = new JoyAxis("DeltaY", DataFlowDirection.Input);

        public Button LeftButton { get; set; } = new Button("Left button", DataFlowDirection.Input);

        public Button RightButton { get; set; } = new Button("Right button", DataFlowDirection.Input);
        public Button MiddleButton { get; set; } = new Button("Middle button", DataFlowDirection.Input);
        public Button FourthButton { get; set; } = new Button("Fourth button", DataFlowDirection.Input);
        public Button FifthButton { get; set; } = new Button("Fifth button", DataFlowDirection.Input);
        public Button ScrollUp { get; set; } = new Button("Scroll wheel up", DataFlowDirection.Input);
        public Button ScrollDown { get; set; } = new Button("Scroll wheel down", DataFlowDirection.Input);
        public Button ScrollRight { get; set; } = new Button("Scroll wheel right", DataFlowDirection.Input);
        public Button ScrollLeft { get; set; } = new Button("Scroll wheel left", DataFlowDirection.Input);
    }
}