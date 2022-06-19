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
    public partial class Schedule : UserControl
    {
        public Schedule()
        {
            InitializeComponent();
        }
        //定義
        public static Panel pa1 = new Panel();
        public static Panel pa2 = new Panel();
        public static Button[] btns = new Button[] { };
        class LocalSetup
        {
            private static void common1(UserControl uc)
            {
                FunCom.AddPanel(pa2, 0, uc, new int[] { 0, 0 });
                FunCom.AddPanel(pa1, 1, uc, new int[] { 0, 100 });
                string[] btnNames = new string[] { "日間","週間","一覧" };
                ConInstance.schedulerDaily = new ScheduleDaily();
                ConInstance.schedulerWeekly = new ScheduleWeekly();
                ConInstance.schedulerList = new ScheduleList();
                ConInstance.scheduleForm = new ScheduleForm();
                UserControl[] userControls = { ConInstance.schedulerDaily, ConInstance.schedulerWeekly, ConInstance.schedulerList };
                for (int i = 0; i < userControls.Length; i++)
                {
                    Button btn = new Button();
                    Array.Resize(ref btns, btns.Length + 1);
                    btns[btns.Length - 1] = btn;
                    FunCom.AddButton(btn, 5, i, pa1, new int[] { 90, 50 });
                    btn.Text = btnNames[i];
                    btn.Location = new Point(10 + i * 95, 10);
                    btn.BackColor = ConColor.subButtonColor;
                    btn.Click += (sender, e) => {
                        for (int j = 0; j < userControls.Length; j++)
                        {
                            btns[j].BackColor = ConColor.subButtonColor;
                            uc = userControls[j];
                            uc.Visible = false;
                        }
                        btns[btn.TabIndex].BackColor = ConColor.subButtonColorPushed;
                        userControls[btn.TabIndex].Visible = true;
                    };
                }
            }
            private static void common2()
            {
                UserControl[] userControls = { ConInstance.schedulerDaily, ConInstance.schedulerWeekly, ConInstance.schedulerList };
                for (int i = 0; i < userControls.Length; i++)
                {
                    UserControl uc = userControls[i];
                    FunCom.AddUserControl(uc, 0, pa2);
                    uc.Visible = false;
                }
                btns[ConMain.recordStartupCode].BackColor = ConColor.subButtonColorPushed;
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
