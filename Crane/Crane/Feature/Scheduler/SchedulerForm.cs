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
    public partial class SchedulerForm : Form
    {
        public SchedulerForm()
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
        class setup
        {
            private static void common(Form frm)
            {
                frm.FormClosing += (e, sender) => funCom.neverClose(e, sender, true);
                frm.VisibleChanged += (e, sender) => load.frmchg(e, sender);
                frm.Location = new Point(
                    int.Parse(string.Format("{0}", funINI.getString(conFILE.iniDefault, "[Sub]", "Location", 0)[0])),
                    int.Parse(string.Format("{0}", funINI.getString(conFILE.iniDefault, "[Sub]", "Location", 0)[1]))
                    );
                frm.Size = new Size(700, 800);
                frm.FormBorderStyle = FormBorderStyle.None;
                funCom.addPanel(pa2, 0, frm, new int[] { 0, 0 });
                funCom.addPanel(pa1, 1, frm, new int[] { 0, 20 });
                funCom.addPanel(pa3, 4, frm, new int[] { 0, 20 });
                pa1.BackColor = Color.SpringGreen;
                pa3.BackColor = Color.SpringGreen;
                funCom.addcontextMenuStrip(pa1, new string[] { "フォームを閉じる" }, new EventHandler[] { (e, sender) => close.main(e, sender, frm) });
            }
            private static void createForm(Form frm)
            {
                funCom.addLabel(lb1, 5, pa2);
                lb1.Location = new Point(30, 30);
                lb1.Font = new Font("Segoe Print", 20, FontStyle.Regular);
                string[] names = new string[] { "目標", "計画/作業", "進捗", "日付", "開始時間", "終了時間" };
                for (int i = 0; i < names.Length; i++)
                {
                    Label l = new Label();
                    funCom.addLabel(l, 5, pa2);
                    l.Text = names[i];
                    l.Location = new Point(70, 150 + 70 * i);
                    l.Font = new Font("Yu mincho", 10, FontStyle.Regular);
                }
                ComboBox[] cbs = new ComboBox[] { cb1, cb2, cb3 };
                for (int i = 0; i < cbs.Length; i++)
                {
                    funCom.addCombobox(cbs[i], 5, i, pa2, new int[] { 200, 10 });
                    cbs[i].Location = new Point(240, 150 + 70 * i);
                    cbs[i].Font = new Font("Yu mincho", 10, FontStyle.Regular);
                }
                cb1.SelectedIndexChanged += (sender, e) => cmbchg(sender, e);
                TextBox[] tbs = new TextBox[] { tb1, tb2, tb3 };
                for (int i = 0; i < tbs.Length; i++)
                {
                    funCom.addTextbox(tbs[i], 5, i + 3, pa2, new int[] { 200, 10 });
                    tbs[i].Location = new Point(240, 360 + 70 * i);
                    tbs[i].Font = new Font("Yu mincho", 10, FontStyle.Regular);
                }
                funCom.addButton(btn1, 5, 6, pa2, new int[] { 90, 50 });
                btn1.Location = new Point(240, 570);
                btn1.BackColor = Color.MediumOrchid;
                btn1.Click += (sender, e) => clickBtn(sender, e, frm);
            }
            public static void clickBtn(object sender, EventArgs e, Form frm)
            {
                if (cb1.SelectedValue == null || cb2.SelectedValue == null || cb3.SelectedValue == null || tb1.Text == "" || tb2.Text == "" || tb3.Text == "")
                {
                    funMSG.errMsg(conMSG.message00001);
                }
                else if (!Regex.IsMatch(tb1.Text, @"^[0-9]{4}-(0[1-9]|1[0-2])-(0[1-9]|[12][0-9]|3[01])$") || !Regex.IsMatch(tb2.Text, @"^([01][0-9]|2[0-3]):[0-5][0-9]$") || !Regex.IsMatch(tb3.Text, @"^([01][0-9]|2[0-3]):[0-5][0-9]$"))
                {
                    funMSG.errMsg(conMSG.message00005);
                }
                else
                {
                    if (conScheduler.execCode == 0)
                    {
                        funSQL.sqlDML("sql0402", conSQL.schedule.sql0402, new string[] { "@WORKID", "@STATUSID", "@SCHEDULEDATE", "@SCHEDULESTARTTIME", "@SCHEDULEENDTIME" },
                            new string[] { cb2.SelectedValue.ToString(), cb3.SelectedValue.ToString(), tb1.Text, tb2.Text, tb3.Text });
                    }
                    else if (conScheduler.execCode == 1)
                    {
                        funSQL.sqlDML("sql0403", conSQL.schedule.sql0403, new string[] { "@WORKID", "@STATUSID", "@SCHEDULEDATE", "@SCHEDULESTARTTIME", "@SCHEDULEENDTIME", "@SCHEDULEID" },
                            new string[] { cb2.SelectedValue.ToString(), cb3.SelectedValue.ToString(), tb1.Text, tb2.Text, tb3.Text, conScheduler.ID });
                    }
                    conScheduler.execCode = 0;
                    conScheduler.ID = "0";
                    conInstance.scheduler.Enabled = true;
                    frm.Visible = false;
                }
            }
            public static void cmbchg(object sender, EventArgs e)
            {
                cleaning.cln2();
            }
            public static void main(Form frm) { common(frm); createForm(frm); }
        }
        class startup
        {
            public static void main() { }
        }
        class cleaning
        {
            private static void cln1()
            {
                SQLiteDataReader reader = funSQL.sqlSELECT("sql0106", conSQL.goal.sql0106, new string[] { }, new string[] { });
                long[] keys = new long[] { };
                string[] values = new string[] { };
                while (reader.Read())
                {
                    Array.Resize(ref keys, keys.Length + 1);
                    keys[keys.Length - 1] = (Int64)reader["GOALID"];
                    Array.Resize(ref values, values.Length + 1);
                    values[values.Length - 1] = (string)reader["GOALNAME"];
                }
                funCom.addComboboxItem(cb1, keys, values);
                reader = funSQL.sqlSELECT("sql9002", conSQL.status.sql9002, new string[] { }, new string[] { });
                keys = new long[] { };
                values = new string[] { };
                while (reader.Read())
                {
                    Array.Resize(ref keys, keys.Length + 1);
                    keys[keys.Length - 1] = (Int64)reader["STATUSID"];
                    Array.Resize(ref values, values.Length + 1);
                    values[values.Length - 1] = (string)reader["STATUSNAME"];
                }
                funCom.addComboboxItem(cb3, keys, values);
            }
            public static void cln2()
            {
                SQLiteDataReader reader = funSQL.sqlSELECT("sql0306", conSQL.work.sql0306, new string[] { "@GOALID" }, new string[] { cb1.SelectedValue.ToString() });
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
                funCom.addComboboxItem(cb2, keys, values);
            }
            public static void main() { cln1(); cln2(); }
        }
        class load
        {
            private static void load1()
            {
                tb1.Text = funDate.getToday(0);
                tb2.Text = funDate.getToday(1);
                tb3.Text = funDate.getToday(1);
            }
            private static void load2()
            {
                SQLiteDataReader reader = funSQL.sqlSELECT("sql0405", conSQL.schedule.sql0405, new string[] { "@SCHEDULEID" }, new string[] { conScheduler.ID });
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
            private static void main()
            {
                if (conScheduler.execCode == 0)
                {
                    load1();
                }
                else if (conScheduler.execCode == 1)
                {
                    load2();
                }
                StringBuilder sb = new StringBuilder();
                sb.Append("ScheduleForm【");
                sb.Append(conCom.defaultBtnNames[conScheduler.execCode]);
                sb.Append("】");
                lb1.Text = sb.ToString();
                btn1.Text = conCom.defaultBtnNames[conScheduler.execCode];
            }
            public static void frmchg(object sender, EventArgs e)
            {
                cleaning.main();
                main();
            }
        }
        private void SchedulerForm_Load(object sender, EventArgs e)
        {
            setup.main(this);
            startup.main();
            //cleaning.main();
            //load.main();
        }
        class close
        {
            public static void main(object sender, EventArgs e, Form frm)
            {
                conInstance.scheduler.Enabled = true;
                frm.Visible = false;
            }
        }
    }
}
