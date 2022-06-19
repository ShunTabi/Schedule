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
        public static string msgLog = FunINI.GetString(iniDefault, "[log]", "MSGLogFolder")[0] + FunDate.getToday(0,0) + "err" + ".log";
        public static string sqlLog = FunINI.GetString(iniDefault, "[db]", "SQLLogFolder")[0] + FunDate.getToday(0,0) + "sql" + ".log";
        public static string debugLog = FunINI.GetString(iniDefault, "[db]", "DEBUGLogFolder")[0] + FunDate.getToday(0, 0) + "debug" + ".log";
    }
}
