using System;
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
        class setup
        {
            public static void main(UserControl uc)
            {
                uc.Padding = new Padding(20, 20, 20, 20);
            }
        }
        class startup
        {
            public static void main() { }
        }
        class cleaning
        {
            public static void main() { }
        }
        class load
        {
            public static void main(UserControl uc)
            {
                for (int i = 0; i < conSetting.names.Length; i++)
                {
                    Label la = new Label();
                    ComboBox cb = new ComboBox();
                    funCom.addLabel(la, 5, uc);
                    funCom.addCombobox(cb, 5, i, uc,new int[] {180,10});
                    la.Text = conSetting.names[i];
                    la.Font = new Font("Yu mincho", 10, FontStyle.Regular);
                    cb.Font = new Font("Yu mincho", 10, FontStyle.Regular);
                    la.Location = new Point(70 + 400 * (i % 3), 90 + 90 * (i / 3));
                    cb.Location = new Point(190 + 400 * (i % 3), 90 + 90 * (i / 3));
                    funCom.addComboboxItem(cb, conSetting.keys, conSetting.values[i]);
                    cb.SelectedIndex = int.Parse(string.Format("{0}",conSetting.startupSettingCodes[i]));
                }
            }
        }
        private void Setting_Load(object sender, EventArgs e)
        {
            setup.main(this);
            startup.main();
            cleaning.main();
            load.main(this);
        }
    }
}
