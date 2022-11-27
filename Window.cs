using System;

namespace PSWindows
{
    public class Window
    {
        public IntPtr Hwnd { get; set; }
        public WindowStyles Styles { get; set; }
        public WindowStyles ExtendedStyles { get; set; }
        public string Title { get; set; }
        public string ClassName { get; set; }
        public WINDOWPLACEMENT Placement { get; set; }
        public RECT Rectangle { get; set; }
    }
}