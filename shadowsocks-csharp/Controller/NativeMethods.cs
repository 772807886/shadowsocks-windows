using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Shadowsocks.Controller {
    public static class NativeMethods {
        /// <summary>
        /// Register hotkey
        /// </summary>
        /// <param name="hWnd">Handle</param>
        /// <param name="id">Hotkey ID, make sure it is the only</param>
        /// <param name="fsModifiers">Identify whether hotkeys only when you press Alt, Ctrl, Shift, Windows key take effect</param>
        /// <param name="vk">Main key</param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, KeyModifiers fsModifiers, Keys vk);

        /// <summary>
        /// Unregister hotkey
        /// </summary>
        /// <param name="hWnd">Handle</param>
        /// <param name="id">Hotkey ID</param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        [Flags]
        public enum KeyModifiers {
            None = 0,
            Alt = 1,
            Ctrl = 2,
            Shift = 4,
            WindowsKey = 8
        }
    }
}
