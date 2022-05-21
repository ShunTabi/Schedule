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
    public partial class Record : UserControl
    {
        public Record()
        {
            InitializeComponent();
        }
        //定義
        public static Panel pa1 = new Panel();
        public static Panel pa2 = new Panel();
        public static Genre genre = conInstance.genre;
        public static Goal goal = conInstance.goal;
        public static Plan plan = conInstance.plan;
        public static Work work = conInstance.work;
        public static UserControl[] userControls = new UserControl[] { genre, goal, plan, work };
        class setup
        {
            private static void common1(UserControl uc)
            {
                //uc.VisibleChanged += (sender, e) => { if (uc.Visible == false) { return; } else { cleaning.main(); load.main(); } };
                funCom.addPanel(pa2, 0, uc, new int[] { 0, 0 });
                funCom.addPanel(pa1, 1, uc, new int[] { 0, 100 });
                string[] btnNames = new string[] { "種別","目標","計画","作業" };
                for ( int i = 0;i< userControls.Length; i++)
                {
                    Button btn = new Button();
                    funCom.addButton(btn, 5, i, pa1, new int[] { 90,50 });
                    btn.Text = btnNames[i];
                    btn.Location = new Point(10 + i* 95, 10);
                    btn.BackColor = Color.MediumOrchid;
                    btn.Click += (sender, e) => clickBtn(sender, btn.TabIndex);
                }
            }
            private static void common2()
            {
                for (int i = 0; i < userControls.Length; i++)
                {
                    UserControl uc = userControls[i];
                    funCom.addUserControl(uc, 0, pa2);
                    uc.Visible = false;
                }
                userControls[conMain.archiveStartupCode].Visible = true;
            }
            public static void main(UserControl uc)
            {
                common1(uc);
                common2();
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
        //class startup
        //{
        //    public static void main()
        //    {

        //    }
        //}
        //class cleaning
        //{
        //    public static void main()
        //    {

        //    }
        //}
        //class load
        //{
        //    public static void main()
        //    {
        //    }
        //}
        private void Record_Load(object sender, EventArgs e)
        {
            setup.main(this);
            //startup.main();
        }
    }
}
