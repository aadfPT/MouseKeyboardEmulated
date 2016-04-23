using System;
using ODIF;

namespace MouseKeyboardOutput
{
    internal class MouseOutputDevice
    {
        public InputChannelTypes.JoyAxis CursorX { get; set; } = new InputChannelTypes.JoyAxis("CursorX", String.Empty);

        public InputChannelTypes.JoyAxis CursorY { get; set; } = new InputChannelTypes.JoyAxis("CursorY", String.Empty);

        public InputChannelTypes.JoyAxis DeltaX { get; set; } = new InputChannelTypes.JoyAxis("DeltaX", String.Empty);

        public InputChannelTypes.JoyAxis DeltaY { get; set; } = new InputChannelTypes.JoyAxis("DeltaY", String.Empty);

        public InputChannelTypes.Button LeftButton { get; set; } = new InputChannelTypes.Button("Left button", String.Empty);

        public InputChannelTypes.Button RightButton { get; set; } = new InputChannelTypes.Button("Right button", String.Empty);
        public InputChannelTypes.Button MiddleButton { get; set; } = new InputChannelTypes.Button("Middle button", String.Empty);
        public InputChannelTypes.Button FourthButton { get; set; } = new InputChannelTypes.Button("Fourth button", String.Empty);
        public InputChannelTypes.Button FifthButton { get; set; } = new InputChannelTypes.Button("Fifth button", String.Empty);
        public InputChannelTypes.Button ScrollUp { get; set; } = new InputChannelTypes.Button("Scroll wheel up", String.Empty);
        public InputChannelTypes.Button ScrollDown { get; set; } = new InputChannelTypes.Button("Scroll wheel down", String.Empty);
        public InputChannelTypes.Button ScrollRight { get; set; } = new InputChannelTypes.Button("Scroll wheel right", String.Empty);
        public InputChannelTypes.Button ScrollLeft { get; set; } = new InputChannelTypes.Button("Scroll wheel left", String.Empty);
    }
}