﻿/* Yet Another Forum.NET Firebird data layer by vzrus
 * Copyright (C) 2006-2016 Vladimir Zakharov
 * https://github.com/vzrus
 * http://sourceforge.net/projects/yaf-datalayers/
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation; version 2 only
 * General class structure is based on MS SQL Server code,
 * created by YAF developers
 *
 * http://www.yetanotherforum.net/
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation;version 2 only
 * of the License.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
 */
CREATE procedure {objectQualifier}DBINFO_TABLE_COLUMNS_INFO
    (
      I_TABLENAME VARCHAR(32)
    )
    RETURNS
    (
    "FieldName" VARCHAR(32),
    "FieldType" INTEGER,
    "TypeName" VARCHAR(32),	
    "FieldSubType" INTEGER,	
    "FieldCharacterLength" INTEGER,
    "FieldScale" INTEGER,
    "FieldLength" INTEGER,
    "SubTypeName" VARCHAR(32),
    "ActualTypeName" VARCHAR(32),
    "ActualSubTypeName" VARCHAR(32)
    )
    AS
    BEGIN
    for   select
    rf.rdb$field_name AS FieldName,
    f.rdb$field_type, 
    t.rdb$type_name, 
    f.rdb$field_sub_type,
    f.rdb$character_length, 
    f.rdb$field_scale,
    f.rdb$field_length,
    st.rdb$type_name as rdb$sub_type_name,
    case f.rdb$field_type
        when 7 then 'smallint'
        when 8 then 'integer'
        when 16 then 'int64'
        when 9 then 'quad'
        when 10 then 'float'
        when 11 then 'd_float'
        when 17 then 'boolean'
        when 27 then 'double'
        when 12 then 'date'
        when 13 then 'time'
        when 35 then 'timestamp'
        when 261 then 'blob'
        when 37 then 'varchar'
        when 14 then 'char'
        when 40 then 'cstring'
        when 45 then 'blob_id'
    end as "ActualType",
    case f.rdb$field_type
            when 7 then
        case f.rdb$field_sub_type
            when 1 then 'numeric'
            when 2 then 'decimal'
        end
        when 8 then
        case f.rdb$field_sub_type
            when 1 then 'numeric'
            when 2 then 'decimal'
        end
        when 16 then
        case f.rdb$field_sub_type
            when 1 then 'numeric'
            when 2 then 'decimal'
            else 'bigint'
        end
        when 14 then
        case f.rdb$field_sub_type
            when 0 then 'unspecified'
            when 1 then 'binary'
            when 3 then 'acl'
            else
            case
                when f.rdb$field_sub_type is null then 'unspecified'           end
        end
        when 37 then
        case f.rdb$field_sub_type
            when 0 then 'unspecified'
            when 1 then 'text'
            when 3 then 'acl'
            else
            case
               when f.rdb$field_sub_type is null then 'unspecified'         end
        end
        when 261 then
        case f.rdb$field_sub_type
            when 0 then 'unspecified'
            when 1 then 'text'
            when 2 then 'blr'
            when 3 then 'acl'
            when 4 then 'reserved'
            when 5 then 'encoded-meta-data'
            when 6 then 'irregular-finished-multi-db-tx'
            when 7 then 'transactional_description'
            when 8 then 'external_file_description'
        end
    end as "ActualSubType"
from rdb$relation_fields rf
 join rdb$relations r on rf.rdb$relation_name = r.rdb$relation_name 
join rdb$fields f on f.rdb$field_name = rf.rdb$field_source
left join rdb$types t
    on t.rdb$type = f.rdb$field_type
    and t.rdb$field_name = 'RDB$FIELD_TYPE'
left join rdb$types st
    on st.rdb$type = f.rdb$field_sub_type
    and st.rdb$field_name = 'RDB$FIELD_SUB_TYPE'
 WHERE (:I_TABLENAME IS NOT NULL AND rf.rdb$relation_name=:I_TABLENAME)   
order by
    f.rdb$field_type, t.rdb$type_name, f.rdb$field_sub_type
    INTO
    :"FieldName",
    :"FieldType",
    :"TypeName",	
    :"FieldSubType",	
    :"FieldCharacterLength",
    :"FieldScale",
    :"FieldLength",
    :"SubTypeName",
    :"ActualTypeName",
    :"ActualSubTypeName"
            DO SUSPEND;      
end;
--GO

 CREATE PROCEDURE {objectQualifier}ANNOUNCEMENTS_LIST(
    I_FORUMID INTEGER, 
    I_USERID INTEGER,
    I_SINCEDATE TIMESTAMP,
    I_TODATE TIMESTAMP,
    I_PAGEINDEX INTEGER,
    I_PAGESIZE INTEGER,
    I_STYLEDNICKS BOOL, 
    I_SHOWMOVED BOOL,
	I_SHOWDELETED BOOL,
    I_FINDLASTUNREAD BOOL,
    I_GETTAGS BOOL,
	i_UTCTIMESTAMP TIMESTAMP)
 RETURNS
 (  "ForumID" INTEGER,
    "TopicID" INTEGER,
    "Posted" TIMESTAMP,
    "LinkTopicID" INTEGER,
    "TopicMovedID" INTEGER,
    "FavoriteCount" INTEGER,
    "Subject" VARCHAR(128),
    "Status" VARCHAR(255),
    "Styles" VARCHAR(255),
    "Description" VARCHAR(255),
    "UserID" INTEGER,    
    "Starter" VARCHAR(128),
    "StarterDisplay" VARCHAR(128),
    "Replies" INTEGER,
    "NumPostsDeleted" INTEGER,  
    "Views" INTEGER,
    "LastPosted" TIMESTAMP,   
    "LastUserID" INTEGER,
    "LastUserName" VARCHAR(128),
    "LastUserDisplayName" VARCHAR(128),
    "LastMessageID" INTEGER,
    "LastTopicID" INTEGER,
    "TopicFlags" INTEGER,
    "Priority" INTEGER,
    "PollID" INTEGER,
    "ForumFlags" INTEGER,
    "FirstMessage"   blob sub_type 1,
    "StarterStyle" VARCHAR(255),
    "LastUserStyle" VARCHAR(255),
    "LastForumAccess" TIMESTAMP,
    "LastTopicAccess" timestamp,
    "TopicTags" varchar(4000),
	"TopicImage" varchar(255),
	"TopicImageType" varchar(50),
	"TopicImageBin" blob sub_type 0,
	"HasAttachments" integer,
    "TotalRows" INTEGER,
    "PageIndex" INTEGER
)
 AS
 DECLARE VARIABLE ici_RowTotalCount INTEGER DEFAULT 0; 
 DECLARE VARIABLE cnt INTEGER DEFAULT 1; 
 DECLARE VARIABLE ici_shiftsticky INTEGER DEFAULT 0;
 DECLARE VARIABLE ici_post_totalrowsnumber INTEGER; 
 DECLARE VARIABLE ici_firstselectrownum INTEGER DEFAULT 0; 
 DECLARE VARIABLE ici_sortsincelatest INTEGER DEFAULT 0;  
 DECLARE VARIABLE ici_firstselectposted timestamp; 
 DECLARE VARIABLE ici_ceiling decimal;
 DECLARE VARIABLE ici_retcount INTEGER DEFAULT 0; 
 DECLARE VARIABLE ici_counter INTEGER DEFAULT 0; 
 DECLARE ici_firstrow INTEGER;
 DECLARE ici_lastrow INTEGER;
BEGIN		
    -- find total returned count
        SELECT 		
        COUNT(1) 
    FROM
        {objectQualifier}TOPIC c1 
    WHERE
        c1.FORUMID = :I_FORUMID	
        AND	(c1."PRIORITY" = 2) 
        AND	(:I_SHOWDELETED = 1 or c1.ISDELETED = 0)
        AND	(c1.TOPICMOVEDID IS NOT NULL OR c1.NUMPOSTS > 0) 
        AND
        ((:I_SHOWMOVED = 1)
        or
        (:I_SHOWMOVED <> 1 AND  c1.TOPICMOVEDID IS NULL))
         INTO :ici_post_totalrowsnumber;

     -- I_PAGEINDEX = :I_PAGEINDEX+1;
     -- ici_firstselectrownum = (:I_PAGEINDEX - 1) * :I_PAGESIZE + 1 ;
      ici_firstselectrownum = (:I_PAGEINDEX*:i_pagesize) + 1;
    
                    
 /*  FOR SELECT FIRST 1
        t.LASTPOSTED
    FROM
        {objectQualifier}TOPIC t 
    where
            t.FORUMID = :I_FORUMID	
        AND	(t."PRIORITY" = 2) 
        AND	t.ISDELETED = 0
        AND	(t.TOPICMOVEDID IS NOT NULL OR t.NUMPOSTS > 0) 
        AND
        ((:I_SHOWMOVED = 1)
        or
        (:I_SHOWMOVED <> 1 AND  t.TOPICMOVEDID IS NULL))
        ORDER BY
        t."PRIORITY" DESC,t.LASTPOSTED DESC
        into :ici_firstselectposted
        DO
            begin
            SUSPEND;
                END
    SELECT :ici_firstselectposted from RDB$DATABASE
    INTO :ici_firstselectposted; */
	 ici_firstrow = :ici_firstselectrownum;
     ici_lastrow = :ici_firstselectrownum + :I_PAGESIZE - 1;

