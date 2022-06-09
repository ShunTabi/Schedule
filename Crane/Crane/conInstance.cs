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
    class ConInstance
    {
        public static ScheduleDaily schedulerDaily;
        public static ScheduleWeekly schedulerWeekly;
        public static ScheduleList schedulerList;
        public static Schedule schedule;
        public static Genre genre;
        public static Goal goal;
        public static Plan plan;
        public static Work work;
        public static Record record;
        public static Impexp impexp;
        public static Statistics statistics;
        public static Review review;
        public static Bin bin;
        public static Setting setting;

        //public static ScheduleDaily schedulerDaily = new ScheduleDaily();
        //public static ScheduleWeekly schedulerWeekly = new ScheduleWeekly();
        //public static ScheduleList schedulerList = new ScheduleList();
        //public static Schedule schedule = new Schedule();
        //public static Genre genre = new Genre();
        //public static Goal goal = new Goal();
        //public static Plan plan = new Plan();
        //public static Work work = new Work();
        //public static Record record = new Record();
        //public static impexp impexp = new impexp();
        //public static Statistics statistics = new Statistics();
        //public static Review review = new Review();
        //public static Bin bin = new Bin();
        //public static Setting setting = new Setting();
        //
        public static int scheduleDailyFirstLoad = int.Parse(string.Format("{0}", FunINI.GetString(ConFILE.iniDefault, "[Setting]", "FirstLoadCode")[0]));
        public static int scheduleWeeklyFirstLoad = int.Parse(string.Format("{0}", FunINI.GetString(ConFILE.iniDefault, "[Setting]", "FirstLoadCode")[1]));
        public static int scheduleListFirstLoad = int.Parse(string.Format("{0}", FunINI.GetString(ConFILE.iniDefault, "[Setting]", "FirstLoadCode")[2]));
        public static int scheduleFirstLoad = int.Parse(string.Format("{0}", FunINI.GetString(ConFILE.iniDefault, "[Setting]", "FirstLoadCode")[3]));
        public static int genreFirstLoad = int.Parse(string.Format("{0}", FunINI.GetString(ConFILE.iniDefault, "[Setting]", "FirstLoadCode")[4]));
        public static int goalFirstLoad = int.Parse(string.Format("{0}", FunINI.GetString(ConFILE.iniDefault, "[Setting]", "FirstLoadCode")[5]));
        public static int planFirstLoad = int.Parse(string.Format("{0}", FunINI.GetString(ConFILE.iniDefault, "[Setting]", "FirstLoadCode")[6]));
        public static int workFirstLoad = int.Parse(string.Format("{0}", FunINI.GetString(ConFILE.iniDefault, "[Setting]", "FirstLoadCode")[7]));
        public static int recordFirstLoad = int.Parse(string.Format("{0}", FunINI.GetString(ConFILE.iniDefault, "[Setting]", "FirstLoadCode")[8]));
        public static int impexpFirstLoad = int.Parse(string.Format("{0}", FunINI.GetString(ConFILE.iniDefault, "[Setting]", "FirstLoadCode")[9]));
        public static int statisticsFirstLoad = int.Parse(string.Format("{0}", FunINI.GetString(ConFILE.iniDefault, "[Setting]", "FirstLoadCode")[10]));
        public static int reviewFirstLoad = int.Parse(string.Format("{0}", FunINI.GetString(ConFILE.iniDefault, "[Setting]", "FirstLoadCode")[11]));
        public static int binFirstLoad = int.Parse(string.Format("{0}", FunINI.GetString(ConFILE.iniDefault, "[Setting]", "FirstLoadCode")[12]));
        public static int settingFirstLoad = int.Parse(string.Format("{0}", FunINI.GetString(ConFILE.iniDefault, "[Setting]", "FirstLoadCode")[13]));
    }
}
