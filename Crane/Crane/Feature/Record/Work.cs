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
        public static int FirstLoadStatus = ConInstance.workFirstLoad;
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
        class LocalSetup
        {
            private static void Common(UserControl uc)
            {
                FunCom.AddPanel(pa2, 0, uc, new int[] { 0, 0 });
                FunCom.AddPanel(pa1, 2, uc, new int[] { 350, 0 });
                pa2.BackColor = Color.Plum;
                pa2.Padding = new Padding(10, 10, 10, 10);
            }
            private static void LeftSide()
            {
                string[] lbs = new string[] { "目標", "計画", "作業", "優先度", "開始日", "終了日" };
                for (int i = 0; i < lbs.Length; i++)
                {
                    Label lb = new Label();
                    FunCom.AddLabel(lb, 5, pa1);
                    lb.Text = lbs[i];
                    lb.Font = new Font("Yu mincho", 10, FontStyle.Regular);
                    lb.Location = new Point(15, 15 + i * 70);
                }
                FunCom.AddCombobox(cb1, 5, 1, pa1, new int[] { 180, 10 });
                cb1.Font = new Font("Yu mincho", 10, FontStyle.Regular);
                cb1.Location = new Point(100, 15);
                cb1.SelectedIndexChanged += (sender, e) => { LocalLoad.CmdLocalLoad2(); };
                FunCom.AddCombobox(cb2, 5, 2, pa1, new int[] { 180, 10 });
                cb2.Font = new Font("Yu mincho", 10, FontStyle.Regular);
                cb2.Location = new Point(100, 85);
                FunCom.AddTextbox(tb2, 5, 3, pa1, new int[] { 180, 10 });
                tb2.Location = new Point(100, 155);
                FunCom.AddCombobox(cb3, 5, 4, pa1, new int[] { 180, 10 });
                cb3.Font = new Font("Yu mincho", 10, FontStyle.Regular);
                cb3.Location = new Point(100, 225);
                FunCom.AddTextbox(tb3, 5, 5, pa1, new int[] { 180, 10 });
                tb3.Location = new Point(100, 295);
                tb3.Text = FunDate.getToday(0, 0);
                FunCom.AddTextbox(tb4, 5, 6, pa1, new int[] { 180, 10 });
                tb4.Location = new Point(100, 365);
                tb4.Text = FunDate.getToday(0, 0);
                FunCom.AddButton(btn1, 5, 7, pa1, new int[] { 90, 50 });
                btn1.Location = new Point(100, 435);
                btn1.BackColor = Color.MediumOrchid;
                btn1.Click += (sender, e) =>
                {
                    if (cb1.Text == "" || cb2.Text == "" || tb2.Text == "" || cb3.Text == "" || tb3.Text == "" || tb4.Text == "")
                    {
                        FunMSG.ErrMsg(ConMSG.CheckMSG.message00001);
                    }
                    else if (!Regex.IsMatch(tb3.Text, @"^[0-9]{4}-(0[1-9]|1[0-2])-(0[1-9]|[12][0-9]|3[01])$") || !Regex.IsMatch(tb4.Text, @"^[0-9]{4}-(0[1-9]|1[0-2])-(0[1-9]|[12][0-9]|3[01])$"))
                    {
                        FunMSG.ErrMsg(ConMSG.CheckMSG.message00003);
                    }
                    else
                    {
                        if (execCode == 0)
                        {
                            FunSQL.SQLDML("SQL0310", ConSQL.WorkSQL.SQL0310, new string[] { "@PLANID", "@WORKNAME", "@PRIORID", "WORKSTARTDATE", "WORKENDDATE" }, new string[] { cb2.SelectedValue.ToString(), tb2.Text, cb3.SelectedValue.ToString(), tb3.Text, tb4.Text });
                        }
                        else if (execCode == 1)
                        {
                            FunSQL.SQLDML("SQL0320", ConSQL.WorkSQL.SQL0320, new string[] { "@PLANID", "@WORKNAME", "@PRIORID", "WORKSTARTDATE", "WORKENDDATE", "@WORKID" }, new string[] { cb2.SelectedValue.ToString(), tb2.Text, cb3.SelectedValue.ToString(), tb3.Text, tb4.Text, ID });
                        }
                        LocalCleaning.LocalMain();
                        LocalLoad.LocalMain();
                    }
                };
            }
            public static void RightSide()
            {
                FunCom.AddDataGridView(dg, 0, pa2, new int[] { 0, 0 });
                dg.BackgroundColor = Color.Plum;
                FunCom.AddDataGridViewColumns(dg, new string[] { "ID", "目標", "計画", "優先度/作業", "開始日", "終了日" });
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
                        SQLiteDataReader reader = FunSQL.SQLSELECT("SQL0301", ConSQL.WorkSQL.SQL0301, new string[] { "@WORKID" }, new string[] { ID });
                        while (reader.Read())
                        {
                            cb1.Text = (string)reader["GOALNAME"];
                            tb2.Text = (string)reader["WORKNAME"];
                            cb3.Text = (string)reader["PRIORNAME"];
                            tb3.Text = ((DateTime)reader["WORKSTARTDATE"]).ToString("yyyy-MM-dd");
                            tb4.Text = ((DateTime)reader["WORKENDDATE"]).ToString("yyyy-MM-dd");
                            LocalLoad.CmdLocalLoad2();cb2.Text = (string)reader["PLANNAME"];
                        }
                        execCode = 1;
                        btn1.Text = ConCom.defaultBtnNames[execCode];
                    },
                    (sender, e) =>
                    {//削除
                        if(dg.SelectedRows.Count == 0){ FunMSG.ErrMsg(ConMSG.CheckMSG.message00007); return; }
                        ID = dg.SelectedRows[0].Cells[0].Value.ToString();
                        FunSQL.SQLDML("SQL0321", ConSQL.WorkSQL.SQL0321, new string[] { "@VISIBLESTATUS","@WORKID" }, new string[] { "1",ID });
                        LocalCleaning.LocalMain();
                        LocalLoad.LocalMain();
                    }
                }
                );
                FunCom.AddPanel(pa3, 1, pa2, new int[] { 0, 50 });
                Label lb1 = new Label();
                FunCom.AddLabel(lb1, 5, pa3);
                lb1.Text = "目標/計画";
                lb1.Location = new Point(0, 0);
                lb1.Font = new Font("Yu mincho", 10, FontStyle.Regular);
                FunCom.AddTextbox(tb1, 5, 1, pa3, new int[] { 180, 10 });
                tb1.Location = new Point(101, 0);
                tb1.TextChanged += (sender, e) => { LocalLoad.DataLocalLoad(); };
            }
            public static void LocalMain(UserControl uc)
            {
                Common(uc);
                LeftSide();
                RightSide();
            }
        }
        //class LocalStartup
        //{
        //    public static void LocalMain()
        //    {

        //    }
        //}
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
                SQLiteDataReader reader = FunSQL.SQLSELECT("SQL0300", ConSQL.WorkSQL.SQL0300, new string[] { "@KEYWORD" }, new string[] { sb.ToString() });
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
                reader = FunSQL.SQLSELECT("SQL9001", ConSQL.PriorSQL.SQL9001, new string[] { }, new string[] { });
                while (reader.Read())
                {
                    Array.Resize(ref keys, keys.Length + 1);
                    keys[keys.Length - 1] = (Int64)reader["PRIORID"];
                    Array.Resize(ref values, values.Length + 1);
                    values[values.Length - 1] = (string)reader["PRIORNAME"];
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

        private void Work_VisibleChanged(object sender, EventArgs e)
        {
            if (ConInstance.work.Visible == true)
            {
                if (ConInstance.workFirstLoad < 2)
                {
                    ConInstance.workFirstLoad += 1;
                }
                int LoadStatus = ConInstance.workFirstLoad;
                if (LoadStatus == 1)
                {
                    LocalSetup.LocalMain(this);
                    //LocalStartup.LocalMain();
                    LocalCleaning.LocalMain();
                    LocalLoad.LocalMain();
                }
                else if (LoadStatus == 2)
                {
                    LocalCleaning.LocalMain();
                    LocalLoad.LocalMain();
                }
            }
            else if (ConInstance.work.Visible == false && ConInstance.workFirstLoad == 1 && FirstLoadStatus == 1)
            {
                ConInstance.workFirstLoad += 1;
                LocalSetup.LocalMain(this);
                //LocalStartup.LocalMain();
                LocalCleaning.LocalMain();
                LocalLoad.LocalMain();
            }
        }
    }
}
