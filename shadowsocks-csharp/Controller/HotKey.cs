using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Shadowsocks.Controller {
    public class HotKey : Form {
        /// <summary>
        /// The Form to receive hotkey message
        /// </summary>
        private static HotKey form;
        /// <summary>
        /// Hotkey id
        /// </summary>
        private static int id = 1;
        /// <summary>
        /// Registered hotkey
        /// </summary>
        private static List<Hotkeys> hotkeys = new List<Hotkeys>();

        /// <summary>
        /// Hotkey info
        /// </summary>
        struct Hotkeys {
            public int id;
            public string name;
            public EventHandler e;
        }

        /// <summary>
        /// Hotkey Event Arguments
        /// </summary>
        public class HotKeyEventArgs : EventArgs {
            private string name;
            public HotKeyEventArgs(string name) {
                this.name = name;
            }
            public string Message {
                get {
                    return name;
                }
            }
        }

        /// <summary>
        /// Initialize
        /// </summary>
        public static void initialize() {
            form = new HotKey();
            form.Text = "Hotkey";
        }

        public static void final() {
            unregisterAll();
            form.Close();
        }

        /// <summary>
        /// Register hotkey
        /// </summary>
        /// <param name="name">name of hotkey to register</param>
        /// <param name="modifier">modifier</param>
        /// <param name="key">main key</param>
        /// <param name="handler">callback</param>
        public static bool register(string name, NativeMethods.KeyModifiers modifier, Keys key, EventHandler handler) {
            if(NativeMethods.RegisterHotKey(form.Handle, id, modifier, key)) {
                hotkeys.Add(new Hotkeys { id = id++, name = name, e = handler });
                return true;
            }
            return false;
        }

        /// <summary>
        /// Unregister hotkey
        /// </summary>
        /// <param name="name">name of hotkey to unregister</param>
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
        /// Unregister all of hotkeys that had registered
        /// </summary>
        public static void unregisterAll() {
            while(hotkeys.Count > 0) {
                unregister(hotkeys[0].name);
            }
        }

        /// <summary>
        /// Message Process
        /// </summary>
        /// <param name="m">Windows Message</param>
        protected override void WndProc(ref Message m) {
            switch(m.Msg) {
            case 0x0312:
                int id = m.WParam.ToInt32();
                foreach(Hotkeys hk in hotkeys) {
                    if(hk.id == id) {
                        hk.e.Invoke(form, new HotKeyEventArgs(hk.name));
                    }
                }
                break;
            }
            base.WndProc(ref m);
        }
    }
}
