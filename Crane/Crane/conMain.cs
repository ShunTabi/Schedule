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
            "Record",
            "Schedule",
            "Review",
            "Analysis",
            "Imp/Exp",
            "Bin",
            "Setting",
        };
        public static int startupCode = int.Parse(string.Format("{0}", FunINI.GetString(ConFILE.iniDefault,"[Main]","startupCode")[0]));
        public static int recordStartupCode = int.Parse(string.Format("{0}", FunINI.GetString(ConFILE.iniDefault, "[Record]", "recordStartupCode")[0]));
        public static int scheduleStartupCode = int.Parse(string.Format("{0}", FunINI.GetString(ConFILE.iniDefault, "[Schedule]", "scheduleStartupCode")[0]));
        public static int reviewStartupCode = int.Parse(string.Format("{0}", FunINI.GetString(ConFILE.iniDefault, "[Review]", "reviewStartupCode")[0]));
        public static int binStartupCode = int.Parse(string.Format("{0}", FunINI.GetString(ConFILE.iniDefault, "[Bin]", "binStartupCode")[0]));
    }
}
