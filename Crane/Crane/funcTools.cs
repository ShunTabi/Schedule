using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crane
{
    class funToday
    {
        public static string getToday(int code0)
        {
            string output = "";
            if (code0 == 0)
            {//日付のみ
                output = DateTime.Now.ToString("yyyy-MM-dd");
            }
            else if (code0 == 1)
            {
                output = DateTime.Now.ToString("hh-mm");
            }
            else if (code0 == 2)
            {
                output = DateTime.Now.ToString("yyyy-MM-dd hh-mm");
            }
            return output;
        }
    }
}