FOR  
    SELECT 		
        c.FORUMID,
        c.TOPICID,
        c.POSTED,
        COALESCE(c.TOPICMOVEDID,c.TOPICID) AS "LinkTopicID",
        c.TOPICMOVEDID,
        (SELECT COUNT(1) FROM {objectQualifier}FAVORITETOPIC 
        WHERE TOPICID = COALESCE(c.TOPICMOVEDID,c.TOPICID)),
        COALESCE(c.TOPIC,NULL) AS "Subject",
        c.STATUS,
        c.STYLES,
        c.DESCRIPTION,
        c.USERID,
        COALESCE(c.USERNAME,b.NAME) AS "Starter",
        COALESCE(c.USERDISPLAYNAME,b.DISPLAYNAME) AS "StarterDisplay",
        (c.NUMPOSTS - 1) AS "Replies",
        (SELECT COUNT(1) FROM {objectQualifier}MESSAGE mes 
                 WHERE mes.TOPICID = c.TOPICID AND mes.ISDELETED <> 0
                 AND mes.ISAPPROVED <> 0 
                 AND ((:I_USERID IS NOT NULL AND mes.USERID = :I_USERID) 
                 OR (:I_USERID IS NULL)) ) AS "NumPostsDeleted",
        c.VIEWS AS "Views",
        c.LASTPOSTED AS LASTPOSTED,
        c.LASTUSERID AS LASTUSERID,
        COALESCE(c.LASTUSERNAME,
        (select x.NAME from {objectQualifier}USER x 
        where x.USERID=c.LASTUSERID)) AS "LastUserName",
        COALESCE(c.LASTUSERDISPLAYNAME,
        (select x.DISPLAYNAME from {objectQualifier}USER x 
        where x.USERID=c.LASTUSERID)) AS "LastUserDisplayName",
        c.LASTMESSAGEID AS LASTMESSAGEID,
        c.TOPICID AS LASTTOPICID,
        c.FLAGS AS "TopicFlags",
        c."PRIORITY",
        c.POLLID,
        d.FLAGS AS "ForumFlags",
        (SELECT FIRST 1 MESSAGE 
                 FROM {objectQualifier}MESSAGE mes2 
                 WHERE mes2.TOPICID = COALESCE(c.TOPICMOVEDID,c.TOPICID) 
                 AND mes2."POSITION" = 0 ORDER BY mes2.TOPICID) AS "FirstMessage",				
      (case(:I_STYLEDNICKS)
            when 1 then (SELECT FIRST 1 usr.USERSTYLE FROM {objectQualifier}USER usr WHERE usr.USERID = c.USERID) 
            else (SELECT '' FROM RDB$DATABASE)	 end),
        (case(:I_STYLEDNICKS)
            when 1 then (SELECT FIRST 1 usr.USERSTYLE FROM {objectQualifier}USER usr WHERE usr.USERID = c.LASTUSERID)  
            else (SELECT '' FROM RDB$DATABASE)	 end),
        (case(:I_FINDLASTUNREAD)
             when 1 then
               (SELECT FIRST 1 LASTACCESSDATE FROM {objectQualifier}FORUMREADTRACKING x WHERE x.FORUMID=c.FORUMID AND x.USERID = c.USERID)
             else (select dateadd(1 day to current_timestamp)  FROM RDB$DATABASE) end) AS "LastForumAccess",
        (case(:I_FINDLASTUNREAD)
             when 1 then
               (SELECT FIRST 1 LASTACCESSDATE FROM {objectQualifier}TOPICREADTRACKING y WHERE y.TOPICID=c.TOPICID AND y.USERID = c.USERID)
             else (select dateadd(1 day to current_timestamp)  FROM RDB$DATABASE)  end) AS  "LastTopicAccess",	
        (case(:I_GETTAGS)
             when 1 then
        (SELECT * FROM {objectQualifier}TOPIC_GETTAGS_STR(c.TopicID))
             else (SELECT '' FROM RDB$DATABASE) end),			
	    c.TOPICIMAGE,
	    c.TOPICIMAGETYPE,
	    c.TOPICIMAGEBIN ,
	    (SELECT 0 FROM RDB$DATABASE),
        (SELECT :ici_post_totalrowsnumber FROM RDB$DATABASE) AS "TotalRows",
        (SELECT :I_PAGEINDEX  FROM RDB$DATABASE) AS  "PageIndex" 
    FROM
        {objectQualifier}TOPIC c 
        JOIN {objectQualifier}USER b ON b.USERID=c.USERID
        JOIN {objectQualifier}FORUM d ON d.FORUMID=c.FORUMID  		
    WHERE c.FORUMID = :I_FORUMID
        AND	(c."PRIORITY" = 2) 
        AND	(:I_SHOWDELETED = 1 or c.ISDELETED = 0)
        AND	((c.TOPICMOVEDID IS NOT NULL) OR (c.NUMPOSTS > 0)) 
        AND
        ((:I_SHOWMOVED = 1)
        or
        (:I_SHOWMOVED <> 1 AND  c.TOPICMOVEDID IS NULL))
    ORDER BY
        c."PRIORITY" DESC,c.LASTPOSTED DESC
    ROWS (:ici_firstrow) TO (:ici_lastrow)
    INTO 
    :"ForumID",
    :"TopicID",
    :"Posted",
    :"LinkTopicID",
    :"TopicMovedID",
    :"FavoriteCount",
    :"Subject",
    :"Status",
    :"Styles",
    :"Description",
    :"UserID",    
    :"Starter",
    :"StarterDisplay",
    :"Replies",
    :"NumPostsDeleted",  
    :"Views",
    :"LastPosted",   
    :"LastUserID",
    :"LastUserName",
    :"LastUserDisplayName",
    :"LastMessageID",
    :"LastTopicID",
    :"TopicFlags",
    :"Priority",
    :"PollID",
    :"ForumFlags",
    :"FirstMessage",
    :"StarterStyle",
    :"LastUserStyle",
    :"LastForumAccess",
    :"LastTopicAccess",
    :"TopicTags",
	:"TopicImage",
	:"TopicImageType",
	:"TopicImageBin",
	:"HasAttachments",
    :"TotalRows",
    :"PageIndex" 
DO	SUSPEND;

END;
--GO 

 CREATE PROCEDURE {objectQualifier}TOPIC_LIST (
    I_FORUMID integer,
    I_USERID integer,
    I_SINCEDATE timestamp,
    I_TODATE timestamp,
    I_PAGEINDEX integer,
    I_PAGESIZE integer,
    I_STYLEDNICKS BOOL,
    I_SHOWMOVED BOOL,
	I_SHOWDELETED BOOL,
    I_FINDLASTUNREAD BOOL,
    I_GETTAGS BOOL,
	I_UTCTIMESTAMP timestamp)
RETURNS (
    "ForumID" integer,
    "TopicID" integer,
    "Posted" timestamp,
    "LinkTopicID" integer,
    "TopicMovedID" integer,
    "FavoriteCount" integer,
    "Subject" varchar(255),
    "Status" varchar(255),
    "Styles" varchar(255),
    "Description" varchar(255),
    "UserID" integer,
    "Starter" varchar(255),
    "StarterDisplay" varchar(255),
    "Replies" integer,
    "NumPostsDeleted" integer,
    "Views" integer,
    "LastPosted" timestamp,
    "LastUserID" integer,
    "LastUserName" varchar(255),
    "LastUserDisplayName" varchar(255),
    "LastMessageID" integer,
    "LastTopicID" integer,
    "LinkDate" timestamp,
    "TopicFlags" integer,
    "Priority" integer,
    "PollID" integer,
    "ForumFlags" integer,
    "FirstMessage" blob sub_type 1,
    "StarterStyle" varchar(255),
    "LastUserStyle" varchar(255),
    "LastForumAccess" timestamp,
    "LastTopicAccess" timestamp,
    "TopicTags" varchar(4000),
	"TopicImage" varchar(255),
	"TopicImageType" varchar(50),
	"TopicImageBin" blob sub_type 0,
	"HasAttachments" integer,
    "TotalRows" integer,
    "PageIndex" integer )
AS
 DECLARE VARIABLE ici_RowTotalCount INTEGER DEFAULT 0; 
 DECLARE VARIABLE cnt INTEGER DEFAULT 1; 
 DECLARE VARIABLE ici_shiftsticky INTEGER DEFAULT 0;
 DECLARE VARIABLE ici_post_totalrowsnumber INTEGER DEFAULT 0; 
 DECLARE VARIABLE ici_p_pr_rnumber_pages INTEGER DEFAULT 0;
 DECLARE VARIABLE ici_post_priorityrowsnumber INTEGER DEFAULT 0;
 DECLARE VARIABLE ici_p_pr_rnumber_shift INTEGER DEFAULT 0;
 DECLARE VARIABLE ici_firstselectrownum INTEGER DEFAULT 0; 
 DECLARE VARIABLE ici_sortsincelatest INTEGER DEFAULT 0;  
 DECLARE VARIABLE ici_firstselectposted timestamp; 
 DECLARE VARIABLE ici_ceiling decimal;
 DECLARE VARIABLE ici_retcount INTEGER DEFAULT 0; 
 DECLARE VARIABLE ici_counter INTEGER DEFAULT 0; 
 DECLARE ici_firstrow INTEGER;
 DECLARE ici_lastrow INTEGER;
BEGIN  

    -- find priority returned count
SELECT 		
        COUNT(1) 
    FROM
        YAF_TOPIC t 
    WHERE
        t.FORUMID = :I_FORUMID	
        AND (t."PRIORITY"=1) 
        AND	(:I_SHOWDELETED = 1 or t.ISDELETED = 0)
        AND	(t.TOPICMOVEDID IS NOT NULL OR t.NUMPOSTS > 0) 
        AND
        ((:I_SHOWMOVED = 1)
        or
        (:I_SHOWMOVED <> 1 AND  t.TOPICMOVEDID IS NULL))	
		
          INTO :ici_post_priorityrowsnumber;
        ici_p_pr_rnumber_pages = CEILING(CAST(:ici_post_priorityrowsnumber AS decimal)/:I_PAGESIZE); 		 
      
        
    -- find total returned count
        SELECT 		
        COUNT(1) 
    FROM
        {objectQualifier}TOPIC t 
    WHERE
       t.FORUMID = :I_FORUMID	
        AND	(t."PRIORITY" = 1 OR (t."PRIORITY" <=0 AND t.LASTPOSTED >= :I_SINCEDATE )) 
        AND (:I_SHOWDELETED = 1 or t.ISDELETED = 0)
        AND	(t.TOPICMOVEDID IS NOT NULL OR t.NUMPOSTS > 0) 
        AND
        ((:I_SHOWMOVED = 1)
        or
        (:I_SHOWMOVED <> 1 AND  t.TOPICMOVEDID IS NULL))
         INTO :ici_post_totalrowsnumber;

      I_PAGEINDEX = :I_PAGEINDEX+1;
     -- ici_firstselectrownum = (:I_PAGEINDEX - 1) * :I_PAGESIZE + 1 ;
      ici_firstselectrownum = ((:I_PAGEINDEX-1)*:i_pagesize) + 1;
    
     if (:ici_p_pr_rnumber_pages <= :I_PAGEINDEX) then
     begin	
     ici_firstselectrownum = :ici_firstselectrownum - :ici_post_priorityrowsnumber;
     end
     else
     begin
     ici_p_pr_rnumber_shift = :ici_post_priorityrowsnumber;
     ici_shiftsticky = 1;
     if (:ici_firstselectrownum > 0) then
     begin
     ici_firstselectrownum = :ici_firstselectrownum 
     + :Ici_post_priorityrowsnumber - 1; 
     end 
     end    
                    
   SELECT FIRST 1
        t.LASTPOSTED			
    FROM
        {objectQualifier}TOPIC t 
    where
            t.FORUMID = :I_FORUMID	
        AND	((:ici_shiftsticky = 1 and t."PRIORITY"=1) OR (t."PRIORITY" <=0 AND t.LASTPOSTED >= :I_SINCEDATE )) 
        AND	(:I_SHOWDELETED = 1 or t.ISDELETED = 0)
        AND	(t.TOPICMOVEDID IS NOT NULL OR t.NUMPOSTS > 0) 
        AND
        ((:I_SHOWMOVED = 1)
        or
        (:I_SHOWMOVED <> 1 AND  t.TOPICMOVEDID IS NULL))
        ORDER BY
        t."PRIORITY" DESC,t.LASTPOSTED DESC
        into :ici_firstselectposted;           
 ici_firstrow = :ici_firstselectrownum + :Ici_post_priorityrowsnumber;
 ici_lastrow = :ici_firstselectrownum + :I_PAGESIZE + :Ici_post_priorityrowsnumber - 1;
