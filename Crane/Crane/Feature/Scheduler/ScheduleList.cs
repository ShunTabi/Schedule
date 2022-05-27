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
    public partial class ScheduleList : UserControl
    {
        public ScheduleList()
        {
            InitializeComponent();
        }
        //定義
        public static int execCode = 0;
        public static string ID = "0";
        public static Panel pa1 = new Panel();
        public static Panel pa2 = new Panel();
        public static Form schedulerForm = new SchedulerForm();
        public static DataGridView dg = new DataGridView();
        class LocalSetup
        {
            private static void Common(UserControl uc)
            {
                uc.EnabledChanged += (sender, e) => { if (uc.Enabled == false) { return; } else { LocalLoad.LocalMain(); } };
                FunCom.AddPanel(pa2, 0, uc, new int[] { 0, 0 });
                FunCom.AddPanel(pa1, 1, uc, new int[] { 0, 70 });
                pa1.BackColor = Color.Plum;
                pa1.Padding = new Padding(10, 10, 10, 10);
                pa2.BackColor = Color.Plum;
                pa2.Padding = new Padding(10, 10, 10, 10);
            }
            private static void Bottom()
            {
                Label l1 = new Label();
                FunCom.AddLabel(l1, 5, pa1);
                l1.Location = new Point(0, 10);
                l1.Text = "LIST";
                l1.Font = new Font("Segoe Print", 20, FontStyle.Regular);
                FunCom.AddDataGridView(dg, 0, pa2, new int[] { 0, 0 });
                dg.BackgroundColor = Color.Plum;
                FunCom.AddContextMenuStrip(dg, ConCom.defaultBtnNames, new EventHandler[]
                {
                    (sender, e) =>
                    {//新規
                        ConScheduler.execCode = 0;
                        ConScheduler.ID = "0";
                        schedulerForm.Visible = true;
                        ConInstance.scheduler.Enabled = false;
                    },
                    (sender, e) =>
                    {//修正
                        ConScheduler.execCode = 1;
                        ConScheduler.ID = dg.SelectedRows[0].Cells[0].Value.ToString();
                        schedulerForm.Visible = true;
                        ConInstance.scheduler.Enabled = false;
                    },
                    (sender, e) =>
                    {//削除
                        string ID = dg.SelectedRows[0].Cells[0].Value.ToString();
                        FunSQL.SQLDML("SQL0421", ConSQL.ScheduleSQL.SQL0421, new string[] { "@VISIBLESTATUS", "@SCHEDULEID" }, new string[] { "1",ID });
                        LocalLoad.LocalMain();
                    }
                });
                FunCom.AddDataGridViewColumns(dg, new string[] { "ID", "目標", "計画/作業", " 進捗","日付", "開始時間", "終了時間" });
            }
            public static void LocalMain(UserControl uc)
            {
                Common(uc);
                Bottom();
            }
        }
        class LocalStartup
        {
            public static void LocalMain()
            {
            }
        }
        class LocalCleaning
        {
        }
        class LocalLoad
        {
            public static void DataLocalload()
            {
                SQLiteDataReader reader = FunSQL.SQLSELECT("SQL0402", ConSQL.ScheduleSQL.SQL0402, new string[] { }, new string[] { });
                dg.Rows.Clear();
                while (reader.Read())
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append((string)reader["PLANNAME"]);
                    sb.Append(":");
                    sb.Append((string)reader["WORKNAME"]);
                    dg.Rows.Add(
                        ((Int64)reader["SCHEDULEID"]).ToString(),
                        (string)reader["GOALNAME"],
                        sb.ToString(),
                        (string)reader["STATUSNAME"],
                        ((DateTime)reader["SCHEDULEDATE"]).ToString("yyyy-MM-dd"),
                        ((DateTime)reader["SCHEDULESTARTTIME"]).ToString("HH:mm"),
                        ((DateTime)reader["SCHEDULEENDTIME"]).ToString("HH:mm")
                        );
                }
            }
            public static void LocalMain()
            {
                DataLocalload();
            }
        }

        private void ScheduleList_VisibleChanged(object sender, EventArgs e)
        {
            int loadStatus = ConInstance.scheduleListFirstLoad;
            if (loadStatus == 1)
            {
                ConInstance.scheduleFirstLoad = 2;
                LocalSetup.LocalMain(this);
                LocalLoad.LocalMain();
            }
            else if (loadStatus == 2)
            {
                if (Visible == false) { return; }
                else
                {
                    LocalLoad.LocalMain();
                }
            }
            else if(loadStatus == 0)
            {
                ConInstance.scheduleListFirstLoad = 1;
            }
        }
    }
}
