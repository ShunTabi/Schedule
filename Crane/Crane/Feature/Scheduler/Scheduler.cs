using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace Crane
{
    public partial class Scheduler : UserControl
    {
        public Scheduler()
        {
            InitializeComponent();
        }
        //定義
        public static Panel pa1 = new Panel();
        public static Panel pa2 = new Panel();
        public static ScheduleDaily scheduleDaily = new ScheduleDaily();
        public static ScheduleWeekly scheduleWeekly = new ScheduleWeekly();
        public static ScheduleList scheduleList = new ScheduleList();
        public static UserControl[] userControls = new UserControl[] { scheduleDaily, scheduleWeekly, scheduleList };
        class LocalSetup
        {
            private static void common1(UserControl uc)
            {
                FunCom.AddPanel(pa2, 0, uc, new int[] { 0, 0 });
                FunCom.AddPanel(pa1, 1, uc, new int[] { 0, 100 });
                string[] btnNames = new string[] { "日間","週間","一覧" };
                for (int i = 0; i < userControls.Length; i++)
                {
                    Button btn = new Button();
                    FunCom.AddButton(btn, 5, i, pa1, new int[] { 90, 50 });
                    btn.Text = btnNames[i];
                    btn.Location = new Point(10 + i * 95, 10);
                    btn.BackColor = Color.MediumOrchid;
                    btn.Click += (sender, e) => {
                        for (int j = 0; j < userControls.Length; j++)
                        {
                            uc = userControls[j];
                            uc.Visible = false;
                        }
                        userControls[btn.TabIndex].Visible = true;
                    };
                }
            }
            private static void common2()
            {
                for (int i = 0; i < userControls.Length; i++)
                {
                    UserControl uc = userControls[i];
                    FunCom.AddUserControl(uc, 0, pa2);
                    uc.Visible = false;
                }
                userControls[ConMain.scheduleStartupCode].Visible = true;
            }
            public static void LocalMain(UserControl uc)
            {
                common1(uc);
                common2();
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

        private void Scheduler_VisibleChanged(object sender, EventArgs e)
        {
            int loadStatus = ConInstance.scheduleFirstLoad;
            if (loadStatus == 1)
            {
                ConInstance.scheduleFirstLoad = 2;
                LocalSetup.LocalMain(this);
                //LocalStartup.LocalMain();
            }
            else if (loadStatus == 2)
            {
                return;
            }
            else if (loadStatus == 0)
            {
                ConInstance.scheduleFirstLoad = 1;
            }
        }
    }
}
