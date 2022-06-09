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
    public partial class Statistics : UserControl
    {
        public Statistics()
        {
            InitializeComponent();
        }
        //定義
        public static Panel p1 = new Panel();
        public static Panel p2 = new Panel();
        class LocalSetup
        {
            private static void Common(UserControl uc)
            {
                FunCom.AddPanel(p2, 0, uc, new int[] { 0,0 });
                FunCom.AddPanel(p1, 1, uc, new int[] { 0,100});

            }
            public static void LocalMain(UserControl uc)
            {
                Common(uc);
            }
        }
        class LocalSatrtup { }
        class LocalCleaning { }
        class LocalLoad
        {
            public static void LocalMain()
            {
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
