using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using WindowsInput;
using WindowsInput.Native;
using ODIF;

namespace MouseKeyboardOutput
{
    public class MyMouseKeyboardOutputDevice : OutputDevice
    {
        internal InputSimulator Device { get; set; } = new InputSimulator();
        internal MouseOutputDevice MouseWrapper { get; set; } = new MouseOutputDevice();
        internal KeyboardOutputDevice KeyboardWrapper { get; set; } = new KeyboardOutputDevice();
        internal Thread MouseDeltasThread { get; set; }
        internal Thread MouseScrollsThread { get; set; }

        private bool StopThread { get; set; }

        public MyMouseKeyboardOutputDevice()
        {
            base.DeviceName = "Emulated Mouse & Keyboard";

            AddInputChannels();
            AddInputListeners();
            MouseDeltasThread = new Thread(DeltasMonitor);
            MouseScrollsThread = new Thread(ScrollsMonitor);
            Global.Mappings.CollectionChanged += ControlThreads;
        }

        private void ControlThreads(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (Global.Mappings.Any(m => DeltasAreInUse(m.SourceChannel) || m.Mappings.Any(DeltasAreInUse)))
            {
                if (!MouseDeltasThread.IsAlive)
                {
                    MouseDeltasThread = new Thread(DeltasMonitor);
                    MouseDeltasThread.Start();
                }
            }
            else
            {
                if (MouseDeltasThread.IsAlive) MouseDeltasThread.Abort();
            }
            if (Global.Mappings.Any(m => ScrollsAreInUse(m.SourceChannel) || m.Mappings.Any(ScrollsAreInUse)))
            {
                if (MouseScrollsThread.IsAlive) return;
                MouseScrollsThread = new Thread(ScrollsMonitor);
                MouseScrollsThread.Start();
            }
            else
            {
                if (MouseScrollsThread.IsAlive) MouseScrollsThread.Abort();
            }
        }

        private bool DeltasAreInUse(DeviceChannel m)
        {
            var channelName = m.ChannelName;
            return m.Parent == this && channelName.StartsWith("Delta");

        }
        private bool ScrollsAreInUse(DeviceChannel m)
        {
            var channelName = m.ChannelName;
            return m.Parent == this && channelName.StartsWith("Scroll wheel");

        }
        private void AddInputListeners()
        {
            MouseWrapper.CursorY.PropertyChanged += CursorPositionOnPropertyChanged;
            MouseWrapper.CursorX.PropertyChanged += CursorPositionOnPropertyChanged;
            MouseWrapper.LeftButton.PropertyChanged += LeftButtonOnPropertyChanged;
            MouseWrapper.RightButton.PropertyChanged += RightButtonOnPropertyChanged;
            MouseWrapper.MiddleButton.PropertyChanged += MiddleButtonOnPropertyChanged;
            MouseWrapper.FourthButton.PropertyChanged += FourthButtonOnPropertyChanged;
            MouseWrapper.FifthButton.PropertyChanged += FifthButtonOnPropertyChanged;
            KeyboardWrapper.Ctrl.PropertyChanged += CtrlOnPropertyChanged;
            KeyboardWrapper.Shift.PropertyChanged += ShiftOnPropertyChanged;
            KeyboardWrapper.Enter.PropertyChanged += EnterOnPropertyChanged;
            KeyboardWrapper.Backspace.PropertyChanged += BackspaceOnPropertyChanged;
            KeyboardWrapper.Esc.PropertyChanged += EscOnPropertyChanged;
            KeyboardWrapper.AltEnter.PropertyChanged += AltEnterOnPropertyChanged;
            KeyboardWrapper.OnScreenKeyboard.PropertyChanged += OnScreenKeyboardOnPropertyChanged;
        }

        private void LeftButtonOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (MouseWrapper.LeftButton.Value)
            {
                Device.Mouse.LeftButtonDown();
            }
            else {
                Device.Mouse.LeftButtonUp();
            }
        }


