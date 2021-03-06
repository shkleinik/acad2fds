using System;
using System.Runtime.InteropServices;

namespace Fds2AcadPlugin.BLL.NativeMethods
{
    public class NativeMethods
    {
        public const uint SW_SHOWMAXIMIZED = 3;

        [DllImport("user32.dll")]
        public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetParent(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr FindWindow(string className, string windowName);

        [DllImport("user32")]
        public static extern int ShowWindow(IntPtr hwnd, uint nCmdShow);
    }
}