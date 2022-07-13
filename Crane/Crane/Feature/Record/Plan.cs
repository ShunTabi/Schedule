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
    public partial class Plan : UserControl
    {
        public Plan()
        {
            InitializeComponent();
        }
        //定義
        public static int FirstLoadStatus = ConInstance.planFirstLoad;
        public static int execCode = 0;
        public static string ID = "0";
        public static Panel pa1 = new Panel();
        public static Panel pa2 = new Panel();
        public static Panel pa3 = new Panel();
        public static Panel pa4 = new Panel();
        public static Panel pa5 = new Panel();
        public static TextBox tb1 = new TextBox();
        public static TextBox tb2 = new TextBox();
        public static Button btn1 = new Button();
        public static ComboBox cb1 = new ComboBox();
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
                string[] lbs = new string[] { "目標", "計画" };
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
                FunCom.AddTextbox(tb2, 5, 2, pa1, new int[] { 180, 10 });
                tb2.Location = new Point(100, 85);
                FunCom.AddButton(btn1, 5, 3, pa1, new int[] { 90, 50 });
                btn1.Location = new Point(100, 155);
                btn1.BackColor = Color.MediumOrchid;
                btn1.Click += (sender, e) =>
                {
                    if (cb1.Text == "" || tb2.Text == "")
                    {
                        FunMSG.ErrMsg(ConMSG.CheckMSG.message00001);
                    }
                    else
                    {
                        if (execCode == 0)
                        {
                            FunSQL.SQLDML("SQL0210", ConSQL.PlanSQL.SQL0210, new string[] { "@GOALID", "@PLANNAME" }, new string[] { cb1.SelectedValue.ToString(), tb2.Text });
                        }
                        else if (execCode == 1)
                        {
                            FunSQL.SQLDML("SQL0220", ConSQL.PlanSQL.SQL0220, new string[] { "@GOALID", "@PLANNAME", "@PLANID" }, new string[] { cb1.SelectedValue.ToString(), tb2.Text, ID });
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
                FunCom.AddDataGridViewColumns(dg, new string[] { "ID", "目標", "計画" });
                FunCom.AddContextMenuStrip(dg, ConCom.defaultBtnNames2, new EventHandler[]
                {
                    (sender, e) =>
                    {//新規
                        ID="0";
                        execCode = 0;
                        btn1.Text = ConCom.defaultBtnNames[execCode];
                    },
                    (sender,e)=>
                    {//修正
                        if(dg.SelectedRows.Count == 0){ FunMSG.ErrMsg(ConMSG.CheckMSG.message00007); return; }
                        ID = dg.SelectedRows[0].Cells[0].Value.ToString();
                        SQLiteDataReader reader = FunSQL.SQLSELECT("SQL0201", ConSQL.PlanSQL.SQL0201, new string[] { "@PLANID" }, new string[] { ID });
                        while (reader.Read())
                        {
                            cb1.Text = (string)reader["GOALNAME"];
                            tb2.Text = (string)reader["PLANNAME"];
                        }
                        execCode = 1;
                        btn1.Text = ConCom.defaultBtnNames[execCode];
                    },
                    (sender,e)=>
                    {//収納箱へ
                        if(dg.SelectedRows.Count == 0){ FunMSG.ErrMsg(ConMSG.CheckMSG.message00007); return; }
                        ID = dg.SelectedRows[0].Cells[0].Value.ToString();
                        FunSQL.SQLDML("SQL0221", ConSQL.PlanSQL.SQL0221, new string[] { "@VISIBLESTATUS","@PLANID" }, new string[] { "2",ID });
                        LocalCleaning.LocalMain();
                        LocalLoad.LocalMain();
                    },
                    (sender,e)=>
                    {//ゴミ箱へ
                        if(dg.SelectedRows.Count == 0){ FunMSG.ErrMsg(ConMSG.CheckMSG.message00007); return; }
                        ID = dg.SelectedRows[0].Cells[0].Value.ToString();
                        FunSQL.SQLDML("SQL0221", ConSQL.PlanSQL.SQL0221, new string[] { "@VISIBLESTATUS","@PLANID" }, new string[] { "1",ID });
                        LocalCleaning.LocalMain();
                        LocalLoad.LocalMain();
                    }
                });
                FunCom.AddPanel(pa3, 1, pa2, new int[] { 0, 50 });
                Label lb1 = new Label();
                FunCom.AddLabel(lb1, 5, pa3);
                lb1.Text = "計画";
                lb1.Location = new Point(0, 0);
                lb1.Font = new Font("Yu mincho", 10, FontStyle.Regular);
                FunCom.AddTextbox(tb1, 5, 1, pa3, new int[] { 180, 10 });
                tb1.Location = new Point(52, 0);
                tb1.TextChanged += (sender, e) =>
                {
                    LocalLoad.DataLocalLoad();
                };
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
        public class LocalCleaning
        {
            public static void LocalMain()
            {
                tb2.Text = "";
                ID = "0";
                execCode = 0;
                btn1.Text = ConCom.defaultBtnNames[execCode];
            }
        }
        class LocalLoad
        {
            public static void DataLocalLoad()
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("%");
                sb.Append(tb1.Text);
                sb.Append("%");
                SQLiteDataReader reader = FunSQL.SQLSELECT("SQL0200", ConSQL.PlanSQL.SQL0200, new string[] { "@PLANNAME" }, new string[] { sb.ToString() });
                dg.Rows.Clear();
                while (reader.Read())
                {
                    dg.Rows.Add(
                        ((Int64)reader["PLANID"]).ToString(),
                        (string)reader["GOALNAME"],
                        (string)reader["PLANNAME"]
                        );
                }
            }
            public static void Cmdload()
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
            }
            public static void LocalMain()
            {
                DataLocalLoad();
                Cmdload();
            }
        }
        private void Plan_VisibleChanged(object sender, EventArgs e)
        {
            if (ConInstance.plan.Visible == true)
            {
                if (ConInstance.planFirstLoad < 2)
                {
                    ConInstance.planFirstLoad += 1;
                }
                int LoadStatus = ConInstance.planFirstLoad;
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
            else if (ConInstance.plan.Visible == false && ConInstance.planFirstLoad == 1 && FirstLoadStatus == 1)
            {
                ConInstance.planFirstLoad += 1;
                LocalSetup.LocalMain(this);
                //LocalStartup.LocalMain();
                LocalCleaning.LocalMain();
                LocalLoad.LocalMain();
            }
        }
    }
}
