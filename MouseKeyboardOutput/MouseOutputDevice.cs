using System;
using ODIF;

namespace MouseKeyboardOutput
{
    internal class MouseOutputDevice
    {
        public JoyAxis CursorX { get; set; } = new JoyAxis("CursorX", DataFlowDirection.Output);

        public JoyAxis CursorY { get; set; } = new JoyAxis("CursorY", DataFlowDirection.Output);

        public JoyAxis DeltaX { get; set; } = new JoyAxis("DeltaX", DataFlowDirection.Output);

        public JoyAxis DeltaY { get; set; } = new JoyAxis("DeltaY", DataFlowDirection.Output);

        public Button LeftButton { get; set; } = new Button("Left button", DataFlowDirection.Output);

        public Button RightButton { get; set; } = new Button("Right button", DataFlowDirection.Output);
        public Button MiddleButton { get; set; } = new Button("Middle button", DataFlowDirection.Output);
        public Button FourthButton { get; set; } = new Button("Fourth button", DataFlowDirection.Output);
        public Button FifthButton { get; set; } = new Button("Fifth button", DataFlowDirection.Output);
        public Button ScrollUp { get; set; } = new Button("Scroll wheel up", DataFlowDirection.Output);
        public Button ScrollDown { get; set; } = new Button("Scroll wheel down", DataFlowDirection.Output);
        public Button ScrollRight { get; set; } = new Button("Scroll wheel right", DataFlowDirection.Output);
        public Button ScrollLeft { get; set; } = new Button("Scroll wheel left", DataFlowDirection.Output);
    }
}