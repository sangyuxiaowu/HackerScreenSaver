using System;
using System.Runtime.InteropServices;

namespace Hacker
{
    public class ScreenHelper
    {
        /// <summary>
        /// 更改父窗体
        /// </summary>
        /// <param name="hWndChild"></param>
        /// <param name="hWndNewParent"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        /// <summary>
        /// 查找窗体句柄
        /// </summary>
        /// <param name="className">则指定窗口类名</param>
        /// <param name="winName">窗口名称 (窗口的标题)。 如果此参数为 NULL，则所有窗口名称都匹配。</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string className, string winName);


        /// <summary>
        /// 搜索子窗口，从指定子窗口后面的子窗口开始，不区分大小写
        /// </summary>
        /// <param name="hwndParent">要搜索其子窗口的父窗口的句柄。</param>
        /// <param name="hwndChildAfter">子窗口的句柄。 搜索其后，若为 NULL 从第一个开始</param>
        /// <param name="className">则指定窗口类名</param>
        /// <param name="winName">窗口名称 (窗口的标题)。 如果此参数为 NULL，则所有窗口名称都匹配。</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string className, string winName);

        public static void SetToProgman(IntPtr form)
        {
            var pi = FindWindow("Progman", null);
            // 没有找到 Progman 
            if (pi == IntPtr.Zero) return;
            var shell = FindWindowEx(pi, IntPtr.Zero, "SHELLDLL_DefView", null);
            if (shell == IntPtr.Zero) return;
            var list = FindWindowEx(shell, IntPtr.Zero, "SysListView32", null);

            SetParent(form, list);
        }
    }
}
