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
using System.Text.RegularExpressions;

namespace Crane
{
    public partial class ScheduleDaily : UserControl
    {
        public ScheduleDaily()
        {
            InitializeComponent();
        }
        //定義
        public static int execCode = 0;
        public static string ID = "0";
        public static Form scheduleForm = new ScheduleForm();
        public static Panel pa1 = new Panel();
        public static Panel pa2 = new Panel();
        public static Panel pa3 = new Panel();
        public static Panel pa4 = new Panel();
        public static Panel pa5 = new Panel();
        public static Panel pa6 = new Panel();
        public static Panel[] ps = new Panel[] { };
        public static TextBox tb1 = new TextBox();
        public static DataGridView dg = new DataGridView();
        class LocalSetup
        {
            private static void Common(UserControl uc)
            {
                uc.EnabledChanged += (sender, e) => { if (uc.Enabled == false) { return; } else {  LocalLoad.LocalMain(); } };
                scheduleForm.Visible = false;
                FunCom.AddPanel(pa2, 0, uc, new int[] { 0, 0 });
                FunCom.AddPanel(pa1, 2, uc, new int[] { 350, 0 });
                pa2.BackColor = Color.Plum;
                pa2.Padding = new Padding(10, 10, 10, 10);
            }
            private static void LeftSide()
            {
                FunCom.AddPanel(pa4, 0, pa1, new int[] { 0, 0 });
                FunCom.AddPanel(pa3, 1, pa1, new int[] { 0, 70 });
                pa4.AutoScroll = true;
                Label l1 = new Label();
                FunCom.AddLabel(l1, 5, pa3);
                l1.Location = new Point(15, 15);
                l1.Font = new Font("Yu mincho", 10, FontStyle.Regular);
                l1.Text = "日付";
                FunCom.AddTextbox(tb1, 5, 0, pa3, new int[] { 160, 10 });
                tb1.Location = new Point(100, 15);
                tb1.Text = FunDate.getToday(0, 0);
                tb1.TextChanged += (sender, e) =>
                {
                    LocalLoad.LocalMain();
                };
            }
            private static void RightSide()
            {
                FunCom.AddDataGridView(dg, 0, pa2, new int[] { 0, 0 });
                dg.BackgroundColor = Color.Plum;
                FunCom.AddContextMenuStrip(dg, ConCom.defaultBtnNames, new EventHandler[]
                {
                    (sender, e) =>
                    {//新規
                        ConSchedule.execCode = 0;
                        ConSchedule.ID = "0";
                        scheduleForm.Visible = true;
                        ConInstance.schedule.Enabled = false;
                    },
                    (sender, e) =>
                    {//修正
                        if(dg.SelectedRows.Count == 0){ FunMSG.ErrMsg(ConMSG.message00010); return; }
                        ConSchedule.execCode = 1;
                        ConSchedule.ID = dg.SelectedRows[0].Cells[0].Value.ToString();
                        scheduleForm.Visible = true;
                        ConInstance.schedule.Enabled = false;
                    },
                    (sender, e) =>
                    {//削除
                        if(dg.SelectedRows.Count == 0){ FunMSG.ErrMsg(ConMSG.message00010); return; }
                        DialogResult result = MessageBox.Show(ConMSG.message00100,"確認",MessageBoxButtons.OKCancel,MessageBoxIcon.Exclamation,MessageBoxDefaultButton.Button2);
                        if(result == DialogResult.OK)
                        {
                            ID = dg.SelectedRows[0].Cells[0].Value.ToString();
                            FunSQL.SQLDML("SQL0421", ConSQL.ScheduleSQL.SQL0421, new string[] { "@VISIBLESTATUS","@SCHEDULEID" }, new string[] { "1",ID });
                            LocalLoad.LocalMain();
                        }
                    }
                });
                FunCom.AddDataGridViewColumns(dg, new string[] { "ID", "目標", "計画/作業", " 進捗", "開始時間", "終了時間" });
                FunCom.AddPanel(pa5, 1, pa2, new int[] { 0, 70 });
                FunCom.AddPanel(pa6, 4, pa2, new int[] { 0, 70 });
                Label l2 = new Label();
                FunCom.AddLabel(l2, 5, pa5);
                l2.Location = new Point(0, 0);
                l2.Text = "TODAY";
                l2.Font = new Font("Segoe Print", 20, FontStyle.Regular);
                pa6.BackColor = Color.Plum;
                Button btn1 = new Button();
                FunCom.AddButton(btn1, 5, 1, pa6, new int[] { 90, 50 });
                btn1.Location = new Point(15, 5);
                btn1.Text = ConCom.defaultBtnNames[0];
                btn1.BackColor = Color.MediumOrchid;
                btn1.Click += (sender, e) =>
                {
                    ConSchedule.execCode = 0;
                    ConSchedule.ID = "0";
                    scheduleForm.Visible = true;
                    ConInstance.schedule.Enabled = false;
                };
            }
            public static void LocalMain(UserControl uc)
            {
                Common(uc);
                LeftSide();
                RightSide();
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
            private static SQLiteDataReader SQL0400()
            {
                SQLiteDataReader reader = FunSQL.SQLSELECT("SQL0400", ConSQL.ScheduleSQL.SQL0400, new string[] { "@SCHEDULEDATE" }, new string[] { tb1.Text });
                ConSchedule.selectedDate = tb1.Text;
                return reader;
            }
            public static void DataLocalload()
            {
                SQLiteDataReader reader = SQL0400();
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
            private static void LbLoadload()
            {
                for (int i = pa4.Controls.Count - 1; i >= 0; i--)
                {
                    pa4.Controls[i].Dispose();
                }
                SQLiteDataReader reader = SQL0400();
                while (reader.Read())
                {
                    Panel p = new Panel();
                    Array.Resize(ref ps, ps.Length + 1);
                    ps[ps.Length - 1] = p;
                    FunCom.AddPanel(p, 99, pa4, new int[] { 250, FunDate.getInt(((DateTime)reader["SCHEDULEENDTIME"]).ToString("HH:mm")) - FunDate.getInt(((DateTime)reader["SCHEDULESTARTTIME"]).ToString("HH:mm"))+1 });
                    p.TabIndex = int.Parse(String.Format("{0}", ((Int64)reader["SCHEDULEID"]).ToString()));
                    p.Location = new Point(70, FunDate.getInt(((DateTime)reader["SCHEDULESTARTTIME"]).ToString("HH:mm")) + 25);
                    p.BackColor = ConSchedule.statusColors[(Int64)reader["STATUSID"] - 1];
                    FunCom.AddContextMenuStrip(p, ConCom.defaultBtnNames, new EventHandler[]
                    {
                        (sender, e) =>
                        {//新規
                            ConSchedule.execCode = 0;
                            ConSchedule.ID = "0";
                            scheduleForm.Visible = true;
                            ConInstance.schedule.Enabled = false;
                        },
                        (sender, e) =>
                        {//修正
                            if(dg.SelectedRows.Count == 0){ FunMSG.ErrMsg(ConMSG.message00010); return; }
                            ConSchedule.execCode = 1;
                            ConSchedule.ID = p.TabIndex.ToString();
                            scheduleForm.Visible = true;
                            ConInstance.schedule.Enabled = false;
                        },
                        (sender, e) =>
                        {//削除
                            if(dg.SelectedRows.Count == 0){ FunMSG.ErrMsg(ConMSG.message00010); return; }
                            DialogResult result = MessageBox.Show(ConMSG.message00100,"確認",MessageBoxButtons.OKCancel,MessageBoxIcon.Exclamation,MessageBoxDefaultButton.Button2);
                            if(result == DialogResult.OK)
                            {
                                ID = p.TabIndex.ToString();
                                FunSQL.SQLDML("SQL0421", ConSQL.ScheduleSQL.SQL0421, new string[] { "@VISIBLESTATUS","@SCHEDULEID" }, new string[] { "1",ID });
                                LocalMain();
                            }
                        }
                    });
                    Label l = new Label();
                    FunCom.AddLabel(l, 5, p);
                    StringBuilder sb = new StringBuilder();
                    sb.Append("-----------------------------------\n");
                    sb.Append("【");
                    sb.Append((string)reader["STATUSSUBNAME"]);
                    sb.Append("】");
                    sb.Append((string)reader["GOALNAME"]);
                    sb.Append("\n-----------------------------------\n");
                    sb.Append((string)reader["PRIORSUBNAME"]);
                    sb.Append((string)reader["PLANNAME"]);
                    sb.Append("(");
                    sb.Append((string)reader["WORKNAME"]);
                    sb.Append(")");
                    l.Text = sb.ToString();
                    l.Font = new Font("Yu mincho", 8, FontStyle.Regular);
                }
                for (int i = 0; i < 25; i++)
                {
                    Label l = new Label();
                    FunCom.AddLabel(l, 5, pa4);
                    l.Location = new Point(0, 10 + i * 200);
                    StringBuilder sb = new StringBuilder();
                    sb.Append("");
                    sb.Append(i.ToString("00"));
                    sb.Append(":00 - - - - - - - - - - - - - - - -");
                    l.Text = sb.ToString();
                    l.Font = new Font("Segoe Print", 8, FontStyle.Regular);
                }
            }
            public static void LocalMain()
            {
                if (Regex.IsMatch(tb1.Text, @"^[0-9]{4}-(0[1-9]|1[0-2])-(0[1-9]|[12][0-9]|3[01])$"))
                {
                    DataLocalload();
                    LbLoadload();
                }
            }
        }
        private void ScheduleDaily_VisibleChanged(object sender, EventArgs e)
        {
            int loadStatus = ConInstance.scheduleDailyFirstLoad;
            if (loadStatus == 1)
            {
                ConInstance.scheduleDailyFirstLoad = 2;
                LocalSetup.LocalMain(this);
                LocalStartup.LocalMain();
                LocalLoad.LocalMain();
            }
            else if(loadStatus == 2)
            {
                if (Visible == false) { return; }
                else
                {
                    LocalLoad.LocalMain();
                }
            }
            else if (loadStatus == 0)
            {
                ConInstance.scheduleDailyFirstLoad = 1;
            }
        }
    }
}
