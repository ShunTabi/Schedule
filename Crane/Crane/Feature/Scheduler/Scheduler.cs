using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crane
{
    public partial class Scheduler : UserControl
    {
        public Scheduler()
        {
            InitializeComponent();
        }
        //定義
        public static int execCode = 0;
        public static string ID = "0";
        public static Panel pa1 = new Panel();
        public static Panel pa2 = new Panel();
        public static Panel pa3 = new Panel();
        public static Panel pa4 = new Panel();
        public static Panel pa5 = new Panel();
        public static Panel pa6 = new Panel();
        public static DataGridView dg = new DataGridView();
        class setup
        {
            private static void common(UserControl uc)
            {
                uc.Padding = new Padding(20, 20, 20, 20);
                funCom.addPanel(pa2, 0, uc, new int[] { 0, 0 });
                funCom.addPanel(pa1, 2, uc, new int[] { 350, 0 });
            }
            private static void leftSide()
            {
                funCom.addPanel(pa4, 0, pa1, new int[] { 0, 0 });
                funCom.addPanel(pa3, 1, pa1, new int[] { 0, 70 });
                pa4.AutoScroll = true;
                for (int i = 0; i < 25; i++)
                {
                    Label l = new Label();
                    funCom.addLabel(l, 5, pa4);
                    l.Location = new Point(10, 10 + i * 200);
                    l.Text = "---" + i.ToString("00") + ":00---";
                    l.Font = new Font("Segoe Print", 9, FontStyle.Regular);
                }
                Label l1 = new Label();
                funCom.addLabel(l1, 5, pa3);
                l1.Location = new Point(0, 20);
                l1.Font = new Font("Yu mincho", 10, FontStyle.Regular);
                l1.Text = "日付";
                TextBox tb = new TextBox();
                funCom.addTextbox(tb, 5, 0, pa3, new int[] { 160, 10 });
                tb.Location = new Point(53, 20);
                tb.Text = funToday.getToday(0);
            }
            private static void rightSide()
            {
                funCom.addDataGridView(dg, 0, pa2, new int[] { 0, 0 });
                dg.BackgroundColor = Color.Plum;
                funCom.addDataGridViewColumns(dg, new string[] { "ID","〆切FLG", "作業","優先度", "開始時間", "終了時間"});
                funCom.addPanel(pa5, 1, pa2, new int[] { 0, 100 });
                funCom.addPanel(pa6, 4, pa2, new int[] { 0, 100 });
                Label l2 = new Label();
                funCom.addLabel(l2, 5, pa5);
                l2.Location = new Point(0, 0);
                l2.Font = new Font("Segoe Print", 25, FontStyle.Regular);
                l2.Text = "TODAY";
                pa6.BackColor = Color.Plum;
                Button btn1 = new Button();
                funCom.addButton(btn1,5,1,pa6, new int[] { 90, 50 });
                btn1.Location = new Point(15, 15);
                btn1.Text = conCom.defaultBtnNames[execCode];
                btn1.BackColor = Color.MediumOrchid;
                btn1.Click += (sender, e) => clickBtn(sender, e, execCode);
            }
            private static void clickBtn(object sender, EventArgs e, int execCode)
            {
                Form todaywork = new Todaywork(execCode,ID);
                todaywork.Show();
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

            }
        }
        class load
        {
            public static void main()
            {

            }
        }
        private void Scheduler_Load(object sender, EventArgs e)
        {
            setup.main(this);
            startup.main();
            cleaning.main();
            load.main();
        }
    }
}