FOR  
    SELECT 		
        c.FORUMID,
        c.TOPICID,
        c.POSTED,
        COALESCE(c.TOPICMOVEDID,c.TOPICID) AS "LinkTopicID",
        c.TOPICMOVEDID,
        (SELECT COUNT(1) FROM {objectQualifier}FAVORITETOPIC 
        WHERE TOPICID = COALESCE(c.TOPICMOVEDID,c.TOPICID)),
        COALESCE(c.TOPIC,NULL) AS "Subject",
        c.STATUS,
        c.STYLES,
        c.DESCRIPTION,
        c.USERID,
        COALESCE(c.USERNAME,b.NAME) AS "Starter",
        COALESCE(c.USERDISPLAYNAME,b.DISPLAYNAME) AS "StarterDisplay",
        (c.NUMPOSTS - 1) AS "Replies",
        (SELECT COUNT(1) FROM {objectQualifier}MESSAGE mes 
                 WHERE mes.TOPICID = c.TOPICID AND mes.ISDELETED <> 0
                 AND mes.ISAPPROVED <> 0 
                 AND ((:I_USERID IS NOT NULL AND mes.USERID = :I_USERID) 
                 OR (:I_USERID IS NULL)) ) AS "NumPostsDeleted",
        c.VIEWS AS "Views",
        c.LASTPOSTED AS LASTPOSTED,
        c.LASTUSERID AS LASTUSERID,
        COALESCE(c.LASTUSERNAME,
        (select NAME from {objectQualifier}USER x 
        where x.USERID=c.LASTUSERID)) AS "LastUserName",
        COALESCE(c.LASTUSERDISPLAYNAME,
        (select DISPLAYNAME from {objectQualifier}USER x 
        where x.USERID=c.LASTUSERID)) AS "LastUserDisplayName",
        c.LASTMESSAGEID AS LASTMESSAGEID,
        c.TOPICID AS LASTTOPICID,
        c.LinkDate,
        c.FLAGS AS "TopicFlags",
        c."PRIORITY",
        c.POLLID,
        d.FLAGS AS "ForumFlags",
        (SELECT FIRST 1 MESSAGE 
                 FROM {objectQualifier}MESSAGE mes2 
                 WHERE mes2.TOPICID = COALESCE(c.TOPICMOVEDID,c.TOPICID) 
                 AND mes2."POSITION" = 0 ORDER BY mes2.TOPICID) AS "FirstMessage",
        (case(:I_STYLEDNICKS)
            when 1 then b.USERSTYLE  
            else (SELECT '' FROM RDB$DATABASE)	 end),
        (case(:I_STYLEDNICKS)
            when 1 then (SELECT FIRST 1 usr.USERSTYLE FROM {objectQualifier}USER usr WHERE usr.USERID = c.LASTUSERID) 
            else (SELECT '' FROM RDB$DATABASE)	 end),
        (case(:I_FINDLASTUNREAD)
             when 1 then
               (SELECT FIRST 1 LASTACCESSDATE FROM {objectQualifier}FORUMREADTRACKING x WHERE x.FORUMID=c.FORUMID AND x.USERID = c.USERID)
             else (select dateadd(1 day to current_timestamp)  FROM RDB$DATABASE) end) AS "LastForumAccess",
        (case(:I_FINDLASTUNREAD)
             when 1 then
               (SELECT FIRST 1 LASTACCESSDATE FROM {objectQualifier}TOPICREADTRACKING y WHERE y.TOPICID=c.TOPICID AND y.USERID = c.USERID)
             else (select dateadd(1 day to current_timestamp)  FROM RDB$DATABASE)  end) AS  "LastTopicAccess",	
        (case(:I_GETTAGS)
             when 1 then
        (SELECT * FROM {objectQualifier}TOPIC_GETTAGS_STR(c.TopicID))
             else (SELECT '' FROM RDB$DATABASE) end),		
	    c.TOPICIMAGE,
	    c.TOPICIMAGETYPE,
	    c.TOPICIMAGEBIN,
	    (SELECT 0 FROM RDB$DATABASE),
        (SELECT :ici_post_totalrowsnumber FROM RDB$DATABASE) AS "TotalRows",
        (SELECT :i_PageIndex  FROM RDB$DATABASE) AS  "PageIndex" 
    FROM
        {objectQualifier}TOPIC c 
        JOIN YAF_USER b ON b.USERID=c.USERID
        JOIN YAF_FORUM d ON d.FORUMID=c.FORUMID  		
    WHERE c.FORUMID = :I_FORUMID
        AND	(c."PRIORITY"=1 OR (c."PRIORITY" <=0 AND c.LASTPOSTED <= :ici_firstselectposted )) 
        AND	c.ISDELETED = 0
        AND	((c.TOPICMOVEDID IS NOT NULL) OR (c.NUMPOSTS > 0)) 
        AND
        ((:I_SHOWMOVED = 1)
        or
        (:I_SHOWMOVED <> 1 AND  c.TOPICMOVEDID IS NULL))
    ORDER BY
        c."PRIORITY" DESC,c.LASTPOSTED DESC
		ROWS (:ici_firstrow) TO (:ici_lastrow)
    INTO 
    :"ForumID",
    :"TopicID",
    :"Posted",
    :"LinkTopicID",
    :"TopicMovedID",
    :"FavoriteCount",
    :"Subject",
    :"Status",
    :"Styles",
    :"Description",
    :"UserID",    
    :"Starter",
    :"StarterDisplay",
    :"Replies",
    :"NumPostsDeleted",  
    :"Views",
    :"LastPosted",   
    :"LastUserID",
    :"LastUserName",
    :"LastUserDisplayName",
    :"LastMessageID",
    :"LastTopicID",
    :"LinkDate",
    :"TopicFlags",
    :"Priority",
    :"PollID",
    :"ForumFlags",
    :"FirstMessage",
    :"StarterStyle",
    :"LastUserStyle",
    :"LastForumAccess",
    :"LastTopicAccess",
    :"TopicTags",
	:"TopicImage",
	:"TopicImageType",
	:"TopicImageBin",
	:"HasAttachments",
    :"TotalRows",
    :"PageIndex" 
DO	SUSPEND;
END;
--GO

 CREATE PROCEDURE {objectQualifier}TOPIC_ACTIVE(
                 I_BOARDID integer,
                 I_CATEGORYID integer,
                 I_PAGEUSERID integer,
                 I_SINCEDATE timestamp,
                 I_TODATE timestamp,
                 I_PAGEINDEX integer,
                 I_PAGESIZE integer,
                 I_STYLEDNICKS BOOL,
                 I_FINDLASTUNREAD BOOL,
				 I_UTCTIMESTAMP timestamp)
        RETURNS (
                 "ForumID" integer,
                 "TopicID" integer,
                 "Posted" timestamp,
                 "LinkTopicID" integer,
                 "Subject" varchar(255),
                 "Status" varchar(255),
                 "Styles" varchar(255),
                 "Description" varchar(255),
                 "UserID" integer,
                 "Starter" varchar(128),
                 "StarterDisplay" varchar(128),
                 "NumPostsDeleted" integer,
                 "Replies" integer,
                 "Views" integer,
                 "LastPosted" timestamp,
                 "LastUserID" integer,
                 "LastUserName" varchar(128),
                 "LastUserDisplayName" varchar(128),
                 "LastMessageID" integer,
                 "LastMessageFlags" integer,
                 "LastTopicID" integer,
                 "TopicFlags" integer,
                 "FavoriteCount" integer,
                 "Priority" integer,
                 "PollID" integer,
                 "ForumName" varchar(128),
                 "TopicMovedID" integer,
                 "ForumFlags" integer,
                 "FirstMessage" varchar(1024),
                 "StarterStyle" varchar(255),
                 "LastUserStyle" varchar(255),
                 "LastForumAccess" timestamp,
                 "LastTopicAccess" timestamp,
                 "TopicTags" varchar(4000),
				 "TopicImage" varchar(255),
				 "TopicImageType" varchar(50),
				 "TopicImageBin" blob sub_type 0,
	             "HasAttachments" integer,
                 "TotalRows" integer,
                 "PageIndex" integer)
        AS
          DECLARE VARIABLE ici_RowTotalCount INTEGER DEFAULT 0; 
          DECLARE VARIABLE cnt INTEGER DEFAULT 1;
          DECLARE VARIABLE ici_topics_totalrowsnumber INTEGER;
          DECLARE VARIABLE ici_firstselectrownum INTEGER DEFAULT 0;
          DECLARE VARIABLE ici_firstselectposted timestamp;
          DECLARE VARIABLE ici_retcount INTEGER DEFAULT 0;
          DECLARE VARIABLE ici_counter INTEGER DEFAULT 0; 
BEGIN  


SELECT 		
        COUNT(1) 
    FROM
        {objectQualifier}TOPIC c
        JOIN {objectQualifier}USER b ON b.USERID=c.USERID
        JOIN {objectQualifier}FORUM d ON d.FORUMID=c.FORUMID
        JOIN {objectQualifier}ACTIVEACCESS x ON x.FORUMID=d.FORUMID
        JOIN {objectQualifier}CATEGORY cat ON cat.CATEGORYID=d.CATEGORYID		
    WHERE
        (c.LASTPOSTED BETWEEN :I_SINCEDATE AND :I_TODATE) AND
        x.USERID = :I_PAGEUSERID AND
        x.READACCESS <> 0 AND 
        cat.BOARDID = :I_BOARDID AND
        (:I_CATEGORYID IS NULL OR cat.CATEGORYID=:I_CATEGORYID) AND
        c.ISDELETED = 0	
        INTO :ici_topics_totalrowsnumber;

      I_PAGEINDEX = :I_PAGEINDEX+1;   
      ici_firstselectrownum = ((:I_PAGEINDEX-1)*:I_PAGESIZE) + 1;	
                    
   SELECT FIRST 1
        c.LASTPOSTED			
    FROM
        {objectQualifier}TOPIC c
        JOIN {objectQualifier}USER b ON b.USERID=c.USERID
        JOIN {objectQualifier}FORUM d ON d.FORUMID=c.FORUMID
        JOIN {objectQualifier}ACTIVEACCESS x ON x.FORUMID=d.FORUMID
        JOIN {objectQualifier}CATEGORY cat ON cat.CATEGORYID=d.CATEGORYID		
    WHERE
        (c.LASTPOSTED BETWEEN :I_SINCEDATE AND :I_TODATE) AND
        x.USERID = :I_PAGEUSERID AND
        x.READACCESS <> 0 AND
        cat.BOARDID = :I_BOARDID AND
        (:I_CATEGORYID IS NULL OR cat.CATEGORYID=:I_CATEGORYID) AND
        c.ISDELETED = 0
    ORDER BY
        c.LASTPOSTED DESC,
        cat.SORTORDER,
        d.SORTORDER,
        d.NAME DESC,
        c."PRIORITY" DESC
    INTO :ici_firstselectposted;
            
    SELECT :ici_firstselectposted from RDB$DATABASE
    INTO :ici_firstselectposted;
    

  
