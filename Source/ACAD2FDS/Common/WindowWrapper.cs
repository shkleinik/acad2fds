namespace Common
{
    using System;
    using System.Windows.Forms;

    /// <summary>
    /// Represents a wrapper for window's handle.
    /// </summary>
    public class WindowWrapper : IWin32Window
    {
        /// <summary>
        /// Initializes a new instance of <see cref="WindowWrapper"/> class.
        /// </summary>
        /// <param name="handle">Handle of window to wrap.</param>
        public WindowWrapper(IntPtr handle)
        {
            Handle = handle;
        }

        public IntPtr Handle { get; set; }
    }
}