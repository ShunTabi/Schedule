﻿using System;
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
    public partial class Crane : Form
    {
        public Crane()
        {
            InitializeComponent();
        }
        //定義
        public static Panel pa1 = new Panel();
        public static Panel pa2 = new Panel();
        public static Panel pa3 = new Panel();
        public static Panel pa4 = new Panel();
        public static Panel pa5 = new Panel();
        public static Panel pa6 = new Panel();
        public static Label la1 = new Label();
        public static Label la2 = new Label();
        public static Label la3 = new Label();
        public static Label la4 = new Label();
        public static Button bt1 = new Button();
        public static Button bt2 = new Button();
        public static Button bt3 = new Button();
        public static Button bt4 = new Button();
        public static Button[] btns = new Button[] { bt1, bt2, bt3, bt4 };
        public static Scheduler scheduler = new Scheduler();
        public static Review review = new Review();
        public static Archive archive = new Archive();
        public static Setting setting = new Setting();
        public static UserControl[] userControls = new UserControl[] { scheduler, archive, review, setting };
        class setup
        {
            public static void main(Form frm)
            {
                frm.Size = new Size(conCom.XY[0], conCom.XY[1]);
                frm.Text = conCom.appName;
                frm.Location = new Point(
                    int.Parse(string.Format("{0}", funINI.getString(conFILE.iniDefault, "[Main]", "Location", 0)[0])),
                    int.Parse(string.Format("{0}", funINI.getString(conFILE.iniDefault, "[Main]", "Location", 0)[1]))
                    );
                frm.FormBorderStyle = FormBorderStyle.None;
                frm.Padding = new Padding(0, 0, 0, 0);
            }
        }
        class startup
        {
            private static void main1(Form frm)
            {
                funCom.addPanel(pa3, 0, frm, new int[] { 0, 0 });
                funCom.addcontextMenuStrip(pa1, new string[] { "アプリケーションを終了" }, new EventHandler[] { click1 });
                funCom.addPanel(pa2, 1, frm, new int[] { 0, 95 });
                funCom.addLabel(la1, 5, pa2);
                la1.Text = conCom.appName;
                la1.Font = new Font("Segoe Print", 30, FontStyle.Bold);
                la1.Location = new Point(10, 10);
                funCom.addPanel(pa1, 1, frm, new int[] { 0, 30 });
                funCom.addPanel(pa4, 4, frm, new int[] { 0, 30 });
                pa1.BackColor = Color.SpringGreen;
                pa4.BackColor = Color.SpringGreen;
                funCom.addPanel(pa6, 0, pa3, new int[] { 0, 0 });
                funCom.addPanel(pa5, 2, pa3, new int[] { 130, 0 });
                for (int i = 0; i < btns.Length; i++)
                {
                    Button btn = btns[i];
                    funCom.addButton(btns[i], 5, i, pa5, new int[] { 100, 70 });
                    btn.Location = new Point(15, 85 * i + 80);
                    btn.Text = conMain.mainButton[i];
                    btn.Font = new Font("Segoe Print", 8, FontStyle.Regular);
                    btn.BackColor = Color.SpringGreen;
                    btn.Click += (sender, e) => clickBtn(sender, btn.TabIndex);
                }
                //scheduler.BackColor = Color.Yellow;
                //archive.BackColor = Color.Red;
                review.BackColor = Color.Blue;
                //setting.BackColor = Color.Black;
            }
            private static void clickBtn(object sender, int code)
            {
                for (int i = 0; i < userControls.Length; i++)
                {
                    UserControl uc = userControls[i];
                    uc.Visible = false;
                }
                userControls[code].Visible = true;
            }
            private static void click1(object sender, EventArgs e)
            {
            }
            public static void main(Form frm) { main1(frm); main2(); }
        }
        private static void main2()
        {
            for (int i = 0; i < userControls.Length; i++)
            {
                UserControl uc = userControls[i];
                funCom.addUserControl(uc, 0, pa6);
                uc.Visible = false;
            }
            userControls[conMain.startupCode].Visible = true;
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
        private void Form1_Load(object sender, EventArgs e)
        {
            setup.main(this);
            startup.main(this);
            cleaning.main();
            load.main();
        }
    }
}
