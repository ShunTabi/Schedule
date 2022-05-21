using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Data;


namespace Crane
{
    class conInstance
    {
        public static Genre genre = new Genre();
        public static Goal goal = new Goal();
        public static Plan plan = new Plan();
        public static Work work = new Work();
        public static Scheduler scheduler = new Scheduler();
        public static Review review = new Review();
        public static Record record = new Record();
        public static Setting setting = new Setting();
    }
}
