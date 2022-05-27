﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crane
{
    class ConTask
    {
        public static void AppStartup()
        {
            Task.Run(() =>
            {
                if (int.Parse(string.Format("{0}", ConSetting.startupSettingCodes[4])) == 1)
                {
                    FunSetting.copyFile(FunSQL.dataSource,"_bak",int.Parse(string.Format("{0}",FunINI.getString(ConFILE.iniDefault,"[Setting]", "backupGeneration"))));
                }
                if (int.Parse(string.Format("{0}", ConSetting.startupSettingCodes[3])) == 1)
                {
                    FunSQL.SQLDML("SQL9003", ConSQL.VacuumSQL.SQL9003, new string[] { }, new string[] { });
                }
            });
        }
    }
}