using System;
using System.Management.Automation;
using System.Text;

namespace PSWindows
{
    [Cmdlet("Set", "WindowPlacement")]
    public class SetWindowPlacementCommand : PSCmdlet
    {
        [Parameter(Mandatory = true, ParameterSetName = "Hwnd", ValueFromPipeline = true)]
        public IntPtr Hwnd { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = "Window", ValueFromPipeline = true)]
        public Window Window { get; set; }

        [Parameter()]
        public WINDOWPLACEMENT Placement { get; set; }

        protected override void ProcessRecord()
        {
            var hwnd = Hwnd;
            if (ParameterSetName == "Window")
            {
                hwnd = Window.Hwnd;
            }

            var placement = Placement;
            Native.SetWindowPlacement(hwnd, ref placement);
        }
    }
}