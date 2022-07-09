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
    public partial class Bin : UserControl
    {
        public Bin()
        {
            InitializeComponent();
        }
        //定義
        public static int FirstLoadStatus = ConInstance.binFirstLoad;
        public static string ID = "0";
        public static Panel pa1 = new Panel();
        public static Panel pa2 = new Panel();
        public static Panel pa3 = new Panel();
        public static DataGridView dg = new DataGridView();
        class LocalSetup
        {
            public static void Common(UserControl uc)
            {
                FunCom.AddPanel(pa3, 0, uc, new int[] { 0, 0 });
                FunCom.AddPanel(pa2, 1, uc, new int[] { 0, 70 });
                FunCom.AddPanel(pa1, 1, uc, new int[] { 0, 50 });
                pa2.BackColor = Color.Plum;
                pa2.Padding = new Padding(10, 10, 10, 10);
                pa3.BackColor = Color.Plum;
                pa3.Padding = new Padding(10, 10, 10, 10);
            }
            private static void Bottom()
            {
                Label l1 = new Label();
                FunCom.AddLabel(l1, 5, pa2);
                l1.Location = new Point(0, 10);
                l1.Text = "RECYCLEBIN";
                l1.Font = new Font("Segoe Print", 20, FontStyle.Regular);
                FunCom.AddDataGridView(dg, 0, pa3, new int[] { 0, 0 });
                dg.BackgroundColor = Color.Plum;
                FunCom.AddContextMenuStrip(dg, new string[] { "復元", "完全削除" }, new EventHandler[]
                {
                    (sender, e) =>
                    {//修正
                        if(dg.SelectedRows.Count == 0){ FunMSG.ErrMsg(ConMSG.CheckMSG.message00007); return; }
                        for (int i = 0;i< dg.SelectedRows.Count;i++)
                        {
                            string ID = dg.SelectedRows[i].Cells[0].Value.ToString();
                            string key = dg.SelectedRows[i].Cells[1].Value.ToString();
                            if (key == "GENRE")
                            {
                                FunSQL.SQLDML("SQL0021", ConSQL.GenreSQL.SQL0021, new string[] { "@VISIBLESTATUS","@GENREID" }, new string[] { "0",ID});
                            }
                            else if (key == "GOAL")
                            {
                                FunSQL.SQLDML("SQL0121", ConSQL.GoalSQL.SQL0121, new string[] { "@VISIBLESTATUS","@GOALID" }, new string[] { "0",ID});
                            }
                            else if(key == "PLAN")
                            {
                                FunSQL.SQLDML("SQL0221", ConSQL.PlanSQL.SQL0221, new string[] { "@VISIBLESTATUS","@PLANID" }, new string[] { "0",ID});
                            }
                            else if(key == "WORK")
                            {
                                FunSQL.SQLDML("SQL0321", ConSQL.WorkSQL.SQL0321, new string[] { "@VISIBLESTATUS","@WORKID" }, new string[] { "0",ID});
                            }
                            else if(key == "SCHEDULE")
                            {
                                FunSQL.SQLDML("SQL0421", ConSQL.ScheduleSQL.SQL0421, new string[] { "@VISIBLESTATUS","@SCHEDULEID" }, new string[] { "0",ID});
                            }
                            else if(key == "REVIEW")
                            {
                                FunSQL.SQLDML("SQL0621", ConSQL.ReviewSQL.SQL0621, new string[] { "@VISIBLESTATUS","@REVIEWID" }, new string[] { "0",ID});
                            }
                        }
                        LocalLoad.LocalMain();
                    },
                    (sender, e) =>
                    {//削除
                        if(dg.SelectedRows.Count == 0){ FunMSG.ErrMsg(ConMSG.CheckMSG.message00007); return; }
                        DialogResult result = MessageBox.Show(ConMSG.CheckMSG.message00005,"確認",MessageBoxButtons.OKCancel,MessageBoxIcon.Exclamation,MessageBoxDefaultButton.Button2);
                        if(result == DialogResult.OK)
                        {
                            for (int i = 0;i< dg.SelectedRows.Count; i++)
                            {
                                string ID = dg.SelectedRows[i].Cells[0].Value.ToString();
                                string key = dg.SelectedRows[i].Cells[1].Value.ToString();
                                if (key == "GENRE")
                                {
                                    FunSQL.SQLDML("SQL0030", ConSQL.GenreSQL.SQL0030, new string[] { "@GENREID" }, new string[] { ID});
                                }
                                else if (key == "GOAL")
                                {
                                    FunSQL.SQLDML("SQL0130", ConSQL.GoalSQL.SQL0130, new string[] { "@GOALID" }, new string[] { ID});
                                }
                                else if(key == "PLAN")
                                {
                                    FunSQL.SQLDML("SQL0230", ConSQL.PlanSQL.SQL0230, new string[] { "@PLANID" }, new string[] { ID});
                                }
                                else if(key == "WORK")
                                {
                                    FunSQL.SQLDML("SQL0330", ConSQL.WorkSQL.SQL0330, new string[] { "@WORKID" }, new string[] { ID});
                                }
                                else if(key == "SCHEDULE")
                                {
                                    FunSQL.SQLDML("SQL0430", ConSQL.ScheduleSQL.SQL0430, new string[] { "@SCHEDULEID" }, new string[] { ID});
                                }
                                else if(key == "REVIEW")
                                {
                                    FunSQL.SQLDML("SQL0630", ConSQL.ReviewSQL.SQL0630, new string[] { "@REVIEWID" }, new string[] { ID});
                                }
                            }
                        }
                        LocalLoad.LocalMain();
                    }
                });
                FunCom.AddDataGridViewColumns(dg, new string[] { "ID", "キー", "内容", "更新日" });
            }
            public static void LocalMain(UserControl uc)
            {
                Common(uc);
                Bottom();
            }
        }
        class LocalStartup
        {
        }
        class LocalCleaning
        {
        }
        class LocalLoad
        {
            public static void DataLocalload()
            {
                SQLiteDataReader reader = FunSQL.SQLSELECT("SQL9004", ConSQL.BinSQL.SQL9004, new string[] { }, new string[] { });
                dg.Rows.Clear();
                while (reader.Read())
                {
                    dg.Rows.Add(
                        ((Int64)reader["ID"]).ToString(),
                        (string)reader["KEY"],
                        (string)reader["NAME"],
                        ((DateTime)reader["UPDATEDATE"]).ToString("yyyy-MM-dd")
                        );
                }
            }
            public static void LocalMain()
            {
                DataLocalload();
            }
        }
        private void Bin_VisibleChanged(object sender, EventArgs e)
        {
            if (ConInstance.bin.Visible == true)
            {
                if (ConInstance.binFirstLoad < 2)
                {
                    ConInstance.binFirstLoad += 1;
                }
                int LoadStatus = ConInstance.binFirstLoad;
                if (LoadStatus == 1)
                {
                    LocalSetup.LocalMain(this);
                    LocalLoad.LocalMain();
                }
                else if (LoadStatus == 2)
                {
                    LocalLoad.LocalMain();
                }
            }
            else if (ConInstance.bin.Visible == false && ConInstance.binFirstLoad == 1 && FirstLoadStatus == 1)
            {
                ConInstance.binFirstLoad += 1;
                LocalSetup.LocalMain(this);
                LocalLoad.LocalMain();
            }
        }
    }
}
