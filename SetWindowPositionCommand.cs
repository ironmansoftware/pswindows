using System;
using System.Management.Automation;
using System.Text;

namespace PSWindows
{
    [Cmdlet("Set", "WindowPosition")]
    public class SetWindowPositionCommand : PSCmdlet
    {
        [Parameter(Mandatory = true, ParameterSetName = "Hwnd", ValueFromPipeline = true)]
        public IntPtr Hwnd { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = "Window", ValueFromPipeline = true)]
        public Window Window { get; set; }

        [Parameter()]
        public IntPtr InsertAfter { get; set; }
        [Parameter()]
        public int X { get; set; }
        [Parameter()]
        public int Y { get; set; }
        [Parameter()]
        public int ClientX { get; set; }
        [Parameter()]
        public int ClientY { get; set; }

        [Parameter()]
        public SetWindowPosFlags Flags { get; set; }

        protected override void ProcessRecord()
        {
            var hwnd = Hwnd;
            if (ParameterSetName == "Window")
            {
                hwnd = Window.Hwnd;
            }

            Native.SetWindowPos(hwnd, InsertAfter, X, Y, ClientX, ClientY, Flags);
        }
    }
}