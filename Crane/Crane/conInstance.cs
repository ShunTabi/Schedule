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
        public static Form scheduleForm;
        public static ScheduleDaily scheduleDaily;
        public static ScheduleWeekly scheduleWeekly;
        public static ScheduleList scheduleList;
        public static Schedule schedule;
        public static Genre genre;
        public static Goal goal;
        public static Plan plan;
        public static Work work;
        public static Record record;
        public static Impexp impexp;
        public static Analysis analysis;
        public static Review review;
        public static Bin bin;
        public static Setting setting;
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
        public static int analysisFirstLoad = int.Parse(string.Format("{0}", FunINI.GetString(ConFILE.iniDefault, "[Setting]", "FirstLoadCode")[10]));
        public static int reviewFirstLoad = int.Parse(string.Format("{0}", FunINI.GetString(ConFILE.iniDefault, "[Setting]", "FirstLoadCode")[12]));
        public static int binFirstLoad = int.Parse(string.Format("{0}", FunINI.GetString(ConFILE.iniDefault, "[Setting]", "FirstLoadCode")[13]));
        public static int settingFirstLoad = int.Parse(string.Format("{0}", FunINI.GetString(ConFILE.iniDefault, "[Setting]", "FirstLoadCode")[14]));
    }
}
