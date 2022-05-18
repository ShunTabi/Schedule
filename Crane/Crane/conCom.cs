using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crane
{
    class conCom
    {
        public static string appName = "Crane";
        public static int[] XY = new int[] { 1400, 900 };
        public static string[] defaultBtnNames = new string[] { "登録", "更新", "削除" };
    }
    class conSetting
    {
        public static string[] names = new string[]
        {
            "sqlLog","msgBox","msgLog","cleaning","未","未",
            "未","未","未","未","未","未",
            "未","未","未","未","未","debug",
        };
        public static long[] keys = new long[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        public static string[][] values = new string[][]
        {
            new string[]{"ON","OFF"},//1
            new string[]{"ON","OFF"},//2
            new string[]{"ON","OFF"},//3
            new string[]{"ON","OFF"},//4
            new string[]{"ON","OFF"},//5
            new string[]{"ON","OFF"},//6
            new string[]{"ON","OFF"},//7
            new string[]{"ON","OFF"},//8
            new string[]{"ON","OFF"},//9
            new string[]{"ON","OFF"},//10
            new string[]{"ON","OFF"},//11
            new string[]{"ON","OFF"},//12
            new string[]{"ON","OFF"},//13
            new string[]{"ON","OFF"},//14
            new string[]{"ON","OFF"},//15
            new string[]{"ON","OFF"},//16
            new string[]{"ON","OFF"},//17
            new string[]{"ON","OFF"},//18
        };
        public static string[] startupSettingCodes = funINI.getString(conFILE.iniDefault, "[Setting]", "startupSettingCode", 0);
    }
}