FOR SELECT
        c.FORUMID,
        c.TOPICID,
        c.POSTED,
        COALESCE(c.TOPICMOVEDID,c.TOPICID) AS "LinkTopicID",
        c.TOPIC AS "Subject",
        c.STATUS,
        c.STYLES,
        c.DESCRIPTION,
        c.USERID,
        COALESCE(c.USERNAME,b.NAME) AS "Starter",
        COALESCE(c.USERDISPLAYNAME, b.DISPLAYNAME),
        (SELECT COUNT(1) 
                      FROM {objectQualifier}MESSAGE mes 
                      WHERE mes.TOPICID = c.TOPICID 
                        AND mes.ISDELETED = 1 
                        AND mes.ISAPPROVED = 1 
                        AND ((:I_PAGEUSERID IS NOT NULL AND mes.USERID = :I_PAGEUSERID) 
                        OR (:I_PAGEUSERID IS NULL)) ) AS "NumPostsDeleted",
        ((SELECT COUNT(1) FROM {objectQualifier}MESSAGE x 
        WHERE x.TOPICID=c.TOPICID and BIN_AND(x.FLAGS, 8)=0) - 1)
                 AS "Replies",
        c.VIEWS AS "Views",
        c.LASTPOSTED AS LASTPOSTED ,
        c.LASTUSERID AS LASTUSERID,
        COALESCE(c.LASTUSERNAME,(SELECT NAME FROM {objectQualifier}USER x 
        where x.USERID=c.LASTUSERID)) AS LASTUSERNAME,	
        COALESCE(c.LASTUSERDISPLAYNAME,(select x.DISPLAYNAME from {objectQualifier}USER x where  x.USERID=c.LASTUSERID)),
        c.LASTMESSAGEID AS LASTMESSAGEID,
        c.LASTMESSAGEFLAGS,
        c.TOPICID AS "LastTopicID",
        c.FLAGS AS "TopicFlags",
        (SELECT COUNT(ID) as FavoriteCount FROM {objectQualifier}FAVORITETOPIC WHERE TOPICID = COALESCE(c.TOPICMOVEDID,c.TOPICID)) AS FavoriteCount,		
        c."PRIORITY",
        c.POLLID,
        d.NAME AS "ForumName",
        c.TOPICMOVEDID,
        d.FLAGS AS "ForumFlags", 
        (SELECT FIRST 1 SUBSTRING(mes2.MESSAGE FROM 1 FOR 1024) 
        FROM {objectQualifier}MESSAGE mes2 
        where mes2.TOPICID = COALESCE(c.TOPICMOVEDID,c.TOPICID) 
        AND mes2."POSITION" = 0) AS "FirstMessage",
            case(:I_STYLEDNICKS)
            when 1 then b.USERSTYLE 
            else (SELECT '' FROM RDB$DATABASE)	 end,	
        case(:I_STYLEDNICKS)
        when 1 then  (SELECT FIRST 1 usr.USERSTYLE FROM {objectQualifier}USER usr WHERE usr.USERID = c.LASTUSERID) 
        else (SELECT '' FROM RDB$DATABASE)	 end,
        (case(:I_FINDLASTUNREAD)
             when 1 then
               (SELECT FIRST 1 LASTACCESSDATE FROM {objectQualifier}FORUMREADTRACKING x WHERE x.FORUMID=c.FORUMID AND x.USERID = c.USERID)
             else (select dateadd(1 day to current_timestamp)  FROM RDB$DATABASE) end) AS "LastForumAccess",
        (case(:I_FINDLASTUNREAD)
             when 1 then
               (SELECT FIRST 1 LASTACCESSDATE FROM {objectQualifier}TOPICREADTRACKING y WHERE y.TOPICID=c.TOPICID AND y.USERID = c.USERID)
             else (select dateadd(1 day to current_timestamp)  FROM RDB$DATABASE)  end) AS  "LastTopicAccess",
        (SELECT * FROM {objectQualifier}TOPIC_GETTAGS_STR(c.TOPICID)),				
	    c.TOPICIMAGE,
	    c.TOPICIMAGETYPE,
	    c.TOPICIMAGEBIN,
	    (SELECT 0 FROM RDB$DATABASE),
        (SELECT :ici_topics_totalrowsnumber FROM RDB$DATABASE) AS "TotalRows",
        (SELECT :i_PageIndex  FROM RDB$DATABASE) AS  "PageIndex" 	   
    FROM
        {objectQualifier}TOPIC c
        JOIN {objectQualifier}USER b ON b.USERID=c.USERID
        JOIN {objectQualifier}FORUM d ON d.FORUMID=c.FORUMID
        JOIN {objectQualifier}ACTIVEACCESS x ON x.FORUMID=d.FORUMID
        JOIN {objectQualifier}CATEGORY cat ON cat.CATEGORYID=d.CATEGORYID		
    WHERE
        c.LASTPOSTED <= :ici_firstselectposted AND
        x.USERID = :I_PAGEUSERID AND
        x.READACCESS <> 0 AND
    cat.BOARDID = :I_BOARDID AND
    (:I_CATEGORYID IS NULL OR cat.CATEGORYID=:I_CATEGORYID) AND
    c.ISDELETED = 0
    ORDER BY
    c.LASTPOSTED DESC,
    cat.SORTORDER,
    d.SORTORDER,
    d.NAME DESC,
    c."PRIORITY" DESC
    
    INTO 
    :"ForumID",
    :"TopicID",
    :"Posted",
    :"LinkTopicID",
    :"Subject",
    :"Status",
    :"Styles",
    :"Description",
    :"UserID",
    :"Starter",
    :"StarterDisplay",
    :"NumPostsDeleted",
    :"Replies",
    :"Views",
    :"LastPosted",
    :"LastUserID",
    :"LastUserName",
    :"LastUserDisplayName",
    :"LastMessageID",
    :"LastMessageFlags", 
    :"LastTopicID",
    :"TopicFlags",
    :"FavoriteCount",
    :"Priority",
    :"PollID",
    :"ForumName",
    :"TopicMovedID",
    :"ForumFlags",
    :"FirstMessage",
    :"StarterStyle",
    :"LastUserStyle",
    :"LastForumAccess",
    :"LastTopicAccess",
    :"TopicTags",
	:"TopicImage",
	:"TopicImageType",
	:"TopicImageBin",
	:"HasAttachments",
    :"TotalRows",
    :"PageIndex" 
DO	
    
  BEGIN
   ici_retcount = :ici_retcount +1;
   if (:ici_retcount between  :ici_firstselectrownum and :ici_firstselectrownum + :I_PAGESIZE) then
    begin
    SUSPEND;
    ici_counter = :ici_counter + 1; 
    end 
if (:ici_counter >= :I_PAGESIZE) then
LEAVE;						
  END

END;
--GO

CREATE PROCEDURE {objectQualifier}DIGEST_TOPICNEW(
                 I_BOARDID integer,
                 I_PAGEUSERID integer,
                 I_SINCEDATE timestamp,
                 I_TODATE timestamp,              
                 I_STYLEDNICKS smallint,               
				 I_UTCTIMESTAMP timestamp)
        RETURNS (
		         "ForumName" varchar(255),
				 "Subject" varchar(255),
				 "StartedUserName" varchar(255),
				 "LastUserName"  varchar(255),
				 "LastMessageID" integer,
				 "LastMessage" BLOB SUB_TYPE 1,
				 "Replies" integer)            
        AS        
BEGIN    
FOR SELECT
        d.NAME,
		c.TOPIC,
		c.USERDISPLAYNAME,		
	    c.LASTUSERDISPLAYNAME,
	    c.LASTMESSAGEID,
		(SELECT x.MESSAGE FROM {objectQualifier}MESSAGE x 
          WHERE x.TOPICID=c.TOPICID and x.MESSAGEID = c.LASTMESSAGEID),
	    (SELECT COUNT(1) FROM {objectQualifier}MESSAGE x 
          WHERE x.TOPICID=c.TOPICID and BIN_AND(x.FLAGS, 8)=0)  
    FROM
        {objectQualifier}TOPIC c  
        JOIN {objectQualifier}FORUM d ON d.FORUMID=c.FORUMID 
		JOIN {objectQualifier}CATEGORY cat ON cat.CATEGORYID = d.CATEGORYID     
        join {objectQualifier}VACCESS x on (x.FORUMID=d.FORUMID AND x.USERID = :I_PAGEUSERID AND  x.READACCESS <> 0)      
    WHERE
	    c.POSTED > :I_SINCEDATE AND
        cat.BOARDID = :I_BOARDID AND   
        c.ISDELETED = 0 
    ORDER BY
	d.SORTORDER,
    c.LASTPOSTED DESC    
    INTO 
      :"ForumName",
	  :"Subject",
	  :"StartedUserName",
	  :"LastUserName",
	  :"LastMessageID",
	  :"LastMessage",
	  :"Replies" 
    DO SUSPEND;
END;
--GO

CREATE PROCEDURE {objectQualifier}DIGEST_TOPICACTIVE(
                 I_BOARDID integer,             
                 I_PAGEUSERID integer,
                 I_SINCEDATE timestamp,
                 I_TODATE timestamp,         
                 I_STYLEDNICKS smallint,               
				 I_UTCTIMESTAMP timestamp)
        RETURNS (
		         "ForumName" varchar(255),
				 "Subject" varchar(255),
				 "StartedUserName" varchar(255),
				 "LastUserName"  varchar(255),
				 "LastMessageID" integer,
				 "LastMessage" BLOB SUB_TYPE 1,
				 "Replies" integer)            
        AS        
BEGIN    
FOR SELECT
        d.NAME,
		c.TOPIC,
		c.USERDISPLAYNAME,		
	    c.LASTUSERDISPLAYNAME,
	    c.LASTMESSAGEID,
		(SELECT x.MESSAGE FROM {objectQualifier}MESSAGE x 
          WHERE x.TOPICID=c.TOPICID and x.MESSAGEID = c.LASTMESSAGEID),
	    ((SELECT COUNT(1) FROM {objectQualifier}MESSAGE x 
          WHERE x.TOPICID=c.TOPICID and BIN_AND(x.FLAGS, 8)=0))    
    FROM
        {objectQualifier}TOPIC c  
        JOIN {objectQualifier}FORUM d ON d.FORUMID=c.FORUMID 
		JOIN {objectQualifier}CATEGORY cat ON cat.CATEGORYID = d.CATEGORYID     
        join {objectQualifier}VACCESS x on (x.FORUMID=d.FORUMID AND x.USERID = :I_PAGEUSERID AND  x.READACCESS <> 0)      
    WHERE
	    c.LASTPOSTED > :I_SINCEDATE and
        c.LASTPOSTED < :I_TODATE    AND 
        cat.BOARDID = :I_BOARDID AND   
        c.ISDELETED = 0
    ORDER BY
	d.SORTORDER,
    c.LASTPOSTED DESC    
    INTO 
      :"ForumName",
	  :"Subject",
	  :"StartedUserName",
	  :"LastUserName",
	  :"LastMessageID",
	  :"LastMessage",
	  :"Replies" 
    DO SUSPEND;
END;
--GO


  CREATE PROCEDURE {objectQualifier}TOPIC_UNREAD (
    I_BOARDID integer,
    I_CATEGORYID integer,
    I_PAGEUSERID integer,
    I_SINCEDATE timestamp,
    I_TODATE timestamp,
    I_PAGEINDEX integer,
    I_PAGESIZE integer,
    I_STYLEDNICKS BOOL,
    I_FINDLASTUNREAD BOOL )
RETURNS (
  "ForumID" integer,
"TopicID" integer,
"Posted" timestamp,
"LinkTopicID" integer,
"Subject" varchar(255),
"Status" varchar(255),
"Styles" varchar(255),
"Description" varchar(255),
"UserID" integer,
"Starter" varchar(128),
"StarterDisplay" varchar(128),
"NumPostsDeleted" integer,
"Replies" integer,
"Views" integer,
"LastPosted" timestamp,
"LastUserID" integer,
"LastUserName" varchar(128),
"LastUserDisplayName" varchar(128),
"LastMessageID" integer,
"LastMessageFlags" integer, 
"LastTopicID" integer,
"TopicFlags" integer,
"FavoriteCount" integer,
"Priority" integer,
"PollID" integer,
"ForumName" varchar(128),
"TopicMovedID" integer,
"ForumFlags" integer,
"FirstMessage" varchar(1024),
"StarterStyle" varchar(255),
"LastUserStyle" varchar(255),
"LastForumAccess" timestamp,
"LastTopicAccess" timestamp,
"TopicTags" varchar(4000),
"TopicImage" varchar(255),
"TopicImageType" varchar(50),
"TopicImageBin" blob sub_type 0,
"HasAttachments" integer,
"TotalRows" integer ,
"PageIndex" integer)
AS
 DECLARE VARIABLE ici_RowTotalCount INTEGER DEFAULT 0; 
 DECLARE VARIABLE cnt INTEGER DEFAULT 1;
 DECLARE VARIABLE ici_topics_totalrowsnumber INTEGER; 
 DECLARE VARIABLE ici_firstselectrownum INTEGER DEFAULT 0;  
 DECLARE VARIABLE ici_firstselectposted timestamp; 
 DECLARE VARIABLE ici_retcount INTEGER DEFAULT 0; 
 DECLARE VARIABLE ici_counter INTEGER DEFAULT 0; 
BEGIN  


SELECT 		
        COUNT(1) 
    FROM
        {objectQualifier}TOPIC c
        JOIN {objectQualifier}USER b ON b.USERID=c.USERID
        JOIN {objectQualifier}FORUM d ON d.FORUMID=c.FORUMID
        JOIN {objectQualifier}ACTIVEACCESS x ON x.FORUMID=d.FORUMID
        JOIN {objectQualifier}CATEGORY cat ON cat.CATEGORYID=d.CATEGORYID		
    WHERE
        (c.LASTPOSTED BETWEEN :I_SINCEDATE AND :I_TODATE) AND
        x.USERID = :I_PAGEUSERID AND
        x.READACCESS <> 0 AND
    cat.BOARDID = :I_BOARDID AND
    (:I_CATEGORYID IS NULL OR cat.CATEGORYID=:I_CATEGORYID) AND
    c.ISDELETED = 0	
          INTO :ici_topics_totalrowsnumber;

      I_PAGEINDEX = :I_PAGEINDEX+1;   
      ici_firstselectrownum = ((:I_PAGEINDEX-1)*:I_PAGESIZE) + 1;	
                    
   SELECT FIRST 1
        c.LASTPOSTED			
    FROM
        {objectQualifier}TOPIC c
        JOIN {objectQualifier}USER b ON b.USERID=c.USERID
        JOIN {objectQualifier}FORUM d ON d.FORUMID=c.FORUMID
        JOIN {objectQualifier}ACTIVEACCESS x ON x.FORUMID=d.FORUMID
        JOIN {objectQualifier}CATEGORY cat ON cat.CATEGORYID=d.CATEGORYID		
    WHERE
        (c.LASTPOSTED BETWEEN :I_SINCEDATE AND :I_TODATE) AND
        x.USERID = :I_PAGEUSERID AND
        x.READACCESS <> 0 AND
    cat.BOARDID = :I_BOARDID AND
    (:I_CATEGORYID IS NULL OR cat.CATEGORYID=:I_CATEGORYID) AND
    c.ISDELETED = 0
    ORDER BY	
    c.LASTPOSTED DESC,
    cat.SORTORDER,
    d.SORTORDER,
    d.NAME DESC,
    c."PRIORITY" DESC
        into :ici_firstselectposted;
            
    SELECT :ici_firstselectposted from RDB$DATABASE
    INTO :ici_firstselectposted;
    

  
