using Gma.System.MouseKeyHook;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Hacker
{
    public partial class Main : Form
    {

        /// <summary>
        /// 是否是预览模式
        /// </summary>
        private bool isPreviewMode = false;

        /// <summary>
        /// 全局鼠标键盘事件管理
        /// </summary>
        private IKeyboardMouseEvents m_GlobalHook;
        public Main()
        {
            InitializeComponent();

            // 鼠标键盘事件用于结束屏保
            m_GlobalHook = Hook.GlobalEvents();
            m_GlobalHook.MouseClick += M_GlobalHook_MouseClick;
            m_GlobalHook.KeyPress += M_GlobalHook_KeyPress;
            m_GlobalHook.MouseMove += M_GlobalHook_MouseMove;
        }

        public Main(IntPtr intPtr)
        {
            InitializeComponent();
            //预览
            ScreenHelper.SetParent(this.Handle, intPtr);
            isPreviewMode = true;
            picLogo.Visible = true;
        }

        /// <summary>
        /// 程序启动目录
        /// </summary>
        public readonly string appPath = Path.GetDirectoryName(typeof(Program).Assembly.Location);


        private void Main_Load(object sender, EventArgs e)
        {
            // 隐藏鼠标
            Cursor.Hide();
            // 不透明度生效
            if (Properties.Settings.Default.Opacity > 9 && Properties.Settings.Default.Opacity < 100) {
                this.Opacity = (double)Properties.Settings.Default.Opacity / 100;
            }

            var url = Properties.Settings.Default.uInfo;
            bool isLocal = Properties.Settings.Default.isLocal;
            // 未设置或未空，则默认
            if (string.IsNullOrWhiteSpace(url))
            {
                webB.Navigate(appPath + "\\html\\hacker.html");
            }
            else
            {
                if (isLocal)
                {
                    webB.Navigate(url);
                }
                else
                {
                    webB.Url = new Uri(url);
                }
            }
        }

        private void M_GlobalHook_KeyPress(object sender, KeyPressEventArgs e)
        {
            Application.Exit();
        }

        private void M_GlobalHook_MouseClick(object sender, MouseEventArgs e)
        {
            Application.Exit();
        }


        private bool isActive = false;
        private Point mouseLocation;
        /// <summary>
        /// 鼠标动了一定程度，就结束屏保
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void M_GlobalHook_MouseMove(object sender, MouseEventArgs e)
        {
            if (isPreviewMode) return;
            if (!isActive)
            {
                isActive = true;
                mouseLocation = new Point(e.X, e.Y);
                return;
            }

            if ((Math.Abs(e.X - mouseLocation.X) > 10) ||
                (Math.Abs(e.Y - mouseLocation.Y) > 10))
            {
                Application.Exit();
            }
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!isPreviewMode)
            {
                m_GlobalHook.MouseClick -= M_GlobalHook_MouseClick;
                m_GlobalHook.KeyPress -= M_GlobalHook_KeyPress;
                m_GlobalHook.MouseMove -= M_GlobalHook_MouseMove;
                m_GlobalHook.Dispose();
            }
        }

        private void picLogo_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
