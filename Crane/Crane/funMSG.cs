using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Crane
{
    class funMSG
    {
        public static void wrtMsg(string fileName,string msg)
        {
            Encoding sjisEnc  = Encoding.GetEncoding("Shift_JIS");
            StreamWriter writer = new StreamWriter(fileName, true, sjisEnc);
            StringBuilder sb = new StringBuilder();
            sb.Append(funDate.getToday(2));
            sb.Append(" >> ");
            sb.Append(msg);
            writer.WriteLine(sb.ToString());
            writer.Close();
        }
        public static void errMsg(string msg)
        {
            if (int.Parse(string.Format("{0}", conSetting.startupSettingCodes[1])) == 1)
            {
                MessageBox.Show(msg,"ERROR",MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            if (int.Parse(string.Format("{0}", conSetting.startupSettingCodes[2])) == 1)
            {
                wrtMsg(conFILE.msgLog, msg);
            }
        }
    }
}