FOR SELECT
        c.FORUMID,
        c.TOPICID,
        c.POSTED,
        COALESCE(c.TOPICMOVEDID,c.TOPICID) AS "LinkTopicID",
        c.TOPIC AS "Subject",
        c.STATUS,
        c.STYLES,
        c.DESCRIPTION,
        c.USERID,
        COALESCE(c.USERNAME,b.NAME) AS "Starter",
        COALESCE(c.USERDISPLAYNAME,b.DISPLAYNAME) AS "StarterDisplay",
        (SELECT COUNT(1) 
                      FROM {objectQualifier}MESSAGE mes 
                      WHERE mes.TOPICID = c.TOPICID 
                        AND mes.ISDELETED = 1 
                        AND mes.ISAPPROVED = 1 
                        AND ((:I_PAGEUSERID IS NOT NULL AND mes.USERID = :I_PAGEUSERID) 
                        OR (:I_PAGEUSERID IS NULL)) ) AS "NumPostsDeleted",
        ((SELECT COUNT(1) FROM {objectQualifier}MESSAGE x 
        WHERE x.TOPICID=c.TOPICID and BIN_AND(x.FLAGS, 8)=0) - 1)
                 AS "Replies",
        c.VIEWS AS "Views",
        c.LASTPOSTED AS LASTPOSTED ,
        c.LASTUSERID AS LASTUSERID,
        COALESCE(c.LASTUSERNAME,(SELECT NAME FROM {objectQualifier}USER x 
        where x.USERID=c.LASTUSERID)) AS LASTUSERNAME,
        COALESCE(c.LASTUSERDISPLAYNAME,(select x.DISPLAYNAME from {objectQualifier}USER x where x.USERID=c.LASTUSERID)),
        c.LASTMESSAGEID AS LASTMESSAGEID,
        c.LASTMESSAGEFLAGS,
        c.TOPICID AS "LastTopicID",
        c.FLAGS AS "TopicFlags",
        (SELECT COUNT(ID) as FavoriteCount FROM {objectQualifier}FAVORITETOPIC WHERE TOPICID = COALESCE(c.TOPICMOVEDID,c.TOPICID)) AS FavoriteCount,		
        c."PRIORITY",
        c.POLLID,
        d.NAME AS "ForumName",
        c.TOPICMOVEDID,
        d.FLAGS AS "ForumFlags", 
        (SELECT FIRST 1 SUBSTRING(mes2.MESSAGE FROM 1 FOR 1024) 
        FROM {objectQualifier}MESSAGE mes2 
        where mes2.TOPICID = COALESCE(c.TOPICMOVEDID,c.TOPICID) 
        AND mes2."POSITION" = 0) AS "FirstMessage",
            case(:I_STYLEDNICKS)
            when 1 then b.USERSTYLE
            else (SELECT '' FROM RDB$DATABASE)	 end,	
        case(:I_STYLEDNICKS)
        when 1 then  (SELECT FIRST 1 usr.USERSTYLE FROM {objectQualifier}USER usr WHERE usr.USERID = c.LASTUSERID) 
        else (SELECT '' FROM RDB$DATABASE)	 end,
        (case(:I_FINDLASTUNREAD)
             when 1 then
               (SELECT FIRST 1 LASTACCESSDATE FROM {objectQualifier}FORUMREADTRACKING x WHERE x.FORUMID=c.FORUMID AND x.USERID = c.USERID)
             else (select dateadd(1 day to current_timestamp)  FROM RDB$DATABASE) end) AS "LastForumAccess",
        (case(:I_FINDLASTUNREAD)
             when 1 then
               (SELECT FIRST 1 LASTACCESSDATE FROM {objectQualifier}TOPICREADTRACKING y WHERE y.TOPICID=c.TOPICID AND y.USERID = c.USERID)
             else (select dateadd(1 day to current_timestamp)  FROM RDB$DATABASE)  end) AS  "LastTopicAccess",
        (SELECT * FROM {objectQualifier}TOPIC_GETTAGS_STR(c.TopicID)),			
	    c.TOPICIMAGE,
	    c.TOPICIMAGETYPE,
	    c.TOPICIMAGEBIN ,
	    (SELECT 0 FROM RDB$DATABASE),
        (SELECT :ici_topics_totalrowsnumber FROM RDB$DATABASE) AS "TotalRows",
        (SELECT :i_PageIndex  FROM RDB$DATABASE) AS  "PageIndex" 	   
    FROM
        {objectQualifier}TOPIC c
        JOIN {objectQualifier}USER b ON b.USERID=c.USERID
        JOIN {objectQualifier}FORUM d ON d.FORUMID=c.FORUMID
        JOIN {objectQualifier}ACTIVEACCESS x ON x.FORUMID=d.FORUMID
        JOIN {objectQualifier}CATEGORY cat ON cat.CATEGORYID=d.CATEGORYID		
    WHERE
        c.LASTPOSTED <= :ici_firstselectposted AND
        x.USERID = :I_PAGEUSERID AND
        x.READACCESS <> 0 AND
    cat.BOARDID = :I_BOARDID AND
    (:I_CATEGORYID IS NULL OR cat.CATEGORYID=:I_CATEGORYID) AND
    c.ISDELETED = 0
    ORDER BY
    c.LASTPOSTED DESC,
    cat.SORTORDER,
    d.SORTORDER,
    d.NAME DESC,
    c."PRIORITY" DESC
    
    INTO 
    :"ForumID",
    :"TopicID",
    :"Posted",
    :"LinkTopicID",
    :"Subject",
    :"Status",
    :"Styles",
    :"Description",
    :"UserID",
    :"Starter",
    :"StarterDisplay", 
    :"NumPostsDeleted",
    :"Replies",
    :"Views",
    :"LastPosted",
    :"LastUserID",
    :"LastUserName",
    :"LastUserDisplayName",
    :"LastMessageID",
    :"LastMessageFlags",
    :"LastTopicID",
    :"TopicFlags",
    :"FavoriteCount",
    :"Priority",
    :"PollID",
    :"ForumName",
    :"TopicMovedID",
    :"ForumFlags",
    :"FirstMessage",
    :"StarterStyle",
    :"LastUserStyle",
    :"LastForumAccess",
    :"LastTopicAccess",
	 :"TopicTags",
	:"TopicImage",
	:"TopicImageType",
	:"TopicImageBin",
	:"HasAttachments",
    :"TotalRows",
    :"PageIndex" 
DO	
    
  BEGIN
   ici_retcount = :ici_retcount +1;
   if (:ici_retcount between  :ici_firstselectrownum and :ici_firstselectrownum + :I_PAGESIZE) then
    begin
    SUSPEND;
    ici_counter = :ici_counter + 1; 
    end 
if (:ici_counter >= :I_PAGESIZE) then
LEAVE;						
  END

END;
--GO

 CREATE PROCEDURE {objectQualifier}TOPIC_UNANSWERED (
    I_BOARDID integer,
    I_CATEGORYID integer,
    I_PAGEUSERID integer,
    I_SINCEDATE timestamp,
    I_TODATE timestamp,
    I_PAGEINDEX integer,
    I_PAGESIZE integer,
    I_STYLEDNICKS BOOL,
    I_FINDLASTUNREAD BOOL,
	I_UTCTIMESTAMP timestamp  )
RETURNS (
        "ForumID" INTEGER,
        "TopicID" INTEGER,
        "TopicMovedID" INTEGER,
        "Posted" TIMESTAMP,

        "LinkTopicID" INTEGER,
        "Subject"  VARCHAR(255),
        "Description"  VARCHAR(255),
        "Status"  VARCHAR(255),

        "Styles" VARCHAR(255),
        "UserID" INTEGER,
        "Starter" VARCHAR(255),
        "StarterDisplay" VARCHAR(255),
        "NumPostsDeleted" INTEGER,
        "Replies" INTEGER,
        "Views" INTEGER,
        "LastPosted" TIMESTAMP,
        "LastUserID" INTEGER,
        "LastUserName"   VARCHAR(255),
        "LastUserDisplayName"   VARCHAR(255),
        "LastMessageID" INTEGER,
        "LastMessageFlags" INTEGER,
        "LastTopicID" INTEGER,
        "TopicFlags" INTEGER,
        "FavoriteCount" INTEGER,
        "Priority" INTEGER,
        "PollID" INTEGER,
        "ForumName"  VARCHAR(255),			
        "ForumFlags" INTEGER,
        "FirstMessage" VARCHAR(1000),
        "StarterStyle" VARCHAR(255),
        "LastUserStyle" VARCHAR(255),
        "LastForumAccess" TIMESTAMP,
        "LastTopicAccess" TIMESTAMP,
		"TopicTags" varchar(4000),
        "TopicImage" varchar(255),
"TopicImageType" varchar(50),
"TopicImageBin" blob sub_type 0,
"HasAttachments" integer,
"TotalRows" integer ,
"PageIndex" integer)
AS
 DECLARE VARIABLE ici_RowTotalCount INTEGER DEFAULT 0; 
 DECLARE VARIABLE cnt INTEGER DEFAULT 1;
 DECLARE VARIABLE ici_topics_totalrowsnumber INTEGER; 
 DECLARE VARIABLE ici_firstselectrownum INTEGER DEFAULT 0;  
 DECLARE VARIABLE ici_firstselectposted timestamp; 
 DECLARE VARIABLE ici_retcount INTEGER DEFAULT 0; 
 DECLARE VARIABLE ici_counter INTEGER DEFAULT 0;
BEGIN  


SELECT 		
        COUNT(1) 
    FROM
        {objectQualifier}TOPIC c
        JOIN {objectQualifier}USER b ON b.USERID=c.USERID
        JOIN {objectQualifier}FORUM d ON d.FORUMID=c.FORUMID
        JOIN {objectQualifier}ACTIVEACCESS x ON x.FORUMID=d.FORUMID
        JOIN {objectQualifier}CATEGORY cat ON cat.CATEGORYID=d.CATEGORYID		
    WHERE
    (c.LASTPOSTED BETWEEN :I_SINCEDATE AND :I_TODATE) AND
    x.USERID = :I_PAGEUSERID and
        x.READACCESS <> 0 and
        cat.BOARDID = :I_BOARDID and
        (:I_CATEGORYID is null or cat.CATEGORYID = :I_CATEGORYID) and
        c.ISDELETED = 0 and	
        c.TOPICMOVEDID is null and
        c.NUMPOSTS = 1
          INTO :ici_topics_totalrowsnumber;      


      I_PAGEINDEX = :I_PAGEINDEX+1;   
      ici_firstselectrownum = ((:I_PAGEINDEX-1)*:I_PAGESIZE) + 1;
    
                    
   SELECT FIRST 1
        c.LASTPOSTED			
    FROM
        {objectQualifier}TOPIC c
        JOIN {objectQualifier}USER b ON b.USERID=c.USERID
        JOIN {objectQualifier}FORUM d ON d.FORUMID=c.FORUMID
        JOIN {objectQualifier}ACTIVEACCESS x ON x.FORUMID=d.FORUMID
        JOIN {objectQualifier}CATEGORY cat ON cat.CATEGORYID=d.CATEGORYID		
    WHERE
        (c.LASTPOSTED BETWEEN :I_SINCEDATE AND :I_TODATE) AND
    x.USERID = :I_PAGEUSERID and
        x.READACCESS <> 0 and
        cat.BOARDID = :I_BOARDID and
        (:I_CATEGORYID is null or cat.CATEGORYID = :I_CATEGORYID) and
        c.ISDELETED = 0 and	
        c.TOPICMOVEDID is null and
        c.NUMPOSTS = 1
    ORDER BY	
    c.LASTPOSTED DESC,
    cat.SORTORDER,
    d.SORTORDER,
    d.NAME DESC,
    c."PRIORITY" DESC
        into :ici_firstselectposted;
            
    SELECT :ici_firstselectposted from RDB$DATABASE
    INTO :ici_firstselectposted;
    

  
