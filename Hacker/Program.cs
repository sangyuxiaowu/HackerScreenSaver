using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hacker
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (args.Length > 0)
            {
                switch (args[0].ToLower().Trim().Substring(0, 2))
                {
                    case "/p":
                        Main pri = new Main(new IntPtr(long.Parse(args[1])));
                        pri.ShowDialog();
                        break;
                    case "/s"://Test 或 执行屏保，大写为测试，小写为被启动，这里不做区分
                        Application.Run(new Main());
                        break;
                    default:
                        ShowSetting();
                        break;
                }
            }
            else
            {
                ShowSetting();
            }
        }

        static void ShowSetting()
        {
            //MessageBox.Show("这个屏幕保护程序没有可以设置的选项。","黑客模拟器",MessageBoxButtons.OK,MessageBoxIcon.Information);
            Application.Run(new SetConfig());
            //Application.Exit();
        }
    }
}
