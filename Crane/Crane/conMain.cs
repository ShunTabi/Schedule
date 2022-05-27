using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crane
{
    class ConMain
    {
        public static string[] mainButton = new string[]
        {
            "Scheduler",
            "Record",
            "Review",
            "Statistic",
            "Imp/Exp",
            "Bin",
            "Setting",
        };
        public static int startupCode = int.Parse(string.Format("{0}", FunINI.getString(ConFILE.iniDefault,"[Main]","startupCode")[0]));
        public static int recordStartupCode = int.Parse(string.Format("{0}", FunINI.getString(ConFILE.iniDefault, "[Record]", "recordStartupCode")[0]));
        public static int scheduleStartupCode = int.Parse(string.Format("{0}", FunINI.getString(ConFILE.iniDefault, "[Schedule]", "scheduleStartupCode")[0]));
    }
}
