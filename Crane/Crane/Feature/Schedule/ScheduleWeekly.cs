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
    public partial class ScheduleWeekly : UserControl
    {
        public ScheduleWeekly()
        {
            InitializeComponent();
        }
        //定義
        public static int FirstLoadStatus = ConInstance.scheduleWeeklyFirstLoad;
        public static string ID = "0";
        public static int y = 0;
        public static int days = 0;
        public static Panel pa1 = new Panel();
        public static Panel pa2 = new Panel();
        public static Panel pa3 = new Panel();
        public static Panel[] ps = new Panel[] { };
        public static TextBox tb1 = new TextBox();
        class LocalSetup
        {
            private static void Common(UserControl uc)
            {
                days = int.Parse(string.Format("{0}", FunINI.GetString(ConFILE.iniDefault, "[Schedule]", "scheduleWeekDays")));
                uc.EnabledChanged += (sender, e) => { if (uc.Enabled == false) { return; } else { LocalLoad.LocalMain(); } };
                ConInstance.scheduleForm.Visible = false;
                FunCom.AddPanel(pa3, 0, uc, new int[] { 0, 0 });
                FunCom.AddPanel(pa2, 1, uc, new int[] { 0, 30 });
                FunCom.AddPanel(pa1, 1, uc, new int[] { 0, 70 });
                pa3.AutoScroll = true;
                Label l1 = new Label();
                FunCom.AddLabel(l1, 5, pa1);
                l1.Location = new Point(15, 15);
                l1.Font = new Font("Yu mincho", 10, FontStyle.Regular);
                l1.Text = "日付";
                FunCom.AddTextbox(tb1, 5, 1, pa1, new int[] { 160, 10 });
                tb1.Location = new Point(100, 15);
                tb1.Text = FunDate.getToday(0, 0);
                tb1.TextChanged += (sender, e) =>
                {
                    LocalLoad.LocalMain();
                };
            }
            public static void LocalMain(UserControl uc)
            {
                Common(uc);
            }
        }
        class LocalStartup
        {
            public static void LocalMain()
            {
            }
        }
        class LocalCleaning { }
        class LocalLoad
        {
            private static void LbLoadload()
            {
                for (int i = pa3.Controls.Count - 1; i >= 0; i--)
                {
                    pa3.Controls[i].Dispose();
                }
                for (int i = pa2.Controls.Count - 1; i >= 0; i--)
                {
                    pa2.Controls[i].Dispose();
                }
                for (int i = 0; i < days; i++)
                {
                    Label l1 = new Label();
                    Action<Label, int, Control> process2 = FunCom.AddLabel;
                    process2(l1, 5, pa2);
                    l1.Text = DateTime.Parse(tb1.Text).AddDays(i - days / 2).ToString("yyyy-MM-dd");
                    l1.Location = new Point(y * i+70, 0);
                    l1.Font = new Font("Segoe Print", 8, FontStyle.Regular);
                    SQLiteDataReader reader = FunSQL.SQLSELECT("SQL0400", ConSQL.ScheduleSQL.SQL0400, new string[] { "@SCHEDULEDATE" }, new string[] { DateTime.Parse(tb1.Text).AddDays(i - days / 2).ToString(l1.Text) });
                    while (reader.Read())
                    {
                        Panel p = new Panel();
                        Array.Resize(ref ps, ps.Length + 1);
                        ps[ps.Length - 1] = p;
                        FunCom.AddPanel(p, 5, pa3, new int[] { y*85/100, FunDate.getInt(((DateTime)reader["SCHEDULEENDTIME"]).ToString("HH:mm")) - FunDate.getInt(((DateTime)reader["SCHEDULESTARTTIME"]).ToString("HH:mm"))+1 });
                        p.TabIndex = int.Parse(String.Format("{0}", ((Int64)reader["SCHEDULEID"]).ToString()));
                        p.Location = new Point(y * i + 70, 45 + FunDate.getInt(((DateTime)reader["SCHEDULESTARTTIME"]).ToString("HH:mm")));
                        p.BackColor = ConSchedule.statusColors[(Int64)reader["STATUSID"] - 1];
                        FunCom.AddContextMenuStrip(p, ConCom.defaultBtnNames, new EventHandler[]
                        {
                            (sender, e) =>
                            {//新規
                                ConSchedule.execCode = 0;
                                ConSchedule.ID = "0";
                                ConInstance.scheduleForm.Visible = true;
                                ConInstance.schedule.Enabled = false;
                            },
                            (sender, e) =>
                            {//修正
                                ConSchedule.execCode = 1;
                                ConSchedule.ID = p.TabIndex.ToString();
                                ConInstance.scheduleForm.Visible = true;
                                ConInstance.schedule.Enabled = false;
                            },
                            (sender, e) =>
                            {//削除
                                ID = p.TabIndex.ToString();
                                FunSQL.SQLDML("SQL0421", ConSQL.ScheduleSQL.SQL0421, new string[] { "@VISIBLESTATUS","@SCHEDULEID" }, new string[] { "1",ID });
                                LocalMain();
                            }
                        });
                        Label l3 = new Label();
                        FunCom.AddLabel(l3, 5, p);
                        StringBuilder sb2 = new StringBuilder();
                        sb2.Append("-----------------------------------\n");
                        sb2.Append("【");
                        sb2.Append((string)reader["STATUSSUBNAME"]);
                        sb2.Append("】");
                        sb2.Append((string)reader["GOALNAME"]);
                        sb2.Append("\n-----------------------------------\n");
                        sb2.Append((string)reader["PRIORSUBNAME"]);
                        sb2.Append((string)reader["PLANNAME"]);
                        sb2.Append("(");
                        sb2.Append((string)reader["WORKNAME"]);
                        sb2.Append(")");
                        sb2.Append("");
                        l3.Text = sb2.ToString();
                        l3.Font = new Font("Yu mincho", 8, FontStyle.Regular);
                    }
                }
                for (int i = 0; i < 25; i++)
                {
                    Label l2 = new Label();
                    FunCom.AddLabel(l2, 5, pa3);
                    l2.Location = new Point(0, 30 + i * 200);
                    StringBuilder sb1 = new StringBuilder();
                    sb1.Append("");
                    sb1.Append(i.ToString("00"));
                    sb1.Append(":00 - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -");
                    l2.Text = sb1.ToString();
                    l2.Font = new Font("Segoe Print", 8, FontStyle.Regular);
                }
            }
            public static void LocalMain()
            {
                if (Regex.IsMatch(tb1.Text, @"^[0-9]{4}-(0[1-9]|1[0-2])-(0[1-9]|[12][0-9]|3[01])$"))
                {
                    y = (pa2.Width - 70) /days;
                    LbLoadload();
                }
            }
        }
        private void ScheduleWeekly_VisibleChanged(object sender, EventArgs e)
        {
            if (ConInstance.scheduleWeekly.Visible == true)
            {
                if (ConInstance.scheduleWeeklyFirstLoad < 2)
                {
                    ConInstance.scheduleWeeklyFirstLoad += 1;
                }
                int LoadStatus = ConInstance.scheduleWeeklyFirstLoad;
                if (LoadStatus == 1)
                {
                    LocalSetup.LocalMain(this);
                    LocalStartup.LocalMain();
                    LocalLoad.LocalMain();
                }
                else if (LoadStatus == 2)
                {
                    LocalLoad.LocalMain();
                }
            }
            else if (ConInstance.scheduleWeekly.Visible == false && ConInstance.scheduleWeeklyFirstLoad == 1 && FirstLoadStatus == 1)
            {
                ConInstance.scheduleWeeklyFirstLoad += 1;
                LocalSetup.LocalMain(this);
                LocalStartup.LocalMain();
                LocalLoad.LocalMain();
            }
        }
    }
}
