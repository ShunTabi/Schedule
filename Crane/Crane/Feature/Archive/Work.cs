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
    public partial class Work : UserControl
    {
        public Work()
        {
            InitializeComponent();
        }
        //定義
        public static int execCode = 0;
        public static string ID = "0";
        public static Panel pa1 = new Panel();
        public static Panel pa2 = new Panel();
        public static Panel pa3 = new Panel();
        public static Panel pa4 = new Panel();
        public static Panel pa5 = new Panel();
        public static TextBox tb1 = new TextBox();
        public static TextBox tb2 = new TextBox();
        public static TextBox tb3 = new TextBox();
        public static TextBox tb4 = new TextBox();
        public static Button btn1 = new Button();
        public static ComboBox cb1 = new ComboBox();
        public static ComboBox cb2 = new ComboBox();
        public static ComboBox cb3 = new ComboBox();
        public static DataGridView dg = new DataGridView();
        class setup
        {
            private static void common(UserControl uc)
            {
                funCom.addPanel(pa2, 0, uc, new int[] { 0, 0 });
                funCom.addPanel(pa1, 2, uc, new int[] { 350, 0 });
                pa2.BackColor = Color.Plum;
            }
            private static void leftSide()
            {
                string[] lbs = new string[] { "目標", "計画", "作業", "優先度", "開始日", "終了日" };
                for (int i = 0; i < lbs.Length; i++)
                {
                    Label lb = new Label();
                    funCom.addLabel(lb, 5, pa1);
                    lb.Text = lbs[i];
                    lb.Font = new Font("Yu mincho", 10, FontStyle.Regular);
                    lb.Location = new Point(15, 15 + i * 70);
                }
                funCom.addCombobox(cb1, 5, 1, pa1, new int[] { 180, 10 });
                cb1.Font = new Font("Yu mincho", 10, FontStyle.Regular);
                cb1.Location = new Point(100, 15);
                cb1.SelectedIndexChanged += (sender, e) => chgcmb(sender, e);
                funCom.addCombobox(cb2, 5, 2, pa1, new int[] { 180, 10 });
                cb2.Font = new Font("Yu mincho", 10, FontStyle.Regular);
                cb2.Location = new Point(100, 85);
                funCom.addTextbox(tb2, 5, 3, pa1, new int[] { 180, 10 });
                tb2.Location = new Point(100, 155);
                funCom.addCombobox(cb3, 5, 4, pa1, new int[] { 180, 10 });
                cb3.Font = new Font("Yu mincho", 10, FontStyle.Regular);
                cb3.Location = new Point(100, 225);
                funCom.addTextbox(tb3, 5, 5, pa1, new int[] { 180, 10 });
                tb3.Location = new Point(100, 295);
                tb3.Text = funDate.getToday(0);
                funCom.addTextbox(tb4, 5, 6, pa1, new int[] { 180, 10 });
                tb4.Location = new Point(100, 365);
                tb4.Text = funDate.getToday(0);
                funCom.addButton(btn1, 5, 7, pa1, new int[] { 90, 50 });
                btn1.Text = conCom.defaultBtnNames[0];
                btn1.Location = new Point(100, 435);
                btn1.BackColor = Color.MediumOrchid;
                btn1.Click += (sender, e) => clickBtn(sender, e);
            }
            public static void rightSide()
            {
                funCom.addDataGridView(dg, 0, pa2, new int[] { 0, 0 });
                dg.BackgroundColor = Color.Plum;
                funCom.addDataGridViewColumns(dg, new string[] { "ID", "目標", "計画", "優先度/作業", "開始日", "終了日" });
                funCom.addcontextMenuStrip(dg, conCom.defaultBtnNames, new EventHandler[] { click1, click2, click3 });
                funCom.addPanel(pa3, 1, pa2, new int[] { 0, 80 });
                Label lb1 = new Label();
                funCom.addLabel(lb1, 5, pa3);
                lb1.Text = "作業";
                lb1.Location = new Point(15, 15);
                lb1.Font = new Font("Yu mincho", 10, FontStyle.Regular);
                funCom.addTextbox(tb1, 5, 1, pa3, new int[] { 180, 10 });
                tb1.Location = new Point(67, 15);
                tb1.TextChanged += (sender, e) => chgTxt(sender, e);
            }
            private static void chgcmb(object sender, EventArgs e)
            {
                load.cmdload2();
            }
            private static void chgTxt(object sender, EventArgs e)
            {
                load.dataload();
            }
            private static void clickBtn(object sender, EventArgs e)
            {
                if (cb1.Text == "" || cb2.Text == "" || tb2.Text == "" || cb3.Text == "" || tb3.Text == "" || tb4.Text == "")
                {
                    funMSG.errMsg(conMSG.message00001);
                }
                else if(!Regex.IsMatch(tb3.Text, @"^[0-9]{4}-(0[1-9]|1[0-2])-(0[1-9]|[12][0-9]|3[01])$") || !Regex.IsMatch(tb4.Text, @"^[0-9]{4}-(0[1-9]|1[0-2])-(0[1-9]|[12][0-9]|3[01])$"))
                {
                    funMSG.errMsg(conMSG.message00005);
                }
                else
                {
                    if (execCode == 0)
                    {
                        funSQL.sqlDML("sql0302", conSQL.work.sql0302, new string[] { "@PLANID", "@WORKNAME", "@PRIORID", "WORKSTARTDATE", "WORKENDDATE" }, new string[] { cb2.SelectedValue.ToString(), tb2.Text, cb3.SelectedValue.ToString(), tb3.Text, tb4.Text });
                    }
                    else if (execCode == 1)
                    {
                        funSQL.sqlDML("sql0303", conSQL.work.sql0303, new string[] { "@PLANID", "@WORKNAME", "@PRIORID", "WORKSTARTDATE", "WORKENDDATE", "@WORKID" }, new string[] { cb2.SelectedValue.ToString(), tb2.Text, cb3.SelectedValue.ToString(), tb3.Text, tb4.Text, ID });
                    }
                    cleaning.main();
                    load.main();
                }
            }
            private static void click1(object sender, EventArgs e)
            {//新規
                execCode = 0;
                btn1.Text = conCom.defaultBtnNames[execCode];
            }
            private static void click2(object sender, EventArgs e)
            {//修正
                ID = dg.SelectedRows[0].Cells[0].Value.ToString();
                SQLiteDataReader reader = funSQL.sqlSELECT("sql0305", conSQL.work.sql0305, new string[] { "@WORKID" }, new string[] { ID });
                while (reader.Read())
                {
                    cb1.Text = (string)reader["GOALNAME"];
                    tb2.Text = (string)reader["WORKNAME"];
                    cb3.Text = (string)reader["PRIORNAME"];
                    tb3.Text = ((DateTime)reader["WORKSTARTDATE"]).ToString("yyyy-MM-dd");
                    tb4.Text = ((DateTime)reader["WORKENDDATE"]).ToString("yyyy-MM-dd");
                    load.cmdload2();
                    cb2.Text = (string)reader["PLANNAME"];
                }
                execCode = 1;
                btn1.Text = conCom.defaultBtnNames[execCode];
            }
            private static void click3(object sender, EventArgs e)
            {//削除
                ID = dg.SelectedRows[0].Cells[0].Value.ToString();
                SQLiteDataReader reader = funSQL.sqlSELECT("sql0304", conSQL.work.sql0304, new string[] { "@WORKID" }, new string[] { ID });
                //cleaning.main();
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
            public static void main()
            {
                tb2.Text = "";
                tb3.Text = funDate.getToday(0);
                tb4.Text = funDate.getToday(0);
                ID = "0";
                execCode = 0;
            }
        }
        class load
        {
            public static void dataload()
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("%");
                sb.Append(tb1.Text);
                sb.Append("%");
                SQLiteDataReader reader = funSQL.sqlSELECT("sql0301", conSQL.work.sql0301, new string[] { "@WORKNAME" }, new string[] { sb.ToString() });
                dg.Rows.Clear();
                while (reader.Read())
                {
                    StringBuilder sb1 = new StringBuilder();
                    sb1.Append((string)reader["PRIORSUBNAME"]);
                    sb1.Append((string)reader["WORKNAME"]);
                    dg.Rows.Add(
                        ((Int64)reader["WORKID"]).ToString(),
                        (string)reader["GOALNAME"],
                        (string)reader["PLANNAME"],
                        sb1.ToString(),
                        ((DateTime)reader["WORKSTARTDATE"]).ToString("yyyy-MM-dd"),
                        ((DateTime)reader["WORKENDDATE"]).ToString("yyyy-MM-dd")
                        );
                }
            }
            private static void cmdload1()
            {
                SQLiteDataReader reader = null;
                long[] keys = new long[] { };
                string[] values = new string[] { };
                reader = funSQL.sqlSELECT("sql0106", conSQL.goal.sql0106, new string[] { }, new string[] { });
                while (reader.Read())
                {
                    Array.Resize(ref keys, keys.Length + 1);
                    keys[keys.Length - 1] = (Int64)reader["GOALID"];
                    Array.Resize(ref values, values.Length + 1);
                    values[values.Length - 1] = (string)reader["GOALNAME"];
                }
                funCom.addComboboxItem(cb1, keys, values);
                keys = new long[] { };
                values = new string[] { };
                reader = funSQL.sqlSELECT("sql9001", conSQL.prior.sql9001, new string[] { }, new string[] { });
                while (reader.Read())
                {
                    Array.Resize(ref keys, keys.Length + 1);
                    keys[keys.Length - 1] = (Int64)reader["PRIORID"];
                    Array.Resize(ref values, values.Length + 1);
                    values[values.Length - 1] = (string)reader["PRIORNAME"];
                }
                funCom.addComboboxItem(cb3, keys, values);
            }
            public static void cmdload2()
            {
                if(cb1.SelectedValue != null)
                {
                    SQLiteDataReader reader = null;
                    long[] keys = new long[] { };
                    string[] values = new string[] { };
                    reader = funSQL.sqlSELECT("sql0206", conSQL.plan.sql0206, new string[] { "@GOALID" }, new string[] { cb1.SelectedValue.ToString() });
                    while (reader.Read())
                    {
                        Array.Resize(ref keys, keys.Length + 1);
                        keys[keys.Length - 1] = (Int64)reader["PLANID"];
                        Array.Resize(ref values, values.Length + 1);
                        values[values.Length - 1] = (string)reader["PLANNAME"];
                    }
                    funCom.addComboboxItem(cb2, keys, values);
                }
            }
            public static void cmdload()
            {
                cmdload1();
                cmdload2();
            }
            public static void main()
            {
                dataload();
                cmdload();
            }
        }
        private void Work_Load(object sender, EventArgs e)
        {
            setup.main(this);
            startup.main();
            cleaning.main();
            load.main();
        }
    }
}
