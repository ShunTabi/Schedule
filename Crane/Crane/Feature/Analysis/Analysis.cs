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
using System.Text.RegularExpressions;

namespace Crane
{
    public partial class Analysis : UserControl
    {
        public Analysis()
        {
            InitializeComponent();
        }
        //定義
        public static int FirstLoadStatus = ConInstance.analysisFirstLoad;
        public static Panel p1 = new Panel();
        public static Panel p2 = new Panel();
        public static Chart ch = new Chart();
        public static ComboBox cb1 = new ComboBox();
        public static ComboBox cb2 = new ComboBox();
        public static TextBox tb1 = new TextBox();
        public static TextBox tb2 = new TextBox();
        public static int mode = 0;
        class LocalSetup
        {
            private static void Common(UserControl uc)
            {
                FunCom.AddChart(ch, 0, uc, new int[] { 0, 0 });
                FunCom.AddPanel(p1, 1, uc, new int[] { 0, 100 });
            }
            private static void Common2()
            {
                FunCom.AddCombobox(cb1, 5, 1, p1, new int[] { 180, 10 });
                cb1.Font = new Font("Yu mincho", 10, FontStyle.Regular);
                cb1.Location = new Point(0, 50);
                FunCom.AddComboboxItem(cb1, new long[] { 0, 1 }, new string[] { "棒グラフ", "折れ線グラフ" });
                cb1.SelectedIndex = int.Parse(string.Format("{0}", FunINI.GetString(ConFILE.iniDefault, "[Analysis]", "type")));
                FunCom.AddCombobox(cb2, 5, 2, p1, new int[] { 180, 10 });
                cb2.Font = new Font("Yu mincho", 10, FontStyle.Regular);
                cb2.Location = new Point(190, 50);
                FunCom.AddTextbox(tb1, 5, 3, p1, new int[] { 180, 10 });
                tb1.Location = new Point(380, 50);
                tb1.Text = FunDate.getToday(3, int.Parse(string.Format("{0}", FunINI.GetString(ConFILE.iniDefault, "[Analysis]", "from"))));
                FunCom.AddTextbox(tb2, 5, 4, p1, new int[] { 180, 10 });
                tb2.Location = new Point(570, 50);
                tb2.Text = FunDate.getToday(3, int.Parse(string.Format("{0}", FunINI.GetString(ConFILE.iniDefault, "[Analysis]", "to"))));
            }
            private static void Common3()
            {
                ch.Click += (sender, e) => { LocalLoad.LocalMain(); };
                cb1.SelectedIndexChanged += (sender, e) => { mode = 1; };
                cb2.SelectedIndexChanged += (sender, e) => { mode = 1; };
                tb1.TextChanged += (sender, e) => { mode = 1; };
                tb2.TextChanged += (sender, e) => { mode = 1; };
                mode = 1;
            }
            public static void LocalMain(UserControl uc)
            {
                Common(uc);
                Common2();
                Common3();
            }
        }
        class LocalSatrtup
        {
            private static void comboload()
            {
                long[] keys = new long[] { 0 };
                string[] values = new string[] { "ALL" };
                SQLiteDataReader reader = FunSQL.SQLSELECT("SQL0504", ConSQL.AnalysisSQL.SQL0504, new string[] { }, new string[] { });
                while (reader.Read())
                {
                    Array.Resize(ref keys, keys.Length + 1);
                    keys[keys.Length - 1] = (Int64)reader["GOALID"];
                    Array.Resize(ref values, values.Length + 1);
                    values[values.Length - 1] = (string)reader["GOALNAME"];
                }
                FunCom.AddComboboxItem(cb2, keys, values);
                mode = 1;
            }
            public static void LocalMain()
            {
                comboload();
            }
        }
        class LocalCleaning { }
        class LocalLoad
        {
            private static void DataLoad()
            {
                if (!Regex.IsMatch(tb1.Text, @"^[0-9]{4}-(0[1-9]|1[0-2])$") || !Regex.IsMatch(tb2.Text, @"^[0-9]{4}-(0[1-9]|1[0-2])$"))
                {
                    FunMSG.ErrMsg(ConMSG.CheckMSG.message00003);
                    return;
                }
                else if (mode == 0) { return; }
                mode = 0;
                ch.Series.Clear();
                ch.Legends.Clear();
                ch.ChartAreas.Clear();
                ch.ChartAreas.Add(new ChartArea("Area"));
                string[] legends = new string[] { };
                SQLiteDataReader reader = null;
                if (int.Parse(string.Format("{0}", cb2.SelectedValue)) == 0)
                {
                    reader = FunSQL.SQLSELECT("SQL0504", ConSQL.AnalysisSQL.SQL0504, new string[] { }, new string[] { });
                }
                else
                {
                    reader = FunSQL.SQLSELECT("SQL0505", ConSQL.AnalysisSQL.SQL0505, new string[] { "@GOALID" }, new string[] { cb2.SelectedValue.ToString() });
                }
                while (reader.Read())
                {
                    Array.Resize(ref legends, legends.Length + 1);
                    legends[legends.Length - 1] = (string)reader["GOALNAME"];
                }
                if (legends.Length > 0)
                {
                    ch.Legends.Add(legends[0]);
                }
                string[] x_label = new string[] { };
                if (int.Parse(string.Format("{0}", cb2.SelectedValue)) == 0)
                {
                    reader = FunSQL.SQLSELECT("SQL0502", ConSQL.AnalysisSQL.SQL0502, new string[] { "@MONTH1", "@MONTH2" }, new string[] { tb1.Text, tb2.Text });
                }
                else
                {
                    reader = FunSQL.SQLSELECT("SQL0503", ConSQL.AnalysisSQL.SQL0503, new string[] { "@GOALID", "@MONTH1", "@MONTH2" }, new string[] { cb2.SelectedValue.ToString(), tb1.Text, tb2.Text });
                }
                while (reader.Read())
                {
                    Array.Resize(ref x_label, x_label.Length + 1);
                    x_label[x_label.Length - 1] = (string)reader["MONTH"];
                }
                for (int i = 0; i < legends.Length; i++)
                {
                    ch.Series.Add(legends[i]);
                    ch.Series[legends[i]].IsVisibleInLegend = true;
                    //ch.Series[legends[i]].IsValueShownAsLabel = true;
                    long[] y_label = new long[] { };
                    if (int.Parse(string.Format("{0}", cb2.SelectedValue)) == 0)
                    {
                        reader = FunSQL.SQLSELECT("SQL0500", ConSQL.AnalysisSQL.SQL0500, new string[] { "@GOALNAME", "@MONTH1", "@MONTH2" }, new string[] { legends[i], tb1.Text, tb2.Text });
                    }
                    else
                    {
                        reader = FunSQL.SQLSELECT("SQL0501", ConSQL.AnalysisSQL.SQL0501, new string[] { "@GOALID", "@MONTH1", "@MONTH2" }, new string[] { cb2.SelectedValue.ToString(), tb1.Text, tb2.Text });
                    }
                    while (reader.Read())
                    {
                        Array.Resize(ref y_label, y_label.Length + 1);
                        y_label[y_label.Length - 1] = (Int64)reader["MINS"];
                    }
                    if (int.Parse(string.Format("{0}", cb1.SelectedValue)) == 0)
                    {
                        ch.Series[legends[i]].ChartType = SeriesChartType.StackedColumn;
                    }
                    else if (int.Parse(String.Format("{0}", cb1.SelectedValue)) == 1)
                    {
                        ch.Series[legends[i]].ChartType = SeriesChartType.Line;
                        ch.Series[legends[i]].MarkerStyle = MarkerStyle.Circle;
                    }
                    for (int j = 0; j < y_label.Length; j++)
                    {
                        ch.Series[legends[i]].Points.AddXY(x_label[j], y_label[j]);
                    }
                }
            }
            public static void LocalMain()
            {
                DataLoad();
            }
        }
        private void Analysis_VisibleChanged(object sender, EventArgs e)
        {
            if (ConInstance.analysis.Visible == true)
            {
                if (ConInstance.analysisFirstLoad < 2)
                {
                    ConInstance.analysisFirstLoad += 1;
                }
                int LoadStatus = ConInstance.analysisFirstLoad;
                if (LoadStatus == 1)
                {
                    LocalSetup.LocalMain(this);
                    LocalSatrtup.LocalMain();
                    LocalLoad.LocalMain();
                }
                else if (LoadStatus == 2)
                {
                    LocalSatrtup.LocalMain();
                    LocalLoad.LocalMain();
                }
            }
            else if (ConInstance.analysis.Visible == false && ConInstance.analysisFirstLoad == 1 && FirstLoadStatus == 1)
            {
                ConInstance.analysisFirstLoad += 1;
                LocalSetup.LocalMain(this);
                LocalSatrtup.LocalMain();
                LocalLoad.LocalMain();
            }
        }
    }
}
