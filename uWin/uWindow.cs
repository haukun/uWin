using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Management;

namespace uWin
{
    partial class uWindow
    {
        private const Int32 SPI_SETWORKAREA     = 0x002F;
        private const Int32 SPI_GETWORKAREA     = 0x0030;
        private const Int32 SPIF_UPDATEINIFILE  = 0x0002;
        private const Int32 SPIF_SENDCHANGE     = 0x0002;

        private const int SWP_NOSIZE            = 0x0001;
        private const int SWP_REMOVE            = 0x0002;
        private const int SWP_FRAMECHANGED      = 0x0020;
        private const int SWP_SHOWWINDOW        = 0x0040;
        private const int SWP_ASYNCWINDOWPOS    = 0x4000;


        private const int HWND_TOP          = 0;
        private const int HWND_TOPMOST      = -1;
        private const int HWND_NOTOPMOST    = -2;
        protected class NativeMethods
        {
            [DllImport("user32.dll")]
            public static extern int SendMessage(IntPtr hWnd, uint Msg, uint wParam, uint lParam);

            [DllImport("user32.dll")]
            public static extern int SetWindowPos(IntPtr hWnd, int HWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);

            [DllImport("user32.dll")]
            public static extern Boolean IsWindowVisible(IntPtr hWnd);

            [DllImport("user32.dll")]
            public static extern Boolean GetWindowRect(IntPtr hWnd, out RECT lpRect);
        }


        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public Int32 Left;
            public Int32 Top;
            public Int32 Right;
            public Int32 Bottom;
        }

        public uWindow()
        {

        }

        //  プロセス監視開始
        public Boolean ProcessStartTrace(Action<object, EventArrivedEventArgs> callback)
        {
            Boolean result = false;

            System.Management.ManagementEventWatcher w = new System.Management.ManagementEventWatcher("Select * From Win32_ProcessStartTrace");
            w.EventArrived += new System.Management.EventArrivedEventHandler(callback);
            w.Start();

            result = true;
            return result;
        }

        public Boolean IsWindowisible(IntPtr hWnd)
        {
            return NativeMethods.IsWindowVisible(hWnd);
        }

        public Boolean SetWindowPos(IntPtr hWnd, int x, int y, int width, int height)
        {
            Boolean result = false;

            NativeMethods.SetWindowPos(hWnd, HWND_TOP, x, y, width, height, SWP_FRAMECHANGED);

            result = true;
            return result;
        }
        public Boolean GetWindowSize(IntPtr hWnd, out int width, out int height)
        {
            Boolean result = false;

            RECT rect;
            NativeMethods.GetWindowRect(hWnd, out rect);

            width = rect.Right - rect.Left;
            height = rect.Bottom - rect.Top;

            result = true;
            return result;
        }

    }

}
