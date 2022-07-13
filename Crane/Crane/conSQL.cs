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
            public static string SQLLimit = FunINI.GetString(ConFILE.iniDefault,"[db]", "SQLLimit")[0];
        }
        public class GenreSQL
        {
            public static string SQL0000 = $"SELECT * FROM T_GENRE WHERE GENREVISIBLESTATUS=0 AND GENRENAME LIKE @GENRENAME ORDER BY GENRENAME LIMIT {ComSQL.SQLLimit}";
            public static string SQL0001 = $"SELECT * FROM T_GENRE WHERE GENREID=@GENREID";
            public static string SQL0002 = $"SELECT GENREID,GENRENAME FROM T_GENRE WHERE GENREVISIBLESTATUS=0 ORDER BY GENREUPDATEDATE DESC";
            public static string SQL0010 = $"INSERT INTO T_GENRE(GENRENAME,GENREUPDATEDATE) VALUES(@GENRENAME,{ComSQL.now})";
            public static string SQL0020 = $"UPDATE T_GENRE SET GENRENAME=@GENRENAME,GENREUPDATEDATE={ComSQL.now} WHERE GENREID=@GENREID";
            public static string SQL0021 = "UPDATE T_GENRE SET GENREVISIBLESTATUS=@VISIBLESTATUS WHERE GENREID=@GENREID";
            public static string SQL0030 = "DELETE FROM T_GENRE WHERE GENREID = @GENREID";
        }
        public class GoalSQL
        {
            public static string SQL0100 = $"SELECT * FROM V_GOAL WHERE GOALNAME LIKE @GOALNAME ORDER BY GOALNAME LIMIT {ComSQL.SQLLimit}";
            public static string SQL0101 = $"SELECT * FROM V_GOAL WHERE GOALID=@GOALID";
            public static string SQL0102 = $"SELECT GOALID,GOALNAME FROM T_GOAL WHERE GOALVISIBLESTATUS=0 ORDER BY GOALUPDATEDATE DESC";
            public static string SQL0110 = $"INSERT INTO T_GOAL(GENREID,GOALNAME,GOALUPDATEDATE) VALUES(@GENREID,@GOALNAME,{ComSQL.now})";
            public static string SQL0120 = $"UPDATE T_GOAL SET GENREID=@GENREID,GOALNAME=@GOALNAME,GOALUPDATEDATE={ComSQL.now} WHERE GOALID=@GOALID";
            public static string SQL0121 = $"UPDATE T_GOAL SET GOALVISIBLESTATUS=@VISIBLESTATUS WHERE GOALID=@GOALID";
            public static string SQL0130 = "DELETE FROM T_GOAL WHERE GOALID = @GOALID";
        }
        public class PlanSQL
        {
            public static string SQL0200 = $"SELECT * FROM V_PLAN WHERE PLANNAME LIKE @PLANNAME ORDER BY GOALNAME,PLANNAME LIMIT {ComSQL.SQLLimit}";
            public static string SQL0201 = $"SELECT * FROM V_PLAN WHERE PLANID=@PLANID";
            public static string SQL0202 = $"SELECT PLANID,PLANNAME FROM V_PLAN WHERE GOALID=@GOALID AND GOALVISIBLESTATUS=0 AND PLANVISIBLESTATUS=0 ORDER BY PLANUPDATEDATE DESC";//特殊
            public static string SQL0210 = $"INSERT INTO T_PLAN(GOALID,PLANNAME,PLANUPDATEDATE) VALUES(@GOALID,@PLANNAME,{ComSQL.now})";
            public static string SQL0220 = $"UPDATE T_PLAN SET GOALID=@GOALID,PLANNAME=@PLANNAME,PLANUPDATEDATE={ComSQL.now} WHERE PLANID=@PLANID";
            public static string SQL0221 = $"UPDATE T_PLAN SET PLANVISIBLESTATUS=@VISIBLESTATUS WHERE PLANID=@PLANID";
            public static string SQL0230 = "DELETE FROM T_PLAN WHERE PLANID = @PLANID";
        }
        public class WorkSQL
        {
            public static string SQL0300 = $"SELECT * FROM V_WORK WHERE GOALNAME LIKE @KEYWORD OR PLANNAME LIKE @KEYWORD ORDER BY PRIORID,WORKSTARTDATE,WORKENDDATE,GOALNAME,PLANNAME,WORKNAME LIMIT {ComSQL.SQLLimit}";
            public static string SQL0301 = $"SELECT * FROM V_WORK WHERE WORKID=@WORKID";
            public static string SQL0302 = $"SELECT WORKID,PLANNAME,WORKNAME FROM V_WORK WHERE GOALID=@GOALID AND WORKVISIBLESTATUS=0 ORDER BY PRIORID,WORKSTARTDATE";
            public static string SQL0310 = $"INSERT INTO T_WORK(PLANID,WORKNAME,PRIORID,WORKSTARTDATE,WORKENDDATE,WORKUPDATEDATE) VALUES(@PLANID,@WORKNAME,@PRIORID,@WORKSTARTDATE,@WORKENDDATE,{ComSQL.now})";
            public static string SQL0320 = $"UPDATE T_WORK SET PLANID=@PLANID,WORKNAME=@WORKNAME,PRIORID=@PRIORID,WORKSTARTDATE=@WORKSTARTDATE,WORKENDDATE=@WORKENDDATE,WORKUPDATEDATE={ComSQL.now} WHERE WORKID=@WORKID";
            public static string SQL0321 = $"UPDATE T_WORK SET WORKVISIBLESTATUS=@VISIBLESTATUS WHERE WORKID=@WORKID";
            public static string SQL0330 = "DELETE FROM T_WORK WHERE WORKID = @WORKID";
        }
        public class ScheduleSQL
        {
            public static string SQL0400 = $"SELECT * FROM V_SCHEDULE WHERE SCHEDULEDATE=@SCHEDULEDATE ORDER BY SCHEDULESTARTTIME LIMIT {ComSQL.SQLLimit}";
            public static string SQL0401 = $"SELECT * FROM V_SCHEDULE WHERE SCHEDULEID=@SCHEDULEID";
            public static string SQL0402 = $"SELECT * FROM V_SCHEDULE WHERE GOALNAME LIKE @KEYWORD OR PLANNAME LIKE @KEYWORD ORDER BY SCHEDULEDATE DESC,SCHEDULESTARTTIME  LIMIT {ComSQL.SQLLimit}";
            public static string SQL0410 = $"INSERT INTO T_SCHEDULE(WORKID,STATUSID,SCHEDULEDATE,SCHEDULESTARTTIME,SCHEDULEENDTIME,SCHEDULEUPDATEDATE) VALUES(@WORKID,@STATUSID,@SCHEDULEDATE,@SCHEDULESTARTTIME,@SCHEDULEENDTIME,{ComSQL.now})";
            public static string SQL0420 = $"UPDATE T_SCHEDULE SET WORKID=@WORKID,STATUSID=@STATUSID,SCHEDULEDATE=@SCHEDULEDATE,SCHEDULESTARTTIME=@SCHEDULESTARTTIME,SCHEDULEENDTIME=@SCHEDULEENDTIME,SCHEDULEUPDATEDATE={ComSQL.now} WHERE SCHEDULEID=@SCHEDULEID";
            public static string SQL0421 = $"UPDATE T_SCHEDULE SET SCHEDULEVISIBLESTATUS=@VISIBLESTATUS WHERE SCHEDULEID=@SCHEDULEID";
            public static string SQL0430 = "DELETE FROM T_SCHEDULE WHERE SCHEDULEID = @SCHEDULEID";
        }
        public class AnalysisSQL
        {
            public static string SQL0500 = $"SELECT MONTH,GOALNAME,MINS FROM V_ST_SCHEDULE2 WHERE MONTH BETWEEN @MONTH1 AND @MONTH2 AND GOALNAME = @GOALNAME GROUP BY MONTH,GOALID" ;
            public static string SQL0501 = $"SELECT SCHEDULEMONTH AS MONTH,GOALID,GOALNAME,SUM(MINS)/60 AS MINS FROM V_SCHEDULE WHERE SCHEDULEMONTH BETWEEN @MONTH1 AND @MONTH2 AND STATUSID=3 AND GOALID=@GOALID GROUP BY SCHEDULEMONTH,GOALID ORDER BY MONTH";
            public static string SQL0502 = $"SELECT DISTINCT MONTH FROM V_ST_SCHEDULE2 WHERE MONTH BETWEEN @MONTH1 AND @MONTH2";
            public static string SQL0503 = $"SELECT DISTINCT MONTH FROM V_ST_SCHEDULE2 WHERE MINS !=0 AND MONTH BETWEEN @MONTH1 AND @MONTH2 AND GOALID = @GOALID  GROUP BY MONTH";
            public static string SQL0504 = $"SELECT DISTINCT GOALID,GOALNAME FROM V_ST_SCHEDULE2  LIMIT {ComSQL.SQLLimit}";
            public static string SQL0505 = $"SELECT DISTINCT GOALID,GOALNAME FROM V_ST_SCHEDULE2 WHERE GOALID = @GOALID LIMIT {ComSQL.SQLLimit}";
        }
        public class ReviewSQL
        {
            public static string SQL0600 = $"SELECT * FROM V_REVIEW WHERE STATUSID !=3 AND (GOALNAME LIKE @KEYWORD OR PLANNAME LIKE @KEYWORD) ORDER BY STATUSID,REVIEWENDDATE,GOALNAME,PLANNAME LIMIT {ComSQL.SQLLimit}";
            public static string SQL0601 = $"SELECT * FROM V_REVIEW WHERE STATUSID =3 AND (GOALNAME LIKE @KEYWORD OR PLANNAME LIKE @KEYWORD) ORDER BY REVIEWENDDATE,GOALNAME,PLANNAME LIMIT {ComSQL.SQLLimit}";
            public static string SQL0602 = $"SELECT * FROM V_REVIEW WHERE REVIEWID=@REVIEWID";
            public static string SQL0610 = $"INSERT INTO T_REVIEW(PLANID,REVIEWNAME,STATUSID,REVIEWENDDATE,REVIEWUPDATEDATE) VALUES(@PLANID,@REVIEWNAME,@STATUSID,@REVIEWENDDATE,{ComSQL.now})";
            public static string SQL0620 = $"UPDATE T_REVIEW SET PLANID=@PLANID,REVIEWNAME=@REVIEWNAME,STATUSID=@STATUSID,REVIEWENDDATE=@REVIEWENDDATE,REVIEWUPDATEDATE={ComSQL.now} WHERE REVIEWID=@REVIEWID";
            public static string SQL0621 = $"UPDATE T_REVIEW SET REVIEWVISIBLESTATUS=@VISIBLESTATUS WHERE REVIEWID=@REVIEWID";
            public static string SQL0630 = "DELETE FROM T_REVIEW WHERE REVIEWID = @REVIEWID";
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
            public static string SQL9004 = $"SELECT * FROM V_STORAGEBIN WHERE KEY LIKE @KEYWORD OR NAME LIKE @KEYWORD ORDER BY KEY,NAME,UPDATEDATE DESC LIMIT {ComSQL.SQLLimit}";
            public static string SQL9005 = $"SELECT * FROM V_RECYCLEBIN WHERE KEY LIKE @KEYWORD OR NAME LIKE @KEYWORD ORDER BY KEY,NAME,UPDATEDATE DESC LIMIT {ComSQL.SQLLimit}";
        }
    }
}
