using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices;
namespace MouseKeyboardOutput
{
    internal class OnScreenKeyboardWrapper
    {
        private static string PathToKeyboardApp
        {
            get;
            set;
        } = Path.Combine(Environment.GetEnvironmentVariable("CommonProgramW6432"), @"microsoft shared\ink\TabTip.exe");
        public static void Toggle()
        {
            var processesByName = Process.GetProcessesByName("TabTip");
            if (processesByName.Any())
            {
                foreach (var process in processesByName)
                {
                    process.Kill();
                }
                return;
            }
            Process.Start(PathToKeyboardApp);
        }
    }
}
