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
        public static ScheduleDaily schedulerDaily = new ScheduleDaily();
        public static ScheduleWeekly schedulerWeekly = new ScheduleWeekly();
        public static ScheduleList schedulerList = new ScheduleList();
        public static Scheduler scheduler = new Scheduler();
        public static Genre genre = new Genre();
        public static Goal goal = new Goal();
        public static Plan plan = new Plan();
        public static Work work = new Work();
        public static Record record = new Record();
        public static impexp impexp = new impexp();
        public static Statistics statistics = new Statistics();
        public static Review review = new Review();
        public static Bin bin = new Bin();
        public static Setting setting = new Setting();
        //
        public static int scheduleDailyFirstLoad = 0;
        public static int scheduleWeeklyFirstLoad = 0;
        public static int scheduleListFirstLoad = 0;
        public static int scheduleFirstLoad = 0;
        public static int genreFirstLoad = 0;
        public static int goalFirstLoad = 0;
        public static int planFirstLoad = 0;
        public static int workFirstLoad = 0;
        public static int recordFirstLoad = 0;
        public static int impexpFirstLoad = 0;
        public static int statisticsFirstLoad = 0;
        public static int reviewFirstLoad = 0;
        public static int binFirstLoad = 0;
        public static int settingFirstLoad = 0;
    }
}
