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
    public partial class Review : UserControl
    {
        public Review()
        {
            InitializeComponent();
        }
        class setup
        {
            public static void main(UserControl uc)
            {
                uc.Padding = new Padding(20, 20, 20, 20);
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
        private void Review_Load(object sender, EventArgs e)
        {
            setup.main(this);
            startup.main();
            cleaning.main();
            load.main();
        }
    }
}
