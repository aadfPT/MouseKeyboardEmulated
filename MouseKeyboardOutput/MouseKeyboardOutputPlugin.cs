using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ODIF;
using ODIF.Extensions;
using System.Management;
using System.Reflection;

namespace MouseKeyboardOutput
{
    [PluginInfo(
        PluginName = "Mouse & Keyboard Emulated",
        PluginDescription = "Adds support to output mouse and keyboard emulated devices",
        PluginVersion = "1.0.0.3",
        PluginID = 58,
        PluginAuthorName = "André Ferreira",
        PluginAuthorEmail = "aadf.pt [at] gmail [dot] com",
        PluginAuthorURL = "https://github.com/aadfPT",
        PluginIconPath = @"pack://application:,,,/MouseKeyboardOutput;component/Resources/Icon.png"
        )]
    public class MouseKeyboardOutputPlugin : OutputDevicePlugin
    {
        public MouseKeyboardOutputPlugin()
        {
            Global.HardwareChangeDetected += CheckForControllersEvent;
            CheckForControllers();
        }

        private void CheckForControllersEvent(object sender, EventArrivedEventArgs e)
        {
            CheckForControllers();
        }

        private void CheckForControllers()
        {
            lock (base.Devices)
            {
                if (Devices.Any(d => d.DeviceName == "Emulated Mouse & Keyboard"))
                {
                    return;
                }
                Devices.Add(new MyMouseKeyboardOutputDevice());
            }
        }
    }
}