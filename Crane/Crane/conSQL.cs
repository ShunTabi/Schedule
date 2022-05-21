using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crane
{
    class conSQL
    {
        public class com
        {
            public static string now = "datetime(strftime('%s', 'now'), 'unixepoch', 'localtime')";
            public static string sqlLimit = funINI.getString(conFILE.iniDefault,"[db]", "sqlLimit",0)[0];
        }
        public class genre
        {
            public static string sql0001 = $"SELECT * FROM T_GENRE WHERE GENREVISIBLESTATUS=0 AND GENRENAME LIKE @GENRENAME ORDER BY GENREUPDATEDATE DESC LIMIT {com.sqlLimit}";
            public static string sql0002 = $"INSERT INTO T_GENRE(GENRENAME,GENREUPDATEDATE) VALUES(@GENRENAME,{com.now})";
            public static string sql0003 = $"UPDATE T_GENRE SET GENRENAME=@GENRENAME,GENREUPDATEDATE={com.now} WHERE GENREID=@GENREID";
            public static string sql0004 = "UPDATE T_GENRE SET GENREVISIBLESTATUS=1 WHERE GENREID=@GENREID";
            public static string sql0005 = $"SELECT * FROM T_GENRE WHERE GENREID=@GENREID";
            public static string sql0006 = $"SELECT GENREID,GENRENAME FROM T_GENRE ORDER BY GENREUPDATEDATE DESC";
        }
        public class goal
        {
            public static string sql0101 = $"SELECT * FROM V_GOAL WHERE GOALNAME LIKE @GOALNAME ORDER BY GOALUPDATEDATE DESC LIMIT {com.sqlLimit}";
            public static string sql0102 = $"INSERT INTO T_GOAL(GENREID,GOALNAME,GOALUPDATEDATE) VALUES(@GENREID,@GOALNAME,{com.now})";
            public static string sql0103 = $"UPDATE T_GOAL SET GENREID=@GENREID,GOALNAME=@GOALNAME,GOALUPDATEDATE={com.now} WHERE GOALID=@GOALID";
            public static string sql0104 = $"UPDATE T_GOAL SET GOALVISIBLESTATUS=1 WHERE GOALID=@GOALID";
            public static string sql0105 = $"SELECT * FROM V_GOAL WHERE GOALID=@GOALID";
            public static string sql0106 = $"SELECT GOALID,GOALNAME FROM T_GOAL ORDER BY GOALUPDATEDATE DESC";
        }
        public class plan
        {
            public static string sql0201 = $"SELECT * FROM V_PLAN WHERE PLANNAME LIKE @PLANNAME ORDER BY PLANUPDATEDATE DESC LIMIT {com.sqlLimit}";
            public static string sql0202 = $"INSERT INTO T_PLAN(GOALID,PLANNAME,PLANUPDATEDATE) VALUES(@GOALID,@PLANNAME,{com.now})";
            public static string sql0203 = $"UPDATE T_PLAN SET GOALID=@GOALID,PLANNAME=@PLANNAME,PLANUPDATEDATE={com.now} WHERE PLANID=@PLANID";
            public static string sql0204 = $"UPDATE T_PLAN SET PLANVISIBLESTATUS=1 WHERE PLANID=@PLANID ORDER BY PLANUPDATEDATE DESC";
            public static string sql0205 = $"SELECT * FROM V_PLAN WHERE PLANID=@PLANID";
            public static string sql0206 = $"SELECT PLANID,PLANNAME FROM V_PLAN WHERE GOALID=@GOALID ORDER BY PLANUPDATEDATE DESC";//特殊
        }
        public class work
        {
            public static string sql0301 = $"SELECT * FROM V_WORK  WHERE GOALNAME LIKE @KEYWORD OR PLANNAME LIKE @KEYWORD ORDER BY PRIORID,WORKSTARTDATE LIMIT {com.sqlLimit}";
            public static string sql0302 = $"INSERT INTO T_WORK(PLANID,WORKNAME,PRIORID,WORKSTARTDATE,WORKENDDATE,WORKUPDATEDATE) VALUES(@PLANID,@WORKNAME,@PRIORID,@WORKSTARTDATE,@WORKENDDATE,{com.now})";
            public static string sql0303 = $"UPDATE T_WORK SET PLANID=@PLANID,WORKNAME=@WORKNAME,PRIORID=@PRIORID,WORKSTARTDATE=@WORKSTARTDATE,WORKENDDATE=@WORKENDDATE,WORKUPDATEDATE={com.now} WHERE WORKID=@WORKID";
            public static string sql0304 = $"UPDATE T_WORK SET WORKVISIBLESTATUS=1 WHERE WORKID=@WORKID";
            public static string sql0305 = $"SELECT * FROM V_WORK WHERE WORKID=@WORKID";
            public static string sql0306 = $"SELECT WORKID,PLANNAME,WORKNAME FROM V_WORK WHERE GOALID=@GOALID ORDER BY PRIORID,WORKSTARTDATE";
        }
        public class schedule
        {
            public static string sql0401 = $"SELECT * FROM V_SCHEDULE WHERE SCHEDULEDATE=@SCHEDULEDATE ORDER BY SCHEDULESTARTTIME LIMIT {com.sqlLimit}";
            public static string sql0402 = $"INSERT INTO T_SCHEDULE(WORKID,STATUSID,SCHEDULEDATE,SCHEDULESTARTTIME,SCHEDULEENDTIME,SCHEDULEUPDATEDATE) VALUES(@WORKID,@STATUSID,@SCHEDULEDATE,@SCHEDULESTARTTIME,@SCHEDULEENDTIME,{com.now})";
            public static string sql0403 = $"UPDATE T_SCHEDULE SET WORKID=@WORKID,STATUSID=@STATUSID,SCHEDULEDATE=@SCHEDULEDATE,SCHEDULESTARTTIME=@SCHEDULESTARTTIME,SCHEDULEENDTIME=@SCHEDULEENDTIME,SCHEDULEUPDATEDATE={com.now} WHERE SCHEDULEID=@SCHEDULEID";
            public static string sql0404 = $"UPDATE T_SCHEDULE SET SCHEDULEVISIBLESTATUS=1 WHERE WORKID=@WORKID";
            public static string sql0405 = $"SELECT * FROM V_SCHEDULE WHERE SCHEDULEID=@SCHEDULEID";
        }
        public class prior
        {
            public static string sql9001 = $"SELECT PRIORID,PRIORNAME FROM V_PRIOR";
        }
        public class status
        {
            public static string sql9002 = $"SELECT STATUSID,STATUSNAME FROM V_STATUS";
        }
    }
}
