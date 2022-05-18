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
    public partial class Archive : UserControl
    {
        public Archive()
        {
            InitializeComponent();
        }
        //定義
        public static Panel pa1 = new Panel();
        public static Panel pa2 = new Panel();
        public static Genre genre = new Genre();
        public static Goal goal = new Goal();
        public static Plan plan = new Plan();
        public static Work work = new Work();
        public static UserControl[] userControls = new UserControl[] { genre, goal, plan, work };
        class setup
        {
            private static void main1(UserControl uc)
            {
                uc.Padding = new Padding(20, 20, 20, 20);
                funCom.addPanel(pa2, 0, uc, new int[] { 0, 0 });
                funCom.addPanel(pa1, 1, uc, new int[] { 0, 100 });
                string[] btnNames = new string[] { "種別","目標","計画","作業" };
                for ( int i = 0;i< userControls.Length; i++)
                {
                    Button btn = new Button();
                    funCom.addButton(btn, 5, i, pa1, new int[] { 90,50 });
                    btn.Text = btnNames[i];
                    btn.Location = new Point(10 + i* 95, 10);
                    btn.BackColor = Color.MediumOrchid; ;
                    btn.Click += (sender, e) => clickBtn(sender, btn.TabIndex);
                }
            }
            private static void main2()
            {
                for (int i = 0; i < userControls.Length; i++)
                {
                    UserControl uc = userControls[i];
                    funCom.addUserControl(uc, 0, pa2);
                    uc.Visible = false;
                }
                userControls[conMain.archiveStartupCode].Visible = true;
                //genre.BackColor = Color.Red;
                //goal.BackColor = Color.Green;
                //plan.BackColor = Color.Yellow;
                //work.BackColor = Color.Blue;
            }
            public static void main(UserControl uc)
            {
                main1(uc);
                main2();
            }
            private static void clickBtn(object sender, int code)
            {
                for (int i = 0; i < userControls.Length; i++)
                {
                    UserControl uc = userControls[i];
                    uc.Visible = false;
                }
                userControls[code].Visible = true;
            }
        }
        class startup
        {
            public static void main()
            {

            }
        }
        class cleaning
        {
            public static void main()
            {

            }
        }
        class load
        {
            public static void main()
            {
            }
        }
        private void Archive_Load(object sender, EventArgs e)
        {
            setup.main(this);
            startup.main();
            cleaning.main();
            load.main();
        }
    }
}
