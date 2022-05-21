using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crane
{
    class conMain
    {
        public static string[] mainButton = new string[]
        {
            "Scheduler",
            "Record",
            "Review",
            "Setting"
        };
        public static int startupCode = int.Parse(string.Format("{0}", funINI.getString(conFILE.iniDefault,"[Main]","startupCode",0)[0]));
        public static int archiveStartupCode = int.Parse(string.Format("{0}", funINI.getString(conFILE.iniDefault, "[Record]", "recordStartupCode", 0)[0]));
    }
}
