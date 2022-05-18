using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crane
{
    class conFILE
    {
        public static string iniDefault = @".\resources\ini\default.ini";
        public static string msgLog = @funINI.getString(iniDefault, "[log]", "msgLogFolder", 0)[0] + "msgLog" + funDate.getToday(0) + ".log";
        public static string sqlLog = @funINI.getString(iniDefault, "[db]", "sqlLogFolder", 0)[0] + "sqlLog" + funDate.getToday(0) + ".log";
    }
}