FOR SELECT			
        c.FORUMID,
        c.TOPICID,
        c.TOPICMOVEDID,
        c.POSTED,
        COALESCE(c.TOPICMOVEDID,c.TOPICID),
        c.TOPIC,
        c.DESCRIPTION,
        c.STATUS,
        c.STYLES,
        c.USERID,
        COALESCE(c.USERNAME,b.NAME),
        COALESCE(c.USERDISPLAYNAME,b.DISPLAYNAME),
        (SELECT COUNT(1) FROM  {objectQualifier}MESSAGE mes WHERE mes.TOPICID = c.TOPICID AND mes.ISDELETED = 1 AND mes.ISAPPROVED = 1 AND ((:I_PAGEUSERID IS NOT NULL AND mes.USERID = :I_PAGEUSERID) OR (:I_PAGEUSERID IS NULL)) ),
        (SELECT COUNT(1) FROM {objectQualifier}MESSAGE x WHERE x.TOPICID=c.TOPICID and x.ISDELETED = 0) - 1,
        c.VIEWS,
        c.LASTPOSTED,
        c.LASTUSERID,
        COALESCE(c.LASTUSERNAME,(SELECT X.NAME FROM {objectQualifier}USER x WHERE x.USERID=c.LASTUSERID)),
        COALESCE(c.LASTUSERDISPLAYNAME,(SELECT X.DISPLAYNAME FROM {objectQualifier}USER x WHERE x.USERID=c.LASTUSERID)),
        c.LASTMESSAGEID,
        c.LASTMESSAGEFLAGS,	
        c.TOPICID AS LastTopicID,
        c.FLAGS,
        (SELECT COUNT(ID) as FAVORITECOUNT FROM {objectQualifier}FAVORITETOPIC WHERE TOPICID = COALESCE(c.TOPICMOVEDID,c.TOPICID)),
        c.PRIORITY,
        c.POLLID,
        d.NAME,			
        d.FLAGS,
        (SELECT FIRST 1 CAST(mes2.MESSAGE as varchar(1000)) FROM {objectQualifier}MESSAGE mes2 where mes2.TOPICID = COALESCE(c.TOPICMOVEDID,c.TOPICID) AND mes2."POSITION" = 0),
        (case(:I_STYLEDNICKS)
            when 1 then  b.USERSTYLE 
            else ''	 end) as StarterStyle ,
        (case(:I_STYLEDNICKS)
            when 1 then  (SELECT FIRST 1 usr.USERSTYLE FROM {objectQualifier}USER usr WHERE usr.USERID = c.LASTUSERID)
            else ''	 end) as LastUserStyle,
       (case(:I_FINDLASTUNREAD)
             when 1 then
               (SELECT FIRST 1 LASTACCESSDATE FROM {objectQualifier}FORUMREADTRACKING x WHERE x.FORUMID=d.FORUMID AND x.USERID = :I_PAGEUSERID)
             else (select dateadd(1 day to current_timestamp)  FROM RDB$DATABASE) end) AS "LastForumAccess",
        (case(:I_FINDLASTUNREAD)
             when 1 then
               (SELECT FIRST 1 LASTACCESSDATE FROM {objectQualifier}TOPICREADTRACKING y WHERE y.TOPICID = c.TOPICID AND y.USERID = :I_PAGEUSERID)
             else (select dateadd(1 day to current_timestamp)  FROM RDB$DATABASE)  end) AS  "LastTopicAccess",
        (SELECT * FROM {objectQualifier}TOPIC_GETTAGS_STR(c.TopicID)),			
	    c.TOPICIMAGE,
	    c.TOPICIMAGETYPE,
	    c.TOPICIMAGEBIN ,
	    (SELECT 0 FROM RDB$DATABASE),
        (SELECT :ici_topics_totalrowsnumber FROM RDB$DATABASE) AS "TotalRows",
        (SELECT :i_PageIndex  FROM RDB$DATABASE) AS  "PageIndex" 	 
    from
        {objectQualifier}TOPIC c
        join {objectQualifier}USER b on b.USERID=c.USERID
        join {objectQualifier}FORUM d on d.FORUMID=c.FORUMID
        join {objectQualifier}ACTIVEACCESS x on x.FORUMID=d.FORUMID
        join {objectQualifier}CATEGORY cat on cat.CATEGORYID=d.CATEGORYID
            
    WHERE
        c.LASTPOSTED <= :ici_firstselectposted AND
    x.USERID = :I_PAGEUSERID and
        x.READACCESS <> 0 and
        cat.BOARDID = :I_BOARDID and
        (:I_CATEGORYID is null or cat.CATEGORYID = :I_CATEGORYID) and
        c.ISDELETED = 0 and	
        c.TOPICMOVEDID is null and
        c.NUMPOSTS = 1
    ORDER BY
    c.LASTPOSTED DESC,
    cat.SORTORDER,
    d.SORTORDER,
    d.NAME DESC,
    c."PRIORITY" DESC
    
    INTO 
    :"ForumID",
    :"TopicID",
    :"TopicMovedID",
    :"Posted",
    :"LinkTopicID",
    :"Subject",
    :"Status",
    :"Styles",
    :"Description",
    :"UserID",
    :"Starter",
    :"StarterDisplay",
    :"NumPostsDeleted",
    :"Replies",
    :"Views",
    :"LastPosted",
    :"LastUserID",
    :"LastUserName",
    :"LastUserDisplayName",
    :"LastMessageID",
    :"LastMessageFlags",
    :"LastTopicID",
    :"TopicFlags",
    :"FavoriteCount",
    :"Priority",
    :"PollID",
    :"ForumName",	
    :"ForumFlags",
    :"FirstMessage",
    :"StarterStyle",
    :"LastUserStyle",
    :"LastForumAccess",
    :"LastTopicAccess",
	:"TopicTags",
	:"TopicImage",
	:"TopicImageType",
	:"TopicImageBin",
	:"HasAttachments",
    :"TotalRows",
    :"PageIndex"	     
DO    
  BEGIN
   ici_retcount = :ici_retcount +1;
   if (:ici_retcount between  :ici_firstselectrownum and :ici_firstselectrownum + :I_PAGESIZE) then
    begin
    SUSPEND;
    ici_counter = :ici_counter + 1; 
    end 
if (:ici_counter >= :I_PAGESIZE) then
LEAVE;						
  END

END;
--GO

CREATE PROCEDURE {objectQualifier}TOPICS_BYUSER (
    I_BOARDID integer,
    I_CATEGORYID integer,
    I_PAGEUSERID integer,
    I_SINCEDATE timestamp,
    I_TODATE timestamp,
    I_PAGEINDEX integer,
    I_PAGESIZE integer,
    I_STYLEDNICKS BOOL,
    I_FINDLASTUNREAD BOOL,
	I_UTCTIMESTAMP timestamp )
RETURNS (
  "ForumID" INTEGER,
        "TopicID" INTEGER,
        "TopicMovedID" INTEGER,
        "Posted" TIMESTAMP,
        "LinkTopicID" INTEGER,
        "Subject" VARCHAR(255),
        "Description" VARCHAR(255),
        "Status" VARCHAR(255),
        "Styles" VARCHAR(255),
        "UserID" INTEGER,
        "Starter" VARCHAR(255),
        "StarterDisplay" VARCHAR(255),
        "NumPostsDeleted" INTEGER,
        "Replies" INTEGER,
        "Views" INTEGER,
        "LastPosted" TIMESTAMP, 
        "LastUserID" INTEGER,
        "LastUserName" VARCHAR(255),
        "LastUserDisplayName" VARCHAR(255),
        "LastMessageID" INTEGER,
        "LastMessageFlags" INTEGER,
        "LastTopicID" INTEGER,
        "TopicFlags" INTEGER,
        "FavoriteCount" INTEGER,
        "Priority" INTEGER,
        "PollID" INTEGER,
        "ForumName" VARCHAR(255),	
        "ForumFlags" INTEGER,
        "FirstMessage" VARCHAR(1024) ,
        "StarterStyle" VARCHAR(255),
        "LastUserStyle" VARCHAR(255),
        "LastForumAccess" TIMESTAMP,
        "LastTopicAccess" TIMESTAMP,
			"TopicTags" varchar(4000),
"TopicImage" varchar(255),
"TopicImageType" varchar(50),
"TopicImageBin" blob sub_type 0,
"HasAttachments" integer,   
"TotalRows" integer ,
"PageIndex" integer)
AS
DECLARE VARIABLE ici_RowTotalCount INTEGER DEFAULT 0; 
 DECLARE VARIABLE cnt INTEGER DEFAULT 1;
 DECLARE VARIABLE ici_topics_totalrowsnumber INTEGER; 
 DECLARE VARIABLE ici_firstselectrownum INTEGER DEFAULT 0;  
 DECLARE VARIABLE ici_firstselectposted timestamp; 
 DECLARE VARIABLE ici_retcount INTEGER DEFAULT 0; 
 DECLARE VARIABLE ici_counter INTEGER DEFAULT 0;
BEGIN  


SELECT 		
        COUNT(1) 
    FROM
        {objectQualifier}TOPIC c
        JOIN {objectQualifier}USER b ON b.USERID=c.USERID
        JOIN {objectQualifier}FORUM d ON d.FORUMID=c.FORUMID
        JOIN {objectQualifier}ACTIVEACCESS x ON x.FORUMID=d.FORUMID
        JOIN {objectQualifier}CATEGORY cat ON cat.CATEGORYID=d.CATEGORYID	
    where
        (c.LASTPOSTED BETWEEN :I_SINCEDATE AND :I_TODATE)  and
        x.UserID =: I_PAGEUSERID and
        x.ReadAccess <> 0 and
        cat.BOARDID = :I_BOARDID and
        (:I_CATEGORYID is null or cat.CATEGORYID=:I_CATEGORYID) and
        c.ISDELETED = 0
        and	c.TOPICMOVEDID is null
        and c.TOPICID = (SELECT FIRST 1 mess.TOPICID FROM {objectQualifier}MESSAGE mess WHERE mess.USERID=:I_PAGEUSERID AND mess.TOPICID=c.TOPICID)
          INTO :ici_topics_totalrowsnumber;      


      I_PAGEINDEX = :I_PAGEINDEX+1;   
      ici_firstselectrownum = ((:I_PAGEINDEX-1)*:I_PAGESIZE) + 1;
    
                    
   SELECT FIRST 1
        c.LASTPOSTED			
    from
        {objectQualifier}TOPIC c
        join {objectQualifier}USER b on b.USERID=c.USERID
        join {objectQualifier}FORUM d on d.FORUMID=c.FORUMID
        join {objectQualifier}ACTIVEACCESS x on x.FORUMID=d.FORUMID
        join {objectQualifier}CATEGORY cat on cat.CATEGORYID=d.CATEGORYID
    where
        (c.LASTPOSTED BETWEEN :I_SINCEDATE AND :I_TODATE)  and
        x.UserID =: I_PAGEUSERID and
        x.ReadAccess <> 0 and
        cat.BOARDID = :I_BOARDID and
        (:I_CATEGORYID is null or cat.CATEGORYID=:I_CATEGORYID) and
        c.ISDELETED = 0
        and	c.TOPICMOVEDID is null
        and c.TOPICID = (SELECT FIRST 1 mess.TOPICID FROM {objectQualifier}MESSAGE mess WHERE mess.USERID=:I_PAGEUSERID AND mess.TOPICID=c.TOPICID)
    order by
        c.LASTPOSTED DESC,		
        cat.SORTORDER ASC,
        d.SORTORDER ASC,
        d.NAME ASC,
        PRIORITY DESC
        into :ici_firstselectposted;
            
    SELECT :ici_firstselectposted from RDB$DATABASE
    INTO :ici_firstselectposted;
    

  
