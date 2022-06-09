using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;

namespace Crane
{
    class FunSQL
    {
        //定義
        public static string dataSource = FunINI.GetString(ConFILE.iniDefault, "[db]", "dataSource")[0];
        private static void SQLLOG(string msg)
        {
            Task t = Task.Run(() =>
            {
                if (int.Parse(string.Format("{0}", ConSetting.startupSettingCodes[0])) == 1)
                {
                    FunMSG.WrtMsg(ConFILE.sqlLog, msg);
                }
            });
        }
        public static SQLiteDataReader SQLSELECT(string sqlcode,string sql,string[] parameters,string[] values)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(sqlcode);
            sb.Append("：");
            SQLiteDataReader output = null;
            SQLiteConnection Conn = new SQLiteConnection($"DataSource={dataSource}");
            Conn.Open();
            SQLiteCommand cmd = new SQLiteCommand(sql, Conn);
            if (int.Parse(string.Format("{0}", ConSetting.startupSettingCodes[17])) == 1)
            {
                StringBuilder sb1 = new StringBuilder();
                sb1.Append(sqlcode);
                sb1.Append(":");
                sb1.Append(sql);
                FunMSG.ErrMsg(sb1.ToString());
            }
            if (parameters.Length != 0)
            {
                for (int i = 0; i < parameters.Length; i++)
                {
                    if(int.Parse(string.Format("{0}", ConSetting.startupSettingCodes[17])) == 1)
                    {
                        StringBuilder sb2 = new StringBuilder();
                        sb2.Append(sqlcode);
                        sb2.Append(":");
                        sb2.Append(parameters[i]);
                        StringBuilder sb3 = new StringBuilder();
                        sb3.Append(sqlcode);
                        sb3.Append(":");
                        sb3.Append(values[i]);
                        FunMSG.ErrMsg(sb2.ToString());
                        FunMSG.ErrMsg(sb3.ToString());
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
                FunMSG.ErrMsg(sb.ToString());
            }
            sb.Append("[Success]");
            SQLLOG(sb.ToString());
            return output;
        }
        public static void SQLDML(string sqlcode,string sql,string[] parameters,string[] values)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(sqlcode);
            sb.Append("：");
            SQLiteConnection Conn = new SQLiteConnection($"DataSource={dataSource}");
            Conn.Open();
            SQLiteCommand cmd = new SQLiteCommand(sql, Conn);
            if (int.Parse(string.Format("{0}", ConSetting.startupSettingCodes[17])) == 1)
            {
                StringBuilder sb1 = new StringBuilder();
                sb1.Append(sqlcode);
                sb1.Append(":");
                sb1.Append(sql);
                FunMSG.ErrMsg(sb1.ToString());
            }
            if (parameters.Length != 0)
            {
                for (int i = 0; i < parameters.Length; i++)
                {
                    if (int.Parse(string.Format("{0}", ConSetting.startupSettingCodes[17])) == 1)
                    {
                        StringBuilder sb2 = new StringBuilder();
                        sb2.Append(sqlcode);
                        sb2.Append(":");
                        sb2.Append(parameters[i]);
                        StringBuilder sb3 = new StringBuilder();
                        sb3.Append(sqlcode);
                        sb3.Append(":");
                        sb3.Append(values[i]);
                        FunMSG.ErrMsg(sb2.ToString());
                        FunMSG.ErrMsg(sb3.ToString());
                    }
                    cmd.Parameters.Add(new SQLiteParameter(parameters[i], values[i]));
                }
            }
            try
            {
                cmd.ExecuteNonQuery();
                sb.Append("[Success]");
                SQLLOG(sb.ToString());
            }
            catch (Exception ex)
            {
                sb.Append(ex.Message);
                FunMSG.ErrMsg(sb.ToString());
            }
        }
    }
}
