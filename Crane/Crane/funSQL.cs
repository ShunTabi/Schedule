using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;

namespace Crane
{
    class funSQL
    {
        //定義
        private static string dataSource = @funINI.getString(conFILE.iniDefault, "[db]", "dataSource", 0)[0];
        private static int sqlLogMode = int.Parse(string.Format("{0}", conSetting.startupSettingCodes[0]));
        private static void sqlLog(string msg)
        {
            if (sqlLogMode == 0)
            {
                funMSG.wrtMsg(conFILE.sqlLog, msg);
            }
        }
        public static SQLiteDataReader sqlSELECT(string sqlcode,string sql,string[] parameters,string[] values)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(sqlcode);
            sb.Append("：");
            SQLiteDataReader output = null;
            SQLiteConnection conn = new SQLiteConnection($"DataSource={dataSource}");
            conn.Open();
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);
            if(parameters.Length != 0)
            {
                for (int i = 0; i < parameters.Length; i++)
                {
                    if(int.Parse(string.Format("{0}", conSetting.startupSettingCodes[17])) == 0)
                    {
                        funMSG.errMsg(sqlcode + parameters[i]);
                        funMSG.errMsg(sqlcode + values[i]);
                    }
                    cmd.Parameters.Add(new SQLiteParameter(parameters[i], values[i]));
                }
            }
            try
            {
                output = cmd.ExecuteReader();
            }
            catch(Exception ex)
            {
                sb.Append(ex.Message);
                funMSG.errMsg(sb.ToString());
            }
            sb.Append("[Success]");
            sqlLog(sb.ToString());
            return output;
        }
        public static void sqlDML(string sqlcode,string sql,string[] parameters,string[] values)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(sqlcode);
            sb.Append("：");
            SQLiteConnection conn = new SQLiteConnection($"DataSource={dataSource}");
            conn.Open();
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);
            if (parameters.Length != 0)
            {
                for (int i = 0; i < parameters.Length; i++)
                {
                    cmd.Parameters.Add(new SQLiteParameter(parameters[i], values[i]));
                }
            }
            try
            {
                cmd.ExecuteNonQuery();
                sb.Append("[Success]");
                sqlLog(sb.ToString());
            }
            catch (Exception ex)
            {
                sb.Append(ex.Message);
                funMSG.errMsg(sb.ToString());
            }
        }
    }
}
