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
        public static TextBox tb1 = new TextBox();
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
            private static void ReadyBotton()
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
                        ConSchedule.execCode = 0;
                        ConSchedule.ID = "0";
                        ConInstance.scheduleForm.Visible = true;
                        ConInstance.schedule.Enabled = false;
                    },
                    (sender, e) =>
                    {//修正
                        if(dg.SelectedRows.Count == 0){ FunMSG.ErrMsg(ConMSG.message00010); return; }
                        ConSchedule.execCode = 1;
                        ConSchedule.ID = dg.SelectedRows[0].Cells[0].Value.ToString();
                        ConInstance.scheduleForm.Visible = true;
                        ConInstance.schedule.Enabled = false;
                    },
                    (sender, e) =>
                    {//削除
                        if(dg.SelectedRows.Count == 0){ FunMSG.ErrMsg(ConMSG.message00010); return; }
                        DialogResult result = MessageBox.Show(ConMSG.message00100,"確認",MessageBoxButtons.OKCancel,MessageBoxIcon.Exclamation,MessageBoxDefaultButton.Button2);
                        if(result == DialogResult.OK)
                        {
                            string ID = dg.SelectedRows[0].Cells[0].Value.ToString();
                            FunSQL.SQLDML("SQL0421", ConSQL.ScheduleSQL.SQL0421, new string[] { "@VISIBLESTATUS", "@SCHEDULEID" }, new string[] { "1",ID });
                            LocalLoad.LocalMain();
                        }
                    }
                });
                FunCom.AddDataGridViewColumns(dg, new string[] { "ID", "目標", "計画/作業", " 進捗","日付", "開始時間", "終了時間" });
            }
            public static void ReadyTextbox()
            {
                Label lb1 = new Label();
                FunCom.AddLabel(lb1, 5, pa1);
                lb1.Text = "目標/計画";
                lb1.Location = new Point(357, 30);
                lb1.Font = new Font("Yu mincho", 10, FontStyle.Regular);
                FunCom.AddTextbox(tb1, 5, 1, pa1, new int[] { 180, 10 });
                tb1.Location = new Point(458, 30);
                tb1.TextChanged += (sender, e) => { LocalLoad.LocalMain(); };
            }
            public static void LocalMain(UserControl uc)
            {
                Common(uc);
                ReadyBotton();
                ReadyTextbox();
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
                StringBuilder sb1 = new StringBuilder("%");
                sb1.Append(tb1.Text);
                sb1.Append("%");
                SQLiteDataReader reader = FunSQL.SQLSELECT("SQL0402", ConSQL.ScheduleSQL.SQL0402, new string[] { "@KEYWORD" }, new string[] { sb1.ToString() });
                dg.Rows.Clear();
                while (reader.Read())
                {
                    StringBuilder sb2 = new StringBuilder();
                    sb2.Append((string)reader["PLANNAME"]);
                    sb2.Append(":");
                    sb2.Append((string)reader["WORKNAME"]);
                    dg.Rows.Add(
                        ((Int64)reader["SCHEDULEID"]).ToString(),
                        (string)reader["GOALNAME"],
                        sb2.ToString(),
                        (string)reader["STATUSNAME"],
                        ((DateTime)reader["SCHEDULEDATE"]).ToString("yyyy-MM-dd"),
                        ((DateTime)reader["SCHEDULESTARTTIME"]).ToString("HH:mm"),
                        ((DateTime)reader["SCHEDULEENDTIME"]).ToString("HH:mm")
                        );
                    if ((DateTime)reader["SCHEDULEDATE"] > (DateTime)reader["WORKENDDATE"])
                    {
                        int count = dg.Rows.Count - 1;
                        dg.Rows[count].DefaultCellStyle.ForeColor = Color.Red;
                        dg.Rows[count].DefaultCellStyle.BackColor = Color.Gainsboro;
                    }
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
