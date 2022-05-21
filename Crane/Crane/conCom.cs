using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Crane
{
    class conCom
    {
        public static string appName = "Crane";
        public static string[] defaultBtnNames = new string[] { "登録", "更新", "削除" };
    }
    class conScheduler
    {
        public static int execCode = 0;
        public static string ID = "0";
        public static Color[] statusColors = new Color[] { Color.Pink,Color.Lavender,Color.DimGray,Color.DimGray };
        //INSERT INTO T_COMITEM(TABLEKIND,CODE,NAME1,NAME2) VALUES('STATUS',1,'対応中','Doing');
        //INSERT INTO T_COMITEM(TABLEKIND, CODE, NAME1, NAME2) VALUES('STATUS',2,'未','Waiting');
        //INSERT INTO T_COMITEM(TABLEKIND, CODE, NAME1, NAME2) VALUES('STATUS',3,'完了','Done');
        //INSERT INTO T_COMITEM(TABLEKIND, CODE, NAME1, NAME2) VALUES('STATUS',4,'保留','Pending');

    }
    class conSetting
    {
        public static string[] names = new string[]
        {
            "sqlLog","msgBox","msgLog","cleaning","backup","未",
            "未","未","未","未","未","未",
            "未","未","未","未","未","debug",
        };
        public static long[] keys = new long[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        public static string[][] values = new string[][]
        {
            new string[]{"OFF","ON"},//1
            new string[]{"OFF","ON"},//2
            new string[]{"OFF","ON"},//3
            new string[]{"OFF","ON"},//4
            new string[]{"OFF","ON"},//5
            new string[]{"OFF","ON"},//6
            new string[]{"OFF","ON"},//7
            new string[]{"OFF","ON"},//8
            new string[]{"OFF","ON"},//9
            new string[]{"OFF","ON"},//10
            new string[]{"OFF","ON"},//11
            new string[]{"OFF","ON"},//12
            new string[]{"OFF","ON"},//13
            new string[]{"OFF","ON"},//14
            new string[]{"OFF","ON"},//15
            new string[]{"OFF","ON"},//16
            new string[]{"OFF","ON"},//17
            new string[]{"OFF","ON"},//18
        };
        public static string[] startupSettingCodes = funINI.getString(conFILE.iniDefault, "[Setting]", "startupSettingCode", 0);
    }
}
