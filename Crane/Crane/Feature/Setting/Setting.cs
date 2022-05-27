﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crane
{
    public partial class Setting : UserControl
    {
        public Setting()
        {
            InitializeComponent();
        }
        //定義
        public static ComboBox[] cbs = new ComboBox[] { };
        class setup
        {
            public static void main(UserControl uc)
            {
                uc.VisibleChanged += (sender, e) => { if (uc.Visible == false) { return; } else { /*cleaning.main();*/ load.main(); } };
                uc.Padding = new Padding(20, 20, 20, 20);
                for (int i = 0; i < ConSetting.names.Length; i++)
                {
                    Label la = new Label();
                    FunCom.AddLabel(la, 5, uc);
                    la.Text = ConSetting.names[i];
                    la.Font = new Font("Yu mincho", 10, FontStyle.Regular);
                    ComboBox cb = new ComboBox();
                    Array.Resize(ref cbs, cbs.Length + 1);
                    cbs[cbs.Length - 1] = cb;
                    FunCom.AddCombobox(cb, 5, i, uc, new int[] { 180, 10 });
                    cb.Font = new Font("Yu mincho", 10, FontStyle.Regular);
                    la.Location = new Point(70 + 400 * (i % 3), 90 + 90 * (i / 3));
                    cb.Location = new Point(190 + 400 * (i % 3), 90 + 90 * (i / 3));
                    FunCom.AddComboboxItem(cb, ConSetting.keys, ConSetting.values[i]);
                }
            }
        }
        //class startup
        //{
        //    public static void main() { }
        //}
        //class cleaning
        //{
        //    public static void main() { }
        //}
        class load
        {
            public static void main()
            {
                for(int i = 0; i < cbs.Length; i++)
                {
                    cbs[i].SelectedIndex = int.Parse(string.Format("{0}", ConSetting.startupSettingCodes[i]));
                }
                for (int i = 0; i < cbs.Length; i++)
                {
                    cbs[i].SelectedIndexChanged += (sender, e) =>
                    {
                        string[] nowSettingValues = new string[] { };
                        for (int j = 0; j < cbs.Length; j++)
                        {
                            Array.Resize(ref nowSettingValues, nowSettingValues.Length + 1);
                            nowSettingValues[nowSettingValues.Length - 1] = cbs[j].SelectedValue.ToString();
                        }
                        ConSetting.startupSettingCodes = nowSettingValues;
                    };
                }
            }
        }
        private void Setting_VisibleChanged(object sender, EventArgs e)
        {
            int loadStatus = ConInstance.settingFirstLoad;
            if (loadStatus == 1)
            {
                ConInstance.settingFirstLoad = 2;
                setup.main(this);
                //startup.main();
            }
            else if (loadStatus == 2)
            {
                return;
            }
            else if (loadStatus == 0)
            {
                ConInstance.settingFirstLoad = 1;
            }
        }
    }
}
