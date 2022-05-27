﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crane
{
    class ConSQL
    {
        public class ComSQL
        {
            public static string now = "datetime(strftime('%s', 'now'), 'unixepoch', 'localtime')";
            public static string SQLLimit = FunINI.getString(ConFILE.iniDefault,"[db]", "SQLLimit")[0];
        }
        public class GenreSQL
        {
            public static string SQL0000 = $"SELECT * FROM T_GENRE WHERE GENREVISIBLESTATUS=0 AND GENRENAME LIKE @GENRENAME ORDER BY GENREUPDATEDATE DESC LIMIT {ComSQL.SQLLimit}";
            public static string SQL0001 = $"SELECT * FROM T_GENRE WHERE GENREID=@GENREID";
            public static string SQL0002 = $"SELECT GENREID,GENRENAME FROM T_GENRE ORDER BY GENREUPDATEDATE DESC";
            public static string SQL0010 = $"INSERT INTO T_GENRE(GENRENAME,GENREUPDATEDATE) VALUES(@GENRENAME,{ComSQL.now})";
            public static string SQL0020 = $"UPDATE T_GENRE SET GENRENAME=@GENRENAME,GENREUPDATEDATE={ComSQL.now} WHERE GENREID=@GENREID";
            public static string SQL0021 = "UPDATE T_GENRE SET GENREVISIBLESTATUS=@VISIBLESTATUS WHERE GENREID=@GENREID";
            public static string SQL0030 = "DELETE FROM T_GENRE WHERE GENREID = @GENREID";
        }
        public class GoalSQL
        {
            public static string SQL0100 = $"SELECT * FROM V_GOAL WHERE GOALNAME LIKE @GOALNAME ORDER BY GOALUPDATEDATE DESC LIMIT {ComSQL.SQLLimit}";
            public static string SQL0101 = $"SELECT * FROM V_GOAL WHERE GOALID=@GOALID";
            public static string SQL0102 = $"SELECT GOALID,GOALNAME FROM T_GOAL ORDER BY GOALUPDATEDATE DESC";
            public static string SQL0110 = $"INSERT INTO T_GOAL(GENREID,GOALNAME,GOALUPDATEDATE) VALUES(@GENREID,@GOALNAME,{ComSQL.now})";
            public static string SQL0120 = $"UPDATE T_GOAL SET GENREID=@GENREID,GOALNAME=@GOALNAME,GOALUPDATEDATE={ComSQL.now} WHERE GOALID=@GOALID";
            public static string SQL0121 = $"UPDATE T_GOAL SET GOALVISIBLESTATUS=@VISIBLESTATUS WHERE GOALID=@GOALID";
            public static string SQL0130 = "DELETE FROM T_GOAL WHERE GOALID = @GOALID";
        }
        public class PlanSQL
        {
            public static string SQL0200 = $"SELECT * FROM V_PLAN WHERE PLANNAME LIKE @PLANNAME ORDER BY PLANUPDATEDATE DESC LIMIT {ComSQL.SQLLimit}";
            public static string SQL0201 = $"SELECT * FROM V_PLAN WHERE PLANID=@PLANID";
            public static string SQL0202 = $"SELECT PLANID,PLANNAME FROM V_PLAN WHERE GOALID=@GOALID ORDER BY PLANUPDATEDATE DESC";//特殊
            public static string SQL0210 = $"INSERT INTO T_PLAN(GOALID,PLANNAME,PLANUPDATEDATE) VALUES(@GOALID,@PLANNAME,{ComSQL.now})";
            public static string SQL0220 = $"UPDATE T_PLAN SET GOALID=@GOALID,PLANNAME=@PLANNAME,PLANUPDATEDATE={ComSQL.now} WHERE PLANID=@PLANID";
            public static string SQL0221 = $"UPDATE T_PLAN SET PLANVISIBLESTATUS=@VISIBLESTATUS WHERE PLANID=@PLANID";
            public static string SQL0230 = "DELETE FROM T_PLAN WHERE PLANID = @PLANID";
        }
        public class WorkSQL
        {
            public static string SQL0300 = $"SELECT * FROM V_WORK  WHERE GOALNAME LIKE @KEYWORD OR PLANNAME LIKE @KEYWORD ORDER BY PRIORID,WORKSTARTDATE LIMIT {ComSQL.SQLLimit}";
            public static string SQL0301 = $"SELECT * FROM V_WORK WHERE WORKID=@WORKID";
            public static string SQL0302 = $"SELECT WORKID,PLANNAME,WORKNAME FROM V_WORK WHERE GOALID=@GOALID ORDER BY PRIORID,WORKSTARTDATE";
            public static string SQL0310 = $"INSERT INTO T_WORK(PLANID,WORKNAME,PRIORID,WORKSTARTDATE,WORKENDDATE,WORKUPDATEDATE) VALUES(@PLANID,@WORKNAME,@PRIORID,@WORKSTARTDATE,@WORKENDDATE,{ComSQL.now})";
            public static string SQL0320 = $"UPDATE T_WORK SET PLANID=@PLANID,WORKNAME=@WORKNAME,PRIORID=@PRIORID,WORKSTARTDATE=@WORKSTARTDATE,WORKENDDATE=@WORKENDDATE,WORKUPDATEDATE={ComSQL.now} WHERE WORKID=@WORKID";
            public static string SQL0321 = $"UPDATE T_WORK SET WORKVISIBLESTATUS=@VISIBLESTATUS WHERE WORKID=@WORKID";
            public static string SQL0330 = "DELETE FROM T_WORK WHERE WORKID = @WORKID";
        }
        public class ScheduleSQL
        {
            public static string SQL0400 = $"SELECT * FROM V_SCHEDULE WHERE SCHEDULEDATE=@SCHEDULEDATE ORDER BY SCHEDULESTARTTIME LIMIT {ComSQL.SQLLimit}";
            public static string SQL0401 = $"SELECT * FROM V_SCHEDULE WHERE SCHEDULEID=@SCHEDULEID";
            public static string SQL0402 = $"SELECT * FROM V_SCHEDULE ORDER BY SCHEDULEDATE DESC,SCHEDULESTARTTIME LIMIT {ComSQL.SQLLimit}";
            public static string SQL0410 = $"INSERT INTO T_SCHEDULE(WORKID,STATUSID,SCHEDULEDATE,SCHEDULESTARTTIME,SCHEDULEENDTIME,SCHEDULEUPDATEDATE) VALUES(@WORKID,@STATUSID,@SCHEDULEDATE,@SCHEDULESTARTTIME,@SCHEDULEENDTIME,{ComSQL.now})";
            public static string SQL0420 = $"UPDATE T_SCHEDULE SET WORKID=@WORKID,STATUSID=@STATUSID,SCHEDULEDATE=@SCHEDULEDATE,SCHEDULESTARTTIME=@SCHEDULESTARTTIME,SCHEDULEENDTIME=@SCHEDULEENDTIME,SCHEDULEUPDATEDATE={ComSQL.now} WHERE SCHEDULEID=@SCHEDULEID";
            public static string SQL0421 = $"UPDATE T_SCHEDULE SET SCHEDULEVISIBLESTATUS=@VISIBLESTATUS WHERE SCHEDULEID=@SCHEDULEID";
            public static string SQL0430 = "DELETE FROM T_SCHEDULE WHERE SCHEDULEID = @SCHEDULEID";
        }
        public class PriorSQL
        {
            public static string SQL9001 = $"SELECT PRIORID,PRIORNAME FROM V_PRIOR";
        }
        public class StatusSQL
        {
            public static string SQL9002 = $"SELECT STATUSID,STATUSNAME FROM V_STATUS";
        }
        public class VacuumSQL
        {
            public static string SQL9003 = "VACUUM";
        }
        public class BinSQL
        {
            public static string SQL9004 = $"SELECT * FROM V_BIN ORDER BY KEY,NAME,UPDATEDATE DESC LIMIT {ComSQL.SQLLimit}";
        }
    }
}
