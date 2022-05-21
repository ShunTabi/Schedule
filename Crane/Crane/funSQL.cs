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
        private static void sqlLog(string msg)
        {
            if (int.Parse(string.Format("{0}", conSetting.startupSettingCodes[0])) == 1)
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
            if (int.Parse(string.Format("{0}", conSetting.startupSettingCodes[17])) == 1)
            {
                StringBuilder sb1 = new StringBuilder();
                sb1.Append(sqlcode);
                sb1.Append(":");
                sb1.Append(sql);
                funMSG.errMsg(sb1.ToString());
            }
            if (parameters.Length != 0)
            {
                for (int i = 0; i < parameters.Length; i++)
                {
                    if(int.Parse(string.Format("{0}", conSetting.startupSettingCodes[17])) == 1)
                    {
                        StringBuilder sb2 = new StringBuilder();
                        sb2.Append(sqlcode);
                        sb2.Append(":");
                        sb2.Append(parameters[i]);
                        StringBuilder sb3 = new StringBuilder();
                        sb3.Append(sqlcode);
                        sb3.Append(":");
                        sb3.Append(values[i]);
                        funMSG.errMsg(sb2.ToString());
                        funMSG.errMsg(sb3.ToString());
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
            if (int.Parse(string.Format("{0}", conSetting.startupSettingCodes[17])) == 1)
            {
                StringBuilder sb1 = new StringBuilder();
                sb1.Append(sqlcode);
                sb1.Append(":");
                sb1.Append(sql);
                funMSG.errMsg(sb1.ToString());
            }
            if (parameters.Length != 0)
            {
                for (int i = 0; i < parameters.Length; i++)
                {
                    if (int.Parse(string.Format("{0}", conSetting.startupSettingCodes[17])) == 1)
                    {
                        StringBuilder sb2 = new StringBuilder();
                        sb2.Append(sqlcode);
                        sb2.Append(":");
                        sb2.Append(parameters[i]);
                        StringBuilder sb3 = new StringBuilder();
                        sb3.Append(sqlcode);
                        sb3.Append(":");
                        sb3.Append(values[i]);
                        funMSG.errMsg(sb2.ToString());
                        funMSG.errMsg(sb3.ToString());
                    }
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
