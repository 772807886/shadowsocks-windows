using Shadowsocks.Controller;
using System;
using System.Text;
using System.Windows.Forms;

namespace Shadowsocks.View {
    public partial class HotkeySetting : Form {
        public HotkeySetting() {
            InitializeComponent();
        }

        /// <summary>
        /// 初始化加载窗体
        /// </summary>
        private void HotkeySetting_Load(object sender, EventArgs e) {
            I18n();
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
                    sb.Append(e.KeyValue);
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

        private void cancel_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void ok_Click(object sender, EventArgs e) {
        }
    }
}