FOR SELECT				
        c.FORUMID,
        c.TOPICID,
        c.TOPICMOVEDID,
        c.POSTED,
        COALESCE(c.TOPICMOVEDID,c.TOPICID),
        c.TOPIC,
        c.DESCRIPTION,
        c.STATUS,
        c.STYLES,
        c.USERID,
        COALESCE(c.USERNAME,b.NAME),
        COALESCE(c.USERDISPLAYNAME,b.DISPLAYNAME),
        (SELECT COUNT(1) FROM {objectQualifier}MESSAGE mes WHERE mes.TOPICID = c.TOPICID AND mes.ISDELETED = 1 AND mes.ISAPPROVED = 1 AND ((:I_PAGEUSERID IS NOT NULL AND mes.USERID = :I_PAGEUSERID) OR (:I_PAGEUSERID IS NULL)) ),
        (select count(1)-1 from {objectQualifier}MESSAGE x where x.TOPICID=c.TOPICID and BIN_AND(x.Flags,8)<> 8),
        c.VIEWS,
        c.LASTPOSTED,
        c.LASTUSERID,
        COALESCE(c.LASTUSERNAME,(select NAME from {objectQualifier}USER x where x.USERID=c.LASTUSERID)),
        COALESCE(c.LASTUSERDISPLAYNAME,(select DISPLAYNAME from {objectQualifier}USER x where x.USERID=c.LASTUSERID)),
        c.LASTMESSAGEID,
        c.LASTMESSAGEFLAGS,
        c.TOPICID,
        c.FLAGS,
        (SELECT COUNT(ID) FROM {objectQualifier}FAVORITETOPIC WHERE TOPICID = COALESCE(c.TOPICMOVEDID,c.TOPICID)),
        c.PRIORITY,
        c.POLLID,
        d.NAME,	
        d.FLAGS,
        (SELECT FIRST 1 SUBSTRING(mes2.MESSAGE FROM 1 FOR 1024) FROM {objectQualifier}MESSAGE mes2 where mes2.TOPICID = COALESCE(c.TOPICMOVEDID,c.TOPICID) AND mes2."POSITION" = 0),
        (case(:I_STYLEDNICKS)
            when 1 then  b.USERSTYLE
            else ''	 end),
        (case(:I_STYLEDNICKS)
            when 1 then  (SELECT FIRST 1 usr.USERSTYLE FROM {objectQualifier}USER usr WHERE usr.USERID = c.LASTUSERID)
            else ''	 end),
        (case(:I_FINDLASTUNREAD)
             when 1 then
               (SELECT FIRST 1  LASTACCESSDATE FROM {objectQualifier}FORUMREADTRACKING x WHERE x.FORUMID=d.FORUMID AND x.USERID = :I_PAGEUSERID)
             else (select dateadd(1 day to current_timestamp) FROM RDB$DATABASE) 	 end),
        (case(:I_FINDLASTUNREAD)
             when 1 then
               (SELECT FIRST 1 LASTACCESSDATE FROM {objectQualifier}TOPICREADTRACKING y WHERE y.TOPICID=c.TOPICID AND y.USERID = :I_PAGEUSERID)
             else (select dateadd(1 day to current_timestamp) FROM RDB$DATABASE)	 end),
        (SELECT * FROM {objectQualifier}TOPIC_GETTAGS_STR(c.TopicID)),			
	    c.TOPICIMAGE,
	    c.TOPICIMAGETYPE,
	    c.TOPICIMAGEBIN ,
	    (SELECT 0 FROM RDB$DATABASE),
        (SELECT :ici_topics_totalrowsnumber FROM RDB$DATABASE) AS "TotalRows",
        (SELECT :i_PageIndex  FROM RDB$DATABASE) AS  "PageIndex" 	 
    from
        {objectQualifier}TOPIC c
        join {objectQualifier}USER b on b.USERID=c.USERID
        join {objectQualifier}FORUM d on d.FORUMID=c.FORUMID
        join {objectQualifier}ACTIVEACCESS x on x.FORUMID=d.FORUMID
        join {objectQualifier}CATEGORY cat on cat.CATEGORYID=d.CATEGORYID
    where
        c.LASTPOSTED <= :ici_firstselectposted  and
        x.UserID =: I_PAGEUSERID and
        x.ReadAccess <> 0 and
        cat.BOARDID = :I_BOARDID and
        (:I_CATEGORYID is null or cat.CATEGORYID=:I_CATEGORYID) and
        c.ISDELETED = 0
        and	c.TOPICMOVEDID is null
        and c.TOPICID = (SELECT FIRST 1 mess.TOPICID FROM {objectQualifier}MESSAGE mess WHERE mess.USERID=:I_PAGEUSERID AND mess.TOPICID=c.TOPICID)
    order by
        c.LASTPOSTED DESC,		
        cat.SORTORDER ASC,
        d.SORTORDER ASC,
        d.NAME ASC,
        PRIORITY DESC	
    INTO 
    :"ForumID",
        :"TopicID",
        :"TopicMovedID",
        :"Posted",
        :"LinkTopicID",
        :"Subject",
        :"Description",
        :"Status",
        :"Styles",
        :"UserID",
        :"Starter",
        :"StarterDisplay",
        :"NumPostsDeleted",
        :"Replies",
        :"Views",
        :"LastPosted",
        :"LastUserID",
        :"LastUserName",
        :"LastUserDisplayName",
        :"LastMessageID",
        :"LastMessageFlags",
        :"LastTopicID",
        :"TopicFlags",
        :"FavoriteCount",
        :"Priority",
        :"PollID",
        :"ForumName",	
        :"ForumFlags",
        :"FirstMessage",
        :"StarterStyle" ,
        :"LastUserStyle",
        :"LastForumAccess",
        :"LastTopicAccess",
		 :"TopicTags",
	:"TopicImage",
	:"TopicImageType",
	:"TopicImageBin",
	:"HasAttachments",
        :"TotalRows",
        :"PageIndex" 
DO	
    
  BEGIN
   ici_retcount = :ici_retcount +1;
   if (:ici_retcount between  :ici_firstselectrownum  and :ici_firstselectrownum + :I_PAGESIZE ) then
    begin
    SUSPEND;
    ici_counter = :ici_counter + 1; 
    end 
if (:ici_counter >= :I_PAGESIZE) then
LEAVE;						
  END

END;
--GO

CREATE PROCEDURE {objectQualifier}TOPIC_FAVORITE_DETAILS (
      I_BOARDID integer,
    I_CATEGORYID integer,
    I_PAGEUSERID integer,
    I_SINCEDATE timestamp,
    I_TODATE timestamp,
    I_PAGEINDEX integer,
    I_PAGESIZE integer,
    I_STYLEDNICKS BOOL,
    I_FINDLASTUNREAD BOOL,
	I_UTCTIMESTAMP timestamp )
RETURNS (
  "ForumID" integer,
"TopicID" integer,
"Posted" timestamp,
"LinkTopicID" integer,
"Subject" varchar(255),
"Status" varchar(255),
"Styles" varchar(255),
"Description" varchar(255),
"UserID" integer,
"Starter"  varchar(128),
"StarterDisplay"  varchar(128),
"NumPostsDeleted" integer,
"Replies" integer,
"Views" integer,
"LastPosted" timestamp,
"LastUserID" integer,
"LastUserName" varchar(255),
"LastUserDisplayName" varchar(255),
"LastMessageID" integer,
"LastMessageFlags" integer,
"LastTopicID" integer,
"TopicFlags" integer,
"FavoriteCount" integer, 
"Priority" integer,
"PollID" integer,
"ForumName" varchar(128),
"TopicMovedID" integer,
"ForumFlags" integer,
"FirstMessage" varchar(1024),
"StarterStyle" varchar(255),
"LastUserStyle" varchar(255),
"LastForumAccess" varchar(255),
"LastTopicAccess" varchar(255),
"TopicTags" varchar(4000),
"TopicImage" varchar(255),
"TopicImageType" varchar(50),
"TopicImageBin" blob sub_type 0,
"HasAttachments" integer,
"TotalRows" integer,
"PageIndex" integer)
AS
DECLARE VARIABLE ici_RowTotalCount INTEGER DEFAULT 0; 
 DECLARE VARIABLE cnt INTEGER DEFAULT 1;
 DECLARE VARIABLE ici_topics_totalrowsnumber INTEGER; 
 DECLARE VARIABLE ici_firstselectrownum INTEGER DEFAULT 0;  
 DECLARE VARIABLE ici_firstselectposted timestamp; 
 DECLARE VARIABLE ici_retcount INTEGER DEFAULT 0; 
 DECLARE VARIABLE ici_counter INTEGER DEFAULT 0;
BEGIN  


SELECT 		
        COUNT(1) 
        from
        {objectQualifier}TOPIC c
        join {objectQualifier}USER b on b.USERID=c.USERID
        join {objectQualifier}FORUM d on d.FORUMID=c.FORUMID
        join {objectQualifier}VACCESS x on x.FORUMID=d.FORUMID
        join {objectQualifier}CATEGORY e on e.CATEGORYID=d.CATEGORYID
        JOIN {objectQualifier}FAVORITETOPIC z ON z.TOPICID=c.TOPICID AND z.USERID=:I_PAGEUSERID
    where
        (c.LASTPOSTED BETWEEN :I_SINCEDATE AND :I_TODATE)  and
        x.USERID = :I_PAGEUSERID and
        x.READACCESS <> 0 and
        e.BOARDID = :I_BOARDID and
        (:I_CATEGORYID is null or e.CATEGORYID=:I_CATEGORYID) and
        c.ISDELETED = 0
        into :ici_topics_totalrowsnumber;      


      I_PAGEINDEX = :I_PAGEINDEX+1;   
      ici_firstselectrownum = ((:I_PAGEINDEX-1)*:I_PAGESIZE) + 1;
    
                    
   SELECT FIRST 1
        c.LASTPOSTED			
    from
        {objectQualifier}TOPIC c
        join {objectQualifier}USER b on b.USERID=c.USERID
        join {objectQualifier}FORUM d on d.FORUMID=c.FORUMID
        join {objectQualifier}VACCESS x on x.FORUMID=d.FORUMID
        join {objectQualifier}CATEGORY e on e.CATEGORYID=d.CATEGORYID
        JOIN {objectQualifier}FAVORITETOPIC z ON z.TOPICID=c.TOPICID AND z.USERID=:I_PAGEUSERID
    where
        (c.LASTPOSTED BETWEEN :I_SINCEDATE AND :I_TODATE) and
        x.USERID = :I_PAGEUSERID and
        x.READACCESS <> 0 and
        e.BOARDID = :I_BOARDID and
        (:I_CATEGORYID is null or e.CATEGORYID=:I_CATEGORYID) and
        c.ISDELETED = 0
    order by
        c.LASTPOSTED desc,
        d.NAME asc,
        c.PRIORITY desc
        into :ici_firstselectposted;
            
    SELECT :ici_firstselectposted from RDB$DATABASE
    INTO :ici_firstselectposted;
    

  
