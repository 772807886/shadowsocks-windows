using Shadowsocks.Controller;
using Shadowsocks.Model;
using Shadowsocks.Properties;
using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Shadowsocks.View {
    public partial class HotkeySetting : Form {
        ShadowsocksController controller;

        public HotkeySetting(ShadowsocksController controller) {
            this.controller = controller;
            InitializeComponent();
            Icon = Icon.FromHandle(Resources.ssw128.GetHicon());
        }

        /// <summary>
        /// 初始化加载窗体
        /// </summary>
        private void HotkeySetting_Load(object sender, EventArgs e) {
            I18n();

            //Load Config
            HotkeyConfig conf = controller.GetConfigurationCopy().hotkey;
            if(conf == null) {
                conf = new HotkeyConfig();
            }
            textBox1.Text = conf.SwitchSystemProxy;
            textBox2.Text = conf.ChangeToPac;
            textBox3.Text = conf.ChangeToGlobal;
            textBox4.Text = conf.SwitchAllowLan;
            textBox5.Text = conf.ShowLogs;
            checkBox1.Checked = conf.AllowSwitchServer;
        }

        /// <summary>
        /// 窗口文本Internationalization
        /// </summary>
        private void I18n() {
            Text = I18N.GetString(Text);
            label1.Text = I18N.GetString(label1.Text);
            label2.Text = I18N.GetString(label2.Text);
            label3.Text = I18N.GetString(label3.Text);
            label4.Text = I18N.GetString(label4.Text);
            label5.Text = I18N.GetString(label5.Text);
            checkBox1.Text = I18N.GetString(checkBox1.Text);
            ok.Text = I18N.GetString(ok.Text);
            cancel.Text = I18N.GetString(cancel.Text);
        }

        private StringBuilder sb = new StringBuilder();

        /// <summary>
        /// 快捷键捕获 - 按下键盘
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">键</param>
        private void HotkeyDown(object sender, KeyEventArgs e) {
            sb.Length = 0;
            //仅允许组合键
            if(e.Modifiers != 0) {
                if(e.Control) {
                    sb.Append("Ctrl + ");
                }
                if(e.Alt) {
                    sb.Append("Alt + ");
                }
                if(e.Shift) {
                    sb.Append("Shift + ");
                }
                if((e.KeyValue >= 33 && e.KeyValue <= 40) ||
                    (e.KeyValue >= 65 && e.KeyValue <= 90) ||
                    (e.KeyValue >= 112 && e.KeyValue <= 123)) {
                    sb.Append(e.KeyCode);
                } else if(e.KeyValue >= 48 && e.KeyValue <= 57) {
                    sb.Append('D').Append((char)e.KeyValue);
                } else if(e.KeyValue >= 96 && e.KeyValue <= 105) {
                    sb.Append("NumPad").Append((char)(e.KeyValue - 48));
                }
            }
            ((TextBox)sender).Text = sb.ToString();
        }

        /// <summary>
        /// 快捷键捕获 - 松开键盘
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">键</param>
        private void HotkeyUp(object sender, KeyEventArgs e) {
            TextBox tb = sender as TextBox;
            string content = tb.Text.TrimEnd();
            if(content.Length >= 1 && content[content.Length - 1] == '+') {
                tb.Text = "";
            }
        }

        /// <summary>
        /// 取消按钮
        /// </summary>
        private void cancel_Click(object sender, EventArgs e) {
            this.Close();
        }

        /// <summary>
        /// 确定按钮
        /// </summary>
        private void ok_Click(object sender, EventArgs e) {
            //Save Config
            HotkeyConfig conf = controller.GetConfigurationCopy().hotkey;
            if(conf == null) {
                conf = new HotkeyConfig();
            }
            conf.SwitchSystemProxy = textBox1.Text;
            conf.ChangeToPac = textBox2.Text;
            conf.ChangeToGlobal = textBox3.Text;
            conf.SwitchAllowLan = textBox4.Text;
            conf.ShowLogs = textBox5.Text;
            conf.AllowSwitchServer = checkBox1.Checked;
            controller.SaveHotkeyConfig(conf);

            this.Close();
        }
    }
}
