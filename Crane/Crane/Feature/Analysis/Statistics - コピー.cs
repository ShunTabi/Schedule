using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Data.SQLite;

namespace Crane
{
    public partial class Statistics : UserControl
    {
        public Statistics()
        {
            InitializeComponent();
        }
        //定義
        public static Panel p1 = new Panel();
        public static Panel p2 = new Panel();
        public static Chart ch = new Chart();
        public static int mode1 = 0;
        public static int mode2 = 0;
        class LocalSetup
        {
            private static void Common(UserControl uc)
            {
                FunCom.AddPanel(p2, 0, uc, new int[] { 0,0 });
                FunCom.AddPanel(p1, 1, uc, new int[] { 0,100});
            }
            private static void Common2()
            {
                p2.Controls.Add(ch);
                ch.Dock = DockStyle.Fill;
                ch.BackColor = Color.AliceBlue;
            }
            public static void LocalMain(UserControl uc)
            {
                Common(uc);
                Common2();
            }
        }
        class LocalSatrtup { }
        class LocalCleaning { }
        class LocalLoad
        {
            private static void DataLoad()
            {
                ch.Series.Clear();
                ch.Series.Add("test");
                string[] legends = new string[] {};
                if(mode1 == 1)
                {
                    SQLiteDataReader reader = FunSQL.SQLSELECT("SQL0502", ConSQL.Statistics.SQL0502, new string[] { }, new string[] { });
                    while (reader.Read())
                    {
                        Array.Resize(ref legends, legends.Length + 1);
                        legends[legends.Length - 1] = (string)reader["GOALNAME"];
                    }
                }
                for (int i= 0; i < legends.Length; i++)
                {
                    ch.Series.Add(legends[i]);
                    string[] x_label = new string[] {"1","2","aaa"};
                    long[] y_label = new long[] {11,2};
                    if (mode2 == 0)
                    {
                        SQLiteDataReader reader = FunSQL.SQLSELECT("SQL0501", ConSQL.Statistics.SQL0501, new string[] { }, new string[] { });
                        while (reader.Read())
                        {
                            Array.Resize(ref x_label, x_label.Length + 1);
                            x_label[x_label.Length - 1] = (string)reader["MONTH"];
                        }
                        reader = FunSQL.SQLSELECT("SQL0500", ConSQL.Statistics.SQL0500, new string[] { "@GENRENAME" }, new string[] { legends[i] });
                        while (reader.Read())
                        {
                            Array.Resize(ref y_label, y_label.Length + 1);
                            y_label[y_label.Length - 1] = (Int64)reader["SEC"];
                        }
                    }
                    if (mode1 == 0)
                    {
                        ch.Series[legends[i]].ChartType = SeriesChartType.StackedColumn;
                    }
                    else if (mode1 == 1)
                    {//折れ線グラフ
                        ch.Series[legends[i]].ChartType = SeriesChartType.Line;
                        ch.Series[legends[i]].MarkerStyle = MarkerStyle.Circle;
                    }
                    for (int j = 0; j < y_label.Length; j++)
                    {
                        ch.Series[legends[i]].Points.AddXY(x_label[y_label.Length - 1 - j], y_label[y_label.Length - 1 - j]);
                    }
                }
            }
            public static void LocalMain()
            {
                DataLoad();
            }
        }
        private void Statistics_VisibleChanged(object sender, EventArgs e)
        {
            int loadStatus = ConInstance.statisticsFirstLoad;
            if (loadStatus == 1)
            {
                ConInstance.statisticsFirstLoad = 2;
                LocalSetup.LocalMain(this);
                LocalLoad.LocalMain();
            }
            else if (loadStatus == 2)
            {
                if (Visible == false) { return; }
                else
                {
                    LocalLoad.LocalMain();
                }
            }
            else if (loadStatus == 0)
            {
                ConInstance.statisticsFirstLoad = 1;
            }
        }
    }
}
