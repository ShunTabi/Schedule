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
    public partial class Todaywork : Form
    {
        public Todaywork(int s_execCode,string s_ID)
        {
            InitializeComponent();
            execCode = s_execCode;
            ID = s_ID;
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
        public static ComboBox cb2 = new ComboBox();
        class setup
        {
            private static void full(Form frm)
            {
                frm.FormBorderStyle = FormBorderStyle.None;
                frm.Padding = new Padding(0, 0, 0, 0);
                frm.Location = new Point(
                    int.Parse(string.Format("{0}", funINI.getString(conFILE.iniDefault, "[Sub]", "Location", 0)[0])),
                    int.Parse(string.Format("{0}", funINI.getString(conFILE.iniDefault, "[Sub]", "Location", 0)[1]))
                    );
                funCom.addPanel(pa2, 0, frm, new int[] { 0, 0 });
                funCom.addPanel(pa1, 1, frm, new int[] { 0, 30 });
                funCom.addPanel(pa3, 4, frm, new int[] { 0, 30 });
                pa1.BackColor = Color.SpringGreen;
                pa2.BackColor = Color.AliceBlue;
                pa3.BackColor = Color.SpringGreen;
                frm.Size = new Size(500, 600);
                string[] lbs = new string[] { "目標", "作業", "開始時間", "終了時間" };
                for (int i = 0; i < lbs.Length; i++)
                {
                    Label lb = new Label();
                    funCom.addLabel(lb, 5, pa2);
                    lb.Text = lbs[i];
                    lb.Font = new Font("Yu mincho", 10, FontStyle.Regular);
                    lb.Location = new Point(35, 35 + i * 70);
                }
                funCom.addCombobox(cb1, 5, 1, pa2, new int[] { 180, 10 });
                cb1.Font = new Font("Yu mincho", 10, FontStyle.Regular);
                cb1.Location = new Point(130, 35);
                cb1.SelectedIndexChanged += (sender, e) => chgcmb(sender, e);
                funCom.addCombobox(cb2, 5, 2, pa2, new int[] { 180, 10 });
                cb2.Font = new Font("Yu mincho", 10, FontStyle.Regular);
                cb2.Location = new Point(130, 105);
                funCom.addTextbox(tb1, 5, 3, pa2, new int[] { 180, 10 });
                tb1.Location = new Point(130, 175);
                tb1.Text = funToday.getToday(0);
                funCom.addTextbox(tb2, 5, 4, pa2, new int[] { 180, 10 });
                tb2.Location = new Point(130, 245);
                tb2.Text = funToday.getToday(0);
                funCom.addButton(btn1, 5, 3, pa2, new int[] { 90, 50 });
                btn1.Text = conCom.defaultBtnNames[0];
                btn1.Location = new Point(130, 315);
                btn1.BackColor = Color.MediumOrchid;
                btn1.Click += (sender, e) => clickBtn(sender, e);
            }
            private static void chgcmb(object sender, EventArgs e)
            {
                load.cmdload2();
            }
            private static void clickBtn(object sender, EventArgs e)
            {
                if (cb1.Text == "" || cb2.Text == "" || tb1.Text == "" || tb2.Text == "")
                {
                    funMSG.errMsg(conMSG.message00001);
                }
                else
                {
                    if (execCode == 0)
                    {
                        //funSQL.sqlDML("sql0302", conSQL.work.sql0302, new string[] { "@PLANID", "@WORKNAME", "@PRIORID", "WORKSTARTDATE", "WORKENDDATE" }, new string[] { cb2.SelectedValue.ToString(), tb2.Text, cb3.SelectedValue.ToString(), tb3.Text, tb4.Text });
                    }
                    else if (execCode == 1)
                    {
                        //funSQL.sqlDML("sql0303", conSQL.work.sql0303, new string[] { "@PLANID", "@WORKNAME", "@PRIORID", "WORKSTARTDATE", "WORKENDDATE", "@WORKID" }, new string[] { cb2.SelectedValue.ToString(), tb2.Text, cb3.SelectedValue.ToString(), tb3.Text, tb4.Text, ID });
                    }
                    //cleaning.main();
                    load.main();
                }
            }
            public static void main(Form frm)
            {
                full(frm);
            }
        }
        class load
        {
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
            }
            public static void cmdload2()
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
            public static void main()
            {
                cmdload1();
                cmdload2();
            }
        }
        private void Todaywork_Load(object sender, EventArgs e)
        {
            setup.main(this);
            load.main();
        }
    }
}