FOR SELECT				
        c.FORUMID,
        c.TOPICID,
        c.POSTED,
        COALESCE(c.TOPICMOVEDID,c.TOPICID) AS LinkTopicID,
        c.TOPIC AS Subject,
        c.STATUS,
        c.STYLES,
        c.DESCRIPTION,
        c.USERID,
        COALESCE(c.USERNAME,b.NAME) as Starter,
        COALESCE(c.USERDISPLAYNAME,b.DISPLAYNAME) as StarterDisplay,
        (SELECT COUNT(1) FROM {objectQualifier}MESSAGE mes WHERE mes.TOPICID = c.TOPICID AND mes.ISDELETED = 1 AND mes.ISAPPROVED = 1 AND ((:I_PAGEUSERID IS NOT NULL AND mes.USERID = :I_PAGEUSERID) OR (:I_PAGEUSERID IS NULL)) ) as NumPostsDeleted,
        (SELECT COUNT(1) FROM {objectQualifier}MESSAGE x WHERE x.TOPICID=c.TOPICID AND BIN_AND(x.FLAGS,8)=0) - 1 AS Replies,
        c.VIEWS AS Views,
        c.LASTPOSTED as LastPosted ,
        c.LASTUSERID as LastUserID,
        COALESCE(c.LASTUSERNAME,(select x.NAME from {objectQualifier}USER x where x.USERID=c.LASTUSERID)) as LastUserName,
        COALESCE(c.LASTUSERDISPLAYNAME,(select x.DISPLAYNAME from {objectQualifier}USER x where x.USERID=c.LASTUSERID)) as LastUserDisplayName,
        c.LASTMESSAGEID AS LastMessageID,
        c.LASTMESSAGEFLAGS,
        c.TOPICID AS LastTopicID,
        c.FLAGS as TopicFlags,
        (SELECT COUNT(ID) FROM  {objectQualifier}FAVORITETOPIC WHERE TOPICID = COALESCE(c.TOPICMOVEDID,c.TOPICID)) AS FAVORITECOUNT,
        c.PRIORITY,
        c.POLLID,
        d.NAME AS ForumName,
        c.TOPICMOVEDID,
        d.FLAGS AS ForumFlags,
        (SELECT FIRST 1 SUBSTRING(mes2.MESSAGE FROM 1 FOR 1024) FROM {objectQualifier}MESSAGE mes2 where mes2.TOPICID = COALESCE(c.TOPICMOVEDID,c.TOPICID) AND mes2."POSITION" = 0) AS FirstMessage,
        (case(:I_STYLEDNICKS)
            when 1 then  b.USERSTYLE 
            else ''	 end) AS StarterStyle,
        (case(:I_STYLEDNICKS)
            when 1 then (SELECT usr.USERSTYLE FROM {objectQualifier}USER usr WHERE usr.USERID = c.LASTUSERID)
            else ''	 end) AS LastUserStyle,
        (case(:I_FINDLASTUNREAD)
             when 1 then
               (SELECT FIRST 1  LASTACCESSDATE FROM {objectQualifier}FORUMREADTRACKING x WHERE x.FORUMID=d.FORUMID AND x.USERID = :I_PAGEUSERID)
             else (select dateadd(1 day to current_timestamp) FROM RDB$DATABASE) 	 end),
        (case(:I_FINDLASTUNREAD)
             when 1 then
               (SELECT FIRST 1 LASTACCESSDATE FROM {objectQualifier}TOPICREADTRACKING y WHERE y.TOPICID=c.TOPICID AND y.USERID = :I_PAGEUSERID)
             else (select dateadd(1 day to current_timestamp) FROM RDB$DATABASE)	 end),
        (SELECT * FROM {objectQualifier}TOPIC_GETTAGS_STR(c.TopicID)),			
	    c.TOPICIMAGE,
	    c.TOPICIMAGETYPE,
	    c.TOPICIMAGEBIN ,
	    (SELECT 0 FROM RDB$DATABASE),
        (SELECT :ici_topics_totalrowsnumber FROM RDB$DATABASE) AS "TotalRows",
        (SELECT :i_PageIndex  FROM RDB$DATABASE) AS  "PageIndex" 
    from
        {objectQualifier}TOPIC c
        join {objectQualifier}USER b on b.USERID=c.USERID
        join {objectQualifier}FORUM d on d.FORUMID=c.FORUMID
        join {objectQualifier}VACCESS x on x.FORUMID=d.FORUMID
        join {objectQualifier}CATEGORY e on e.CATEGORYID=d.CATEGORYID
        JOIN {objectQualifier}FAVORITETOPIC z ON z.TOPICID=c.TOPICID AND z.USERID=:I_PAGEUSERID
    where
        c.LASTPOSTED <= :ici_firstselectposted  and
        x.USERID = :I_PAGEUSERID and
        x.READACCESS <> 0 and
        e.BOARDID = :I_BOARDID and
        (:I_CATEGORYID is null or e.CATEGORYID=:I_CATEGORYID) and
        c.ISDELETED = 0
    order by
        c.LASTPOSTED desc,
        d.NAME asc,
        c.PRIORITY desc
    INTO 
    :"ForumID",
        :"TopicID",
        :"Posted",
        :"LinkTopicID",
        :"Subject",
        :"Status",
        :"Styles",
        :"Description",
        :"UserID",
        :"Starter",
        :"StarterDisplay",
        :"NumPostsDeleted",
        :"Replies",
        :"Views",
        :"LastPosted",
        :"LastUserID",
        :"LastUserName",
        :"LastUserDisplayName",
        :"LastMessageID",
        :"LastMessageFlags",
        :"LastTopicID",
        :"TopicFlags",
        :"FavoriteCount",
        :"Priority",
        :"PollID",
        :"ForumName",
        :"TopicMovedID",
        :"ForumFlags",
        :"FirstMessage",
        :"StarterStyle",
        :"LastUserStyle",
        :"LastForumAccess",
        :"LastTopicAccess",
		 :"TopicTags",
	:"TopicImage",
	:"TopicImageType",
	:"TopicImageBin",
	:"HasAttachments",
        :"TotalRows",
        :"PageIndex" 
DO	
    
  BEGIN
   ici_retcount = :ici_retcount +1;
   if (:ici_retcount between  :ici_firstselectrownum  and :ici_firstselectrownum + :I_PAGESIZE) then
    begin
    SUSPEND;
    ici_counter = :ici_counter + 1; 
    end 
if (:ici_counter >= :I_PAGESIZE) then
LEAVE;						
  END

END;
--GO

CREATE PROCEDURE  {objectQualifier}FORUM_TAGS(I_BOARDID INTEGER, I_PAGEUSERID INTEGER, I_FORUMID INTEGER, I_PAGEINDEX INTEGER, I_PAGESIZE INTEGER, I_SEARCHTEXT VARCHAR(255), I_BEGINSWITH BOOL) 
RETURNS
( "TagID" INTEGER,
  "Tag" VARCHAR(255),
  "TagCount" INTEGER,
  "MaxTagCount" INTEGER,
  "TotalCount" INTEGER
  )
AS
 DECLARE ICI_ALLCOUNT INTEGER;
 DECLARE VARIABLE ICI_TAGS_TOTALROWSNUMBER INTEGER; 
 DECLARE VARIABLE ici_firstselectrownum INTEGER DEFAULT 0;  
 DECLARE VARIABLE ici_lastselectrownum INTEGER DEFAULT 0; 
begin
IF (I_FORUMID = 0 ) THEN I_FORUMID = NULL;
SELECT 
           MAX(tg.TAGCOUNT)  
           FROM {objectQualifier}TAGS tg 
           JOIN {objectQualifier}TOPICTAGS tt ON tt.TAGID = tg.TAGID 
           JOIN {objectQualifier}TOPIC t ON tt.TOPICID = t.TOPICID
           JOIN {objectQualifier}ACTIVEACCESS aa ON (aa.FORUMID = t.FORUMID AND aa.USERID = :I_PAGEUSERID)
           WHERE aa.BoardID=:I_BOARDID and (:I_FORUMID IS NULL OR t.FORUMID=:I_FORUMID) AND BIN_AND(t.FLAGS, 8) != 8
           AND (LOWER(tg.TAG) LIKE (CASE 
            WHEN (:I_BEGINSWITH = 0 AND :I_SEARCHTEXT IS NOT NULL AND CHAR_LENGTH(:I_SEARCHTEXT) > 0) THEN ('%' || LOWER(:I_SEARCHTEXT) || '%')   
            WHEN (:I_BEGINSWITH = 1 AND :I_SEARCHTEXT IS NOT NULL AND CHAR_LENGTH(:I_SEARCHTEXT) > 0) THEN (LOWER(:I_SEARCHTEXT) || '%')                
            ELSE '%' END))  INTO :ICI_ALLCOUNT;

SELECT 		

        COUNT(DISTINCT(tg.TAGID)) 
         FROM {objectQualifier}TAGS tg 
           JOIN {objectQualifier}TOPICTAGS tt ON tt.TAGID = tg.TAGID 
           JOIN {objectQualifier}TOPIC t ON tt.TOPICID = t.TOPICID
           JOIN {objectQualifier}ACTIVEACCESS aa ON (aa.FORUMID = t.ForumID AND aa.USERID = :I_PAGEUSERID)
           WHERE aa.BOARDID=:I_BOARDID and (:I_FORUMID IS NULL OR t.FORUMID=:I_FORUMID) AND BIN_AND(t.FLAGS, 8) != 8  
             AND (LOWER(tg.TAG) LIKE (CASE 
            WHEN (:I_BEGINSWITH = 0 AND :I_SEARCHTEXT IS NOT NULL AND CHAR_LENGTH(:I_SEARCHTEXT) > 0) THEN ('%' || LOWER(:I_SEARCHTEXT) || '%')   
            WHEN (:I_BEGINSWITH = 1 AND :I_SEARCHTEXT IS NOT NULL AND CHAR_LENGTH(:I_SEARCHTEXT) > 0) THEN (LOWER(:I_SEARCHTEXT) || '%') 
			ELSE '%' END))               
        INTO :ICI_TAGS_TOTALROWSNUMBER; 

      I_PAGEINDEX = :I_PAGEINDEX+1;   
      ici_firstselectrownum = ((:I_PAGEINDEX-1)*:I_PAGESIZE) + 1;	           
	  ici_lastselectrownum = (:I_PAGESIZE + :ici_firstselectrownum - 1); 
    
FOR	SELECT 
           DISTINCT(tg.tagid),
           tg.Tag,
           tg.TAGCOUNT,
           (SELECT :ICI_ALLCOUNT FROM RDB$DATABASE) ,
           (SELECT :ICI_TAGS_TOTALROWSNUMBER FROM RDB$DATABASE)
           FROM {objectQualifier}TAGS tg 
           JOIN {objectQualifier}TOPICTAGS tt ON tt.TAGID = tg.TAGID 
           JOIN {objectQualifier}TOPIC t ON tt.TOPICID = t.TOPICID
           JOIN {objectQualifier}ACTIVEACCESS aa ON (aa.FORUMID = t.FORUMID AND aa.USERID = :I_PAGEUSERID)
            WHERE aa.BoardID=:I_BOARDID and (:I_FORUMID IS NULL OR t.FORUMID=:I_FORUMID) AND BIN_AND(t.FLAGS, 8) != 8
           AND (LOWER(tg.TAG) LIKE CASE 
            WHEN (:I_BEGINSWITH = 0 AND :I_SEARCHTEXT IS NOT NULL AND CHAR_LENGTH(:I_SEARCHTEXT) > 0) THEN '%' || LOWER(:I_SEARCHTEXT) || '%'   
            WHEN (:I_BEGINSWITH = 1 AND :I_SEARCHTEXT IS NOT NULL AND CHAR_LENGTH(:I_SEARCHTEXT) > 0) THEN LOWER(:I_SEARCHTEXT) || '%'                        
            ELSE '%' END)
           ORDER BY tg.TAG ROWS :ici_firstselectrownum TO :ici_lastselectrownum
           INTO
           :"TagID",
           :"Tag",
           :"TagCount",
           :"MaxTagCount",
           :"TotalCount"
          DO SUSPEND;  
end;
--GO

