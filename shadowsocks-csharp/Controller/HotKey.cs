using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Shadowsocks.Controller {
    public class HotKey : Form {
        /// <summary>
        /// 接受热键的窗体
        /// </summary>
        private static HotKey form;
        /// <summary>
        /// 热键ID
        /// </summary>
        private static int id = 1;
        /// <summary>
        /// 已注册的热键
        /// </summary>
        private static List<Hotkeys> hotkeys = new List<Hotkeys>();

        /// <summary>
        /// 热键信息
        /// </summary>
        struct Hotkeys {
            public int id;
            public string name;
            public EventHandler e;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public static void initialize() {
            form = new HotKey();
            form.Text = "Hotkey";
        }

        public static void final() {
            while(hotkeys.Count > 0) {
                unregister(hotkeys[0].name);
            }
            form.Close();
        }

        /// <summary>
        /// 注册热键
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="modifier">组合键</param>
        /// <param name="key">热键</param>
        /// <param name="handler">处理函数</param>
        public static bool register(string name, NativeMethods.KeyModifiers modifier, Keys key, EventHandler handler) {
            if(NativeMethods.RegisterHotKey(form.Handle, id, modifier, key)) {
                hotkeys.Add(new Hotkeys { id = id++, name = name, e = handler });
                return true;
            }
            return false;
        }

        /// <summary>
        /// 注销热键
        /// </summary>
        /// <param name="name">名称</param>
        public static bool unregister(string name) {
            for(int i = 0; i < hotkeys.Count; i++) {
                if(hotkeys[i].name == name) {
                    if(NativeMethods.UnregisterHotKey(form.Handle, hotkeys[i].id)) {
                        hotkeys.RemoveAt(i);
                        return true;
                    }
                    return false;
                }
            }
            return false;
        }

        /// <summary>
        /// 消息处理函数
        /// </summary>
        /// <param name="m">消息</param>
        protected override void WndProc(ref Message m) {
            switch(m.Msg) {
            case 0x0312:
                int id = m.WParam.ToInt32();
                foreach(Hotkeys hk in hotkeys) {
                    if(hk.id == id) {
                        hk.e.Invoke(form, null);
                    }
                }
                break;
            }
            base.WndProc(ref m);
        }
    }
}
