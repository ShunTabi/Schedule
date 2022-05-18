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
        public static int execCode = 0;
        public static string ID = "0";
        public static Form schedulerForm = new SchedulerForm();
        public static Panel pa1 = new Panel();
        public static Panel pa2 = new Panel();
        public static Panel pa3 = new Panel();
        public static Panel pa4 = new Panel();
        public static Panel pa5 = new Panel();
        public static Panel pa6 = new Panel();
        public static TextBox tb1 = new TextBox();
        public static DataGridView dg = new DataGridView();
        class setup
        {
            private static void common(UserControl uc)
            {
                schedulerForm.Visible = false;
                uc.Padding = new Padding(20, 20, 20, 20);
                funCom.addPanel(pa2, 0, uc, new int[] { 0, 0 });
                funCom.addPanel(pa1, 2, uc, new int[] { 350, 0 });
            }
            private static void leftSide()
            {
                funCom.addPanel(pa4, 0, pa1, new int[] { 0, 0 });
                funCom.addPanel(pa3, 1, pa1, new int[] { 0, 70 });
                pa4.AutoScroll = true;
                Label l1 = new Label();
                funCom.addLabel(l1, 5, pa3);
                l1.Location = new Point(0, 20);
                l1.Font = new Font("Yu mincho", 10, FontStyle.Regular);
                l1.Text = "日付";
                funCom.addTextbox(tb1, 5, 0, pa3, new int[] { 160, 10 });
                tb1.Location = new Point(53, 20);
                tb1.Text = funDate.getToday(0);
                tb1.TextChanged += (sender, e) => chgTxt(sender, e);
            }
            private static void rightSide()
            {
                funCom.addDataGridView(dg, 0, pa2, new int[] { 0, 0 });
                dg.BackgroundColor = Color.Plum;
                funCom.addcontextMenuStrip(dg, conCom.defaultBtnNames, new EventHandler[] { click1, click2, click3 });
                funCom.addDataGridViewColumns(dg, new string[] { "ID", "目標", "計画/作業", " 進捗", "開始時間", "終了時間" });
                funCom.addPanel(pa5, 1, pa2, new int[] { 0, 100 });
                funCom.addPanel(pa6, 4, pa2, new int[] { 0, 100 });
                Label l2 = new Label();
                funCom.addLabel(l2, 5, pa5);
                l2.Location = new Point(0, 0);
                l2.Font = new Font("Segoe Print", 25, FontStyle.Regular);
                l2.Text = "TODAY";
                pa6.BackColor = Color.Plum;
                Button btn1 = new Button();
                funCom.addButton(btn1, 5, 1, pa6, new int[] { 90, 50 });
                btn1.Location = new Point(15, 15);
                btn1.Text = conCom.defaultBtnNames[0];
                btn1.BackColor = Color.MediumOrchid;
                btn1.Click += (sender, e) => clickBtn(sender, e);
            }
            private static void chgTxt(object sender, EventArgs e)
            {
                load.dataload();
                load.lbload();
            }
            private static void clickBtn(object sender, EventArgs e)
            {//新規
                conScheduler.execCode = 0;
                conScheduler.ID = "0";
                schedulerForm.Visible = true;
            }
            private static void click1(object sender, EventArgs e)
            {//新規
                conScheduler.execCode = 0;
                conScheduler.ID = "0";
                schedulerForm.Visible = true;
            }
            private static void click2(object sender, EventArgs e)
            {//修正
                conScheduler.execCode = 1;
                conScheduler.ID = dg.SelectedRows[0].Cells[0].Value.ToString();
                schedulerForm.Visible = true;
            }
            private static void click3(object sender, EventArgs e)
            {//削除
                string ID = dg.SelectedRows[0].Cells[0].Value.ToString();
                SQLiteDataReader reader = funSQL.sqlSELECT("sql0404", conSQL.schedule.sql0404, new string[] { "@SCHEDULEID" }, new string[] { ID });
                load.main();
            }
            public static void main(UserControl uc)
            {
                common(uc);
                leftSide();
                rightSide();
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
        }
        class load
        {
            private static SQLiteDataReader sql0401()
            {
                SQLiteDataReader reader = funSQL.sqlSELECT("sql0401", conSQL.schedule.sql0401, new string[] { "@SCHEDULEDATE" }, new string[] { tb1.Text });
                return reader;
            }
            public static void dataload()
            {
                SQLiteDataReader reader = sql0401();
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
                }
            }
            public static void lbload()
            {
                pa4.Controls.Clear();
                for (int i = 0; i < 25; i++)
                {
                    Label l = new Label();
                    funCom.addLabel(l, 5, pa4);
                    l.Location = new Point(10, 10 + i * 200);
                    StringBuilder sb = new StringBuilder();
                    sb.Append("--");
                    sb.Append(i.ToString("00"));
                    sb.Append(":00--");
                    l.Text = sb.ToString();
                    l.Font = new Font("Segoe Print", 9, FontStyle.Regular);
                }
                SQLiteDataReader reader = sql0401();
                while (reader.Read())
                {
                    Panel p = new Panel();
                    funCom.addPanel(p, 99, pa4, new int[] { 300, funDate.getInt(((DateTime)reader["SCHEDULEENDTIME"]).ToString("HH:mm")) - funDate.getInt(((DateTime)reader["SCHEDULESTARTTIME"]).ToString("HH:mm")) });
                    p.Location = new Point(10, funDate.getInt(((DateTime)reader["SCHEDULESTARTTIME"]).ToString("HH:mm")));
                    p.BackColor = Color.Plum;
                    Label l = new Label();
                    l.Padding = new Padding(5, 25, 25, 5);
                    funCom.addLabel(l, 5, p);
                    StringBuilder sb = new StringBuilder();
                    sb.Append((string)reader["PRIORSUBNAME"]);
                    sb.Append((string)reader["PLANNAME"]);
                    sb.Append(":");
                    sb.Append((string)reader["WORKNAME"]);
                    sb.Append("\n--");
                    sb.Append((string)reader["STATUSSUBNAME"]);
                    sb.Append("--");
                    l.Text = sb.ToString();
                    l.Font = new Font("Yu mincho", 8, FontStyle.Regular);
                }
            }
            public static void main()
            {
                dataload();
                lbload();
            }
        }
        private void Scheduler_Load(object sender, EventArgs e)
        {
            setup.main(this);
            startup.main();
            load.main();
        }
    }
}
