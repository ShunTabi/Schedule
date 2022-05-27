using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crane
{
    class FunDate
    {
        public static string getToday(int code0,int code1)
        {
            string output = "";
            if (code0 == 0)
            {//日付のみ
                output = DateTime.Now.ToString("yyyy-MM-dd");
            }
            else if (code0 == 1)
            {
                output = DateTime.Now.ToString("HH:mm");
            }
            else if (code0 == 2)
            {
                output = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            }
            else if (code0 == 4)
            {
                output = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            }
            return output;
        }
        public static int getInt(string time)
        {
            int[] hs = new int[] { };
            string[] times = time.Split(':');
            for (int i = 0; i < times.Length; i++)
            {
                Array.Resize(ref hs, hs.Length + 1);
                hs[hs.Length - 1] = int.Parse(string.Format("{0}", times[i]));
            }
            
            return hs[0] * 4800 / 24  + hs[1] * 4800 / 24 / 60 + 30;
        }
    }
}
