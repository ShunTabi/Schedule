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
using System.Diagnostics;
using System.Text.RegularExpressions;


namespace Crane
{
    public partial class ScheduleForm : Form
    {
        public ScheduleForm()
        {
            InitializeComponent();
        }
        //定義
        public static Panel pa1 = new Panel();
        public static Panel pa2 = new Panel();
        public static Panel pa3 = new Panel();
        public static Label lb1 = new Label();
        public static TextBox tb1 = new TextBox();
        public static TextBox tb2 = new TextBox();
        public static TextBox tb3 = new TextBox();
        public static Button btn1 = new Button();
        public static ComboBox cb1 = new ComboBox();
        public static ComboBox cb2 = new ComboBox();
        public static ComboBox cb3 = new ComboBox();
        class LocalSetup
        {
            private static void Common(Form frm)
            {
                frm.FormClosing += (e, sender) => FunCom.neverClose(e, sender, true);
                frm.VisibleChanged += (e, sender) =>
                {
                    LocalCleaning.LocalMain();
                    LocalLoad.LocalMain();
                };
                frm.Location = new Point(
                    int.Parse(string.Format("{0}", FunINI.GetString(ConFILE.iniDefault, "[Sub]", "Location")[0])),
                    int.Parse(string.Format("{0}", FunINI.GetString(ConFILE.iniDefault, "[Sub]", "Location")[1]))
                    );
                frm.Size = new Size(700, 800);
                frm.FormBorderStyle = FormBorderStyle.None;
                FunCom.AddPanel(pa2, 0, frm, new int[] { 0, 0 });
                FunCom.AddPanel(pa1, 1, frm, new int[] { 0, 20 });
                FunCom.AddPanel(pa3, 4, frm, new int[] { 0, 20 });
                pa1.BackColor = Color.SpringGreen;
                pa3.BackColor = Color.SpringGreen;
                FunCom.AddContextMenuStrip(pa1, new string[] { "フォームを閉じる" }, new EventHandler[] { (e, sender) => LocalClose.LocalMain(frm) });
            }
            private static void CreateForm(Form frm)
            {
                FunCom.AddLabel(lb1, 5, pa2);
                lb1.Location = new Point(30, 30);
                lb1.Font = new Font("Segoe Print", 20, FontStyle.Regular);
                string[] names = new string[] { "目標", "計画/作業", "進捗", "日付", "開始時間", "終了時間" };
                for (int i = 0; i < names.Length; i++)
                {
                    Label l = new Label();
                    FunCom.AddLabel(l, 5, pa2);
                    l.Text = names[i];
                    l.Location = new Point(70, 150 + 70 * i);
                    l.Font = new Font("Yu mincho", 10, FontStyle.Regular);
                }
                ComboBox[] cbs = new ComboBox[] { cb1, cb2, cb3 };
                for (int i = 0; i < cbs.Length; i++)
                {
                    FunCom.AddCombobox(cbs[i], 5, i, pa2, new int[] { 200, 10 });
                    cbs[i].Location = new Point(240, 150 + 70 * i);
                    cbs[i].Font = new Font("Yu mincho", 10, FontStyle.Regular);
                }
                cb1.SelectedIndexChanged += (sender, e) =>
                {
                    LocalCleaning.CLN2();
                };
                TextBox[] tbs = new TextBox[] { tb1, tb2, tb3 };
                for (int i = 0; i < tbs.Length; i++)
                {
                    FunCom.AddTextbox(tbs[i], 5, i + 3, pa2, new int[] { 200, 10 });
                    tbs[i].Location = new Point(240, 360 + 70 * i);
                    tbs[i].Font = new Font("Yu mincho", 10, FontStyle.Regular);
                }
                FunCom.AddButton(btn1, 5, 6, pa2, new int[] { 90, 50 });
                btn1.Location = new Point(240, 570);
                btn1.BackColor = Color.MediumOrchid;
                btn1.Click += (sender, e) =>
                {
                    btn1.Enabled = false;
                    if (cb1.SelectedValue == null || cb2.SelectedValue == null || cb3.SelectedValue == null || tb1.Text == "" || tb2.Text == "" || tb3.Text == "")
                    {
                        FunMSG.ErrMsg(ConMSG.message00001);
                    }
                    else if (!Regex.IsMatch(tb1.Text, @"^[0-9]{4}-(0[1-9]|1[0-2])-(0[1-9]|[12][0-9]|3[01])$") || !Regex.IsMatch(tb2.Text, @"^([01][0-9]|2[0-3]):[0-5][0-9]$") || !Regex.IsMatch(tb3.Text, @"^([01][0-9]|2[0-3]):[0-5][0-9]$"))
                    {
                        FunMSG.ErrMsg(ConMSG.message00005);
                    }
                    else
                    {
                        if (ConSchedule.execCode == 0)
                        {
                            FunSQL.SQLDML("SQL0410", ConSQL.ScheduleSQL.SQL0410, new string[] { "@WORKID", "@STATUSID", "@SCHEDULEDATE", "@SCHEDULESTARTTIME", "@SCHEDULEENDTIME" },
                                new string[] { cb2.SelectedValue.ToString(), cb3.SelectedValue.ToString(), tb1.Text, tb2.Text, tb3.Text });
                        }
                        else if (ConSchedule.execCode == 1)
                        {
                            FunSQL.SQLDML("SQL0420", ConSQL.ScheduleSQL.SQL0420, new string[] { "@WORKID", "@STATUSID", "@SCHEDULEDATE", "@SCHEDULESTARTTIME", "@SCHEDULEENDTIME", "@SCHEDULEID" },
                                new string[] { cb2.SelectedValue.ToString(), cb3.SelectedValue.ToString(), tb1.Text, tb2.Text, tb3.Text, ConSchedule.ID });
                        }
                        ConSchedule.execCode = 0;
                        ConSchedule.ID = "0";
                        ConInstance.schedule.Enabled = true;
                        frm.Visible = false;
                        btn1.Enabled = true;
                    }
                };
            }
            public static void LocalMain(Form frm) { Common(frm); CreateForm(frm); }
        }
        class LocalStartup
        {
            public static void LocalMain() { }
        }
        class LocalCleaning
        {
            private static void CLN1()
            {
                SQLiteDataReader reader = FunSQL.SQLSELECT("SQL0102", ConSQL.GoalSQL.SQL0102, new string[] { }, new string[] { });
                long[] keys = new long[] { };
                string[] values = new string[] { };
                while (reader.Read())
                {
                    Array.Resize(ref keys, keys.Length + 1);
                    keys[keys.Length - 1] = (Int64)reader["GOALID"];
                    Array.Resize(ref values, values.Length + 1);
                    values[values.Length - 1] = (string)reader["GOALNAME"];
                }
                FunCom.AddComboboxItem(cb1, keys, values);
                reader = FunSQL.SQLSELECT("SQL9002", ConSQL.StatusSQL.SQL9002, new string[] { }, new string[] { });
                keys = new long[] { };
                values = new string[] { };
                while (reader.Read())
                {
                    Array.Resize(ref keys, keys.Length + 1);
                    keys[keys.Length - 1] = (Int64)reader["STATUSID"];
                    Array.Resize(ref values, values.Length + 1);
                    values[values.Length - 1] = (string)reader["STATUSNAME"];
                }
                FunCom.AddComboboxItem(cb3, keys, values);
            }
            public static void CLN2()
            {
                SQLiteDataReader reader = FunSQL.SQLSELECT("SQL0302", ConSQL.WorkSQL.SQL0302, new string[] { "@GOALID" }, new string[] { cb1.SelectedValue.ToString() });
                long[] keys = new long[] { };
                string[] values = new string[] { };
                while (reader.Read())
                {
                    StringBuilder sb = new StringBuilder();
                    Array.Resize(ref keys, keys.Length + 1);
                    keys[keys.Length - 1] = (Int64)reader["WORKID"];
                    Array.Resize(ref values, values.Length + 1);
                    sb.Append((string)reader["PLANNAME"]);
                    sb.Append(":");
                    sb.Append((string)reader["WORKNAME"]);
                    values[values.Length - 1] = sb.ToString();
                }
                FunCom.AddComboboxItem(cb2, keys, values);
            }
            public static void LocalMain() { CLN1(); CLN2(); }
        }
        class LocalLoad
        {
            private static void LocalLoad1()
            {
                if (Regex.IsMatch(ConSchedule.selectedDate, @"^[0-9]{4}-(0[1-9]|1[0-2])-(0[1-9]|[12][0-9]|3[01])$"))
                {
                    tb1.Text = ConSchedule.selectedDate;
                }
                else
                {
                    tb1.Text = FunDate.getToday(0, 0);
                }
                tb2.Text = FunDate.getToday(1, 0);
                tb3.Text = FunDate.getToday(1, 0);
            }
            private static void LocalLoad2()
            {
                SQLiteDataReader reader = FunSQL.SQLSELECT("SQL0401", ConSQL.ScheduleSQL.SQL0401, new string[] { "@SCHEDULEID" }, new string[] { ConSchedule.ID });
                while (reader.Read())
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append((string)reader["PLANNAME"]);
                    sb.Append(":");
                    sb.Append((string)reader["WORKNAME"]);
                    cb1.Text = (string)reader["GOALNAME"];
                    cb2.Text = sb.ToString();
                    cb3.Text = (string)reader["STATUSNAME"];
                    tb1.Text = ((DateTime)reader["SCHEDULEDATE"]).ToString("yyyy-MM-dd");
                    tb2.Text = ((DateTime)reader["SCHEDULESTARTTIME"]).ToString("HH:mm");
                    tb3.Text = ((DateTime)reader["SCHEDULEENDTIME"]).ToString("HH:mm");
                }
            }
            public static void LocalMain()
            {
                if (ConSchedule.execCode == 0)
                {
                    LocalLoad1();
                }
                else if (ConSchedule.execCode == 1)
                {
                    LocalLoad2();
                }
                StringBuilder sb = new StringBuilder();
                sb.Append("ScheduleForm【");
                sb.Append(ConCom.defaultBtnNames[ConSchedule.execCode]);
                sb.Append("】");
                lb1.Text = sb.ToString();
                btn1.Text = ConCom.defaultBtnNames[ConSchedule.execCode];
            }
        }
        private void ScheduleForm_Load(object sender, EventArgs e)
        {
            LocalSetup.LocalMain(this);
            LocalStartup.LocalMain();
            //LocalCleaning.LocalMain();
            //LocalLoad.LocalMain();
        }
        class LocalClose
        {
            public static void LocalMain(Form frm)
            {
                ConInstance.schedule.Enabled = true;
                frm.Visible = false;
            }
        }
    }
}
