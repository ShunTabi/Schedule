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
    public partial class Genre : UserControl
    {
        public Genre()
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
                Label lb1 = new Label();
                funCom.addLabel(lb1, 5, pa1);
                lb1.Text = "種別";
                lb1.Font = new Font("Yu mincho", 10, FontStyle.Regular);
                lb1.Location = new Point(15, 15);
                funCom.addTextbox(tb2, 5, 2, pa1, new int[] { 180, 10 });
                tb2.Location = new Point(100, 15);
                funCom.addButton(btn1, 5, 3, pa1, new int[] { 90, 50 });
                btn1.Text = conCom.defaultBtnNames[execCode];
                btn1.Location = new Point(100, 85);
                btn1.BackColor = Color.MediumOrchid;
                btn1.Click += (sender, e) => clickBtn(sender,e);
            }
            private static void rightSide()
            {
                funCom.addDataGridView(dg, 0, pa2, new int[] { 0, 0 });
                dg.BackgroundColor = Color.Plum;
                funCom.addDataGridViewColumns(dg, new string[] { "ID", "種別", "更新日" });
                funCom.addcontextMenuStrip(dg, conCom.defaultBtnNames, new EventHandler[] {click1,click2,click3});
                funCom.addPanel(pa3, 1, pa2, new int[] { 0, 80 });
                Label lb1 = new Label();
                funCom.addLabel(lb1, 5, pa3);
                lb1.Text = "種別";
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
                if (tb2.Text == "")
                {
                    funMSG.errMsg(conMSG.message00001);
                }
                else
                {
                    if(execCode == 0)
                    {
                        funSQL.sqlDML("sql0002", conSQL.genre.sql0002, new string[] { "@GENRENAME" }, new string[] { tb2.Text });
                    }
                    else if(execCode == 1)
                    {
                        funSQL.sqlDML("sql0003",conSQL.genre.sql0003, new string[] { "@GENRENAME","@GENREID" }, new string[] { tb2.Text,ID });
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
                execCode = 1;
                btn1.Text = conCom.defaultBtnNames[execCode];
                ID =  dg.SelectedRows[0].Cells[0].Value.ToString();
                SQLiteDataReader reader = funSQL.sqlSELECT("sql0005", conSQL.genre.sql0005,new string[]{ "@GENREID" },new string[]{ ID });
                while (reader.Read())
                {
                    tb2.Text = (string)reader["GENRENAME"];
                }
            }
            private static void click3(object sender, EventArgs e)
            {//削除
                ID = dg.SelectedRows[0].Cells[0].Value.ToString();
                SQLiteDataReader reader = funSQL.sqlSELECT("sql0004", conSQL.genre.sql0004, new string[] { "@GENREID" }, new string[] { ID });
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
                SQLiteDataReader reader = funSQL.sqlSELECT("sql0001", conSQL.genre.sql0001, new string[] { "@GENRENAME" }, new string[] { sb.ToString() });
                dg.Rows.Clear();
                while (reader.Read())
                {
                    dg.Rows.Add(
                        ((Int64)reader["GENREID"]).ToString(),
                        (string)reader["GENRENAME"],
                        ((DateTime)reader["GENREUPDATEDATE"]).ToString()
                        );
                }
            }
            public static void main()
            {
                dataload();
            }
        }
        private void Genre_Load(object sender, EventArgs e)
        {
            setup.main(this);
            startup.main();
            cleaning.main();
            load.main();
        }
    }
}
