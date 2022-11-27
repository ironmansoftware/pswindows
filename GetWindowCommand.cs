using System;
using System.Management.Automation;
using System.Text;
using System.Text.RegularExpressions;

namespace PSWindows
{
    [Cmdlet("Get", "Window")]
    public class GetWindowCommand : PSCmdlet
    {

        [Parameter()]
        public string ClassName { get; set; }

        [Parameter()]
        public string Title { get; set; }

        [Parameter(ValueFromPipeline = true)]
        public Window Parent { get; set; }

        protected override void ProcessRecord()
        {
            if (Parent != null)
            {
                Native.EnumChildWindows(Parent.Hwnd, new Native.EnumWindowsProc(EnumWindowProc), IntPtr.Zero);
            }
            else
            {
                Native.EnumWindows(new Native.EnumWindowsProc(EnumWindowProc), IntPtr.Zero);
            }
        }

        private bool EnumWindowProc(IntPtr hwnd, IntPtr lParam)
        {
            var title = new StringBuilder(1024);
            Native.GetWindowText(hwnd, title, title.Capacity);

            var className = new StringBuilder(1024);
            Native.GetClassName(hwnd, className, className.Capacity);

            if (IsMatch(className.ToString(), ClassName) && IsMatch(title.ToString(), Title))
            {

                var placement = new WINDOWPLACEMENT();
                Native.GetWindowPlacement(hwnd, ref placement);
                Native.GetWindowRect(hwnd, out RECT rect);

                WriteObject(new Window
                {
                    Hwnd = hwnd,
                    Styles = (WindowStyles)Native.GetWindowLongA(hwnd, GWL.GWL_STYLE),
                    ExtendedStyles = (WindowStyles)Native.GetWindowLongA(hwnd, GWL.GWL_EXSTYLE),
                    Title = title.ToString(),
                    ClassName = className.ToString(),
                    Placement = placement,
                    Rectangle = rect
                });
            }

            return true;
        }

        private bool IsMatch(string value, string pattern)
        {
            if (string.IsNullOrEmpty(pattern)) return true;

            if (pattern.Contains("*") && Regex.IsMatch(value, Regex.Escape(ClassName).Replace("\\*", ".*?")))
            {
                return true;
            }

            return pattern.Equals(value, StringComparison.OrdinalIgnoreCase);
        }
    }
}
