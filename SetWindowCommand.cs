using System;
using System.Management.Automation;
using System.Text;

namespace PSWindows
{
    [Cmdlet("Set", "Window")]
    public class SetWindowCommand : PSCmdlet
    {
        [Parameter(Mandatory = true, ParameterSetName = "Hwnd", ValueFromPipeline = true)]
        public IntPtr Hwnd { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = "Window", ValueFromPipeline = true)]
        public Window Window { get; set; }

        [Parameter(Mandatory = true)]
        public GWL Index { get; set; }

        [Parameter()]
        public WindowStyles Style { get; set; }

        protected override void ProcessRecord()
        {
            var hwnd = Hwnd;
            if (ParameterSetName == "Window")
            {
                hwnd = Window.Hwnd;
            }

            Native.SetWindowLong(hwnd, (int)Index, (uint)Style);
        }
    }
}