        private void RightButtonOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (MouseWrapper.RightButton.Value)
            {
                Device.Mouse.RightButtonDown();
            }
            else {
                Device.Mouse.RightButtonUp();
            }
        }
        private void MiddleButtonOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (MouseWrapper.MiddleButton.Value)
            {
                Device.Mouse.MiddleButtonDown();
            }
            else {
                Device.Mouse.MiddleButtonUp();
            }
        }
        private void FourthButtonOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (MouseWrapper.FourthButton.Value)
            {
                Device.Mouse.XButtonDown(1);
            }
            else {
                Device.Mouse.XButtonUp(1);
            }
        }
        private void FifthButtonOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (MouseWrapper.FifthButton.Value)
            {
                Device.Mouse.XButtonDown(2);
            }
            else {
                Device.Mouse.XButtonUp(2);
            }
        }
        private void CtrlOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (KeyboardWrapper.Ctrl.Value)
            {
                Device.Keyboard.KeyDown(VirtualKeyCode.CONTROL);
            }
            else
            {

                Device.Keyboard.KeyUp(VirtualKeyCode.CONTROL);
            }
        }
        private void ShiftOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (KeyboardWrapper.Shift.Value)
            {
                Device.Keyboard.KeyDown(VirtualKeyCode.SHIFT);
            }
            else
            {

                Device.Keyboard.KeyUp(VirtualKeyCode.SHIFT);
            }
        }
        private void EnterOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (KeyboardWrapper.Enter.Value)
            {
                Device.Keyboard.KeyDown(VirtualKeyCode.RETURN);
            }
            else
            {

                Device.Keyboard.KeyUp(VirtualKeyCode.RETURN);
            }
        }
        private void EscOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (KeyboardWrapper.Esc.Value)
            {
                Device.Keyboard.KeyDown(VirtualKeyCode.ESCAPE);
            }
            else
            {

                Device.Keyboard.KeyUp(VirtualKeyCode.ESCAPE);
            }
        }
        private void BackspaceOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (KeyboardWrapper.Backspace.Value)
            {
                Device.Keyboard.KeyDown(VirtualKeyCode.BACK);
            }
            else
            {

                Device.Keyboard.KeyUp(VirtualKeyCode.BACK);
            }
        }

        private void AltEnterOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (KeyboardWrapper.AltEnter.Value)
            {
                Device.Keyboard.ModifiedKeyStroke(VirtualKeyCode.MENU, VirtualKeyCode.RETURN);
            }
        }

        private void OnScreenKeyboardOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (KeyboardWrapper.OnScreenKeyboard.Value)
            {
                OnScreenKeyboardWrapper.Toggle();
            }
        }

        private void DeltasMonitor()
        {
            while (!this.StopThread && !Global.IsShuttingDown)
            {
                Device.Mouse.MoveMouseBy(Convert.ToInt32(Math.Floor(MouseWrapper.DeltaX.Value * 5)),
                    Convert.ToInt32(Math.Floor(MouseWrapper.DeltaY.Value * 5)));
                Thread.Sleep(5);
            }
        }
        private void ScrollsMonitor()
        {
            while (!this.StopThread && !Global.IsShuttingDown)
            {
                if (MouseWrapper.ScrollDown.Value)
                {
                    Device.Mouse.VerticalScroll(-1);
                }

                if (MouseWrapper.ScrollUp.Value)
                {
                    Device.Mouse.VerticalScroll(1);
                }
                if (MouseWrapper.ScrollRight.Value)
                {
                    Device.Mouse.HorizontalScroll(1);
                }
                if (MouseWrapper.ScrollLeft.Value)
                {
                    Device.Mouse.HorizontalScroll(-1);
                }
                Thread.Sleep(20);
            }
        }

        private void CursorPositionOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            Device.Mouse.MoveMouseTo(32767 * Math.Abs(Math.Min(1, MouseWrapper.CursorX.Value) + 1), 32767 * Math.Abs(Math.Min(1, MouseWrapper.CursorY.Value) + 1));
        }

        private void AddInputChannels()
        {
            InputChannels.Add(MouseWrapper.CursorX);
            InputChannels.Add(MouseWrapper.CursorY);
            InputChannels.Add(MouseWrapper.DeltaX);
            InputChannels.Add(MouseWrapper.DeltaY);
            InputChannels.Add(MouseWrapper.LeftButton);
            InputChannels.Add(MouseWrapper.RightButton);
            InputChannels.Add(MouseWrapper.MiddleButton);
            InputChannels.Add(MouseWrapper.FourthButton);
            InputChannels.Add(MouseWrapper.FifthButton);
            InputChannels.Add(MouseWrapper.ScrollDown);
            InputChannels.Add(MouseWrapper.ScrollUp);
            InputChannels.Add(MouseWrapper.ScrollRight);
            InputChannels.Add(MouseWrapper.ScrollLeft);
            InputChannels.Add(KeyboardWrapper.Ctrl);
            InputChannels.Add(KeyboardWrapper.Shift);
            InputChannels.Add(KeyboardWrapper.Enter);
            InputChannels.Add(KeyboardWrapper.Backspace);
            InputChannels.Add(KeyboardWrapper.Esc);
            InputChannels.Add(KeyboardWrapper.AltEnter);
            InputChannels.Add(KeyboardWrapper.OnScreenKeyboard);
        }
        protected override void Dispose(bool disposing)
        {
            StopThread = true;
            MouseDeltasThread?.Abort();
            MouseScrollsThread?.Abort();
            base.Dispose(disposing);
        }
    }

}