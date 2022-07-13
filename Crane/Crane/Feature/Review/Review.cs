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
    public partial class Review : UserControl
    {
        public Review()
        {
            InitializeComponent();
        }
        //定義
        public static int FirstLoadStatus = ConInstance.reviewFirstLoad;
        public static int execCode = 0;
        public static string ID = "0";
        public static int LoadCode = 0;
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
        public static Button[] btns = new Button[] { };
        class LocalSetup
        {
            private static void Common1(UserControl uc)
            {
                //uc.VisibleChanged += (sender, e) => { if (uc.Visible == false) { return; } else { cleaning.LocalMain(); load.LocalMain(); } };
                FunCom.AddPanel(pa2, 0, uc, new int[] { 0, 0 });
                FunCom.AddPanel(pa1, 1, uc, new int[] { 0, 100 });
                string[] btnNames = new string[] { "未完了", "完了" };
                for (int i = 0; i < btnNames.Length; i++)
                {
                    Button btn = new Button();
                    Array.Resize(ref btns, btns.Length + 1);
                    btns[btns.Length - 1] = btn;
                    FunCom.AddButton(btn, 5, i, pa1, new int[] { 90, 50 });
                    btn.Text = btnNames[i];
                    btn.Location = new Point(10 + i * 95, 10);
                    btn.BackColor = ConColor.subButtonColor;
                    btn.Click += (sender, e) =>
                    {
                        for (int j = 0; j < btnNames.Length; j++)
                        {
                            btns[j].BackColor = ConColor.subButtonColor;
                            LoadCode = btn.TabIndex;
                            LocalCleaning.LocalMain();
                            LocalLoad.DataLocalLoad();
                        }
                        btns[btn.TabIndex].BackColor = ConColor.subButtonColorPushed;
                    };
                }
            }
            private static void Common2()
            {
                btns[ConMain.reviewStartupCode].BackColor = ConColor.subButtonColorPushed;
                LoadCode = ConMain.reviewStartupCode;
            }
            private static void Common3()
            {
                FunCom.AddPanel(pa4, 0, pa2, new int[] { 0, 0 });
                FunCom.AddPanel(pa3, 2, pa2, new int[] { 350, 0 });
                pa4.BackColor = Color.Plum;
                pa4.Padding = new Padding(10, 10, 10, 10);
            }
            private static void LeftSide()
            {
                string[] lbs = new string[] { "目標", "計画", "復習", "進捗", "終了日" };
                for (int i = 0; i < lbs.Length; i++)
                {
                    Label lb = new Label();
                    FunCom.AddLabel(lb, 5, pa3);
                    lb.Text = lbs[i];
                    lb.Font = new Font("Yu mincho", 10, FontStyle.Regular);
                    lb.Location = new Point(15, 15 + i * 70);
                }
                FunCom.AddCombobox(cb1, 5, 1, pa3, new int[] { 180, 10 });
                cb1.Font = new Font("Yu mincho", 10, FontStyle.Regular);
                cb1.Location = new Point(100, 15);
                cb1.SelectedIndexChanged += (sender, e) => { LocalLoad.CmdLocalLoad2(); };
                FunCom.AddCombobox(cb2, 5, 2, pa3, new int[] { 180, 10 });
                cb2.Font = new Font("Yu mincho", 10, FontStyle.Regular);
                cb2.Location = new Point(100, 85);
                FunCom.AddTextbox(tb2, 5, 3, pa3, new int[] { 180, 10 });
                tb2.Location = new Point(100, 155);
                FunCom.AddCombobox(cb3, 5, 4, pa3, new int[] { 180, 10 });
                cb3.Font = new Font("Yu mincho", 10, FontStyle.Regular);
                cb3.Location = new Point(100, 225);
                FunCom.AddTextbox(tb3, 5, 5, pa3, new int[] { 180, 10 });
                tb3.Location = new Point(100, 295);
                tb3.Text = FunDate.getToday(0, 0);
                FunCom.AddButton(btn1, 5, 7, pa3, new int[] { 90, 50 });
                btn1.Location = new Point(100, 365);
                btn1.BackColor = Color.MediumOrchid;
                btn1.Click += (sender, e) =>
                {
                    if (cb1.Text == "" || cb2.Text == "" || tb2.Text == "" || cb3.Text == "" || tb3.Text == "")
                    {
                        FunMSG.ErrMsg(ConMSG.CheckMSG.message00001);
                    }
                    else if (!Regex.IsMatch(tb3.Text, @"^[0-9]{4}-(0[1-9]|1[0-2])-(0[1-9]|[12][0-9]|3[01])$"))
                    {
                        FunMSG.ErrMsg(ConMSG.CheckMSG.message00003);
                    }
                    else
                    {
                        if (execCode == 0)
                        {
                            FunSQL.SQLDML("SQL0610", ConSQL.ReviewSQL.SQL0610, new string[] { "@PLANID", "@REVIEWNAME", "@STATUSID", "REVIEWENDDATE" }, new string[] { cb2.SelectedValue.ToString(), tb2.Text, cb3.SelectedValue.ToString(), tb3.Text});
                        }
                        else if (execCode == 1)
                        {
                            FunSQL.SQLDML("SQL0620", ConSQL.ReviewSQL.SQL0620, new string[] { "@PLANID", "@REVIEWNAME", "@STATUSID", "REVIEWENDDATE", "@REVIEWID" }, new string[] { cb2.SelectedValue.ToString(), tb2.Text, cb3.SelectedValue.ToString(), tb3.Text, ID });
                        }
                        LocalCleaning.LocalMain();
                        LocalLoad.LocalMain();
                    }
                };
            }
            public static void RightSide()
            {
                FunCom.AddDataGridView(dg, 0, pa4, new int[] { 0, 0 });
                dg.BackgroundColor = Color.Plum;
                FunCom.AddDataGridViewColumns(dg, new string[] { "ID","目標", "計画", "進捗/復習", "終了日" });
                FunCom.AddContextMenuStrip(dg, ConCom.defaultBtnNames, new EventHandler[]
                {
                    (sender, e) =>
                    {//新規
                        ID = "0";
                        execCode = 0;
                        btn1.Text = ConCom.defaultBtnNames[execCode];
                    },
                    (sender, e) =>
                    {//更新
                        if(dg.SelectedRows.Count == 0){ FunMSG.ErrMsg(ConMSG.CheckMSG.message00007); return; }
                        ID = dg.SelectedRows[0].Cells[0].Value.ToString();
                        SQLiteDataReader reader = FunSQL.SQLSELECT("SQL0602", ConSQL.ReviewSQL.SQL0602, new string[] { "@REVIEWID" }, new string[] { ID });
                        while (reader.Read())
                        {
                            cb1.Text = (string)reader["GOALNAME"];
                            tb2.Text = (string)reader["REVIEWNAME"];
                            cb3.Text = (string)reader["STATUSNAME"];
                            tb3.Text = ((DateTime)reader["REVIEWENDDATE"]).ToString("yyyy-MM-dd");
                            LocalLoad.CmdLocalLoad2();
                            cb2.Text = (string)reader["PLANNAME"];
                        }
                        execCode = 1;
                        btn1.Text = ConCom.defaultBtnNames[execCode];
                    },
                    (sender, e) =>
                    {//削除
                        if(dg.SelectedRows.Count == 0){ FunMSG.ErrMsg(ConMSG.CheckMSG.message00007); return; }
                        ID = dg.SelectedRows[0].Cells[0].Value.ToString();
                        FunSQL.SQLDML("SQL0621", ConSQL.ReviewSQL.SQL0621, new string[] { "@VISIBLESTATUS","@REVIEWID" }, new string[] { "1",ID });
                        LocalCleaning.LocalMain();
                        LocalLoad.LocalMain();
                    }
                }
                );
                FunCom.AddPanel(pa5, 1, pa4, new int[] { 0, 50 });
                Label lb1 = new Label();
                FunCom.AddLabel(lb1, 5, pa5);
                lb1.Text = "目標/計画";
                lb1.Location = new Point(0, 0);
                lb1.Font = new Font("Yu mincho", 10, FontStyle.Regular);
                FunCom.AddTextbox(tb1, 5, 1, pa5, new int[] { 180, 10 });
                tb1.Location = new Point(101, 0);
                tb1.TextChanged += (sender, e) => { LocalLoad.DataLocalLoad(); };
            }
            public static void LocalMain(UserControl uc)
            {
                Common1(uc);
                Common2();
                Common3();
                LeftSide();
                RightSide();
            }
        }
        class LocalStartup
        {

        }
        class LocalCleaning
        {
            public static void LocalMain()
            {
                tb2.Text = "";
                tb3.Text = FunDate.getToday(0, 0);
                tb4.Text = FunDate.getToday(0, 0);
                ID = "0";
                execCode = 0;
                btn1.Text = ConCom.defaultBtnNames[execCode];
            }
        }
        class LocalLoad
        {
            public static void DataLocalLoad()
            {
                StringBuilder sb = new StringBuilder("%");
                sb.Append(tb1.Text);
                sb.Append("%");
                SQLiteDataReader reader;
                if (LoadCode == 0)
                {
                    reader = FunSQL.SQLSELECT("SQL0600", ConSQL.ReviewSQL.SQL0600, new string[] { "@KEYWORD" }, new string[] { sb.ToString() });
                }
                else
                {
                    reader = FunSQL.SQLSELECT("SQL0601", ConSQL.ReviewSQL.SQL0601, new string[] { "@KEYWORD" }, new string[] { sb.ToString() });
                }
                dg.Rows.Clear();
                while (reader.Read())
                {
                    StringBuilder sb1 = new StringBuilder("【");
                    sb1.Append((string)reader["STATUSNAME"]);
                    sb1.Append("】");
                    sb1.Append((string)reader["REVIEWNAME"]);
                    dg.Rows.Add(
                        ((Int64)reader["REVIEWID"]).ToString(),
                        (string)reader["GOALNAME"],
                        (string)reader["PLANNAME"],
                        sb1.ToString(),
                        ((DateTime)reader["REVIEWENDDATE"]).ToString("yyyy-MM-dd")
                        );
                    if ((DateTime.Parse(FunDate.getToday(0, 0)) > (DateTime)reader["REVIEWENDDATE"]) && ((Int64)reader["STATUSID"] !=3 ))
                    {
                        int count = dg.Rows.Count - 1;
                        dg.Rows[count].DefaultCellStyle.ForeColor = Color.Red;
                        dg.Rows[count].DefaultCellStyle.BackColor = Color.Gainsboro;
                    }
                }
            }
            private static void CmdLocalLoad1()
            {
                long[] keys = new long[] { };
                string[] values = new string[] { };
                SQLiteDataReader reader = FunSQL.SQLSELECT("SQL0102", ConSQL.GoalSQL.SQL0102, new string[] { }, new string[] { });
                while (reader.Read())
                {
                    Array.Resize(ref keys, keys.Length + 1);
                    keys[keys.Length - 1] = (Int64)reader["GOALID"];
                    Array.Resize(ref values, values.Length + 1);
                    values[values.Length - 1] = (string)reader["GOALNAME"];
                }
                FunCom.AddComboboxItem(cb1, keys, values);
                keys = new long[] { };
                values = new string[] { };
                reader = FunSQL.SQLSELECT("SQL9001", ConSQL.StatusSQL.SQL9002, new string[] { }, new string[] { });
                while (reader.Read())
                {
                    Array.Resize(ref keys, keys.Length + 1);
                    keys[keys.Length - 1] = (Int64)reader["STATUSID"];
                    Array.Resize(ref values, values.Length + 1);
                    values[values.Length - 1] = (string)reader["STATUSNAME"];
                }
                FunCom.AddComboboxItem(cb3, keys, values);
            }
            public static void CmdLocalLoad2()
            {
                if (cb1.SelectedValue != null)
                {
                    long[] keys = new long[] { };
                    string[] values = new string[] { };
                    SQLiteDataReader reader = FunSQL.SQLSELECT("SQL0202", ConSQL.PlanSQL.SQL0202, new string[] { "@GOALID" }, new string[] { cb1.SelectedValue.ToString() });
                    while (reader.Read())
                    {
                        Array.Resize(ref keys, keys.Length + 1);
                        keys[keys.Length - 1] = (Int64)reader["PLANID"];
                        Array.Resize(ref values, values.Length + 1);
                        values[values.Length - 1] = (string)reader["PLANNAME"];
                    }
                    FunCom.AddComboboxItem(cb2, keys, values);
                }
            }
            public static void CmdLocalLoad()
            {
                CmdLocalLoad1();
                CmdLocalLoad2();
            }
            public static void LocalMain()
            {
                DataLocalLoad();
                CmdLocalLoad();
            }
        }
        private void Review_VisibleChanged(object sender, EventArgs e)
        {
            if (ConInstance.review.Visible == true)
            {
                if (ConInstance.reviewFirstLoad < 2)
                {
                    ConInstance.reviewFirstLoad += 1;
                }
                int LoadStatus = ConInstance.reviewFirstLoad;
                if (LoadStatus == 1)
                {
                    LocalSetup.LocalMain(this);
                    LocalCleaning.LocalMain();
                    LocalLoad.LocalMain();
                }
            }
            else if (ConInstance.review.Visible == false && ConInstance.reviewFirstLoad == 1 && FirstLoadStatus == 1)
            {
                ConInstance.reviewFirstLoad += 1;
                LocalSetup.LocalMain(this);
                LocalCleaning.LocalMain();
                LocalLoad.LocalMain();
            }
        }
    }
}
