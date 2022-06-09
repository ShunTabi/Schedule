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
    public partial class Impexp : UserControl
    {
        public Impexp()
        {
            InitializeComponent();
        }
        //定義
        class LocalSetup
        {
            public static void LocalMain(UserControl uc)
            {
            }
        }
        class LocalStartup
        {
            public static void LocalMain()
            {
            }
        }
        class LocalCleaning
        {
        }
        class LocalLoad
        {
            public static void LocalMain()
            { 
            }
        }
    }
}
