using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Hacker
{
    public partial class SetConfig : Form
    {
        public SetConfig()
        {
            InitializeComponent();
        }
        
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (radLocal.Checked)
            {
                if (!File.Exists(txtInfo.Text))
                {
                    if (txtInfo.Text == "")
                    {
                        MessageBox.Show($"未修改任何设置，将使用默认地址:\n{appPath}\\html\\hacker.html", "黑客模拟器", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("指定的网页文件不存在，请检查设置。", "黑客模拟器", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
            }
            else
            {
                if (!txtInfo.Text.StartsWith("http", true, null))
                {
                    MessageBox.Show("请输入 http 或 https 开头的网址。", "黑客模拟器", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            Properties.Settings.Default.isLocal = radLocal.Checked;
            Properties.Settings.Default.uInfo = txtInfo.Text;
            Properties.Settings.Default.Opacity = optBar.Value;
            Properties.Settings.Default.Save();
            Application.Exit();
        }

        /// <summary>
        /// 程序启动目录
        /// </summary>
        public readonly string appPath = Path.GetDirectoryName(typeof(Program).Assembly.Location);

        // 保存初始值
        string selHtml = string.Empty;
        string setUrl = string.Empty;
        private void SetConfig_Load(object sender, EventArgs e)
        {
            var url = Properties.Settings.Default.uInfo;
            bool isLocal = Properties.Settings.Default.isLocal;

            optBar.Value = Properties.Settings.Default.Opacity;
            labTip.Text = isLocal?"点击下方文本框选择一个网页文件":"请在下方输入一个网址";
            radLocal.Checked= isLocal;
            radNet.Checked= !isLocal;
            txtInfo.ReadOnly = isLocal;

            //保存信息
            if (isLocal)
            {
                selHtml = url;
            }
            else
            {
                setUrl = url;
            }

            if (!string.IsNullOrWhiteSpace(url))txtInfo.Text = url;
        }

        private void radLocal_CheckedChanged(object sender, EventArgs e)
        {
            txtInfo.ReadOnly = radLocal.Checked;
            labTip.Text = radLocal.Checked ? "点击下方文本框选择一个网页文件" : "请在下方输入一个网址";
            txtInfo.Text = radLocal.Checked ? selHtml : setUrl;
        }

        private void txtInfo_Click(object sender, EventArgs e)
        {
            if (radLocal.Checked)
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.InitialDirectory= appPath;
                dialog.Multiselect = false;
                dialog.Title = "请选择文件夹";
                dialog.Filter = "网页文件(*.html)|*.html";
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    txtInfo.Text = dialog.FileName;
                }
            }
        }

    }
}
