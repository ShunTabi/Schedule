using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crane
{
    class ConFILE
    {
        public static string iniDefault = @".\resources\ini\default.ini";
        public static string msgLog = FunINI.getString(iniDefault, "[log]", "MSGLogFolder")[0] + FunDate.getToday(0,0) + "msgLog" + ".log";
        public static string sqlLog = FunINI.getString(iniDefault, "[db]", "SQLLogFolder")[0] + FunDate.getToday(0,0) + "sqlLog" + ".log";
    }
}
