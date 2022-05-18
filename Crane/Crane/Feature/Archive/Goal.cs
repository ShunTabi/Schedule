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
    public partial class Goal : UserControl
    {
        public Goal()
        {
            InitializeComponent();
        }
        //定義
        public static int execCode = 0;
        public static string ID = "0";
        public static Panel pa1 = new Panel();
        public static Panel pa2 = new Panel();
        public static Panel pa3 = new Panel();
        public static TextBox tb1 = new TextBox();
        public static TextBox tb2 = new TextBox();
        public static Button btn1 = new Button();
        public static ComboBox cb1 = new ComboBox();
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
                string[] lbs = new string[] { "種別", "目標" };
                for (int i = 0; i < lbs.Length; i++)
                {
                    Label lb1 = new Label();
                    funCom.addLabel(lb1, 5, pa1);
                    lb1.Text = lbs[i];
                    lb1.Font = new Font("Yu mincho", 10, FontStyle.Regular);
                    lb1.Location = new Point(15, 15 + i * 70);
                }
                funCom.addCombobox(cb1, 5, 1, pa1, new int[] { 180, 10 });
                cb1.Font = new Font("Yu mincho", 10, FontStyle.Regular);
                cb1.Location = new Point(100, 15);
                funCom.addTextbox(tb2, 5, 2, pa1, new int[] { 180, 10 });
                tb2.Location = new Point(100, 85);
                funCom.addButton(btn1, 5, 3, pa1, new int[] { 90, 50 });
                btn1.Text = conCom.defaultBtnNames[0];
                btn1.Location = new Point(100, 155);
                btn1.BackColor = Color.MediumOrchid;
                btn1.Click += (sender, e) => clickBtn(sender, e);
            }
            public static void rightSide()
            {
                funCom.addDataGridView(dg, 0, pa2, new int[] { 0, 0 });
                dg.BackgroundColor = Color.Plum;
                funCom.addDataGridViewColumns(dg, new string[] { "ID", "種別", "目標" });
                funCom.addcontextMenuStrip(dg, conCom.defaultBtnNames, new EventHandler[] { click1, click2, click3 });
                funCom.addPanel(pa3, 1, pa2, new int[] { 0, 80 });
                Label lb1 = new Label();
                funCom.addLabel(lb1, 5, pa3);
                lb1.Text = "目標";
                lb1.Location = new Point(15, 15);
                lb1.Font = new Font("Yu mincho", 10, FontStyle.Regular);
                funCom.addTextbox(tb1, 5, 1, pa3, new int[] { 180, 10 });
                tb1.Location = new Point(67, 15);
                tb1.TextChanged += (sender, e) => chgTxt(sender, e);
            }
            private static void chgTxt(object sender, EventArgs e)
            {
                load.dataload();
            }
            private static void clickBtn(object sender, EventArgs e)
            {
                if (cb1.Text == "" || tb2.Text == "")
                {
                    funMSG.errMsg(conMSG.message00001);
                }
                else
                {
                    if (execCode == 0)
                    {
                        funSQL.sqlDML("sql0102", conSQL.goal.sql0102, new string[] { "@GENREID","@GOALNAME" }, new string[] { cb1.SelectedValue.ToString(),tb2.Text });
                    }
                    else if (execCode == 1)
                    {
                        funSQL.sqlDML("sql0103", conSQL.goal.sql0103, new string[] { "@GENREID", "@GOALNAME","@GOALID" }, new string[] { cb1.SelectedValue.ToString(), tb2.Text, ID });
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
                SQLiteDataReader reader = funSQL.sqlSELECT("sql0105", conSQL.goal.sql0105, new string[] { "@GOALID" }, new string[] { ID });
                while (reader.Read())
                {
                    cb1.Text = (string)reader["GENRENAME"];
                    tb2.Text = (string)reader["GOALNAME"];
                }
                execCode = 1;
                btn1.Text = conCom.defaultBtnNames[execCode];
            }
            private static void click3(object sender, EventArgs e)
            {//削除
                ID = dg.SelectedRows[0].Cells[0].Value.ToString();
                SQLiteDataReader reader = funSQL.sqlSELECT("sql0104", conSQL.goal.sql0104, new string[] { "@GOALID" }, new string[] { ID });
                cleaning.main();
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
                SQLiteDataReader reader = funSQL.sqlSELECT("sql0101", conSQL.goal.sql0101, new string[] { "@GOALNAME" }, new string[] { sb.ToString() });
                dg.Rows.Clear();
                while (reader.Read())
                {
                    dg.Rows.Add(
                        ((Int64)reader["GOALID"]).ToString(),
                        (string)reader["GENRENAME"],
                        (string)reader["GOALNAME"]
                        );
                }
            }
            public static void cmdload()
            {
                long[] keys = new long[] { };
                string[] values = new string[] { };
                SQLiteDataReader reader = funSQL.sqlSELECT("sql0006", conSQL.genre.sql0006, new string[] { }, new string[] { });
                while (reader.Read())
                {
                    Array.Resize(ref keys, keys.Length + 1);
                    keys[keys.Length - 1] = (Int64)reader["GENREID"];
                    Array.Resize(ref values, values.Length + 1);
                    values[values.Length - 1] = (string)reader["GENRENAME"];
                }
                funCom.addComboboxItem(cb1, keys, values);
            }
            public static void main()
            {
                dataload();
                cmdload();
            }
        }
        private void Goal_Load(object sender, EventArgs e)
        {
            setup.main(this);
            startup.main();
            cleaning.main();
            load.main();
        }
    }
}
