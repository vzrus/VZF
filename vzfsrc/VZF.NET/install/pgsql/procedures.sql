-- This scripts for PostgreSQL Yet Another Forum https://github.com/vzrus/YetAnotherForumExtraDataLayers http://sourceforge.net/projects/yafdotnet/
-- were created by vzrus from vz-team  https://github.com/vzrus
-- They are distributed under terms of GPLv2 licence only as in http://www.fsf.org/licensing/licenses/gpl.html
-- Copyright vzrus(c) 2009-2012

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}user_savestyle
						   (
						   i_groupid integer, 
						   i_rankid integer
						   ) 
				  RETURNS void AS
$BODY$DECLARE
			_usridtmp int;
			_styletmp varchar(255);
			_rankidtmp int;
			_rec RECORD;
				  
BEGIN
FOR _rec IN select userid,userstyle,rankid from {databaseSchema}.{objectQualifier}user
		 LOOP 
			 UPDATE {databaseSchema}.{objectQualifier}user 
			 SET userstyle = 
			 COALESCE(
			 (SELECT f.style FROM {databaseSchema}.{objectQualifier}usergroup e 
			  join {databaseSchema}.{objectQualifier}group f on f.groupid=e.groupid WHERE e.userid= _rec.userid AND f.style is not null ORDER BY f.sortorder limit 1), 
			 (SELECT r.style FROM {databaseSchema}.{objectQualifier}rank r where rankid =  _rec.rankid)
					 )   
			  WHERE userid = _rec.userid;
		 END LOOP;
END;$BODY$
	LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER COST 100;  
--GO
SELECT {databaseSchema}.{objectQualifier}user_savestyle(null, null);
--GO
CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}vaccess_combo
						   (
						   i_userid integer, 
						   i_forumid integer
						   )
				  RETURNS SETOF {databaseSchema}.{objectQualifier}vaccess_combo_return_type 
AS
$BODY$DECLARE
			 _rec {databaseSchema}.{objectQualifier}vaccess_combo_return_type%ROWTYPE;

			 ici_userid integer;
			 ici_ForumID integer;
			 ici_IsAdmin integer  DEFAULT 0;
			 ici_IsForumModerator integer  DEFAULT 0;
			 ici_IsModerator integer DEFAULT 0;
			 ici_ReadAccess  integer DEFAULT 0;
			 ici_PostAccess integer DEFAULT 0;
			 ici_ReplyAccess integer DEFAULT 0;
			 ici_PriorityAccess integer DEFAULT 0;
			 ici_PollAccess integer DEFAULT 0;
			 ici_VoteAccess integer DEFAULT 0;
			 ici_ModeratorAccess integer DEFAULT 0;
			 ici_EditAccess integer DEFAULT 0;
			 ici_DeleteAccess integer DEFAULT 0;
			 ici_UploadAccess integer DEFAULT 0;
			 ici_DownloadAccess integer DEFAULT 0;
			 ici_UserForumAccess integer DEFAULT 0;
			 out_UserID integer;
			 out_ForumID integer;
			 out_IsAdmin integer DEFAULT 0;
			 out_IsForumModerator integer DEFAULT 0;
			 out_IsModerator integer DEFAULT 0;
			 out_ReadAccess integer DEFAULT 0;
			 out_PostAccess integer DEFAULT 0;
			 out_ReplyAccess integer DEFAULT 0;
			 out_PriorityAccess integer DEFAULT 0;
			 out_PollAccess integer DEFAULT 0;
			 out_VoteAccess integer DEFAULT 0;
			 out_ModeratorAccess integer DEFAULT 0;
			 out_EditAccess integer DEFAULT 0;
			 out_DeleteAccess integer DEFAULT 0;
			 out_UploadAccess integer DEFAULT 0;
			 out_DownloadAccess integer DEFAULT 0;
			 out_UserForumAccess integer DEFAULT 0;
BEGIN
SELECT
	  COALESCE(userid,i_userid),
	  COALESCE(forumid,0),
	  COALESCE(ReadAccess,0), 
	  COALESCE(PostAccess,0),
	  COALESCE(ReplyAccess,0),
	  COALESCE(PriorityAccess,0),
	  COALESCE(PollAccess,0),
	  COALESCE(VoteAccess,0),
	  COALESCE(ModeratorAccess,0),
	  COALESCE(EditAccess,0),
	  COALESCE(DeleteAccess,0),
	  COALESCE(UploadAccess,0),
	  COALESCE(DownloadAccess,0),
	  COALESCE(UserForumAccess,0)	       
	  INTO
	  ici_userid,	  
	  ici_ForumID,
	  ici_ReadAccess,
	  ici_PostAccess,
	  ici_ReplyAccess,
	  ici_PriorityAccess,
	  ici_PollAccess,
	  ici_VoteAccess,
	  ici_ModeratorAccess, 
	  ici_EditAccess,
	  ici_DeleteAccess,
	  ici_UploadAccess,
	  ici_DownloadAccess,
	  ici_UserForumAccess
	  FROM
	  {databaseSchema}.{objectQualifier}vaccessfull1
	  WHERE userid=i_userid
		AND forumid = COALESCE(i_forumid,0) LIMIT 1; 
 
	  SELECT
			COALESCE(userid,i_UserID),
			COALESCE(forumid,0),
			COALESCE(ReadAccess,0),
			COALESCE(PostAccess,0),
			COALESCE(ReplyAccess,0),
			COALESCE(PriorityAccess,0),
			COALESCE(PollAccess,0),
			COALESCE(VoteAccess,0),
			COALESCE(ModeratorAccess,0),
			COALESCE(EditAccess,0),
			COALESCE(DeleteAccess,0),
			COALESCE(UploadAccess,0),
			COALESCE(DownloadAccess,0),
			COALESCE(UserForumAccess,0)			
		INTO
			out_UserID,
			out_ForumID,
			out_ReadAccess,
			out_PostAccess,
			out_ReplyAccess,
			out_PriorityAccess,
			out_PollAccess,
			out_VoteAccess,
			out_ModeratorAccess,
			out_EditAccess,
			out_DeleteAccess,
			out_UploadAccess,
			out_DownloadAccess,
			out_UserForumAccess
		FROM
		{databaseSchema}.{objectQualifier}vaccessfull2  
		WHERE userid=i_userid 
		  AND forumid = COALESCE(i_forumid,0) LIMIT 1;
  
 SELECT         
	   MAX(b.flags & 1),      
	   MAX(b.flags & 8) 
 INTO  out_IsAdmin, out_IsForumModerator 
 FROM {databaseSchema}.{objectQualifier}usergroup a             
	  JOIN {databaseSchema}.{objectQualifier}group b
	  ON b.groupid = a.groupid
	  WHERE a.userid=i_userid LIMIT 1;
 
FOR _rec IN SELECT
				  i_userid AS UserID,
				  COALESCE(i_forumid,0) AS ForumID,
				  COALESCE(out_IsAdmin,0) AS IsAdmin,
				  COALESCE(out_IsForumModerator,0) AS IsForumModerator,
				  (SELECT     COUNT(v.userid) AS Expr1
				  FROM          {databaseSchema}.{objectQualifier}usergroup AS v 
				  INNER JOIN    {databaseSchema}.{objectQualifier}group AS w 
				  ON v.groupid = w.groupid
				  CROSS JOIN  {databaseSchema}.{objectQualifier}forumaccess AS x 
				  CROSS JOIN  {databaseSchema}.{objectQualifier}accessmask AS y
				  WHERE (v.userid = i_userid)
						 AND (x.groupid = w.groupid)
						 AND (y.accessmaskid = x.accessmaskid)
						 AND (y.flags & 64 <> 0)) AS IsModerator,
				  COALESCE(GREATEST(ici_ReadAccess,out_ReadAccess),0) AS ReadAccess,
				  COALESCE(GREATEST(ici_PostAccess,out_PostAccess),0) AS PostAccess,
				  COALESCE(GREATEST(ici_ReplyAccess,out_ReplyAccess),0) AS ReplyAccess,
				  COALESCE(GREATEST(ici_PriorityAccess,out_PriorityAccess),0) AS PriorityAccess,
				  COALESCE(GREATEST(ici_PollAccess,out_PollAccess),0) AS PollAccess,
				  COALESCE(GREATEST(ici_VoteAccess,out_VoteAccess),0) AS VoteAccess,
				  COALESCE(GREATEST(ici_ModeratorAccess,out_ModeratorAccess),0) AS ModeratorAccess,
				  COALESCE(GREATEST(ici_EditAccess,out_EditAccess),0) AS EditAccess,
				  COALESCE(GREATEST(ici_DeleteAccess,out_DeleteAccess),0) AS DeleteAccess,
				  COALESCE(GREATEST(ici_UploadAccess,out_UploadAccess),0) AS UploadAccess,
				  COALESCE(GREATEST(ici_DownloadAccess,out_DownloadAccess),0) AS DownloadAccess,
				  COALESCE(GREATEST(ici_UserForumAccess,out_UserForumAccess),0) AS UserForumAccess
			 LOOP
				 RETURN NEXT _rec;
			 END LOOP;
END;$BODY$
	LANGUAGE 'plpgsql' STABLE SECURITY DEFINER COST 100 ROWS 1000; 
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}vaccess_combo_rows
						   (
						   i_userid integer
						   )
				  RETURNS SETOF {databaseSchema}.{objectQualifier}vaccess_combo_return_type
AS
$BODY$DECLARE
			 _rec {databaseSchema}.{objectQualifier}vaccess_combo_return_type%ROWTYPE;

			 ici_userid integer;
			 ici_ForumID integer :=-1;
	  BEGIN
	  FOR _rec IN SELECT
						userid,
						forumid,
						isadmin,
						isforummoderator,
						ismoderator,
						readaccess,			
						postaccess,
						replyaccess,
						priorityaccess,
						pollaccess,
						voteaccess,
						moderatoraccess,
						editaccess,
						deleteaccess,
						uploadaccess,		
						downloadaccess,
						userforumaccess
				  FROM {databaseSchema}.{objectQualifier}vaccess 
				  where userid = i_userid
	  LOOP
	  -- SELECT first row from duplicates
	  IF ici_ForumID != _rec."ForumID" THEN
		 RETURN NEXT _rec;
		 ici_ForumID := _rec."ForumID";
	  END IF;
	  END LOOP;
END;$BODY$
	 LANGUAGE 'plpgsql' STABLE SECURITY DEFINER COST 100 ROWS 1000; 
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}accessmask_delete
						   (
						   i_accessmaskid integer
						   )
				  RETURNS integer 
AS
$BODY$
BEGIN
 IF EXISTS (SELECT 1 FROM {databaseSchema}.{objectQualifier}forumaccess
			WHERE  {databaseSchema}.{objectQualifier}forumaccess.accessmaskid = i_accessmaskid)
	OR EXISTS (SELECT 1 FROM   {databaseSchema}.{objectQualifier}userforum 
		   WHERE  {databaseSchema}.{objectQualifier}userforum.accessmaskid = i_accessmaskid) THEN
		   return 0::integer;
 ELSE
	DELETE FROM {databaseSchema}.{objectQualifier}accessmask 
	  WHERE       accessmaskid = i_accessmaskid;
 END IF;

RETURN 1::integer;

END;$BODY$
	LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER COST 100; 
 --GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}accessmask_list
						   (
						   i_boardid integer, 
						   i_accessmaskid integer,
						   i_excludeflags integer,
						   i_pageuserid integer,
						   i_isusermask boolean,
						   i_isadminmask boolean,
						   i_iscommonmask boolean,
						   i_pageindex integer,
						   i_pagesize integer
						   )
				  RETURNS SETOF {databaseSchema}.{objectQualifier}accessmask_list_return_type AS
$BODY$
DECLARE
	   _rec {databaseSchema}.{objectQualifier}accessmask_list_return_type%ROWTYPE;
	   ici_totalrows int = 0;
	   ici_firstselectrownumber int = 0;
BEGIN
 IF i_accessmaskid IS NULL THEN

		i_pageindex := i_pageindex + 1;
		 
		select count(1) into ici_totalrows 
		FROM      {databaseSchema}.{objectQualifier}accessmask a
		  WHERE    a.boardid = i_boardid  and
			(a.flags & i_excludeflags) = 0
			and (not i_iscommonmask or (not a.isusermask and not a.isadminmask)) 
			and (not i_isusermask or a.isusermask) 
			and (not i_isadminmask or a.isadminmask) 
			and (i_pageuserid is null or createdbyuserid = i_pageuserid);         
			
		ici_firstselectrownumber := (i_pageindex - 1) * i_pagesize;  
	 FOR _rec IN
		SELECT
			  a.accessmaskid,
			  a.boardid,
			  a.name,
			  a.flags,
			  a.sortorder,
			  a.isusermask,
			  a.isadminmask,
			  a.createdbyuserid,
			  a.createdbyusername,
			  a.createdbyuserdisplayname, 
			  ici_totalrows as TotalRows      
		  FROM      {databaseSchema}.{objectQualifier}accessmask a
		  WHERE    a.boardid = i_boardid  and
			(a.flags & i_excludeflags) = 0
			and (not i_iscommonmask or ( not a.isusermask and not a.isadminmask)) 
			and (not i_isusermask or a.isusermask) 
			and (not i_isadminmask or a.isadminmask) 
			and (i_pageuserid is null or createdbyuserid = i_pageuserid) 
		   ORDER BY a.sortorder OFFSET ici_firstselectrownumber LIMIT i_pagesize
	   LOOP
			 RETURN NEXT _rec;
	   END LOOP;
 ELSE
	FOR _rec IN
	   SELECT
			  a.accessmaskid,
			  a.boardid,
			  a.name,
			  a.flags,
			  a.sortorder,
			  a.isusermask,
			  a.isadminmask,
			  a.createdbyuserid,
			  a.createdbyusername,
			  a.createdbyuserdisplayname,
			  ici_totalrows as TotalRows
		FROM      {databaseSchema}.{objectQualifier}accessmask a
		 WHERE    a.boardid = i_boardid
		  AND a.accessmaskid = i_accessmaskid
	  LOOP
			 RETURN NEXT _rec;
	  END LOOP;
 END IF;     
END;$BODY$
	LANGUAGE 'plpgsql' STABLE SECURITY DEFINER COST 100 ROWS 1000;   
--GO


CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}accessmask_searchlist
						   (
						   i_boardid integer, 
						   i_accessmaskid integer,
						   i_excludeflags integer,
						   i_pageuserid integer,
						   i_isusermask boolean,
						   i_isadminmask boolean,
						   i_iscommonmask boolean,
						   i_pageindex integer,
						   i_pagesize integer
						   )
				  RETURNS SETOF {databaseSchema}.{objectQualifier}accessmask_list_return_type AS
$BODY$
DECLARE
	   _rec {databaseSchema}.{objectQualifier}accessmask_list_return_type%ROWTYPE;
	   ici_totalrows int = 0;
	   ici_firstselectrownumber int = 0;
BEGIN
 IF i_accessmaskid IS NULL THEN

		i_pageindex := i_pageindex + 1;
		 
		select count(1) into ici_totalrows 
		FROM      {databaseSchema}.{objectQualifier}accessmask a
		  WHERE    a.boardid = i_boardid  and
			(a.flags & i_excludeflags) = 0
			and (not i_iscommonmask or (not a.isusermask and not a.isadminmask)) 
			and (not i_isusermask or a.isusermask) 
			and (not i_isadminmask or a.isadminmask) 
			and (i_pageuserid is null or createdbyuserid = i_pageuserid);         
			
		ici_firstselectrownumber := (i_pageindex - 1) * i_pagesize;  
	 FOR _rec IN
		SELECT
			  a.accessmaskid,
			  a.boardid,
			  a.name,
			  a.flags,
			  a.sortorder,
			  a.isusermask,
			  a.isadminmask,
			  a.createdbyuserid,
			  a.createdbyusername,
			  a.createdbyuserdisplayname, 
			  ici_totalrows as TotalRows      
		  FROM      {databaseSchema}.{objectQualifier}accessmask a
		  WHERE    a.boardid = i_boardid  and
			(a.flags & i_excludeflags) = 0
			and (not i_iscommonmask or ( not a.isusermask and not a.isadminmask)) 
			and (not i_isusermask or a.isusermask) 
			and (not i_isadminmask or a.isadminmask) 
			and (i_pageuserid is null or createdbyuserid = i_pageuserid) 
		   ORDER BY a.sortorder OFFSET ici_firstselectrownumber LIMIT i_pagesize
	   LOOP
			 RETURN NEXT _rec;
	   END LOOP;
 ELSE
	FOR _rec IN
	   SELECT
			  a.accessmaskid,
			  a.boardid,
			  a.name,
			  a.flags,
			  a.sortorder,
			  a.isusermask,
			  a.isadminmask,
			  a.createdbyuserid,
			  a.createdbyusername,
			  a.createdbyuserdisplayname,
			  ici_totalrows as TotalRows
		FROM      {databaseSchema}.{objectQualifier}accessmask a
		 WHERE    a.boardid = i_boardid
		  AND a.accessmaskid = i_accessmaskid
	  LOOP
			 RETURN NEXT _rec;
	  END LOOP;
 END IF;     
END;$BODY$
	LANGUAGE 'plpgsql' STABLE SECURITY DEFINER COST 100 ROWS 1000;   
--GO

DROP FUNCTION IF EXISTS {databaseSchema}.{objectQualifier}accessmask_pforumlist (integer,integer,integer,integer,boolean,boolean);
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}accessmask_pforumlist(i_boardid integer, i_accessmaskid integer, i_excludeflags integer, i_pageuserid integer, i_isusermask boolean, i_iscommonmask boolean)
  RETURNS SETOF {databaseSchema}.{objectQualifier}accessmask_list_return_type AS
$BODY$
DECLARE
	   _rec {databaseSchema}.{objectQualifier}accessmask_list_return_type%ROWTYPE;
BEGIN
 IF i_accessmaskid IS NULL THEN
 -- we select common masks if no personal masks are spoted.
 IF (i_iscommonmask) THEN
FOR _rec IN
		SELECT
			  a.accessmaskid,
			  a.boardid,
			  a.name,
			  a.flags,
			  a.sortorder,
			  a.isusermask,
			  a.isadminmask,
			  a.createdbyuserid,
			  a.createdbyusername,
			  a.createdbyuserdisplayname,
			  0 as TotalRows
		 FROM  {databaseSchema}.{objectQualifier}accessmask a
		 WHERE a.boardid = i_boardid 
			   and (a.flags & i_excludeflags) = 0
			   and (( not i_isusermask or (a.createdbyuserid = i_pageuserid and a.isusermask)) 
			   or (not i_iscommonmask or (a.isadminmask = false and a.isusermask = false)))
			ORDER BY a.isusermask, a.sortorder
	   LOOP
			 RETURN NEXT _rec;
END LOOP;
ELSE
FOR _rec IN
		SELECT
			  a.accessmaskid,
			  a.boardid,
			  a.name,
			  a.flags,
			  a.sortorder,
			  a.isusermask,
			  a.isadminmask,
			  a.createdbyuserid,
			  a.createdbyusername,
			  a.createdbyuserdisplayname,
			  0 as TotalRows
		 FROM  {databaseSchema}.{objectQualifier}accessmask a
		 WHERE a.boardid = i_boardid 
			   and (a.flags & i_excludeflags) = 0
				 and (i_isusermask and (a.createdbyuserid = i_pageuserid and a.isusermask))
			ORDER BY a.isusermask, a.sortorder
	   LOOP
			 RETURN NEXT _rec;
END LOOP;
END IF;
ELSE
	FOR _rec IN
	   SELECT
			  a.accessmaskid,
			  a.boardid,
			  a.name,
			  a.flags,
			  a.sortorder,
			  a.isusermask,
			  a.isadminmask,
			  a.createdbyuserid,
			  a.createdbyusername,
			  a.createdbyuserdisplayname,
			  0 as TotalRows
		FROM      {databaseSchema}.{objectQualifier}accessmask a
		 WHERE    a.boardid = i_boardid
		  AND a.accessmaskid = i_accessmaskid
			LIMIT 1
	  LOOP
			 RETURN NEXT _rec;
END LOOP;
END IF;
END;
$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100
  ROWS 1000; 
--GO

DROP FUNCTION IF EXISTS {databaseSchema}.{objectQualifier}accessmask_aforumlist (integer,integer,integer,integer,boolean,boolean);
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}accessmask_aforumlist
						   (
						   i_boardid integer, 
						   i_accessmaskid integer,
						   i_excludeflags integer,
						   i_pageuserid integer,
						   i_isadminmask boolean,
						   i_iscommonmask boolean
						   )
				  RETURNS SETOF {databaseSchema}.{objectQualifier}accessmask_list_return_type AS
$BODY$
DECLARE
	   _rec {databaseSchema}.{objectQualifier}accessmask_list_return_type%ROWTYPE;
BEGIN
 IF i_accessmaskid IS NULL THEN
 -- we select common masks if no admin masks are spotted.
 IF (not exists (select 1 from {databaseSchema}.{objectQualifier}accessmask where isadminmask limit 1)) THEN
 i_isadminmask := false;
 END IF;
	 FOR _rec IN
		SELECT
			  a.accessmaskid,
			  a.boardid,
			  a.name,
			  a.flags,
			  a.sortorder,
			  a.isusermask,
			  a.isadminmask,
			  a.createdbyuserid,
			  a.createdbyusername,
			  a.createdbyuserdisplayname,
			  0 as TotalRows
		 FROM      {databaseSchema}.{objectQualifier}accessmask a
		  WHERE    a.boardid = i_boardid  and
			(a.flags & i_excludeflags) = 0		
		   and ((i_isadminmask and a.isadminmask)
			 or (i_iscommonMask and (not a.isadminmask and not a.isusermask)))
		   ORDER BY a.sortorder
	   LOOP
			 RETURN NEXT _rec;
	   END LOOP;
 ELSE
	FOR _rec IN
	   SELECT
			  a.accessmaskid,
			  a.boardid,
			  a.name,
			  a.flags,
			  a.sortorder,
			  a.isusermask,
			  a.isadminmask,
			  a.createdbyuserid,
			  a.createdbyusername,
			  a.createdbyuserdisplayname,
			  0 as TotalRows
		FROM      {databaseSchema}.{objectQualifier}accessmask a
		 WHERE    a.boardid = i_boardid
			 and ((i_isadminmask and a.isadminmask)
			 or (i_iscommonMask and (not a.isadminmask and not a.isusermask)))
	  LIMIT 1
	  LOOP
			 RETURN NEXT _rec;
	  END LOOP;
 END IF;     
END;$BODY$
	LANGUAGE 'plpgsql' STABLE SECURITY DEFINER COST 100 ROWS 1000;   
--GO

-- Function: {databaseSchema}.{objectQualifier}accessmask_save(integer, integer, varchar, integer, integer, integer, integer, integer, integer, integer, integer, integer, integer, integer)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}accessmask_save(integer, integer, varchar, integer, integer, integer, integer, integer, integer, integer, integer, integer, integer, integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}accessmask_save
						   (
						   i_accessmaskid integer,
						   i_boardid integer, 
						   i_name varchar, 
						   i_readaccess boolean,
						   i_postaccess boolean, 
						   i_replyaccess boolean, 
						   i_priorityaccess boolean,
						   i_pollaccess boolean, 
						   i_voteaccess boolean, 
						   i_moderatoraccess boolean,
						   i_editaccess boolean,
						   i_deleteaccess boolean,
						   i_uploadaccess boolean, 
						   i_downloadaccess boolean,
						   i_userforumaccess boolean, 
						   i_sortorder integer,
						   i_userid integer,
						   i_isusermask boolean,
						   i_isadminmask boolean,
						   i_utctimestamp timestamp)
				  RETURNS void AS
$BODY$DECLARE
ici_UserName  VARCHAR(255);
ici_userdisplayname  VARCHAR(255);
			 ici_flags integer:=0;
BEGIN
IF i_readaccess IS NOT FALSE THEN 
ici_flags := ici_flags | 1;
END IF;
IF i_postaccess IS NOT FALSE THEN
ici_flags := ici_flags | 2;
END IF;
IF i_replyaccess IS NOT FALSE THEN
ici_flags := ici_flags | 4;
END IF;
IF i_priorityaccess IS NOT FALSE THEN
ici_flags := ici_flags | 8;
END IF;
IF i_pollaccess IS NOT FALSE THEN
ici_flags := ici_flags | 16;
END IF;
IF i_voteaccess IS NOT FALSE THEN
ici_flags := ici_flags | 32;
END IF;
IF i_moderatoraccess IS NOT FALSE THEN
ici_flags := ici_flags | 64;
END IF;
IF i_editaccess IS NOT FALSE THEN
ici_flags := ici_flags | 128;
END IF;
IF i_deleteaccess IS NOT FALSE THEN
ici_flags := ici_flags | 256;
END IF;
IF i_uploadaccess IS NOT FALSE THEN
ici_flags := ici_flags | 512;
END IF;
IF i_downloadaccess IS NOT FALSE THEN
ici_flags := ici_flags | 1024;
END IF;
IF i_userforumaccess IS NOT FALSE THEN
ici_flags := ici_flags | 32768;
END IF;
 IF i_userid IS NOT NULL THEN   
	SELECT Name, DisplayName INTO ici_UserName, ici_userdisplayname FROM {databaseSchema}.{objectQualifier}user where userid = i_userid LIMIT  1;
  end if;
IF i_accessmaskid IS NULL THEN
	   INSERT INTO {databaseSchema}.{objectQualifier}accessmask
		 (name,boardid,flags,sortorder,isusermask, isadminmask,createdbyuserid,createdbyusername,createdbyuserdisplayname, createddate)
	   VALUES(substr(i_name,1,128),i_boardid,ici_flags, i_sortorder,i_isusermask, i_isadminmask, i_userid,ici_UserName,ici_userdisplayname,i_utctimestamp)
	   returning accessmaskid into i_accessmaskid;
ELSE
	   UPDATE {databaseSchema}.{objectQualifier}accessmask
	   SET    name = substr(i_name,1,128), flags = ici_flags, sortorder = i_sortorder, isusermask = i_isusermask , isadminmask = i_isadminmask
	   WHERE  accessmaskid = i_accessmaskid;
END IF;

 IF i_userid IS NOT NULL THEN   
	SELECT Name, DisplayName INTO ici_UserName, ici_userdisplayname FROM {databaseSchema}.{objectQualifier}user where userid = i_userid LIMIT  1;
	   -- guests should not create forums
	ELSE
   
	SELECT boardid INTO i_boardid FROM {databaseSchema}.{objectQualifier}category WHERE CategoryID = i_CategoryID;
	SELECT Name, DisplayName INTO ici_UserName, ici_userdisplayname FROM {databaseSchema}.{objectQualifier}user where BoardID = i_boardid and (Flags & 4) = 4  ORDER BY Joined LIMIT 1;
	END IF;
	if exists (select 1 from {databaseSchema}.{objectQualifier}accessmaskhistory where accessmaskid = i_accessmaskid and changeddate = i_utctimestamp LIMIT 1) THEN
	update {databaseSchema}.{objectQualifier}accessmaskhistory set 
		   changeduserid = i_userid,	
		   changedusername = ici_UserName,
		   changeddisplayname = ici_userdisplayname
	 where accessmaskid = i_accessmaskid and changeddate = i_utctimestamp; 
	else    
	INSERT INTO {databaseSchema}.{objectQualifier}accessmaskhistory(accessmaskid,ChangedUserID,ChangedUserName,changeddisplayname,ChangedDate)
	VALUES (i_accessmaskid,i_UserID,ici_UserName,ici_userdisplayname,i_utctimestamp);
	end IF;

RETURN;
END;$BODY$
	LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER COST 100;
--GO

-- Function: {databaseSchema}.{objectQualifier}active_list(integer, boolean)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}active_list(integer, boolean);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}active_list
						   (
						   i_boardid integer,
						   i_guests boolean,
						   i_showcrawlers boolean,
						   i_activetime integer,
						   i_stylednicks boolean,
						   i_utctimestamp timestamp)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}active_list_return_type AS
$BODY$DECLARE
			 _rec {databaseSchema}.{objectQualifier}active_list_return_type%ROWTYPE;
			 i_interval integer := i_activetime;
BEGIN
  -- Default i_guests  boolean 0

  -- delete non-active
  DELETE FROM {databaseSchema}.{objectQualifier}active
  WHERE    lastactive < i_utctimestamp - (i_interval::varchar(11) || ' minute')::interval;
  -- we don't delete guest access
  DELETE FROM {databaseSchema}.{objectQualifier}activeaccess where  lastactive < i_utctimestamp - (i_interval::varchar(11) || ' minute')::interval AND  IsGuestX is false;
	
		-- SELECT active
		IF i_guests IS TRUE THEN
		  FOR _rec in   
			SELECT a.userid,
			 a.name AS UserName,
			 a.displayname AS UserDisplayName,
			 c.ip,
			 c.sessionid,
			 c.forumid,
			 c.topicid,
			 (SELECT x.name 
				FROM   {databaseSchema}.{objectQualifier}forum x                                 
				  WHERE  x.forumid = c.forumid  LIMIT 1) as ForumName,
			 (SELECT x.topic
				FROM   {databaseSchema}.{objectQualifier}topic x
				  WHERE  x.topicid = c.topicid) as TopicName,
			 COALESCE(SIGN(c.flags & 2)::integer::boolean,FALSE) as IsGuest,
			 COALESCE(SIGN(c.flags & 8)::integer::boolean,FALSE) AS IsCrawler,		
			 a.isactiveexcluded AS IsHidden,
			 CASE(i_stylednicks)
			 WHEN TRUE THEN  a.userstyle  
			 ELSE '' END,
			 1,
			 c.login,
			 c.lastactive,
			 c.location,
			 (extract(minute from (c.lastactive::timestamp - c.login::timestamp))),
			 c.browser,
			 c.platform,
			 c.forumpage,
			 i_interval 
			 FROM     {databaseSchema}.{objectQualifier}user a
			 inner join
			 {databaseSchema}.{objectQualifier}active c ON  c.userid = a.userid
			 WHERE   
			 c.boardid = i_boardid
			 ORDER BY c.lastactive DESC
	   loop
		 return next _rec;
	   end loop;
elseif i_showcrawlers IS TRUE and i_guests IS FALSE THEN
 FOR _rec in   
			SELECT a.userid,
			 a.name AS UserName,
			 a.displayname AS UserDisplayName,
			 c.ip,
			 c.sessionid,
			 c.forumid,
			 c.topicid,
			 (SELECT x.name 
				FROM   {databaseSchema}.{objectQualifier}forum x            
				  WHERE  x.forumid = c.forumid  LIMIT 1) as ForumName,
			 (SELECT x.topic
				FROM   {databaseSchema}.{objectQualifier}topic x
				  WHERE  x.topicid = c.topicid) as TopicName,
			 COALESCE(SIGN(c.flags & 2)::integer::boolean,FALSE) as IsGuest,
			 COALESCE(SIGN(c.flags & 8)::integer::boolean,FALSE) AS IsCrawler,		
			 a.isactiveexcluded AS IsHidden,
			 CASE(i_stylednicks)
			 WHEN TRUE THEN  a.userstyle 
			 ELSE '' END,
			 1,
			 c.login,
			 c.lastactive,
			 c.location,
			 (extract(minute from (c.lastactive::timestamp - c.login::timestamp))),
			 c.browser,
			 c.platform,
			 c.forumpage,
			 i_interval 
			 FROM     {databaseSchema}.{objectQualifier}user a,
			 {databaseSchema}.{objectQualifier}active c
			 WHERE
			 c.userid = a.userid
			 AND c.boardid = i_boardid
			   and ((c.flags & 4) = 4 OR (c.flags & 2) <> 2 OR  (c.flags & 8) = 8)					  
		  ORDER BY c.lastactive DESC
	   loop
		 return next _rec;
	   end loop;

ELSE
		FOR _rec in   
			SELECT a.userid,
			 a.name AS UserName,
			 a.displayname AS UserDisplayName,
			 c.ip,
			 c.sessionid,
			 c.forumid,
			 c.topicid,
			 (SELECT x.name 
				FROM   {databaseSchema}.{objectQualifier}forum x               
				  WHERE  x.forumid = c.forumid  LIMIT 1) as ForumName,
			 (SELECT x.topic
				FROM   {databaseSchema}.{objectQualifier}topic x
				  WHERE  x.topicid = c.topicid) as TopicName,
			 COALESCE(SIGN(c.flags & 2)::integer::boolean,FALSE) as IsGuest,
			 COALESCE(SIGN(c.flags & 8)::integer::boolean,FALSE) AS IsCrawler,		
			 a.isactiveexcluded AS IsHidden,
			 CASE(i_stylednicks)
			 WHEN TRUE THEN  a.userstyle  
			 ELSE '' END,
			 1,
			 c.login,
			 c.lastactive,
			 c.location,
			 (extract(minute from (c.lastactive::timestamp - c.login::timestamp))),
			 c.browser,
			 c.platform,
			 c.forumpage,
			 i_interval 
			 FROM     {databaseSchema}.{objectQualifier}user a,
			 {databaseSchema}.{objectQualifier}active c
			 WHERE
			 c.userid = a.userid
			 AND c.boardid = i_boardid
				 -- no guests
			AND not exists(				
				select  1 
					from {databaseSchema}.{objectQualifier}usergroup x
						inner join {databaseSchema}.{objectQualifier}group y ON y.groupid=x.groupid 
					where x.userid=a.userid and (y.flags & 2)<>0 LIMIT 1
				)
				   ORDER BY c.lastactive DESC
		  loop
		   return next _rec;
		  end loop;
END IF;
END;$BODY$
	LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER COST 100 ROWS 1000;
--GO

-- Function: {databaseSchema}.{objectQualifier}active_list_user(integer, boolean)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}active_list_user(integer, boolean);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}active_list_user
						   (
						   i_boardid integer,
						   i_userid integer,
						   i_guests boolean,
						   i_showcrawlers boolean,
						   i_activetime integer,
						   i_stylednicks boolean,
						   i_utctimestamp timestamp
						   )
				  RETURNS SETOF {databaseSchema}.{objectQualifier}active_list_user_return_type AS
$BODY$DECLARE
			 _rec {databaseSchema}.{objectQualifier}active_list_user_return_type%ROWTYPE;
			 i_interval integer := i_activetime;
BEGIN
-- Default i_guests  boolean 0
-- delete non-active 
DELETE FROM {databaseSchema}.{objectQualifier}active
WHERE   lastactive < i_utctimestamp - (i_interval::varchar(11) || ' minute')::interval;
		-- SELECT active
		IF (i_guests IS TRUE) THEN
		  FOR _rec in   
			SELECT a.userid,
			 a.name AS UserName,
			 a.displayname AS UserDisplayName,
			 c.ip,
			 c.sessionid,
			 c.forumid,                                  
			 (SELECT SIGN("ReadAccess") FROM {databaseSchema}.{objectQualifier}vaccess_combo(i_userid, c.forumid))::integer AS  HasForumAccess,
			 c.topicid,
			 -- ForumName
			 (SELECT x.name 
				FROM   {databaseSchema}.{objectQualifier}forum x                
				  WHERE  x.forumid = c.forumid  LIMIT 1),
				  -- TopicName
			 (select topic from {databaseSchema}.{objectQualifier}topic x 
			 where x.topicid=c.topicid limit 1),
			 COALESCE(((c.flags & 2)=2)::boolean,FALSE) as IsGuest,
			 COALESCE(((c.flags & 8)=8)::boolean,FALSE) AS IsCrawler,		
			  a.isactiveexcluded AS IsHidden,
			 (CASE(i_stylednicks)
			 WHEN TRUE THEN  a.userstyle  
			 ELSE '' END),
			 1,
			 c.login,
			 c.lastactive,
			 c.location,
			 (extract(minute from (c.lastactive::timestamp - c.login::timestamp))),
			 c.browser,
			 c.platform,
			 c.forumpage,
			 i_interval 
			 FROM     {databaseSchema}.{objectQualifier}user a,
			 {databaseSchema}.{objectQualifier}active c
			 WHERE    c.userid = a.userid
			 AND c.boardid = i_boardid
			 ORDER BY c.lastactive DESC
	   loop	   
		 return next _rec;
	   end loop;
ELSEIF (i_guests IS FALSE AND i_showcrawlers IS TRUE) THEN
		  FOR _rec in   
			SELECT a.userid,
			 a.name AS UserName,
			 a.displayname AS UserDisplayName,
			 c.ip,
			 c.sessionid,
			 c.forumid,                                 
			(SELECT SIGN("ReadAccess") FROM {databaseSchema}.{objectQualifier}vaccess_combo(i_userid, c.forumid))::integer AS  HasForumAccess,
			 c.topicid,
			 (SELECT x.name 
				FROM   {databaseSchema}.{objectQualifier}forum x
				  WHERE  x.forumid = c.forumid  LIMIT 1),
			(select topic from {databaseSchema}.{objectQualifier}topic x 
			 where x.topicid=c.topicid limit 1),
			 COALESCE(SIGN(c.flags & 2)::integer::boolean,FALSE) as IsGuest,
			 COALESCE(SIGN(c.flags & 8)::integer::boolean,FALSE) AS IsCrawler,		
			 a.isactiveexcluded AS IsHidden,
			 (CASE(i_stylednicks)
			 WHEN TRUE THEN  a.userstyle  
			 ELSE '' END),
			 1,
			 c.login,
			 c.lastactive,
			 c.location,
			 (extract(minute from (c.lastactive::timestamp - c.login::timestamp))),
			 c.browser,
			 c.platform,
			 c.forumpage,
			 i_interval 
			 FROM     {databaseSchema}.{objectQualifier}user a,
			 {databaseSchema}.{objectQualifier}active c
			 WHERE    c.userid = a.userid
			 AND c.boardid = i_boardid
			 and ((c.flags & 4) = 4 OR  (c.flags & 8) = 8)		
			 ORDER BY c.lastactive DESC
	   loop
		 return next _rec;
	   end loop;


ELSE
	   FOR _rec in 
		   SELECT  DISTINCT
				   a.userid,
				   a.name AS UserName,
				   a.displayname AS UserDisplayName,
				   c.ip,
				   c.sessionid,
				   c.forumid, 
				  (SELECT SIGN("ReadAccess") FROM {databaseSchema}.{objectQualifier}vaccess_combo(i_userid, c.forumid))::integer AS  HasForumAccess,  
				   c.topicid,
				  (SELECT x.name
				  FROM   {databaseSchema}.{objectQualifier}forum x
				  WHERE  x.forumid = c.forumid ),            
				 (select topic from {databaseSchema}.{objectQualifier}topic x 
				  where x.topicid=c.topicid limit 1),
			 COALESCE(SIGN(c.flags & 2)::integer::boolean,FALSE) as IsGuest,
			 COALESCE(SIGN(c.flags & 8)::integer::boolean,FALSE) AS IsCrawler,		
			 COALESCE(SIGN(a.flags & 16)::integer::boolean,FALSE) AS IsHidden,
				  case(i_stylednicks)
				  when TRUE THEN  a.userstyle 
				  else ''	 end,		
				  1 AS UserCount,
				  c.login,
				  c.lastactive,
				  c.location,
				  (extract(minute from (c.lastactive::timestamp - c.login::timestamp))),
				  c.browser,
				  c.platform,
				  c.forumpage,
				  i_interval 
				  FROM     {databaseSchema}.{objectQualifier}user a,
				  {databaseSchema}.{objectQualifier}active c
				  WHERE    c.userid = a.userid
				  AND c.boardid = i_boardid
				  AND 
   -- no guests
			not exists(				
				select  1 
					from {databaseSchema}.{objectQualifier}usergroup x
						inner join {databaseSchema}.{objectQualifier}group y ON y.groupid=x.groupid 
					where x.userid=a.userid and (y.flags & 2)<>0 LIMIT 1
				)
				   ORDER BY c.lastactive DESC              
		  loop
		   return next _rec;
		  end loop; 
END IF;
END;$BODY$
	LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER COST 100 ROWS 1000; 
--GO

-- Function: objectQualifier_active_listforum(integer)

-- DROP FUNCTION objectQualifier_active_listforum(integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}active_listforum
						   (
						   i_forumid integer, 
						   i_stylednicks boolean
						   )
				  RETURNS SETOF {databaseSchema}.{objectQualifier}active_listforum_return_type AS
$BODY$DECLARE
			 _rec {databaseSchema}.{objectQualifier}active_listforum_return_type%ROWTYPE;

BEGIN
	  FOR _rec IN  
		 SELECT
			a.userid AS UserID,
			b.name AS UserName,
			b.displayname AS UserDisplayName,
			COALESCE(((a.flags & 8) = 8)::boolean,FALSE) AS IsCrawler,	
			COALESCE(((b.flags & 16) = 16)::boolean,false) AS IsHidden, 
			(CASE WHEN (i_stylednicks IS TRUE)		
			THEN  COALESCE(( SELECT f.style FROM {databaseSchema}.{objectQualifier}usergroup e 
		join {databaseSchema}.{objectQualifier}group f 
		on f.groupid=e.groupid 
		WHERE e.userid=a.userid AND 
		LENGTH(f.style) > 2 
		ORDER BY f.sortorder LIMIT 1), r.style)  
		else ''	 end),   
		COUNT(a.userid),
		a.browser          
		 FROM     {databaseSchema}.{objectQualifier}active a
		  JOIN {databaseSchema}.{objectQualifier}user b
		  ON b.userid = a.userid
		  JOIN {databaseSchema}.{objectQualifier}rank r
		  ON b.rankid = r.rankid
		  WHERE    a.forumid = i_forumid
	  GROUP BY
		a.userid,
		b.displayname,
		b.name,
		a.flags,
		b.flags,
		a.browser,
		r.style
	  ORDER BY b.name
	LOOP
	 RETURN NEXT _rec;
	END LOOP;
RETURN;
END;$BODY$
	LANGUAGE 'plpgsql' STABLE SECURITY DEFINER COST 100 ROWS 1000;
--GO

-- Function: objectQualifier_active_listtopic(integer)

-- DROP FUNCTION objectQualifier_active_listtopic(integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}active_listtopic
						   (
						   i_topicid integer, 
						   i_stylednicks boolean
						   )
				  RETURNS SETOF {databaseSchema}.{objectQualifier}active_listtopic_return_type AS
$BODY$DECLARE
			 _rec {databaseSchema}.{objectQualifier}active_listtopic_return_type%ROWTYPE;
BEGIN
FOR _rec IN  
		   SELECT
		   a.userid AS UserID,
		   b.name AS UserName,
		   b.displayname AS UserDisplayName,
		   COALESCE(((a.flags & 8) = 8)::boolean,FALSE) AS IsCrawler,	
		   COALESCE(((b.flags & 16) = 16)::boolean,false) AS IsHidden, 
		   CASE WHEN (i_stylednicks IS TRUE)
		   THEN  COALESCE(( SELECT f.style FROM {databaseSchema}.{objectQualifier}usergroup e 
		   join {databaseSchema}.{objectQualifier}group f on f.groupid=e.groupid WHERE e.userid=a.userid AND LENGTH(f.style) > 2 ORDER BY f.sortorder LIMIT 1), r.style)  
		   else ''	 end AS Style,
		   COUNT(a.userid),
		   a.browser
		   FROM    {databaseSchema}.{objectQualifier}active a
		   JOIN {databaseSchema}.{objectQualifier}user b
		   ON b.userid = a.userid
		   JOIN {databaseSchema}.{objectQualifier}rank r 
		   ON b.rankid = r.rankid
		   WHERE    a.topicid = i_topicid
		   GROUP BY
		   a.userid,
		   b.displayname,
		   b.name,
		   a.flags,
		   b.flags,
		   a.browser,
		   r.style
		   ORDER BY b.name 
				LOOP
					RETURN NEXT _rec;
				END LOOP;
END;$BODY$
	LANGUAGE 'plpgsql' STABLE SECURITY DEFINER COST 100 ROWS 1000; 
--GO

-- Function: objectQualifier_active_stats(integer)

-- DROP FUNCTION objectQualifier_active_stats(integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}active_stats
						   (
						   i_boardid integer
						   )
				  RETURNS {databaseSchema}.{objectQualifier}active_stats_return_type  AS
$BODY$DECLARE
			 _rec {databaseSchema}.{objectQualifier}active_stats_return_type;
BEGIN

SELECT COUNT(1) INTO _rec."ActiveUsers"
FROM {databaseSchema}.{objectQualifier}active x
JOIN {databaseSchema}.{objectQualifier}user usr
ON x.userid = usr.userid
WHERE x.boardid = i_boardid
AND usr.isactiveexcluded IS FALSE OR usr.flags IS NULL;

SELECT count(1) INTO _rec."ActiveMembers"
FROM {databaseSchema}.{objectQualifier}active x
JOIN {databaseSchema}.{objectQualifier}user usr
ON x.userid = usr.userid
WHERE x.boardid = i_boardid
AND EXISTS(select 1
from {databaseSchema}.{objectQualifier}usergroup y
inner join {databaseSchema}.{objectQualifier}group z
on y.groupid=z.groupid
where y.userid=x.userid
and (z.flags & 2)=0
AND usr.isactiveexcluded IS FALSE OR usr.flags IS NULL LIMIT 1);

SELECT count(1) INTO  _rec."ActiveGuests"
from {databaseSchema}.{objectQualifier}active x
WHERE x.boardid = i_boardid
AND EXISTS(SELECT 1 from {databaseSchema}.{objectQualifier}usergroup y
inner join {databaseSchema}.{objectQualifier}group z
on y.groupid=z.groupid
WHERE y.userid=x.userid
and (z.flags & 2)<>0 LIMIT 1);

SELECT count(1) INTO _rec."ActiveHidden"
from {databaseSchema}.{objectQualifier}active x
JOIN {databaseSchema}.{objectQualifier}user usr
ON x.userid = usr.userid
WHERE x.boardid = i_boardid
AND EXISTS(SELECT 1 from {databaseSchema}.{objectQualifier}usergroup y
inner join {databaseSchema}.{objectQualifier}group z
on y.groupid=z.groupid
WHERE y.userid=x.userid
and (z.flags & 2)=0  
AND usr.isactiveexcluded IS TRUE LIMIT 1);

-- Only one record
	RETURN  _rec;
END;$BODY$
	LANGUAGE 'plpgsql' STABLE SECURITY DEFINER COST 100; 
--GO

-- Function: {databaseSchema}.{objectQualifier}active_updatemaxstats(integer)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}active_updatemaxstats(integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}active_updatemaxstats
						   (
						   i_boardid integer, 
						   i_utctimestamp timestamp
						   )
				  RETURNS void AS
$BODY$DECLARE
			 ici_count integer;
BEGIN
	
-- Here we're finding current max users value 

SELECT COALESCE((SELECT COUNT (*) FROM (SELECT DISTINCT a.ip
FROM   {databaseSchema}.{objectQualifier}active a
WHERE  a.boardid = i_boardid) as  tmp) ,1)  INTO ici_count;


-- SET TRANSACTION ISOLATION LEVEL READ COMMITTED;
	IF NOT EXISTS ( SELECT 1 FROM {databaseSchema}.{objectQualifier}registry 
	WHERE boardid = i_boardid and name = 'maxusers' LIMIT 1) THEN
	INSERT INTO {databaseSchema}.{objectQualifier}registry
(boardid,
name,
value)
VALUES     (i_boardid,
'maxusers',
COALESCE(ici_count::text,'1'));
INSERT INTO {databaseSchema}.{objectQualifier}registry
(boardid,
name,
value)
VALUES     (i_boardid,
'maxuserswhen',
i_utctimestamp::text);
	
	ELSEIF (ici_count > COALESCE((SELECT value  
FROM   {databaseSchema}.{objectQualifier}registry
WHERE  boardid = i_boardid
AND name = 'maxusers')::integer,0))	THEN
	
		-- In the case we of course simply update 2 registry values
		   UPDATE {databaseSchema}.{objectQualifier}registry
		   SET    value = ici_count::text
		   WHERE  boardid = i_boardid
		   AND name = 'maxusers';

		   UPDATE {databaseSchema}.{objectQualifier}registry
		   SET    value = i_utctimestamp::text
		   WHERE  boardid = i_boardid
		   AND name = 'maxuserswhen';
	END  IF;
	
END;$BODY$
	LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER COST 100;
--GO

-- Function: objectQualifier_attachment_delete(integer)

-- DROP FUNCTION objectQualifier_attachment_delete(integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}attachment_delete
						   (
						   i_attachmentid integer
						   )
				  RETURNS void AS
'DELETE FROM {databaseSchema}.{objectQualifier}attachment
WHERE   attachmentid = $1;'
  LANGUAGE 'sql' VOLATILE SECURITY DEFINER
  COST 100;
COMMENT ON FUNCTION {databaseSchema}.{objectQualifier}attachment_delete(integer) IS 'Delete an attachement.';
  --GO

-- Function: objectQualifier_attachment_download(integer)

-- DROP FUNCTION objectQualifier_attachment_download(integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}attachment_download
						   (
						   i_attachmentid integer
						   )
				  RETURNS void AS
'UPDATE {databaseSchema}.{objectQualifier}attachment
SET    downloads = downloads + 1
WHERE  attachmentid = $1;'
LANGUAGE 'sql' VOLATILE SECURITY DEFINER COST 100;
--GO


-- Function: objectQualifier_attachment_list(integer, integer, integer)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}attachment_list(IN "i_MessageID" integer,IN "i_AttachmentID" integer,IN "i_BoardID " integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}attachment_list
						   (
						   i_messageid integer,  
						   i_attachmentid integer, 
						   i_boardid integer,
						   i_pageindex integer,
						   i_pagesize integer
						   )
				  RETURNS SETOF {databaseSchema}.{objectQualifier}attachment_list_return_type AS
$BODY$DECLARE  
			 ici_totalrows int :=0; 
			 ici_FirstSelectRowID int :=0; 
			 ici_firstselectrownumber int :=0; 
			 _rec {databaseSchema}.{objectQualifier}attachment_list_return_type%ROWTYPE;
BEGIN
   IF i_messageid IS NOT NULL THEN        
   FOR _rec IN

	 SELECT 
		   a.attachmentid,
		   a.messageid,
		   a.filename,
		   a.bytes,
		   a.fileid,
		   a.contenttype,
		   a.downloads,
		   a.filedata,
		   e.boardid,
		   null AS Posted,
		   null AS ForumID,
		   null AS ForumName,
		   null AS TopicID,
		   null AS TopicName,
		   1000  
		from
			{databaseSchema}.{objectQualifier}attachment a
			inner join {databaseSchema}.{objectQualifier}message b on b.messageid = a.messageid
			inner join {databaseSchema}.{objectQualifier}topic c on c.topicid = b.topicid
			inner join {databaseSchema}.{objectQualifier}forum d on d.forumid = c.forumid
			inner join {databaseSchema}.{objectQualifier}category e on e.categoryid = d.categoryid
			inner join {databaseSchema}.{objectQualifier}board brd on brd.boardid = e.boardid
		where
			a.messageid=i_messageid
			 LOOP
	RETURN NEXT _rec;
END LOOP; 
   ELSEIF i_attachmentid IS NOT NULL THEN
   FOR _rec IN
	 SELECT 
		   a.attachmentid,
		   a.messageid,
		   a.filename,
		   a.bytes,
		   a.fileid,
		   a.contenttype,
		   a.downloads,
		   a.filedata,
		   e.boardid,
		   null AS ForumID,
		   null AS ForumName,
		   null AS TopicID,
		   null AS TopicName,
		   1000  
		from
			{databaseSchema}.{objectQualifier}attachment a
			inner join {databaseSchema}.{objectQualifier}message b on b.messageid = a.messageid
			inner join {databaseSchema}.{objectQualifier}topic c on c.topicid = b.topicid
			inner join {databaseSchema}.{objectQualifier}forum d on d.forumid = c.forumid
			inner join {databaseSchema}.{objectQualifier}category e on e.categoryid = d.categoryid
			inner join {databaseSchema}.{objectQualifier}board brd on brd.boardid = e.boardid
		where 
			a.attachmentid=i_attachmentid
			 LOOP
	RETURN NEXT _rec;
END LOOP; 
   ELSE
	 i_pageindex := i_pageindex + 1;
		 
		select count(1) into ici_totalrows 
		FROM  {databaseSchema}.{objectQualifier}attachment a
			inner join {databaseSchema}.{objectQualifier}message b on b.messageid = a.messageid
			inner join {databaseSchema}.{objectQualifier}topic c on c.topicid = b.topicid
			inner join {databaseSchema}.{objectQualifier}forum d on d.forumid = c.forumid
			inner join {databaseSchema}.{objectQualifier}category e on e.categoryid = d.categoryid
		WHERE e.boardid = i_boardid;
			
		ici_firstselectrownumber := (i_pageindex - 1) * i_pagesize;  
   FOR _rec IN 
	SELECT 
		   a.attachmentid,
		   a.messageid,
		   a.filename,
		   a.bytes,
		   a.fileid,
		   a.contenttype,
		   a.downloads,
		   a.filedata,
		   i_BoardID AS BoardID,
		   b.posted AS Posted,
		   d.forumid AS ForumID,
		   d.name AS ForumName,
		   c.topicid AS TopicID,
		   c.topic AS TopicName,
		   ici_totalrows as TotalRows		
		from 
			{databaseSchema}.{objectQualifier}attachment a
			inner join {databaseSchema}.{objectQualifier}message b on b.messageid = a.messageid
			inner join {databaseSchema}.{objectQualifier}topic c on c.topicid = b.topicid
			inner join {databaseSchema}.{objectQualifier}forum d on d.forumid = c.forumid
			inner join {databaseSchema}.{objectQualifier}category e on e.categoryid = d.categoryid
		where
			e.boardid = i_boardid
		order by
			a.attachmentid OFFSET ici_firstselectrownumber LIMIT i_pagesize
			 LOOP
	RETURN NEXT _rec;
END LOOP; 
   END IF; 
END;$BODY$
	LANGUAGE 'plpgsql' STABLE SECURITY DEFINER COST 100;  
--GO

-- Function: objectQualifier_attachment_save(integer, varchar, integer, varchar, bytea)

-- DROP FUNCTION objectQualifier_attachment_save(integer, varchar, integer, varchar, bytea);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}attachment_save
						   (
						   i_messageid integer, 
						   i_filename varchar, 
						   i_bytes integer, 
						   i_contenttype varchar, 
						   i_filedata bytea
						   )
				  RETURNS void AS
'INSERT INTO {databaseSchema}.{objectQualifier}attachment
	(messageid,filename,bytes,contenttype,downloads,filedata)
 VALUES ($1,$2,$3,$4,0,$5);'
LANGUAGE 'sql' VOLATILE SECURITY DEFINER COST 100;
  --GO

-- Function: objectQualifier_bannedip_delete(integer)

-- DROP FUNCTION objectQualifier_bannedip_delete(integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}bannedip_delete
						   (
						   i_id integer
						   )
				  RETURNS void AS
$BODY$
BEGIN
DELETE FROM {databaseSchema}.{objectQualifier}bannedip
WHERE       id = i_id;
RETURN;
END;$BODY$
	LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER COST 100; 
  --GO 

-- Function: objectQualifier_bannedip_list(integer, integer)

-- DROP FUNCTION objectQualifier_bannedip_list(integer, integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}bannedip_list
						   (
						   i_boardid integer, 
						   i_id integer,
						   i_pageindex integer,
						   i_pagesize integer
						   )
				  RETURNS SETOF {databaseSchema}.{objectQualifier}bannedip_list_return_type AS
$BODY$DECLARE
			 ici_totalrows int :=0;            
			 ici_firstselectrownumber int :=0; 
			 _rec {databaseSchema}.{objectQualifier}bannedip_list_return_type%ROWTYPE;
BEGIN
IF i_id IS NULL THEN
 i_pageindex := i_pageindex + 1;
		 
		select count(1) into ici_totalrows 
		FROM  {databaseSchema}.{objectQualifier}bannedip
		WHERE boardid = i_boardid;
			
		ici_firstselectrownumber := (i_pageindex - 1) * i_pagesize;     

  FOR _rec in
	  SELECT
			id,
			boardid,
			mask,
			since,
			reason,
			userid,
			ici_totalrows as TotalRows		
	 FROM  {databaseSchema}.{objectQualifier}bannedip
	 WHERE boardid = i_boardid order by mask  OFFSET ici_firstselectrownumber LIMIT i_pagesize
  LOOP
	  RETURN NEXT _rec;
  END LOOP;
ELSE
FOR _rec in
SELECT
	  id,
	  boardid,
	  mask,
	  since,
	  reason,
	  userid,
	  1::integer  as TotalRows	
FROM   {databaseSchema}.{objectQualifier}bannedip
WHERE  boardid = i_boardid
AND id = i_id 
LOOP
	RETURN NEXT _rec;
	EXIT;
END LOOP;
END IF;
END;$BODY$
	LANGUAGE 'plpgsql' STABLE SECURITY DEFINER COST 100;
  --GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}bannedip_save
						   (
						   i_id integer, 
						   i_boardid integer, 
						   i_mask varchar, 
						   i_reason varchar, 
						   i_userid integer,
						   i_utctimestamp timestamp
						   )
				  RETURNS void AS
$BODY$
BEGIN
IF i_id IS NULL
OR i_id = 0 THEN
BEGIN

INSERT INTO {databaseSchema}.{objectQualifier}bannedip
(boardid,
mask,
since,
reason,
userid)
VALUES     (i_boardid,
i_mask,
i_utctimestamp,
i_reason,
i_userid);
END;
ELSE
BEGIN
UPDATE {databaseSchema}.{objectQualifier}bannedip
SET    mask = i_mask, reason = i_reason, userid = i_userid
WHERE  id = i_id;
END;
END IF;
RETURN;
END;$BODY$
	LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER COST 100; 
--GO

-- Function: objectQualifier_bbcode_delete(integer)

-- DROP FUNCTION objectQualifier_bbcode_delete(integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}bbcode_delete(
						   i_bbcodeid integer)
				  RETURNS void AS
$BODY$
BEGIN
	 IF i_bbcodeid IS NOT NULL THEN
		DELETE FROM {databaseSchema}.{objectQualifier}bbcode WHERE bbcodeid = i_bbcodeid;
	 ELSE
		DELETE FROM {databaseSchema}.{objectQualifier}bbcode;
	 END IF;
   RETURN;
 END;$BODY$
	 LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER COST 100;
--GO
 
 -- Function: {databaseSchema}.{objectQualifier}bbcode_list(integer, integer)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}bbcode_list(integer, integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}bbcode_list
						   (
						   i_boardid integer,
						   i_bbcodeid integer
						   )
				  RETURNS SETOF {databaseSchema}.{objectQualifier}bbcode_list_return_type AS
$BODY$DECLARE
			 _rec {databaseSchema}.{objectQualifier}bbcode_list_return_type%ROWTYPE;
BEGIN
	IF i_bbcodeid IS NULL THEN
	FOR _rec IN 
		SELECT  
			 bbcodeid,
			 boardid,
			 name,
			 description,
			 onclickjs,
			 displayjs,
			 editjs,
			 displaycss,
			 searchregex,
			 replaceregex,
			 variables,
			 usemodule,
			 moduleclass,
			 execorder
		FROM {databaseSchema}.{objectQualifier}bbcode   
		WHERE boardid = i_boardid 
		ORDER BY execorder, name DESC
		LOOP
		RETURN NEXT _rec;
		END LOOP; 
	ELSE
	FOR _rec IN 
		SELECT   
			 bbcodeid,
			 boardid,
			 name,
			 description,
			 onclickjs,
			 displayjs,
			 editjs,
			 displaycss,
			 searchregex,
			 replaceregex,
			 variables,
			 usemodule,
			 moduleclass,
			 execorder
		FROM {databaseSchema}.{objectQualifier}bbcode    
		WHERE bbcodeid = i_bbcodeid  		
		LOOP
		RETURN NEXT _rec;
		EXIT;
		END LOOP; 
   END IF;   
END;$BODY$
	LANGUAGE 'plpgsql' STABLE SECURITY DEFINER COST 100
  ROWS 1000;
--GO

-- Function: objectQualifier_bbcode_save(integer, integer, varchar, varchar, varchar, text, text, text, text, text, varchar, boolean, varchar, integer)

-- DROP FUNCTION objectQualifier_bbcode_save(integer, integer, varchar, varchar, varchar, text, text, text, text, text, varchar, boolean, varchar, integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}bbcode_save
						   (
						   i_bbcodeid integer,
						   i_boardid integer,
						   i_name varchar,
						   i_description varchar,
						   i_onclickjs varchar,
						   i_displayjs text,
						   i_editjs text,
						   i_displaycss text,
						   i_searchregex text,
						   i_replaceregex text,
						   i_variables varchar,
						   i_usemodule boolean,
						   i_moduleclass varchar,
						   i_execorder integer
						   )
				  RETURNS void AS
$BODY$
  BEGIN
	IF i_bbcodeid IS NOT NULL THEN
		
		UPDATE
			{databaseSchema}.{objectQualifier}bbcode
		SET
			name = i_name,
			description = i_description,
			onclickjs = i_onclickjs,
			displayjs = i_displayjs,
			editjs = i_editjs,
			displaycss = i_displaycss,
			searchregex = i_searchregex,
			replaceregex = i_replaceregex,
			variables = i_variables,
			usemodule = i_usemodule,
			moduleclass = i_moduleclass,			
			execorder = i_execorder
		WHERE
			bbcodeid = i_bbcodeid;
	
	ELSE 
		IF NOT EXISTS(SELECT 1 FROM {databaseSchema}.{objectQualifier}bbcode WHERE boardid = i_BoardID AND name = i_Name LIMIT 1) THEN
			INSERT INTO
				{databaseSchema}.{objectQualifier}bbcode
				 (boardid,name,description,onclickjs,displayjs,editjs,displaycss,searchregex,replaceregex,variables,usemodule,moduleclass,execorder)
			VALUES (i_boardid,i_name,i_description,i_onclickjs,i_displayjs,i_editjs,i_displaycss,i_searchregex,i_replaceregex,i_variables,i_usemodule,i_moduleclass,i_execorder);
		END IF; 
	END IF;
	RETURN;
END;$BODY$
	LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER COST 100; 
--GO

-- Function: objectQualifier_board_create(varchar, varchar, varchar, varchar, varchar, boolean)

DROP FUNCTION IF EXISTS  {databaseSchema}.{objectQualifier}board_create(varchar, varchar, varchar, varchar, varchar, uuid, boolean);
--GO
CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}board_create
						   (
						   i_boardname varchar, 
						   i_languagefile varchar, 
						   i_culture varchar(10), 
						   i_membershipappname varchar, 
						   i_rolesappname varchar, 
						   i_username varchar, 
						   i_useremail varchar, 
						   i_userkey varchar, 
						   i_ishostadmin boolean,                           
						   i_roleprefix varchar,
						   i_utctimestamp timestamp
						   )
				  RETURNS integer AS
$BODY$DECLARE
			 ici_boardid                integer;
			 i_timezone                 integer;
			 i_forumemail			    varchar(255);
			 l_GroupIDAdmin			    integer;
			 l_GroupIDGuest			    integer;
			 l_GroupIDMember		    integer;
			 l_AccessMaskIDAdmin		integer;
			 l_AccessMaskIDModerator	integer;
			 l_AccessMaskIDMember		integer;
			 l_AccessMaskIDReadOnly	    integer;
			 l_UserIDAdmin			    integer;
			 l_UserIDGuest			    integer;
			 l_RankIDAdmin			    integer;
			 l_RankIDGuest			    integer;
			 l_RankIDNewbie			    integer;
			 l_RankIDMember			    integer;
			 l_RankIDAdvanced		    integer;
			 l_CategoryID			    integer;
			 l_ForumID			        integer;
			 l_UserFlags			    integer;		
BEGIN
	

   i_timezone := (SELECT CAST(CAST(value AS varchar(10)) AS integer)                                           
						 FROM   {databaseSchema}.{objectQualifier}registry
						 WHERE  Lower(name) = Lower('TimeZone'));
   i_forumemail := (SELECT CAST(value AS varchar(50))                                               			
						   FROM   {databaseSchema}.{objectQualifier}registry
						   WHERE  Lower(name) = Lower('ForumEmail'));                          
						   
	
	
	 -- Board       
		INSERT INTO {databaseSchema}.{objectQualifier}board
				   (name, allowthreaded, membershipappname, rolesappname )
		VALUES     (i_boardname,false, i_membershipappname, i_rolesappname);
	  -- GET DIAGNOSTICS ici_boardid = ROW_COUNT; 
			  
			  
	   SELECT CURRVAL(pg_get_serial_sequence('{databaseSchema}.{objectQualifier}board','boardid')) INTO ici_boardid;
	
	PERFORM {databaseSchema}.{objectQualifier}registry_save('culture',i_culture,ici_boardid);
	PERFORM {databaseSchema}.{objectQualifier}registry_save('language',i_languagefile,ici_boardid);
		
	-- Rank
		INSERT INTO {databaseSchema}.{objectQualifier}rank
				   (boardid,
					name,
					flags,
					minposts,
					pmlimit,
					sortorder)
		VALUES     (ici_boardid,
					'Administration',
					0,
					NULL,
					2147483647,
					0);
	   l_RankIDAdmin := (SELECT CURRVAL(pg_get_serial_sequence('{databaseSchema}.{objectQualifier}rank','rankid')));
	 
		INSERT INTO {databaseSchema}.{objectQualifier}rank
				   (boardid,
					name,
					flags,
					minposts,
					pmlimit,					
					sortorder)
		VALUES     (ici_boardid,
					'Guest',
					4,
					NULL,
					0,
					1);
		
		l_RankIDGuest := (SELECT CURRVAL(pg_get_serial_sequence('{databaseSchema}.{objectQualifier}rank','rankid')));
		INSERT INTO {databaseSchema}.{objectQualifier}rank
				   (boardid,
					name,
					flags,
					minposts,
					pmlimit,
					sortorder)
		VALUES     (ici_boardid,
					'Newbie',
					3,
					0,
					10,
					2);
		
		l_RankIDNewbie := (SELECT CURRVAL(pg_get_serial_sequence('{databaseSchema}.{objectQualifier}rank','rankid')));
		INSERT INTO {databaseSchema}.{objectQualifier}rank
				   (boardid,
					name,
					flags,
					minposts,
					pmlimit,
					sortorder)
		VALUES     (ici_boardid,
					'Member',
					2,
					10,
					30,
					3);
		
		l_RankIDMember := (SELECT CURRVAL(pg_get_serial_sequence('{databaseSchema}.{objectQualifier}rank','rankid')));
		INSERT INTO {databaseSchema}.{objectQualifier}rank
				   (boardid,
					name,
					flags,
					minposts,
					pmlimit,
					sortorder)
		VALUES     (ici_boardid,
					'Advanced Member',
					2,
					30,
					100,
					4);
		
		l_RankIDAdvanced := (SELECT CURRVAL(pg_get_serial_sequence('{databaseSchema}.{objectQualifier}rank','rankid')));
  
	-- AccessMask
	INSERT INTO {databaseSchema}.{objectQualifier}accessmask(boardid,name,flags,sortorder)
	VALUES(ici_boardid,'Admin Access',1023 + 1024,4);
	l_AccessMaskIDAdmin := (SELECT CURRVAL(pg_get_serial_sequence('{databaseSchema}.{objectQualifier}accessmask','accessmaskid')));
 

	INSERT INTO {databaseSchema}.{objectQualifier}accessmask(boardid,name,flags,sortorder)
	VALUES(ici_boardid,'Moderator Access',487 + 1024,3);
	l_AccessMaskIDModerator := (SELECT CURRVAL(pg_get_serial_sequence('{databaseSchema}.{objectQualifier}accessmask','accessmaskid')));

	INSERT INTO {databaseSchema}.{objectQualifier}accessmask(boardid,name,flags,sortorder)
	VALUES(ici_boardid,'Member Access',423 + 1024,2);
	l_AccessMaskIDMember := (SELECT CURRVAL(pg_get_serial_sequence('{databaseSchema}.{objectQualifier}accessmask','accessmaskid')));

	INSERT INTO {databaseSchema}.{objectQualifier}accessmask(boardid,name,flags,sortorder)
	VALUES(ici_boardid,'Read Only Access',1,1);
	l_AccessMaskIDReadOnly := (SELECT CURRVAL(pg_get_serial_sequence('{databaseSchema}.{objectQualifier}accessmask','accessmaskid')));

	INSERT INTO {databaseSchema}.{objectQualifier}accessmask(boardid,name,flags,sortorder)
	VALUES(ici_boardid,'No Access',0,100);   
 
	-- Group
	INSERT INTO {databaseSchema}.{objectQualifier}group(boardid,name,flags,pmlimit,style,sortorder,usrsigchars,usrsigbbcodes,usralbums,usralbumimages) 
	values(ici_boardid, COALESCE(i_roleprefix, '') || 'Administrators',1,2147483647,'default!font-size: 8pt; color: red/flatearth!font-size: 8pt; color:blue',0,256,'URL,IMG,SPOILER,QUOTE',10,120);
	l_GroupIDAdmin := (SELECT CURRVAL(pg_get_serial_sequence('{databaseSchema}.{objectQualifier}group','groupid')));
	INSERT INTO {databaseSchema}.{objectQualifier}group(boardid,name,flags,pmlimit,sortorder,usrsigchars,usrsigbbcodes,usralbums,usralbumimages) values(ici_boardid,'Guests',2,0,1,0,null,0,0);
	l_GroupIDGuest := (SELECT CURRVAL(pg_get_serial_sequence('{databaseSchema}.{objectQualifier}group','groupid')));
	INSERT INTO {databaseSchema}.{objectQualifier}group(boardid,name,flags,pmlimit,sortorder,usrsigchars,usrsigbbcodes,usralbums,usralbumimages) values(ici_boardid, COALESCE(i_roleprefix, '') || 'Registered',4,30,2,128,null,5,30);
	l_GroupIDMember := (SELECT CURRVAL(pg_get_serial_sequence('{databaseSchema}.{objectQualifier}group','groupid')));	
	
	-- User (GUEST)
	INSERT INTO {databaseSchema}.{objectQualifier}user(boardid,rankid,name,displayname,password,email,joined,lastvisit,numposts,timezone,flags)
	VALUES(ici_boardid,l_RankIDGuest,'Guest','Guest','na',i_forumemail,i_utctimestamp,i_utctimestamp,0,i_timezone,6);
	l_UserIDGuest := (SELECT CURRVAL(pg_get_serial_sequence('{databaseSchema}.{objectQualifier}user','userid')));	
	
	l_UserFlags := 2;
	IF i_ishostadmin IS TRUE THEN l_UserFlags := 3; END IF;

  -- User (ADMIN)
  INSERT INTO {databaseSchema}.{objectQualifier}user(boardid,rankid,name,displayname,password,email,provideruserkey, joined,lastvisit,numposts,timezone,flags)
  VALUES(ici_boardid,l_RankIDAdmin,i_username,i_username,'na',i_useremail,i_userkey,i_utctimestamp,i_utctimestamp,0,i_timezone,l_UserFlags);
  l_UserIDAdmin := (SELECT CURRVAL(pg_get_serial_sequence('{databaseSchema}.{objectQualifier}user','userid')));	

 -- UserGroup
  INSERT INTO {databaseSchema}.{objectQualifier}usergroup(userid,groupid)
   VALUES(l_UserIDAdmin,l_GroupIDAdmin);
  INSERT INTO {databaseSchema}.{objectQualifier}usergroup(userid,groupid) 
  VALUES(l_UserIDGuest,l_GroupIDGuest);

 --  Category
  INSERT INTO {databaseSchema}.{objectQualifier}category(boardid,name,sortorder) 
  VALUES(ici_boardid,'Test Category',1);
  l_CategoryID := (SELECT CURRVAL(pg_get_serial_sequence('{databaseSchema}.{objectQualifier}category','categoryid')));	

 -- Forum
  INSERT INTO {databaseSchema}.{objectQualifier}forum(categoryid,name,description,sortorder,numtopics,numposts,flags, left_key, right_key,"level")
  VALUES(l_CategoryID,'Test Forum','A test forum',1,0,0,4,1,2,0) returning forumid into l_ForumID; 

 --  ForumAccess 
  INSERT INTO {databaseSchema}.{objectQualifier}forumaccess(groupid,forumid,accessmaskid) 
  VALUES(l_GroupIDAdmin,l_ForumID,l_AccessMaskIDAdmin);
  INSERT INTO {databaseSchema}.{objectQualifier}forumaccess(groupid,forumid,accessmaskid) 
  VALUES(l_GroupIDGuest,l_ForumID,l_AccessMaskIDReadOnly);
  INSERT INTO {databaseSchema}.{objectQualifier}forumaccess(groupid,forumid,accessmaskid) 
  VALUES(l_GroupIDMember,l_ForumID,l_AccessMaskIDMember); 
 RETURN ici_boardid;
END;$BODY$
	LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER COST 100; 
--GO

-- Function: objectQualifier_board_delete(integer)

-- DROP FUNCTION objectQualifier_board_delete(integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}board_delete(
						   i_boardid integer)
				  RETURNS void AS
$BODY$DECLARE 
			 itmpForumID integer;
			 board_cursor refcursor; 
BEGIN
  
  
  OPEN board_cursor  FOR
  SELECT   forumid
  FROM     {databaseSchema}.{objectQualifier}forum a
  JOIN {databaseSchema}.{objectQualifier}category b
  ON a.categoryid = b.categoryid
  WHERE    b.boardid = i_boardid
  ORDER BY forumid DESC; 

 LOOP
  FETCH board_cursor  INTO itmpForumID ;
  EXIT WHEN NOT FOUND;
  PERFORM {databaseSchema}.{objectQualifier}forum_delete(itmpForumID, false, false);
  EXIT WHEN NOT FOUND;
END LOOP;
  CLOSE board_cursor; 

  DELETE FROM {databaseSchema}.{objectQualifier}forumaccess
  WHERE       EXISTS (SELECT 1
  FROM   {databaseSchema}.{objectQualifier}group x
  WHERE  x.GroupID = {databaseSchema}.{objectQualifier}forumaccess.groupid
  AND x.boardid = i_boardid);
  -- a trigger works here to rebuild tree - double work
  DELETE FROM {databaseSchema}.{objectQualifier}forum
  WHERE       EXISTS (SELECT 1
  FROM   {databaseSchema}.{objectQualifier}category x
  WHERE  x.categoryid = {databaseSchema}.{objectQualifier}forum.categoryid
  AND x.boardid = i_boardid);

  DELETE FROM {databaseSchema}.{objectQualifier}usergroup
  WHERE       EXISTS (SELECT 1
  FROM   {databaseSchema}.{objectQualifier}user x
  WHERE  x.userid = {databaseSchema}.{objectQualifier}usergroup.userid
  AND x.boardid = i_boardid);
  DELETE FROM {databaseSchema}.{objectQualifier}activeaccess
  WHERE       boardid = i_boardid;
  DELETE FROM {databaseSchema}.{objectQualifier}active
  WHERE       boardid = i_boardid;
  DELETE FROM {databaseSchema}.{objectQualifier}category
  WHERE       boardid = i_boardid;
  DELETE FROM {databaseSchema}.{objectQualifier}user
  WHERE       boardid = i_boardid;
  DELETE FROM {databaseSchema}.{objectQualifier}rank
  WHERE       boardid = i_boardid;
  DELETE FROM {databaseSchema}.{objectQualifier}group
  WHERE       boardid = i_boardid;
  DELETE FROM {databaseSchema}.{objectQualifier}accessmask
  WHERE       boardid = i_boardid;
  DELETE FROM {databaseSchema}.{objectQualifier}bbcode
  WHERE       boardid = i_boardid;
  DELETE FROM {databaseSchema}.{objectQualifier}extension
  WHERE       boardid = i_boardid;
  DELETE FROM {databaseSchema}.{objectQualifier}shoutboxmessage
  WHERE       boardid = i_boardid;
  DELETE FROM {databaseSchema}.{objectQualifier}medal
  WHERE       boardid = i_boardid;
  DELETE FROM {databaseSchema}.{objectQualifier}smiley
  WHERE       boardid = i_boardid;
  DELETE FROM {databaseSchema}.{objectQualifier}replace_words
  WHERE       boardid = i_boardid;
  DELETE FROM {databaseSchema}.{objectQualifier}nntpserver
  WHERE       boardid = i_boardid;
  DELETE FROM {databaseSchema}.{objectQualifier}bannedip
  WHERE       boardid = i_boardid;
  DELETE FROM {databaseSchema}.{objectQualifier}registry
  WHERE       boardid = i_boardid;
  DELETE FROM {databaseSchema}.{objectQualifier}board
  WHERE       boardid = i_boardid;
  perform  {databaseSchema}.{objectQualifier}forum_ns_recreate();
  RETURN;
END;$BODY$
	LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER COST 100; 
  --GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}board_list(
						   i_boardid integer)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}board_list_return_type AS
$BODY$DECLARE
			 _rec {databaseSchema}.{objectQualifier}board_list_return_type%ROWTYPE;
BEGIN   
  IF i_boardid IS NULL THEN
  FOR _rec IN 
  SELECT boardid,
		 name,
		 allowthreaded,
		 membershipappname,
		 rolesappname,
  (' ' || version() || '.' || ' Started '|| pg_postmaster_start_time() || '.') AS SQLVersion
  FROM   {databaseSchema}.{objectQualifier}board 
  LOOP
	RETURN NEXT _rec;
  END LOOP;
  ELSE  
  SELECT 
		 boardid,
		 name,
		 allowthreaded,
		 membershipappname,
		 rolesappname,
 (' ' || version()  || '.' || ' Started ' || pg_postmaster_start_time() || '.') AS SQLVersion
  INTO _rec
  FROM   {databaseSchema}.{objectQualifier}board 
  WHERE  boardid = i_boardid;
  RETURN  NEXT _rec;
  END IF;
END;$BODY$
	LANGUAGE 'plpgsql' STABLE SECURITY DEFINER COST 100 ROWS 1000; 
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}board_poststats(
						   i_boardid integer,
						   i_usestylednicks boolean, 
						   i_shownocountposts boolean, 
						   i_getdefaults boolean,
						   i_utctimestamp timestamp)
				  RETURNS {databaseSchema}.{objectQualifier}board_poststats_return_type AS
$BODY$DECLARE
			 _rec {databaseSchema}.{objectQualifier}board_poststats_return_type;
BEGIN
IF i_getdefaults IS NOT TRUE THEN
 SELECT COALESCE((SELECT COUNT(1) 
  FROM   {databaseSchema}.{objectQualifier}message a
  JOIN {databaseSchema}.{objectQualifier}topic b
  ON b.topicid = a.topicid
  JOIN {databaseSchema}.{objectQualifier}forum c
  ON c.forumid = b.forumid
  JOIN {databaseSchema}.{objectQualifier}category d
  ON d.categoryid = c.categoryid
  WHERE  d.boardid = i_boardid  AND (a.flags & 24)=16),0) INTO _rec."Posts";
  
SELECT COUNT(1) INTO _rec."Topics"
FROM   {databaseSchema}.{objectQualifier}topic a
JOIN {databaseSchema}.{objectQualifier}forum b
ON b.forumid = a.forumid
JOIN {databaseSchema}.{objectQualifier}category c
ON c.categoryid = b.categoryid
WHERE  c.boardid = i_boardid AND (a.flags & 8) <> 8;

SELECT COUNT(1)  INTO _rec."Forums"
	FROM   {databaseSchema}.{objectQualifier}forum a
	JOIN {databaseSchema}.{objectQualifier}category b
	ON b.categoryid = a.categoryid
	WHERE  b.boardid = i_boardid; 
	
SELECT  1 AS "LastPostInfoID",
a.posted AS "LastPost",
a.userid AS "LastUserID",
e.name AS "LastUser",
COALESCE(a.userdisplayname, a.username, e.displayname),
(CASE WHEN (i_usestylednicks) THEN COALESCE(e.userstyle,'') ELSE '' END) AS "LastUserStyle"
INTO _rec."LastPostInfoID",
	 _rec."LastPost",
	 _rec."LastUserID",
	 _rec."LastUser",
	 _rec."LastUserDisplayName",
	 _rec."LastUserStyle"
FROM     {databaseSchema}.{objectQualifier}message a
JOIN {databaseSchema}.{objectQualifier}topic b
ON b.topicid = a.topicid
JOIN {databaseSchema}.{objectQualifier}forum c
ON c.forumid = b.forumid
JOIN {databaseSchema}.{objectQualifier}category d
ON d.categoryid = c.categoryid
JOIN {databaseSchema}.{objectQualifier}user e
ON e.userid = a.userid
WHERE   (a.flags & 24) = 16
	AND (b.flags & 8) <> 8 
	AND d.boardid = i_boardid
	AND (b.flags & 8) <> (CASE WHEN i_shownocountposts IS TRUE THEN -1 ELSE 8 END)
ORDER BY  "LastPostInfoID", a.posted DESC LIMIT 1;
else
select 0, 0, 1, 1, null, null, null, null, '' into _rec;
END IF;
 -- first delete tags
 DELETE FROM {databaseSchema}.{objectQualifier}topictags 
		 WHERE topicid IN (SELECT TopicID FROM {databaseSchema}.{objectQualifier}topic WHERE topicmovedid IS NOT NULL AND linkdate IS NOT NULL AND linkdate < i_UTCTIMESTAMP);
 -- then a link
DELETE FROM {databaseSchema}.{objectQualifier}topic where topicmovedid IS NOT NULL AND linkdate IS NOT NULL AND linkdate < i_utctimestamp;
RETURN _rec;
-- this can be in any very rare updatable cached place 
	
END;$BODY$
	LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER COST 100;
--GO

-- Function: {databaseSchema}.{objectQualifier}board_userstats(integer)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}board_userstats(integer, boolean);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}board_userstats(
						   i_boardid integer, i_stylednicks boolean)
				  RETURNS {databaseSchema}.{objectQualifier}board_userstats_return_type AS
$BODY$DECLARE
			 _rec {databaseSchema}.{objectQualifier}board_userstats_return_type;
BEGIN
	
	SELECT COUNT(1) INTO _rec."Members"
	FROM   {databaseSchema}.{objectQualifier}user a
	WHERE  a.boardid = i_boardid 
	AND a.isapproved IS TRUE
	AND a.isguest IS FALSE;

	SELECT  COALESCE((SELECT CAST(value AS integer) 
	FROM {databaseSchema}.{objectQualifier}registry 
	WHERE LOWER(name) = LOWER('maxusers') 
	AND      boardid=i_boardid),0)  INTO _rec."MaxUsers";
	
	SELECT CAST(value AS varchar(255)) INTO _rec."MaxUsersWhen" 
	FROM {databaseSchema}.{objectQualifier}registry 
	WHERE LOWER(name) = LOWER('maxuserswhen') 
	AND     boardid=i_boardid;

SELECT DISTINCT 
				1 AS "LastMemberInfoID",
				userid AS "LastMemberID",
				name AS "LastMember",
				displayname
				INTO 
				_rec."LastMemberInfoID",
				_rec."LastMemberID",
				_rec."LastMember",
				_rec."LastMemberDisplayName"
FROM     {databaseSchema}.{objectQualifier}user
WHERE      isapproved IS TRUE
	 AND isguest IS FALSE
	 AND boardid = i_boardid 
ORDER BY "LastMemberInfoID","LastMemberID" DESC LIMIT 1;

RETURN _rec;
END;$BODY$
	LANGUAGE 'plpgsql' STABLE SECURITY DEFINER COST 100;
--GO

-- Function: objectQualifier_board_resync(integer)

-- DROP FUNCTION objectQualifier_board_resync(integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}board_resync(
						   i_boardid integer)
				  RETURNS void AS
$BODY$DECLARE
			 currBoards refcursor;
			 itmpBoardID integer;
			 nullForum integer;
BEGIN
	IF i_boardid IS NULL THEN 		
		 OPEN currBoards  FOR
			SELECT boardid FROM {databaseSchema}.{objectQualifier}board; 		
		
		 -- cycle through forums
		 LOOP
			   FETCH currBoards INTO itmpBoardID;
			   EXIT WHEN NOT FOUND;
				  -- resync board forums
			   PERFORM {databaseSchema}.{objectQualifier}forum_resync(itmpBoardID,nullForum);
			   EXIT WHEN NOT FOUND;
		 END LOOP;
		 CLOSE currBoards;               
		-- deallocate curBoards	
	ELSE
		-- resync board forums
		PERFORM {databaseSchema}.{objectQualifier}forum_resync(i_boardid,nullForum);
	END IF;
	RETURN;
END;$BODY$
	LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER COST 100;
--GO

-- Function: objectQualifier_board_save(integer, varchar, boolean)
-- drop an old function with differing parameters
DROP FUNCTION IF EXISTS {databaseSchema}.{objectQualifier}board_save(
						integer, 
						varchar, 
						char, 
						varchar,
						boolean);

--GO
DROP FUNCTION IF EXISTS {databaseSchema}.{objectQualifier}board_save(
						integer, 
						varchar, 
						varchar, 
						varchar,
						boolean);
--GO
CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}board_save(
						   i_boardid integer, 
						   i_name varchar, 
						   i_languagefile varchar,
						   i_culture varchar, 
						   i_allowthreaded boolean)
				  RETURNS integer AS
$BODY$
BEGIN
	PERFORM {databaseSchema}.{objectQualifier}registry_save('culture',i_culture,i_boardid);
	PERFORM {databaseSchema}.{objectQualifier}registry_save('language',i_languagefile,i_boardid);
UPDATE {databaseSchema}.{objectQualifier}board
		SET    name = i_name,
			   allowthreaded = i_allowthreaded
		WHERE  boardid = i_boardid;
		RETURN i_boardid;
END;$BODY$
	LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER COST 100; 
--GO

-- Function: objectQualifier_board_stats(integer)

-- DROP FUNCTION objectQualifier_board_stats(integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}board_stats(
						   i_boardid integer)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}board_stats_return_type AS
$BODY$DECLARE
			 _rec {databaseSchema}.{objectQualifier}board_stats_return_type%ROWTYPE;
BEGIN
	IF i_boardid IS NULL THEN
 FOR _rec IN
		SELECT
		-- approved and not deleted
		(SELECT COUNT(1) FROM {databaseSchema}.{objectQualifier}message where (flags & 16) = 16 AND (a.flags & 8) != 8) AS NumPosts,
		-- not deleted 8 flag
		(SELECT COUNT(1) FROM {databaseSchema}.{objectQualifier}topic t where (t.flags & 8) != 8) AS NumTopics,
		-- approved only
		(SELECT COUNT(1) FROM {databaseSchema}.{objectQualifier}user u where u.isapproved IS TRUE) AS NumUsers,
		(SELECT MIN(joined) FROM {databaseSchema}.{objectQualifier}user) AS BoardStart
LOOP
	RETURN NEXT _rec;
END LOOP; 	
	ELSE
		 FOR _rec IN
		SELECT
		(SELECT COUNT(1) FROM  {databaseSchema}.{objectQualifier}message a
		JOIN {databaseSchema}.{objectQualifier}topic b ON a.topicid=b.topicid
		JOIN {databaseSchema}.{objectQualifier}forum c ON b.forumid=c.forumid
		JOIN {databaseSchema}.{objectQualifier}category d ON c.categoryid=d.categoryid
		WHERE (a.flags & 16) = 16 
				  AND (a.flags & 8) != 8 
				  AND ((b.flags & 8) != 8) 
				  AND d.boardid=i_boardid) AS NumPosts,
		(SELECT COUNT(1) FROM {databaseSchema}.{objectQualifier}topic a
		JOIN {databaseSchema}.{objectQualifier}forum b 
		ON a.forumid=b.forumid
		JOIN {databaseSchema}.{objectQualifier}category c 
		ON b.categoryid=c.categoryid
		WHERE c.boardid=i_boardid 
				  AND (a.flags & 8) != 8) AS NumTopics,
		(SELECT COUNT(1) from {databaseSchema}.{objectQualifier}user
		-- is approved
				WHERE isapproved IS TRUE 
				   AND boardid=i_boardid) AS NumUsers,
			(SELECT MIN(joined) FROM {databaseSchema}.{objectQualifier}user
			where boardid=i_boardid) AS BoardStart
LOOP
	RETURN NEXT _rec;
END LOOP;
	END IF;
END;$BODY$
	 LANGUAGE 'plpgsql' STABLE SECURITY DEFINER COST 100;
--GO

-- Function: objectQualifier_category_delete(integer)

-- DROP FUNCTION objectQualifier_category_delete(integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}category_delete(
						   i_categoryid integer, i_newcategoryid integer)
				  RETURNS integer AS
$BODY$DECLARE 
			 i_flag integer;
BEGIN
		if (i_newcategoryid is not null) then		  
			 update {databaseSchema}.{objectQualifier}forum set categoryid = i_newcategoryid where categoryid = i_categoryid;
			 perform  {databaseSchema}.{objectQualifier}forum_ns_recreate();
		  end if;
		IF EXISTS (SELECT 1
				   FROM   {databaseSchema}.{objectQualifier}forum
				   WHERE  categoryid = i_categoryid) THEN       
			i_flag = 0;       
		ELSE		 
			DELETE FROM  {databaseSchema}.{objectQualifier}category
			WHERE       categoryid = i_categoryid;
			i_flag = 1;
		END IF;

		RETURN i_flag;
	END;$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100;
--GO

-- Function: objectQualifier_category_list(integer, integer)

-- DROP FUNCTION objectQualifier_category_list(integer, integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}category_list(
						   i_boardid integer, 
						   i_categoryid integer)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}category_list_return_type AS
$BODY$DECLARE 
			 _rec {databaseSchema}.{objectQualifier}category_list_return_type%ROWTYPE;
BEGIN
		
	IF i_categoryid IS NULL THEN
	FOR _rec IN
		SELECT  categoryid,
			 boardid,
			 name,
			 categoryimage,
			 sortorder,
			 pollgroupid,	
			 canhavepersforums
	   FROM {databaseSchema}.{objectQualifier}category WHERE boardid = i_boardid ORDER BY sortorder
	   LOOP
	RETURN NEXT _rec;
END LOOP; 	
	ELSE
	FOR _rec IN 
		SELECT categoryid,
			 boardid,
			 name,
			 categoryimage,
			 sortorder,
			 pollgroupid,	
			 canhavepersforums
		FROM {databaseSchema}.{objectQualifier}category WHERE boardid = i_boardid AND categoryid = i_categoryid LIMIT 1
		 LOOP
	RETURN NEXT _rec;
END LOOP; 	
		END IF;
END;$BODY$
	 LANGUAGE 'plpgsql' STABLE SECURITY DEFINER COST 100 ROWS 1000; 
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}category_pfaccesslist(
						   i_boardid integer, 
						   i_categoryid integer)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}category_pfaccesslist_return_type AS
$BODY$DECLARE 
			 _rec {databaseSchema}.{objectQualifier}category_pfaccesslist_return_type%ROWTYPE;
BEGIN
		
	IF i_categoryid IS NULL THEN
	FOR _rec IN
		SELECT  c.categoryid,
			 c.boardid,
			 c.name,
			 c.categoryimage,
			 c.sortorder,
			 c.pollgroupid,	
			 c.canhavepersforums,
			 COALESCE((SELECT CAST(SIGN(f.forumid) AS varchar)::boolean FROM {databaseSchema}.{objectQualifier}forum f where f.categoryid = c.categoryid and f.canhavepersforums LIMIT 1),false)  AS HasForumsForPersForums
	   FROM {databaseSchema}.{objectQualifier}category c WHERE boardid = i_boardid ORDER BY c.sortorder
	   LOOP
	RETURN NEXT _rec;
END LOOP; 	
	ELSE
	FOR _rec IN 
		SELECT c.categoryid,
			 c.boardid,
			 c.name,
			 c.categoryimage,
			 c.sortorder,
			 c.pollgroupid,	
			 c.canhavepersforums,
			 COALESCE((SELECT CAST(SIGN(f.forumid) AS varchar)::boolean FROM {databaseSchema}.{objectQualifier}forum f where f.categoryid = c.categoryid and f.canhavepersforums LIMIT 1),false)  AS HasForumsForPersForums
		FROM {databaseSchema}.{objectQualifier}category c WHERE boardid = i_boardid AND c.categoryid = i_categoryid LIMIT 1
		 LOOP
	RETURN NEXT _rec;
END LOOP; 	
		END IF;
END;$BODY$
	 LANGUAGE 'plpgsql' STABLE SECURITY DEFINER COST 100 ROWS 1000; 
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}category_getadjacentforum(i_boardid integer, 
						   i_categoryid integer,i_pageuserid integer, i_isafter boolean) 
				  RETURNS  {databaseSchema}.{objectQualifier}category_getadjacentforum_rt AS
$BODY$DECLARE 
			ici_forumid  integer :=0;
			ici_sortorder integer :=0;
			_rec {databaseSchema}.{objectQualifier}category_getadjacentforum_rt%ROWTYPE;
BEGIN
   -- find first forum in category and its sort order. If no forums, returns 0 for both and is a first forum in a category.
   if i_isafter then 
	select COALESCE(f.forumid,0),COALESCE(f.sortorder,0)
	into  ici_forumid,ici_sortorder
	from {databaseSchema}.{objectQualifier}forum f 
	JOIN {databaseSchema}.{objectQualifier}category c
	ON c.categoryid = f.categoryid
	JOIN {databaseSchema}.{objectQualifier}activeaccess aa
	ON (aa.forumid = f.forumid and aa.userid = i_pageuserid)
	where c.boardid = i_boardid and f.categoryid = i_categoryid order by f.sortorder limit 1;
   end if;

   -- increase order to shift forums order in the category
  if  ici_forumid > 0 and ici_sortorder = 0 then  
  UPDATE {databaseSchema}.{objectQualifier}forum set sortorder = sortorder + 1 where categoryid =  i_categoryid;
  end if;
   SELECT ici_forumid, ici_sortorder into _rec; 
   RETURN _rec;
END;$BODY$
	 LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER COST 100; 
--GO

-- Function: objectQualifier_category_listread(integer, integer, integer)

-- DROP FUNCTION objectQualifier_category_listread(integer, integer, integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}category_listread(
						   i_boardid integer, 
						   i_userid integer, 
						   i_categoryid integer)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}category_listread_return_type AS
$BODY$DECLARE 
			 _rec {databaseSchema}.{objectQualifier}category_listread_return_type%ROWTYPE;
BEGIN
-- we don't need b.forumid,b.flags - they used to check access 
FOR _rec IN
SELECT   a.categoryid,
a.name,
a.categoryimage,
b.forumid,
b.flags
FROM     {databaseSchema}.{objectQualifier}category a
JOIN {databaseSchema}.{objectQualifier}forum b
ON b.categoryid = a.categoryid
/* JOIN {databaseSchema}.{objectQualifier}vaccess v
ON v.forumid = b.forumid */
WHERE    a.boardid = i_boardid
/* AND v.userid = i_userid
AND (v.readaccess <> 0
OR (b.flags & 2) = 0)*/
AND (i_categoryid IS NULL
OR a.categoryid = i_categoryid)
AND b.parentid IS NULL
GROUP BY a.categoryid,a.name,a.sortorder,
a.categoryimage, b.forumid, b.flags
ORDER BY a.sortorder
LOOP
IF (SELECT "ReadAccess" FROM {databaseSchema}.{objectQualifier}vaccess_combo(i_userid, _rec."ForumID") LIMIT 1) > 0 OR (_rec."Flags" & 2) = 0 THEN
RETURN NEXT _rec;
END IF;
END LOOP;	
END;$BODY$
	LANGUAGE 'plpgsql' STABLE SECURITY DEFINER COST 100 ROWS 1000;  
--GO

-- STORED PROCEDURE CREATED BY VZ-TEAM 
-- Function: {databaseSchema}.{objectQualifier}category_simplelist(integer, integer)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}category_simplelist(integer, integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}category_simplelist(
						   i_startid integer,
						   i_limit integer)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}category_simplelist_return_type AS
$BODY$DECLARE
			 cntr integer :=0;
			 _rec {databaseSchema}.{objectQualifier}category_simplelist_return_type%ROWTYPE;
BEGIN
FOR _rec IN
SELECT   c.categoryid,
c.name
FROM     {databaseSchema}.{objectQualifier}category c
WHERE    c.categoryid >= i_startid
		ORDER BY c.categoryid
LOOP
RETURN NEXT _rec;
EXIT WHEN cntr >= i_limit;
cntr:=cntr+1;
END LOOP;

END;$BODY$
	LANGUAGE 'plpgsql' STABLE SECURITY DEFINER COST 100 ROWS 1000;
--GO
  COMMENT ON FUNCTION {databaseSchema}.{objectQualifier}category_simplelist(integer, integer) IS 'i_startid is real id, i_limit is number of record to return';
--GO


-- Function: objectQualifier_category_save(integer, integer, varchar, smallint, varchar)

-- DROP FUNCTION objectQualifier_category_save(integer, integer, varchar, smallint, varchar);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}category_save(
						   i_boardid integer, 
						   i_categoryid integer, 
						   i_name varchar, 
						   i_sortorder integer, 
						   i_categoryimage varchar,
						   i_canhavepersforums boolean,
						   i_adjacentcategoryid integer,
						   i_adjacentcategorymode integer)
				  RETURNS integer AS
$BODY$DECLARE                    
			 _rec record;
			 ici_cntr int :=0;
			 afterset boolean;
BEGIN
	-- set new sort order for categories if adjacent category was supplied 
	-- i_adjacentcategorymode can be over(1) or before(2).
	-- -1 mode means that category sortorder will not be updated.  
	 if (i_adjacentcategoryid is not null) then	 
				  -- over doesn't possible 	
				
	   for _rec in select categoryid from {databaseSchema}.{objectQualifier}category 
		  where boardid = i_boardid  
		  order by sortorder	     
		 LOOP 
			   if (i_adjacentcategoryid = _rec.categoryid) THEN	          	
				-- before
					 if (i_adjacentcategorymode = 1) THEN		               
					   i_sortorder := ici_cntr;
					   ici_cntr := ici_cntr + 1;	               
					   end if;                 
			   -- after
				   if (i_adjacentcategorymode = 2) THEN		               
					   i_sortorder := ici_cntr + 1;
					   afterset := true;	
					   end if;              
			   end if;
			if (i_sortorder = ici_cntr and afterset) THEN 
			 ici_cntr := ici_cntr + 1;
			end if;
			update	{databaseSchema}.{objectQualifier}category
			set sortorder = ici_cntr where categoryid = _rec.categoryid;
			
			ici_cntr := ici_cntr + 1;	
		
	END LOOP;
	end if;	

   IF i_categoryid > 0 THEN
			 UPDATE {databaseSchema}.{objectQualifier}category
			 SET     name = i_name ,
					 categoryimage = i_categoryimage,
					 sortorder = (case when i_adjacentcategorymode != -1 THEN i_sortorder else sortorder  end), 
					 canhavepersforums = i_canhavepersforums
			 WHERE  categoryid = i_categoryid;
   ELSE
			INSERT INTO {databaseSchema}.{objectQualifier}category
						(boardid,name,categoryimage,sortorder,canhavepersforums)
			VALUES     (i_boardid,substr(i_name, 1, 255),i_categoryimage,i_sortorder,i_canhavepersforums)
			returning categoryid INTO i_categoryid; 
   END IF;
  
   RETURN  i_categoryid;
END;$BODY$
	LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER COST 100;
--GO

-- Function: objectQualifier_checkemail_list(varchar)

-- DROP FUNCTION objectQualifier_checkemail_list(varchar);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}checkemail_list(
						   i_email varchar)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}checkemail_list_return_type AS
$BODY$DECLARE
			 _rec {databaseSchema}.{objectQualifier}checkemail_list_return_type%ROWTYPE;
BEGIN
IF i_email IS NULL THEN
FOR _rec IN
SELECT       checkemailid,
			 userid,
			 email,
			 created,
			 hash
FROM {databaseSchema}.{objectQualifier}checkemail
 LOOP
	RETURN NEXT _rec;
END LOOP; 	
ELSE
FOR _rec IN
SELECT  checkemailid,
			 userid,
			 email,
			 created,
			 hash FROM {databaseSchema}.{objectQualifier}checkemail WHERE email = LOWER(i_email)
 LOOP
	RETURN NEXT _rec;
END LOOP; 	
END IF;

END;$BODY$
	LANGUAGE 'plpgsql' STABLE SECURITY DEFINER COST 100 ROWS 1000;
--GO

-- Function: objectQualifier_checkemail_save(integer, varchar, varchar)

-- DROP FUNCTION objectQualifier_checkemail_save(integer, varchar, varchar);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}checkemail_save(
						   i_iserid integer, 
						   i_hash varchar, 
						   i_email varchar,
						   i_utctimestamp timestamp)
				  RETURNS void AS
'INSERT INTO {databaseSchema}.{objectQualifier}checkemail
(userid,email,created,hash)
VALUES
($1,LOWER($3),$4,$2);'
LANGUAGE 'sql' VOLATILE SECURITY DEFINER COST 100;
--GO
 COMMENT ON FUNCTION {databaseSchema}.{objectQualifier}checkemail_save(integer, varchar, varchar,timestamp ) IS 'Saves email message data for delivering in CheckEmail table.';
--GO

-- Function: objectQualifier_checkemail_update(varchar)

-- DROP FUNCTION databaseSchema."objectQualifier_checkemail_update"(varchar);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}checkemail_update(
						   i_hash varchar)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}checkemail_update_return_type AS
$BODY$DECLARE
			 l_UserID integer;
			 l_CheckEmailID integer;
			 l_Email varchar(50);
			 _rec {databaseSchema}.{objectQualifier}checkemail_update_return_type%ROWTYPE;
BEGIN 
SELECT
checkemailid,
userid,
email
INTO l_CheckEmailID,l_UserID,l_Email
FROM
{databaseSchema}.{objectQualifier}checkemail
WHERE
hash = i_hash;

IF l_UserID IS NULL THEN
FOR _rec IN
SELECT CAST(NULL AS character(64)) AS ProviderUserKey, 
CAST(NULL AS character(255)) AS Email
LOOP
	RETURN NEXT _rec;
END LOOP;
ELSE
-- Update new user email
UPDATE {databaseSchema}.{objectQualifier}user SET email = LOWER(l_Email), flags = Flags | 2 WHERE userid = l_UserID;
DELETE FROM {databaseSchema}.{objectQualifier}checkemail WHERE checkemailid = l_CheckEmailID;

-- return the UserProviderKey
FOR _rec IN
SELECT provideruserkey, email FROM {databaseSchema}.{objectQualifier}user WHERE userid = l_UserID
LOOP
	RETURN NEXT _rec;
END LOOP;
END IF;
END;$BODY$
	LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER COST 100 ROWS 1000;  
--GO

-- Function: objectQualifier_choice_add(integer, varchar)

-- DROP FUNCTION objectQualifier_choice_add(integer, varchar);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}choice_add(
						   i_pollid integer,
						   i_choice varchar,
						   i_objectpath varchar,
						   i_mimetype varchar)
				  RETURNS void AS
'INSERT INTO {databaseSchema}.{objectQualifier}choice
(pollid, choice, votes, objectpath, mimetype)
VALUES
($1, $2, 0, $3, $4);'
LANGUAGE 'sql' VOLATILE SECURITY DEFINER COST 100; 
--GO
  COMMENT ON FUNCTION {databaseSchema}.{objectQualifier}choice_add(integer, varchar, varchar, varchar) IS 'Inserts a single Poll option into Choice table';
--GO

-- Function: objectQualifier_choice_delete(integer)

-- DROP FUNCTION objectQualifier_choice_delete(integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}choice_delete(
						   i_choiceid integer)
				  RETURNS void AS
'DELETE FROM {databaseSchema}.{objectQualifier}choice
WHERE choiceid = $1;'
LANGUAGE 'sql' VOLATILE SECURITY DEFINER COST 100;  
--GO

-- Function: objectQualifier_choice_update(integer, varchar)

-- DROP FUNCTION objectQualifier_choice_update(integer, varchar);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}choice_update(
						   i_choiceid integer,
						   i_choice varchar,
						   i_objectpath varchar,
						   i_mimetype varchar)
				  RETURNS void AS
'UPDATE {databaseSchema}.{objectQualifier}choice
SET choice = $2, objectpath =$3, mimetype = $4
WHERE choiceid = $1;'
LANGUAGE 'sql' VOLATILE SECURITY DEFINER COST 100;  
--GO

-- Function: objectQualifier_choice_vote(integer, integer, varchar)

-- DROP FUNCTION objectQualifier_choice_vote(integer, integer, varchar);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}choice_vote(
						   i_choiceid integer, 
						   i_userid integer, 
						   i_remoteip varchar)
				  RETURNS void AS
$BODY$DECLARE
			 l_PollID integer;
BEGIN
SELECT pollid INTO l_PollID 
FROM {databaseSchema}.{objectQualifier}choice 
WHERE choiceid = i_choiceid;

IF i_userid IS NULL THEN
IF i_remoteip IS NOT NULL THEN
INSERT INTO 
{databaseSchema}.{objectQualifier}pollvote (pollid, userid, remoteip, choiceid) 
VALUES  (l_PollID,NULL,i_remoteip, choiceid);  
 END IF;

ELSE
INSERT INTO {databaseSchema}.{objectQualifier}pollvote (pollid, userid, remoteip, choiceid) VALUES (l_PollID,i_userid,i_remoteip, i_choiceid);
END IF;

UPDATE {databaseSchema}.{objectQualifier}choice SET votes = votes + 1 WHERE choiceid = i_choiceid;
END;$BODY$
	LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER COST 100; 
--GO

-- Function: objectQualifier_eventlog_create(integer, varchar, text, integer)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}eventlog_create(integer, varchar, text, integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}eventlog_create(
						   i_userid integer, 
						   i_source varchar, 
						   i_description text, 
						   i_type integer,
						   i_utctimestamp timestamp)
				  RETURNS void AS
$BODY$DECLARE
			 topLogID integer;
BEGIN

INSERT INTO {databaseSchema}.{objectQualifier}eventlog
(userid,
source,
description,
type,
eventtime)
VALUES     (i_userid,
i_source,
i_description,
i_type,
i_utctimestamp);

 RETURN; 
END;$BODY$
	LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER COST 100; 
--GO

-- Function: objectQualifier_eventlog_delete(integer, integer)

-- DROP FUNCTION objectQualifier_eventlog_delete(integer, integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}eventlog_delete(
						   i_eventlogid integer, 
						   i_boardid integer,
						   i_pageuserid integer)
				  RETURNS void AS
$BODY$
BEGIN
	-- either EventLogID or BoardID must be null, not both at the same time
	IF i_eventlogid IS NULL THEN 
		-- delete all events of this board
		DELETE FROM {databaseSchema}.{objectQualifier}eventlog
		WHERE
			(userid IS NULL or
			userid IN (SELECT userid FROM {databaseSchema}.{objectQualifier}user WHERE boardid=i_boardid));
	
	ELSE 
		 -- delete just one event
		DELETE FROM  {databaseSchema}.{objectQualifier}eventlog WHERE eventlogid=i_eventlogid;
	END IF;
END;$BODY$
	LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER COST 100;
--GO

-- Function: objectQualifier_eventlog_list(integer)

-- DROP FUNCTION objectQualifier_eventlog_list(integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}eventlog_list
						   (
						   i_boardid integer, 
						   i_pageuserid integer, 
						   i_maxrows integer, 
						   i_maxdays integer,  
						   i_pageindex int,
						   i_pagesize int, 
						   i_sincedate timestamp, 
						   i_todate timestamp, 
						   i_eventids  varchar(8000), 
						   i_utctimestamp timestamp
						   )
				  RETURNS SETOF {databaseSchema}.{objectQualifier}eventlog_list_return_type AS
$BODY$DECLARE
			 _rec {databaseSchema}.{objectQualifier}eventlog_list_return_type%ROWTYPE;
			 ici_eventid varchar(11);
			 i_eventids text := TRIM(BOTH FROM i_eventids) || ',';
			 ici_pos integer := POSITION(',' IN i_eventids);
			 ici_messagearray int array;
			 ici_msgcntr int := 0;
			 ici_firstselectrownumber int := 0;
			 ici_totalrows integer := 0;
BEGIN 
-- i_eventids := TRIM(BOTH FROM i_messageids) || ',';
-- ici_pos := POSITION(',' IN i_eventids);
IF CHAR_LENGTH(i_eventids) = 0 then i_eventids = null; end if;
IF REPLACE(i_eventids, ',', '') <> '' THEN

 WHILE ici_pos > 0 
	   LOOP		 
			-- left function replaced by substring 
			ici_eventid := TRIM(BOTH FROM (SUBSTRING(i_eventids FOR ici_pos - 1)));
			IF ici_eventid <> '' THEN	
				ici_messagearray[ici_msgcntr] := (CAST(ici_eventid AS integer));				
				ici_msgcntr := ici_msgcntr + 1;
				--Use Appropriate conversion
			END IF;
			-- right implementation here we remove first id from string
			i_eventids := SUBSTRING(i_eventids from ici_pos+1 for (char_length(i_eventids) - ici_pos));
			ici_pos := POSITION(',' IN i_eventids);			
	   END LOOP;
							-- to be sure that last value is inserted
IF (LENGTH(i_eventids)>0) THEN
ici_msgcntr := ici_msgcntr + 1;
ici_messagearray[ici_msgcntr] := (CAST(i_eventids AS integer));                          
						   END IF;          

END	IF;	 

-- delete entries older than 10 days
DELETE FROM {databaseSchema}.{objectQualifier}eventlog
WHERE   (eventtime +  (i_maxdays::varchar(11) || ' day')::interval) < i_utctimestamp;
	   -- or IF there are more then i_maxrows
		IF ((SELECT COUNT(1)
			 FROM   {databaseSchema}.{objectQualifier}eventlog) >= i_maxrows) THEN
DELETE FROM {databaseSchema}.{objectQualifier}eventlog WHERE eventlogid 
IN (SELECT eventlogid FROM {databaseSchema}.{objectQualifier}eventlog 
ORDER BY eventtime LIMIT 100) ; 
		END IF;

-- IN  (SELECT * FROM unnest(ici_messagearray))
-- hostadmin
 i_pageindex := i_pageindex + 1;
if (exists (select 1 from {databaseSchema}.{objectQualifier}user where ((flags & 1) = 1 and userid = i_pageuserid) limit 1)) then
 
		 
		select count(1) into ici_totalrows from
		{databaseSchema}.{objectQualifier}eventlog el		
		left join {databaseSchema}.{objectQualifier}user b 
		on b.UserID=el.UserID
		where	   
		 (b.userid IS NULL or b.boardid = i_boardid)	
		 and ((i_eventids IS NULL )  OR  
		 el.type IN (select * from unnest(ici_messagearray)))  
		 and el.EventTime between i_sincedate and i_todate;
			
		ici_firstselectrownumber := (i_pageindex - 1) * i_pagesize ;
	 
FOR _rec IN
		SELECT
		el.eventlogid,
		el.eventtime,
		el.userid,
		el.source,
		el.description,
		el.type,    
		COALESCE(u.name,'System')  AS "Name",
		ici_totalrows as TotalRows
		FROM     {databaseSchema}.{objectQualifier}eventlog el
				 LEFT JOIN {databaseSchema}.{objectQualifier}user u
				   ON el.userid = u.userid
		WHERE    (u.UserID IS NULL or u.BoardID = i_boardid) and ((i_eventids IS NULL )  OR  el.type IN (select * from unnest(ici_messagearray)))  and el.EventTime between i_sincedate and i_todate
	   ORDER BY el.eventlogid DESC OFFSET ici_firstselectrownumber LIMIT i_pagesize
		LOOP		
	RETURN NEXT _rec;	
END LOOP; 
else

		select count(1) into ici_totalrows from
		{databaseSchema}.{objectQualifier}eventlog el
				 left join {databaseSchema}.{objectQualifier}eventloggroupaccess e on e.eventtypeid = el.type
				 join {databaseSchema}.{objectQualifier}usergroup ug on (ug.userid =  i_pageuserid and ug.groupid = e.groupid)
				 LEFT JOIN {databaseSchema}.{objectQualifier}user u
				 ON u.userid = el.userid
		where	   
		(u.UserID IS NULL or u.boardid = i_boardid)	and ((i_eventids IS NULL )  OR  el.type IN (select * from unnest(ici_messagearray)))  and EventTime between i_sincedate and i_todate;
			
		ici_firstselectrownumber := (i_pageindex - 1) * i_pagesize ;
	 
FOR _rec IN
		SELECT
		el.eventlogid,
		el.eventtime,
		el.userid,
		el.source,
		el.description,
		el.type,    
		COALESCE(u.name,'System')  AS "Name",
		ici_totalrows as TotalRows
		FROM    {databaseSchema}.{objectQualifier}eventlog el
				 left join {databaseSchema}.{objectQualifier}eventloggroupaccess e on e.eventtypeid = el.type
				 join {databaseSchema}.{objectQualifier}usergroup ug on (ug.userid =  i_pageuserid and ug.groupid = e.groupid)
				 LEFT JOIN {databaseSchema}.{objectQualifier}user u
				 ON u.userid = el.userid
		WHERE  (u.UserID IS NULL or u.boardid = i_boardid)	and ((i_eventids IS NULL )  OR  el.type IN (select * from unnest(ici_messagearray)))  and el.EventTime between i_sincedate and i_todate
	   ORDER BY el.eventlogid DESC OFFSET ici_firstselectrownumber LIMIT i_pagesize
		LOOP		
	RETURN NEXT _rec;	
END LOOP; 

end if;
END;$BODY$
	LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER COST 100 ROWS 1000;   
--GO

-- Function: objectQualifier_extension_delete(integer)

-- DROP FUNCTION databaseSchema."objectQualifier_extension_delete"(integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}extension_delete(
						   i_extensionid integer)
				  RETURNS void AS
'DELETE FROM {databaseSchema}.{objectQualifier}extension 
	WHERE extensionid = $1;'
LANGUAGE 'sql' VOLATILE SECURITY DEFINER COST 100; 
--GO

-- Function: objectQualifier_extension_edit(integer)

-- DROP FUNCTION objectQualifier_extension_edit(integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}extension_edit(
						   i_extensionid integer)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}extension_return_type AS
$BODY$DECLARE
			 _rec {databaseSchema}.{objectQualifier}extension_return_type%ROWTYPE;
BEGIN
FOR _rec IN
	SELECT extensionid,
			 boardid,
			 extension 
	FROM {databaseSchema}.{objectQualifier}extension 
	WHERE extensionid = i_extensionid 
	ORDER BY extension
			LOOP
	RETURN NEXT _rec;
END LOOP;

END;$BODY$
	LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER COST 100 ROWS 1000; 
--GO

-- Function: objectQualifier_extension_list(integer, varchar)

-- DROP FUNCTION objectQualifier_extension_list(integer, varchar);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}extension_list(
						   i_boardid integer, 
						   i_extension varchar)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}extension_return_type AS
$BODY$DECLARE 
			 _rec {databaseSchema}.{objectQualifier}extension_return_type%ROWTYPE; 
BEGIN 
	-- If an extension is passed, THEN we want to check for THAT extension-
	IF i_extension IS NOT NULL THEN
		FOR _rec IN
			SELECT
				extensionid,
			 boardid,
			 extension 
			FROM
				{databaseSchema}.{objectQualifier}extension 
			WHERE
				boardid = i_boardid AND extension=i_extension
			ORDER BY
				extension
 LOOP
	RETURN NEXT _rec;
END LOOP;
	ELSE
		-- Otherwise, just get a list for the given i_BoardId
		FOR _rec IN
			SELECT
				extensionid,
			 boardid,
			 extension 
			FROM
				{databaseSchema}.{objectQualifier}extension 
			WHERE
				boardid = i_boardid	
			ORDER BY
				extension
				
		LOOP
		  RETURN NEXT _rec;
		END LOOP;
	END IF;

END;$BODY$
	LANGUAGE 'plpgsql' STABLE SECURITY DEFINER COST 100 ROWS 1000;  
--GO

-- Function: {databaseSchema}.{objectQualifier}extension_save(integer, integer, varchar)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}extension_save(integer, integer, varchar);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}extension_save(
						   i_extensionid integer, 
						   i_boardid integer, 
						   i_extension varchar)
				  RETURNS void AS
$BODY$
BEGIN
	IF i_extensionid IS NULL OR i_extensionid = 0 OR (not exists (select 1 from {databaseSchema}.{objectQualifier}extension where boardid = i_boardid and extension like i_extension)) THEN		         
		INSERT INTO {databaseSchema}.{objectQualifier}extension (boardid,extension) 
		VALUES(i_boardid,i_extension);
	ELSE
		UPDATE {databaseSchema}.{objectQualifier}extension
		SET extension = i_extension 
		WHERE extensionid = i_extensionid;
	END IF;
END;$BODY$
	LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER COST 100; 
--GO

-- Function: {databaseSchema}.{objectQualifier}forum_delete(integer)

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}forum_delete(
						   i_forumid integer, i_movechildren boolean, i_rebuildtree boolean)
				  RETURNS void AS
$BODY$DECLARE
			 itmpTopicID integer;
			 topic_cursor refcursor;
BEGIN      

		-- Maybe an idea to use cascading foreign keys instead Too bad they don't work on MS SQL 7.0...

		UPDATE {databaseSchema}.{objectQualifier}forum
		SET    lastmessageid = NULL,
				lasttopicid = NULL
		WHERE  forumid = i_forumid;

		UPDATE {databaseSchema}.{objectQualifier}topic
		SET    lastmessageid = NULL
		WHERE  forumid = i_forumid;

		UPDATE {databaseSchema}.{objectQualifier}active 
		SET forumid=NULL 
		WHERE forumid=i_ForumID;


	 
	   DELETE  FROM {databaseSchema}.{objectQualifier}watchtopic
		USING  {databaseSchema}.{objectQualifier}topic        
		WHERE forumid = i_forumid
		AND {databaseSchema}.{objectQualifier}watchtopic.topicid = {databaseSchema}.{objectQualifier}topic.topicid;
	   
	
	   DELETE  FROM {databaseSchema}.{objectQualifier}active USING {databaseSchema}.{objectQualifier}topic       
		WHERE {databaseSchema}.{objectQualifier}topic.forumid = i_forumid
		AND   {databaseSchema}.{objectQualifier}active.topicid = {databaseSchema}.{objectQualifier}topic.topicid;

		DELETE FROM {databaseSchema}.{objectQualifier}nntptopic USING {databaseSchema}.{objectQualifier}nntpforum         
		 WHERE        {databaseSchema}.{objectQualifier}nntpforum.forumid = i_forumid
		 AND {databaseSchema}.{objectQualifier}nntptopic.nntpforumid = {databaseSchema}.{objectQualifier}nntpforum.nntpforumid;

		DELETE FROM {databaseSchema}.{objectQualifier}nntpforum
		WHERE       forumid = i_forumid;

		DELETE FROM {databaseSchema}.{objectQualifier}watchforum
		WHERE       forumid = i_forumid;
		
	   --Delete topics, messages and attachments  
		OPEN topic_cursor FOR  SELECT   topicid
		FROM     {databaseSchema}.{objectQualifier}topic
		WHERE    forumid = i_forumid
		ORDER BY topicid DESC;
LOOP
  FETCH topic_cursor INTO itmpTopicID;
  EXIT WHEN NOT FOUND;
  PERFORM {databaseSchema}.{objectQualifier}topic_delete(itmpTopicID, null, true , true);
  EXIT WHEN NOT FOUND;
END LOOP;        
		   CLOSE topic_cursor;
	  
		-- TopicDelete finished 
	  
		DELETE FROM {databaseSchema}.{objectQualifier}forumaccess
		WHERE       forumid = i_forumid;
	  
		-- Delete UserForums
		-- a trigger works to rebuild tree
		DELETE FROM {databaseSchema}.{objectQualifier}userforum
		WHERE       forumid = i_forumid;

	 --And after this we can delete Forum itself-
	
		DELETE FROM {databaseSchema}.{objectQualifier}forum
		WHERE       forumid = i_forumid;

		if (i_rebuildtree) then
		i_forumid := i_forumid;
		-- perform {databaseSchema}.{objectQualifier}forum_ns_recreate();
		end if;
END;$BODY$
	LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER COST 100; 
--GO

-- Function: {databaseSchema}.{objectQualifier}forum_delete(integer)

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}forum_move(
						   i_forumoldid integer, i_forumnewid integer)
				  RETURNS void AS
$BODY$DECLARE
			 itmpTopicID integer;
			 topic_cursor refcursor;
BEGIN 
		UPDATE {databaseSchema}.{objectQualifier}forum
		SET    lastmessageid = NULL,
				lasttopicid = NULL
		WHERE  forumid = i_forumoldid;     

		UPDATE {databaseSchema}.{objectQualifier}active 
		SET forumid=NULL 
		WHERE forumid=i_forumoldid;

		update {databaseSchema}.{objectQualifier}NntpForum set forumid=i_forumnewid where forumid=i_forumoldid;
		update {databaseSchema}.{objectQualifier}WatchForum set forumid=i_forumnewid where forumid=i_forumoldid;
		delete from {databaseSchema}.{objectQualifier}ForumReadTracking where forumid = i_forumoldid;
	 
	  
	   --Move topics, messages and attachments        
	  

		OPEN topic_cursor FOR  SELECT   topicid
		FROM     {databaseSchema}.{objectQualifier}topic
		WHERE    forumid = i_forumoldid
		ORDER BY topicid DESC;
LOOP
  FETCH topic_cursor INTO itmpTopicID;
  EXIT WHEN NOT FOUND;
  PERFORM {databaseSchema}.{objectQualifier}topic_move(itmpTopicID, i_forumnewid, false, -1, current_timestamp at time zone 'utc');
  EXIT WHEN NOT FOUND;
END LOOP;        
		   CLOSE topic_cursor;      
		-- TopicDelete finished       
		DELETE FROM {databaseSchema}.{objectQualifier}forumaccess
		WHERE       forumid = i_forumoldid;

		  --Update UserForums Too 
	  -- update {databaseSchema}.{objectQualifier}userforum set forumid = i_forumnewid where forumid = i_forumoldid
	  
		-- Delete UserForums

	   DELETE FROM {databaseSchema}.{objectQualifier}userforum
	   WHERE       forumid = i_forumoldid;

	 -- And after this we can delete Forum itself-
	 -- a trigger works here to rebuild tree
		DELETE FROM {databaseSchema}.{objectQualifier}forum
		WHERE       forumid = i_forumoldid;
END;$BODY$
	LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER COST 100; 
--GO

-- Function: {databaseSchema}.{objectQualifier}forum_list(integer, integer)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}forum_list(integer, integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}forum_list
						   (
						   i_boardid integer, 
						   i_forumid integer,
						   i_userid integer,
						   i_isuserforum boolean
						   )
				  RETURNS SETOF {databaseSchema}.{objectQualifier}forum_list_return_type  AS
$BODY$DECLARE
			 _rec {databaseSchema}.{objectQualifier}forum_list_return_type%ROWTYPE;
BEGIN
	   IF i_forumid IS NULL THEN
	   FOR _rec IN 
					  SELECT a.forumid,
							 a.categoryid,
							 a.parentid,
							 a.name,
							 a.description,
							 a.sortorder,
							 a.lastposted,
							 a.lasttopicid,
							 a.lastmessageid,
							 a.lastuserid,
							 a.lastusername,
							 a.numtopics,
							 a.numposts,
							 a.remoteurl,
							 a.flags,
							 a.themeurl,
							 a.imageurl,
							 a.styles,
							 a.pollgroupid,
							 a.isuserforum,
							 a.createdbyuserid,
							 a.canhavepersforums 
					FROM {databaseSchema}.{objectQualifier}forum a 
								  JOIN {databaseSchema}.{objectQualifier}category b 
									 ON b.categoryid=a.categoryid 
										WHERE b.boardid=i_boardid 									
										  ORDER BY a.sortorder
LOOP
RETURN NEXT _rec;
END LOOP;
	ELSE
	FOR _rec IN 
		SELECT a.forumid,
							 a.categoryid,
							 a.parentid,
							 a.name,
							 a.description,
							 a.sortorder,
							 a.lastposted,
							 a.lasttopicid,
							 a.lastmessageid,
							 a.lastuserid,
							 a.lastusername,
							 a.numtopics,
							 a.numposts,
							 a.remoteurl,
							 a.flags,
							 a.themeurl,
							 a.imageurl,
							 a.styles,
							 a.pollgroupid,
							 a.isuserforum,
							 a.createdbyuserid,
							 a.canhavepersforums
		FROM {databaseSchema}.{objectQualifier}forum a 
				   JOIN {databaseSchema}.{objectQualifier}category b 
					ON b.categoryid=a.categoryid 
					 WHERE b.boardid=i_boardid 
					  AND a.forumid = i_forumid					
					 LOOP
RETURN NEXT _rec;
END LOOP;
		END IF;

END;$BODY$
	LANGUAGE 'plpgsql' STABLE SECURITY DEFINER COST 100 ROWS 1000;
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}forum_byuserlist
						   (
						   i_boardid integer, 
						   i_forumid integer,
						   i_userid integer,
						   i_isuserforum boolean
						   )
				  RETURNS SETOF {databaseSchema}.{objectQualifier}forum_list_return_type  AS
$BODY$DECLARE
			 _rec {databaseSchema}.{objectQualifier}forum_list_return_type%ROWTYPE;
BEGIN
	   IF i_forumid IS NULL THEN
	   FOR _rec IN 
					  SELECT a.forumid,
							 a.categoryid,
							 a.parentid,
							 a.name,
							 a.description,
							 a.sortorder,
							 a.lastposted,
							 a.lasttopicid,
							 a.lastmessageid,
							 a.lastuserid,
							 a.lastusername,
							 a.numtopics,
							 a.numposts,
							 a.remoteurl,
							 a.flags,
							 a.themeurl,
							 a.imageurl,
							 a.styles,
							 a.pollgroupid,
							 a.isuserforum,
							 a.createdbyuserid,
							 a.canhavepersforums  
					FROM {databaseSchema}.{objectQualifier}forum a 
								  JOIN {databaseSchema}.{objectQualifier}category b 
									 ON b.categoryid=a.categoryid 
										WHERE b.boardid=i_boardid 
										and  a.isuserforum = i_isuserforum
										and (i_userid is null or a.createdbyuserid = i_userid)
										  ORDER BY a.sortorder
LOOP
RETURN NEXT _rec;
END LOOP;
	ELSE
	FOR _rec IN 
		SELECT a.forumid,
							 a.categoryid,
							 a.parentid,
							 a.name,
							 a.description,
							 a.sortorder,
							 a.lastposted,
							 a.lasttopicid,
							 a.lastmessageid,
							 a.lastuserid,
							 a.lastusername,
							 a.numtopics,
							 a.numposts,
							 a.remoteurl,
							 a.flags,
							 a.themeurl,
							 a.imageurl,
							 a.styles,
							 a.pollgroupid,
							 a.isuserforum,
							 a.createdbyuserid,
							 a.canhavepersforums  
		FROM {databaseSchema}.{objectQualifier}forum a 
				   JOIN {databaseSchema}.{objectQualifier}category b 
					ON b.categoryid=a.categoryid 
					 WHERE b.boardid=i_boardid 
					  AND a.forumid = i_forumid
					  and  a.isuserforum = i_isuserforum
					  and (i_userid is null or a.createdbyuserid = i_userid)
					  LOOP
RETURN NEXT _rec;
END LOOP;
		END IF;

END;$BODY$
	LANGUAGE 'plpgsql' STABLE SECURITY DEFINER COST 100;
--GO

-- Function: {databaseSchema}.{objectQualifier}forum_listall(integer, integer, integer)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}forum_listall(integer, integer, integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}forum_listall(
						   i_boardid integer, 
						   i_userid integer, 
						   i_root integer,
						   i_returnall boolean)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}forum_listall_return_type AS
$BODY$DECLARE
_rec {databaseSchema}.{objectQualifier}forum_listall_return_type%ROWTYPE;
 BEGIN          
			IF i_root = 0 THEN
			FOR _rec IN
				 SELECT
					b.categoryid,
					b.name AS Category,
					a.forumid,					
					a.name AS Forum,
					0 AS Indent,
					a.parentid,
					a.flags,
					a.pollgroupid,
					(a.flags & 2 = 2),
					a.canhavepersforums,
					false as ReadAccess
			  FROM
					{databaseSchema}.{objectQualifier}forum a
					JOIN {databaseSchema}.{objectQualifier}category b ON b.categoryid=a.categoryid
				   
			  WHERE
				   /* c.userid=i_userid AND */
					b.boardid=i_boardid /* AND
					c.readaccess >0 */
			  ORDER BY
					b.sortorder,
					a.sortorder,
					b.categoryid,
					a.forumid
LOOP
SELECT "ReadAccess" FROM {databaseSchema}.{objectQualifier}vaccess_combo(i_userid, _rec."ForumID") LIMIT 1 INTO _rec."ReadAccess";
IF (i_returnall OR _rec."ReadAccess") THEN
RETURN NEXT _rec;
END IF;
END LOOP;
			  ELSEIF  i_root > 0  THEN
FOR _rec IN
	SELECT
		b.categoryid,
		b.name AS Category,
		a.forumid,					
		a.name AS Forum,
		0 AS Indent,
		a.parentid,
		a.flags,
		a.pollgroupid,
		(a.flags & 2 = 2),
		a.canhavepersforums,
		false as ReadAccess
	FROM
		{databaseSchema}.{objectQualifier}forum a
		JOIN {databaseSchema}.{objectQualifier}category b ON b.categoryid=a.categoryid
	   /* JOIN {databaseSchema}.{objectQualifier}vaccess c ON c.forumid=a.forumid */
	WHERE
	   /* c.userid=i_userid AND */
		b.boardid=i_boardid AND
	   /* c.readaccess>0 AND */
		a.forumid = i_root
	ORDER BY
		b.sortorder,
		a.sortorder,
		b.categoryid,
		a.forumid
		LOOP
SELECT "ReadAccess" FROM {databaseSchema}.{objectQualifier}vaccess_combo(i_userid, _rec."ForumID") LIMIT 1 INTO _rec."ReadAccess";
IF (i_returnall OR _rec."ReadAccess") THEN
RETURN NEXT _rec;
END IF;
END LOOP;
ELSE
   FOR _rec IN 
   SELECT
		b.categoryid,
		b.name AS Category,
		a.forumid,
		a.name AS Forum,
		0 AS Indent,
		a.parentid,
		(a.flags & 2 = 2),
		a.canhavepersforums,
		false as ReadAccess
	FROM
		{databaseSchema}.{objectQualifier}forum a
		JOIN {databaseSchema}.{objectQualifier}category b ON b.categoryid=a.categoryid
	   /* JOIN {databaseSchema}.{objectQualifier}vaccess c ON c.forumid=a.forumid */
	WHERE
	   /* c.userid=i_userid AND */
		b.boardid=i_boardid AND
	  /*  c.readaccess>0 AND */
		b.categoryid = -i_root
	ORDER BY
		b.sortorder,
		a.sortorder,
		b.categoryid,
		a.forumid
LOOP
SELECT "ReadAccess" FROM {databaseSchema}.{objectQualifier}vaccess_combo(i_userid, _rec."ForumID") LIMIT 1 INTO _rec."ReadAccess";
IF (i_returnall OR _rec."ReadAccess") THEN
RETURN NEXT _rec;
END IF;
END LOOP;
END IF;

END;$BODY$
	LANGUAGE 'plpgsql' STABLE SECURITY DEFINER COST 100 ROWS 1000;  
  --GO

-- Function: {databaseSchema}.{objectQualifier}forum_listall_fromcat(integer, integer)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}forum_listall_fromcat(integer, integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}forum_listall_fromcat(
						   i_boardid integer, 
						   i_categoryid integer,
						   i_allowuserforumsonly boolean)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}forum_listall_fromcat_return_type AS
$BODY$DECLARE
			 _rec {databaseSchema}.{objectQualifier}forum_listall_fromcat_return_type%ROWTYPE;
BEGIN
FOR _rec IN
	SELECT     b.categoryid, 
			   b.name AS Category, 
			   a.forumid, 
			   a.name AS Forum, 
			   a.parentid,
			   a.pollgroupid,
			   a.canhavepersforums   
	FROM       {databaseSchema}.{objectQualifier}forum a 
		INNER JOIN
			 {databaseSchema}.{objectQualifier}category b ON b.categoryid = a.categoryid
		WHERE
			b.categoryid=i_categoryid AND
			b.boardid=i_boardid
			AND (not i_allowuserforumsonly or a.isuserforum) 
		ORDER BY
			b.sortorder,
			a.sortorder
LOOP
RETURN NEXT _rec;
END LOOP;
			
END;$BODY$
	LANGUAGE 'plpgsql' STABLE SECURITY DEFINER COST 100 ROWS 1000;   
--GO

-- Function: {databaseSchema}.{objectQualifier}forum_listallmymoderated(integer, integer)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}forum_listallmymoderated(integer, integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}forum_listallmymoderated(
						   i_boardid integer, 
						   i_userid integer)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}forum_listallmymoderated_return_type AS
$BODY$DECLARE
_rec {databaseSchema}.{objectQualifier}forum_listallmymoderated_return_type%ROWTYPE;
BEGIN
		FOR _rec IN
	SELECT
		b.categoryid,
		b.Name AS Category,
		a.ForumID,
		a.Name AS Forum ,
		x.Indent
	FROM
		((SELECT
			b.forumid,
			0 AS Indent
		FROM
			{databaseSchema}.{objectQualifier}category a
			JOIN {databaseSchema}.{objectQualifier}forum b ON b.categoryid=a.categoryid
		WHERE
			a.boardid=i_boardid AND
			b.parentid IS NULL)
	
		UNION
	
		(SELECT
			c.forumid,
			1 AS Indent
		FROM
			{databaseSchema}.{objectQualifier}category a
			JOIN {databaseSchema}.{objectQualifier}forum b on b.categoryid=a.categoryid
			JOIN {databaseSchema}.{objectQualifier}forum c on c.parentid=b.forumid
		WHERE
			a.boardid=i_boardid and
			b.parentid IS NULL)
	
		UNION
	
		(SELECT
			d.forumid,
			2 AS Indent 
		FROM
			{databaseSchema}.{objectQualifier}category a
			JOIN {databaseSchema}.{objectQualifier}forum b ON b.categoryid=a.categoryid
			JOIN {databaseSchema}.{objectQualifier}forum c ON c.parentid=b.forumid
			JOIN {databaseSchema}.{objectQualifier}forum d ON d.parentid=c.forumid
		WHERE
			a.boardid=i_boardid AND
			b.parentid IS NULL
		)) AS x
		JOIN {databaseSchema}.{objectQualifier}forum a ON a.forumid=x.forumid
		JOIN {databaseSchema}.{objectQualifier}category b ON b.categoryid=a.categoryid
		JOIN {databaseSchema}.{objectQualifier}vaccess c ON c.forumid=a.forumid
	WHERE
		c.userid=i_userid AND
		b.boardid=i_boardid AND
		c.moderatoraccess >0
	ORDER BY
		b.sortorder,
		a.sortorder
LOOP
RETURN NEXT _rec;
END LOOP;
		
END;

$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100 ROWS 1000;  
  --GO 

-- Function: {databaseSchema}.{objectQualifier}forum_listpath(integer)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}forum_listpath(integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}forum_listpath(
						   i_forumid integer)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}forum_listpath_return_type AS
$BODY$DECLARE
_rec {databaseSchema}.{objectQualifier}forum_listpath_return_type%ROWTYPE;
BEGIN
-- supports up to 4 levels of nested forums
FOR _rec IN
							SELECT a.forumid,
							a.name
							FROM     ((SELECT a.forumid,
							0 AS Indent
							FROM   {databaseSchema}.{objectQualifier}forum a
							WHERE  a.forumid = i_forumid)
							UNION
							(SELECT b.forumid,
							1 AS Indent
							FROM   {databaseSchema}.{objectQualifier}forum a
							JOIN {databaseSchema}.{objectQualifier}forum b
							ON b.forumid = a.parentid
							WHERE  a.forumid = i_forumid)
							UNION
							(SELECT c.forumid,
							2 AS Indent
							FROM   {databaseSchema}.{objectQualifier}forum a
							JOIN {databaseSchema}.{objectQualifier}forum b
							ON b.forumid = a.parentid
							JOIN {databaseSchema}.{objectQualifier}forum c
							ON c.forumid = b.parentid
							WHERE  a.forumid = i_forumid)
							UNION
							(SELECT d.forumid,
							3 AS Indent
							FROM   {databaseSchema}.{objectQualifier}forum a
							JOIN {databaseSchema}.{objectQualifier}forum b
							ON b.forumid = a.parentid
							JOIN {databaseSchema}.{objectQualifier}forum c
							ON c.forumid = b.parentid
							JOIN {databaseSchema}.{objectQualifier}forum d
							ON d.forumid = c.parentid
							WHERE  a.forumid = i_forumid)
							UNION
							(SELECT d.forumid,
							4 AS Indent
							FROM   {databaseSchema}.{objectQualifier}forum a
							JOIN {databaseSchema}.{objectQualifier}forum b
							ON b.forumid = a.parentid
							JOIN {databaseSchema}.{objectQualifier}forum c
							ON c.forumid = b.parentid
							JOIN {databaseSchema}.{objectQualifier}forum d
							ON d.forumid = c.parentid
							JOIN {databaseSchema}.{objectQualifier}forum e
							ON e.forumid = d.parentid
							WHERE  a.forumid = i_forumid
							)) AS x
							JOIN {databaseSchema}.{objectQualifier}forum a
							ON a.forumid = x.forumid
							ORDER BY x.Indent DESC
LOOP
RETURN NEXT _rec;
END LOOP;
						 
END; $BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100 ROWS 1000;
  --GO
   
 CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}forum_listread_helper(
							i_forumid integer)
				   RETURNS SETOF {databaseSchema}.{objectQualifier}forum_listread_helper_return_type AS
 $BODY$DECLARE
_rec {databaseSchema}.{objectQualifier}forum_listread_helper_return_type%ROWTYPE; 
 ici_lastposted timestamp ;
 ici_LastMessageID integer;
 ici_lastmessageflags integer;
 ici_LastUserID integer;
 ici_LastTopicID integer;
 ici_TopicMovedID integer; 
BEGIN
 
 SELECT			
		b.lastposted,
		b.lastmessageid,
		b.lastmessageflags,
		b.lastuserid AS LastUserID, 		                
		b.lasttopicid AS LastTopicID,
		b.TopicMovedID 
 INTO 	ici_lastposted,ici_LastMessageID,ici_LastUserID,ici_LastTopicID, ici_topicmovedid	
 FROM 
		{databaseSchema}.{objectQualifier}forum b
		WHERE
		 b.forumid=i_ForumID OR b.parentid=i_ForumID AND b.lastposted IS NOT NULL		
	ORDER BY 		
		b.lastposted DESC LIMIT 1;
		
 --We make it in loop to not return empty row		
 FOR _rec IN SELECT  
		ici_lastposted  LastPosted,
		ici_LastMessageID LastMessageID,
		ici_lastmessageflags LastMessageFlags,
		ici_LastUserID LastUserID, 	
		ici_LastTopicID LastTopicID,
		ici_topicmovedid TopicMovedID ,
		t.topic LastTopicName 		
	FROM 
		{databaseSchema}.{objectQualifier}topic t WHERE
		 t.topicID =ici_LastTopicID
		 LOOP
		 RETURN NEXT _rec;
		 END LOOP;
 
END;
$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100
  ROWS 1; 
--GO 

-- Function: {databaseSchema}.{objectQualifier}forum_listread(integer, integer, integer, integer)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}forum_listread(integer, integer, integer, integer,boolean);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}forum_listread(
						   i_boardid integer,
						   i_userid integer,
						   i_categoryid integer,
						   i_parentid integer,
						   i_stylednicks boolean,
						   i_findlastunread boolean,
						   i_showcommonforums boolean,
						   i_showpersonalforums boolean,
						   i_forumcreatedbyuserid integer,
						   i_UTCTIMESTAMP timestamp)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}forum_listread_return_type AS
$BODY$DECLARE
-- ici_lasttopicid integer;
ici_topicmovedid integer;
ici_lastposted timestamp ;
ici_lastmessageid integer;
ici_lastmessageflags integer;
ici_lastuserid integer;
ici_lasttopicname varchar(255);
ici_lastuserdisplayname varchar(255);
ici_lasttopicstyles varchar(255);
ici_lastuser varchar(255);
ici_pollgroupid integer; 
ici_style varchar(255):='';
ici_lasttopicstatus  varchar(255):='';
ici_lasttopicaccess  timestamp ;
ici_forumids int array; 
ici_forumids1 int array; 
intcnt integer:=0; 
_rectemp {databaseSchema}.{objectQualifier}forum_listread_tmp%ROWTYPE;
_rec {databaseSchema}.{objectQualifier}forum_listread_return_type%ROWTYPE; 
BEGIN

FOR _rectemp IN SELECT 
		i_userid, 		
		b.forumid AS ForumID,	
		b.parentid,
		0 as readaccess
	FROM 
		{databaseSchema}.{objectQualifier}category a
		JOIN {databaseSchema}.{objectQualifier}forum b on b.categoryid=a.categoryid
		JOIN {databaseSchema}.{objectQualifier}activeaccess x on x.forumid=b.forumid 		
	WHERE 
		a.boardid = i_boardid AND
		((b.flags & 2)=0 OR x.readaccess  IS NOT FALSE  ) AND
		(i_CategoryID IS NULL OR a.categoryid=i_categoryid) AND
		((i_ParentID IS NULL AND b.parentid IS NULL) 
				OR b.parentid=i_parentid)
				AND
		x.userid = i_userid
	ORDER BY 
		a.sortorder,
		b.sortorder
		LOOP
		ici_forumids[intcnt] = _rectemp."ForumID";
		ici_forumids1[intcnt] = _rectemp."ForumID";
		intcnt := intcnt + 1;
		END LOOP;
		
FOR _rectemp IN SELECT	
		i_userid, 		
		b.forumid AS ForumID,	
		b.parentid,
		0 as readaccess
	FROM 
		{databaseSchema}.{objectQualifier}category a
		JOIN {databaseSchema}.{objectQualifier}forum b on b.categoryid=a.categoryid
		JOIN {databaseSchema}.{objectQualifier}activeaccess x on x.forumid=b.forumid 		
	WHERE 
		a.boardid = i_boardid AND
		((b.flags & 2)=0 OR x.readaccess IS NOT FALSE  ) AND
		(i_CategoryID IS NULL OR a.categoryid=i_categoryid) AND
		(b.parentid IN (SELECT * FROM unnest(ici_forumids1))) 
				AND
		x.userid = i_userid
	ORDER BY 
		a.sortorder,
		b.sortorder
		LOOP
		intcnt := intcnt + 1;
		ici_forumids[intcnt] = _rectemp."ForumID";		
		END LOOP;
 -- more childrens can be added to display as a tree
		


FOR _rec IN
	SELECT 
		a.categoryid, 
		a.name AS Category, 
		b.forumid AS ForumID,
		b.parentid,
		b.name AS Forum, 
		b.description, 		
		b.imageurl,		
		b.pollgroupid, 
		b.isuserforum,		
		{databaseSchema}.{objectQualifier}forum_topics(b.forumid) AS Topics,
		{databaseSchema}.{objectQualifier}forum_posts(b.forumid) AS Posts, 	
		b.lasttopicid,
		ici_lasttopicstatus,
		ici_lasttopicstyles,
		ici_topicmovedid,
		b.lastposted,
		ici_lastmessageid,
		ici_lastmessageflags,
		ici_lastuserid,
		ici_lasttopicname,
		ici_lastuser,
		ici_lastuserdisplayname,
		b.flags,
		ici_style AS "Style",
	(SELECT COUNT(1) FROM {databaseSchema}.{objectQualifier}active xz 
	JOIN {databaseSchema}.{objectQualifier}user usr 
	ON xz.userid = usr.userid 
	WHERE xz.forumid=b.forumid 
	AND usr.isactiveexcluded IS FALSE) AS Viewing, 
		b.remoteurl,
		x.readaccess::integer,
		(case(i_findlastunread)
			 when true THEN
			   (SELECT LastAccessDate FROM {databaseSchema}.{objectQualifier}forumreadtracking x WHERE x.forumid=b.forumid AND x.userid = i_userid LIMIT 1)
			 else null	 end) AS LastForumAccess,
			 ici_lasttopicaccess  AS LastTopicAccess		
	FROM 
		{databaseSchema}.{objectQualifier}category a
		JOIN {databaseSchema}.{objectQualifier}forum b on b.categoryid=a.categoryid
		JOIN {databaseSchema}.{objectQualifier}activeaccess x on x.forumid=b.forumid
	--	JOIN {databaseSchema}.{objectQualifier}forum_ns_getsubtree(i_parentid, true) q ON q."ForumID" = b.forumid
	WHERE
		(i_categoryid IS NULL OR a.categoryid=i_categoryid) AND
		 x.userid = i_userid  and		
		 (b.forumid IN (SELECT * FROM unnest(ici_forumids)) ) 	
		 and ((b.flags & 2)=0 OR x.readaccess IS NOT FALSE ) 	
	ORDER BY 
		a.sortorder,
		b.sortorder 		
	LOOP 	 
					IF  (_rec."LastTopicID" IS NULL OR _rec."LastPosted"	IS NULL) THEN	 
					 _rec."LastTopicID" := {databaseSchema}.{objectQualifier}forum_lasttopic(_rec."ForumID",i_userid,_rec."LastTopicID",_rec."LastPosted");
	END IF;
	IF  (_rec."LastTopicID" IS NULL OR _rec."LastPosted"	IS NULL AND _rec."TopicMovedID" IS NOT NULL) THEN	 
	 _rec."LastTopicID":={databaseSchema}.{objectQualifier}forum_lasttopic(_rec."ForumID",i_userid,_rec."TopicMovedID",_rec."LastPosted");
	END IF;	  	
	 SELECT    t.lastposted , 
			   t.lastmessageid, 
			   t.lastmessageflags, 
			   t.lastuserid,
			   t.topicmovedid,
			   t.topic,
			   t.status,
			   t.styles,
			   (case when (i_findlastunread IS TRUE)
			   THEN
			   (SELECT lastaccessdate FROM {databaseSchema}.{objectQualifier}topicreadtracking y WHERE y.topicid=t.topicid AND y.userid = i_userid)
			   else null	 end)
	INTO
	_rec."LastPosted" , 
	_rec."LastMessageID" , 
	_rec."LastMessageFlags",
	_rec."LastUserID",
	_rec."TopicMovedID",
	_rec."LastTopicName", 
	_rec."LastTopicStatus",
	_rec."LastTopicStyles",
	_rec."LastTopicAccess"
	 FROM 
		{databaseSchema}.{objectQualifier}topic t
		WHERE   t.topicid=_rec."LastTopicID" LIMIT 1; 		
	  
	   _rec."LastUser" = COALESCE((SELECT t.lastusername FROM 
		{databaseSchema}.{objectQualifier}topic t
		WHERE  t.topicid=_rec."LastTopicID" LIMIT 1),(SELECT u2.name
			 FROM   {databaseSchema}.{objectQualifier}user u2
			 WHERE  u2.userid = _rec."LastUserID" LIMIT 1));
	   _rec."LastUserDisplayName" = COALESCE((SELECT t.lastuserdisplayname FROM 
		{databaseSchema}.{objectQualifier}topic t
		WHERE  t.topicid=_rec."LastTopicID"  LIMIT 1),(SELECT u2.displayname
			 FROM   {databaseSchema}.{objectQualifier}user u2
			 WHERE  u2.userid = _rec."LastUserID" LIMIT 1));
	_rec."Style" := (case(i_stylednicks)
			when true THEN  (SELECT us.userstyle FROM {databaseSchema}.{objectQualifier}user us where us.userid = _rec."LastUserID")   
			else ''	 end); 
	RETURN NEXT _rec;	
END LOOP;

END;

$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100
  ROWS 1000; 
  --GO
  
   CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}forum_listreadpersonal(
						   i_boardid integer,
						   i_userid integer,
						   i_categoryid integer,
						   i_parentid integer,
						   i_stylednicks boolean,
						   i_findlastunread boolean,
						   i_showcommonforums boolean,
						   i_showpersonalforums boolean,
						   i_forumcreatedbyuserid integer,
						   i_UTCTIMESTAMP timestamp)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}forum_listread_return_type AS
$BODY$DECLARE
-- ici_lasttopicid integer;
ici_topicmovedid integer;
ici_lastposted timestamp ;
ici_lastmessageid integer;
ici_lastmessageflags integer;
ici_lastuserid integer;
ici_lasttopicname varchar(255);
ici_lastuserdisplayname varchar(255);
ici_lasttopicstyles varchar(255);
ici_lastuser varchar(255);
ici_pollgroupid integer; 
ici_style varchar(255):='';
ici_lasttopicstatus  varchar(255):='';
ici_lasttopicaccess  timestamp ;
ici_forumids int array; 
ici_forumids1 int array; 
intcnt integer:=0; 
_rectemp {databaseSchema}.{objectQualifier}forum_listread_tmp%ROWTYPE;
_rec {databaseSchema}.{objectQualifier}forum_listread_return_type%ROWTYPE; 
BEGIN   


FOR _rec IN
	SELECT 
		a.categoryid, 
		a.name AS Category, 
		b.forumid AS ForumID,
		b.parentid,
		b.name AS Forum, 
		b.description, 		
		b.imageurl,		
		b.pollgroupid, 
		b.isuserforum,		
		{databaseSchema}.{objectQualifier}forum_topics(b.forumid) AS Topics,
		{databaseSchema}.{objectQualifier}forum_posts(b.forumid) AS Posts, 	
		b.lasttopicid,
		ici_lasttopicstatus,
		ici_lasttopicstyles,
		ici_topicmovedid,
		b.lastposted,
		ici_lastmessageid,
		ici_lastmessageflags,
		ici_lastuserid,
		ici_lasttopicname,
		ici_lastuser,
		ici_lastuserdisplayname,
		b.flags,
		ici_style AS "Style",
	(SELECT COUNT(1) FROM {databaseSchema}.{objectQualifier}active xz 
	JOIN {databaseSchema}.{objectQualifier}user usr 
	ON xz.userid = usr.userid 
	WHERE xz.forumid=b.forumid 
	AND usr.isactiveexcluded IS FALSE) AS Viewing, 
		b.remoteurl,
		x.readaccess::integer,
		(case(i_findlastunread)
			 when true THEN
			   (SELECT LastAccessDate FROM {databaseSchema}.{objectQualifier}forumreadtracking x WHERE x.forumid=b.forumid AND x.userid = i_userid LIMIT 1)
			 else null	 end) AS LastForumAccess,
			 ici_lasttopicaccess  AS LastTopicAccess		
	FROM 
		{databaseSchema}.{objectQualifier}category a
		JOIN {databaseSchema}.{objectQualifier}forum b on b.categoryid=a.categoryid
		JOIN {databaseSchema}.{objectQualifier}activeaccess x on x.forumid=b.forumid
	--	JOIN {databaseSchema}.{objectQualifier}forum_ns_getsubtree(i_parentid, true) q ON q."ForumID" = b.forumid
	WHERE
		(i_categoryid IS NULL OR a.categoryid=i_categoryid) AND
		 x.userid = i_userid  and		
		 b.createdbyuserid = i_forumcreatedbyuserid and b.isuserforum 	
		 and ((b.flags & 2)=0 OR x.readaccess) 	
	ORDER BY 
		a.sortorder,
		b.sortorder 		
	LOOP 	 
					IF  (_rec."LastTopicID" IS NULL OR _rec."LastPosted"	IS NULL) THEN	 
					 _rec."LastTopicID" := {databaseSchema}.{objectQualifier}forum_lasttopic(_rec."ForumID",i_userid,_rec."LastTopicID",_rec."LastPosted");
	END IF;
	IF  (_rec."LastTopicID" IS NULL OR _rec."LastPosted"	IS NULL AND _rec."TopicMovedID" IS NOT NULL) THEN	 
	 _rec."LastTopicID":={databaseSchema}.{objectQualifier}forum_lasttopic(_rec."ForumID",i_userid,_rec."TopicMovedID",_rec."LastPosted");
	END IF;	  	
	 SELECT    t.lastposted , 
			   t.lastmessageid, 
			   t.lastmessageflags, 
			   t.lastuserid,
			   t.topicmovedid,
			   t.topic,
			   t.status,
			   t.styles,
			   (case when (i_findlastunread IS TRUE)
			   THEN
			   (SELECT lastaccessdate FROM {databaseSchema}.{objectQualifier}topicreadtracking y WHERE y.topicid=t.topicid AND y.userid = i_userid)
			   else null	 end)
	INTO
	_rec."LastPosted" , 
	_rec."LastMessageID" , 
	_rec."LastMessageFlags",
	_rec."LastUserID",
	_rec."TopicMovedID",
	_rec."LastTopicName", 
	_rec."LastTopicStatus",
	_rec."LastTopicStyles",
	_rec."LastTopicAccess"
	 FROM 
		{databaseSchema}.{objectQualifier}topic t
		WHERE   t.topicid=_rec."LastTopicID" LIMIT 1; 		
	  
	   _rec."LastUser" = COALESCE((SELECT t.lastusername FROM 
		{databaseSchema}.{objectQualifier}topic t
		WHERE  t.topicid=_rec."LastTopicID" LIMIT 1),(SELECT u2.name
			 FROM   {databaseSchema}.{objectQualifier}user u2
			 WHERE  u2.userid = _rec."LastUserID" LIMIT 1));
	   _rec."LastUserDisplayName" = COALESCE((SELECT t.lastuserdisplayname FROM 
		{databaseSchema}.{objectQualifier}topic t
		WHERE  t.topicid=_rec."LastTopicID"  LIMIT 1),(SELECT u2.displayname
			 FROM   {databaseSchema}.{objectQualifier}user u2
			 WHERE  u2.userid = _rec."LastUserID" LIMIT 1));
	_rec."Style" := (case(i_stylednicks)
			when true THEN  (SELECT us.userstyle FROM {databaseSchema}.{objectQualifier}user us where us.userid = _rec."LastUserID")   
			else ''	 end); 
	RETURN NEXT _rec;	
END LOOP;

END;

$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100
  ROWS 1000; 
  --GO 

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}forum_ns_listread(
						   i_boardid integer,
						   i_userid integer,
						   i_categoryid integer,
						   i_parentid integer,
						   i_stylednicks boolean,
						   i_findlastunread boolean,
						   i_showcommonforums boolean,
						   i_showpersonalforums boolean,
						   i_forumcreatedbyuserid integer,
						   i_UTCTIMESTAMP timestamp)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}forum_ns_listread_return_type AS
$BODY$DECLARE
ici_lasttopicid integer;
ici_topicmovedid integer;
ici_lastposted timestamp ;
ici_lastmessageid integer;
ici_lastmessageflags integer;
ici_lastuserid integer;
ici_lastuserdisplayname varchar(255);
ici_lasttopicname varchar(255);
ici_lasttopicstyles varchar(255);
ici_lastuser varchar(255);
ici_pollgroupid integer; 
ici_style varchar(255):='';
ici_lasttopicstatus  varchar(255):='';
ici_lasttopicaccess  timestamp;
lvl integer := 0;
rk integer; 
lk integer;
intcnt integer:=0; 
_rectemp {databaseSchema}.{objectQualifier}forum_listread_tmp%ROWTYPE;
_rec {databaseSchema}.{objectQualifier}forum_ns_listread_return_type%ROWTYPE; 
BEGIN	
 

 IF i_parentid IS NOT NULL THEN
 SELECT "level",left_key + 1, right_key - 1 into lvl, lk, rk 
 from {databaseSchema}.{objectQualifier}forum   
 where forumid = i_parentid limit 1;
 END IF;
 IF i_parentid IS NULL AND i_categoryid > 0 THEN
 SELECT -1, min(n.left_key), max(n.right_key)  into lvl, lk, rk 
 from {databaseSchema}.{objectQualifier}forum n
 where n.categoryid = i_categoryid;; 
 END IF;
 IF i_parentid IS NULL AND i_categoryid IS NULL THEN
 SELECT -1, min(n.left_key), max(n.right_key) into lvl, lk, rk 
 from {databaseSchema}.{objectQualifier}forum n 
 join {databaseSchema}.{objectQualifier}category c
 on c.categoryid = n.categoryid
 where c.boardid = i_boardid;
 END IF;

FOR _rec IN
	SELECT 
		a.categoryid, 
		a.name AS Category, 
		b.forumid AS ForumID,
		b.parentid,
		b.name AS Forum, 
		b.description, 		
		b.imageurl,		
		b.pollgroupid, 
		b.isuserforum,	
		b.left_key,
		b.right_key,
		i_userid,	
		(select sum(fp.NumTopics) from {databaseSchema}.{objectQualifier}Forum fp
		where fp.CategoryID = b.CategoryID and fp.left_key >= b.left_key and fp.right_key <= b.right_key) as Topics,
		(select sum(fp.NumPosts) from {databaseSchema}.{objectQualifier}Forum fp
		where fp.CategoryID = b.CategoryID and fp.left_key >= b.left_key and fp.right_key <= b.right_key) as Posts,	
		b.lasttopicid,
		ici_lasttopicstatus,
		ici_lasttopicstyles,
		ici_topicmovedid,
		b.lastposted,
		ici_lastmessageid,
		ici_lastmessageflags,
		ici_lastuserid,
		ici_lasttopicname,
		ici_lastuser,
		'',
		b.flags,
		ici_style AS "Style",
	(SELECT COUNT(1) FROM {databaseSchema}.{objectQualifier}active xz 
	JOIN {databaseSchema}.{objectQualifier}user usr 
	ON xz.userid = usr.userid 
	WHERE xz.forumid=b.forumid 
	AND usr.isactiveexcluded IS FALSE) AS Viewing, 
		b.remoteurl,
		x.readaccess::integer,
		(case(i_findlastunread)
			 when true THEN
			   (SELECT LastAccessDate FROM {databaseSchema}.{objectQualifier}forumreadtracking x WHERE x.forumid=b.forumid AND x.userid = i_userid LIMIT 1)
			 else null	 end) AS LastForumAccess,
			 ici_lasttopicaccess  AS LastTopicAccess			 		
	FROM 
		{databaseSchema}.{objectQualifier}category a
		JOIN {databaseSchema}.{objectQualifier}forum b on (b.categoryid=a.categoryid and a.boardid = i_boardid)
		JOIN {databaseSchema}.{objectQualifier}activeaccess x on (x.forumid=b.forumid and x.userid = i_userid) 
	WHERE
		(i_categoryid IS NULL OR a.categoryid = i_categoryid) AND
		(b."level" >= lvl-1) and		 
		((b.flags & 2)=0 OR x.readaccess) and         	
		b.left_key >= lk and b.right_key <= rk 	
	ORDER BY
		a.sortorder,
		b.left_key
	LOOP 	 
	IF  (_rec."LastTopicID" IS NULL OR _rec."LastPosted" IS NULL) THEN
	 _rec."LastTopicID" := {databaseSchema}.{objectQualifier}forum_ns_lasttopic(_rec."left_key",_rec."right_key", _rec."CategoryID", _rec."PageUserID"); 
	END IF; 
	IF  (_rec."LastTopicID" IS NULL OR _rec."LastPosted" IS NULL AND _rec."TopicMovedID" IS NOT NULL) THEN	 
			_rec."LastTopicID" := {databaseSchema}.{objectQualifier}forum_ns_lasttopic(_rec."left_key",_rec."right_key", _rec."CategoryID", _rec."PageUserID");   
	END IF;	  	
	 SELECT    t.lastposted, 
			   t.lastmessageid, 
			   t.lastmessageflags, 
			   t.lastuserid,
			   COALESCE(t.lastusername,(SELECT u2.name FROM   {databaseSchema}.{objectQualifier}user u2 WHERE  u2.userid = t.lastuserid LIMIT 1)),
			   COALESCE(t.lastuserdisplayname,(SELECT u2.displayname FROM   {databaseSchema}.{objectQualifier}user u2 WHERE  u2.userid = t.lastuserid LIMIT 1)),
			   t.topicmovedid,
			   t.topic,
			   t.status,
			   t.styles,
			   (case when (i_findlastunread IS TRUE)
			   THEN
			   (SELECT lastaccessdate FROM {databaseSchema}.{objectQualifier}topicreadtracking y WHERE y.topicid=t.topicid AND y.userid = i_userid)
			   else null	 end)
	INTO
	_rec."LastPosted" , 
	_rec."LastMessageID" , 
	_rec."LastMessageFlags",
	_rec."LastUserID",
	_rec."LastUser",
	_rec."LastUserDisplayName",
	_rec."TopicMovedID",
	_rec."LastTopicName", 
	_rec."LastTopicStatus",
	_rec."LastTopicStyles",
	_rec."LastTopicAccess"
	 FROM 
		{databaseSchema}.{objectQualifier}topic t
		WHERE   t.topicid=_rec."LastTopicID" LIMIT 1; 	  

	 _rec."Style" := (case(i_stylednicks)
			when true THEN (SELECT us.userstyle FROM {databaseSchema}.{objectQualifier}user us where us.userid = _rec."LastUserID")   
			else ''	 end); 
	RETURN NEXT _rec;	
END LOOP;

END;

$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100
  ROWS 1000; 
  --GO 

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}forum_ns_listread_test(
						   i_boardid integer,
						   i_userid integer,
						   i_categoryid integer,
						   i_parentid integer,
						   i_stylednicks boolean,
						   i_findlastunread boolean,
						   i_showcommonforums boolean,
						   i_showpersonalforums boolean,
						   i_forumcreatedbyuserid integer,
						   i_UTCTIMESTAMP timestamp)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}forum_ns_listread_return_type AS
$BODY$DECLARE
lvl integer := 0;
rk integer; 
lk integer;
_rec {databaseSchema}.{objectQualifier}forum_ns_listread_return_type%ROWTYPE; 
BEGIN
 IF i_parentid IS NOT NULL THEN
 SELECT "level",left_key + 1, right_key - 1 into lvl, lk, rk 
 from {databaseSchema}.{objectQualifier}forum   
 where forumid = i_parentid limit 1;
 END IF;
 IF i_parentid IS NULL AND i_categoryid > 0 THEN
 SELECT 0, min(n.left_key), max(n.right_key)  into lvl, lk, rk 
 from {databaseSchema}.{objectQualifier}forum n
 where n.categoryid = i_categoryid; 
 END IF;
 IF i_parentid IS NULL AND i_categoryid IS NULL THEN
 SELECT 0, min(n.left_key), max(n.right_key) into lvl, lk, rk 
 from {databaseSchema}.{objectQualifier}forum n 
 join {databaseSchema}.{objectQualifier}category c
 on c.categoryid = n.categoryid
 where c.boardid = i_boardid;
 END IF;
 lvl := lvl -1;
FOR _rec IN
	SELECT 
		a.categoryid, 
		a.name AS Category, 
		b.forumid AS ForumID,
		b.parentid,
		b.name AS Forum, 
		b.description, 		
		b.imageurl,		
		b.pollgroupid, 
		b.isuserforum,	
		b.left_key,
		b.right_key,
		i_userid,	
		(select sum(fp.NumTopics) from {databaseSchema}.{objectQualifier}Forum fp	
		where fp.CategoryID = b.CategoryID and fp.left_key >= b.left_key and fp.right_key <= b.right_key) as Topics,
		(select sum(fp.NumPosts) from {databaseSchema}.{objectQualifier}Forum fp		
		where fp.CategoryID = b.CategoryID and fp.left_key >= b.left_key and fp.right_key <= b.right_key) as Posts,		
		t.topicid,
		t.status,
		t.styles,
		t.topicmovedid,
		t.lastposted, 
		t.lastmessageid, 
		t.lastmessageflags, 
		t.lastuserid,
		t.topic,
		COALESCE(t.lastusername,(SELECT u2.name FROM   {databaseSchema}.{objectQualifier}user u2 WHERE  u2.userid = t.lastuserid LIMIT 1)),
		COALESCE(t.lastuserdisplayname,(SELECT u2.name FROM   {databaseSchema}.{objectQualifier}user u2 WHERE  u2.userid = t.lastuserid LIMIT 1)),
		b.flags,
		(case(i_stylednicks)
			when true THEN (SELECT us.userstyle FROM {databaseSchema}.{objectQualifier}user us where us.userid = _rec."LastUserID")   
			else ''	 end) as "Style",
	(SELECT COUNT(1) FROM {databaseSchema}.{objectQualifier}active xz 
	JOIN {databaseSchema}.{objectQualifier}user usr 
	ON xz.userid = usr.userid 
	WHERE xz.forumid=b.forumid 
	AND usr.isactiveexcluded IS FALSE) AS Viewing, 
		b.remoteurl,
		x.readaccess::integer,
		(case(i_findlastunread)
			 when true THEN
			   (SELECT LastAccessDate FROM {databaseSchema}.{objectQualifier}forumreadtracking x WHERE x.forumid=b.forumid AND x.userid = i_userid LIMIT 1)
			 else null	 end) AS LastForumAccess,
			 (case when (i_findlastunread IS TRUE)
			   THEN
			   (SELECT lastaccessdate FROM {databaseSchema}.{objectQualifier}topicreadtracking y WHERE y.topicid=t.topicid AND y.userid = i_userid)
			   else null end)  AS LastTopicAccess			 		
	FROM 
		{databaseSchema}.{objectQualifier}category a
		JOIN {databaseSchema}.{objectQualifier}forum b on (b.categoryid=a.categoryid and a.boardid = i_boardid)
		JOIN {databaseSchema}.{objectQualifier}activeaccess x on (x.forumid=b.forumid and x.userid = i_userid) 
		left outer join {databaseSchema}.{objectQualifier}topic t ON t.topicid = {databaseSchema}.{objectQualifier}forum_ns_lasttopic(b.left_key,b.right_key, a.categoryid, i_userid)
	WHERE
		(i_categoryid IS NULL OR a.categoryid = i_categoryid) AND
		(b."level" >= lvl) and		 
		((b.flags & 2)=0 OR x.readaccess) and         	
		b.left_key >= lk and b.right_key <= rk 	
	ORDER BY
		a.sortorder,
		b.left_key
	LOOP     
	RETURN NEXT _rec;	
	END LOOP;
END;

$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100
  ROWS 1000; 
  --GO 

-- Function: {databaseSchema}.{objectQualifier}forum_listsubforums(integer)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}forum_listsubforums(integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}forum_listsubforums(
						   i_forumid integer)
				  RETURNS integer AS
$BODY$
BEGIN
RETURN (SELECT SUM(1) FROM {databaseSchema}.{objectQualifier}forum WHERE parentid = i_forumid);
END;
$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100; 
--GO

-- Function: {databaseSchema}.{objectQualifier}forum_listtopics(integer)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}forum_listtopics(integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}forum_listtopics(
						   i_forumid integer)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}forum_listtopics_return_type AS
$BODY$DECLARE
_rec  {databaseSchema}.{objectQualifier}forum_listtopics_return_type%ROWTYPE;
BEGIN
	 FOR _rec IN 
	 SELECT 
		   t.topicid,
		   t.forumid,
		   t.userid,
		   t.username,
		   t.posted,
		   t.topic,
		   t.views,
		   t.priority,
		   t.pollid,
		   t.topicmovedid,
		   t.lastposted,
		   t.lastmessageid,
		   t.lastuserid,
		   t.lastusername,
		   t.numposts,
		   t.flags,
		   t.answermessageid,
		   t.lastmessageflags,
		   t.description,
		   t.status,
		   t.isdeleted,
		   t.isquestion
	FROM {databaseSchema}.{objectQualifier}topic t
	WHERE t.forumid = i_forumid
		 LOOP
			 RETURN NEXT _rec;
		 END LOOP;

END;$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100 ROWS 1000; 
--GO

-- Function: {databaseSchema}.{objectQualifier}forum_moderatelist(integer, integer)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}forum_moderatelist(integer, integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}forum_moderatelist(
						   i_boardid integer, 
						   i_userid integer,
						   i_isuserforum boolean)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}forum_moderatelist_return_type AS
$BODY$DECLARE
_rec {databaseSchema}.{objectQualifier}forum_moderatelist_return_type%ROWTYPE;
BEGIN
 FOR _rec IN
 SELECT
 
		b.forumid,
		b.categoryid,
		b.parentid,
		b.name,
		b.description,
		b.sortorder,
		b.lastposted,
		b.lasttopicid,
		b.lastmessageid,
		b.lastuserid,
		b.lastusername,
		b.numtopics,
		b.numposts,
		b.remoteurl,
		b.flags,
		b.themeurl,
		b.imageurl,
		b.styles,
		b.pollgroupid,
		(SELECT     COUNT({databaseSchema}.{objectQualifier}message.messageid)
		FROM         {databaseSchema}.{objectQualifier}message
				INNER JOIN  {databaseSchema}.{objectQualifier}topic 
				ON {databaseSchema}.{objectQualifier}message.topicid =  {databaseSchema}.{objectQualifier}topic.topicid
		WHERE (({databaseSchema}.{objectQualifier}message.flags & 16)=0) and (({databaseSchema}.{objectQualifier}Message.flags & 8)=0) 
						AND (({databaseSchema}.{objectQualifier}topic.flags & 8) = 0) 
						AND ({databaseSchema}.{objectQualifier}topic.forumid=b.forumid))   AS MessageCount,
		(SELECT     count({databaseSchema}.{objectQualifier}message.messageid)
		FROM         {databaseSchema}.{objectQualifier}message 
				INNER JOIN  {databaseSchema}.{objectQualifier}topic
				ON {databaseSchema}.{objectQualifier}message.topicid = {databaseSchema}.{objectQualifier}topic.topicid
		WHERE (({databaseSchema}.{objectQualifier}message.flags & 128)=128) 
				AND (({databaseSchema}.{objectQualifier}message.flags & 8)=0) 
				AND (({databaseSchema}.{objectQualifier}topic.flags & 8) = 0) 
				AND ({databaseSchema}.{objectQualifier}topic.forumid=b.forumid)) AS ReportedCount ,
		 0       		
			FROM
		{databaseSchema}.{objectQualifier}category a 
			JOIN {databaseSchema}.{objectQualifier}forum b ON b.categoryid=a.categoryid
		  /*  JOIN {databaseSchema}.{objectQualifier}vaccess c ON c.forumid=b.forumid  */
			WHERE
		a.boardid=i_boardid AND
		b.isuserforum = i_isuserforum		
		 /* AND
		c.moderatoraccess>0 AND
		c.userid=i_userid */
			ORDER BY
		a.sortorder,
		b.sortorder
LOOP
	-- IF _rec."MessageCount" > 0 OR _rec."ReportCount" > 0  OR _rec."SpamCount" >0 THEN
	IF COALESCE((SELECT "ModeratorAccess" FROM {databaseSchema}.{objectQualifier}vaccess_combo(i_userid, _rec."ForumID") LIMIT 1),0) > 0  THEN
	RETURN NEXT _rec;	
	END IF;
END LOOP;
		
 END;
$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100
  ROWS 1000;
--GO

-- Function: {databaseSchema}.{objectQualifier}forum_moderators()

-- DROP FUNCTION {databaseSchema}.{objectQualifier}forum_moderators();

/* CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}forum_moderators(i_stylednicks boolean)
  RETURNS SETOF {databaseSchema}.{objectQualifier}forum_moderators_return_type AS
$BODY$DECLARE 
bf0 boolean:=false;
bf1 boolean:=true;
ici_moderatoraccess integer;
ici_admingroup  integer;
_rec {databaseSchema}.{objectQualifier}forum_moderators_return_type%ROWTYPE;
BEGIN
FOR _rec IN
	SELECT
		CAST(a.forumid AS integer) AS forumid, 
		a.groupid AS "ModeratorID", 
		b.name AS "ModeratorName",
		'' AS "Style",
		bf1 AS "IsGroup" 
		/*
		0 AS "ModeratorAccess",
		0 AS "AdminGroup" */
	FROM
		{databaseSchema}.{objectQualifier}forumaccess a
		INNER JOIN {databaseSchema}.{objectQualifier}group b 
		ON b.groupid = a.groupid
		INNER JOIN {databaseSchema}.{objectQualifier}accessmask c 
		ON c.accessmaskid = a.accessmaskid
	WHERE
		(b.flags & 1) = 0 
				 AND
		(c.flags & 64) <> 0
	UNION ALL
	SELECT 
		CAST(acc.forumid AS integer) AS forumid,  		 
		usr.userid AS "ModeratorID", 
		usr.name AS "ModeratorName",
		'' AS "Style",
		/* (case when (i_stylednicks IS TRUE)
			THEN  COALESCE((SELECT f.style FROM {databaseSchema}.{objectQualifier}usergroup e 
			join {databaseSchema}.{objectQualifier}group f on f.groupid=e.groupid WHERE e.userid=usr.userid AND LEN(f.style) > 2 ORDER BY f.sortorder limit 1), 
			r.style)  
			else ''	end) */		 
		bf0 AS "IsGroup"
		/*
		0 AS "ModeratorAccess",
		0 AS "AdminGroup" */
		FROM
		{databaseSchema}.{objectQualifier}userselectview usr
		JOIN {databaseSchema}.{objectQualifier}rank r
		INNER JOIN (
			SELECT
				a.userid,
				x.forumid,
				max(x.moderatoraccess) AS "ModeratorAccess" 
			FROM
				{databaseSchema}.{objectQualifier}vaccessfull as x
				INNER JOIN {databaseSchema}.{objectQualifier}usergroup a 
				 ON a.userid=x.userid 
				INNER JOIN {databaseSchema}.{objectQualifier}group b 
				ON b.groupid=a.groupid
			 WHERE 
			x.moderatoraccess <> 0 AND	x.admingroup = 0 
			GROUP BY
				a.userid,x.forumid		
		) acc ON acc.userid = usr.userid
	WHERE
		acc."ModeratorAccess" <> 0
	ORDER BY
		"IsGroup" desc,
		"ModeratorName" asc
LOOP
  /*  IF _rec."IsGroup" IS FALSE THEN
	SELECT "ModeratorAccess","IsGroup" into ici_moderatoraccess, ici_admingroup  FROM {databaseSchema}.{objectQualifier}vaccess_combo(i_userid, _rec."ForumID")
	IF ici_moderatoraccess <> 0 AND ici_admingroup = 0 THEN
	RETURN NEXT _rec;	
	END IF;
	ELSE
	RETURN NEXT _rec;
	END IF; */

RETURN NEXT _rec;
END LOOP;

END;$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100 ROWS 1000;
   GRANT EXECUTE ON FUNCTION {databaseSchema}.{objectQualifier}forum_moderators() TO granteeName;
GO 

select
		ForumID = a.ForumID, 
		ModeratorID = a.GroupID, 
		ModeratorName = b.Name,
		IsGroup=1
	from
		[dbo].[yaf_ForumAccess] a WITH(NOLOCK)
		INNER JOIN [dbo].[yaf_Group] b WITH(NOLOCK) ON b.GroupID = a.GroupID
		INNER JOIN [dbo].[yaf_AccessMask] c WITH(NOLOCK) ON c.AccessMaskID = a.AccessMaskID
	where
		(b.Flags & 1)=0 and
		(c.Flags & 64)<>0
	union all
	select 
		ForumID = access.ForumID, 
		ModeratorID = usr.UserID, 
		ModeratorName = usr.Name,
		IsGroup=0
	from
		[dbo].[yaf_User] usr WITH(NOLOCK)
		INNER JOIN (        
			select
				UserID                = a.UserID,
				ForumID                = x.ForumID,
				ModeratorAccess        = MAX(ModeratorAccess)
			from
				(
				SELECT UserID, ForumID, MAX(ReadAccess) AS ReadAccess, MAX(PostAccess) AS PostAccess, MAX(ReplyAccess) AS ReplyAccess, MAX(PriorityAccess) AS PriorityAccess, 
							MAX(PollAccess) AS PollAccess, MAX(VoteAccess) AS VoteAccess, MAX(ModeratorAccess) AS ModeratorAccess, MAX(EditAccess) AS EditAccess, MAX(DeleteAccess) 
				AS DeleteAccess, MAX(UploadAccess) AS UploadAccess, MAX(DownloadAccess) AS DownloadAccess, MAX(AdminGroup) AS AdminGroup
				FROM 
				(
				SELECT UserID, ForumID, ReadAccess, PostAccess, ReplyAccess, PriorityAccess, PollAccess, VoteAccess, ModeratorAccess, EditAccess, DeleteAccess, 
											 UploadAccess, DownloadAccess, AdminGroup
				FROM dbo.yaf_vaccess_user AS b where ModeratorAccess <> 0 AND AdminGroup = 0
				UNION ALL
				SELECT UserID, ForumID, ReadAccess, PostAccess, ReplyAccess, PriorityAccess, PollAccess, VoteAccess, ModeratorAccess, EditAccess, DeleteAccess, 
											 UploadAccess, DownloadAccess, AdminGroup
				FROM dbo.yaf_vaccess_group AS b where ModeratorAccess <> 0 AND AdminGroup = 0
				UNION ALL
				SELECT UserID, ForumID, ReadAccess, PostAccess, ReplyAccess, PriorityAccess, PollAccess, VoteAccess, ModeratorAccess, EditAccess, DeleteAccess, 
											 UploadAccess, DownloadAccess, AdminGroup
				FROM dbo.yaf_vaccess_null AS b where ModeratorAccess <> 0 AND AdminGroup = 0
				) AS access
				GROUP BY UserID, ForumID
			) as x         
				INNER JOIN [dbo].[yaf_UserGroup] a WITH(NOLOCK) on a.UserID=x.UserID
				INNER JOIN [dbo].[yaf_Group] b WITH(NOLOCK) on b.GroupID=a.GroupID
			WHERE 
				ModeratorAccess <> 0 AND x.AdminGroup = 0
			GROUP BY a.UserId, x.ForumID
		) access ON usr.UserID = access.UserID
	where
		access.ModeratorAccess<>0
	order by
		IsGroup desc,
		ModeratorName asc

*/


CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}forum_moderators(
						   i_stylednicks boolean)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}forum_moderators_return_type AS
$BODY$DECLARE 
bf0 boolean:=false;
bf1 boolean:=true;
ici_moderatoraccess integer;
ici_admingroup  integer;
_rec {databaseSchema}.{objectQualifier}forum_moderators_return_type%ROWTYPE;
BEGIN
FOR _rec IN
	SELECT
		CAST(a.forumid AS integer) AS forumid, 
		f.Name AS "ForumName",
		a.groupid AS "ModeratorID", 
		b.name AS "ModeratorName",
		b.name AS "ModeratorDisplayName",
		'' AS "ModeratorEmail",
		'' AS "ModeratorAvatar",
		FALSE as "ModeratorAvatarImage",
		case(i_stylednicks)
			when TRUE THEN b.style  
			else ''	 end AS "Style",
		true AS "IsGroup"
	FROM
		{databaseSchema}.{objectQualifier}forum f
		INNER JOIN {databaseSchema}.{objectQualifier}forumaccess a ON a.forumid = f.forumid
		INNER JOIN {databaseSchema}.{objectQualifier}group b ON b.groupid = a.groupid
		INNER JOIN {databaseSchema}.{objectQualifier}accessmask c 
		ON c.accessmaskid = a.accessmaskid
	WHERE
		(b.flags & 1)<>1 
				 AND
		(c.flags & 64)=64
	UNION ALL
	SELECT 
		CAST(acc.forumid AS integer) AS forumid, 
		f.Name AS "ForumName",	 
		usr.userid AS "ModeratorID", 
		usr.name AS "ModeratorName",
		usr.displayname AS "ModeratorDisplayName",
		usr.email AS "ModeratorEmail",
		COALESCE(usr.avatar, '') AS "ModeratorAvatar",
		(EXISTS (select 1 from {databaseSchema}.{objectQualifier}user x where x.userid=usr.userid and avatarimage is not null LIMIT 1))  AS "ModeratorAvatarImage",		
		(case when (i_stylednicks IS TRUE)
			THEN  usr.userstyle
			else ''	end) AS "Style",
		false AS "IsGroup"
			FROM
		{databaseSchema}.{objectQualifier}user usr	
		INNER JOIN (
			SELECT
				a.userid,
				x.forumid,
				max(x.moderatoraccess) AS "ModeratorAccess" 
			FROM
				((SELECT b.userid, 
			b.forumid,          
			(c.flags & 64) AS moderatoraccess,
			0 AS admingroup 
	 FROM ({databaseSchema}.{objectQualifier}userforum b 
	 JOIN {databaseSchema}.{objectQualifier}accessmask c 
	 ON ((c.accessmaskid = b.accessmaskid))) 
	 UNION ALL 
	 SELECT b.userid,
			c.forumid,           
			(d.flags & 64) AS moderatoraccess,
			(e.flags & 1) AS admingroup 
	FROM ((({databaseSchema}.{objectQualifier}usergroup b 
	JOIN {databaseSchema}.{objectQualifier}forumaccess c 
	ON ((c.groupid = b.groupid))) 
	JOIN {databaseSchema}.{objectQualifier}accessmask d 
	ON ((d.accessmaskid = c.accessmaskid))) 
	JOIN {databaseSchema}.{objectQualifier}group e 
	ON ((e.groupid = b.groupid)))) 
	UNION ALL 
	SELECT 
		  a.userid, 
		  0 AS forumid,      
		  0 AS moderatoraccess,
		  0 AS admingroup 
	 FROM {databaseSchema}.{objectQualifier}user a) as x
				INNER JOIN {databaseSchema}.{objectQualifier}usergroup a 
				 ON a.userid=x.userid 
				INNER JOIN {databaseSchema}.{objectQualifier}group b 
				ON b.groupid=a.groupid				
			 WHERE 
			x.moderatoraccess > 0 AND x.admingroup = 0 
			GROUP BY
				a.userid,x.forumid		
		) acc ON usr.userid = acc.userid
		JOIN {databaseSchema}.{objectQualifier}forum f
		 ON f.forumid = acc.forumid
	WHERE
		acc."ModeratorAccess">0
	ORDER BY
		"IsGroup" desc,
		"ModeratorName" asc
LOOP
  /*  IF _rec."IsGroup" IS FALSE THEN
	SELECT "ModeratorAccess","IsGroup" into ici_moderatoraccess, ici_admingroup  FROM {databaseSchema}.{objectQualifier}vaccess_combo(i_userid, _rec."ForumID")
	IF ici_moderatoraccess <> 0 AND ici_admingroup = 0 THEN
	RETURN NEXT _rec;	
	END IF;
	ELSE
	RETURN NEXT _rec;
	END IF; */
RETURN NEXT _rec;
END LOOP;

END;$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100 ROWS 1000;  
--GO

-- DROP FUNCTION {databaseSchema}.{objectQualifier}forum_resync(integer, integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}forum_resync(
						  i_boardid integer, 
						  i_forumid integer)
				  RETURNS void AS
$BODY$DECLARE
currForums refcursor;
itmpForumID INT;
BEGIN        

	IF i_forumid IS NULL THEN		
 
		OPEN currForums FOR
			SELECT 
				a.forumid
			FROM
				{databaseSchema}.{objectQualifier}forum a
				JOIN {databaseSchema}.{objectQualifier}category b on a.categoryid=b.categoryid
				JOIN {databaseSchema}.{objectQualifier}board c on b.boardid = c.boardid  
			WHERE
				c.boardid=i_boardid;

		
		 -- cycle through forums
			   LOOP
				FETCH currForums INTO itmpForumID; 
		EXIT WHEN NOT FOUND;

		  -- update statistics
		  PERFORM  {databaseSchema}.{objectQualifier}forum_updatestats(itmpForumID);
		  -- update last post
		  PERFORM {databaseSchema}.{objectQualifier}forum_updatelastpost(itmpForumID);	
 
		EXIT WHEN NOT FOUND;
				END LOOP;
		CLOSE currForums; 	
	
	ELSE 		
		-- update statistics
		PERFORM {databaseSchema}.{objectQualifier}forum_updatestats(i_forumid);
		-- update last post
		PERFORM {databaseSchema}.{objectQualifier}forum_updatelastpost(i_forumid);
	END IF;
END;$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100;
 --GO

-- Function: {databaseSchema}.{objectQualifier}forum_save(integer, integer, integer, varchar, varchar, smallint, boolean, boolean, boolean, boolean, varchar, varchar, integer)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}forum_save(integer, integer, integer, varchar, varchar, smallint, boolean, boolean, boolean, boolean, varchar, varchar, varchar, varchar,integer,integer,boolean,boolean,timestamp);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}forum_save(
						   i_forumid integer, 
						   i_categoryid integer, 
						   i_parentid integer, 
						   i_name varchar, 
						   i_description varchar, 
						   i_sortorder integer, 
						   i_locked boolean, 
						   i_hidden boolean, 
						   i_istest boolean, 
						   i_moderated boolean, 
						   i_remoteurl varchar, 
						   i_themeurl varchar, 
						   i_imageurl  varchar,
						   i_styles  varchar,
						   i_accessmaskid integer,
						   i_userid integer,
						   i_isuserforum boolean,
						   i_canhavepersforums boolean,
						   i_adjacentforumid int,
						   i_adjacentforummode int,
						   i_utctimestamp timestamp)
				   RETURNS integer AS
$BODY$DECLARE 
	  ici_ForumID integer:=i_forumid;
	  ici_boardid	integer;
	  ici_flags		integer:=0 ;	
	  ici_nid		integer;
	  ici_UserName  VARCHAR(255);
	  ici_userdisplayname  VARCHAR(255);
	  l_ForumId integer;
	  ici_cntr integer := 0;
	  _rec RECORD;
	  afterset bool;
	  newlk integer;
BEGIN
	IF i_parentid = 0 THEN 
	   i_parentid = null;     
	END IF;
  -- set new sort order for categories if adjacent category was supplied 
	-- i_adjacentforummode can be before(1), after(2) and over(3)
	-- -1 mode means that forum sortorder will not be updated. 

	 if (i_adjacentforumid is not null) then	 
	   -- over  	
				  if (i_adjacentforummode = 3) THEN		               
					   i_sortorder := 0;
					   ici_cntr := 1;
				  end if;   
	   FOR _rec in select f.forumid from {databaseSchema}.{objectQualifier}forum f
		   join {databaseSchema}.{objectQualifier}category c
		   on c.categoryid = f.categoryid
		   where c.categoryid = i_categoryid and (i_parentid is null or f.parentid = i_parentid)   
		   order by f.sortorder	     
		 LOOP 
			   if (i_adjacentforumid = _rec.forumid) THEN	          	
				-- before
					 if (i_adjacentforummode = 1) THEN		               
					   i_sortorder := ici_cntr;
					   ici_cntr := ici_cntr + 1;	               
					   end if;                 
			   -- after
				   if (i_adjacentforummode = 2) THEN		               
					   i_sortorder := ici_cntr + 1;
					   afterset := true;	
					   end if;              
			   end if;
			if (i_sortorder::integer = ici_cntr and afterset) THEN 
			 ici_cntr := ici_cntr + 1;
			end if;
			update	{databaseSchema}.{objectQualifier}forum
			set sortorder = ici_cntr where forumid = _rec.forumid;
			
			ici_cntr := ici_cntr + 1;	
		
	END LOOP;
	end if;	
   

	IF i_locked IS NOT FALSE THEN ici_flags := ici_flags | 1;END IF;
	IF i_hidden IS NOT FALSE THEN  ici_flags := ici_flags | 2;END IF;
	IF i_istest IS NOT FALSE THEN  ici_flags := ici_flags | 4;END IF;
	IF i_moderated IS NOT FALSE THEN  ici_flags := ici_flags | 8;END IF;
  SELECT boardid INTO ici_boardid from {databaseSchema}.{objectQualifier}category
	WHERE categoryid=i_categoryid;

 IF i_userid IS NOT NULL THEN   
	SELECT Name, DisplayName INTO ici_UserName, ici_userdisplayname FROM {databaseSchema}.{objectQualifier}user where userid = i_userid LIMIT  1;
	   -- guests should not create forums
	ELSE
	SELECT BoardID INTO ici_BoardID FROM {databaseSchema}.{objectQualifier}category  WHERE categoryid=i_categoryid;
	SELECT Name, DisplayName INTO ici_UserName, ici_userdisplayname FROM {databaseSchema}.{objectQualifier}user where BoardID = ici_BoardID and (Flags & 4) = 4  ORDER BY Joined LIMIT 1;
	END IF;


  IF ici_ForumID > 0 THEN
   if (i_adjacentforumid is not null)  then  
	if (i_adjacentforummode = 1) then 
	select left_key into newlk from {databaseSchema}.{objectQualifier}forum where forumid = i_adjacentforumid;
	end if;
	-- after 
	if (i_adjacentforummode = 2) then 
	select right_key into newlk from {databaseSchema}.{objectQualifier}forum where forumid = i_adjacentforumid;	
	end if;

  end if;
  
  UPDATE {databaseSchema}.{objectQualifier}forum
  SET
  parentid = i_parentid,
  name= substr(i_name, 1, 255),
  description=i_description,
  sortorder= (case when i_adjacentforummode != -1 THEN i_sortorder else sortorder  end),
  categoryid=i_categoryid,
  remoteurl = i_remoteurl,
  themeurl = i_themeurl,
  flags = ici_flags,
  imageurl = i_imageurl,
  styles = i_styles,
  isuserforum = i_isuserforum,
  canhavepersforums = i_canhavepersforums 
  WHERE forumid=ici_ForumID; 
  -- temporary total rebuilding
  perform  {databaseSchema}.{objectQualifier}forum_ns_recreate();
  ELSE
   -- tree rebuilding is being made by {databaseSchema}_{objectQualifier}forum_before_insert_tr, drop constraint first
  -- alter table only {databaseSchema}.{objectQualifier}forum drop constraint fk_{databaseSchema}_{objectQualifier}_forum_{objectQualifier}_forum_parentid;
  
  if (i_adjacentforumid is not null)  then  
	if (i_adjacentforummode = 1) then 
	select left_key into newlk from {databaseSchema}.{objectQualifier}forum where forumid = i_adjacentforumid;
	end if;
	-- after 
	if (i_adjacentforummode = 2) then 
	select right_key+1 into newlk from {databaseSchema}.{objectQualifier}forum where forumid = i_adjacentforumid;	
	end if;
  end if;
  INSERT INTO {databaseSchema}.{objectQualifier}forum(categoryid,parentid,name,description,sortorder,
  numtopics,numposts,remoteurl,themeurl,flags,imageurl,styles,isuserforum,createdbyuserid,createdbyusername,createdbyuserdisplayname,createddate,canhavepersforums, left_key)
  VALUES(i_categoryid,i_parentid,i_name,i_description,
  i_sortorder,0,0,i_remoteurl,i_themeurl,ici_flags,i_imageurl,i_styles,i_isuserforum,i_userid, ici_UserName, ici_userdisplayname,i_utctimestamp,i_canhavepersforums, newlk) returning forumid INTO ici_ForumID; 
--   ALTER TABLE ONLY {databaseSchema}.{objectQualifier}forum
 --  ADD CONSTRAINT fk_{databaseSchema}_{objectQualifier}_forum_{objectQualifier}_forum_parentid FOREIGN KEY (parentid) REFERENCES {databaseSchema}.{objectQualifier}forum(forumid);
  
  INSERT INTO {databaseSchema}.{objectQualifier}forumaccess(groupid,forumid,accessmaskid)
  SELECT groupid,ici_ForumID,i_accessmaskid
  FROM {databaseSchema}.{objectQualifier}group
  WHERE boardid=ici_boardid;
  END IF;
  -- NS
  -- move node to it's place by recreteating table. We make it only when the forum was already saved with access mask 
 -- if (i_accessmaskid > 0) then

 -- perform  {databaseSchema}.{objectQualifier}forum_ns_recreate();
 -- end if;   
   

	if exists (select 1 from {databaseSchema}.{objectQualifier}forumhistory where forumid = ici_ForumID and changeddate = i_utctimestamp LIMIT 1) THEN
	update {databaseSchema}.{objectQualifier}forumhistory set 
		   ChangedUserID = i_UserID,	
		   ChangedUserName = ici_UserName,
		   changeddisplayname = ici_userdisplayname
	 where forumid = ici_ForumID and changeddate = i_utctimestamp; 
	else    
	INSERT INTO {databaseSchema}.{objectQualifier}forumhistory(forumid,ChangedUserID,ChangedUserName,changeddisplayname,ChangedDate)
	VALUES (ici_ForumID,i_UserID,ici_UserName,ici_userdisplayname,i_utctimestamp);
	end IF;
  
RETURN ici_ForumID;
  END;
$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100;
--GO
-- Function: {databaseSchema}.{objectQualifier}forum_subforums(integer, integer)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}forum_subforums(integer, integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}forum_subforums(
						   i_forumid integer, 
						   i_userid integer)
				  RETURNS integer AS
$BODY$DECLARE 
ici_NumSubforums integer:=0;
begin	

	select 
		COUNT(1) INTO ici_NumSubforums	
	from 
		{databaseSchema}.{objectQualifier}forum a 
		/* join {databaseSchema}.{objectQualifier}vaccess x on x.forumid = a.forumid */
	where 
		((SELECT "ReadAccess" FROM {databaseSchema}.{objectQualifier}vaccess_combo(i_userid, a.forumid) LIMIT 1) > 0 OR (a.flags & 2) = 0) AND 
		/*((a.flags & 2)=0 or x.readaccess<>0) and  */
		(a.parentid=i_forumid) /*and	
		(x.userid = i_userid)*/ GROUP BY a.forumid;

	return COALESCE(ici_NumSubforums,0);
end;$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100;
--GO

-- Function: {databaseSchema}.{objectQualifier}forum_updatelastpost(integer)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}forum_updatelastpost(integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}forum_updatelastpost(
						  i_forumid integer)
				  RETURNS void AS
$BODY$DECLARE 
ici_ParentID integer;
ici_tmpParent integer;
ici_tmpMaxPosted3 timestamp ;

ici_LastPostedTmp timestamp ;
ici_LastTopicIDTmp integer;
ici_LastMessageIDTmp integer;
ici_LastUserIDTmp integer;
ici_LastUserNameTmp varchar(128);
ici_LastUserDisplayNameTmp varchar(128);

ici_MaxTPosted timestamp ;
 
ici_lastposted timestamp ;
ici_LastTopicID integer;
ici_LastMessageID integer;
ici_LastUserID integer;
ici_LastUserName varchar(128);
ici_LastUserDisplayName varchar(128);


BEGIN
SELECT z.ParentID
INTO ici_ParentID
FROM {databaseSchema}.{objectQualifier}Forum z
WHERE z.forumid = i_forumid;

SELECT DISTINCT y.Posted,y.TopicID,y.MessageID,y.UserID,y.UserName, y.UserDisplayName
INTO ici_lastposted,ici_LastTopicID,ici_LastMessageID,ici_LastUserID,ici_LastUserName,ici_LastUserDisplayName
FROM
{databaseSchema}.{objectQualifier}Forum z
JOIN {databaseSchema}.{objectQualifier}Topic x ON z.forumid=x.forumid 
JOIN {databaseSchema}.{objectQualifier}Message y ON y.TopicID=x.TopicID
WHERE x.forumid = i_forumid
AND (y.Flags & 24)=16
AND x.IsDeleted IS FALSE
ORDER BY y.Posted DESC LIMIT 1;



SELECT DISTINCT y.posted,y.topicid,y.messageid, y.userid,y.username,y.userdisplayname,f.parentid
INTO ici_lastposted,ici_LastTopicID,ici_LastMessageID,
ici_LastUserID ,ici_LastUserName,ici_LastUserDisplayName,ici_ParentID
FROM  {databaseSchema}.{objectQualifier}forum f 
JOIN {databaseSchema}.{objectQualifier}topic x 
   ON x.forumid =f.forumid
	 JOIN {databaseSchema}.{objectQualifier}message y 
		ON y.topicid=x.topicid 
			WHERE x.forumid = i_forumid 
			   AND (y.flags & 24)=16 
				  AND x.isdeleted IS FALSE 
					ORDER BY y.posted 
						DESC LIMIT 1;
						
	UPDATE {databaseSchema}.{objectQualifier}forum
  SET
  lastposted = ici_lastposted,
 lasttopicid = ici_LastTopicID,                        
 lastmessageid = ici_LastMessageID,
 lastuserid = ici_LastUserID,
lastusername = ici_LastUserName,
	lastuserdisplayname = ici_LastUserDisplayName             
	WHERE forumid = i_forumid;
						 
		 -- these values will be compared with last post for use in updated forum parent
 SELECT DISTINCT f.lastposted,
f.lasttopicid,
f.lastmessageid,
f.lastuserid,
f.lastusername,
f.lastuserdisplayname
INTO 
ici_LastPostedTmp,ici_LastTopicIDTmp,ici_LastMessageIDTmp,ici_LastUserIDTmp,ici_LastUserNameTmp,ici_LastUserDisplayNameTmp
from {databaseSchema}.{objectQualifier}forum f
WHERE f.parentid =i_forumid ORDER BY f.lastposted DESC LIMIT 1;

IF (ici_LastPostedTmp IS NOT NULL AND ici_lastposted IS NOT NULL AND
ici_LastPostedTmp > ici_lastposted) OR  (ici_LastPostedTmp IS NOT NULL AND ici_lastposted IS NULL) THEN

ici_lastposted :=ici_LastPostedTmp;
ici_LastTopicID :=ici_LastTopicIDTmp;
ici_LastMessageID :=ici_LastMessageIDTmp;
ici_LastUserID :=ici_LastUserIDTmp;
ici_LastUserName :=ici_LastUserNameTmp;
ici_LastUserDisplayName :=ici_LastUserDisplayNameTmp;
END IF;                    
  UPDATE {databaseSchema}.{objectQualifier}forum
  SET
  lastposted = ici_lastposted,
 lasttopicid = ici_LastTopicID,                        
 lastmessageid = ici_LastMessageID,
 lastuserid = ici_LastUserID,
lastusername = ici_LastUserName,
		lastuserdisplayname = ici_LastUserDisplayName                
	WHERE forumid = i_forumid;
	
 
 WHILE ici_ParentID > 0 LOOP
	
	SELECT DISTINCT f.lastposted,
f.lasttopicid,
f.lastmessageid,
f.lastuserid,
f.lastusername,
f.lastuserdisplayname
INTO 
ici_lastposted,ici_LastTopicID,ici_LastMessageID,
ici_LastUserID,ici_LastUserName,ici_LastUserDisplayName
FROM {databaseSchema}.{objectQualifier}forum f
WHERE f.parentid =ici_ParentID 
ORDER BY f.lastposted DESC LIMIT 1;

 UPDATE {databaseSchema}.{objectQualifier}forum
  SET
	 lastposted = ici_lastposted,
 lasttopicid = ici_LastTopicID,                        
 lastmessageid = ici_LastMessageID,
 lastuserid = ici_LastUserID,
lastusername = ici_LastUserName,
		  lastuserdisplayname = ici_LastUserDisplayName               
	WHERE forumid = ici_ParentID;
	
 SELECT DISTINCT f.parentid
INTO ici_ParentID
FROM  {databaseSchema}.{objectQualifier}forum f 
WHERE f.forumid = ici_ParentID;		
	END LOOP; 
	-- it looks only in children add siblings for each next parent forum IF nulls */
 SELECT z.ParentID
INTO ici_ParentID
FROM {databaseSchema}.{objectQualifier}Forum z
WHERE z.forumid = i_forumid;

/*SELECT DISTINCT LastPosted,
TopicID,
LastMessageID,
LastUserID,
LastUserName
INTO ici_lastposted,ici_LastTopicID,ici_LastMessageID,ici_LastUserID,ici_LastUserName
from {databaseSchema}.{objectQualifier}Topic
WHERE forumid =i_ForumID ORDER BY LastPosted DESC LIMIT 1;

IF ici_LastTopicID IS NULL THEN*/
SELECT y.Posted,y.TopicID,y.MessageID,y.UserID,y.UserName,y.UserDisplayName
INTO ici_lastposted,ici_LastTopicID,ici_LastMessageID,ici_LastUserID,ici_LastUserName,ici_LastUserDisplayName
FROM
{databaseSchema}.{objectQualifier}Forum z
JOIN {databaseSchema}.{objectQualifier}Topic x ON z.forumid=x.forumid 
JOIN {databaseSchema}.{objectQualifier}Message y ON y.TopicID=x.TopicID
WHERE x.forumid = i_forumid
AND (y.Flags & 24)=16
AND x.IsDeleted IS NOT TRUE
ORDER BY y.Posted DESC LIMIT 1;
/*END IF; Look for it in children*/

SELECT LastPosted,
LastTopicID,
LastMessageID,
LastUserID,
LastUserName,
LastUserDisplayName
INTO 
ici_LastPostedTmp,ici_LastTopicIDTmp,ici_LastMessageIDTmp,
ici_LastUserIDTmp,ici_LastUserNameTmp,ici_LastUserDisplayNameTmp
FROM {databaseSchema}.{objectQualifier}Forum
WHERE parentid =i_forumid ORDER BY lastposted DESC LIMIT 1;
-- END IF;

IF ici_LastPostedTmp IS NOT NULL AND ici_lastposted IS NOT NULL THEN
IF (ici_LastPostedTmp > ici_lastposted) THEN

ici_lastposted :=ici_LastPostedTmp;
ici_LastTopicID :=ici_LastTopicIDTmp;
ici_LastMessageID :=ici_LastMessageIDTmp;
ici_LastUserID :=ici_LastUserIDTmp;
ici_LastUserName :=ici_LastUserNameTmp;
ici_LastUserDisplayName :=ici_LastUserDisplayNameTmp;
END IF;
END IF;

IF ici_LastPostedTmp IS NOT NULL AND ici_lastposted IS NULL THEN

ici_lastposted :=ici_LastPostedTmp;
ici_LastTopicID :=ici_LastTopicIDTmp;
ici_LastMessageID :=ici_LastMessageIDTmp;
ici_LastUserID :=ici_LastUserIDTmp;
ici_LastUserName :=ici_LastUserNameTmp;
ici_LastUserDisplayName :=ici_LastUserDisplayNameTmp;
END IF;


IF (ici_LastTopicID IS NOT NULL AND ici_LastPostedTmp IS NOT NULL
 AND (ici_LastPostedTmp <= ici_lastposted)) OR (ici_LastTopicID IS NOT NULL AND
ici_LastPostedTmp IS NULL) THEN

UPDATE {databaseSchema}.{objectQualifier}Forum
   SET
		LastPosted = ici_lastposted,
				LastTopicID = ici_LastTopicID,
				LastMessageID = ici_LastMessageID,
				LastUserID = ici_LastUserID,
				LastUserName = ici_LastUserName,
				LastUserDisplayName = ici_LastUserDisplayName
 WHERE forumid = i_forumid;

END IF;

 PERFORM {databaseSchema}.{objectQualifier}forum_updatestats(i_forumid);



--   max value  in the current forum we compare with its peers to use in parent

IF ici_ParentID >0  THEN

	 -- CALL {databaseSchema}.{objectQualifier}forum_updatestats(i_ForumID);

-- In peers to use in parent
ici_tmpMaxPosted3 :=
(SELECT lastposted
FROM {databaseSchema}.{objectQualifier}Forum
WHERE ParentID = ici_ParentID
AND forumid != i_forumid AND lastposted IS NOT NULL ORDER BY lastposted DESC LIMIT 1);


IF ici_tmpMaxPosted3 IS NOT NULL AND ici_lastposted IS NULL THEN
ici_MaxTPosted := ici_tmpMaxPosted3; END IF;

IF
(ici_tmpMaxPosted3 IS NULL AND ici_lastposted IS NOT NULL)
OR ((ici_tmpMaxPosted3 IS NOT NULL AND ici_lastposted IS NOT NULL)
AND (ici_tmpMaxPosted3 <= ici_lastposted)) THEN
ici_MaxTPosted := ici_lastposted; END IF;

IF ici_tmpMaxPosted3 IS NOT NULL
AND ici_lastposted IS NOT NULL
AND ici_tmpMaxPosted3 > ici_lastposted THEN
ici_MaxTPosted := ici_tmpMaxPosted3; END IF;

ici_tmpMaxPosted3 := NULL;

/* In parent themes
SELECT DISTINCT LastPosted
INTO ici_tmpMaxPosted3
FROM {databaseSchema}.{objectQualifier}Topic
WHERE forumid=ici_ParentID ORDER BY LastPosted LIMIT 1;


IF ici_tmpMaxPosted3 IS NOT NULL AND ici_MaxTPosted IS NULL THEN
SET ici_MaxTPosted = ici_tmpMaxPosted3;
END IF;

IF ici_tmpMaxPosted3 IS NOT NULL
AND ici_lastposted IS NOT NULL
AND UNIX_TIMESTAMP(ici_tmpMaxPosted3) > UNIX_TIMESTAMP(ici_MaxTPosted) THEN
SET ici_MaxTPosted = ici_tmpMaxPosted3;
END IF; */

IF ici_MaxTPosted IS NOT NULL THEN
SELECT DISTINCT LastPosted,
LastTopicID,
LastMessageID,
LastUserID,
LastUserName,
LastUserDisplayName
INTO ici_lastposted,ici_LastTopicID,ici_LastMessageID,ici_LastUserID,ici_LastUserName,ici_LastUserDisplayName
FROM {databaseSchema}.{objectQualifier}Forum
WHERE LastPosted =ici_MaxTPosted ORDER BY LastPosted DESC LIMIT 1;



	  UPDATE {databaseSchema}.{objectQualifier}Forum
		  SET
		LastPosted = ici_lastposted,
				LastTopicID = ici_LastTopicID,
				LastMessageID = ici_LastMessageID,
				LastUserID = ici_LastUserID,
				LastUserName = ici_LastUserName,
				LastUserDisplayName = ici_LastUserDisplayName                      
	  WHERE forumid = ici_ParentID;
END IF;
PERFORM {databaseSchema}.{objectQualifier}forum_updatestats(ici_ParentID);



SELECT DISTINCT parentid INTO  ici_tmpParent 
  FROM  {databaseSchema}.{objectQualifier}forum
  WHERE forumid = ici_ParentID;


 -- Here we set new values in parents

WHILE ici_tmpParent  > 0 
LOOP
IF ici_tmpParent > 0 THEN
ici_MaxTPosted :=
(SELECT lastposted
FROM {databaseSchema}.{objectQualifier}forum
WHERE parentid = ici_tmpParent
AND lastposted IS NOT NULL
ORDER BY lastposted DESC LIMIT 1);
IF ici_MaxTPosted IS NOT NULL THEN
SELECT lastposted,
lasttopicid,
lastmessageid,
lastuserid,
lastusername,
lastuserdisplayname
INTO ici_lastposted,
ici_LastTopicID,
ici_LastMessageID,
ici_LastUserID,
ici_LastUserName,
ici_LastUserDisplayName
FROM {databaseSchema}.{objectQualifier}forum
WHERE LastPosted =ici_MaxTPosted ORDER BY LastPosted DESC LIMIT 1;
END IF;

		UPDATE {databaseSchema}.{objectQualifier}forum SET
				lastposted = ici_lastposted,
				lasttopicid = ici_LastTopicID,
				lastmessageid = ici_LastMessageID,
				lastuserid = ici_LastUserID,
				lastusername = ici_LastUserName,
				lastuserdisplayname = ici_LastUserDisplayName
			WHERE
				forumid = ici_tmpParent
		AND ((lastposted <= ici_lastposted)
		OR lastposted IS NULL);
		PERFORM {databaseSchema}.{objectQualifier}forum_updatestats(ici_tmpParent);
 END IF;
		 SELECT DISTINCT parentid INTO  ici_tmpParent
  FROM  {databaseSchema}.{objectQualifier}Forum
  WHERE forumid = ici_tmpParent;

  END LOOP;

  END IF;		
END;
$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100; 
--GO

-- Function: {databaseSchema}.{objectQualifier}forum_updatestats(integer)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}forum_updatestats(integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}forum_updatestats(
						   i_forumid integer)
				  RETURNS void AS
$BODY$
BEGIN
		UPDATE {databaseSchema}.{objectQualifier}forum 
		   SET 
		numposts = (SELECT COUNT(1) FROM {databaseSchema}.{objectQualifier}message x 
							   JOIN {databaseSchema}.{objectQualifier}topic y 
								  ON y.topicid=x.topicid 
								   WHERE y.forumid = i_forumid 
									 AND x.isapproved IS TRUE
									   AND x.isdeleted IS NOT TRUE 
										 AND y.isdeleted IS NOT TRUE ),
		numtopics = (SELECT COUNT(distinct x.topicid) FROM {databaseSchema}.{objectQualifier}topic x 
							   JOIN {databaseSchema}.{objectQualifier}message y 
								  ON y.topicid=x.topicid 
								   WHERE x.forumid = i_forumid 
									 AND y.isapproved IS TRUE 
										 AND y.isdeleted IS NOT TRUE
										   AND x.isdeleted IS NOT TRUE)
	WHERE forumid=i_forumid;
	RETURN;
END;$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100;  
--GO

-- Function: {databaseSchema}.{objectQualifier}forum_simplelist(integer, integer)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}forum_simplelist(integer, integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}forum_simplelist(
						   i_startid integer, 
						   i_limit integer)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}forum_simplelist_return_type AS
$BODY$DECLARE
 _rec {databaseSchema}.{objectQualifier}forum_simplelist_return_type%ROWTYPE;
BEGIN   
	 
  FOR _rec IN          
   SELECT   f.forumid,
				  f.name,
				  f.lastposted
		 FROM     {databaseSchema}.{objectQualifier}forum f
		 WHERE    f.forumid >= i_startid  
		 and     f.forumid < i_startid + i_limit
		 ORDER BY f.forumid	
 LOOP
 RETURN NEXT _rec;
END LOOP;

END;$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100 ROWS 1000; 
--GO

-- Function: {databaseSchema}.{objectQualifier}forumaccess_group(integer)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}forumaccess_group(integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}forumaccess_group(
						   i_groupid integer, i_userid integer, i_includeuserforums boolean)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}forumaccess_group_return_type AS
$BODY$DECLARE
			 _rec {databaseSchema}.{objectQualifier}forumaccess_group_return_type%ROWTYPE;
BEGIN
FOR _rec IN
	SELECT 
		  a.groupid,
		  a.forumid,
		  a.accessmaskid,
		  b.name AS ForumName,
		  c.name AS CategoryName,
		  b.categoryid AS CategoryID,
		  b.parentid AS ParentID,               
		  brd.Name AS BoardName 
	FROM 
		{databaseSchema}.{objectQualifier}forumaccess a
		INNER JOIN {databaseSchema}.{objectQualifier}forum b on b.forumid=a.forumid
		INNER JOIN {databaseSchema}.{objectQualifier}category c on c.categoryid=b.categoryid
			INNER JOIN {databaseSchema}.{objectQualifier}board brd on brd.boardid=c.boardid
	
WHERE 
		a.groupid = i_groupid and (not i_includeuserforums  or (b.isuserforum and b.CreatedByUserID = i_userid))
	ORDER BY 
		brd.Name,
		c.sortorder,
		b.sortorder
LOOP
	RETURN NEXT _rec;
END LOOP;
		
 END;
$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100 ROWS 1000;  
--GO

-- Function: {databaseSchema}.{objectQualifier}forumaccess_list(integer)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}forumaccess_list(integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}forumaccess_list(
				  i_forumid integer, i_personalgroupuserid integer, i_includeusergroups boolean, i_includecommongroups boolean, i_includeadmingroups boolean)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}forumaccess_list_return_type AS
$BODY$DECLARE
			 _rec {databaseSchema}.{objectQualifier}forumaccess_list_return_type%ROWTYPE;
BEGIN
FOR _rec IN
		SELECT 
			   a.groupid,
			   a.forumid,
			   a.accessmaskid,
			   b.name AS GroupName,
			   b.isusergroup,
			   b.isadmingroup			   
		FROM   {databaseSchema}.{objectQualifier}forumaccess a
			   INNER JOIN {databaseSchema}.{objectQualifier}group b ON b.groupid=a.groupid
		WHERE  a.forumid = i_forumid   
	   AND 
		b.isusergroup = (case when i_includeusergroups and not i_includecommongroups AND b.CreatedByUserID = i_personalgroupuserid then true
		else false end)
		AND 
		((i_includeadmingroups AND i_includecommongroups) OR b.ishidden = i_includeadmingroups) 
	   LOOP
	RETURN NEXT _rec;
	END LOOP;   
END;
$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100 ROWS 1000;
--GO

-- Function: {databaseSchema}.{objectQualifier}forumaccess_save(integer, integer, integer)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}forumaccess_save(integer, integer, integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}forumaccess_save(i_forumid integer, i_groupid integer, i_accessmaskid integer)
  RETURNS void AS
'UPDATE {databaseSchema}.{objectQualifier}forumaccess
		SET    accessmaskid = $3
		WHERE  forumid = $1
		AND groupid = $2;'     
  LANGUAGE 'sql' VOLATILE SECURITY DEFINER
  COST 100; 
--GO

-- Function: {databaseSchema}.{objectQualifier}group_delete(integer)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}group_delete(integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}group_delete(
						   i_groupid integer)
				  RETURNS void AS
$BODY$
BEGIN
		DELETE FROM {databaseSchema}.{objectQualifier}eventloggroupaccess
		WHERE       groupid = i_groupid;
		DELETE FROM {databaseSchema}.{objectQualifier}forumaccess
		WHERE       groupid = i_groupid;
		DELETE FROM {databaseSchema}.{objectQualifier}usergroup
		WHERE       groupid = i_groupid;
		DELETE FROM {databaseSchema}.{objectQualifier}group
		WHERE       groupid = i_groupid;
		END;
$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100; 
--GO

-- Function: {databaseSchema}.{objectQualifier}group_list(integer, integer)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}group_list(integer, integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}group_list(
						   i_boardid integer, 
						   i_groupid integer,
						   i_pageindex integer,
						   i_pagesize integer)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}group_list_return_type AS
$BODY$DECLARE
			 _rec {databaseSchema}.{objectQualifier}group_list_return_type%ROWTYPE;
			 ici_totalrows int = 0;
			 ici_firstselectrownumber int = 0;
BEGIN

		IF i_groupid IS NULL THEN
		i_pageindex := i_pageindex + 1;
		 
		select count(1) into ici_totalrows 
		FROM  {databaseSchema}.{objectQualifier}group
		WHERE boardid = i_boardid;
			
		ici_firstselectrownumber := (i_pageindex - 1) * i_pagesize;     

  FOR _rec in
	   SELECT 
			 groupid,
			 boardid,
			 name,
			 flags,
			 pmlimit,
			 style,
			 sortorder,
			 description,
			 usrsigchars,
			 usrsigbbcodes,
			 usrsightmltags,
			 usralbums,
			 usralbumimages,
			 isadmingroup,
			 isusergroup,            
			 usrpersonalmasks,
			 usrpersonalgroups,
			 usrpersonalforums,
			 createdbyuserid,
			 createdbyusername,
			 createdbyuserdisplayname, 
			 ici_totalrows as TotalRows
		FROM   {databaseSchema}.{objectQualifier}group
		WHERE boardid = i_boardid order by sortorder OFFSET ici_firstselectrownumber LIMIT i_pagesize
  LOOP
	  RETURN NEXT _rec;
  END LOOP;    
		ELSE
		FOR _rec IN
		SELECT 
			 groupid,
			 boardid,
			 name,
			 flags,
			 pmlimit,
			 style,
			 sortorder,
			 description,
			 usrsigchars,
			 usrsigbbcodes,
			 usrsightmltags,
			 usralbums,
			 usralbumimages,
			 isadmingroup,
			 isusergroup,            
			 usrpersonalmasks,
			 usrpersonalgroups,
			 usrpersonalforums,
			 createdbyuserid,
			 createdbyusername,
			 createdbyuserdisplayname, 
			 ici_totalrows as TotalRows
		FROM   {databaseSchema}.{objectQualifier}group
		WHERE  boardid = i_boardid
		AND groupid = i_groupid LIMIT 1
		 LOOP
	RETURN NEXT _rec;
END LOOP;
		END IF;

 END;
$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100 ROWS 1000;    
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}group_byuserlist(
						   i_boardid integer, 
						   i_groupid integer,
						   i_userid integer,
						   i_isusergroup boolean)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}group_list_return_type AS
$BODY$DECLARE
			 _rec {databaseSchema}.{objectQualifier}group_list_return_type%ROWTYPE;
BEGIN

		IF i_groupid IS NULL THEN
		FOR _rec IN
		SELECT 
			  groupid,
			 boardid,
			 name,
			 flags,
			 pmlimit,
			 style,
			 sortorder,
			 description,
			 usrsigchars,
			 usrsigbbcodes,
			 usrsightmltags,
			 usralbums,
			 usralbumimages,
			 isadmingroup,
			 isusergroup,            
			 usrpersonalmasks,
			 usrpersonalgroups,
			 usrpersonalforums,
			 createdbyuserid,
			 createdbyusername,
			 createdbyuserdisplayname
		FROM   {databaseSchema}.{objectQualifier}group
		WHERE  boardid = i_boardid and createdbyuserid = i_userid
		 LOOP
	RETURN NEXT _rec;
END LOOP;
		ELSE
		FOR _rec IN
		SELECT 
			  groupid,
			 boardid,
			 name,
			 flags,
			 pmlimit,
			 style,
			 sortorder,
			 description,
			 usrsigchars,
			 usrsigbbcodes,
			 usrsightmltags,
			 usralbums,
			 usralbumimages,
			 isadmingroup,
			 isusergroup,            
			 usrpersonalmasks,
			 usrpersonalgroups,
			 usrpersonalforums,
			 createdbyuserid,
			 createdbyusername,
			 createdbyuserdisplayname
		FROM   {databaseSchema}.{objectQualifier}group
		WHERE  boardid = i_boardid and createdbyuserid = i_userid
		AND groupid = i_groupid LIMIT 1
		 LOOP
	RETURN NEXT _rec;
END LOOP;
		END IF;

 END;
$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100 ROWS 1000;    
--GO

-- Function: {databaseSchema}.{objectQualifier}group_medal_delete(integer, integer)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}group_medal_delete(integer, integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}group_medal_delete(
						   i_groupid integer, 
						   i_medalid integer)
				  RETURNS void AS
'DELETE FROM {databaseSchema}.{objectQualifier}groupmedal 
	WHERE groupid=$1 AND medalid=$2;'
  LANGUAGE 'sql' VOLATILE SECURITY DEFINER
  COST 100;   
--GO

-- Function: {databaseSchema}.{objectQualifier}group_medal_list(integer, integer)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}group_medal_list(integer, integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}group_medal_list(
						   i_groupid integer, 
						   i_medalid integer)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}group_medal_list_return_type AS
$BODY$DECLARE
			 _rec {databaseSchema}.{objectQualifier}group_medal_list_return_type%ROWTYPE;
BEGIN
 FOR _rec IN
	SELECT 
		a.medalid,
				a.boardid,
		a.name,
		a.medalurl,
		a.ribbonurl,
		a.smallmedalurl,
		a.smallribbonurl,
		a.smallmedalwidth,
		a.smallmedalheight,
		a.smallribbonwidth,
		a.smallribbonheight,
		b.sortorder,
		a.flags,
		c.name AS GroupName,
		b.groupid,
		COALESCE(b.message,a.message) AS Message,
		b.message AS MessageEx,
		b.hide,
		b.onlyribbon,
		b.sortorder AS CurrentSortOrder
	FROM
		{databaseSchema}.{objectQualifier}medal a
		INNER JOIN {databaseSchema}.{objectQualifier}groupmedal b 
		ON b.medalid = a.medalid
		INNER JOIN {databaseSchema}.{objectQualifier}group c 
		ON  c.groupid = b.groupid
	WHERE
		(i_groupid IS NULL OR b.groupid = i_groupid) AND
		(i_medalid IS NULL OR b.medalid = i_medalid)		
	ORDER BY
		c.name ASC,
		b.sortorder ASC

		 LOOP
	RETURN NEXT _rec;
END LOOP; 		
 
END;$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100 ROWS 1000;
--GO

-- Function: {databaseSchema}.{objectQualifier}group_medal_save(integer, integer, varchar, boolean, boolean, smallint)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}group_medal_save(integer, integer, varchar, boolean, boolean, smallint);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}group_medal_save(
						   i_groupid integer, 
						   i_medalid integer, 
						   i_message varchar, 
						   i_hide boolean, 
						   i_onlyribbon boolean, 
						   i_sortorder integer)
				  RETURNS void AS
$BODY$
DECLARE
	   ici_sortorder smallint:=i_sortorder;
BEGIN
	 IF ici_sortorder > 0 THEN 
	IF EXISTS(SELECT 1 FROM {databaseSchema}.{objectQualifier}groupmedal 
	WHERE sortorder = ici_sortorder) THEN
	UPDATE {databaseSchema}.{objectQualifier}groupmedal
			SET sortorder = sortorder+1				
		WHERE sortorder BETWEEN ici_sortorder AND 254;
	END IF;
	ELSE
	 SELECT MAX(sortorder) INTO ici_sortorder FROM {databaseSchema}.{objectQualifier}groupmedal;
	IF ici_sortorder IS NULL 
	THEN ici_sortorder:=1; 
	ELSE
	ici_sortorder :=ici_sortorder+1;
	END IF;
	END IF;
	 
	IF EXISTS (SELECT 1 FROM {databaseSchema}.{objectQualifier}groupmedal 
	WHERE groupid=i_groupid AND medalid=i_medalid) THEN
		UPDATE {databaseSchema}.{objectQualifier}groupmedal
		SET
			message = i_message,
			hide = i_hide,
			onlyribbon = i_onlyribbon,
			sortorder = ici_sortorder
		WHERE
			groupid=i_groupid and 
			medalid=i_medalid;
	
	ELSE
 
		INSERT INTO {databaseSchema}.{objectQualifier}groupmedal
			(groupid,medalid,message,hide,onlyribbon,sortorder)
		VALUES
			(i_groupid,i_medalid,i_message,i_hide,i_onlyribbon,ici_sortorder);
	END IF;
 
END;$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100;  
--GO

-- Function: {databaseSchema}.{objectQualifier}group_member(integer, integer)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}group_member(integer, integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}group_member(
						   i_boardid integer, 
						   i_userid integer)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}group_member_return_type AS
$BODY$DECLARE
			 _rec {databaseSchema}.{objectQualifier}group_member_return_type%ROWTYPE;
BEGIN
FOR _rec IN
		SELECT   a.groupid,
		a.name,
		(a.flags & 16 = 16),
		a.isusergroup,
		a.style,
		(SELECT COUNT(1)
		FROM   {databaseSchema}.{objectQualifier}usergroup x
		WHERE  x.userid = i_userid
		AND x.groupid = a.groupid) AS Member
		FROM     {databaseSchema}.{objectQualifier}group a
		WHERE    a.boardid = i_boardid
		ORDER BY a.name
	   LOOP
RETURN NEXT _rec;
END LOOP;
		END;

$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100 ROWS 1000; 
--GO

-- Function: {databaseSchema}.{objectQualifier}group_save(integer, integer, varchar, boolean, boolean, boolean, boolean, integer)

DROP FUNCTION IF EXISTS {databaseSchema}.{objectQualifier}group_save(integer, integer, varchar, boolean, boolean, boolean, boolean, integer);
--GO
CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}group_save(
						   i_groupid integer,
						   i_boardid integer,
						   i_name varchar,
						   i_isadmin boolean,
						   i_isguest boolean,
						   i_isstart boolean,
						   i_ismoderator boolean,
						   i_ishidden boolean,
						   i_accessmaskid integer,
						   i_pmlimit integer,
						   i_style varchar(255),
						   i_sortorder integer,
						   i_description varchar(128),
						   i_usrsigchars integer,
						   i_usrsigbbcodes	varchar(255),
						   i_usrsightmltags varchar(255),
						   i_usralbums integer,
						   i_usralbumimages integer,
						   i_userid integer,
						   i_isusergroup boolean,
						   i_personalaccessmasksnumber integer,
						   i_personalgroupsnumber integer,
						   i_personalforumsnumber integer,
						   i_utctimestamp timestamp)
				  RETURNS int AS
$BODY$DECLARE
			 ici_UserName  VARCHAR(255);
			 ici_userdisplayname  VARCHAR(255);
			 ici_groupid integer:=i_groupid;
			 iciFlags integer:=0;
BEGIN         
		
		IF i_isadmin IS NOT FALSE THEN
		iciFlags := iciFlags | 1 ; END IF;
		IF i_isguest IS NOT FALSE THEN
		iciFlags := iciFlags | 2; END IF;
		IF i_isstart IS NOT FALSE THEN
		iciFlags := iciFlags | 4; END IF;
		IF i_ismoderator IS NOT FALSE THEN
		iciFlags := iciFlags | 8; END IF;
		 IF i_ishidden IS NOT FALSE THEN
		iciFlags := iciFlags | 16; END IF;
		IF char_length(i_style) <= 2 THEN  
	i_style := null; 
	END IF;
		
		SELECT name,displayname into ici_UserName,ici_userdisplayname from {databaseSchema}.{objectQualifier}user where userid = i_userid; 
				  IF i_UserID IS NOT NULL THEN   
	SELECT name,displayname INTO ici_UserName, ici_userdisplayname FROM {databaseSchema}.{objectQualifier}user where userid = i_userid LIMIT  1;
	   -- guests should not create forums
	ELSE    
	SELECT name,displayname INTO ici_UserName, ici_userdisplayname FROM {databaseSchema}.{objectQualifier}user where BoardID = i_BoardID and (Flags & 4) = 4  ORDER BY Joined LIMIT 1;
	END IF;
		IF i_groupid > 0 THEN        
			UPDATE {databaseSchema}.{objectQualifier}group
			SET    name = i_name,
				   flags = iciFlags,
				   pmlimit = i_pmlimit,
				   style =  i_style,
				   sortorder = i_sortorder,
				   description = i_description,
				   usrsigchars = i_usrsigchars,
				   usrsigbbcodes = i_usrsigbbcodes,
				   usrsightmltags = i_usrsightmltags,
				   usralbums = i_usralbums,
				   usralbumimages = i_usralbumimages,
				   isusergroup = i_isusergroup,
				   usrpersonalmasks = i_personalaccessmasksnumber,
				   usrpersonalgroups = i_personalgroupsnumber,
				   usrpersonalforums = i_personalforumsnumber
			WHERE  groupid = i_groupid;
		   -- SELECT i_groupid INTO ici_groupid;        
		ELSE        
			INSERT INTO {databaseSchema}.{objectQualifier}group
					   (name,
						boardid,
						flags,
						pmlimit,
						style,
						sortorder,
						description,
						usrsigchars,
						usrsigbbcodes,
						usrsightmltags,
						usralbums,
						usralbumimages,
						isusergroup,
						createdbyuserid,
						createdbyusername,
						createdbyuserdisplayname,
						createddate,
						usrpersonalmasks,
						usrpersonalgroups,
						usrpersonalforums)
			VALUES     (i_name,
						i_boardid,
						iciFlags,
						i_pmlimit,
						i_style,
						i_sortorder,
						i_description,
						i_usrsigchars,
						i_usrsigbbcodes,
						i_usrsightmltags,
						i_usralbums,
						i_usralbumimages,
						i_isusergroup,
						i_userid,
						ici_UserName,
						ici_userdisplayname,
						i_utctimestamp,
						i_personalaccessmasksnumber,
						i_personalgroupsnumber,
						i_personalforumsnumber)
						RETURNING groupid INTO ici_groupid;            
			
			INSERT INTO {databaseSchema}.{objectQualifier}forumaccess
					   (groupid,
						forumid,
						accessmaskid)
			SELECT ici_groupid,
				   a.forumid,
				   i_accessmaskid
			FROM   {databaseSchema}.{objectQualifier}forum a
				   JOIN {databaseSchema}.{objectQualifier}category b
					 ON b.categoryid = a.categoryid
			WHERE  b.boardid = i_boardid;
		--    SELECT ici_groupid INTO _rec; 
		 END IF;

	/* if exists (select 1 from {databaseSchema}.{objectQualifier}grouphistory where groupid = ici_groupid and changeddate = i_utctimestamp  LIMIT 1) THEN
	update {databaseSchema}.{objectQualifier}grouphistory set 
		   ChangedUserID = i_UserID,	
		   ChangedUserName = ici_UserName,
		   ChangedDisplayName = ici_userdisplayname
	 where  groupid = ici_groupid  and changeddate = i_utctimestamp; 
	else    
	INSERT INTO {databaseSchema}.{objectQualifier}grouphistory(GroupID,ChangedUserID,ChangedUserName,ChangedDisplayName,ChangedDate)
	VALUES (i_GroupID, i_UserID,ici_UserName,ici_userdisplayname,i_UTCTIMESTAMP);
	end IF; */
	-- group styles override user styles, don't sync if style is not present here
   IF (i_style is not null and char_length(i_style) > 2) THEN
		   PERFORM {databaseSchema}.{objectQualifier}user_savestyle(
						ici_groupid, null);
	END IF;
						   return ici_groupid;
	END;$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100;  
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}group_rank_style(
						   i_boardid integer)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}group_rank_style_type AS
$BODY$
DECLARE
	   _rec {databaseSchema}.{objectQualifier}group_rank_style_type%ROWTYPE;
BEGIN
FOR _rec IN
(SELECT 1 AS LegendID,name,style FROM {databaseSchema}.{objectQualifier}group
WHERE boardID = i_boardid ORDER BY sortorder)
UNION  ALL
(SELECT 2  AS LegendID,name,style FROM {databaseSchema}.{objectQualifier}rank
WHERE boardID = i_boardid  ORDER BY sortorder)
LOOP
RETURN NEXT _rec;
END LOOP;
END;$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100
  ROWS 1000; 
 --GO

-- Function: {databaseSchema}.{objectQualifier}mail_create(varchar, varchar, varchar, varchar, varchar, text, text)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}mail_create(varchar, varchar, varchar, varchar, varchar, text, text);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}mail_create(
						   i_from varchar, 
						   i_fromname varchar, 
						   i_to varchar, 
						   i_toname varchar, 
						   i_subject varchar, 
						   i_body text, 
						   i_bodyhtml text,
						   i_utctimestamp timestamp)
				  RETURNS void AS
$BODY$
BEGIN
	INSERT INTO {databaseSchema}.{objectQualifier}mail
		(fromuser,fromusername,touser,tousername,created,subject,body,bodyhtml)
	VALUES
		(i_from,i_fromname,i_to,i_toname,i_utctimestamp,i_subject,i_body,i_bodyhtml);	
 END;$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100;  
--GO

-- Function: {databaseSchema}.{objectQualifier}mail_createwatch(integer, varchar, varchar, varchar, text, text, integer)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}mail_createwatch(integer, varchar, varchar, varchar, text, text, integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}mail_createwatch(
						   i_topicid integer, 
						   i_from varchar, 
						   i_fromname varchar, 
						   i_subject varchar, 
						   i_body text, 
						   i_bodyhtml text, 
						   i_userid integer,
						   i_utctimestamp timestamp)
				 RETURNS void AS
$BODY$
BEGIN
	INSERT INTO {databaseSchema}.{objectQualifier}mail(fromuser,fromusername,touser,tousername,created,subject,body,bodyhtml)
	SELECT
		i_from,
		i_fromname,
		b.email,
		b.name,
		i_utctimestamp,
		i_subject,
		i_body,
		i_bodyhtml
	FROM
		{databaseSchema}.{objectQualifier}watchtopic a
		INNER JOIN {databaseSchema}.{objectQualifier}user b on b.userid=a.userid
	WHERE
		b.userid <> i_userid AND
		b.notificationtype NOT IN (10, 20) AND
		a.topicid = i_topicid AND
		(a.lastmail IS NULL OR a.lastmail < b.lastvisit);
	
	INSERT INTO {databaseSchema}.{objectQualifier}mail(fromuser,
	fromusername,touser,tousername,created,subject,body,bodyhtml)
	SELECT
		i_from,
		i_fromname,
		b.email,
		b.name,
		i_utctimestamp,
		i_subject,
		i_body,
		i_bodyhtml
	FROM
		{databaseSchema}.{objectQualifier}watchforum a
		INNER JOIN {databaseSchema}.{objectQualifier}user b ON b.userid=a.userid
		INNER JOIN {databaseSchema}.{objectQualifier}topic c ON c.forumid=a.forumid
	WHERE
		b.userid <> i_userid AND
		b.notificationtype NOT IN (10, 20) AND
		c.topicid = i_topicid AND
		(a.lastmail IS NULL OR a.lastmail < b.lastvisit) AND
		NOT EXISTS (SELECT 1 FROM {databaseSchema}.{objectQualifier}watchtopic x 
		WHERE x.userid=b.userid AND x.topicid=c.topicid);
 
	UPDATE {databaseSchema}.{objectQualifier}watchtopic SET lastmail = i_utctimestamp
	WHERE topicid = i_topicid
	AND userid <> i_userid;
	
	UPDATE {databaseSchema}.{objectQualifier}watchforum SET lastmail = i_utctimestamp
	WHERE forumid = (SELECT forumid FROM {databaseSchema}.{objectQualifier}topic 
	WHERE topicid = i_topicid)
	AND userid <> i_userid;
END;
$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100;    
--GO

-- Function: {databaseSchema}.{objectQualifier}mail_delete(integer)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}mail_delete(integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}mail_delete(
						   i_mailid integer)
				  RETURNS void AS
'DELETE FROM {databaseSchema}.{objectQualifier}mail WHERE mailid = $1;'
  LANGUAGE 'sql' VOLATILE SECURITY DEFINER
  COST 100; 
--GO
-- Function: {databaseSchema}.{objectQualifier}mail_list(integer)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}mail_list(integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}mail_listupdate(
						   i_processid integer,
						   i_utctimestamp timestamp)
				  RETURNS void AS
$BODY$DECLARE 
			 intervaladd integer :=5;
			 timesendattempt timestamp ;
BEGIN
timesendattempt :=i_utctimestamp + (intervaladd || ' minute')::interval;

	  UPDATE {databaseSchema}.{objectQualifier}mail  
	  SET processid = NULL 
	  WHERE processid IS NOT NULL 
	  AND sendattempt > i_utctimestamp;

	 UPDATE {databaseSchema}.{objectQualifier}mail
	SET 
		sendtries = sendtries + 1,
		sendattempt = timesendattempt,
		processid = i_processid
	WHERE
		mailid IN (SELECT mailid FROM {databaseSchema}.{objectQualifier}mail 
   WHERE sendattempt IS NULL OR sendattempt < i_utctimestamp ORDER BY sendattempt,created   LIMIT 10);
		  
		 
		 
 END;$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100 ; 
--GO

-- Function: {databaseSchema}.{objectQualifier}mail_list(integer)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}mail_list(integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}mail_list(
						   i_processid integer,
						   i_utctimestamp timestamp)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}mail_list_return_type AS
$BODY$DECLARE 
			 _rec {databaseSchema}.{objectQualifier}mail_list_return_type%ROWTYPE;
			 intervaladd integer :=5;
			 timesendattempt timestamp ;
BEGIN
timesendattempt :=i_utctimestamp + (intervaladd || ' minute')::interval;

		UPDATE {databaseSchema}.{objectQualifier}mail
		SET 
			ProcessID = NULL
		WHERE
			ProcessID IS NOT NULL AND SendAttempt > i_utctimestamp;

		UPDATE {databaseSchema}.{objectQualifier}Mail
		SET 
			SendTries = SendTries + 1,
			SendAttempt = timesendattempt,
			ProcessID = i_processid
		WHERE
			MailID IN (SELECT MailID FROM {databaseSchema}.{objectQualifier}Mail WHERE SendAttempt < i_utctimestamp OR SendAttempt IS NULL ORDER BY SendAttempt, Created limit 10);
		 
	-- now SELECT all mail reserved for this process...
	FOR _rec IN
	SELECT
			 mailid,
			 fromuser,
			 fromusername,
			 touser,
			 tousername,
			 created,
			 subject,
			 body,
			 bodyhtml,
			 sendtries,
			 sendattempt,
			 processid FROM {databaseSchema}.{objectQualifier}mail 
	WHERE processid = i_processid  ORDER BY sendattempt, created LIMIT 10
LOOP
RETURN NEXT _rec;
END LOOP; 

	 
		
 END;$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100 ROWS 1000; 
--GO



-- Function: {databaseSchema}.{objectQualifier}medal_delete(integer, integer, varchar)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}medal_delete(integer, integer, varchar);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}medal_delete(
						   i_boardid integer, 
						   i_medalid integer, 
						   i_category varchar)
				  RETURNS void AS
$BODY$
BEGIN
 
	IF i_medalid IS NOT NULL THEN
		DELETE from {databaseSchema}.{objectQualifier}groupmedal WHERE medalid = i_medalid;
		DELETE from {databaseSchema}.{objectQualifier}usermedal WHERE medalid = i_medalid; 
		DELETE from {databaseSchema}.{objectQualifier}medal WHERE medalid=i_medalid;
	
	ELSEIF i_category IS NOT NULL AND i_boardid IS NOT NULL THEN
		DELETE from {databaseSchema}.{objectQualifier}groupmedal 
			WHERE medalid in 
			(SELECT medalid FROM {databaseSchema}.{objectQualifier}medal 
			WHERE category=i_category and boardid=i_boardid);
 
		DELETE from {databaseSchema}.{objectQualifier}usermedal 
			WHERE medalid in 
			(SELECT medalid FROM {databaseSchema}.{objectQualifier}medal 
			WHERE category=i_category and boardid=i_boardid);
 
		DELETE from {databaseSchema}.{objectQualifier}medal WHERE category=i_category;
	
	ELSEIF  i_boardid IS NOT NULL THEN
		DELETE from {databaseSchema}.{objectQualifier}groupmedal 
			WHERE medalid
			 in (SELECT medalid FROM {databaseSchema}.{objectQualifier}medal 
			 WHERE boardid=i_boardid);
 
		DELETE from {databaseSchema}.{objectQualifier}usermedal 
			WHERE medalid in 
			(SELECT medalid FROM {databaseSchema}.{objectQualifier}medal 
			WHERE boardid=i_boardid);

		DELETE from {databaseSchema}.{objectQualifier}medal WHERE boardid=i_boardid;
	END IF;
 RETURN;
END;$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100; 
--GO

-- Function: {databaseSchema}.{objectQualifier}medal_list(integer, integer, varchar)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}medal_list(integer, integer, varchar);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}medal_list(
						   i_boardid integer, 
						   i_medalid integer, 
						   i_category varchar)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}medal_list_return_type AS
$BODY$DECLARE
			 _rec {databaseSchema}.{objectQualifier}medal_list_return_type%ROWTYPE;
BEGIN
 
	IF i_medalid IS NOT NULL THEN
	FOR _rec IN
		SELECT 
			 medalid,
			 boardid,
			 name,
			 description,
			 message,
			 category,
			 medalurl,
			 ribbonurl,
			 smallmedalurl,
			 smallribbonurl,
			 smallmedalwidth,
			 smallmedalheight,
			 smallribbonwidth,
			 smallribbonheight,
			 sortorder,
			 flags 
		FROM 
			{databaseSchema}.{objectQualifier}medal 
		WHERE 
			medalid=i_medalid 
		ORDER BY 
			category ASC, 
			sortorder ASC
	LOOP
RETURN NEXT _rec;
END LOOP; 
	ELSEIF i_category IS NOT NULL AND  i_boardid IS NOT NULL THEN
	FOR _rec IN
		SELECT 
			medalid,
			 boardid,
			 name,
			 description,
			 message,
			 category,
			 medalurl,
			 ribbonurl,
			 smallmedalurl,
			 smallribbonurl,
			 smallmedalwidth,
			 smallmedalheight,
			 smallribbonwidth,
			 smallribbonheight,
			 sortorder,
			 flags  
		FROM 
			{databaseSchema}.{objectQualifier}medal 
		WHERE 
			category=i_category and boardid=i_boardid
		ORDER BY 
			category ASC, 
			sortorder ASC
			LOOP
RETURN NEXT _rec;
END LOOP; 
	ELSEIF i_boardid IS NOT NULL THEN
	FOR _rec IN
		SELECT 
			 medalid,
			 boardid,
			 name,
			 description,
			 message,
			 category,
			 medalurl,
			 ribbonurl,
			 smallmedalurl,
			 smallribbonurl,
			 smallmedalwidth,
			 smallmedalheight,
			 smallribbonwidth,
			 smallribbonheight,
			 sortorder,
			 flags 
		FROM 
			{databaseSchema}.{objectQualifier}medal 
		WHERE 
			boardid=i_boardid
		ORDER BY 
			category ASC, 
			sortorder ASC
			LOOP
RETURN NEXT _rec;
END LOOP; 
	  END IF;

 
END;$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100 ROWS 1000;
--GO

-- Function: {databaseSchema}.{objectQualifier}medal_listusers(integer)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}medal_listusers(integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}medal_listusers(
						   i_medalid integer)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}medal_listusers_return_type AS
$BODY$DECLARE
			 _rec {databaseSchema}.{objectQualifier}medal_listusers_return_type%ROWTYPE; 
BEGIN
FOR _rec IN
	SELECT((SELECT 
		a.userid, a.name
	FROM 
		{databaseSchema}.{objectQualifier}user a
		INNER JOIN {databaseSchema}.{objectQualifier}usermedal b ON a.userid = b.userid
	WHERE
		b.medalid=i_medalid)
	UNION	
 
	(SELECT 
		a.userid, a.name
	FROM 
		{databaseSchema}.{objectQualifier}user a
		INNER JOIN {databaseSchema}.{objectQualifier}usergroup b ON a.userid = b.userid
		INNER JOIN {databaseSchema}.{objectQualifier}groupmedal c ON b.groupid = c.groupid
	WHERE
		c.medalid=i_medalid))
LOOP
RETURN NEXT _rec;
END LOOP; 	 

 
END;
 $BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100 ROWS 1000; 
--GO

-- Function: {databaseSchema}.{objectQualifier}medal_resort(integer, integer, integer)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}medal_resort(integer, integer, integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}medal_resort(
						   i_boardid integer, 
						   i_medalid integer, 
						   i_move integer)
				  RETURNS void AS
$BODY$DECLARE 
			 i_Position integer;
			 i_Category varchar(128);
BEGIN
	
	SELECT 
		sortorder,
		category
		INTO i_Position,i_Category
	FROM 
		{databaseSchema}.{objectQualifier}medal 
	WHERE 
		boardid=i_boardid and medalid=i_medalid;
 
	IF (i_Position IS NOT NULL) THEN
 
	IF (i_move > 0) THEN
		UPDATE 
			{databaseSchema}.{objectQualifier}medal
		SET 
			sortorder=sortorder-1
		WHERE 
			boardid=i_boardid and 
			category=i_Category and
			sortorder between i_Position 
						 AND (i_Position + i_move) 
						 AND sortorder between 1 and 255; 	
	ELSEIF (i_move < 0) THEN
		UPDATE
			{databaseSchema}.{objectQualifier}medal
		SET
			sortorder=sortorder+1
		WHERE 
			BoardID=i_boardid AND 
			category=i_Category AND
			sortorder BETWEEN (i_Position+i_move) AND i_Position AND
			sortorder BETWEEN 0 and 254;
	END IF;
 
	i_Position := i_Position + i_move;
 
	IF (i_Position>255) THEN i_Position := 255;
	ELSEIF (i_Position<0) THEN 
		i_Position := 0; END IF;
	UPDATE {databaseSchema}.{objectQualifier}medal
		SET sortorder=i_Position
		WHERE boardid=i_boardid AND 
			medalid=i_medalid;
		END IF;
		RETURN;
END;$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100; 
--GO

-- Function: {databaseSchema}.{objectQualifier}medal_save(integer, integer, varchar, text, varchar, varchar, varchar, varchar, varchar, varchar, integer, integer, integer, integer, smallint, integer)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}medal_save(integer, integer, varchar, text, varchar, varchar, varchar, varchar, varchar, varchar, integer, integer, integer, integer, smallint, integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}medal_save(
						   i_boardid integer, 
						   i_medalid integer, 
						   i_name varchar, 
						   i_description text, 
						   i_message varchar, 
						   i_category varchar, 
						   i_medalurl varchar, 
						   i_ribbonurl varchar, 
						   i_smallmedalurl varchar, 
						   i_smallribbonurl varchar, 
						   i_smallmedalwidth integer, 
						   i_smallmedalheight integer, 
						   i_smallribbonwidth integer, 
						   i_smallribbonheight integer, 
						   i_sortorder integer, 
						   i_flags integer)
				 RETURNS  integer AS
$BODY$DECLARE
			 ici_sortorder smallint:=i_sortorder;
			 ici_medalid integer :=i_medalid;
BEGIN 
	
	IF ici_sortorder > 0 THEN 
	IF EXISTS(SELECT 1 FROM {databaseSchema}.{objectQualifier}medal WHERE sortorder = ici_sortorder) THEN
	UPDATE {databaseSchema}.{objectQualifier}medal
			SET sortorder = sortorder+1				
		WHERE sortorder BETWEEN ici_sortorder AND 254;
	END IF;
	ELSE
	 SELECT MAX(sortorder) INTO ici_sortorder FROM {databaseSchema}.{objectQualifier}medal;
	IF ici_sortorder IS NULL 
	THEN ici_sortorder:=1; 
	ELSE
	ici_sortorder :=ici_sortorder+1;
	END IF;
	END IF;
	
	IF i_medalid IS NULL THEN
		INSERT INTO {databaseSchema}.{objectQualifier}medal
			(boardid,name,description,message,category,
			medalurl,ribbonurl,smallmedalurl,smallribbonurl,
			smallmedalwidth,smallmedalheight,smallribbonwidth,
			smallribbonheight,
			sortorder,flags)
		VALUES
			(i_boardid,i_name,i_description,i_message,i_category,
			i_medalurl,i_ribbonurl,i_smallmedalurl,i_smallribbonurl,
			i_smallmedalwidth,i_smallmedalheight,i_smallribbonwidth,i_smallribbonheight,
			i_sortorder,i_flags);
			SELECT CURRVAL(pg_get_serial_sequence('{databaseSchema}.{objectQualifier}medal','medalid')) INTO ici_medalid;   	
	ELSE 
		UPDATE {databaseSchema}.{objectQualifier}medal
			SET boardid = i_boardid,
				name = i_name,
				description = i_description,
				message = i_message,
				category = i_category,
				medalurl = i_medalurl,
				ribbonurl = i_ribbonurl,
				smallmedalurl = i_smallmedalurl,
				smallribbonurl = i_smallribbonurl,
				smallmedalwidth = i_smallmedalwidth,
				smallmedalheight = i_smallmedalheight,
				smallribbonwidth = i_smallribbonwidth,
				smallribbonheight = i_smallribbonheight,
				sortorder = i_sortorder,
				flags = i_flags
		WHERE medalid = ici_medalid;               
	 END IF;
RETURN ici_medalid;
END;
$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100;
--GO

-- Function: {databaseSchema}.{objectQualifier}message_approve(integer)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}message_approve(integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}message_approve(
						   i_messageid integer)
				  RETURNS void AS
$BODY$DECLARE
			 ici_userid	    integer;
			 ici_ForumID	integer;
			 ici_topicid	integer;
			 ici_Posted	    timestamp;
			 ici_UserName	varchar(128);
			 ici_userdisplayname	varchar(128);
			 ici_NewFlag    integer;
			 ici_parentid   integer;
BEGIN 	
	SELECT 
		 a.userid,
		 a.topicid,
		 b.forumid,
		 a.posted,
		 a.username,
		 a.userdisplayname,
		 a.flags
		INTO ici_userid,ici_topicid,ici_ForumID,ici_Posted,ici_UserName,ici_userdisplayname,ici_NewFlag
	FROM
		{databaseSchema}.{objectQualifier}message a
		inner join {databaseSchema}.{objectQualifier}topic b on b.topicid=a.topicid
	WHERE
		a.messageid = i_messageid;
 
	-- update Message table, set meesage flag to approved 
	ici_NewFlag := ici_NewFlag | 16;
	UPDATE {databaseSchema}.{objectQualifier}message 
	SET flags = flags | 16
	WHERE messageid = i_messageid;

	-- update User table to increase postcount
	IF EXISTS(SELECT 1 FROM {databaseSchema}.{objectQualifier}forum
	 WHERE forumid=ici_ForumID AND (flags & 4)=0) THEN
	
		UPDATE {databaseSchema}.{objectQualifier}user
		 set numposts = numposts + 1 
		 where userid = ici_userid;
		-- upgrade user, i.e. promote rank IF conditions allow it
		PERFORM {databaseSchema}.{objectQualifier}user_upgrade (ici_userid);
	
 END IF;
 
	-- update Topic table with info about last post in topic
	UPDATE {databaseSchema}.{objectQualifier}topic set
		lastposted = ici_Posted,
		lastmessageid = i_messageid,
		lastuserid = ici_userid,
		lastusername = ici_UserName,
		lastuserdisplayname = ici_userdisplayname,
		lastmessageflags = ici_NewFlag,
		numposts = 
		(select count(1) 
		from {databaseSchema}.{objectQualifier}message x 
		WHERE x.topicid={databaseSchema}.{objectQualifier}topic.topicid 
		AND x.isapproved IS TRUE and x.isdeleted IS NOT TRUE)
	WHERE topicid = ici_topicid;
	
	-- update Forum table with last topic/post info
	
	
  SELECT DISTINCT parentid INTO  ici_parentid
  FROM  {databaseSchema}.{objectQualifier}forum
  WHERE forumid = ici_ForumID;  
 
	 UPDATE {databaseSchema}.{objectQualifier}forum set
		lastposted = ici_Posted,
		lasttopicid = ici_topicid,
		lastmessageid = i_messageid,
		lastuserid = ici_userid,
		lastusername = ici_UserName,
		lastuserdisplayname = ici_userdisplayname
	WHERE forumid = ici_ForumID;

	-- update forum stats
	PERFORM {databaseSchema}.{objectQualifier}forum_updatestats (ici_ForumID);
	if (ici_ParentID IS NOT NULL) THEN
	WHILE ici_ParentID > 0 
	LOOP
		UPDATE {databaseSchema}.{objectQualifier}forum SET
				lastposted = ici_Posted,
				lasttopicid = ici_topicid,
				lastmessageid = i_messageid,
				lastuserid = ici_userid,
				lastusername = ici_UserName,
				lastuserdisplayname = ici_userdisplayname
			WHERE
				forumid = ici_parentid
		AND ((lastposted < ici_Posted)
		OR lastposted IS NULL); 
		-- update forum stats
		PERFORM {databaseSchema}.{objectQualifier}forum_updatestats(ici_parentid);        
		 SELECT DISTINCT parentid INTO  ici_parentid
  FROM  {databaseSchema}.{objectQualifier}forum
  WHERE forumid = ici_ParentID;  
  END LOOP; 
  END IF;
	RETURN;
END;
$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100;  
--GO

-- Function: {databaseSchema}.{objectQualifier}message_delete(integer, boolean)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}message_delete(integer, boolean);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}message_delete(
						   i_messageid integer, 
						   i_erasemessage boolean)
				  RETURNS void AS
$BODY$DECLARE
			 ici_topicid		integer;
			 ici_ForumID		integer;
			 ici_MessageCount	integer;
			 ici_LastMessageID	integer;
			 ici_userid		    integer;
BEGIN	
 
	-- Find TopicID and ForumID
	SELECT b.topicid,b.forumid,a.userid 
		INTO ici_topicid,ici_ForumID,ici_userid
		FROM 
			{databaseSchema}.{objectQualifier}message a
			INNER JOIN  {databaseSchema}.{objectQualifier}topic b 
			ON b.topicid=a.topicid
		WHERE
			a.messageid=i_messageid;
 
	-- UPDATE LastMessageID in Topic
	UPDATE {databaseSchema}.{objectQualifier}topic SET 
		lastposted = NULL,
		lastmessageid = NULL,
		lastuserid = NULL,
		lastusername = NULL,
		lastuserdisplayname = NULL,
		lastmessageflags = 22
	WHERE lastmessageid = i_messageid;
 
	-- UPDATE LastMessageID in Forum
	UPDATE {databaseSchema}.{objectQualifier}forum SET 
		lastposted = NULL,
		lasttopicid = NULL,
		lastmessageid = NULL,
		lastuserid = NULL,
		lastusername = NULL,
		lastuserdisplayname = NULL
	WHERE lastmessageid = i_messageid;  	
 
	-- should it be physically deleter or not
	IF i_erasemessage THEN
		DELETE FROM {databaseSchema}.{objectQualifier}attachment WHERE messageid = i_messageid;
		DELETE FROM {databaseSchema}.{objectQualifier}messagereportedaudit WHERE messageid = i_messageid;
		DELETE FROM {databaseSchema}.{objectQualifier}messagereported WHERE messageid = i_messageid;
		DELETE FROM {databaseSchema}.{objectQualifier}messagehistory WHERE messageid = i_messageid;
		DELETE FROM {databaseSchema}.{objectQualifier}thanks WHERE messageid = i_messageid;
		DELETE FROM {databaseSchema}.{objectQualifier}message WHERE messageid = i_messageid; 	
	ELSE
		-- "Delete" it only by setting deleted flag message
		UPDATE {databaseSchema}.{objectQualifier}message 
		SET flags = Flags | 8 WHERE messageid = i_messageid;
	END IF;
	
	-- UPDATE user post count
	UPDATE {databaseSchema}.{objectQualifier}user 
	SET numposts = 
	(SELECT count(1) FROM {databaseSchema}.{objectQualifier}message 
	WHERE userid = ici_userid AND isdeleted IS NOT TRUE AND isapproved IS TRUE) 
	WHERE userid = ici_userid;
	
	-- Delete topic IF there are no more messages
   if not exists (select 1 
	FROM {databaseSchema}.{objectQualifier}message 
	WHERE topicid = ici_topicid limit 1) then  
	 PERFORM {databaseSchema}.{objectQualifier}topic_delete (ici_topicid, null, i_erasemessage, false); 
   END IF;
 
	-- UPDATE lastpost
	PERFORM {databaseSchema}.{objectQualifier}topic_updatelastpost(ici_ForumID,ici_topicid);
	PERFORM {databaseSchema}.{objectQualifier}forum_updatestats(ici_ForumID);
 
	-- UPDATE topic numposts
	UPDATE {databaseSchema}.{objectQualifier}topic
	 SET numposts = 
	 (SELECT COUNT(1) FROM {databaseSchema}.{objectQualifier}message  
	 WHERE {databaseSchema}.{objectQualifier}message.topicid={databaseSchema}.{objectQualifier}topic.topicid 
	 and {databaseSchema}.{objectQualifier}message.isapproved IS TRUE and {databaseSchema}.{objectQualifier}message.isdeleted IS NOT TRUE)
	WHERE {databaseSchema}.{objectQualifier}topic.topicid = ici_topicid;
	RETURN;
END; $BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100; 
--GO

-- Function: {databaseSchema}.{objectQualifier}message_deleteundelete(integer, boolean, varchar, integer)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}message_deleteundelete(integer, boolean, varchar, integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}message_deleteundelete(
						   i_messageid integer, 
						   i_ismoderatorchanged boolean, 
						   i_deletereason varchar, 
						   i_isdeleteaction integer)
				  RETURNS void AS
$BODY$DECLARE
			 ici_topicid		integer;
			 ici_ForumID		integer;
			 ici_MessageCount	integer;
			 ici_LastMessageID	integer;
			 ici_userid		    integer;
			 ici_DeleteAction   integer:=i_isdeleteaction*8;
BEGIN
	-- Find TopicID and ForumID
	SELECT b.topicid,b.forumid,a.userid 
		INTO ici_topicid,ici_ForumID,ici_userid
	FROM 
		{databaseSchema}.{objectQualifier}message a
		INNER JOIN {databaseSchema}.{objectQualifier}topic b ON b.topicid=a.topicid
	WHERE 
		a.messageid=i_messageid;
 
	-- Update LastMessageID in Topic and Forum
	UPDATE {databaseSchema}.{objectQualifier}topic SET
		lastposted = NULL,
		lastmessageid = NULL,
		lastuserid = NULL,
		lastusername = NULL,
		lastuserdisplayname = NULL,
		lastmessageflags = 22
	WHERE lastmessageid = i_messageid;
 
	UPDATE {databaseSchema}.{objectQualifier}forum 
			SET
		lastposted = NULL,
		lasttopicid = NULL,
		lastmessageid = NULL,
		lastuserid = NULL,
		lastusername = NULL,
		lastuserdisplayname = NULL
	   WHERE lastmessageid = i_messageid; 	
	
	-- "Delete" message
	  
	 UPDATE {databaseSchema}.{objectQualifier}message 
	 set ismoderatorchanged = i_ismoderatorchanged, 
		  deletereason = i_deletereason, 
		  flags = flags # 8 
	 WHERE messageid = i_messageid and (flags & 8 <> i_isdeleteaction);
	
	
	 
	-- update num posts for user now that the delete/undelete status has been toggled...
	 UPDATE {databaseSchema}.{objectQualifier}user 
	 SET numposts = (SELECT count(messageid) 
	 FROM {databaseSchema}.{objectQualifier}message 
	 WHERE userid = ici_userid AND isdeleted IS NOT TRUE AND isapproved IS TRUE) 
	 WHERE userid = ici_userid;
 
	-- Delete topic IF there are no more messages
	SELECT COUNT(1) INTO ici_MessageCount 
	FROM {databaseSchema}.{objectQualifier}message 
	WHERE topicid = ici_topicid AND (flags & 8)=0;
	IF ici_MessageCount=0 THEN 
	PERFORM {databaseSchema}.{objectQualifier}topic_delete(ici_topicid,null,false,true); 
	END IF;
	-- update lastpost
	PERFORM {databaseSchema}.{objectQualifier}topic_updatelastpost(ici_ForumID,ici_topicid);
	PERFORM {databaseSchema}.{objectQualifier}forum_updatestats(ici_ForumID);
	-- update topic numposts
	UPDATE {databaseSchema}.{objectQualifier}topic
	 SET numposts = 
	 (SELECT COUNT(1) FROM {databaseSchema}.{objectQualifier}message  
	 WHERE {databaseSchema}.{objectQualifier}message.topicid={databaseSchema}.{objectQualifier}topic.topicid 
	 and {databaseSchema}.{objectQualifier}message.isapproved IS TRUE and {databaseSchema}.{objectQualifier}message.isdeleted IS NOT TRUE)
	WHERE {databaseSchema}.{objectQualifier}topic.topicid = ici_topicid;
END;
$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100; 
--GO

-- Function: {databaseSchema}.{objectQualifier}message_findunread(integer, timestamp )

-- DROP FUNCTION {databaseSchema}.{objectQualifier}message_findunread(integer, timestamp );

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}message_findunread(
						   i_topicid integer,
						   i_messageid integer,
						   i_lastread timestamp,
						   i_showdeleted boolean,
						   i_authoruserid integer)
				 RETURNS SETOF {databaseSchema}.{objectQualifier}message_findunread_return_type AS
$BODY$DECLARE
			 _rec {databaseSchema}.{objectQualifier}message_findunread_return_type%ROWTYPE;
			 _candidatrow {databaseSchema}.{objectQualifier}message_findunread_return_type%ROWTYPE;

ici_firstmessageid integer;
cntrt integer := 1; 

BEGIN
-- find first message id
select 	  
		m.messageid
		into
		ici_firstmessageid
	from
		{databaseSchema}.{objectQualifier}message m	
	where
		m.topicid = i_topicid ORDER BY m.posted limit 1;

-- loop through
FOR _rec IN
		SELECT messageid, cntrt, ici_firstmessageid
		FROM     {databaseSchema}.{objectQualifier}message
		WHERE    topicid = i_topicid
		-- approved
		AND (flags & 16) = 16
		-- not deleted
	   AND (i_showdeleted IS TRUE OR (flags & 8) <> 8 OR (i_authoruserid > 0 AND userid = i_authoruserid))
		AND posted > i_lastread
		ORDER BY posted DESC 
LOOP
 -- RETURN NEXT _rec;
			
  -- simply return last post IF no unread message is found	
  
	-- the messageid was already supplied, find a particular message
	_rec."MessagePosition" = cntrt;
  IF (i_messageid > 0) THEN	
	   IF (_rec."MessageID" = i_messageid) THEN
			 RETURN NEXT _rec;
			 EXIT;
	   END IF;	
	else	
	   -- simply return last message as no MessageID was supplied   
	   _candidatrow := _rec;	  
		  -- return last unread			 
	END IF; 
	cntrt :=  cntrt + 1;
END LOOP; 
 -- simply return last post IF no unread message is found
 
	 IF (_rec."MessageID" is null) THEN
		   _rec  := _candidatrow;
			 IF (_rec."MessageID" is null) THEN
			   FOR _rec in	select m.messageid,  1, ici_firstmessageid 
			   from
				 {databaseSchema}.{objectQualifier}message m	
			  where
		m.topicid = i_topicid			
		AND (m.flags & 16) = 16
		AND (i_showdeleted IS TRUE OR (m.flags & 8) <> 8 OR (i_authoruserid > 0 AND m.UserID = i_authoruserid))
	order by		
		m.posted DESC limit 1
		LOOP
		RETURN NEXT _rec;		
		END LOOP; 
			else	 
			  RETURN NEXT _rec;
			 END IF;
	END IF;
	-- RETURN NEXT _rec;	
	   
		END;$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100 ROWS 1000;    
--GO

-- Function: {databaseSchema}.{objectQualifier}message_getReplies"(integer)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}message_getReplies(integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}message_getReplies(
						   i_messageid integer)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}message_getReplies_return_type AS
$BODY$DECLARE
			 _rec {databaseSchema}.{objectQualifier}message_getReplies_return_type%ROWTYPE;
BEGIN
 FOR _rec IN 
 SELECT messageid 
 FROM {databaseSchema}.{objectQualifier}message 
 WHERE replyto = i_messageid
 LOOP
RETURN NEXT _rec;
END LOOP; 
	   
 END;$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100 ROWS 1000; 
--GO

-- Function: {databaseSchema}.{objectQualifier}message_getReplies"(integer)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}message_getReplies(integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}message_secdata(
						   i_messageid integer,
						   i_pageuserid integer)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}message_secdata_return_type AS
$BODY$DECLARE
			 _rec {databaseSchema}.{objectQualifier}message_secdata_return_type%ROWTYPE;
			 ici_pageuserid integer:=i_pageuserid; 
BEGIN
-- BoardID=i_boardid and
IF (ici_pageuserid is null) THEN
select userid INTO ici_pageuserid from {databaseSchema}.{objectQualifier}user 
where  isguest IS TRUE ORDER BY joined DESC LIMIT 1;
END IF;
	FOR _rec IN	SELECT
		m.messageid,
		m.userid,
		COALESCE(t.username, u.name) as Name,
		COALESCE(t.userdisplayname, u.displayname) as DisplayName,	
		m.message,
		m.posted,
		t.topicid,
		t.forumid,
		t.topic,
		t.priority,
		m.flags,		
		COALESCE(m.edited,m.posted) AS Edited ,
		t.flags AS TopicFlags,		
		m.editreason,
		m.position,
		m.ismoderatorchanged,
		m.deletereason,
		m.blogpostid,
		t.pollid,
		m.ip,
		COALESCE(m.editedby,m.userid)       
	FROM
		{databaseSchema}.{objectQualifier}topic t
		join {databaseSchema}.{objectQualifier}message m  on m.topicid = t.topicid		
		join {databaseSchema}.{objectQualifier}user u on u.userid = t.userid	
		left join {databaseSchema}.{objectQualifier}vaccess x on 
		x.forumid=COALESCE(t.forumid,0)			
	WHERE
		m.messageid = i_messageid AND x.userid = ici_pageuserid AND x.readaccess > 0	LIMIT 1
		LOOP
		RETURN NEXT _rec;
		END LOOP;
		
		 END;$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100 ROWS 1;
--GO

-- Function: {databaseSchema}.{objectQualifier}message_list(integer)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}message_list(integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}message_list(
						   i_messageid integer)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}message_list_return_type AS
$BODY$DECLARE
			 _rec {databaseSchema}.{objectQualifier}message_list_return_type%ROWTYPE;
BEGIN
FOR _rec IN
	SELECT
		a.messageid,
		a.userid,
		b.name AS UserName,
		b.displayname AS UserDisplayName,
		a.message,
		c.topicid,
		c.forumid,
		c.topic,
		c.status,
		c.styles,
		c.priority,
		c.description,
		a.flags,
		c.userid AS TopicOwnerID,
		COALESCE(a.edited,a.posted) AS Edited,
		c.flags AS TopicFlags,
		d.flags AS ForumFlags,
		a.editreason,
		a.position,
		a.ismoderatorchanged,
		a.deletereason,
		a.blogpostid,
		c.pollid,
		a.ip,
		a.replyto,
		a.externalmessageid,
		a.referencemessageid
	FROM
		{databaseSchema}.{objectQualifier}message a
		INNER JOIN {databaseSchema}.{objectQualifier}user b ON b.userid = a.userid
		INNER JOIN {databaseSchema}.{objectQualifier}topic c ON c.topicid = a.topicid
		INNER JOIN {databaseSchema}.{objectQualifier}forum d ON c.forumid = d.forumid
	WHERE
		a.messageid = i_messageid
		LOOP
RETURN NEXT _rec;
END LOOP; 
	   
 END;$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100 ROWS 1000;    
  --GO

-- Function: {databaseSchema}.{objectQualifier}message_listreported(integer, integer)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}message_listreported(integer, integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}message_listreported(
						   i_forumid integer)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}message_listreported_return_type AS
$BODY$DECLARE
			 _rec {databaseSchema}.{objectQualifier}message_listreported_return_type%ROWTYPE;
BEGIN
FOR _rec IN
	SELECT
		a.messageid,
		a.message,
		a.resolved,
		a.resolvedby,
		a.resolveddate,
		b.message AS OriginalMessage,
		b.flags,
		b.ismoderatorchanged,
		COALESCE(b.username,d.name) AS UserName,
		COALESCE(b.userdisplayname,d.displayname) AS UserName,
		b.userid AS UserID,
		b.posted AS Posted,
		b.topicid,	
		c.topic AS Topic,
		(SELECT count(logid) FROM {databaseSchema}.{objectQualifier}messagereportedaudit 
		WHERE {databaseSchema}.{objectQualifier}messagereportedaudit.messageid = a.messageid) AS NumberOfReports
	FROM
		{databaseSchema}.{objectQualifier}messagereported a
	INNER JOIN
		{databaseSchema}.{objectQualifier}message b ON a.messageid = b.messageid
	INNER JOIN
		{databaseSchema}.{objectQualifier}topic c ON b.topicid = c.topicid
	INNER JOIN
		{databaseSchema}.{objectQualifier}user d ON b.userid = d.userid
	WHERE
		c.forumid = i_forumid AND
		(c.flags & 16)=0 AND
		(b.flags & 8)=0 AND
		(c.flags & 8)=0 AND
		(b.flags & 128) = 128
	ORDER BY
		b.topicid DESC, b.posted DESC
LOOP
RETURN NEXT _rec;
END LOOP; 
		
END;

$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100 ROWS 1000; 
--GO

-- Function: {databaseSchema}.{objectQualifier}message_move(integer, integer)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}message_move(integer, integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}message_move(
						   i_messageid integer, 
						   i_movetotopic integer)
				  RETURNS void AS
$BODY$DECLARE            
			 ici_Position integer;
			 ici_ReplyToID integer;
			 ici_OldTopicID integer;
			 ici_OldForumID integer;
			 ici_NewForumID	integer;          
			 ici_LastMessageID integer;
			 ici_userid integer;
			 ici_username varchar(255);
			 ici_userdisplayname varchar(255);
			 ici_eraseoldtopic boolean :=false;
			 ici_lastmessageflags integer :=22;
BEGIN   

SELECT    forumid INTO ici_NewForumID
				FROM         {databaseSchema}.{objectQualifier}topic
				WHERE     (topicid = i_movetotopic); 
 
SELECT    topicid INTO ici_OldTopicID
				FROM         {databaseSchema}.{objectQualifier}message
				WHERE     (messageid = i_messageid);
 
SELECT     forumid INTO ici_OldForumID
				FROM         {databaseSchema}.{objectQualifier}topic
				WHERE     (topicid = ici_OldTopicID);
 
SELECT     messageid INTO ici_ReplyToID
			FROM         {databaseSchema}.{objectQualifier}message
			WHERE     (position = 0) AND (topicid = i_movetotopic);
 
SELECT    MAX(position) + 1 AS Expr1 INTO ici_Position
			FROM         {databaseSchema}.{objectQualifier}message
			WHERE     (topicid = i_movetotopic) 
			and posted < (SELECT posted 
			FROM {databaseSchema}.{objectQualifier}message 
			WHERE messageid = i_messageid ) ;
 
IF ici_Position IS NULL THEN  ici_Position := 0; END IF;
 
 update {databaseSchema}.{objectQualifier}message SET
		position = position+1
	 WHERE     (topicid = i_movetotopic) 
	 and posted > (SELECT posted from {databaseSchema}.{objectQualifier}message 
	 WHERE messageid = i_messageid);
 
 update {databaseSchema}.{objectQualifier}message set
		position = position-1
	 WHERE     (topicid = ici_OldTopicID) 
	 and posted > (SELECT posted 
	 from {databaseSchema}.{objectQualifier}message WHERE messageid = i_messageid);
 
	 -- Update LastMessageID in Topic and Forum
	UPDATE {databaseSchema}.{objectQualifier}topic set
		lastposted = NULL,
		lastmessageid = NULL,
		lastuserid = NULL,	
		lastusername = NULL,
		lastuserdisplayname = NULL
	WHERE lastmessageid = i_messageid;
 
	UPDATE {databaseSchema}.{objectQualifier}forum set
		lastposted = NULL,
		lasttopicid = NULL,
		lastmessageid = NULL,
		lastuserid = NULL,
		lastusername = NULL,
		lastuserdisplayname = NULL
	WHERE lastmessageid = i_messageid;
 
 
 UPDATE {databaseSchema}.{objectQualifier}message SET
	topicid = i_movetotopic,
	replyto = ici_ReplyToID,
	position = ici_Position
 WHERE  messageid = i_messageid;
 
	 -- Delete topic IF there are no more messages
  
	IF NOT EXISTS (SELECT 1 
	FROM {databaseSchema}.{objectQualifier}message 
	WHERE topicid = ici_OldTopicID and (flags & 8)=0) THEN	
 SELECT userid, username, userdisplayname INTO ici_userid,ici_username,ici_userdisplayname
		FROM {databaseSchema}.{objectQualifier}message WHERE messageid=i_messageid;	 
 UPDATE {databaseSchema}.{objectQualifier}topic SET
	userid = ici_userid,
	username = ici_username,
	userdisplayname = ici_userdisplayname
 WHERE  topicid = i_movetotopic;
 IF NOT EXISTS (SELECT 1 
	FROM {databaseSchema}.{objectQualifier}message 
	WHERE topicid = ici_OldTopicID and (flags & 8)=8) THEN	
 ici_eraseoldtopic = true;
 end if;
	PERFORM {databaseSchema}.{objectQualifier}topic_delete (ici_OldTopicID,null, ici_eraseoldtopic, true); 
	END IF;
 
	 -- update lastpost
	PERFORM {databaseSchema}.{objectQualifier}topic_updatelastpost (ici_OldForumID,ici_OldTopicID);
	PERFORM {databaseSchema}.{objectQualifier}topic_updatelastpost (ici_NewForumID,i_movetotopic);
 
	-- update topic numposts
	UPDATE {databaseSchema}.{objectQualifier}topic SET
		numposts = (SELECT count(1) from {databaseSchema}.{objectQualifier}message x 
					  WHERE x.topicid={databaseSchema}.{objectQualifier}topic.topicid 
					  and x.isapproved IS TRUE and not x.isdeleted)
	WHERE topicid = ici_OldTopicID;
	UPDATE {databaseSchema}.{objectQualifier}topic set
		numposts = (SELECT count(1) from {databaseSchema}.{objectQualifier}message x 
					   WHERE x.topicid={databaseSchema}.{objectQualifier}topic.topicid
						 and x.isapproved IS TRUE and not x.isdeleted)
	WHERE topicid = i_MoveToTopic;
	
	PERFORM {databaseSchema}.{objectQualifier}forum_updatelastpost(ici_OldForumID);
	PERFORM {databaseSchema}.{objectQualifier}forum_updatestats(ici_OldForumID);
	
	PERFORM {databaseSchema}.{objectQualifier}forum_updatelastpost(ici_NewForumID);
	PERFORM {databaseSchema}.{objectQualifier}forum_updatestats(ici_NewForumID);	
	
 RETURN;
 END;$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100; 
--GO

-- DROP FUNCTION {databaseSchema}.{objectQualifier}message_reply_list(integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}message_reply_list(
						   i_messageid integer)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}message_reply_list_return_type AS
$BODY$DECLARE
			 _rec {databaseSchema}.{objectQualifier}message_reply_list_return_type%ROWTYPE;
BEGIN
	
 /*SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;*/
 FOR _rec IN	SELECT
				 a.messageid,
		a.posted,
		c.topic AS Subject,
		a.message,
		a.userid,
		a.flags,
		COALESCE(a.username,b.name) AS UserName,
		b.signature
	FROM
		{databaseSchema}.{objectQualifier}message a
		INNER JOIN {databaseSchema}.{objectQualifier}user b on b.userid = a.userid
		INNER JOIN {databaseSchema}.{objectQualifier}topic c on c.topicid = a.topicid
	WHERE
		(a.flags & 16)=16 AND
		a.replyto = i_messageid 
LOOP
RETURN NEXT _rec;
END LOOP; 
  
  /*SET TRANSACTION ISOLATION LEVEL READ COMMITTED;*/
END;
$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100
  ROWS 1000;   
--GO
-- DROP FUNCTION {databaseSchema}.{objectQualifier}message_reply_list(integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}message_listreporters(
						   i_messageid integer, 
						   i_userid integer)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}message_listreporters_return_type AS
$BODY$DECLARE
			 _rec {databaseSchema}.{objectQualifier}message_listreporters_return_type%ROWTYPE;
BEGIN
IF i_userid > 0 THEN
	FOR _rec IN
	SELECT DISTINCT b.userid, a.name AS UserName, a.displayname AS UserDisplayName, b.reportednumber, b.reporttext 
	FROM {databaseSchema}.{objectQualifier}user a,
	{databaseSchema}.{objectQualifier}messagereportedaudit b
	WHERE a.userid = b.userid AND b.messageid = i_messageid AND a.userid = i_userid
	LOOP
	  RETURN NEXT _rec;
	END LOOP;	
ELSE
 FOR _rec IN
	SELECT DISTINCT b.userid, a.name AS UserName, a.displayname AS UserDisplayName, b.reportednumber, b.reporttext 
	FROM {databaseSchema}.{objectQualifier}user a,
	{databaseSchema}.{objectQualifier}messagereportedaudit b
	WHERE a.userid = b.userid AND b.messageid = i_messageid
	LOOP
	  RETURN NEXT _rec;
	END LOOP;	
	END IF;   
END;
$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100
  ROWS 1000;  
--GO

-- Function: {databaseSchema}.{objectQualifier}message_report(integer, integer, integer, timestamp )

-- DROP FUNCTION {databaseSchema}.{objectQualifier}message_report(integer, integer, integer, timestamp );

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}message_report(
						   i_messageid integer, 
						   i_reporterid integer, 
						   i_reporteddate timestamp, 
						   i_reporttext varchar(4000),
						   i_utctimestamp timestamp)
				  RETURNS void AS
$BODY$
BEGIN 
	IF NOT EXISTS (SELECT messageid FROM {databaseSchema}.{objectQualifier}messagereported 
		   WHERE messageid=i_messageid LIMIT 1) THEN
		  INSERT INTO {databaseSchema}.{objectQualifier}messagereported(messageid, message)
		SELECT 
			a.messageid,
			a.message
		FROM
			{databaseSchema}.{objectQualifier}message a
		WHERE
			a.messageid = i_messageid;
	END IF;
	IF NOT EXISTS (SELECT messageid from 
	{databaseSchema}.{objectQualifier}messagereportedaudit 
	 WHERE messageid=i_messageid AND 
	 userid=i_reporterid LIMIT 1) THEN
		INSERT INTO {databaseSchema}.{objectQualifier}messagereportedaudit
		 (messageid,userid,reported, reporttext) VALUES (i_messageid,i_reporterid,i_reporteddate, i_utctimestamp::varchar(40) || '??' || i_reporttext ); 
	ELSE
		UPDATE {databaseSchema}.{objectQualifier}messagereportedaudit SET reportednumber = ( CASE WHEN reportednumber < 2147483647 THEN  reportednumber  + 1 ELSE reportednumber END ), reported = i_reporteddate,  reporttext = (CASE WHEN (LENGTH(reporttext) + LENGTH(i_reporttext ) + 255 < 4000)  THEN  reporttext ||  '|' || CAST(i_utctimestamp as varchar(40)) || '??' ||  i_reporttext END) WHERE messageid=i_messageid AND userid=i_reporterid; 
	END IF;

	
 
	-- update Message table to set message with flag Reported
	UPDATE {databaseSchema}.{objectQualifier}message 
	SET flags = flags | 128 
	WHERE messageid = i_messageid;

 END;
$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100;
--GO  

-- Function: {databaseSchema}.{objectQualifier}message_reportcopyover(integer)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}message_reportcopyover(integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}message_reportcopyover(
						   i_messageid integer)
				  RETURNS void AS
$BODY$DECLARE
			 ici_Message text;
BEGIN
SELECT m.message INTO ici_Message
FROM {databaseSchema}.{objectQualifier}messagereported mr
	JOIN {databaseSchema}.{objectQualifier}message m
   ON m.messageid = mr.messageid
	WHERE mr.messageid = i_messageid;
	
	UPDATE {databaseSchema}.{objectQualifier}messagereported
	SET {databaseSchema}.{objectQualifier}messagereported.message = ici_Message
	WHERE mr.messageid = i_messageid;
	RETURN;
END;$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100; 
--GO

-- Function: {databaseSchema}.{objectQualifier}message_reportresolve(integer, integer, integer)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}message_reportresolve(integer, integer, integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}message_reportresolve(
						   i_messageflag integer, 
						   i_messageid integer, 
						   i_userid integer,
						   i_utctimestamp timestamp)
				  RETURNS void AS
$BODY$
BEGIN
	UPDATE {databaseSchema}.{objectQualifier}messagereported
	SET resolved = TRUE, resolvedby = i_userid, resolveddate = i_utctimestamp
	WHERE messageid = i_messageid;
	
	-- Remove Flag 
	UPDATE {databaseSchema}.{objectQualifier}message
	SET flags = flags & (~CAST(POWER(2, i_messageflag) AS integer))
	WHERE messageid = i_messageid;
 END;$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100; 
--GO

-- Function: {databaseSchema}.{objectQualifier}message_save(integer, integer, text, varchar, varchar, timestamp, integer, varchar, integer)

DROP FUNCTION IF EXISTS {databaseSchema}.{objectQualifier}message_save(integer,integer,text,varchar,varchar,timestamp,integer,varchar,varchar,varchar,integer,varchar,timestamp);
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}message_save(
						   i_topicid integer, 
						   i_userid integer, 
						   i_message text, 
						   i_username varchar(255), 
						   i_ip varchar(100), 
						   i_posted timestamp, 
						   i_replyto integer, 
						   i_blogpostid varchar(128), 
						   i_externalmessageid varchar(255),
						   i_referencemessageid varchar(255),                         
						   i_messagedescription  varchar(255),
						   i_flags integer,
						   i_utctimestamp timestamp
						   )
				  RETURNS integer AS
$BODY$DECLARE
			 i_messageid integer;
			 ici_ForumID integer;
			 ici_ForumFlags integer;
			 ici_Position integer;
			 ici_Indent integer;
			 ici_temp integer;
			 irr integer;
			 ici_Posted timestamp:=i_posted;
			 ici_ReplyTo int:=i_replyto;
			 ici_OverrideDisplayName boolean;			
BEGIN
 
	IF ici_Posted IS NULL THEN
		 ici_Posted := i_utctimestamp; END IF;
 
	SELECT  x.forumid,  y.flags
		INTO ici_ForumID,ici_ForumFlags
	FROM 
		{databaseSchema}.{objectQualifier}topic x
	INNER JOIN 
		{databaseSchema}.{objectQualifier}forum y ON y.forumid=x.forumid
	WHERE x.topicid = i_topicid;
 
	IF i_ReplyTo IS NULL THEN
			SELECT 0,0 INTO ici_Position, ici_Indent; -- New thread
 
	ELSEIF i_ReplyTo<0 THEN
		-- Find post to reply to AND indent of this post 
		SELECT  messageid, indent+1
				INTO ici_ReplyTo,ici_Indent
		FROM {databaseSchema}.{objectQualifier}message
		WHERE topicid = i_topicid AND replyto IS NULL
		ORDER BY posted DESC LIMIT 1;
 
	ELSE
		-- Got reply, find indent of this post 
			SELECT indent+1 INTO ici_Indent
			FROM {databaseSchema}.{objectQualifier}message
			WHERE messageid=i_ReplyTo;
		END IF;  
 
	-- Find position 
	IF ici_ReplyTo IS NOT NULL THEN
		
		 SELECT replyto,position INTO ici_temp,ici_Position 
		 FROM {databaseSchema}.{objectQualifier}message 
		 WHERE messageid=i_ReplyTo;
 
		 IF ici_temp IS NULL THEN
			-- We are replying to first post 
			 SELECT MAX(position)+1 INTO ici_Position  
			 FROM {databaseSchema}.{objectQualifier}message 
			 WHERE topicid=i_topicid;
 
		 ELSE
			-- Last position of replies to parent post
			 SELECT MIN(position) INTO ici_Position 
			 FROM {databaseSchema}.{objectQualifier}message 
			 WHERE replyto=ici_temp 
			 AND position>ici_Position;
 
		 -- No replies, THEN USE parent post's position+1
		 IF ici_Position IS NULL THEN
			 SELECT position+1 INTO ici_Position 
			 FROM {databaseSchema}.{objectQualifier}message 
			 WHERE messageid=ici_ReplyTo;
		-- Increase position of posts after this

		 UPDATE {databaseSchema}.{objectQualifier}message SET position=position+1 
		 WHERE topicid=i_TopicID 
		 AND position>=ici_Position; 
		 END IF;
		 END IF;
	 END IF;
 

 SELECT EXISTS (SELECT 1 FROM {databaseSchema}.{objectQualifier}user WHERE userid = i_userid and name != i_username) INTO ici_OverrideDisplayName;
	 INSERT INTO {databaseSchema}.{objectQualifier}message( 
				userid, 
				message,
				description,
				topicid, 
				posted,
				edited,
				username,
				userdisplayname, 
				ip, 
				replyto, 
				position, 
				indent,
				flags, 
				blogpostid, 
				externalmessageid, 
				referencemessageid)
	VALUES    ( i_userid,
				i_message,
				i_messagedescription,
				i_topicid,
				ici_posted,
				ici_posted,
				(CASE WHEN ici_OverrideDisplayName is true THEN i_username ELSE (SELECT name FROM {databaseSchema}.{objectQualifier}user WHERE userid = i_userid) END),
				(CASE WHEN ici_OverrideDisplayName is true THEN i_username 
					ELSE (SELECT displayname FROM {databaseSchema}.{objectQualifier}user WHERE userid = i_userid) END), 
				i_ip, 
				ici_ReplyTo,
				ici_Position, 
				ici_Indent, 
				i_flags & ~16, 
				i_blogpostid, 
				i_externalmessageid,                 i_referencemessageid) returning messageid INTO i_MessageID;
	
	
	-- IF ((ici_ForumFlags & 8) = 0) OR ((i_flags & 16) = 16) THEN
	 IF ((i_flags & 16) = 16) THEN
	 PERFORM {databaseSchema}.{objectQualifier}message_approve(i_messageid); 
	 END IF;
  
RETURN i_MessageID;
	  END;			
$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100;
--GO

 -- Function: {databaseSchema}.{objectQualifier}message_simplelist(integer, integer)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}message_simplelist(integer, integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}message_simplelist(
						   i_startid integer, 
						   i_limit integer)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}message_simplelist_return_type AS
$BODY$DECLARE
			 cntr integer:=0;
			 _rec {databaseSchema}.{objectQualifier}message_simplelist_return_type%ROWTYPE;
BEGIN     
	   FOR _rec IN      
SELECT  m.messageid,
				 m.topicid        
		FROM     {databaseSchema}.{objectQualifier}message m
		WHERE    m.messageid >= i_startid
		AND m.messageid < (i_startid + i_limit)
		AND m.topicid IS NOT NULL ORDER BY m.messageid
		 LOOP      
RETURN NEXT _rec;
END LOOP; 
RETURN;          
	END;$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100
  ROWS 1000;
--GO

-- Function: {databaseSchema}.{objectQualifier}message_unapproved(integer)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}message_unapproved(integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}message_unapproved(
						   i_forumid integer)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}message_unapproved_return_type AS
$BODY$DECLARE
			 _rec {databaseSchema}.{objectQualifier}message_unapproved_return_type%ROWTYPE;
BEGIN
FOR _rec IN
	  SELECT
	  b.messageid AS MessageID,
	  b.userid AS UserID,
	  COALESCE(b.username,c.name) AS UserName,
	  COALESCE(b.userdisplayname,c.displayname) AS UserName,
	  b.posted AS Posted,
	  a.topicid, 
	  a.topic AS Topic,
	  b.numposts,
	  b.message AS Message,
	  b.flags AS Flags	,
	  b.ismoderatorchanged AS IsModeratorChanged
	  FROM
	  {databaseSchema}.{objectQualifier}topic a
	  INNER JOIN {databaseSchema}.{objectQualifier}message b on b.topicid = a.topicid
	  INNER JOIN {databaseSchema}.{objectQualifier}user c on c.userid = b.userid
	  WHERE
	  a.forumid = i_forumid AND
	  (b.flags & 16)=0 AND
		(a.flags & 8)=0 AND
		(b.flags & 8)=0
	ORDER BY
		a.posted
LOOP
RETURN NEXT _rec;
END LOOP;   
		
END;
$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100 ROWS 1000; 
--GO

-- Function: {databaseSchema}.{objectQualifier}message_update(integer, integer, varchar, integer, text, varchar, boolean, boolean)

DROP FUNCTION IF EXISTS {databaseSchema}.{objectQualifier}message_update(integer, integer, varchar, integer, text, varchar, boolean, boolean);
--GO
DROP FUNCTION IF EXISTS {databaseSchema}.{objectQualifier}message_update(integer,integer,character varying,character varying,character varying,character varying,integer,text,character varying,integer,boolean,boolean,text,character varying,text,timestamp without time zone);
--GO
CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}message_update
						   (
						   i_messageid integer, 
						   i_priority integer, 
						   i_subject varchar, 
						   i_status varchar, 
						   i_styles varchar,
						   i_description varchar,                           
						   i_flags integer, 
						   i_message text, 
						   i_reason varchar, 
						   i_editedby integer, 
						   i_ismoderatorchanged boolean, 
						   i_overrideapproval boolean, 
						   i_originalmessage text,                           
						   i_messagedescription varchar,
						   i_tags text,
						   i_utctimestamp timestamp )
				  RETURNS void AS
$BODY$DECLARE
			 ici_topicid	integer;
			 ici_ForumFlags	integer;
			 ici_flags integer:= i_flags & ~16;
			 intNull integer;
BEGIN		
	
	SELECT 
	a.topicid,
	c.flags
		INTO ici_topicid,ici_ForumFlags
	FROM 
		{databaseSchema}.{objectQualifier}message a
		INNER JOIN {databaseSchema}.{objectQualifier}topic b ON b.topicid = a.topicid
		INNER JOIN {databaseSchema}.{objectQualifier}forum c ON c.forumid = b.forumid
	WHERE 
		a.messageid = i_messageid;
 
	IF (i_overrideapproval IS TRUE OR (ici_ForumFlags & 8)=0) THEN  
	ici_flags := ici_flags | 16; END IF;
 
 -- insert current message variant - use OriginalMessage in future 	
	insert into {databaseSchema}.{objectQualifier}messagehistory
	(messageid,		
	 message,
	 ip,
	 edited,
	 editedby,		
	 editreason,
	 ismoderatorchanged,
	 flags)
	select
	messageid, 
	i_originalmessage, 
	ip, 
	COALESCE(edited,posted), 
	COALESCE(editedby,userid), 
	editreason, 
	ismoderatorchanged, 
	flags
	from {databaseSchema}.{objectQualifier}message where
		messageid = i_messageid;
		
	UPDATE {databaseSchema}.{objectQualifier}message SET
		message = i_message,
		description = i_messagedescription,
		edited = i_utctimestamp,
		editedby = i_editedby,
		flags = i_Flags, 		
		ismoderatorchanged  = i_ismoderatorchanged,
				 editreason = i_reason
	WHERE
		messageid = i_messageid;
 
	IF i_Priority IS NOT NULL THEN
		UPDATE {databaseSchema}.{objectQualifier}topic SET
			priority = i_priority
		WHERE
			topicid = ici_topicid;
	END IF;
 
	IF NOT i_Subject = '' AND i_subject IS NOT NULL THEN
		UPDATE {databaseSchema}.{objectQualifier}topic SET
			topic = substr(i_subject, 1, 128),
			description = i_description,
			status = i_status,
			styles = i_styles
		WHERE
			topicid = ici_topicid;
	END IF; 

	PERFORM {databaseSchema}.{objectQualifier}topic_tagsave(ici_topicid, i_tags);

	-- If forum is moderated, make sure last post pointers are correct
	IF (ici_ForumFlags & 8)<>0 THEN 
	PERFORM {databaseSchema}.{objectQualifier}topic_updatelastpost(intNull,intNull);
	END IF;
RETURN;
END;$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100;
--GO 

-- Function: {databaseSchema}.{objectQualifier}nntpforum_delete(integer)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}nntpforum_delete(integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}nntpforum_delete(
						   i_nntpforumid integer)
				  RETURNS void AS
$BODY$
BEGIN
DELETE FROM {databaseSchema}.{objectQualifier}nntptopic where nntpforumid = i_nntpforumid;
DELETE FROM {databaseSchema}.{objectQualifier}nntpforum where nntpforumid = i_nntpforumid;
END;$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100; 
--GO

-- Function: {databaseSchema}.{objectQualifier}nntpforum_list(integer, integer, integer, boolean)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}nntpforum_list(integer, integer, integer, boolean);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}nntpforum_list(
						   i_boardid integer, 
						   i_minutes integer, 
						   i_nntpforumid integer, 
						   i_active boolean,
						   i_utctimestamp timestamp)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}nntpforum_list_return_type AS
$BODY$DECLARE
			 i_tmptimestmp timestamp ;
			 _rec {databaseSchema}.{objectQualifier}nntpforum_list_return_type%ROWTYPE;
BEGIN
 FOR _rec IN	
 SELECT
		a.name,
		a.address,
		COALESCE(a.port,119) AS Port,
		a.username,
		a.userpass,		
		a.nntpserverid,
		b.nntpforumid,		
		b.groupname,
		b.forumid,
		b.lastmessageno,
		b.lastupdate,
		b.active,
		b.datecutoff,
		c.name AS "ForumName",
		c.categoryid 
	FROM
		{databaseSchema}.{objectQualifier}nntpserver a
		JOIN {databaseSchema}.{objectQualifier}nntpforum b 
				ON b.nntpserverid = a.nntpserverid
		JOIN {databaseSchema}.{objectQualifier}forum c 
				ON c.forumid = b.forumid
	WHERE
		(i_minutes IS NULL 
				 OR EXTRACT(MINUTE FROM (i_utctimestamp - b.lastupdate))>i_minutes) 
				 AND
		(i_nntpforumid IS NULL OR b.nntpforumid=i_nntpforumid) AND
		a.boardid=i_boardid AND
		(i_Active IS NULL OR b.active=i_active)
	ORDER BY
		a.name,
		b.groupname
LOOP
RETURN NEXT _rec;
END LOOP; 
		 
END;$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100
  ROWS 1000; 
--GO

-- Function: {databaseSchema}.{objectQualifier}nntpforum_save(integer, integer, varchar, varchar, boolean)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}nntpforum_save(integer, integer, varchar, varchar, boolean);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}nntpforum_save(
						   i_nntpforumid integer, 
						   i_nntpserverid integer, 
						   i_groupname varchar,
						   i_forumid integer, 
						   i_active boolean,
						   i_datecutoff timestamp,
						   i_utctimestamp timestamp)
				  RETURNS void AS
$BODY$
BEGIN
	IF i_nntpforumid IS NULL THEN
		INSERT INTO {databaseSchema}.{objectQualifier}nntpforum(
					nntpserverid,
					groupname,
					forumid,
					lastmessageno,
					lastupdate,
					active,
					datecutoff)
		VALUES(i_nntpserverid,i_groupname,i_forumid,0,i_utctimestamp,i_Active, i_datecutoff);
	ELSE
		UPDATE {databaseSchema}.{objectQualifier}nntpforum SET
			   nntpserverid = i_nntpserverid,
			   groupname = i_groupname,
			   forumid = i_forumid,
			   active = i_active,
			   datecutoff = i_datecutoff
		WHERE nntpforumid = i_nntpforumid;
	END IF;
RETURN;
END;

$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100; 
--GO

-- Function: {databaseSchema}.{objectQualifier}nntpforum_update(integer, integer, integer)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}nntpforum_update(integer, integer, integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}nntpforum_update(
						   i_nntpforumid integer, 
						   i_lastmessageno integer, 
						   i_userid integer,
						   i_utctimestamp timestamp)
				  RETURNS void AS
$BODY$DECLARE	
			 ici_ForumID	integer;
BEGIN 	
	SELECT forumid INTO ici_ForumID from {databaseSchema}.{objectQualifier}nntpforum where nntpforumid=i_nntpforumid;
 
	UPDATE {databaseSchema}.{objectQualifier}nntpforum SET
		lastmessageno = i_lastmessageno,
		lastupdate = i_utctimestamp
	WHERE nntpforumid = i_nntpforumid;
 
	UPDATE {databaseSchema}.{objectQualifier}topic SET 
		numposts = 
		(SELECT COUNT(1) FROM {databaseSchema}.{objectQualifier}message x 
		WHERE x.TopicID={databaseSchema}.{objectQualifier}topic.topicid 
		AND x.isapproved IS TRUE AND x.isdeleted IS NOT TRUE)
	WHERE forumid=ici_ForumID;
 
	-- exec {databaseSchema}.{objectQualifier}user_upgrade` i_UserID 
	PERFORM {databaseSchema}.{objectQualifier}forum_updatestats(ici_ForumID);
	-- exec {databaseSchema}.{objectQualifier}topic_updatelastpost` ici_ForumID,null
END;
$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100;  
--GO

-- Function: {databaseSchema}.{objectQualifier}nntpserver_delete(integer)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}nntpserver_delete(integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}nntpserver_delete(
						   i_nntpserverid integer)
				  RETURNS void AS
$BODY$
BEGIN
	DELETE FROM {databaseSchema}.{objectQualifier}nntptopic WHERE nntpforumid IN (SELECT 
nntpforumid FROM {databaseSchema}.{objectQualifier}nntpforum WHERE nntpserverid =
i_nntpserverid);
	DELETE FROM {databaseSchema}.{objectQualifier}nntpforum WHERE nntpserverid = 
i_nntpserverid;
	DELETE FROM {databaseSchema}.{objectQualifier}nntpserver WHERE nntpserverid = 
i_nntpserverid;
END;$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100; 
--GO

-- Function: {databaseSchema}.{objectQualifier}nntpserver_list(integer, integer)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}nntpserver_list(integer, integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}nntpserver_list(
						   i_boardid integer, 
						   i_nntpserverid integer)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}nntpserver_list_return_type AS
$BODY$DECLARE
			 _rec {databaseSchema}.{objectQualifier}nntpserver_list_return_type%ROWTYPE;
BEGIN

	IF i_nntpserverid IS NULL THEN
	FOR _rec IN
		SELECT 
			 nntpserverid,
			 boardid,
			 name,
			 address,
			 port,
			 username,
			 userpass 
		FROM {databaseSchema}.{objectQualifier}nntpserver 
		WHERE boardid=i_boardid ORDER BY name
LOOP
RETURN NEXT _rec;
END LOOP; 
	ELSE
	FOR _rec IN
		SELECT 
			 nntpserverid,
			 boardid,
			 name,
			 address,
			 port,
			 username,
			 userpass  
		FROM {databaseSchema}.{objectQualifier}nntpserver 
		WHERE nntpserverid=i_nntpserverid

	LOOP
RETURN NEXT _rec;
END LOOP; 	
		END IF;
		
END;$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100;
--GO

-- Function: {databaseSchema}.{objectQualifier}nntpserver_save(integer, integer, integer, integer, integer, integer, integer)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}nntpserver_save(integer, integer, integer, integer, integer, integer, integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}nntpserver_save(
						   i_nntpserverid integer, 
						   i_boardid integer, 
						   i_name varchar, 
						   i_address varchar, 
						   i_port integer, 
						   i_username varchar, 
						   i_userpass varchar)
				  RETURNS void AS
$BODY$
BEGIN
	IF i_nntpserverid IS NULL THEN
		INSERT INTO 
		{databaseSchema}.{objectQualifier}nntpserver(name,boardid,
		address,port,username,userpass)
		VALUES(i_name,i_boardid,i_address,i_port,i_username,i_userpass);
	ELSE
		UPDATE {databaseSchema}.{objectQualifier}nntpserver SET
			name = i_name,
			address = i_address,
			port = i_port,
			username = i_username,
			userpass = i_userpass
		WHERE nntpserverid = i_nntpserverid;
		END IF;
		RETURN;
END;$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100;  
--GO

-- Function: {databaseSchema}.{objectQualifier}nntptopic_list(character)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}nntptopic_list(character);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}nntptopic_list(
						   i_thread varchar(64))
				  RETURNS SETOF {databaseSchema}.{objectQualifier}nntptopic_list_return_type AS
$BODY$DECLARE
			 _rec {databaseSchema}.{objectQualifier}nntptopic_list_return_type%ROWTYPE;
BEGIN
FOR _rec IN
	SELECT
		  nntptopicid,
		  nntpforumid,
		  thread,
		  topicid
	FROM
		{databaseSchema}.{objectQualifier}nntptopic 
	WHERE
		a.thread = i_thread
LOOP
RETURN NEXT _rec;
END LOOP; 
		
END;
$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100 ROWS 1000; 
--GO

-- Function: {databaseSchema}.{objectQualifier}nntptopic_savemessage(integer, varchar, text, integer, varchar, varchar, timestamp, varchar)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}nntptopic_savemessage(integer, varchar, text, integer, varchar, varchar, timestamp, varchar);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}nntptopic_addmessage(
						   i_nntpforumid integer, 
						   i_topic varchar(255), 
						   i_body text, 
						   i_userid integer, 
						   i_username varchar(255), 
						   i_ip varchar(100), 
						   i_posted timestamp, 
						   i_externalmessageid varchar(255),
						   i_referencemessageid varchar(255),
						   i_utctimestamp timestamp)
				  RETURNS void AS
$BODY$DECLARE
			 ici_forumid	integer;
			 ici_topicid	integer;
			 ici_MessageID	integer;                 
			 ici_replyto	integer;
			 varchardummy varchar(128);
BEGIN 	
 
	SELECT forumid INTO ici_forumid 
	FROM {databaseSchema}.{objectQualifier}nntpforum WHERE nntpforumid=i_nntpforumid;

	select  topicid, messageid 
		INTO ici_topicid, ici_ReplyTo
		from 
		{databaseSchema}.{objectQualifier}message 
		where externalmessageid = i_referencemessageid;

	-- referenced message doesn't exist
	IF (ici_topicid is null and i_referencemessageid IS NULL and not exists(select 1 from {databaseSchema}.{objectQualifier}message where  externalmessageid = i_externalmessageid)) then     
	  -- thread doesn't exists
	  
		INSERT INTO {databaseSchema}.{objectQualifier}topic(forumid,
		userid,username,userdisplayname,posted,topic,views,priority,numposts,LastMessageFlags)
		VALUES(ici_forumid,i_userid,i_username,i_username,i_posted,i_topic,0,0,0,17) 
		returning topicid into ici_topicid;   
	 

		INSERT INTO {databaseSchema}.{objectQualifier}nntptopic(nntpforumid,thread,topicid)
		VALUES (i_nntpforumid,'',ici_topicid);  
	END IF;  

	IF ici_topicid IS NOT NULL THEN
		SELECT {databaseSchema}.{objectQualifier}message_save(ici_topicid, i_userid, i_body, i_username, i_ip, i_posted, ici_replyto, NULL, i_externalmessageid, i_referencemessageid, NULL, 17, i_utctimestamp)
		INTO ici_MessageID;

		 -- update user if this is a count post forum 
	IF EXISTS(SELECT 1 FROM {databaseSchema}.{objectQualifier}forum
	 WHERE forumid=ici_ForumID AND (flags & 4)=0 limit 1) THEN 	
		UPDATE {databaseSchema}.{objectQualifier}user
		SET numposts=numposts+1 WHERE userid=i_userid;
	END IF;
	
	-- update topic last post
		UPDATE {databaseSchema}.{objectQualifier}topic SET 
		lastposted		= i_posted,
		lastmessageid	= ici_MessageID,
		lastuserid		= i_userid,
		lastusername	= i_username,
		lastuserdisplayname	= i_username
	WHERE topicid=ici_topicid;	
	-- update forum 
	UPDATE {databaseSchema}.{objectQualifier}forum SET
		lastposted		= i_posted,
		lasttopicid	= ici_topicid,
		lastmessageid	= ici_MessageID,
		lastuserid		= i_userid,
		lastusername	= i_username,
		lastuserdisplayname	= i_username
	WHERE forumid=ici_ForumID; 
		
	-- PERFORM {databaseSchema}.{objectQualifier}topic_updatelastpost(ici_ForumID,ici_topicid); 
	-- PERFORM {databaseSchema}.{objectQualifier}forum_updatelastpost(ici_ForumID);

	END IF;   
END;
$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100;  
--GO

-- Function: {databaseSchema}.{objectQualifier}pmessage_archive(integer)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}pmessage_archive(integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}pmessage_archive(
						   i_userpmessageid integer)
				  RETURNS void AS
$BODY$
BEGIN
	-- set IsArchived boolean 
	UPDATE {databaseSchema}.{objectQualifier}userpmessage 
	SET flags = (flags | 4)
	WHERE 
userpmessageid = i_userpmessageid 
AND CAST(CAST(SIGN(flags & 4) AS char(1))AS boolean) IS FALSE;
 --IsArchived computed value
 END;$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100;  
--GO

-- Function: {databaseSchema}.{objectQualifier}pmessage_delete(integer, boolean)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}pmessage_delete(integer, boolean);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}pmessage_delete(
						   i_userpmessageid integer, 
						   i_fromoutbox boolean)
				  RETURNS void AS
$BODY$DECLARE 
			 ici_PMessageID integer;
			 ici_msgcount integer;
BEGIN 	 
 
	SELECT pmessageid INTO ici_PMessageID 
	FROM {databaseSchema}.{objectQualifier}userpmessage 
	where userpmessageid = i_userpmessageid LIMIT 1;
 
 IF ( i_fromoutbox IS TRUE AND EXISTS(SELECT 1 FROM {databaseSchema}.{objectQualifier}userpmessage WHERE userpmessageid = i_userpmessageid AND isinoutbox IS TRUE) ) THEN
		-- set IsInOutbox bit which will remove it from the senders outbox
		UPDATE {databaseSchema}.{objectQualifier}userpmessage SET flags = flags # 2 WHERE userpmessageid = i_userpmessageid;
	END IF;
	
	IF ( i_fromoutbox IS FALSE ) THEN
	-- The pmessage is in archive but still is in sender outbox  
	IF ( EXISTS(SELECT 1 FROM {databaseSchema}.{objectQualifier}userpmessage WHERE userpmessageid = i_userpmessageid AND SIGN(flags & 2) = 1 AND SIGN(flags & 4) = 1 AND SIGN(flags & 8) = 0) ) THEN
	-- Remove archive flag and set IsDeleted flag
	UPDATE {databaseSchema}.{objectQualifier}userpmessage SET flags = flags # 4  WHERE userpmessageid = i_userpmessageid;	
	END IF;
		-- set is deleted...
		UPDATE {databaseSchema}.{objectQualifier}userpmessage SET flags = flags # 8 WHERE userpmessageid = i_userpmessageid;
	END	 IF;
	
	-- see IF there are no longer references to this PM.
	IF ( EXISTS(SELECT 1 FROM {databaseSchema}.{objectQualifier}userpmessage WHERE userpmessageid = i_userpmessageid AND isinoutbox IS FALSE AND isdeleted IS TRUE ) ) THEN
		-- delete
		DELETE FROM {databaseSchema}.{objectQualifier}userpmessage WHERE pmessageid = ici_PMessageID;
		DELETE FROM {databaseSchema}.{objectQualifier}pmessage WHERE pmessageid = ici_PMessageID;
	END IF;	
	
 
 /* set IsDeleted bit which will remove it from the senders outbox	
	UPDATE {databaseSchema}.{objectQualifier}userpmessage SET flags = (flags # 8) 		
		WHERE userpmessageid = i_userpmessageid;
	
	SELECT COUNT (UserPMessageID) INTO ici_msgcount 
	FROM {databaseSchema}.{objectQualifier}userpmessage  
	WHERE  SIGN(Flags & 8) = 0;
	
	IF (ici_msgcount =0 OR ici_msgcount IS NULL) THEN 
	
		DELETE FROM {databaseSchema}.{objectQualifier}userpmessage 
		WHERE pmessageid = ici_PMessageID;
		DELETE FROM {databaseSchema}.{objectQualifier}pmessage 
		WHERE pmessageid = ici_PMessageID;
		
	END IF; */	

 END;
$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100;  
--GO

-- Function: {databaseSchema}.{objectQualifier}pmessage_info()

-- DROP FUNCTION {databaseSchema}.{objectQualifier}pmessage_info();

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}pmessage_info()
				  RETURNS SETOF {databaseSchema}.{objectQualifier}pmessage_info_return_type AS
$BODY$DECLARE
			 _rec {databaseSchema}.{objectQualifier}pmessage_info_return_type%ROWTYPE;
BEGIN
FOR _rec IN
	SELECT
		(SELECT COUNT(1) FROM {databaseSchema}.{objectQualifier}userpmessage 
		WHERE isread IS NOT FALSE   AND NOT isdeleted  ) AS NumRead,
		(SELECT COUNT(1) FROM {databaseSchema}.{objectQualifier}userpmessage
		WHERE isread IS FALSE   AND NOT isdeleted) AS NumUnread,
		(SELECT COUNT(1) FROM {databaseSchema}.{objectQualifier}userpmessage
		WHERE NOT isdeleted) AS NumTotal
LOOP
RETURN NEXT _rec;
END LOOP; 

END;$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100 ROWS 1;   
--GO

-- Function: {databaseSchema}.{objectQualifier}pmessage_list(integer, integer, integer)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}pmessage_list(integer, integer, integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}pmessage_list(
						   i_fromuserid integer,
						   i_touserid integer,                             
						   i_userpmessageid integer)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}pmessage_list_return_type AS
$BODY$DECLARE
			 _rec {databaseSchema}.{objectQualifier}pmessage_list_return_type%ROWTYPE;
BEGIN
	  FOR _rec IN
	  SELECT
			a.pmessageid,
			a.replyto,
			b.userpmessageid,
			a.fromuserid,
			d.name AS fromuser,
			b.userid AS touserid,
			c.name AS touser,
			a.created,
			a.subject,
			a.body,
			a.flags,
			b.isread,
			b.isinoutbox,
			b.isarchived,
			b.isdeleted,
			b.isreply
	  FROM {databaseSchema}.{objectQualifier}pmessage a
	  JOIN {databaseSchema}.{objectQualifier}userpmessage b 
	  ON a.pmessageid = b.pmessageid
	  JOIN {databaseSchema}.{objectQualifier}user c 
	  ON b.userid = c.userid
	  JOIN {databaseSchema}.{objectQualifier}user d 
	  ON a.fromuserid = d.userid
	  WHERE	((i_userpmessageid IS NOT NULL 
			AND b.userpmessageid=i_userpmessageid)
			   OR
			   (i_touserid   IS NOT NULL AND b.userid = i_touserid)
			   OR
			   (i_fromuserid IS NOT NULL AND a.fromuserid =i_fromuserid)) 				 
	  ORDER BY a.created DESC
	  LOOP
		  RETURN NEXT _rec;
	  END LOOP; 		
END;
$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100 ROWS 1000;
--GO

-- Function: {databaseSchema}.{objectQualifier}pmessage_markread(integer)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}pmessage_markread(integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}pmessage_markread(
						   i_userpmessageid integer)
				  RETURNS void AS
'UPDATE {databaseSchema}.{objectQualifier}userpmessage 
	SET flags = flags | 1 	
	WHERE userpmessageid = $1 
	AND CAST(CAST(SIGN(flags & 1) AS char(1))AS boolean)  IS FALSE;'
  LANGUAGE 'sql' VOLATILE SECURITY DEFINER
  COST 100; 
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}pmessage_prune(
						   i_daysread integer, 
						   i_daysunread integer, 
						   i_utctimestamp timestamp)
				  RETURNS void AS
$BODY$
BEGIN
	DELETE FROM {databaseSchema}.{objectQualifier}userpmessage 	 
	WHERE {databaseSchema}.{objectQualifier}int_to_bool_helper(SIGN(flags & 1))  IS NOT TRUE
	AND current_date-CAST((SELECT x.created FROM
{databaseSchema}.{objectQualifier}pmessage x 
WHERE x.pmessageid={databaseSchema}.{objectQualifier}userpmessage.pmessageid) AS date) >i_daysread;
-- IsRead computed column 
	DELETE FROM {databaseSchema}.{objectQualifier}userpmessage
	WHERE {databaseSchema}.{objectQualifier}int_to_bool_helper(SIGN(flags & 1))  IS FALSE
	AND  current_date-CAST((SELECT x.created FROM
{databaseSchema}.{objectQualifier}pmessage x WHERE 
x.pmessageid={databaseSchema}.{objectQualifier}userpmessage.pmessageid) AS date) >
i_daysunread;
 
	DELETE FROM {databaseSchema}.{objectQualifier}pmessage
	WHERE NOT EXISTS(SELECT 1 FROM {databaseSchema}.{objectQualifier}userpmessage x WHERE
		x.pmessageid={databaseSchema}.{objectQualifier}pmessage.pmessageid);
END;
$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100; 
--GO

-- Function: {databaseSchema}.{objectQualifier}pmessage_save(integer, integer, varchar, text, integer)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}pmessage_save(integer, integer, varchar, text, integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}pmessage_save(
						   i_fromuserid integer, 
						   i_touserid integer, 
						   i_subject varchar, 
						   i_body text, 
						   i_flags integer,
						   i_replyto integer,
						   i_utctimestamp timestamp)
				  RETURNS void AS
$BODY$DECLARE
			 ici_PMessageID integer;
			 ici_userid integer;
BEGIN	

	IF i_replyto is null or i_replyto < 0 then
		INSERT INTO {databaseSchema}.{objectQualifier}pmessage(fromuserid,created,subject,body,flags)
		VALUES(i_fromuserid,i_utctimestamp,i_subject,i_body, COALESCE(i_flags,0));    
	else
	   INSERT INTO {databaseSchema}.{objectQualifier}pmessage(fromuserid,created,subject,body,flags,replyto)
		VALUES(i_fromuserid,i_utctimestamp,i_subject,i_body, COALESCE(i_flags,0),i_replyto);
		UPDATE {databaseSchema}.{objectQualifier}userpmessage SET isreply = TRUE WHERE pmessageid = i_replyto;
	end if;
	SELECT CURRVAL(pg_get_serial_sequence('{databaseSchema}.{objectQualifier}pmessage','pmessageid')) INTO ici_PMessageID;  
		
 
	IF (i_touserid = 0) THEN 	
		INSERT INTO {databaseSchema}.{objectQualifier}userpmessage
(userid,pmessageid,flags)
		SELECT
		a.userid,
				ici_PMessageID,
				2
		FROM
				{databaseSchema}.{objectQualifier}user a
				JOIN {databaseSchema}.{objectQualifier}usergroup b 
								on b.userid=a.userid
				JOIN {databaseSchema}.{objectQualifier}group c 
								on c.groupid=b.groupid 
								WHERE
				(c.flags & 2)=0 AND
				c.boardid=(SELECT boardid from
							   {databaseSchema}.{objectQualifier}user x 
							  WHERE  x.userid=i_fromuserid) 
							  AND a.userid<>i_fromuserid
						GROUP BY a.userid; 	
	ELSE
	
		INSERT INTO {databaseSchema}.{objectQualifier}userpmessage
(userid,pmessageid,flags) 
				VALUES (i_touserid,
						ici_PMessageID,
						2);
	END IF;
END;$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100;
 --GO


-- Function: {databaseSchema}.{objectQualifier}poll_remove(integer)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}poll_remove(integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}poll_remove(
						   i_pollgroupid integer,
						   i_pollid integer,
						   i_boardid integer,
						   i_removecompletely boolean,
						   i_removeeverywhere boolean)
				  RETURNS void AS
$BODY$DECLARE
			 ici_groupcount int;
BEGIN
	IF i_removecompletely IS TRUE THEN
	
	/*DELETE vote records first*/
	DELETE FROM {databaseSchema}.{objectQualifier}pollvote WHERE pollid = i_pollid;
	/*DELETE choices first*/
	DELETE FROM {databaseSchema}.{objectQualifier}choice WHERE pollid = i_pollid;
	/*DELETE it from topic itself*/
	-- delete poll
	Update  {databaseSchema}.{objectQualifier}poll 
	set pollgroupid = NULL 
	where pollid = i_pollid;

	delete from  {databaseSchema}.{objectQualifier}poll  where pollid = i_pollid; 	
	IF  NOT EXISTS (SELECT 1 FROM {databaseSchema}.{objectQualifier}poll  where pollgroupid = i_pollgroupid) THEN 	
			  
				   Update {databaseSchema}.{objectQualifier}topic set pollid = NULL where pollid = i_pollgroupid; 
				   Update {databaseSchema}.{objectQualifier}forum set pollgroupid = NULL where pollgroupid = i_pollgroupid;
				   Update {databaseSchema}.{objectQualifier}category set pollgroupid = NULL where pollgroupid = i_pollgroupid; 	
	   
		 
		DELETE FROM  {databaseSchema}.{objectQualifier}pollgroupcluster WHERE pollgroupid = i_pollgroupid;
		end  IF;	
	else   
	Update {databaseSchema}.{objectQualifier}poll set PollGroupID = NULL where pollid = i_pollid; 		                         
	END IF;
 END;
$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100;  
--GO
-- Function: {databaseSchema}.{objectQualifier}poll_stats(integer)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}poll_stats(integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}poll_stats(i_pollid integer)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}poll_stats_return_type AS
$BODY$DECLARE
			 iciCase integer;
			 iciCount integer;
			 iciStats integer;
			 _rec {databaseSchema}.{objectQualifier}poll_stats_return_type%ROWTYPE;
BEGIN 
SELECT SUM(x.votes) INTO iciCase
FROM   {databaseSchema}.{objectQualifier}choice x
WHERE  x.pollid = i_pollid;

IF iciCase =0 THEN iciCount :=1;
ELSE
iciCount :=iciCase ;
END IF;

FOR _rec IN
SELECT 
a.pollid AS PollID,
b.question AS Question,
b.closes AS Closes,
b.userid,
a.objectpath,
a.mimetype,
b.objectpath,
b.mimetype,
a.choiceid AS ChoiceID,
a.choice AS Choice,
a.votes AS Votes,
(pg.Flags & 2 = 2)::boolean, 
(b.Flags & 4 = 4)::boolean, 	
(b.Flags & 8 = 8)::boolean,
(b.Flags & 16 = 16)::boolean,
(b.Flags & 32 = 32)::boolean,
iciCase AS Total,
(SELECT 100*a.votes/iciCount) AS Stats
FROM   {databaseSchema}.{objectQualifier}choice a
JOIN
{databaseSchema}.{objectQualifier}poll b ON b.PollID = a.PollID
INNER JOIN  
{databaseSchema}.{objectQualifier}pollgroupcluster pg ON pg.pollgroupid = b.pollgroupid	
WHERE  
b.pollid = a.pollid
AND b.pollid = i_pollid
LOOP
RETURN NEXT _rec;
END LOOP;

END;$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100
  ROWS 1000;
--GO

-- Function: {databaseSchema}.{objectQualifier}pollgroup_stats(integer)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}pollgroup_stats(integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}pollgroup_stats(
						   i_pollgroupid integer)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}pollgroup_stats_return_type AS
$BODY$DECLARE
			 iciCase integer;
			 iciCount integer;
			 iciStats integer;
			 _rec {databaseSchema}.{objectQualifier}pollgroup_stats_return_type%ROWTYPE;
BEGIN 

FOR _rec IN
SELECT 
pg.userid, 
a.pollid AS PollID,
b.pollgroupid, 
b.question AS Question,
b.closes AS Closes,
a.choiceid AS ChoiceID,
a.choice AS Choice,
a.votes AS Votes,
a.objectpath,
a.mimetype,
b.objectpath,
b.mimetype,
(pg.Flags & 2 = 2)::boolean, 
(b.Flags & 4 = 4)::boolean, 	
(b.Flags & 8 = 8)::boolean,
(b.Flags & 16 = 16)::boolean,
(b.Flags & 32 = 32)::boolean,
iciCount AS Total,
iciStats AS Stats
FROM   {databaseSchema}.{objectQualifier}choice a
JOIN
{databaseSchema}.{objectQualifier}poll b ON b.PollID = a.PollID
INNER JOIN  
{databaseSchema}.{objectQualifier}pollgroupcluster pg ON pg.pollgroupid = b.pollgroupid	
WHERE  
pg.pollgroupid = i_pollgroupid
ORDER BY pg.pollgroupid, a.choiceid
LOOP
_rec."Total" := (SELECT SUM(x.votes)
FROM   {databaseSchema}.{objectQualifier}choice x
WHERE  x.pollid = _rec."PollID");

_rec."Total"  := CASE WHEN _rec."Total" = 0 THEN 1 ELSE _rec."Total" END;

_rec."Stats" := 100 * _rec."Votes" / _rec."Total"; 
RETURN NEXT _rec;
END LOOP;
END;$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100
  ROWS 1000;
--GO

-- Function: {databaseSchema}.{objectQualifier}pollgroup_votecheck(integer)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}pollgroup_votecheck(integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}pollgroup_votecheck(
						   i_pollgroupid integer,
						   i_userid integer,
						   i_remoteip varchar(39))
				  RETURNS SETOF {databaseSchema}.{objectQualifier}pollgroup_votecheck_return_type AS
$BODY$DECLARE
			 _rec {databaseSchema}.{objectQualifier}pollgroup_votecheck_return_type%ROWTYPE;
BEGIN 
	IF i_userid IS NULL THEN	  
		IF i_remoteip IS NOT NULL THEN		
			-- check by remote IP
			FOR _rec IN 
			SELECT pv.pollid, 
			pv.choiceid, 
			usr.name as UserName 
			FROM {databaseSchema}.{objectQualifier}pollvote pv
			JOIN {databaseSchema}.{objectQualifier}user usr ON usr.userid = pv.userid
			WHERE (pv.remoteip = i_remoteip) AND (pv.pollid IN 
			(SELECT p.pollid FROM {databaseSchema}.{objectQualifier}poll p
			WHERE p.pollgroupid = i_pollgroupid)) 
			 
			LOOP
			RETURN NEXT _rec;
			END LOOP;	
		ELSE	
			FOR _rec IN 
			SELECT pv.pollid, pv.choiceid, usr.name as UserName FROM {databaseSchema}.{objectQualifier}pollvote pv
			JOIN {databaseSchema}.{objectQualifier}user usr ON usr.userid = pv.userid
			WHERE (i_remoteip IS NULL OR pv.remoteip = i_remoteip) AND
			(pv.pollid IN ( SELECT p.pollid FROM {databaseSchema}.{objectQualifier}poll p
			WHERE p.pollgroupid = i_pollgroupid)) 			
			LOOP
			RETURN NEXT _rec;
			END LOOP;	
		END IF;	 
	ELSE  
	
		FOR _rec IN 
		SELECT pv.pollid,
		pv.choiceid, 
		usr.Name as UserName 
		FROM {databaseSchema}.{objectQualifier}pollvote pv
		JOIN {databaseSchema}.{objectQualifier}user usr ON usr.userid = pv.userid
		WHERE ((pv.userid = i_userid OR pv.remoteip = i_remoteip)  AND (pv.pollid IN (SELECT p.pollid FROM {databaseSchema}.{objectQualifier}poll p 
		WHERE p.pollgroupid = i_pollgroupid)))  
		LOOP
		RETURN NEXT _rec;
		END LOOP;	
	END IF;	

END;$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100
  ROWS 1000;
--GO

-- Function: {databaseSchema}.{objectQualifier}poll_update(integer, varchar, timestamp )

-- DROP FUNCTION {databaseSchema}.{objectQualifier}poll_update(integer, varchar, timestamp );

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}poll_update(
						   i_pollid integer,
						   i_question varchar,
						   i_closes timestamp,
						   i_questionobjectpath varchar,
						   i_questionmimetype varchar,
						   i_isbounded boolean,
						   i_isclosedbounded boolean,
						   i_allowmultiplechoices boolean,
						   i_showvoters boolean,
						   i_allowskipvote boolean)
				  RETURNS void AS
$BODY$DECLARE
			 ici_pgid integer;
			 ici_flags integer;
BEGIN
		update {databaseSchema}.{objectQualifier}poll
		set flags = 0 where pollid = i_pollid AND flags IS NULL;

		SELECT flags INTO ici_flags FROM {databaseSchema}.{objectQualifier}poll		
		where pollid = i_pollid;

		-- is closed bound flag
		ici_flags := (CASE				
		WHEN i_isclosedbounded IS TRUE AND (ici_flags & 4) <> 4 THEN ici_flags | 4		
		WHEN i_isclosedbounded IS NOT TRUE AND (ici_flags & 4) = 4  THEN ici_flags # 4
		ELSE ici_flags END);

		-- allow multiple choices flag
		ici_flags := (CASE				
		WHEN i_allowmultiplechoices IS TRUE AND (ici_flags & 8) <> 8 THEN ici_flags | 8		
		WHEN i_allowmultiplechoices IS NOT TRUE AND (ici_flags & 8) = 8  THEN ici_flags # 8
		ELSE ici_flags END);
		
		-- show voters flag
		ici_flags := (CASE				
		WHEN i_showvoters IS TRUE AND (ici_flags & 16) <> 16 THEN ici_flags | 16		
		WHEN i_showvoters IS NOT TRUE AND (ici_flags & 16) = 16  THEN ici_flags # 16
		ELSE ici_flags END);

		-- allow skip vote flag
		ici_flags := (CASE				
		WHEN i_allowskipvote IS TRUE AND (ici_flags & 32) <> 8 THEN ici_flags | 32		
		WHEN i_allowskipvote IS NOT TRUE AND (ici_flags & 32) = 32  THEN ici_flags # 32
		ELSE ici_flags END);


UPDATE {databaseSchema}.{objectQualifier}poll
		SET question = i_question,
		closes = i_closes,
		objectpath  = i_questionobjectpath,
		mimetype = i_questionmimetype,
		flags = ici_flags
		WHERE pollid = i_pollid;

 SELECT  pollgroupid INTO ici_pgid FROM {databaseSchema}.{objectQualifier}poll
	  where pollid = i_pollid;
   
	update {databaseSchema}.{objectQualifier}pollgroupcluster
		set flags	= (CASE 
		WHEN i_isbounded IS TRUE AND (flags & 2) <> 2 THEN flags | 2 		
		WHEN i_isbounded IS NOT TRUE AND (flags & 2) = 2 THEN flags # 2 		
		ELSE flags END)		
		where pollgroupid = ici_pgid;

END;$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100;
--GO

-- Function: {databaseSchema}.{objectQualifier}pollvote_check(integer, integer, varchar)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}pollvote_check(integer, integer, varchar);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}pollvote_check(
						   i_pollid integer, 
						   i_userid integer, 
						   i_remoteip varchar)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}pollvote_check_return_type AS
$BODY$DECLARE
			 _rec {databaseSchema}.{objectQualifier}pollvote_check_return_type%ROWTYPE;
BEGIN
	IF i_userid IS NULL THEN 	
		IF i_remoteip IS NOT NULL THEN 		
			/*check by remote IP*/
	   FOR _rec IN
			SELECT pollvoteid FROM {databaseSchema}.{objectQualifier}pollvote
						WHERE pollid = i_pollid AND remoteip = i_remoteip
LOOP
RETURN NEXT _rec;
END LOOP;
		END IF;
	ELSE
		/*check by userid or remote IP*/
	   FOR _rec IN
		  SELECT pollvoteid FROM {databaseSchema}.{objectQualifier}pollvote
				   WHERE pollid = i_pollid AND (userid = i_userid OR remoteip = i_remoteip)
LOOP
RETURN NEXT _rec;
END LOOP;
  END IF;
-- RETURN;
END;$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100;
--GO

-- Function: {databaseSchema}.{objectQualifier}post_last10user(integer, integer, integer)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}post_last10user(integer, integer, integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}post_alluser(
						   i_boardid integer, 
						   i_userid integer, 
						   i_pageuserid integer, 
						   i_topcount integer)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}post_alluser_type AS
$BODY$DECLARE
			 _rec {databaseSchema}.{objectQualifier}post_alluser_type%ROWTYPE;
			 _counter integer:=1;
BEGIN
		FOR _rec IN
		SELECT DISTINCT a.posted,
				c.topic AS Subject,
				a.messageid,
				a.message,
				a.ip,
				a.userid,
				a.flags,
				COALESCE(a.username,b.name) AS UserName,
				COALESCE(a.userdisplayname,b.displayname) AS UserName,
						b.signature,
						c.topicid,
						d.forumid,
						1 as ReadAccess
		FROM     {databaseSchema}.{objectQualifier}message a
				 JOIN {databaseSchema}.{objectQualifier}user b
				   ON b.userid = a.userid
				 JOIN {databaseSchema}.{objectQualifier}topic c
				   ON c.topicid = a.topicid
				 JOIN {databaseSchema}.{objectQualifier}forum d
				   ON d.forumid = c.forumid
				 JOIN {databaseSchema}.{objectQualifier}category e
				   ON e.categoryid = d.categoryid
			   /*  JOIN {databaseSchema}.{objectQualifier}vaccess x
				   ON x.forumid = d.forumid */
		WHERE    a.userid = i_userid
	   /* AND x.userid = i_pageuserid
		AND x.readaccess <> 0 */
		AND e.boardid = i_boardid
		AND (a.flags & 24) = 16
		AND (c.flags & 8) = 0
		ORDER BY a.posted DESC
LOOP
EXIT WHEN _counter >= i_topcount;
IF (SELECT "ReadAccess" FROM {databaseSchema}.{objectQualifier}vaccess_combo(i_pageuserid, _rec."ForumID") LIMIT 1) > 0 OR (_rec."Flags" & 2) = 0 THEN
RETURN NEXT _rec;
_counter:=_counter+1;
END IF;
END LOOP;

END;
$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100; 
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}post_list(
						   i_topicid integer,						
						   i_pageuserid integer,
						   i_authoruserid integer,
						   i_updateviewcount smallint,
						   i_showdeleted boolean,
						   i_stylednicks boolean,
						   i_showreputation boolean,
						   i_sinceposteddate timestamp,
						   i_toposteddate timestamp,
						   i_sinceediteddate timestamp,
						   i_toediteddate timestamp,
						   i_pageindex integer,
						   i_pagesize integer,
						   i_sortposted integer,
						   i_sortedited  integer,
						   i_sortposition  integer,
						   i_showthanks boolean,
						   i_messageposition integer,
						   i_messageid integer,
						   i_lastread timestamp,
						   i_utctimestamp timestamp)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}post_list_type AS
$BODY$DECLARE
			 ici_sortposted integer := i_sortposted;
			 ici_pageindex integer := i_pageindex;
			 _rec {databaseSchema}.{objectQualifier}post_list_type%ROWTYPE;
			 ici_post_totalrowsnumber integer:=0;
			 ici_firstselectrownum integer:= 0;
			 ici_firstselectposted timestamp ;
			 ici_firstselectedited timestamp ;
			 ici_floor decimal :=0;
			 ici_ceiling decimal := 0;
			 ici_newpageindex integer;
			 ici_offset integer:=0;
			 ici_pagecorrection integer:=0;
			 ici_pageshift integer:=0;
			 ici_counter integer := 0;
			 ici_retcount integer := 0;
BEGIN

IF (ici_sortposted IS NULL) THEN
ici_sortposted := 2;
END IF;

IF (ici_pageindex IS NULL) THEN
ici_pageindex := 1;
END IF;

	/* how set nocount on*/
	IF i_updateviewcount > 0 THEN
		UPDATE {databaseSchema}.{objectQualifier}topic 
		SET views = views + 1 WHERE topicid = i_topicid;
	END IF;

	  -- find total returned count
		select
		COUNT(m.messageid) into ici_post_totalrowsnumber
	from
		{databaseSchema}.{objectQualifier}message m
	where
		m.topicid = i_topicid		
		-- is approved
		AND (m.flags & 16) = 16 
		-- is deleted
		AND (i_showdeleted IS TRUE OR (m.flags & 8) <> 8 OR (i_authoruserid > 0 AND m.UserID = i_authoruserid))
		AND m.posted BETWEEN
		 i_sinceposteddate AND i_toposteddate;
		 /*
		AND 
		m.Edited >= SinceEditedDate
		*/
		-- SELECT last page	
   IF (i_messageposition > 0) THEN     
   -- round to ceiling   
   ici_ceiling := CEILING((ici_post_totalrowsnumber::decimal)/i_pagesize); 
   -- round to floor
   ici_floor := FLOOR((ici_post_totalrowsnumber::decimal)/i_pagesize);
   -- number of messages on the last page @post_totalrowsnumber - @floor*i_pagesize
   ici_pageshift := i_messageposition - (ici_post_totalrowsnumber - ici_floor*i_pagesize);            
			   
   IF (ici_pageshift < 0) THEN  
   ici_pageshift := 0;
   END IF;

   IF (ici_pageshift <= 0) THEN   
   ici_pageshift := 0;
   else 
   ici_pageshift := CEILING((ici_pageshift::decimal)/i_pagesize);  
   END IF; 

   ici_pageindex := ici_ceiling - ici_pageshift; 
   IF (ici_ceiling != ici_floor) THEN
   ici_pageindex := ici_pageindex - 1;
   END IF;	      

   ici_firstselectrownum := (ici_pageindex) * i_pagesize + 1; 		  
   else 
   ici_pageindex := ici_pageindex+1;
   ici_firstselectrownum := (ici_pageindex - 1) * i_pagesize + 1;
   END IF; 
  
   /* find first SELECT edrowid 
   IF (ici_firstselectrownum > 0)   
   set rowcount ici_firstselectrownum
   else
   should not be 0
   set rowcount 1 */

	SELECT 		
		m.posted,
		m.edited
	into ici_firstselectposted, ici_firstselectedited
	from	
		{databaseSchema}.{objectQualifier}message m 	
	where
		m.topicid = i_topicid
		-- is approved
		AND (m.flags & 16)  = 16  
		-- is deleted
	   AND (i_showdeleted IS TRUE OR (m.flags & 8) <> 8 OR (i_authoruserid > 0 AND m.UserID = i_authoruserid))
		AND m.posted BETWEEN
		 i_sinceposteddate AND i_toposteddate
		 /*
		AND m.Edited > @SinceEditedDate
		*/
		ORDER BY 		
		 (case 
		when i_sortposition = 1 THEN m.position end) ASC,	
		(case 
		when ici_sortposted = 2 THEN m.posted end) DESC,
		(case 
		when ici_sortposted = 1 THEN m.posted end) ASC, 
		(case 
		when i_sortedited = 2 THEN m.edited end) DESC,
		(case 
		when i_sortedited = 1 THEN m.edited end) ASC;  	

	FOR _rec IN
	SELECT
		d.topicid AS TopicID,
		d.topic,
		d.priority,
		d.description,
		d.status,
		d.styles,
		d.pollid,
		d.userid AS TopicOwnerID,
		d.flags AS TopicFlags,
		g.flags AS ForumFlags,
		m.messageid AS MessageID,
		m.posted AS Posted, 	
		m.message,
		m.description as MessageDescription,
		m.userid,
		m.position,
		m.indent,
		m.ip,
		m.flags,
		m.editreason,
		m.ismoderatorchanged,
		(m.flags & 8) = 8,
		m.deletereason,
		m.blogpostid,
		m.externalmessageid,
		m.referencemessageid,
		COALESCE(m.username,b.name) AS UserName,
		COALESCE(m.userdisplayname,b.displayname) AS UserDisplayName,
		b.suspended,
		b.joined,
		b.avatar,
		b.signature,
		b.numposts AS Posts,
		b.points,
		(CASE WHEN i_showreputation IS TRUE THEN CAST(COALESCE((select  votedate from {databaseSchema}.{objectQualifier}reputationvote repVote where repVote.ReputationToUserID=b.UserID and repVote.ReputationFromUserID=i_pageuserid limit 1), TIMESTAMP '-infinity') as timestamp) ELSE i_utctimestamp END) AS ReputationVoteDate,		
		COALESCE(((b.Flags & 4) = 4),FALSE),
		d.views,
		d.forumid,
		c.name AS RankName,		
		c.rankimage,
		c.style AS RankStyle,
		(case(i_stylednicks)
			when true THEN  b.userstyle
			else ''	 end), 
		COALESCE(m.edited,m.posted) AS Edited,
		m.editedby,
		COALESCE((SELECT 1 FROM {databaseSchema}.{objectQualifier}attachment x 
		WHERE x.messageid=m.messageid LIMIT 1),0) AS HasAttachments,
		COALESCE((SELECT 1 FROM {databaseSchema}.{objectQualifier}user x 
		WHERE x.userid=b.userid AND x.avatarimage IS NOT NULL LIMIT 1),0)
				AS HasAvatarImage,
		ici_post_totalrowsnumber AS TotalRows,
		ici_pageindex	AS 	PageIndex,
		ici_firstselectrownum	
		FROM
		{databaseSchema}.{objectQualifier}message m
		JOIN {databaseSchema}.{objectQualifier}user b ON b.userid=m.userid
		JOIN {databaseSchema}.{objectQualifier}topic d ON d.topicid=m.topicid
		JOIN {databaseSchema}.{objectQualifier}forum g ON g.forumid=d.forumid
		JOIN {databaseSchema}.{objectQualifier}category h ON h.categoryid=g.categoryid
		JOIN {databaseSchema}.{objectQualifier}rank c ON c.rankid=b.rankid
	WHERE
		m.topicid = i_topicid
		-- is approved
		AND (m.flags & 16)  = 16  
		-- is deleted
		AND (i_showdeleted IS TRUE OR (m.flags & 8) <> 8 OR (i_authoruserid > 0 AND m.UserID = i_authoruserid))
		 AND (m.posted is null OR (m.posted is not null
		AND
		(m.posted >= (case 
		when ici_sortposted = 1 THEN
		 ici_firstselectposted end) OR m.posted <= (case 
		when ici_sortposted = 2 THEN ici_firstselectposted end) OR
		m.posted >= (case 
		when ici_sortposted = 0 THEN TIMESTAMP '-infinity' end))))	AND
		(m.posted <= i_toposteddate)	 		
		
		/* AND (m.Edited is null OR (m.Edited is not null AND
		(m.Edited >= (case 
		when @SortEdited = 1 THEN @firstselectedited end) 
		OR m.Edited <= (case 
		when @SortEdited = 2 THEN @firstselectedited end) OR
		m.Edited >= (case 
		when @SortEdited = 0 THEN 0
		end)))) */
		
	ORDER BY 		
		 (case 
		when i_sortposition = 1 THEN m.position end) ASC,	
		(case 
		when ici_sortposted = 2 THEN m.posted end) DESC,
		(case 
		when ici_sortposted = 1 THEN m.posted end) ASC, 
		(case 
		when i_sortedited = 2 THEN m.edited end) DESC,
		(case 
		when i_sortedited = 1 THEN m.edited end) ASC  	 		
			
LOOP
-- RETURN NEXT _rec;
ici_retcount := ici_retcount +1; 
IF (ici_retcount between  ici_firstselectrownum and ici_firstselectrownum+i_pagesize) THEN
RETURN NEXT _rec;
ici_counter := ici_counter + 1; 
END IF;
IF (ici_counter >= i_pagesize) THEN
EXIT;
END IF;
END LOOP; 

END;$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100 ROWS 1000;
--GO
-- Function: {databaseSchema}.{objectQualifier}post_list_reverse10(integer)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}post_list_reverse10(integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}post_list_reverse10(
						   i_topicid integer)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}post_list_reverse10_return_type AS
$BODY$DECLARE
			 _rec {databaseSchema}.{objectQualifier}post_list_reverse10_return_type%ROWTYPE;
BEGIN
	/*set nocount on*/
 -- SET TRANSACTION ISOLATION LEVEL READ COMMITTED;
FOR _rec IN 	
SELECT
		a.posted,
		d.topic AS Subject,
		a.message,
		a.userid,
		a.flags,
		COALESCE(a.username,b.name) AS UserName,
		COALESCE(a.userdisplayname,b.displayname) AS UserDisplayName,
		b.signature
	FROM
		{databaseSchema}.{objectQualifier}message a 
		inner join {databaseSchema}.{objectQualifier}user b on b.userid = a.userid
		inner join {databaseSchema}.{objectQualifier}topic d on d.topicid = a.topicid
	WHERE
		(a.flags & 24)=16 AND
		a.topicid = i_topicid
	ORDER BY
		a.posted DESC LIMIT 10
LOOP
RETURN NEXT _rec;
END LOOP; 

		-- SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
END;$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100 ROWS 1000;
--GO

-- Function: {databaseSchema}.{objectQualifier}rank_delete(integer)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}rank_delete(integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}rank_delete(
						   i_rankid integer)
				  RETURNS void AS
'DELETE from {databaseSchema}.{objectQualifier}rank 
	where rankid = $1;'
  LANGUAGE 'sql' VOLATILE SECURITY DEFINER
  COST 100;
--GO

-- Function: {databaseSchema}.{objectQualifier}rank_list(integer, integer)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}rank_list(integer, integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}rank_list(
						   i_boardid integer, 
						   i_rankid integer)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}rank_list_return_type AS
$BODY$DECLARE
			 _rec {databaseSchema}.{objectQualifier}rank_list_return_type%ROWTYPE; 
BEGIN
	IF i_rankid IS NULL THEN
		   FOR _rec IN
		SELECT
			a.rankid,
			a.boardid,
			a.name,
			a.minposts,
			a.rankimage,
			a.flags,
			a.pmlimit,
			a.style,
			a.sortorder,
			a.description,
			a.usrsigchars,
			a.usrsigbbcodes,
			a.usrsightmltags,
			a.usralbums,
			a.usralbumimages
		FROM
			{databaseSchema}.{objectQualifier}rank a
		WHERE
			a.boardid=i_boardid
		ORDER BY
			a.minposts,
			a.name
LOOP
RETURN NEXT _rec;
END LOOP;
	ELSE
			 FOR _rec IN 
		SELECT
			a.rankid,
			a.boardid,
			a.name,
			a.minposts,
			a.rankimage,
			a.flags,
			a.pmlimit,
			a.style,
			a.sortorder,
			a.description,
			a.usrsigchars,
			a.usrsigbbcodes,
			a.usrsightmltags,
			a.usralbums,
			a.usralbumimages
		FROM
			{databaseSchema}.{objectQualifier}rank a
		WHERE
			a.rankid = i_rankid
LOOP
RETURN NEXT _rec;
END LOOP;
		END IF;

END;$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100 ROWS 1000; 
--GO

-- Function: {databaseSchema}.{objectQualifier}rank_save(integer, integer, varchar, boolean, boolean, integer, varchar)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}rank_save(integer, integer, varchar, boolean, boolean, integer, varchar);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}rank_save(
						   i_rankid integer,
						   i_boardid integer,
						   i_name varchar,
						   i_isstart boolean,
						   i_isladder boolean,
						   i_isguest boolean,
						   i_minposts integer,
						   i_rankimage varchar,
						   i_pmlimit integer,
						   i_style varchar(255),
						   i_sortorder integer,
						   i_description varchar(128),
						   i_usrsigchars integer,
						   i_usrsigbbcodes	varchar(255),
						   i_usrsightmltags varchar(255),
						   i_usralbums integer,
						   i_usralbumimages integer)
				 RETURNS void AS
$BODY$DECLARE
			 ici_flags integer:=0;
			 i_MinPosts integer:=i_minposts;
			 _rankid integer := i_rankid;
BEGIN 
	IF i_isladder IS FALSE THEN  
	i_MinPosts := NULL; 
	END IF; 
	IF i_isladder IS TRUE AND i_MinPosts IS NULL THEN 
	i_MinPosts := 0; 
	END IF; 	 	
	
	 
	IF i_isstart IS NOT FALSE THEN  
	ici_flags := ici_flags | 1; 
	END IF;
	IF i_isladder IS NOT FALSE THEN  
	ici_flags := ici_flags | 2; 
	END IF;
	IF i_isguest IS NOT FALSE THEN  
	ici_flags := ici_flags | 4; 
	END IF;

	IF char_length(i_style) <= 2 THEN  
	i_style := null; 
	END IF;
	
	IF i_rankid > 0 THEN
		UPDATE {databaseSchema}.{objectQualifier}rank 
		 SET
			name = i_name,
			flags = ici_flags,
			minposts = i_MinPosts,
			rankimage = i_rankimage,
			pmlimit = i_pmlimit,
			style = i_style,
			sortorder = i_sortorder,
			description = i_description,
			usrsigchars = i_usrsigchars,
			usrsigbbcodes = i_usrsigbbcodes,
			usrsightmltags = i_usrsightmltags,
			usralbums = i_usralbums,
			usralbumimages = i_usralbumimages
		WHERE rankid = i_rankid; 	
	ELSE 
		INSERT INTO {databaseSchema}.{objectQualifier}rank(
			boardid,
			name,
			flags,
			minposts,
			rankimage,
			pmlimit, 
			style, 
			sortorder,
			description,
			usrsigchars,
			usrsigbbcodes,
			usrsightmltags,
			usralbums,
			usralbumimages)
		VALUES(
			i_boardid,
			i_name,
			ici_flags,
			i_MinPosts,
			i_rankimage,
			i_pmlimit, 
			i_style, 
			i_sortorder,
			i_description,
			i_usrsigchars,
			i_usrsigbbcodes,
			i_usrsightmltags,
			i_usralbums,
			i_usralbumimages) RETURNING rankid INTO _rankid;
	END IF;
-- group styles override user styles, don't sync if style is not present here
	IF (i_style is not null and char_length(i_style) > 2) THEN
			 PERFORM {databaseSchema}.{objectQualifier}user_savestyle(
						   null, i_rankid);
	  END IF;
	
END;

$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100; 
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}registry_list(
						   i_name varchar, 
						   i_boardid integer)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}registry_return_type AS
$BODY$DECLARE
			 _rec {databaseSchema}.{objectQualifier}registry_return_type%ROWTYPE;
BEGIN
	IF i_boardid IS NULL THEN
	
		IF i_name IS NULL OR i_name = '' THEN
		FOR _rec IN
			SELECT  registryid,name,value,boardid 
			FROM {databaseSchema}.{objectQualifier}registry 
						WHERE boardid IS NULL
LOOP
RETURN NEXT _rec;
END LOOP;
		 ELSE

		FOR _rec IN
			SELECT registryid,name,value,boardid  
			FROM {databaseSchema}.{objectQualifier}registry 
						WHERE LOWER(name) = LOWER(i_name) and boardid IS NULL
LOOP
RETURN NEXT _rec;
END LOOP;
		END IF;
	ELSE 	
		IF i_name IS NULL OR i_name = '' THEN
		FOR _rec IN
			SELECT registryid,name,value,boardid  
			FROM {databaseSchema}.{objectQualifier}registry 
						WHERE boardid=i_boardid
LOOP
RETURN NEXT _rec;
END LOOP;
		 ELSE
		FOR _rec IN
			SELECT registryid,name,value,boardid  
			FROM {databaseSchema}.{objectQualifier}registry 
						WHERE LOWER(name) = LOWER(i_Name) and boardid=i_boardid
LOOP
RETURN NEXT _rec;
END LOOP;
		END IF;
	END IF;
END;

$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100;
--GO

-- Function: {databaseSchema}.{objectQualifier}registry_save(varchar, text, integer)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}registry_save(varchar, text, integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}registry_save(
						   i_name varchar, 
						   i_value text, 
						   i_boardid integer)
				  RETURNS void AS
$BODY$
BEGIN
		
		IF i_boardid IS NULL THEN        
			IF EXISTS (SELECT 1
					   FROM   {databaseSchema}.{objectQualifier}registry
					   WHERE  Lower("name") = Lower(i_name) LIMIT 1) THEN
			UPDATE {databaseSchema}.{objectQualifier}registry
			SET    "value" = i_value
			WHERE  Lower("name") = Lower(i_name)
			AND boardid IS NULL;
			ELSE            
				INSERT INTO {databaseSchema}.{objectQualifier}registry
						   ("name",
							"value")
				VALUES     (Lower(i_name),
							i_value);
			END IF;        
		ELSE        
			IF EXISTS (SELECT 1
					   FROM   {databaseSchema}.{objectQualifier}registry
					   WHERE  Lower("name") = Lower(i_name)
					   AND boardid = i_boardid LIMIT 1) THEN
			UPDATE {databaseSchema}.{objectQualifier}registry
			SET    "value" = i_value
			WHERE  Lower("name") = Lower(i_name)
			AND boardid = i_boardid;
			ELSE            
				INSERT INTO {databaseSchema}.{objectQualifier}registry
						   ("name",
							"value",
							boardid)
				VALUES     (Lower(i_name),
							i_value,
							i_boardid);
			END IF;        
		END IF;
		RETURN;
	END;$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100; 
--GO

-- Function: {databaseSchema}.{objectQualifier}replace_words_delete(integer)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}replace_words_delete(integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}replace_words_delete(
						   i_id integer)
				  RETURNS void AS
'DELETE FROM {databaseSchema}.{objectQualifier}replace_words 
WHERE id = $1;'
  LANGUAGE 'sql' VOLATILE SECURITY DEFINER
  COST 100; 
--GO

-- Function: {databaseSchema}.{objectQualifier}replace_words_list(integer, integer)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}replace_words_list(integer, integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}replace_words_list(
						   i_boardid integer, 
						   i_id integer)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}replace_words_list_select AS
$BODY$DECLARE
			 _rec {databaseSchema}.{objectQualifier}replace_words_list_select%ROWTYPE;
BEGIN
	IF (i_id IS NOT NULL AND i_id <> 0) THEN
FOR _rec IN
		SELECT id, boardid, badword,goodword 
		FROM {databaseSchema}.{objectQualifier}replace_words
		WHERE boardid = i_boardid AND id = i_id
LOOP
RETURN NEXT _rec;
EXIT;
END LOOP;
	ELSE
FOR _rec IN
		SELECT id, boardid, badword,goodword  FROM {databaseSchema}.{objectQualifier}replace_words WHERE boardid = i_boardid
LOOP
RETURN NEXT _rec;
END LOOP;
END IF;
 END;$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100;
--GO

-- Function: {databaseSchema}.{objectQualifier}replace_words_save(integer, integer, varchar, varchar)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}replace_words_save(integer, integer, varchar, varchar);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}replace_words_save(
						   i_boardid integer, 
						   i_id integer, 
						   i_badword varchar, 
						   i_goodword varchar)
				  RETURNS void AS
$BODY$
BEGIN
	IF (i_id IS NOT NULL AND i_id <> 0) THEN 	
		UPDATE {databaseSchema}.{objectQualifier}replace_words 
				 SET badword = i_badword, goodword = i_goodword 
				 WHERE id = i_id;		
	ELSE 
		INSERT INTO {databaseSchema}.{objectQualifier}replace_words
			(boardid,badword,goodword)
		VALUES
			(i_boardid,i_badword,i_GoodWord);
	END IF;
 END;
$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100;  
--GO

-- Function: {databaseSchema}.{objectQualifier}smiley_delete(integer)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}smiley_delete(integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}smiley_delete(
						   i_smileyid integer)
				  RETURNS void AS
$BODY$
BEGIN
	IF i_smileyid IS NOT NULL THEN
		DELETE FROM {databaseSchema}.{objectQualifier}smiley WHERE smileyid=i_smileyid;
	ELSE
		DELETE FROM {databaseSchema}.{objectQualifier}smiley;
		END IF;
END;$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100;
--GO

-- Function: {databaseSchema}.{objectQualifier}smiley_list(integer, integer)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}smiley_list(integer, integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}smiley_list(
						   i_boardid integer, 
						   i_smileyid integer)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}smiley_list_selecttype AS
$BODY$DECLARE
			 _rec {databaseSchema}.{objectQualifier}smiley_list_selecttype%ROWTYPE;
BEGIN
IF i_smileyid IS NULL THEN
FOR _rec IN
		SELECT  smileyid,boardid,code,icon,emoticon,sortorder 
		FROM {databaseSchema}.{objectQualifier}smiley
		WHERE boardid=i_boardid ORDER BY sortorder, code DESC
LOOP
RETURN NEXT _rec;
END LOOP;
	ELSE
FOR _rec IN
		SELECT smileyid,boardid,code,icon,emoticon,sortorder  
		FROM {databaseSchema}.{objectQualifier}smiley 
		WHERE smileyid=i_smileyid LIMIT 1
LOOP
RETURN NEXT _rec;
EXIT;
END LOOP;
		END IF;

END;
$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100 ROWS 1000;  
--GO

-- Function: {databaseSchema}.{objectQualifier}smiley_listunique(integer)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}smiley_listunique(integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}smiley_listunique(
						   i_boardid integer)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}smiley_listunique_selecttype AS
$BODY$DECLARE 
			 _rec {databaseSchema}.{objectQualifier}smiley_listunique_selecttype%ROWTYPE;
BEGIN
FOR _rec IN
	SELECT 
		icon AS Icon, 
		emoticon AS Emoticon,
		(SELECT code from {databaseSchema}.{objectQualifier}smiley x 
		where x.icon={databaseSchema}.{objectQualifier}smiley.icon LIMIT 1) AS Code,
		(SELECT sortorder from {databaseSchema}.{objectQualifier}smiley x 
		where x.icon={databaseSchema}.{objectQualifier}smiley.icon
		 ORDER BY x.sortorder ASC LIMIT 1) AS SortOrder
	FROM 
		{databaseSchema}.{objectQualifier}smiley
	WHERE
		boardid=i_boardid
	GROUP BY
		icon,
		emoticon,
		sortorder,
		code 
	ORDER BY
		sortorder,
		code
LOOP
RETURN NEXT _rec;
END LOOP;

END;$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100 ROWS 1000;  
--GO

-- Function: {databaseSchema}.{objectQualifier}smiley_resort(integer, integer, integer)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}smiley_resort(integer, integer, integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}smiley_resort(
						   i_boardid integer, 
						   i_smileyid integer, 
						   i_move integer)
				  RETURNS void AS
$BODY$DECLARE
			 ici_Position integer;
BEGIN 	
 
	SELECT sortorder INTO ici_Position FROM {databaseSchema}.{objectQualifier}smiley
	 WHERE boardid=i_boardid AND smileyid=i_smileyid;
 
	IF (ici_Position IS NOT NULL) THEN
 
	IF (i_move > 0) THEN
		UPDATE {databaseSchema}.{objectQualifier}smiley
			SET sortorder=sortorder-1
			WHERE boardid=i_boardid and 
				sortorder BETWEEN ici_Position 
				AND (ici_Position + i_move) AND
				sortorder BETWEEN 1 and 255;
	
	ELSEIF (i_move < 0) THEN
		update {databaseSchema}.{objectQualifier}smiley
			set sortorder=sortorder+1
			where boardid=i_boardid and 
				sortorder between (ici_Position+i_move) and ici_Position and
				sortorder between 0 and 254;
	END IF;
 
	ici_Position := ici_Position + i_move;
 
	IF (ici_Position>255) THEN  ici_Position := 255;
	ELSEIF (ici_Position<0) THEN   ici_Position := 0; END IF;

	UPDATE {databaseSchema}.{objectQualifier}smiley
		SET sortorder=ici_Position
		WHERE boardid=i_boardid AND 
			smileyid=i_smileyid;
END IF;
END;$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100; 
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}smiley_save(
						   i_smileyid integer, 
						   i_boardid integer, 
						   i_code varchar, 
						   i_icon varchar, 
						   i_emoticon varchar, 
						   i_sortorder integer, 
						   i_replace boolean)
				  RETURNS void AS
$BODY$
BEGIN
	IF i_smileyid IS NOT NULL THEN
		UPDATE {databaseSchema}.{objectQualifier}smiley
		 SET code = i_code, 
		 icon = i_icon, 
		 emoticon = i_emoticon, 
		 sortorder = i_sortorder
		 WHERE smileyid = i_smileyid;
	
	ELSE
		IF i_replace THEN
			DELETE FROM {databaseSchema}.{objectQualifier}smiley WHERE code=i_code; 
				END IF;
 
		IF NOT EXISTS(SELECT 1 from {databaseSchema}.{objectQualifier}smiley 
							   WHERE boardid=i_boardid AND code=i_code LIMIT 1) THEN
			INSERT INTO {databaseSchema}.{objectQualifier}smiley(boardid,code,
			icon,emoticon,sortorder)     
			VALUES(i_boardid,i_code,i_icon,i_emoticon,i_sortorder); 
				END IF;
				RETURN;
	END IF;
END;$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100; 
--GO

-- Function: {databaseSchema}.{objectQualifier}system_initialize(varchar, integer, varchar, varchar, varchar, character)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}system_initialize(varchar, integer, varchar, varchar, varchar, character);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}system_initialize(
						   i_name varchar, 
						   i_timezone integer,                         
						   i_culture varchar, 
						   i_languagefile varchar,  
						   i_forumemail varchar, 
						   i_smtpserver varchar, 
						   i_user varchar, 
						   i_useremail varchar, 
						   i_userkey varchar,                          
						   i_roleprefix varchar,
						   i_utctimestamp timestamp)
				  RETURNS void AS
$BODY$DECLARE
			 ici_tmpvalue text:= CAST(i_TimeZone AS text);
			 ici_varnull varchar;
			 ici_smtpserver text:= CAST(i_SmtpServer AS text);
			 ici_forumemail text:= CAST(i_ForumEmail AS text);
BEGIN
	
	 /*initalize required 'registry' settings*/
		-- ici_tmpValue:= CAST(i_TimeZone AS varchar);
	PERFORM {databaseSchema}.{objectQualifier}registry_save('version','1',null);
	PERFORM {databaseSchema}.{objectQualifier}registry_save('versionname','1.0.0',null); 
	PERFORM {databaseSchema}.{objectQualifier}registry_save('timezone',ici_tmpvalue ,null);
	PERFORM {databaseSchema}.{objectQualifier}registry_save('smtpserver', ici_smtpserver ,null);
	PERFORM {databaseSchema}.{objectQualifier}registry_save('forumemail', ici_forumemail,null);
	PERFORM {databaseSchema}.{objectQualifier}registry_save('culture' , CAST(i_culture AS text),null);
	PERFORM {databaseSchema}.{objectQualifier}registry_save('language', CAST(i_languagefile AS text),null);
 
	-- initalize new board
	PERFORM {databaseSchema}.{objectQualifier}board_create (i_name, i_languagefile, i_culture , ici_varnull,ici_varnull,i_user,i_useremail,i_userkey,true,i_roleprefix,i_utctimestamp  );
	RETURN;
 END;
$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100; 
--GO

   /* STORED PROCEDURE CREATED BY VZ-TEAM topic_announcements */

 -- Function: {databaseSchema}.{objectQualifier}topic_announcements(integer, integer, integer)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}topic_announcements(integer, integer, integer);
-- Function: {databaseSchema}.{objectQualifier}system_updateversion(integer, varchar)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}system_updateversion(integer, varchar);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}system_updateversion(
						   i_version integer, 
						   i_versionname varchar)
				  RETURNS void AS
$BODY$DECLARE 
			 ici_tmpvalue text;
BEGIN	
	SELECT CAST(i_version AS TEXT) INTO ici_tmpvalue;
	PERFORM {databaseSchema}.{objectQualifier}registry_save ('Version',ici_tmpvalue,null);
	PERFORM  {databaseSchema}.{objectQualifier}registry_save ('VersionName',i_versionname,null);
RETURN;
END;$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100;
--GO


CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}topic_active(
						   i_boardid integer,
						   i_categoryid integer,
						   i_pageuserid integer,
						   i_sincedate timestamp,
						   i_todate timestamp,
						   i_pageindex integer,
						   i_pagesize integer,
						   i_stylednicks boolean,
						   i_findlastunread boolean,
						   i_utctimestamp timestamp)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}topic_active_return_type AS
$BODY$DECLARE 
			 ici_topics_totalrowsnumber  integer;
			 ici_firstselectrownum integer;
			 ici_firstselectposted timestamp ;
			 ici_pageindex integer := i_pageindex;
			 ici_retcount integer := 0;
			 ici_counter integer := 0;
			 _rec {databaseSchema}.{objectQualifier}topic_active_return_type%ROWTYPE;
BEGIN  
		-- find total returned count
		select
		COUNT(1) into ici_topics_totalrowsnumber
			from
		{databaseSchema}.{objectQualifier}topic c
		join {databaseSchema}.{objectQualifier}user b on b.UserID=c.UserID
		join {databaseSchema}.{objectQualifier}forum d on d.ForumID=c.ForumID
		join {databaseSchema}.{objectQualifier}activeaccess x on x.ForumID=d.ForumID
		join {databaseSchema}.{objectQualifier}category cat on cat.CategoryID=d.CategoryID
	where
		(c.lastposted between i_sincedate and i_todate) and
		x.userid = i_pageuserid and
		x.readaccess IS TRUE and
		cat.boardid = i_boardid and
		(i_categoryid is null or cat.categoryid=i_categoryid) and
		c.isdeleted IS FALSE and
		c.topicmovedid IS NULL;	

		 ici_pageindex := ici_pageindex+1;
		 ici_firstselectrownum := (ici_pageindex - 1) * i_pagesize + 1;

	SELECT 		
		c.lastposted
	into ici_firstselectposted
	from
		{databaseSchema}.{objectQualifier}topic c
		join {databaseSchema}.{objectQualifier}user b on b.UserID=c.UserID
		join {databaseSchema}.{objectQualifier}forum d on d.ForumID=c.ForumID
		join {databaseSchema}.{objectQualifier}activeaccess x on x.ForumID=d.ForumID
		join {databaseSchema}.{objectQualifier}category cat on cat.CategoryID=d.CategoryID
	where
		(c.lastposted between i_sincedate and i_todate) and
		x.userid = i_pageuserid and
		x.readaccess IS TRUE and
		cat.boardid = i_boardid and
		(i_categoryid is null or cat.categoryid=i_categoryid) and
		c.isdeleted IS FALSE and
		c.topicmovedid IS NULL	
	ORDER BY
	c.lastposted DESC,
	cat.sortorder,
	d.sortorder,
	d.name DESC,
	c.priority DESC;	

 FOR _rec IN
	SELECT 	
		c.forumid AS ForumID,
		c.topicid AS TopicID,
		c.topicmovedid,
		c.posted AS Posted,
		COALESCE(c.topicmovedid,c.topicid) AS LinkTopicID,
		c.topic AS Subject,
		c.status,
		c.styles,
		c.description,
		c.userid,
		COALESCE(c.username,b.name) AS Starter,
		COALESCE(c.userdisplayname,b.displayname) AS StarterDisplay,
		(SELECT COUNT(1) 
					  FROM {databaseSchema}.{objectQualifier}message mes 
					  WHERE mes.topicid = c.topicid 
						AND mes.isdeleted IS TRUE 
						AND mes.isapproved IS TRUE 
						AND ((i_pageuserid IS NOT NULL AND mes.userid = i_pageuserid) 
						OR (i_pageuserid IS NULL)) ) AS NumPostsDeleted,
		((SELECT COUNT(1) FROM {databaseSchema}.{objectQualifier}message x 
		WHERE x.topicid=c.topicid and (x.flags & 8)=0) - 1)
				 AS Replies,
		c.views AS Views,
		c.lastposted AS LastPosted ,
		c.lastuserid AS LastUserID,
		COALESCE(c.lastusername,(SELECT x.name FROM {databaseSchema}.{objectQualifier}user x 
		where x.userid=c.lastuserid)) AS   LastUserName,
		COALESCE(c.lastuserdisplayname,(SELECT x.displayname FROM {databaseSchema}.{objectQualifier}user x 
		where x.userid=c.lastuserid)) AS   LastUserDisplayName,
		c.lastmessageid AS LastMessageID,
		c.lastmessageflags,
		c.topicid AS LastTopicID,
		c.flags AS TopicFlags,
		(SELECT COUNT(id) FROM {databaseSchema}.{objectQualifier}favoritetopic WHERE topicid = COALESCE(c.topicmovedid,c.topicid)),		
		c.priority,
		c.pollid,
		d.name AS ForumName, 		
		d.flags AS ForumFlags, 
		(SELECT CAST(message AS text) 
		FROM {databaseSchema}.{objectQualifier}message mes2 
		where mes2.topicid = COALESCE(c.topicmovedid,c.topicid) 
		AND mes2.position = 0 LIMIT 1) AS FirstMessage,
		(case(i_stylednicks)
			when true THEN  (SELECT us.userstyle FROM {databaseSchema}.{objectQualifier}user us where us.userid = c.userid)  
			else ''	 end),
		(case(i_stylednicks)
			when true THEN  (SELECT us.userstyle FROM {databaseSchema}.{objectQualifier}user us where us.userid = c.lastuserid)  
			else ''	 end),
		(case(i_findlastunread)
			 when true THEN
			   COALESCE((SELECT lastaccessdate FROM {databaseSchema}.{objectQualifier}forumreadtracking x WHERE x.forumid=d.forumid AND x.userid = i_pageuserid LIMIT 1), i_utctimestamp)
			 else TIMESTAMP '-infinity' end) AS LastForumAccess,
		(case(i_findlastunread)
			 when true THEN
			   COALESCE((SELECT lastaccessdate FROM {databaseSchema}.{objectQualifier}topicreadtracking y WHERE c.topicid=c.topicid AND y.userid = i_pageuserid LIMIT 1), i_utctimestamp)
			 else TIMESTAMP '-infinity'	 end) AS LastTopicAccess,
		(SELECT string_agg(tag, ',') FROM {databaseSchema}.{objectQualifier}tags tg join {databaseSchema}.{objectQualifier}topictags tt on tt.tagid = tg.tagid where 	  tt.topicid = c.topicid),	
		c.topicimage,
		c.topicimagetype,
		c.topicimagebin,
		0 as HasAttachments,	
		ici_topics_totalrowsnumber AS TotalRows,
		ici_pageindex	AS 	PageIndex	  	     
	FROM
		{databaseSchema}.{objectQualifier}topic c
		JOIN {databaseSchema}.{objectQualifier}user b ON b.userid=c.userid
		JOIN {databaseSchema}.{objectQualifier}forum d ON d.forumid=c.forumid
		JOIN {databaseSchema}.{objectQualifier}activeaccess x ON x.forumid=d.forumid
		JOIN {databaseSchema}.{objectQualifier}category cat ON cat.categoryid=d.categoryid
	WHERE
		c.lastposted <= ici_firstselectposted AND
		x.userid = i_pageuserid AND
		x.readaccess IS TRUE AND 
	cat.boardid = i_boardid AND
	(i_categoryid IS NULL OR cat.categoryid=i_categoryid) AND
	c.isdeleted IS FALSE AND
	c.topicmovedid IS NULL
	ORDER BY
	c.lastposted DESC,
	cat.sortorder,
	d.sortorder,
	d.name DESC,
	c.priority DESC			
LOOP
-- RETURN NEXT _rec;
ici_retcount := ici_retcount +1; 
IF (ici_retcount between  ici_firstselectrownum and ici_firstselectrownum+i_pagesize) THEN
RETURN NEXT _rec;
ici_counter := ici_counter + 1;
END IF;
IF (ici_counter >= i_pagesize) THEN
EXIT;
END IF;
END LOOP; 		
END;
$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100
  ROWS 1000; 
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}topic_announcements(
						   i_boardid integer, 
						   i_numposts integer, 
						   i_pageuserid integer)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}topic_announcements_return_type AS
$BODY$DECLARE
			 cntr integer:=0;
_rec {databaseSchema}.{objectQualifier}topic_announcements_return_type%ROWTYPE;
BEGIN 
	
 FOR _rec IN
	SELECT DISTINCT 
		t.topic, t.lastposted, t.posted, t.topicid,t.lastmessageid,t.lastmessageflags, f.forumid, f.flags
		FROM {databaseSchema}.{objectQualifier}topic t                         
						  INNER JOIN {databaseSchema}.{objectQualifier}forum f 
						  ON t.forumid = f.forumid
						  INNER JOIN {databaseSchema}.{objectQualifier}category c
						  ON c.categoryid = f.categoryid 
						 
						/*   JOIN {databaseSchema}.{objectQualifier}vaccess v 
						   on v.forumid=f.forumid */
							WHERE c.boardid = i_boardid
					/*    AND v.userid=i_userid
					   AND (v.readaccess <> 0 
					   OR (f.flags & 2) = 0)  */
					   AND (t.flags & 8) != 8 
					   AND t.topicmovedid IS NULL
					   AND (t.priority = 2) ORDER BY t.lastposted DESC 
								LOOP
IF (SELECT "ReadAccess" FROM {databaseSchema}.{objectQualifier}vaccess_combo(i_pageuserid, _rec."ForumID") LIMIT 1) > 0 OR (_rec."Flags" & 2) = 0 THEN
RETURN NEXT _rec;
END IF;
EXIT WHEN  cntr >= i_numposts;
cntr:=cntr+1;
END LOOP;    
 END;
 $BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100
  ROWS 1000;
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}rss_topic_latest(
						   i_boardid integer,
						   i_numposts integer,
						   i_pageuserid integer,
						   i_stylednicks boolean,
						   i_shownocountposts boolean)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}rss_topic_latest_return_type AS
$BODY$DECLARE            
			 _rec {databaseSchema}.{objectQualifier}rss_topic_latest_return_type%ROWTYPE; 
BEGIN		
	
	FOR _rec IN SELECT
		m.message,
		t.lastPosted,
		t.forumid,
		f.name as Forum,
		t.topic,
		t.topicID,
		t.topicmovedid,
		t.userid,
		t.username,	
		t.userdisplayname,
		(select x.isguest from {databaseSchema}.{objectQualifier}user x where x.userid = t.userid),	
		t.lastmessageid,
		t.lastmessageflags,
		t.lastuserid,	
		t.posted,		
		COALESCE(t.lastusername,(select x.name from {databaseSchema}.{objectQualifier}user x where x.userid = t.lastuserid)),
		COALESCE(t.lastuserdisplayname,(select x.displayname from {databaseSchema}.{objectQualifier}user x where x.userid = t.lastuserid)),
		(select x.isguest from {databaseSchema}.{objectQualifier}user x where x.userid = t.lastuserid)	
	FROM
		{databaseSchema}.{objectQualifier}message m 
	INNER JOIN	
		{databaseSchema}.{objectQualifier}topic t  ON t.LastMessageID = m.MessageID
	INNER JOIN
		{databaseSchema}.{objectQualifier}forum f ON t.ForumID = f.ForumID	
	INNER JOIN
		{databaseSchema}.{objectQualifier}category c ON c.CategoryID = f.CategoryID
	JOIN
		{databaseSchema}.{objectQualifier}vaccess v ON v.forumid=f.forumid
	WHERE	
		c.boardid = i_boardid
		AND t.topicmovedid is NULL
		AND v.userid= i_pageuserid
		AND (v.readaccess <> 0)
		-- is deleted flag
		AND t.flags & 8 != 8
		AND t.lastposted IS NOT NULL
		AND
		f.flags & 4 <> (CASE WHEN i_shownocountposts IS TRUE THEN -1 ELSE 4 END)
	ORDER BY
		t.lastposted DESC LIMIT i_numposts
		LOOP			
		return next _rec;		
		END LOOP;
	END;
$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100 ROWS 1000;
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}topic_create_by_message(
						   i_messageid integer, 
						   i_forumid integer, 
						   i_subject varchar,
						   i_utctimestamp timestamp)
				  RETURNS integer AS
$BODY$DECLARE
			 ici_userid integer;
			 ici_Posted timestamp ;
			 ici_topicid integer;
			 varcharnull varchar;			
 BEGIN    

   SELECT userid,posted
   INTO ici_userid,ici_Posted  
   FROM {databaseSchema}.{objectQualifier}message 
   WHERE messageid =  i_messageid;
   
	/*declare i_MessageID int*/

	IF ici_Posted IS NULL THEN ici_Posted = i_utctimestamp; END IF;

	INSERT INTO {databaseSchema}.{objectQualifier}topic(forumid,topic,userid,posted,views,priority,pollid,username,numposts,lastmessageflags)
	VALUES(i_forumid,i_subject,ici_userid,ici_Posted,0,0,null,varcharnull,0,22);
	SELECT CURRVAL(pg_get_serial_sequence('{databaseSchema}.{objectQualifier}topic','topicid')) INTO ici_topicid;  	

	-- PERFORM {databaseSchema}.{objectQualifier}message_save(i_TopicID,ici_userid,i_Message,i_UserName,i_IP,ici_Posted,i_ReplyToNull,BlogPostIDNull,i_Flags);
	RETURN ici_topicid;
	END;
$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100;
--GO
-- Function: {databaseSchema}.{objectQualifier}topic_delete(integer, boolean, boolean)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}topic_delete(integer, boolean, boolean);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}topic_delete(
						   i_topicid integer, 
						   i_topicmovedid integer,                            
						   i_erasetopic boolean,
						   i_updatelastpost boolean)
				  RETURNS void AS
$BODY$DECLARE 
			 ici_ForumID integer;           
			 ici_pollID integer;
			 ici_Deleted integer;			 
	BEGIN
	 
	-- just remove link    	
	IF (i_topicmovedid IS NOT NULL AND i_topicmovedid = i_topicid) THEN	   
	DELETE FROM {databaseSchema}.{objectQualifier}topic where topicmovedid = i_topicid; 	 
	ELSE 
	SELECT forumid
	INTO ici_ForumID 
	FROM  {databaseSchema}.{objectQualifier}topic WHERE topicid=i_topicid;

	UPDATE  {databaseSchema}.{objectQualifier}topic SET lastmessageid = NULL 
	WHERE topicid = i_topicid; 

  UPDATE  {databaseSchema}.{objectQualifier}forum SET
	lasttopicid = NULL,
	lastmessageid = NULL,
	lastuserid = NULL,
	lastusername = NULL,
	lastuserdisplayname = NULL,
	lastposted = NULL
	WHERE lastmessageid IN (SELECT messageid from  {databaseSchema}.{objectQualifier}message where topicid = i_topicid);    
	
	  
   UPDATE  {databaseSchema}.{objectQualifier}active SET topicid = NULL WHERE topicid = i_topicid;
   --delete messages and topics   
  
  IF not i_erasetopic THEN   
	UPDATE  {databaseSchema}.{objectQualifier}topic 
	SET 
	flags = flags | 8
	  WHERE topicid = i_topicid OR topicmovedid=i_topicid; 
	UPDATE  {databaseSchema}.{objectQualifier}message 
	SET 
	flags = flags | 8
	  WHERE topicid = i_topicid; 
	   
	ELSE
	DELETE FROM  {databaseSchema}.{objectQualifier}nntptopic WHERE topicid = i_topicid; 
 -- remove polls
	SELECT  pollid INTO ici_pollID FROM  {databaseSchema}.{objectQualifier}topic 
	WHERE topicid = i_topicid;
	IF ici_pollID IS NOT NULL THEN
		UPDATE  {databaseSchema}.{objectQualifier}topic 
	SET pollid = NULL 
	WHERE topicid = i_topicid;
	
	PERFORM {databaseSchema}.{objectQualifier}pollgroup_remove (ici_pollID, i_topicid, null, null, null, false, false);
	END IF;

	-- delete messages and topics
	  
	UPDATE {databaseSchema}.{objectQualifier}tags set tagcount = tagcount - 1 where tagid in (select tagid from {databaseSchema}.{objectQualifier}topictags  WHERE topicid = i_topicid);
	DELETE FROM {databaseSchema}.{objectQualifier}topictags  WHERE topicid IN (select topicid from {databaseSchema}.{objectQualifier}topic where topicid = i_topicid or topicmovedid = i_topicid);

	DELETE FROM  {databaseSchema}.{objectQualifier}topic WHERE topicmovedid = i_topicid;
   
	DELETE FROM  {databaseSchema}.{objectQualifier}attachment
	WHERE messageid IN
	(SELECT messageid FROM  {databaseSchema}.{objectQualifier}message WHERE topicid = i_topicid);
	DELETE FROM  {databaseSchema}.{objectQualifier}messagehistory
	WHERE messageid IN
	(SELECT messageid FROM  {databaseSchema}.{objectQualifier}message WHERE topicid = i_topicid);
	DELETE FROM  {databaseSchema}.{objectQualifier}message WHERE topicid = i_topicid;    
	DELETE FROM  {databaseSchema}.{objectQualifier}watchtopic WHERE topicid = i_topicid;
	DELETE FROM {databaseSchema}.{objectQualifier}favoritetopic  WHERE topicid = i_topicid;
	 DELETE FROM  {databaseSchema}.{objectQualifier}topicreadtracking WHERE topicid = i_topicid;
  
	DELETE FROM  {databaseSchema}.{objectQualifier}messagereportedaudit 
	  WHERE messageid IN 
		(SELECT messageid FROM  {databaseSchema}.{objectQualifier}message WHERE topicid = i_topicid);
	DELETE FROM  {databaseSchema}.{objectQualifier}messagereported 
	  WHERE messageid IN 
		(SELECT messageid FROM {databaseSchema}.{objectQualifier}message WHERE topicid = i_topicid);	
	DELETE FROM  {databaseSchema}.{objectQualifier}topic WHERE topicmovedid = i_topicid;
	DELETE FROM  {databaseSchema}.{objectQualifier}topic WHERE topicid = i_topicid;
	END IF;  
   
	-- commit
	IF i_updatelastpost IS NOT FALSE THEN
		PERFORM  {databaseSchema}.{objectQualifier}forum_updatelastpost (ici_ForumID);END IF;
	
	IF ici_ForumID IS NOT NULL THEN
		PERFORM  {databaseSchema}.{objectQualifier}forum_updatestats(ici_ForumID); END IF;
END IF;
RETURN;
END;$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100;  
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}pollgroup_remove(
						  i_pollgroupid integer, i_topicid integer, 
						  i_forumid integer, 
						  i_categoryid integer, 
						  i_boardid integer, 
						  i_removecompletely boolean, 
						  i_removeeverywhere boolean)
				  RETURNS void AS
$BODY$	
BEGIN
		 -- we delete poll from the place only it persists in other places 
		 IF i_removeeverywhere IS FALSE THEN			
				   IF i_topicid > 0 THEN
				   Update {databaseSchema}.{objectQualifier}topic set pollid = NULL where topicid = i_topicid;                 
				   END IF;
				   IF i_forumid > 0 THEN
				   Update {databaseSchema}.{objectQualifier}forum set pollgroupid = NULL where forumid = i_forumid;
				   END IF;   
				   IF i_categoryid > 0 THEN
				   Update {databaseSchema}.{objectQualifier}category set pollgroupid = NULL where categoryid = i_categoryid;
				   END IF; 
		 end  IF;      
			
		  -- we remove poll group links from all places where they are
		 IF ( i_removeeverywhere IS TRUE OR i_removecompletely IS TRUE) THEN	
				   Update {databaseSchema}.{objectQualifier}topic set pollid = NULL where pollid = i_pollgroupid; 
				   Update {databaseSchema}.{objectQualifier}forum set pollgroupid = NULL where pollgroupid = i_pollgroupid;
				   Update {databaseSchema}.{objectQualifier}category set pollgroupid = NULL where pollgroupid = i_pollgroupid;				 
		 END IF;

		 -- simply remove all polls
	IF i_removecompletely IS TRUE THEN
			DELETE FROM  {databaseSchema}.{objectQualifier}pollvote WHERE pollid IN (select PollID from {databaseSchema}.{objectQualifier}poll where pollgroupid = i_pollgroupid);
			DELETE FROM  {databaseSchema}.{objectQualifier}choice WHERE pollid IN (select PollID from {databaseSchema}.{objectQualifier}poll where pollgroupid = i_pollgroupid);
			DELETE FROM  {databaseSchema}.{objectQualifier}poll  WHERE pollgroupid = i_pollgroupid; 
			DELETE FROM  {databaseSchema}.{objectQualifier}pollgroupcluster WHERE pollgroupid = i_pollgroupid;		
	END IF;
			

	-- don't remove cluster IF the polls are not removed from db 
END;$BODY$
  LANGUAGE 'plpgsql' 
  COST 100; 
--GO

-- Function: {databaseSchema}.{objectQualifier}pollgroup_list(integer)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}pollgroup_list(integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}pollgroup_attach(
						   i_pollgroupid integer,
						   i_topicid integer, 
						   i_forumid integer, 
						   i_categoryid integer, 
						   i_boardid integer)
				  RETURNS integer AS
$BODY$DECLARE
			 retRes integer := 1;
BEGIN	
  -- this deletes possible polls without choices it should not normally happen
				  
				   DELETE FROM {databaseSchema}.{objectQualifier}pollvote WHERE pollid IN (SELECT pollid FROM {databaseSchema}.{objectQualifier}poll WHERE pollgroupid = NULL);
				   DELETE FROM {databaseSchema}.{objectQualifier}choice WHERE pollid IN (SELECT pollid FROM {databaseSchema}.{objectQualifier}poll WHERE pollgroupid = NULL);
				   DELETE FROM {databaseSchema}.{objectQualifier}poll WHERE pollid IN (SELECT pollid FROM {databaseSchema}.{objectQualifier}poll WHERE pollgroupid = NULL);				   				   
				  
				   IF i_topicid > 0 THEN				 
				   IF exists (select 1 from {databaseSchema}.{objectQualifier}topic where topicid = i_topicid  and pollid is not null limit 1) THEN
				   retRes := 1;				  
				   else				  
				   Update {databaseSchema}.{objectQualifier}topic set pollid = i_pollgroupid where topicid = i_topicid; 
				   retRes := 0;
				   END IF;
				   END IF;             
				  
				   IF i_forumid > 0 THEN				 
				   IF exists (select 1 from {databaseSchema}.{objectQualifier}forum where forumid = i_forumid and pollgroupid is not null limit 1) THEN
				   retRes := 1;				 
				   else				   
				   Update {databaseSchema}.{objectQualifier}forum  set pollgroupid = i_pollgroupid where forumid = i_forumid;
					retRes := 0;			   
				   END IF;
				   END IF;

				   IF i_categoryid > 0 THEN				   
				   IF exists (select 1 from {databaseSchema}.{objectQualifier}category  where categoryid = i_categoryid and pollgroupid is null limit 1) THEN
				   retRes := 1;				  
				   else				   
				   UPDATE {databaseSchema}.{objectQualifier}category SET pollgroupid = i_pollgroupid where categoryid = i_categoryid;
				   retRes := 0;
				   END IF;				
				   END IF;
RETURN  retRes;
END;$BODY$
  LANGUAGE 'plpgsql' 
  COST 100;  
--GO

-- Function: {databaseSchema}.{objectQualifier}pollgroup_list(integer)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}pollgroup_list(integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}pollgroup_list(
						   i_userid integer, 
						   i_forumid integer, 
						   i_boardid integer)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}pollgroup_list_return_type AS
$BODY$DECLARE
			 _rec {databaseSchema}.{objectQualifier}pollgroup_list_return_type%ROWTYPE;
BEGIN
	
FOR _rec in SELECT distinct(p.question), p.pollgroupid from {databaseSchema}.{objectQualifier}poll p
	LEFT JOIN 	{databaseSchema}.{objectQualifier}pollgroupcluster pgc ON pgc.pollgroupid = p.pollgroupid
	WHERE p.pollgroupid is not null
	-- WHERE p.Closes IS NULL OR p.Closes > GETUTCDATE()
	order by question asc
LOOP
RETURN NEXT _rec;
END LOOP;
END;$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100 ROWS 1000; 
--GO

-- Function: {databaseSchema}.{objectQualifier}pollgroup_list(integer)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}pollgroup_list(integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}topic_findduplicate(
						   i_topicname varchar)
				  RETURNS integer AS
$BODY$
BEGIN	
IF i_topicname IS NOT NULL THEN		
		IF EXISTS (SELECT 1 FROM {databaseSchema}.{objectQualifier}topic WHERE topic LIKE  i_topicname AND topicmovedid IS NULL limit 1) THEN
		return 1;
		ELSE
		return 0;
	END IF;
	ELSE	
		return 0;
	END	IF;
END;$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100; 
--GO

-- Function: {databaseSchema}.{objectQualifier}topic_findnext(integer)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}topic_findnext(integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}topic_findnext(
						   i_topicid integer)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}topic_findnext_return_type AS
$BODY$DECLARE 
			 ici_lastposted timestamp;
			 ici_ForumID integer;
			 _rec {databaseSchema}.{objectQualifier}topic_findnext_return_type%ROWTYPE;
BEGIN
   
	SELECT lastposted,forumid INTO ici_lastposted, ici_ForumID  
	FROM {databaseSchema}.{objectQualifier}topic 
	WHERE topicid = i_topicid AND topicmovedid is null;
FOR _rec IN 
	SELECT topicid FROM {databaseSchema}.{objectQualifier}topic 
	WHERE lastposted>ici_lastposted 
	AND forumid = ici_ForumID 
	AND (flags & 8) = 0 
	AND topicmovedid is null
	ORDER BY lastposted ASC 
	LOOP
RETURN  NEXT _rec;
END LOOP;
END;$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100 ROWS 1;  
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}topic_findprev(
						   i_topicid integer)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}topic_findprevnext_return_type AS
$BODY$DECLARE
			 ici_lastposted timestamp;
			 ici_ForumID integer;			
			 _rec {databaseSchema}.{objectQualifier}topic_findprevnext_return_type%ROWTYPE;			
BEGIN 

	SELECT lastposted,forumid INTO ici_lastposted,ici_ForumID 
		  FROM {databaseSchema}.{objectQualifier}topic 
			WHERE topicid = i_topicid AND topicmovedid is null;
  FOR _rec IN  
	SELECT topicid 
	FROM {databaseSchema}.{objectQualifier}topic 
	WHERE lastposted<ici_lastposted 
	AND forumid = ici_ForumID 
	AND (flags & 8) = 0 
	AND topicmovedid is null
	ORDER BY lastposted DESC 
	LOOP
	RETURN  NEXT _rec;
	END LOOP;
END;$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100; 
--GO

-- Function: {databaseSchema}.{objectQualifier}topic_info(integer, boolean)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}topic_info(integer, boolean);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}topic_info(
						   i_topicid integer, 						
						   i_showdeleted boolean,
						   i_gettags boolean)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}topic_info_return_type AS
$BODY$DECLARE
			 ici_topicid integer:= i_topicid;
			 _rec {databaseSchema}.{objectQualifier}topic_info_return_type%ROWTYPE;
 BEGIN 	
 
	IF ici_topicid IS NULL OR ici_topicid = 0 THEN
	
	IF i_showdeleted THEN
FOR _rec IN
			SELECT
				  t.topicid,
				  t.forumid,
				  t.userid,
				  t.username,
				  t.posted,
				  t.topic,
				  t.views,
				  t.priority,
				  t.pollid,
				  t.topicmovedid,
				  t.lastposted,
				  t.lastmessageid,
				  t.lastuserid,
				  t.lastusername,
				  t.numposts,
				  t.flags,
				  t.answermessageid,
				  t.lastmessageflags,
				  t.description,
				  t.status,
				  t.styles,
				  (CASE WHEN i_gettags THEN
				  (SELECT string_agg(tg.tag, ',') FROM {databaseSchema}.{objectQualifier}tags tg join {databaseSchema}.{objectQualifier}topictags tt on tt.tagid = tg.tagid where tt.topicid = t.topicid) ELSE '' END),				  	  
				  t.topicimage,
				  t.topicimagetype,
				  t.topicimagebin,
				  (SELECT mes2.message 
				 FROM {databaseSchema}.{objectQualifier}message mes2 
				 WHERE mes2.topicid = COALESCE(t.topicmovedid,t.topicid) 
				 AND mes2.position = 0 ORDER BY mes2.topicid LIMIT 1),
				  t.islocked,
				  t.isdeleted,
				  t.ispersistent,
				  t.isquestion 			 
			FROM {databaseSchema}.{objectQualifier}topic t
LOOP
	RETURN NEXT _rec;
END LOOP;
		ELSE
FOR _rec IN
SELECT
				  t.topicid,
				  t.forumid,
				  t.userid,
				  t.username,
				  t.posted,
				  t.topic,
				  t.views,
				  t.priority,
				  t.pollid,
				  t.topicmovedid,
				  t.lastposted,
				  t.lastmessageid,
				  t.lastuserid,
				  t.lastusername,
				  t.numposts,
				  t.flags,
				  t.answermessageid,
				  t.lastmessageflags,
				  t.description,
				  t.status,
				  t.styles,
				  (CASE WHEN i_gettags THEN
				  (SELECT string_agg(tg.tag, ',') FROM {databaseSchema}.{objectQualifier}tags tg join {databaseSchema}.{objectQualifier}topictags tt on tt.tagid = tg.tagid where tt.topicid = t.topicid) ELSE '' END),		  
				  t.topicimage,
				  t.topicimagetype,
				  t.topicimagebin,
				  (SELECT mes2.message 
				 FROM {databaseSchema}.{objectQualifier}message mes2 
				 WHERE mes2.topicid = COALESCE(t.topicmovedid,t.topicid) 
				 AND mes2.position = 0 LIMIT 1),
				  t.islocked,
				  t.isdeleted,
				  t.ispersistent,
				  t.isquestion 			 
			FROM {databaseSchema}.{objectQualifier}topic t 
			WHERE (t.flags & 8) <> 8
LOOP
	RETURN NEXT _rec;
END LOOP;
	END IF;
	ELSE 	
		IF i_showdeleted IS TRUE THEN
FOR _rec IN
SELECT
				  t.topicid,
				  t.forumid,
				  t.userid,
				  t.username,
				  t.posted,
				  t.topic,
				  t.views,
				  t.priority,
				  t.pollid,
				  t.topicmovedid,
				  t.lastposted,
				  t.lastmessageid,
				  t.lastuserid,
				  t.lastusername,
				  t.numposts,
				  t.flags,
				  t.answermessageid,
				  t.lastmessageflags,
				  t.description,
				  t.status,
				  t.styles,
				  (CASE WHEN i_gettags THEN
				  (SELECT string_agg(tg.tag, ',') FROM {databaseSchema}.{objectQualifier}tags tg join {databaseSchema}.{objectQualifier}topictags tt on tt.tagid = tg.tagid where tt.topicid = t.topicid) ELSE '' END),		  
				  t.topicimage,
				  t.topicimagetype,
				  t.topicimagebin,
				  (SELECT mes2.message 
				 FROM {databaseSchema}.{objectQualifier}message mes2 
				 WHERE mes2.topicid = COALESCE(t.topicmovedid,t.topicid) 
				 AND mes2.position = 0 LIMIT 1),
				  t.islocked,
				  t.isdeleted,
				  t.ispersistent,
				  t.isquestion 			 
			FROM {databaseSchema}.{objectQualifier}topic t
			WHERE t.topicid = ici_topicid
LOOP
RETURN NEXT _rec;
END LOOP;
		ELSE
FOR _rec IN SELECT
				  t.topicid,
				  t.forumid,
				  t.userid,
				  t.username,
				  t.posted,
				  t.topic,
				  t.views,
				  t.priority,
				  t.pollid,
				  t.topicmovedid,
				  t.lastposted,
				  t.lastmessageid,
				  t.lastuserid,
				  t.lastusername,
				  t.numposts,
				  t.flags,
				  t.answermessageid,
				  t.lastmessageflags,
				  t.description,
				  t.status,
				  t.styles,
				  (CASE WHEN i_gettags THEN
				  (SELECT string_agg(tg.tag, ',') FROM {databaseSchema}.{objectQualifier}tags tg join {databaseSchema}.{objectQualifier}topictags tt on tt.tagid = tg.tagid where tt.topicid = t.topicid) ELSE '' END),		  
				  t.topicimage,
				  t.topicimagetype,
				  t.topicimagebin,
				  (SELECT mes2.message 
				 FROM {databaseSchema}.{objectQualifier}message mes2 
				 WHERE mes2.topicid = COALESCE(t.topicmovedid,t.topicid) 
				 AND mes2.position = 0 LIMIT 1),
				  t.islocked,
				  t.isdeleted,
				  t.ispersistent,
				  t.isquestion 			 
			FROM {databaseSchema}.{objectQualifier}topic t 
				  WHERE t.topicid = ici_topicid
AND (t.flags & 8) = 0
LOOP
RETURN NEXT _rec;
END LOOP;		
	END IF;
		END IF; 

END;$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100
  ROWS 1000; 
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}topic_latest(
						   i_boardid integer,
						   i_numposts integer,
						   i_pageuserid integer,
						   i_stylednicks boolean,
						   i_shownocountposts boolean,
						   i_findlastunread boolean,
						   i_utctimestamp timestamp)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}topic_latest_return_type AS
$BODY$DECLARE           
			 i_StartID integer:=0;
			 _rec {databaseSchema}.{objectQualifier}topic_latest_return_type%ROWTYPE;
BEGIN 	
	FOR _rec IN
	SELECT
		t.lastposted,
		t.forumid,
		f.name as Forum,
		t.topic,
		t.status,
		t.styles,
		t.topicid,
		t.topicmovedid,
		t.userid,
		COALESCE(t.username,(select x.name from {databaseSchema}.{objectQualifier}user x where x.userid = t.userid)),
		COALESCE(t.userdisplayname,(select x.displayname from {databaseSchema}.{objectQualifier}user x where x.userid = t.userid)),
		(select x.isguest from {databaseSchema}.{objectQualifier}user x where x.userid = t.userid),	
		t.lastmessageid,
		t.lastmessageflags,
		t.lastuserid,
		t.numposts,
		t.posted,
		COALESCE(t.lastusername,
		(SELECT x.name FROM {databaseSchema}.{objectQualifier}user x 
WHERE x.userid = t.lastuserid)) AS LastUserName, 
	   COALESCE(t.lastuserdisplayname,
		(SELECT x.displayname FROM {databaseSchema}.{objectQualifier}user x 
WHERE x.userid = t.lastuserid)) AS LastUserDisplayName, 
		(case when i_stylednicks is true THEN  (SELECT u.userstyle FROM {databaseSchema}.{objectQualifier}user u where u.userid = t.lastuserid LIMIT 1)  
			else ''	 end)   AS LastUserStyle,
	(select x.isguest from {databaseSchema}.{objectQualifier}user x where x.userid = t.lastuserid),	
		(case(i_findlastunread)
			 when true THEN
			   COALESCE((SELECT LastAccessDate FROM {databaseSchema}.{objectQualifier}forumreadtracking x WHERE x.forumid=f.forumid AND x.userid = i_pageuserid), i_utctimestamp)
			 else TIMESTAMP '-infinity'	 end) AS LastForumAccess,
		(case(i_findlastunread)
			 when true THEN
			   COALESCE((SELECT LastAccessDate FROM {databaseSchema}.{objectQualifier}topicreadtracking y WHERE y.topicid=t.topicid AND y.userid = i_pageuserid), i_utctimestamp)
			 else TIMESTAMP '-infinity' end) AS LastTopicAccess
	FROM 
		{databaseSchema}.{objectQualifier}topic t
	INNER JOIN
		{databaseSchema}.{objectQualifier}forum f ON t.forumid = f.forumid	
	INNER JOIN
		{databaseSchema}.{objectQualifier}category c ON c.categoryid = f.categoryid
	JOIN
		{databaseSchema}.{objectQualifier}activeaccess v ON v.forumid=f.forumid
	WHERE
		c.boardid = i_boardid
		AND t.topicmovedid is NULL
		AND v.userid= i_pageuserid 
		AND (v.readaccess IS TRUE)
	-- is not deleted
	AND t.flags & 8 <> 8
	AND t.lastposted IS NOT NULL
	AND f.flags & 4 <> (CASE WHEN i_shownocountposts IS TRUE THEN -1 ELSE 4 END)	
	ORDER BY
	t.lastposted DESC LIMIT i_numposts
LOOP  
RETURN NEXT _rec;
END LOOP;
   
	END;$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100
  ROWS 1000;
--GO

-- Function: {databaseSchema}.{objectQualifier}topic_list(integer, integer, integer, timestamp, integer, integer)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}topic_list(integer, integer, integer, timestamp, integer, integer, boolean);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}announcements_list(
						   i_forumid integer,
						   i_userid integer,
						   i_sincedate timestamp,
						   i_todate timestamp,
						   i_pageindex integer,
						   i_pagesize integer,
						   i_stylednicks boolean,
						   i_showmoved boolean,
						   i_showdeleted boolean,
						   i_findlastunread boolean,
						   i_gettags boolean,
						   i_utctimestamp timestamp)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}topic_list_return_type AS
$BODY$DECLARE
			 ici_shiftsticky integer :=0;
			 ici_post_totalrowsnumber  integer;
			 ici_post_priorityrowsnumber_pages integer;
			 ici_post_priorityrowsnumber integer;
			 ici_post_priorityrowsnumber_shift integer;
			 ici_firstselectrownum integer;
			 ici_firstselectposted timestamp ;
			 ici_ceiling decimal;
			 ici_pageindex integer := i_pageindex;
			 ici_retcount integer := 0;
			 ici_counter integer := 0;
			 _rec {databaseSchema}.{objectQualifier}topic_list_return_type%ROWTYPE;
BEGIN  
		-- find total returned count
		select
		COUNT(t.topicid) into ici_post_priorityrowsnumber
	FROM {databaseSchema}.{objectQualifier}topic t 
	WHERE t.forumid = i_forumid		
		AND	(t.priority=2) 
		AND	(i_showdeleted  or (t.flags & 8) <> 8)
		AND	(t.topicmovedid IS NOT NULL OR t.numposts > 0) 
		AND
		((i_showmoved IS TRUE)
		or
		(i_showmoved IS NOT TRUE AND  t.topicmovedid IS NULL));

		 ici_pageindex := ici_pageindex+1;
		 ici_firstselectrownum := (ici_pageindex - 1) * i_pagesize + 1;

	SELECT 		
		t.lastposted
	into ici_firstselectposted
	from 	
		{databaseSchema}.{objectQualifier}topic t 
	where
		t.forumID = i_forumid	    
		AND	(t.priority=2) 
		AND	(i_showdeleted  or (t.flags & 8) <> 8)
		AND	(t.topicmovedid IS NOT NULL OR t.numposts > 0) 
		AND
		((i_showmoved IS TRUE)
		or
		(i_showmoved IS NOT TRUE AND  t.topicmovedid IS NULL))		
	order by
		 t.priority DESC, t.lastposted DESC; 

 FOR _rec IN
	SELECT 	
		t.forumid,
		t.topicid,
		t.posted,
		COALESCE(t.topicmovedid,t.topicid) AS LinkTopicID,
		t.topicmovedid,
		(SELECT COUNT(id) FROM {databaseSchema}.{objectQualifier}favoritetopic WHERE topicid = COALESCE(t.topicmovedid,t.topicid)),	
		t.topic AS Subject,		
		t.description,
		t.status,
		t.styles,
		t.userid,
		COALESCE(t.username,b.name) AS Starter,
		COALESCE(t.userdisplayname,b.displayname) AS StarterDisplay,
		t.numposts - 1 AS Replies,
		(SELECT COUNT(1) FROM
		 {databaseSchema}.{objectQualifier}message mes 
				 WHERE mes.topicid = t.topicid AND ((mes.flags & 8) = 8)
				 AND ((mes.flags & 16) = 16) 
				 AND ((i_userid IS NOT NULL AND mes.userid = i_userid) 
				 OR (i_userid IS NULL)) ) AS NumPostsDeleted, 
		t.views AS Views,
		t.lastposted AS LastPosted,
		t.lastuserid AS LastUserID,
		COALESCE(t.lastusername,
		(select x.name from {databaseSchema}.{objectQualifier}user x 
		where x.userid=t.lastuserid)) AS LastUserName,
		COALESCE(t.lastuserdisplayname,
		(select x.displayname from {databaseSchema}.{objectQualifier}user x 
		where x.userid=t.lastuserid)) AS LastUserDisplayName,
		t.lastmessageid AS LastMessageID,
		t.topicid AS LastTopicID,
		t.linkdate,
		t.flags AS TopicFlags,
		t.priority,
		t.pollid,
		d.flags AS ForumFlags,
		CAST((SELECT mes2.message 
				 FROM {databaseSchema}.{objectQualifier}message mes2 
				 WHERE mes2.topicid = COALESCE(t.topicmovedid,t.topicid) 
				 AND mes2.position = 0 ORDER BY mes2.topicid LIMIT 1) AS 
varchar(4000)) AS FirstMessage,
		(case(i_stylednicks)
			when true THEN  (SELECT us.userstyle FROM {databaseSchema}.{objectQualifier}user us where us.userid = t.userid)  
			else ''	 end) AS StarterStyle, 
		(case(i_stylednicks)
			when true THEN  (SELECT us.userstyle FROM {databaseSchema}.{objectQualifier}user us where us.userid = t.lastuserid)  
			else ''	 end)  AS LastUserStyle,
	   (case(i_findlastunread)
			 when true THEN
			   COALESCE((SELECT lastaccessdate FROM {databaseSchema}.{objectQualifier}forumreadtracking x WHERE x.forumid=d.forumid AND x.userid = i_userid), TIMESTAMP '-infinity')
			 else TIMESTAMP '-infinity'	 end) AS LastForumAccess,
		(case(i_findlastunread)
			 when true THEN
			   COALESCE((SELECT lastaccessdate FROM {databaseSchema}.{objectQualifier}topicreadtracking y WHERE y.topicid=t.topicid AND y.userid = i_userid), TIMESTAMP '-infinity')
			 else TIMESTAMP '-infinity'	 end) AS LastTopicAccess,
		(case(i_gettags)
			 when true THEN
		(SELECT string_agg(tag, ',') FROM {databaseSchema}.{objectQualifier}tags tg join {databaseSchema}.{objectQualifier}topictags tt on tt.tagid = tg.tagid where tt.topicid = t.topicid)
		 else '' end),	
		t.topicimage,
		t.topicimagetype,
		t.topicimagebin,
		0 as HasAttachments, 
		ici_post_priorityrowsnumber AS TotalRows,
		ici_pageindex	AS 	PageIndex
		FROM
		{databaseSchema}.{objectQualifier}topic t 
		JOIN {databaseSchema}.{objectQualifier}user  b 
		ON b.UserID=t.UserID
		JOIN {databaseSchema}.{objectQualifier}rank r 
		ON r.rankid=b.rankid
		join {databaseSchema}.{objectQualifier}forum d on d.ForumID=t.ForumID	
		WHERE t.forumid = i_forumid
		AND	(t.priority=2) 
		AND	(i_showdeleted  or (t.flags & 8) <> 8)
		AND	((t.topicmovedid IS NOT NULL) OR (t.numposts > 0)) 
		AND
		t.lastposted <= COALESCE(ici_firstselectposted,TIMESTAMP '-infinity')
		AND
		((i_showmoved IS TRUE)
		or
		(i_showmoved IS NOT TRUE AND  t.topicmovedid IS NULL))		
	order by
		 t.priority DESC,t.lastposted DESC		
			
LOOP
-- RETURN NEXT _rec;
ici_retcount := ici_retcount +1; 
IF (ici_retcount between  ici_firstselectrownum and ici_firstselectrownum+i_pagesize) THEN
RETURN NEXT _rec;
ici_counter := ici_counter + 1; 
END IF;
IF (ici_counter >= i_pagesize) THEN
EXIT;
END IF;
END LOOP; 		
END;
$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100
  ROWS 1000; 
--GO

-- Function: {databaseSchema}.{objectQualifier}topic_list(integer, integer, integer, timestamp, integer, integer)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}topic_list(integer, integer, integer, timestamp, integer, integer, boolean);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}topic_list(
						   i_forumid integer,
						   i_userid integer,
						   i_sincedate timestamp,
						   i_todate timestamp,
						   i_pageindex integer,
						   i_pagesize integer,
						   i_stylednicks boolean,
						   i_showmoved boolean,
						   i_showdeleted boolean,
						   i_findlastunread boolean,
						   i_gettags boolean,
						   i_utctimestamp timestamp)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}topic_list_return_type AS
$BODY$DECLARE
			 ici_shiftsticky integer :=0;
			 ici_post_totalrowsnumber  integer;
			 ici_post_priorityrowsnumber_pages integer;
			 ici_post_priorityrowsnumber integer;
			 ici_post_priorityrowsnumber_shift integer;
			 ici_firstselectrownum integer;
			 ici_firstselectposted timestamp ;
			 ici_ceiling decimal;
			 ici_pageindex integer := i_pageindex;
			 ici_retcount integer := 0;
			 ici_counter integer := 0;
			 _rec {databaseSchema}.{objectQualifier}topic_list_return_type%ROWTYPE;
BEGIN      
	   
		-- find priority returned count
		select
		COUNT(t.topicid) into ici_post_priorityrowsnumber
	FROM {databaseSchema}.{objectQualifier}topic t 
	WHERE t.forumid = i_forumid		
		AND (t.priority=1) 
		AND	(i_showdeleted  or (t.flags & 8) <> 8)
		AND	(t.topicmovedid IS NOT NULL OR t.numposts > 0) 
		AND
		((i_showmoved IS TRUE)
		or
		(i_showmoved IS NOT TRUE AND  t.topicmovedid IS NULL));
		ici_post_priorityrowsnumber_pages := CEILING(CAST(ici_post_priorityrowsnumber AS decimal)/i_pagesize);	 

		-- find total returned count including priority
		select
		COUNT(t.topicid) into ici_post_totalrowsnumber
	FROM {databaseSchema}.{objectQualifier}topic t 
	WHERE t.forumid = i_forumid		
		AND	(
			(t.priority>0 AND t.priority<>2) 
			OR 
			(t.priority <=0 AND t.lastposted>=i_sincedate)
			) 
		AND	(i_showdeleted  or (t.flags & 8) <> 8)
		AND	(t.topicmovedid IS NOT NULL OR t.numposts > 0) 
		AND
		((i_showmoved IS TRUE)
		or
		(i_showmoved IS NOT TRUE AND  t.topicmovedid IS NULL));

		 ici_pageindex := ici_pageindex+1;
		 ici_firstselectrownum := (ici_pageindex - 1) * i_pagesize;	

	SELECT 		
		t.lastposted
	into ici_firstselectposted
	from 	
		{databaseSchema}.{objectQualifier}topic t 
	where
		t.forumID = i_forumid	    
	AND	 ( 
		 (ici_shiftsticky = 1 and t.priority>0 AND t.priority<>2)		 
		 OR 
		 (t.priority <=0 AND t.lastposted >= i_sincedate)		
		 )
		AND	(i_showdeleted  or (t.flags & 8) <> 8)
		AND	(t.topicmovedid IS NOT NULL OR t.numposts > 0) 
		AND
		((i_showmoved IS TRUE)
		or
		(i_showmoved IS NOT TRUE AND  t.topicmovedid IS NULL))		
	order by
		 t.priority DESC, t.lastposted DESC; 

 FOR _rec IN
	SELECT 	
		t.forumid,
		t.topicid,
		t.posted,
		COALESCE(t.topicmovedid,t.topicid) AS LinkTopicID,
		t.topicmovedid,
		(SELECT COUNT(id) FROM {databaseSchema}.{objectQualifier}favoritetopic WHERE topicid = COALESCE(t.topicmovedid,t.topicid)),	
		t.topic AS Subject,		
		t.description,
		t.status,
		t.styles,
		t.userid,
		COALESCE(t.username,b.name) AS Starter,
		COALESCE(t.userdisplayname,b.displayname) AS StarterDisplay,
		t.numposts - 1 AS Replies,
		(SELECT COUNT(1) FROM
		 {databaseSchema}.{objectQualifier}message mes 
				 WHERE mes.topicid = t.topicid AND ((mes.flags & 8) = 8)
				 AND ((mes.flags & 16) = 16) 
				 AND ((i_userid IS NOT NULL AND mes.userid = i_userid) 
				 OR (i_userid IS NULL)) ) AS NumPostsDeleted,
		t.views AS Views,
		t.lastposted AS LastPosted,
		t.lastuserid AS LastUserID,
		COALESCE(t.lastusername,
		(select x.name from {databaseSchema}.{objectQualifier}user x 
		where x.userid=t.lastuserid)) AS LastUserName,
		COALESCE(t.lastuserdisplayname,
		(select x.displayname from {databaseSchema}.{objectQualifier}user x 
		where x.userid=t.lastuserid)) AS LastUserDisplayName,
		t.lastmessageid AS LastMessageID,
		t.topicid AS LastTopicID,
		t.linkdate,
		t.flags AS TopicFlags,
		t.priority,
		t.pollid,
		d.flags AS ForumFlags,
		CAST((SELECT message 
				 FROM {databaseSchema}.{objectQualifier}message mes2 
				 WHERE mes2.topicid = COALESCE(t.topicmovedid,t.topicid) 
				 AND mes2.position = 0 ORDER BY mes2.topicid LIMIT 1) AS 
varchar(4000)) AS FirstMessage,
		(case(i_stylednicks)
			when true THEN  (SELECT us.userstyle FROM {databaseSchema}.{objectQualifier}user us where us.userid = t.userid)  
			else ''	 end)  AS StarterStyle , 
		(case(i_stylednicks)
			when true THEN  (SELECT us.userstyle FROM {databaseSchema}.{objectQualifier}user us where us.userid = t.lastuserid)  
			else ''	 end)  AS LastUserStyle,
		(case(i_findlastunread)
			 when true THEN
			   COALESCE((SELECT lastaccessdate FROM {databaseSchema}.{objectQualifier}forumreadtracking x WHERE x.forumid=d.forumid AND x.userid = i_userid), i_utctimestamp)
			 else TIMESTAMP '-infinity' end) AS LastForumAccess,
		(case(i_findlastunread)
			 when true THEN
			   COALESCE((SELECT lastaccessdate FROM {databaseSchema}.{objectQualifier}topicreadtracking y WHERE y.topicid=t.topicid AND y.userid = i_userid), i_utctimestamp)
			 else TIMESTAMP '-infinity' end) AS LastTopicAccess,
		(case(i_gettags)
			 when true THEN
		(SELECT string_agg(tag, ',') FROM {databaseSchema}.{objectQualifier}tags tg join {databaseSchema}.{objectQualifier}topictags tt on tt.tagid = tg.tagid where 	  tt.topicid = t.topicid)
		 else '' end),	
		t.topicimage,
		t.topicimagetype,
		t.topicimagebin,
		0 as HasAttachments,
		ici_post_totalrowsnumber AS TotalRows,
		ici_pageindex	AS 	PageIndex
		FROM
		{databaseSchema}.{objectQualifier}topic t 
		JOIN {databaseSchema}.{objectQualifier}user  b 
		ON b.UserID=t.UserID
		JOIN {databaseSchema}.{objectQualifier}rank r 
		ON r.rankid=b.rankid
		join {databaseSchema}.{objectQualifier}forum d on d.ForumID=t.ForumID	
		WHERE t.forumid = i_forumid
		AND	(( (t.priority>0 AND t.priority<>2)) OR (t.priority <=0 AND t.lastposted<= COALESCE(ici_firstselectposted,TIMESTAMP '-infinity'))) 
		AND	(i_showdeleted  or (t.flags & 8) <> 8)
		AND	((t.topicmovedid IS NOT NULL) OR (t.numposts > 0)) 
		AND
		((i_showmoved IS TRUE)
		or
		(i_showmoved IS NOT TRUE AND  t.topicmovedid IS NULL))		
	order by
		 t.priority DESC,t.lastposted DESC		
			
LOOP
-- RETURN NEXT _rec;
ici_retcount := ici_retcount +1; 
IF (ici_retcount between  ici_firstselectrownum + ici_post_priorityrowsnumber_pages and ici_firstselectrownum+i_pagesize) THEN
RETURN NEXT _rec;
ici_counter := ici_counter + 1; 
END IF;
IF (ici_counter >= i_pagesize) THEN
EXIT;
END IF;
END LOOP; 		
END;
$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100
  ROWS 1000; 
--GO

-- Function: {databaseSchema}.{objectQualifier}topic_listmessages(integer)

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}topic_listmessages(
						   i_topicid integer)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}topic_listmessages_return_type AS
$BODY$DECLARE
			 _rec {databaseSchema}.{objectQualifier}topic_listmessages_return_type%ROWTYPE;
BEGIN
FOR _rec IN SELECT
	a.messageid,
a.topicid,
a.replyto,
a.position,
a.indent,
a.userid,
a.username,
a.userdisplayname,
a.posted,
a.message,
a.ip,
a.edited,
a.flags,
a.editreason,
a.ismoderatorchanged,
a.deletereason,
COALESCE((a.flags & 8)=8,false) AS isdeleted,
COALESCE((a.flags & 16)=16,false) AS isapproved,
a.blogpostid 
		   FROM {databaseSchema}.{objectQualifier}message a
	WHERE a.topicid = i_topicid
LOOP
RETURN NEXT _rec;
END LOOP;

END;$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100; 
--GO

-- Function: {databaseSchema}.{objectQualifier}topic_lock(integer, boolean)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}topic_lock(integer, boolean);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}topic_lock(
						   i_topicid integer, 
						   i_locked boolean)
				  RETURNS void AS
$BODY$
BEGIN

	IF i_locked IS NOT FALSE THEN
		UPDATE {databaseSchema}.{objectQualifier}topic 
		SET flags = flags | 1
		WHERE topicid = i_topicid;
	ELSE
		UPDATE {databaseSchema}.{objectQualifier}topic 
		SET flags = flags & ~1
		WHERE topicid = i_topicid;
		END IF;
END;$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100; 
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}topic_move(
						   i_topicid integer, 
						   i_forumid integer, 
						   i_showmoved boolean,
						   i_linkdays integer,
						   i_utctimestamp timestamp)
				  RETURNS void AS
$BODY$DECLARE
			 ici_OldForumID integer;
			 ici_newTimestamp timestamp;
			 ici_addinterval interval := i_linkdays || ' day';
			 ici_movedtopicid integer;
BEGIN     
		if i_linkdays > -1 then		
		 ici_newTimestamp := i_utctimestamp + ici_addinterval;
		end if;
	 SELECT  forumid INTO ici_OldForumID FROM {databaseSchema}.{objectQualifier}topic 
	 WHERE topicid = i_topicid;
  IF ici_OldForumID != i_ForumID THEN 
	  IF i_ShowMoved THEN
		 /*create a moved message*/   
		 -- delete an old link IF exists 
		 delete from {databaseSchema}.{objectQualifier}topic where topicmovedid = i_topicid;

		 INSERT INTO {databaseSchema}.{objectQualifier}topic(forumid,userid,username,userdisplayname,posted,topic,views,flags,priority,pollid,topicmovedid,lastposted,numposts, linkdate)
		 SELECT forumid,userid,username,userdisplayname,posted,topic,0,flags,priority,pollid,i_TopicID,lastposted,0,ici_newTimestamp
		 FROM {databaseSchema}.{objectQualifier}topic where topicid = i_topicid returning topicid into ici_movedtopicid;		  
		 INSERT INTO {databaseSchema}.{objectQualifier}topictags(topicid,tagid) 
		 select ici_movedtopicid,tagid from {databaseSchema}.{objectQualifier}topictags WHERE topicid = i_topicid;
	 ELSE
		 DELETE FROM {databaseSchema}.{objectQualifier}topictags 
		 WHERE topicid = i_topicid;
	 END IF;
		 UPDATE {databaseSchema}.{objectQualifier}tags	set  tagcount = tagcount - 1 where tagid in (select tagid from 	{databaseSchema}.{objectQualifier}topictags WHERE topicid = i_topicid);
   
	-- move the topic 
	 UPDATE {databaseSchema}.{objectQualifier}topic SET forumid = i_forumid WHERE topicid = i_topicid;
	  
	-- update last posts 
	PERFORM {databaseSchema}.{objectQualifier}forum_updatelastpost(ici_OldForumID);
	PERFORM {databaseSchema}.{objectQualifier}forum_updatelastpost(i_forumid);
	 
	 -- update stats 
	  PERFORM {databaseSchema}.{objectQualifier}forum_updatestats (ici_OldForumID);
	 PERFORM {databaseSchema}.{objectQualifier}forum_updatestats (i_forumid);
  END IF;   
END;$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100;
--GO

DROP FUNCTION IF EXISTS {databaseSchema}.{objectQualifier}topic_poll_update(
						integer, 
						integer, integer);
-- GO
CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}topic_prune(
						   i_boardid integer,
						   i_forumid integer, 
						   i_days integer, 
						   i_deletedonly boolean,
						   i_utctimestamp timestamp)
				  RETURNS SETOF integer AS
$BODY$DECLARE
			 _rec RECORD;     
BEGIN 
   FOR _rec IN  SELECT yt.topicid
		FROM  {databaseSchema}.{objectQualifier}topic  yt
		INNER JOIN
		{databaseSchema}.{objectQualifier}forum yf
		ON
		yt.forumid = yf.forumid
		INNER JOIN
		{databaseSchema}.{objectQualifier}category yc
		ON
		yc.boardid = i_boardid AND
		yf.categoryid = yc.categoryid
		WHERE (i_forumid is null or forumid = i_forumid)
		AND yt.priority = 0
		AND (yt.flags & 512) = 0
		-- not flagged as persistent 
		AND (i_utctimestamp-CAST(lastposted AS date) > i_days)
		AND (i_deletedonly = 0 OR (yt.flags & 8) = 8)
	 LOOP
 RETURN NEXT _rec.topicid;
END LOOP;    
	 END;
$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100 ROWS 1000;
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}topic_save(
						   i_forumid integer,
						   i_subject varchar,
						   i_status varchar,
						   i_styles varchar,
						   i_description varchar,
						   i_userid integer,
						   i_message text,
						   i_priority smallint,
						   i_username varchar,
						   i_ip varchar,
						   i_posted timestamp,
						   i_blogpostid varchar,
						   i_flags integer,
						   i_messagedescription varchar,
						   i_tags text,
						   i_utctimestamp timestamp)
				 RETURNS {databaseSchema}.{objectQualifier}topic_save_return_type AS
$BODY$DECLARE
			 ici_topicid     integer;
			 ici_MessageID   integer;
			 ici_Posted	     timestamp :=i_posted;
			 ici_ReplyToNull integer;
			 ici_blogpostid varchar:=i_blogpostid;
			 _rec  {databaseSchema}.{objectQualifier}topic_save_return_type; 
			 ici_OverrideDisplayName boolean;				 
BEGIN 
	 
	 IF ici_blogpostid = '' 
	 THEN 
		 ici_blogpostid := null; 
	 END IF;

	 IF  ici_Posted IS NULL 
	 THEN
		 ici_Posted := i_utctimestamp; 
	 END IF;
	 SELECT EXISTS (SELECT 1 FROM {databaseSchema}.{objectQualifier}user WHERE userid = i_userid and name != i_username) INTO ici_OverrideDisplayName;
	 -- create the topic	
	 INSERT INTO {databaseSchema}.{objectQualifier}topic(forumid,topic,userid,posted,views,priority,username,userdisplayname,numposts, description, status, styles, flags)
	 VALUES(i_forumid,substr(i_subject, 1, 128),i_userid,ici_Posted,0,i_priority,(CASE WHEN ici_OverrideDisplayName is true THEN i_username ELSE (SELECT name FROM {databaseSchema}.{objectQualifier}user WHERE userid = i_userid) END),(CASE WHEN ici_OverrideDisplayName is true THEN i_username ELSE (SELECT displayname FROM {databaseSchema}.{objectQualifier}user WHERE userid = i_userid) END),0, i_description, i_status, i_styles, 0) RETURNING topicid INTO ici_topicid;
	   
	 -- add message to the topic 
	 SELECT {databaseSchema}.{objectQualifier}message_save(ici_topicid,i_userid,i_message,i_username,i_ip,ici_Posted,ici_ReplyToNull,ici_blogpostid,null, null,i_messagedescription,i_flags,i_utctimestamp) INTO ici_MessageID;
	
	 PERFORM {databaseSchema}.{objectQualifier}topic_tagsave(ici_topicid, i_tags);
	
	 SELECT   ici_topicid,  ici_MessageID INTO _rec;

 RETURN _rec;
	 END;
$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100; 
--GO

-- Function: {databaseSchema}.{objectQualifier}topic_list(integer, integer, integer, timestamp, integer, integer)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}topic_list(integer, integer, integer, timestamp, integer, integer, boolean);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}topic_bytags(
						   i_boardid integer,
						   i_forumid integer,
						   i_pageuserid integer,
						   i_tags varchar(1024),
						   i_sincedate timestamp,						   
						   i_pageindex integer,
						   i_pagesize integer)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}topic_bytags_rt AS
$BODY$DECLARE
			 ici_shiftsticky integer :=0;
			 ici_post_totalrowsnumber  integer;
			 ici_firstselectrownum integer;
			 ici_firstselectposted timestamp ;
			 ici_ceiling decimal;
			 ici_pageindex integer := i_pageindex;
			 ici_retcount integer := 0;
			 ici_counter integer := 0;
			 _rec {databaseSchema}.{objectQualifier}topic_bytags_rt%ROWTYPE;
BEGIN    
	   


		-- find total returned count including priority
		select
		COUNT(t.topicid) into ici_post_totalrowsnumber
	FROM {databaseSchema}.{objectQualifier}topic t 
	JOIN {databaseSchema}.{objectQualifier}topictags tt ON t.topicid = tt.topicid
	JOIN {databaseSchema}.{objectQualifier}activeaccess aa on (aa.forumid = t.forumid and aa.userid = i_pageuserid)
	WHERE aa.boardid = i_boardid and tt.tagid = i_tags::integer 
	and not t.isdeleted and	t.topicmovedid IS NULL and (i_forumid <=0 or t.forumid = i_forumid);

		 ici_pageindex := ici_pageindex+1;

		 ici_firstselectrownum := (ici_pageindex - 1) * i_pagesize;	

 FOR _rec IN
	SELECT 	
		t.forumid,
		t.topicid,
		t.posted,
		COALESCE(t.topicmovedid,t.topicid) AS LinkTopicID,
		t.topicmovedid,
		(SELECT COUNT(id) FROM {databaseSchema}.{objectQualifier}favoritetopic WHERE topicid = COALESCE(t.topicmovedid,t.topicid)),	
		t.topic AS Subject,		
		t.description,
		t.status,
		t.styles,
		t.userid,
		COALESCE(t.username,b.name) AS Starter,
		COALESCE(t.userdisplayname,b.displayname) AS StarterDisplay,
		t.numposts - 1 AS Replies,
		(SELECT COUNT(1) FROM
		 {databaseSchema}.{objectQualifier}message mes 
				 WHERE mes.topicid = t.topicid AND ((mes.flags & 8) = 8)
				 AND ((mes.flags & 16) = 16) 
				 AND ((i_pageuserid IS NOT NULL AND mes.userid = i_pageuserid) 
				 OR (i_pageuserid IS NULL)) ) AS NumPostsDeleted,
		t.views AS Views,
		t.lastposted AS LastPosted,
		t.lastuserid AS LastUserID,
		COALESCE(t.lastusername,
		(select x.name from {databaseSchema}.{objectQualifier}user x 
		where x.userid=t.lastuserid)) AS LastUserName,
		COALESCE(t.lastuserdisplayname,
		(select x.displayname from {databaseSchema}.{objectQualifier}user x 
		where x.userid=t.lastuserid)) AS LastUserDisplayName,
		t.lastmessageid AS LastMessageID,
		t.topicid AS LastTopicID,
		t.linkdate,
		t.flags AS TopicFlags,
		t.priority,
		t.pollid,
		d.flags AS ForumFlags,
		CAST((SELECT message 
				 FROM {databaseSchema}.{objectQualifier}message mes2 
				 WHERE mes2.topicid = COALESCE(t.topicmovedid,t.topicid) 
				 AND mes2.position = 0 ORDER BY mes2.topicid LIMIT 1) AS 
varchar(4000)) AS FirstMessage,
		(case(true)
			when true THEN  (SELECT us.userstyle FROM {databaseSchema}.{objectQualifier}user us where us.userid = t.userid)  
			else ''	 end)  AS StarterStyle , 
		(case(true)
			when true THEN  (SELECT us.userstyle FROM {databaseSchema}.{objectQualifier}user us where us.userid = t.lastuserid)  
			else ''	 end)  AS LastUserStyle,
		(case(false)
			 when true THEN
			   COALESCE((SELECT lastaccessdate FROM {databaseSchema}.{objectQualifier}forumreadtracking x WHERE x.forumid=d.forumid AND x.userid = i_pageuserid), current_timestamp)
			 else TIMESTAMP '-infinity' end) AS LastForumAccess,
		(case(false)
			 when true THEN
			   COALESCE((SELECT lastaccessdate FROM {databaseSchema}.{objectQualifier}topicreadtracking y WHERE y.topicid=t.topicid AND y.userid = i_pageuserid), current_timestamp)
			 else TIMESTAMP '-infinity' end) AS LastTopicAccess,		
		(SELECT tag FROM {databaseSchema}.{objectQualifier}tags where tagid = i_tags::integer limit 1),
		(SELECT string_agg(tag, ',') FROM {databaseSchema}.{objectQualifier}tags tg join {databaseSchema}.{objectQualifier}topictags tt on tt.tagid = tg.tagid where tt.topicid = t.topicid),
		t.topicimage,
		t.topicimagebin,
		t.topicimagetype,
		0 as HasAttachments, 	
		ici_post_totalrowsnumber AS TotalRows,
		ici_pageindex	AS 	PageIndex
		FROM
		{databaseSchema}.{objectQualifier}topic t 
		JOIN {databaseSchema}.{objectQualifier}user  b 
		ON b.UserID=t.UserID
		JOIN {databaseSchema}.{objectQualifier}rank r 
		ON r.rankid=b.rankid
		join {databaseSchema}.{objectQualifier}forum d on d.ForumID=t.ForumID	
	JOIN {databaseSchema}.{objectQualifier}topictags tt ON t.topicid = tt.topicid
	JOIN {databaseSchema}.{objectQualifier}activeaccess aa on (aa.forumid = t.forumid and aa.userid = i_pageuserid)
	WHERE aa.boardid = i_boardid and tt.tagid = i_tags::integer 
	and not t.isdeleted and	t.topicmovedid IS NULL and (i_forumid <=0 or t.forumid = i_forumid)
	order by
		 t.posted DESC		
			
LOOP
-- RETURN NEXT _rec;
ici_retcount := ici_retcount +1; 
IF (ici_retcount between  ici_firstselectrownum and ici_firstselectrownum+i_pagesize) THEN
RETURN NEXT _rec;
ici_counter := ici_counter + 1; 
END IF;
IF (ici_counter >= i_pagesize) THEN
EXIT;
END IF;
END LOOP; 		
END;
$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100
  ROWS 1000; 
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}topic_simplelist
						   (
						   i_startid integer, 
						   i_limit integer
						   )
				  RETURNS SETOF {databaseSchema}.{objectQualifier}topic_simplelist_return_type AS
$BODY$DECLARE
			 cntr integer:=0;
			 _rec {databaseSchema}.{objectQualifier}topic_simplelist_return_type%ROWTYPE;
BEGIN
	FOR _rec IN 
	SELECT   t.topicid,
	t.topic
	FROM     {databaseSchema}.{objectQualifier}topic t
	WHERE    t.topicid >= i_startid
	AND t.topicid < (i_startid + i_limit)
		 ORDER BY t.topicid
		 LOOP
			IF cntr >= i_startid AND cntr <=i_limit THEN 
			RETURN NEXT _rec;
			END IF;
		 EXIT WHEN cntr < i_startid OR cntr >= i_limit;
		 cntr:=cntr+1;
		 END LOOP;
	RETURN;         
END;$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100
  ROWS 1000;
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}topic_updatelastpost(
						   i_forumid integer, 
						   i_topicid integer)
				  RETURNS void AS
$BODY$DECLARE
			 ici_ForumID integer:=i_forumid;
			 ici_topicid integer:=i_topicid;
			 ici_lastposted timestamp ;
			 ici_LastMessageID integer;
			 ici_lastmessageflags integer;
			 ici_LastUserID integer;
			 ici_LastUserName varchar(128);
			 ici_LastUserDisplayName varchar(128);
BEGIN                      
IF ici_topicid IS NOT NULL THEN
		 SELECT DISTINCT x.posted,x.messageid, x.userid, x.username, x.userdisplayname, x.flags INTO
		 ici_lastposted,ici_LastMessageID,ici_LastUserID,ici_LastUserName,ici_LastUserDisplayName, ici_lastmessageflags
		 FROM    {databaseSchema}.{objectQualifier}message x 
		 INNER JOIN {databaseSchema}.{objectQualifier}topic t
		 ON x.topicid = t.topicid
		 WHERE  
		 (x.flags & 24) = 16 AND x.topicid = ici_topicid
							 ORDER BY x.posted DESC LIMIT 1;
									  
			UPDATE {databaseSchema}.{objectQualifier}topic
			   SET    lastposted = ici_lastposted,
					  lastmessageid=ici_LastMessageID,
					  lastuserid=ici_LastUserID,
					  lastusername=ici_LastUserName,
					  lastuserdisplayname=ici_LastUserDisplayName,
					  lastmessageflags=ici_lastmessageflags
				WHERE  topicid = ici_topicid;
ELSE
		SELECT DISTINCT x.posted,x.messageid, x.userid, x.username, x.userdisplayname, x.flags 
		INTO ici_lastposted,ici_LastMessageID,ici_LastUserID,ici_LastUserName, ici_LastUserDisplayName, ici_lastmessageflags
		FROM    {databaseSchema}.{objectQualifier}message x 
		INNER JOIN {databaseSchema}.{objectQualifier}topic t
		ON x.topicid = t.topicid
		WHERE  
		 (x.flags & 24) = 16 AND x.topicid = topicmovedid
							 ORDER BY x.posted DESC LIMIT 1;   
								   
		UPDATE {databaseSchema}.{objectQualifier}topic
		 SET    lastposted = ici_lastposted,
			lastmessageid=ici_LastMessageID,
			lastuserid=ici_LastUserID,
			lastusername=ici_LastUserName,
			lastuserdisplayname=ici_LastUserDisplayName,
			lastmessageflags=ici_lastmessageflags
	 WHERE  topicmovedid IS NULL
	 AND (ici_ForumID IS NULL
	 OR forumid = ici_ForumID);

 END IF;   
	 PERFORM {databaseSchema}.{objectQualifier}forum_updatelastpost(ici_ForumID);
	 RETURN;
	 END ;
$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100;
--GO 

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}topic_updatetopic(
						   i_topicid integer, 
						   i_topic  varchar)
				  RETURNS void AS
$BODY$
BEGIN
		IF i_topicid is not null THEN
		update {databaseSchema}.{objectQualifier}topic set
			topic = i_topic
		where topicid = i_topicid;
		END IF;
  RETURN;         
  END;$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100;
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}user_accessmasks(
						   i_boardid integer, 
						   i_userid integer)
				 RETURNS SETOF {databaseSchema}.{objectQualifier}user_accessmasks_return_type AS
$BODY$DECLARE
			 _rec {databaseSchema}.{objectQualifier}user_accessmasks_return_type%ROWTYPE;
BEGIN
FOR _rec IN
	 SELECT   accessmaskid,
			  accessmaskname,
			  forumid,
			  forumname,
			  categoryid,
			  parentid
	 FROM     ((SELECT   e.accessmaskid AS accessmaskid,
	 e.name AS accessmaskname,
	 f.forumid AS forumid,
	 f.name AS forumname,
	 f.categoryid,
	 f.parentid
	 FROM     {databaseSchema}.{objectQualifier}user a
	 JOIN {databaseSchema}.{objectQualifier}usergroup b
	 ON b.userid = a.userid
	 JOIN {databaseSchema}.{objectQualifier}group c
	 ON c.groupid = b.groupid
	 JOIN {databaseSchema}.{objectQualifier}forumaccess d
	 ON d.groupid = c.groupid
	 JOIN {databaseSchema}.{objectQualifier}accessmask e
	 ON e.accessmaskid = d.accessmaskid
	 JOIN {databaseSchema}.{objectQualifier}forum f
	 ON f.forumid = d.forumid
	 WHERE    a.userid = i_userid
	 AND c.boardid = i_boardid	
	 GROUP BY e.accessmaskid,e.name,f.categoryid,f.parentid,f.forumid,f.name)
	 UNION
	 (SELECT   c.accessmaskid AS accessmaskid,
	 c.name AS accessmaskname,
	 d.forumid AS forumid,
	 d.name AS  forumname,
	 d.categoryid,
	 d.parentid
	 FROM     {databaseSchema}.{objectQualifier}user a
	 JOIN {databaseSchema}.{objectQualifier}userforum b
	 ON b.userid = a.userid
	 JOIN {databaseSchema}.{objectQualifier}accessmask c
	 ON c.accessmaskid = b.accessmaskid
	 JOIN {databaseSchema}.{objectQualifier}forum d
	 ON d.forumid = b.forumid
	 WHERE    a.userid = i_userid
	 AND c.boardid = i_boardid
	 GROUP BY c.accessmaskid,c.name,d.categoryid,d.parentid,d.forumid,d.name)) AS x
	 ORDER BY forumname,
	 accessmaskname
LOOP
RETURN NEXT _rec;
END LOOP;

	 END;   $BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100
  ROWS 1000; 
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}user_accessmasksbygroup(
						   i_boardid integer, 
						   i_userid integer)
				 RETURNS SETOF {databaseSchema}.{objectQualifier}user_accessmasks_by_type AS
$BODY$DECLARE
			 _rec {databaseSchema}.{objectQualifier}user_accessmasks_by_type%ROWTYPE;
BEGIN
FOR _rec IN
SELECT
	 e.accessmaskid AS accessmaskid,
	 e.name AS accessmaskname,
	 e.flags AS accessmaskflags,
	 false as IsUserMask,
	 f.forumid AS forumid,
	 f.name AS forumname,
	 f.categoryid,
	 f.parentid,
	 c.groupid,
	 c.name
	 FROM     {databaseSchema}.{objectQualifier}user a
	 JOIN {databaseSchema}.{objectQualifier}usergroup b
	 ON b.userid = a.userid
	 JOIN {databaseSchema}.{objectQualifier}group c
	 ON c.groupid = b.groupid
	 JOIN {databaseSchema}.{objectQualifier}forumaccess d
	 ON d.groupid = c.groupid
	 JOIN {databaseSchema}.{objectQualifier}accessmask e
	 ON e.accessmaskid = d.accessmaskid
	 JOIN {databaseSchema}.{objectQualifier}forum f
	 ON f.forumid = d.forumid
	 JOIN {databaseSchema}.{objectQualifier}category cat
	 ON cat.categoryid = f.categoryid
	 WHERE    a.userid = i_userid
	 AND c.boardid = i_boardid	    
LOOP
RETURN NEXT _rec;
END LOOP;

	 END;   $BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100
  ROWS 1000; 
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}user_accessmasksbyforum(
						   i_boardid integer, 
						   i_userid integer)
				 RETURNS SETOF {databaseSchema}.{objectQualifier}user_accessmasks_by_type AS
$BODY$DECLARE
			 _rec {databaseSchema}.{objectQualifier}user_accessmasks_by_type%ROWTYPE;
BEGIN
FOR _rec IN
	 SELECT   c.accessmaskid AS accessmaskid,
	 c.name AS accessmaskname,
	 c.flags AS accessmaskflags,
	 true as IsUserMask,
	 d.forumid AS forumid,
	 d.name AS  forumname,
	 d.categoryid,
	 d.parentid,
	 0,
	 ''
	 FROM     {databaseSchema}.{objectQualifier}user a
	 JOIN {databaseSchema}.{objectQualifier}userforum b
	 ON b.userid = a.userid
	 JOIN {databaseSchema}.{objectQualifier}accessmask c
	 ON c.accessmaskid = b.accessmaskid
	 JOIN {databaseSchema}.{objectQualifier}forum d
	 ON d.forumid = b.forumid
	 JOIN {databaseSchema}.{objectQualifier}category cat
	 ON cat.categoryid = d.categoryid
	 WHERE    a.userid = i_userid
	 AND c.boardid = i_boardid 
	 UNION
	 SELECT
	 e.accessmaskid AS accessmaskid,
	 e.name AS accessmaskname,
	 e.flags AS accessmaskflags,
	 false as IsUserMask,
	 f.forumid AS forumid,
	 f.name AS forumname,
	 f.categoryid,
	 f.parentid,
	 c.groupid,
	 c.name
	 FROM     {databaseSchema}.{objectQualifier}user a
	 JOIN {databaseSchema}.{objectQualifier}usergroup b
	 ON b.userid = a.userid
	 JOIN {databaseSchema}.{objectQualifier}group c
	 ON c.groupid = b.groupid
	 JOIN {databaseSchema}.{objectQualifier}forumaccess d
	 ON d.groupid = c.groupid
	 JOIN {databaseSchema}.{objectQualifier}accessmask e
	 ON e.accessmaskid = d.accessmaskid
	 JOIN {databaseSchema}.{objectQualifier}forum f
	 ON f.forumid = d.forumid
	 JOIN {databaseSchema}.{objectQualifier}category cat
	 ON cat.categoryid = f.categoryid
	 WHERE    a.userid = i_userid
	 AND c.boardid = i_boardid	     
LOOP
RETURN NEXT _rec;
END LOOP;

	 END;   $BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100
  ROWS 1000; 
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}user_activity_rank(
						   i_boardid integer, 
						   i_displaynumber integer, 
						   i_startdate timestamp )
				  RETURNS SETOF {databaseSchema}.{objectQualifier}user_activity_rank_return_type AS
$BODY$DECLARE
			 cntr integer:=0;
			 _rec {databaseSchema}.{objectQualifier}user_activity_rank_return_type%ROWTYPE;
			 ici_GuestUserID integer;
			 ici_AllIntervalPostCount integer;
BEGIN 
	
	SELECT 
		a.userid INTO ici_GuestUserID
	FROM
		{databaseSchema}.{objectQualifier}user a
		INNER JOIN {databaseSchema}.{objectQualifier}usergroup b on b.userid = a.userid
		INNER JOIN {databaseSchema}.{objectQualifier}group c on b.groupid = c.groupid
	WHERE
		a.boardid = i_boardid and
		(c.flags & 2)<>0 GROUP BY a.userid LIMIT 1 ;
	   SELECT  Count(m.userid) INTO ici_AllIntervalPostCount FROM {databaseSchema}.{objectQualifier}message m
			WHERE m.posted >= i_startdate;

FOR _rec IN
   SELECT
	counter.id,
	u.name,
	u.displayname,
	u.joined,
	u.userstyle,
	u.isactiveexcluded,
	counter."NumOfPosts",
	ici_AllIntervalPostCount as NumOfAllIntervalPosts
	FROM
	{databaseSchema}.{objectQualifier}user u inner join
	(
	SELECT m.userid as ID, Count(m.userid) as "NumOfPosts" 
	FROM {databaseSchema}.{objectQualifier}message m
	WHERE m.posted >= i_startdate
	GROUP BY m.userid
	) AS counter ON u.userid = counter.id
	WHERE
	u.boardid =i_boardid and u.userid != ici_GuestUserID
	ORDER BY
	"NumOfPosts" DESC
			 LOOP
RETURN NEXT _rec;
EXIT WHEN cntr >= i_displaynumber;
cntr:=cntr+1;
END LOOP;       
   
	END;
$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100
  ROWS 1000;
--GO
 
CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}user_addpoints(
						   i_userid integer,
						   i_fromuserid integer,
						   i_utctimestamp timestamp,
						   i_points integer)
				  RETURNS void AS
$BODY$
DECLARE _votedate timestamp;
BEGIN 
UPDATE {databaseSchema}.{objectQualifier}user
	 SET    points = points + i_points
	 WHERE  userid = i_userid;
	 IF i_fromuserid IS NOT NULL then
	select votedate into _votedate from {databaseSchema}.{objectQualifier}reputationvote where reputationfromuserid=i_fromuserid AND reputationtouserid=i_userid;
	IF _votedate is not null then		     
		  update {databaseSchema}.{objectQualifier}reputationvote set votedate=i_utctimestamp 
		  where votedate = _votedate AND reputationfromuserid=i_fromuserid AND reputationtouserid=i_userid;
	eLSE
	  
		  insert into {databaseSchema}.{objectQualifier}reputationvote(reputationfromuserid,reputationtouserid,votedate)
		  values (i_fromuserid, i_userid, i_utctimestamp);
	  end if;
	END IF;
END;
$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100;
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}user_adminsave(
						   i_boardid integer,
						   i_userid integer,
						   i_name varchar,
						   i_displayname varchar,
						   i_email varchar,
						   i_flags integer,
						   i_rankid integer)
				  RETURNS {databaseSchema}.{objectQualifier}user_adminsave_return_type AS
$BODY$DECLARE
			 _rec {databaseSchema}.{objectQualifier}user_adminsave_return_type;
 BEGIN
 SELECT i_userid INTO _rec;
	 UPDATE {databaseSchema}.{objectQualifier}user
	 SET
	 name = i_name,
	 displayname = i_displayname,
	 email = i_email,
	 rankid = i_rankid,
	 flags = i_flags    
	 WHERE userid = i_userid;     
 RETURN _rec;
	 END;
$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100;
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}user_approve(
						   i_userid integer)
				  RETURNS boolean AS
$BODY$DECLARE
			 ici_CheckEmailID integer;
			 ici_email varchar;
			 ici_bit boolean:=true;
BEGIN
		  SELECT
	 checkemailid,
	 email
	 INTO ici_CheckEmailID,ici_email
	 FROM
	 {databaseSchema}.{objectQualifier}checkemail
	 WHERE
	 userid = i_userid;

	 /*Update new user email*/
	 UPDATE {databaseSchema}.{objectQualifier}user SET email = ici_email, 
flags = flags | 2 WHERE userid = i_userid;
	 DELETE FROM {databaseSchema}.{objectQualifier}checkemail WHERE checkemailid = ici_CheckEmailID;
	 RETURN ici_bit;
	 END;$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100;  
--GO



CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}user_approveall(
						   i_boardid integer)
				  RETURNS void AS
$BODY$DECLARE
			 ici_userid integer;            
			 _refUserID integer;
			 _userslist refcursor;

BEGIN
	OPEN _userslist FOR SELECT userid 
	FROM {databaseSchema}.{objectQualifier}user 
	WHERE boardid=i_boardid AND isapproved IS FALSE ;
	LOOP
		  FETCH _userslist INTO _refUserID;
		  EXIT WHEN NOT FOUND;
		  EXECUTE {databaseSchema}.{objectQualifier}user_approve(_refUserID);           
		EXIT WHEN NOT FOUND; 
		END LOOP; 
	CLOSE _userslist;
 RETURN;
END;$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100;
--GO


CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}user_aspnet(
						   i_boardid integer,
						   i_username varchar, 
						   i_displayname varchar,
						   i_email varchar, 
						   i_provideruserkey varchar,
						   i_isapproved boolean,
						   i_utctimestamp timestamp)
				  RETURNS integer AS
$BODY$DECLARE 
			 ici_userid integer;
			 ici_rankid integer;
			 ici_displayname varchar :=i_displayname;
			 ici_approvedFlag integer:=0;
BEGIN
	ici_approvedFlag := 0;
	IF (i_isapproved IS TRUE) THEN ici_approvedFlag := 2;END IF;	
	
	IF EXISTS(SELECT 1 FROM {databaseSchema}.{objectQualifier}user 
				  WHERE boardid=i_boardid 
				  AND ((provideruserkey = i_provideruserkey) OR (name = i_username))) THEN
	
		SELECT DISTINCT userid INTO ici_userid 
		from {databaseSchema}.{objectQualifier}user 
		where boardid=i_boardid 
		and ((provideruserkey=i_provideruserkey) OR (name = i_username)) GROUP BY userid LIMIT 1;
		
		IF (ici_displayname IS NULL) THEN		
			SELECT COALESCE(displayname, name) INTO ici_displayname 
			FROM {databaseSchema}.{objectQualifier}user 
			WHERE userid = ici_userid ORDER BY userid desc LIMIT 1;
		END IF;
		
		UPDATE {databaseSchema}.{objectQualifier}user SET 
			name = i_username,
			email = i_email,
			flags = flags | ici_approvedFlag,
			displayname = COALESCE(ici_displayname,i_username)
		WHERE
			userid = ici_userid;
			
	ELSE
	
		SELECT rankid INTO ici_rankid FROM {databaseSchema}.{objectQualifier}rank 
				  WHERE (flags & 1)<>0 AND boardid=i_boardid;

		IF (i_displayname IS NULL) THEN	
			 ici_displayname := i_username;
		END	IF;	
		
		INSERT INTO {databaseSchema}.{objectQualifier}user(boardid,rankid,name,displayname,password,email,joined,lastvisit,numposts,timezone,flags,provideruserkey)
		VALUES(i_boardid,ici_rankid,i_username,COALESCE(ici_displayname,i_username),'-',i_email,i_utctimestamp,i_utctimestamp,0,COALESCE((SELECT value FROM  {databaseSchema}.{objectQualifier}registry where name LIKE 'timezone' and boardid=i_boardid),(SELECT value FROM  {databaseSchema}.{objectQualifier}registry where name LIKE 'timezone'))::varchar::int,ici_approvedFlag,i_provideruserkey) returning userid into ici_userid;
	  -- SELECT CURRVAL(pg_get_serial_sequence('{databaseSchema}.{objectQualifier}user','userid')) INTO ici_userid;                 
				 
	END IF;                          

RETURN  ici_userid;
  END;$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100;
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}user_deleteavatar(
						   i_userid integer)
				  RETURNS void AS
'UPDATE {databaseSchema}.{objectQualifier}user
	SET avatarimage = null,
	avatar = null,
	avatarimagetype = null
	where userid = $1;'
  LANGUAGE 'sql' VOLATILE SECURITY DEFINER
  COST 100;
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}user_avatarimage(
						   i_userid integer)
				  RETURNS {databaseSchema}.{objectQualifier}user_avatarimage_return_type AS
$BODY$
				  SELECT 
				  userid,
				  avatarimage,
				  avatarimagetype
				  FROM {databaseSchema}.{objectQualifier}user 
				  WHERE userid=$1;
$BODY$
  LANGUAGE 'sql' STABLE SECURITY DEFINER
  COST 100;
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}user_changepassword(
						   i_userid integer, 
						   i_oldpassword varchar, 
						   i_newpassword varchar)
				  RETURNS {databaseSchema}.{objectQualifier}user_changepassword_return_type AS
$BODY$DECLARE 
			 ici_CurrentOld varchar(32);
			 ici_Success boolean:=true;
			 _rec {databaseSchema}.{objectQualifier}user_changepassword_return_type;
BEGIN
				 
	 SELECT  password 
	 INTO ici_CurrentOld 
	 FROM {databaseSchema}.{objectQualifier}user 
	 WHERE userid = i_userid;
	 IF ici_CurrentOld<>i_oldpassword THEN
	  ici_Success:=FALSE;  	 	
		ELSE 
	UPDATE {databaseSchema}.{objectQualifier}user 
	SET password = i_newpassword 
	WHERE userid = i_userid; 	
		END IF;
SELECT ici_Success INTO _rec;
 RETURN _rec;
END;
$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100;
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}user_delete(
						   i_userid integer)
				  RETURNS void AS
$BODY$DECLARE
			 ici_GuestUserID	integer;
			 ici_UserName	varchar(128);
			 ici_userdisplayname	varchar(128);
			 ici_GuestCount	integer;

BEGIN	
 
	SELECT name,displayname 
	INTO ici_UserName, ici_userdisplayname
	FROM {databaseSchema}.{objectQualifier}user WHERE userid=i_userid;
 
	SELECT 
		 a.userid INTO ici_GuestUserID
	FROM
		{databaseSchema}.{objectQualifier}user a
		inner join {databaseSchema}.{objectQualifier}usergroup b on b.userid = a.userid
		inner join {databaseSchema}.{objectQualifier}group c on b.groupid = c.groupid
	WHERE
		(c.flags & 2)<>0 ORDER BY a.userid LIMIT 1;
 
	SELECT 
		  COUNT(1) INTO  ici_GuestCount
	FROM 
		{databaseSchema}.{objectQualifier}usergroup a
		join {databaseSchema}.{objectQualifier}group b on b.groupid=a.groupid
	WHERE
		(b.flags & 2)<>0;

	IF NOT (ici_GuestUserID = i_userid AND ici_GuestCount = 1) THEN

	UPDATE {databaseSchema}.{objectQualifier}message SET username=ici_UserName,userdisplayname=ici_userdisplayname,userid=ici_GuestUserID,editedby = ici_GuestUserID WHERE userid = i_userid;
	UPDATE {databaseSchema}.{objectQualifier}topic SET username=ici_UserName,userdisplayname=ici_userdisplayname,userid=ici_GuestUserID WHERE userid = i_userid;
	UPDATE {databaseSchema}.{objectQualifier}topic SET lastusername=ici_UserName,userdisplayname=ici_userdisplayname,lastuserid=ici_GuestUserID WHERE lastuserid = i_userid;
	UPDATE {databaseSchema}.{objectQualifier}forum SET lastusername=ici_UserName,lastuserdisplayname=ici_userdisplayname,lastuserid=ici_GuestUserID WHERE lastuserid = i_userid;

	DELETE FROM {databaseSchema}.{objectQualifier}activeaccess WHERE userid = i_userid;
	DELETE FROM {databaseSchema}.{objectQualifier}active WHERE userid = i_userid;
	DELETE FROM {databaseSchema}.{objectQualifier}eventlog WHERE userid = i_userid;
	DELETE FROM {databaseSchema}.{objectQualifier}userpmessage WHERE userid = i_userid;
	DELETE FROM {databaseSchema}.{objectQualifier}pmessage
	WHERE fromuserid = i_userid
	AND pmessageid NOT IN (SELECT pmessageid FROM {databaseSchema}.{objectQualifier}pmessage);
	
	/*set messages as from guest so the User can be deleted*/
	UPDATE {databaseSchema}.{objectQualifier}pmessage 
	SET fromuserid = ici_GuestUserID WHERE fromuserid = i_userid;
	DELETE FROM {databaseSchema}.{objectQualifier}checkemail WHERE userid = i_userid;
	DELETE FROM {databaseSchema}.{objectQualifier}watchtopic WHERE userid = i_userid;
	DELETE FROM {databaseSchema}.{objectQualifier}watchforum WHERE userid = i_userid;
	DELETE FROM {databaseSchema}.{objectQualifier}watchforum WHERE userid = i_userid;
	DELETE FROM {databaseSchema}.{objectQualifier}topicreadtracking WHERE userid = i_userid;
	DELETE FROM {databaseSchema}.{objectQualifier}forumreadtracking  WHERE userid = i_userid;
	DELETE FROM {databaseSchema}.{objectQualifier}reputationvote WHERE reputationfromuserid = i_userid;	
	DELETE FROM {databaseSchema}.{objectQualifier}usergroup WHERE userid = i_userid;  
	DELETE FROM  {databaseSchema}.{objectQualifier}userforum WHERE userID = i_userid; 
	DELETE FROM {databaseSchema}.{objectQualifier}ignoreuser WHERE userid = i_userid;
	DELETE FROM {databaseSchema}.{objectQualifier}ignoreuser WHERE ignoreduserid = i_userid;
	-- Delete all the thanks entries associated with this UserID.
	DELETE FROM {databaseSchema}.{objectQualifier}thanks WHERE thanksfromuserid = i_userid;
	DELETE FROM {databaseSchema}.{objectQualifier}thanks WHERE thankstouserid = i_userid;
	-- Delete all the Buddy relations between this user and other users.
	DELETE FROM {databaseSchema}.{objectQualifier}buddy where fromuserid = i_userid; 
	DELETE FROM {databaseSchema}.{objectQualifier}buddy where touserid = i_userid;
	
	-- Delete all the FavoriteTopic entries associated with this UserID.
	delete from {databaseSchema}.{objectQualifier}favoritetopic where userid = i_userid; 
	DELETE FROM  {databaseSchema}.{objectQualifier}user WHERE userid = i_userid;
	
	END IF;
	END;$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100;
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}user_deleteold(
						   i_boardid integer, 
						   i_days integer,
						   i_utctimestamp timestamp)
				  RETURNS void AS
$BODY$DECLARE 
			 ici_Since timestamp :=i_utctimestamp;
BEGIN  

	DELETE FROM {databaseSchema}.{objectQualifier}eventlog  
	WHERE userid IN 
	(SELECT userid from {databaseSchema}.{objectQualifier}user 
	WHERE boardid=i_boardid and isapproved IS FALSE
	and date_part('day', (ici_Since-joined))>i_days);  
	  
	DELETE FROM {databaseSchema}.{objectQualifier}checkemail 
	WHERE userid IN 
	(SELECT userid from {databaseSchema}.{objectQualifier}user 
	WHERE boardid=i_boardid 
	AND  isapproved IS FALSE 
	AND date_part('day', ici_Since-joined)>i_days);
   
	DELETE FROM {databaseSchema}.{objectQualifier}usergroup 
	WHERE userid IN 
	(SELECT userid from {databaseSchema}.{objectQualifier}user 
	WHERE boardid=i_boardid 
	AND  isapproved IS FALSE 
	and date_part('day', ici_Since-joined)>i_days);
	
	DELETE FROM {databaseSchema}.{objectQualifier}user 
	WHERE boardid=i_boardid 
	AND  isapproved IS FALSE 
	and date_part('day', ici_Since-joined)>i_days;
	END;$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100;
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}user_emails(
						   i_boardid integer, 
						   i_groupid integer)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}user_emails_return_type AS
$BODY$DECLARE
			 _rec {databaseSchema}.{objectQualifier}user_emails_return_type%ROWTYPE;
 BEGIN   
	IF i_groupid IS NULL THEN
FOR _rec IN
	SELECT
	a.email
	FROM
	{databaseSchema}.{objectQualifier}user a
	WHERE
	a.email IS NOT NULL AND
	a.boardid = i_boardid and
	a.email IS NOT NULL AND
	a.email<>''
LOOP
RETURN NEXT _rec;
END LOOP;
	ELSE
FOR _rec IN
		SELECT 
			a.email 
		FROM
			{databaseSchema}.{objectQualifier}user a 
			JOIN {databaseSchema}.{objectQualifier}usergroup b 
			ON b.userid=a.userid
			JOIN {databaseSchema}.{objectQualifier}group c 
			ON c.groupid=b.groupid		
		WHERE 
			b.groupid = i_groupid AND
			(c.flags & 2)=0 AND 
			a.email IS NOT NULL AND 
			a.email<>''
LOOP
RETURN NEXT _rec;
END LOOP;
		END IF;

END;
$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100;
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}user_find(
						   i_boardid integer,
						   i_filter boolean,
						   i_username varchar,
						   i_email varchar,
						   i_displayname varchar,
						   i_notificationtype integer,
						   i_dailydigest boolean)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}user_find_return_type 
AS
$BODY$DECLARE
			 ici_UserName varchar(128):=i_username;
			 ici_displayname varchar(128) :=i_displayname;
			 _rec {databaseSchema}.{objectQualifier}user_find_return_type%ROWTYPE;
BEGIN
	IF (i_filter IS TRUE) THEN 	
		IF ici_UserName IS NOT NULL THEN
			ici_UserName := '.*' || ici_UserName || '.*';  
		END IF;
		IF (ici_displayname is not null) THEN
			ici_displayname := '.*' || ici_displayname || '.*';  
		END IF;		

 FOR _rec IN
		SELECT 
			a.userid,    
			a.boardid,    
			a.provideruserkey::varchar(36),    
			a.name,    
			a.password,    
			a.email,    
			a.joined,    
			a.lastvisit,    
			a.ip,    
			a.numposts,    
			a.timezone,    
			a.avatar,    
			a.signature,    
			a.avatarimage,    
			a.avatarimagetype,    
			a.rankid,    
			a.suspended,    
			a.languagefile,    
			a.themefile,
			a.overridedefaultthemes,    
			a.pmnotification,    
			a.flags,    
			a.points,
			a.autowatchtopics,
			a.displayname,
			a.culture,
			a.culture,
			a.dailydigest,
			a.notificationtype,
			a.texteditor,
			a.usesinglesignon,			
			COALESCE((a.flags & 2)= 2,false) AS isapproved,
			COALESCE((a.flags & 16)=16,false) AS isactiveexcluded,
			EXISTS (SELECT x.groupid FROM {databaseSchema}.{objectQualifier}usergroup x 
			 join {databaseSchema}.{objectQualifier}group y ON x.groupid=y.groupid 
			 WHERE x.userid=a.userid AND (y.flags & 2)=2 LIMIT 1) AS IsGuest,
			EXISTS (SELECT x.groupid FROM {databaseSchema}.{objectQualifier}usergroup x 
			 join {databaseSchema}.{objectQualifier}group y ON x.groupid=y.groupid 
			 WHERE x.userid=a.userid AND (y.flags & 1)=1 LIMIT 1) AS IsAdmin
		FROM 
			{databaseSchema}.{objectQualifier}user a
		WHERE 
			a.boardid = i_boardid AND
			((ici_UserName IS NOT NULL and a.name 
			~* ici_UserName) OR 
			(i_Email IS NOT NULL and a.email LIKE i_email) 
			or (ici_displayname is not null and a.displayname ~* ici_displayname) 
			or (i_notificationtype is not null and a.notificationtype = i_notificationtype) or
			(i_dailydigest is not null and a.dailydigest = i_dailydigest))	
		ORDER BY
			a.name
				LOOP
				  RETURN NEXT _rec;
				END LOOP;
	ELSE 	
	   FOR _rec IN
		SELECT
			a.userid,    
			a.boardid,    
			a.provideruserkey::varchar(36),    
			a.name,    
			a.password,    
			a.email,    
			a.joined,    
			a.lastvisit,    
			a.ip,    
			a.numposts,    
			a.timezone,    
			a.avatar,    
			a.signature,    
			a.avatarimage,    
			a.avatarimagetype,    
			a.rankid,    
			a.suspended,    
			a.languagefile,    
			a.themefile,
			a.overridedefaultthemes,    
			a.pmnotification,    
			a.flags,    
			a.points,
			a.autowatchtopics,
			a.displayname,
			a.culture,
			a.culture,
			a.dailydigest,
			a.notificationtype,
			a.texteditor,
			a.usesinglesignon,						
			COALESCE((a.flags & 2)= 2,false) AS isapproved,
			COALESCE((a.flags & 16)=16,false) AS isactiveexcluded,
			EXISTS (SELECT x.groupid FROM {databaseSchema}.{objectQualifier}usergroup x 
			join {databaseSchema}.{objectQualifier}group y ON x.groupid=y.groupid 
			WHERE x.userid=a.userid AND (y.flags & 2)=2 LIMIT 1) AS IsGuest,
			EXISTS (SELECT x.groupid FROM {databaseSchema}.{objectQualifier}usergroup x 
			join {databaseSchema}.{objectQualifier}group y ON x.groupid=y.groupid 
			WHERE x.userid=a.userid AND (y.flags & 1)=1 LIMIT 1) AS IsAdmin	
		FROM 
			{databaseSchema}.{objectQualifier}user a
		WHERE
			a.boardid = i_boardid AND
			((ici_UserName IS NOT NULL AND a.name=ici_UserName) 
			OR (i_Email IS NOT NULL AND a.email=i_email) or 
			(ici_displayname is not null and a.displayname = ici_displayname)
			or	(i_notificationtype is not null and a.notificationtype = i_notificationtype) or
			(i_dailydigest is not null and a.dailydigest = i_dailydigest))
				LOOP
				   RETURN NEXT _rec;
				END LOOP;
	 END IF;
END;
$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100
  ROWS 1000;
--GO  

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}user_savenotification(
						   i_userid	            integer,
						   i_pmnotification		boolean,
						   i_autowatchtopics       boolean,
						   i_notificationtype	    integer,
						   i_dailydigest		    boolean)
				  RETURNS void AS
$BODY$
BEGIN
		UPDATE
			{databaseSchema}.{objectQualifier}user
		SET
			pmnotification = (CASE WHEN (i_pmnotification is not null) THEN  i_pmnotification ELSE pmnotification END),
			autowatchtopics = (CASE WHEN (i_autowatchtopics is not null) THEN  i_autowatchtopics ELSE autowatchtopics END),
			notificationtype =  (CASE WHEN (i_notificationtype is not null) THEN  i_notificationtype ELSE notificationtype END),
			dailydigest = (CASE WHEN (i_dailydigest is not null) THEN  i_dailydigest ELSE dailydigest END)
		WHERE
			userid = i_userid;
END;
$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100;
--GO  

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}user_get(
						   i_boardid integer, 
						   i_provideruserkey varchar)
				  RETURNS {databaseSchema}.{objectQualifier}user_get_return_type AS
$BODY$
	SELECT userid FROM {databaseSchema}.{objectQualifier}user 
		WHERE boardid=$1 
		AND provideruserkey=$2;          
$BODY$
  LANGUAGE 'sql' STABLE SECURITY DEFINER
  COST 100;
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}user_getpoints(
						   i_userid integer)
				  RETURNS {databaseSchema}.{objectQualifier}user_getpoints_return_type AS
$BODY$
	SELECT points FROM {databaseSchema}.{objectQualifier}user WHERE userid = $1;
$BODY$
  LANGUAGE 'sql' STABLE SECURITY DEFINER
  COST 100;
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}user_getsignature(
						   i_userid integer)
				  RETURNS  {databaseSchema}.{objectQualifier}user_getsignature_return_type AS
$BODY$
	SELECT signature FROM {databaseSchema}.{objectQualifier}user 
	WHERE userid = $1;
$BODY$
  LANGUAGE 'sql' STABLE SECURITY DEFINER
  COST 100;  
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}user_guest
						   (
						   i_boardid integer
						   )
				  RETURNS integer AS
'SELECT 
		a.userid 
	FROM
		{databaseSchema}.{objectQualifier}user a
		INNER JOIN {databaseSchema}.{objectQualifier}usergroup b ON b.userid = a.userid
		INNER JOIN {databaseSchema}.{objectQualifier}group c ON b.groupid = c.groupid
	WHERE
		a.boardid = $1 AND
		(c.flags & 2)<>0 ORDER BY a.userid ASC LIMIT 1;'
  LANGUAGE 'sql' STABLE SECURITY DEFINER
  COST 100;   
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}user_ignoredlist
						   (
						   i_userid integer
						   )
				  RETURNS SETOF {databaseSchema}.{objectQualifier}ignoreuser_return_type AS
$BODY$DECLARE
			 _rec {databaseSchema}.{objectQualifier}ignoreuser_return_type%ROWTYPE;
BEGIN
FOR _rec IN
	SELECT * FROM {databaseSchema}.{objectQualifier}ignoreuser
		WHERE userid = i_userid 
		LOOP       
		RETURN  NEXT _rec; 
		END LOOP;      
END;$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100 ROWS 1000;
--GO
-- drop an old variant
DROP FUNCTION IF EXISTS {databaseSchema}.{objectQualifier}user_list
						(
						integer, 
						integer, 
						boolean, 
						integer, 
						integer);
--GO
CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}user_list(
						   i_boardid integer,
						   i_userid integer,
						   i_approved boolean, 
						   i_groupid integer,
						   i_rankid integer, 
						   i_stylednicks boolean,
						   i_utctimestamp timestamp)
				 RETURNS SETOF {databaseSchema}.{objectQualifier}user_list_return_type AS
$BODY$DECLARE
			 _rec {databaseSchema}.{objectQualifier}user_list_return_type%ROWTYPE;
BEGIN
	IF i_userid IS NOT NULL THEN
FOR _rec IN
		SELECT
			a.userid,    
			a.boardid,    
			a.provideruserkey::varchar(36),    
			a.name,    
			a.password,    
			a.email,    
			a.joined,    
			a.lastvisit,    
			a.ip,    
			a.numposts,    
			a.timezone,    
			a.avatar,    
			a.signature,    
			a.avatarimage,    
			a.avatarimagetype,    
			a.rankid,    
			a.suspended,    
			a.languagefile,    
			a.themefile,
			a.overridedefaultthemes,    
			a.pmnotification,    
			a.flags,    
			a.points,
			a.autowatchtopics,
			a.displayname,
			a.culture,
			a.culture,
			a.dailydigest,
			a.notificationtype,
			a.isfacebookuser,
			a.istwitteruser,
			a.texteditor,
			a.usesinglesignon,
			a.isapproved,
			a.isactiveexcluded,
			a.topicsperpage,
			a.postsperpage,
			b.name AS RankName,
			(case(i_stylednicks)
			when true THEN  a.userstyle 
			else ''	 end),  			
			date_part('day', i_utctimestamp - a.joined)+1 AS NumDays,
			(SELECT COUNT(1) FROM {databaseSchema}.{objectQualifier}message x WHERE x.isapproved is true and x.isdeleted is false)::integer AS NumPostsForum,
						COALESCE((SELECT SIGN(1)  FROM {databaseSchema}.{objectQualifier}user x  
						  WHERE x.userid=a.userid 
							AND x.avatarimage IS NOT NULL limit 1),0) AS HasAvatarImage, 
			COALESCE({databaseSchema}.{objectQualifier}int_to_bool_helper(a.flags & 4),FALSE) AS IsGuest,
			COALESCE((a.flags & 1)=1,false)::integer AS IsHostAdmin,
			0,
			false,
			false
			/*COALESCE({databaseSchema}.{objectQualifier}int_to_bool_helper(c.isadmin),false) AS IsAdmin,
			COALESCE({databaseSchema}.{objectQualifier}int_to_bool_helper(c.isforummoderator),false) AS IsForumModerator,
			COALESCE({databaseSchema}.{objectQualifier}int_to_bool_helper(c.ismoderator),false) AS IsModerator,
			c.forumid*/
		FROM 
			{databaseSchema}.{objectQualifier}user a
			JOIN {databaseSchema}.{objectQualifier}rank b ON b.rankid=a.rankid
		/*	LEFT JOIN {databaseSchema}.{objectQualifier}vaccess c ON c.userid=a.userid */
		WHERE 
			a.userid = i_userid AND
			a.boardid = i_boardid AND
			/*COALESCE(c.forumid,0) = 0 AND */
			(i_approved IS NULL OR (i_approved IS NOT TRUE AND a.isapproved IS FALSE) 
							OR (i_approved IS TRUE and a.isapproved IS TRUE))
		ORDER BY 
			a.name

LOOP
SELECT 
COALESCE({databaseSchema}.{objectQualifier}int_to_bool_helper("IsForumModerator"),false),
COALESCE({databaseSchema}.{objectQualifier}int_to_bool_helper("IsModerator"),false)
INTO _rec."IsForumModerator",_rec."IsModerator"  FROM {databaseSchema}.{objectQualifier}vaccess_combo(i_userid, null) LIMIT 1; 
RETURN NEXT _rec;
END LOOP;
	ELSEIF i_groupid IS NULL and i_rankid IS NULL THEN
FOR _rec IN
		SELECT 
			a.userid,    
			a.boardid,    
			a.provideruserkey::varchar(36),    
			a.name,    
			a.password,    
			a.email,    
			a.joined,    
			a.lastvisit,    
			a.ip,    
			a.numposts,    
			a.timezone,    
			a.avatar,    
			a.signature,    
			a.avatarimage,    
			a.avatarimagetype,    
			a.rankid,    
			a.suspended,    
			a.languagefile,    
			a.themefile,
			a.overridedefaultthemes,    
			a.pmnotification,    
			a.flags,    
			a.points,
			a.autowatchtopics,
			a.displayname,
			a.culture,
			a.culture,
			a.dailydigest,
			a.notificationtype,
			a.isfacebookuser,
			a.istwitteruser,
			a.texteditor,
			a.usesinglesignon,
			a.isapproved,
			a.isactiveexcluded,
			a.topicsperpage,
			a.postsperpage,
			b.name AS RankName,
			case(i_stylednicks)
			when true THEN  (SELECT us.userstyle FROM {databaseSchema}.{objectQualifier}user us where us.userid = a.userid)  
			else ''	 end,   
			NULL AS NumDays,
			NULL AS NumPostsForum,	
			0 AS HasAvatarImage,
			COALESCE({databaseSchema}.{objectQualifier}int_to_bool_helper(a.flags & 4),FALSE) AS IsGuest,
			COALESCE({databaseSchema}.{objectQualifier}int_to_bool_helper(a.flags & 1),FALSE)::integer AS IsHostAdmin,		
			(SELECT COALESCE(SIGN(1),0) from {databaseSchema}.{objectQualifier}usergroup x
						   JOIN {databaseSchema}.{objectQualifier}group y 
							 ON y.groupid=x.groupid 
							   WHERE x.userid=a.userid
								 AND (y.flags & 1)<>0 LIMIT 1)::integer AS IsAdmin, 			
			FALSE AS IsForumModerator,
			FALSE AS IsModerator			
			
		FROM 
			{databaseSchema}.{objectQualifier}user a
			JOIN {databaseSchema}.{objectQualifier}rank b ON b.rankid=a.rankid
		WHERE 
			a.boardid = i_boardid 
						  AND (i_approved IS NULL 
						   OR (i_approved IS NOT TRUE AND a.isapproved IS FALSE) 
							 OR (i_approved IS TRUE AND a.isapproved IS TRUE))
		ORDER BY 
			a.name
LOOP
RETURN NEXT _rec;
END LOOP;
	ELSE
FOR _rec IN
		SELECT 
			a.userid,    
			a.boardid,    
			a.provideruserkey::varchar(36),    
			a.name,    
			a.password,    
			a.email,    
			a.joined,    
			a.lastvisit,    
			a.ip,    
			a.numposts,    
			a.timezone,    
			a.avatar,    
			a.signature,    
			a.avatarimage,    
			a.avatarimagetype,    
			a.rankid,    
			a.suspended,    
			a.languagefile,    
			a.themefile,
			a.overridedefaultthemes,    
			a.pmnotification,    
			a.flags,    
			a.points,
			a.autowatchtopics,
			a.displayname,
			a.culture,
			a.culture,
			a.dailydigest,
			a.notificationtype,
			a.isfacebookuser,
			a.istwitteruser,
			a.texteditor,
			a.usesinglesignon, 
			a.isapproved,
			a.isactiveexcluded,
			a.topicsperpage,
			a.postsperpage,
			b.name AS RankName,
			case(i_stylednicks)
			when true THEN  (SELECT us.userstyle FROM {databaseSchema}.{objectQualifier}user us where us.userid = a.userid)  
			else ''	 end,   
			NULL AS NumDays,
			NULL AS NumPostsForum,	
			0 AS HasAvatarImage,
			COALESCE({databaseSchema}.{objectQualifier}int_to_bool_helper(a.flags & 4),FALSE) AS IsGuest,
			COALESCE({databaseSchema}.{objectQualifier}int_to_bool_helper(a.flags & 1),FALSE)::integer AS IsHostAdmin,			
			(SELECT COALESCE(SIGN(1),0) from {databaseSchema}.{objectQualifier}usergroup x 
						JOIN {databaseSchema}.{objectQualifier}group y ON y.groupid=x.groupid 
						WHERE x.userid=a.userid 
						AND (y.flags & 1)<>0 LIMIT 1)::integer AS IsAdmin, 			
			FALSE AS IsForumModerator,
			FALSE AS IsModerator		
		FROM 
			{databaseSchema}.{objectQualifier}user a
			JOIN {databaseSchema}.{objectQualifier}rank b ON b.rankid=a.rankid
		WHERE 
			a.boardid = i_boardid and
			(i_approved IS NULL  OR (i_approved IS FALSE AND a.isapproved IS FALSE) 
						  OR (i_approved IS TRUE AND a.isapproved IS TRUE)) 
						  AND (i_groupid IS NULL OR EXISTS
							  (SELECT 1 FROM {databaseSchema}.{objectQualifier}usergroup x 
								 WHERE x.userid=a.userid AND x.groupid=i_groupid LIMIT 1)) 
								  AND (i_rankid IS NULL OR a.rankid=i_rankid)
		ORDER BY 
			a.name
LOOP
RETURN NEXT _rec;
END LOOP;

		 END IF;

END;
$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100;   
--GO
CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}admin_list(
						   i_boardid integer,                       
						   i_stylednicks boolean,
						   i_utctimestamp timestamp)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}admin_list_rt AS
$BODY$DECLARE
			 _rec {databaseSchema}.{objectQualifier}admin_list_rt%ROWTYPE;
BEGIN
		FOR _rec in SELECT 
					a.userid,    
					a.boardid,  
					b.name,  
					a.provideruserkey,    
					a.name,    
					a.password,    
					a.email,
					a.joined,
					a.lastvisit,
					a.ip,
					a.numposts,
					a.timezone,
					a.avatar,
					a.signature,
					a.avatarimage,
					a.avatarimagetype,
					a.rankid,
					a.suspended,
					a.languagefile,
					a.themefile,
					a.overridedefaultthemes,
					a.pmnotification,
					a.flags,
					a.points,
					a.autowatchtopics,
					a.displayname,
					a.culture,
					a.dailydigest,
					a.notificationtype,
					a.isfacebookuser,
					a.istwitteruser,
					a.texteditor,
					a.usesinglesignon,
					r.name AS RankName,
			case(i_stylednicks)
			when true THEN  (SELECT us.userstyle FROM {databaseSchema}.{objectQualifier}user us where us.userid = a.userid)  
			else ''	 end as Style, 
			(date_part('days', i_utctimestamp - a.joined) + 1) as NumDays,		
			(select count(1) from {databaseSchema}.{objectQualifier}message x where x.isapproved is true and x.isdeleted is false) AS NumPostsForum,
			(select count(1) from {databaseSchema}.{objectQualifier}user x where x.userid=a.userid and AvatarImage is not null) AS HasAvatarImage ,
			COALESCE(c.IsAdmin,0)::boolean AS IsAdmin,			
			COALESCE(a.Flags & 1,0)::boolean AS IsHostAdmin
		from 
			{databaseSchema}.{objectQualifier}user a
			JOIN
			{databaseSchema}.{objectQualifier}board b	
			ON b.boardid = a.boardid
			JOIN
			{databaseSchema}.{objectQualifier}rank r	
			ON r.rankid = a.rankid
			LEFT  JOIN	
			{databaseSchema}.{objectQualifier}vaccess c 
			ON c.userid = a.userid
		where 			
			(i_boardid is null or a.boardID = i_boardid) and
			-- is not guest 
			a.isguest IS FALSE and
			COALESCE(c.forumid,0) = 0 and
			-- is admin 
			COALESCE(c.isadmin,0) <> 0 
		order by 
			a.displayname
			LOOP			
			RETURN NEXT _rec;
			END LOOP;
END;
$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100 ROWS 1000;   
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}admin_pageaccesslist(
						   i_boardid integer,                       
						   i_stylednicks boolean,
						   i_utctimestamp timestamp)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}admin_pageaccess_rt AS
$BODY$DECLARE
			 _rec {databaseSchema}.{objectQualifier}admin_pageaccess_rt%ROWTYPE;
BEGIN
		FOR _rec in SELECT 
					a.userid,    
					a.boardid,
					b.name,                  
					a.name,                 
					a.displayname,
					a.culture,				
			case(i_stylednicks)
			when true THEN  (SELECT us.userstyle FROM {databaseSchema}.{objectQualifier}user us where us.userid = a.userid)  
			else ''	 end as Style
		from 
			{databaseSchema}.{objectQualifier}user a	
			JOIN
			{databaseSchema}.{objectQualifier}board b	
			ON b.boardid = a.boardid
			LEFT  JOIN	
			{databaseSchema}.{objectQualifier}vaccess c 
			ON c.userid = a.userid
		where 			
			(i_boardid is null or a.boardID = i_boardid) and
			-- is not guest 
			a.isguest IS FALSE and
			COALESCE(c.forumid,0) = 0 and
			-- is admin 
			COALESCE(c.isadmin,0) <> 0 and
			-- is not host admin 
			COALESCE((a.flags & 1),0) = 0 
		order by 
			a.displayname
			LOOP			
			RETURN NEXT _rec;
			END LOOP;
END;
$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100 ROWS 1000;   
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}user_listmembers(
						   i_boardid integer,
						   i_userid integer,
						   i_approved boolean,
						   i_groupid integer,
						   i_rankid integer,
						   i_stylednicks boolean,
						   i_literals varchar(255),
						   i_exclude boolean,
						   i_beginswith boolean,
						   i_pageindex integer,
						   i_pagesize integer,
						   i_sortname integer,
						   i_sortrank integer,
						   i_sortjoined integer,
						   i_sortposts integer,
						   i_sortlastvisit integer,
						   i_numposts integer,
						   i_numpostscompare integer)  
				  RETURNS SETOF {databaseSchema}.{objectQualifier}user_listmembers_return_type AS
$BODY$DECLARE
			 _rec {databaseSchema}.{objectQualifier}user_listmembers_return_type%ROWTYPE;
			 ici_user_totalrowsnumber integer:=0;
			 ici_pagelowerbound integer := i_pagesize*i_pageindex;
			 ici_pageupperbound integer := i_pagesize -1 + ici_pagelowerbound;
			 ici_counter integer:=0;
			 ici_firstselectrownum integer;
			 ici_firstselectuserid varchar(255);
			 ici_firstselectrankid integer;
			 ici_firstselectlastvisit timestamp ;
			 ici_firstselectjoined timestamp ;
			 ici_firstselectposts integer;
BEGIN 
   -- get total number of users in the db
   SELECT count(a.userid) INTO ici_user_totalrowsnumber
	from {databaseSchema}.{objectQualifier}user a 
	  join {databaseSchema}.{objectQualifier}rank b 
	  on b.rankid=a.rankid
	  where
	   a.boardid = i_boardid	   
	   and
		
		(i_approved is null or (i_approved is false and a.isapproved IS FALSE) or (i_approved is true and a.isapproved IS TRUE)) and
		(i_groupid is null or exists(select 1 from {databaseSchema}.{objectQualifier}usergroup x where x.userid=a.userid and x.groupid=i_groupid limit 1)) and
		(i_rankid is null or a.rankid=i_rankid) AND
		a.isguest IS FALSE 
			AND
		a.displayname ILIKE CASE 
			WHEN (not i_beginswith  AND i_literals IS NOT NULL AND LENGTH(i_literals) > 0) THEN ('%' || i_literals || '%') 
			WHEN (i_beginswith AND i_literals IS NOT NULL AND LENGTH(i_literals) > 0) THEN (i_literals || '%')
			ELSE '%' END  
		and
		(a.numposts >= (case 
		when i_numpostscompare = 3 THEN  i_numposts end) 
		OR a.numposts <= (case 
		when i_numpostscompare = 2 THEN i_numposts end) OR
		a.numposts = (case 
		when i_numpostscompare = 1 THEN i_numposts end)) 
		order by 1;

				

   -- Set the page bounds
	ici_pagelowerbound = i_pagesize*i_pageindex;
	ici_pageupperbound= i_pagesize -1 + ici_pagelowerbound;    

   
	FOR _rec in SELECT 
			a.userid,    
			a.boardid,    
			a.provideruserkey,    
			a.name,    
			a.password,    
			a.email,    
			a.joined,    
			a.lastvisit,    
			a.ip,    
			a.numposts,    
			a.timezone,    
			a.avatar,    
			a.signature,    
			a.avatarimage,    
			a.avatarimagetype,    
			a.rankid,    
			a.suspended,    
			a.languagefile,    
			a.themefile,
			a.overridedefaultthemes,    
			a.pmnotification,    
			a.flags,    
			a.points,
			a.autowatchtopics,
			a.displayname,
			a.culture,
			a.dailydigest,
			a.notificationtype,
			a.texteditor,
			a.usesinglesignon,
			COALESCE(CAST(SIGN(a.flags & 2) AS integer)>0,false) AS isapproved,
			COALESCE(CAST(SIGN(a.flags & 16) AS integer)>0,false) AS isactiveexcluded,
			b.name AS RankName,
			(case(i_stylednicks)
			when true THEN  a.userstyle   
			else ''	 end) AS "Style",
			ici_user_totalrowsnumber 
			from {databaseSchema}.{objectQualifier}user a 
			join {databaseSchema}.{objectQualifier}rank b on b.rankid=a.rankid	
	   where
	   a.boardid = i_boardid	   
	   and
		(i_approved is null or (i_approved is false and a.isapproved IS FALSE) or (i_approved is true and a.isapproved IS TRUE)) and
		(i_groupid is null or exists(select 1 from {databaseSchema}.{objectQualifier}usergroup x where x.userid=a.userid and x.groupid=i_groupid)) and
		(i_rankid is null or a.rankid=i_rankid) AND
		a.isguest IS FALSE 
			AND
	   a.displayname ILIKE CASE 
			WHEN (not i_beginswith  AND i_literals IS NOT NULL AND LENGTH(i_literals) > 0) THEN ('%' || i_literals || '%') 
			WHEN (i_beginswith AND i_literals IS NOT NULL AND LENGTH(i_literals) > 0) THEN (i_literals || '%')
			ELSE '%' END  
		and
		(a.numposts >= (case 
		when i_numpostscompare = 3 THEN  i_numposts end) 
		OR a.numposts <= (case 
		when i_numpostscompare = 2 THEN i_numposts end) OR
		a.numposts = (case 
		when i_numpostscompare = 1 THEN i_numposts end)) 
	ORDER BY  (case 
		when i_sortname = 0 THEN a.name 
		else null  end) DESC,
		(case 
		when i_sortname = 1 THEN a.name 
		else null  end) ASC, 
		(case 
		when i_sortrank = 0 THEN a.rankid 
		else null  end) DESC,
		(case 
		when i_sortrank = 1 THEN a.rankid 
		else null  end) ASC,		
		(case 
		when i_sortjoined = 0 THEN a.joined 
		else null  end) DESC,
		(case 
		when i_sortjoined = 1 THEN a.joined 
		else null  end) ASC,
		(case 
		when i_sortlastvisit = 0 THEN a.lastvisit 
		else null  end) DESC,
		(case 
		when i_sortlastvisit = 1 THEN a.lastvisit 
		else null  end) ASC,
		(case
		 when i_sortposts = 0 THEN a.numposts 
		else null  end) DESC, 
		(case
		 when i_sortposts = 1 THEN a.numposts 
		else null  end) ASC 
		LOOP
		ici_counter := ici_counter+1;	
		IF ici_counter > ici_pageupperbound THEN 
		exit;
		END IF;
		IF ici_counter >= ici_pagelowerbound THEN  
		RETURN NEXT _rec;
		END IF;
		
		END LOOP;  
  
END;
$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100 ROWS 1000;
--GO

-- Function: {databaseSchema}.{objectQualifier}user_listmedals(integer)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}user_listmedals(integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}user_listmedals(
						   i_userid integer)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}user_listmedals_return_type AS
$BODY$DECLARE
			 _rec {databaseSchema}.{objectQualifier}user_listmedals_return_type%ROWTYPE;
BEGIN
 FOR _rec IN
	SELECT
		a.medalid AS MedalID,
		a.name,
		COALESCE(b.message, a.message) AS Message,
		a.medalurl AS MedalURL,
		a.ribbonurl AS RibbonURL,
		a.smallmedalurl AS SmallMedalURL,
		COALESCE(a.smallribbonurl, a.smallmedalurl) AS SmallRibbonURL,
		a.smallmedalwidth AS SmallMedalWidth,
		a.smallmedalheight AS SmallMedalHeight,
		COALESCE(a.smallribbonwidth, a.smallmedalwidth) AS SmallRibbonWidth,
		COALESCE(a.smallribbonheight, a.smallmedalheight) AS SmallRibbonHeight,
		{databaseSchema}.{objectQualifier}medal_getsortorder(b.sortorder,a.sortorder,a.flags) as "SortOrder",
		{databaseSchema}.{objectQualifier}medal_gethide(b.hide,a.flags) AS Hide,
		{databaseSchema}.{objectQualifier}medal_getribbonsetting(a.smallribbonurl,a.flags,b.onlyribbon) AS "OnlyRibbon",
		a.flags,
		b.dateawarded
	FROM
		{databaseSchema}.{objectQualifier}medal a
		INNER JOIN {databaseSchema}.{objectQualifier}usermedal b ON a.medalid = b.medalid
	WHERE
		b.userid = i_userid
 
	UNION
 
	SELECT	a.medalid, a.name,
		COALESCE(b.message, a.message) as Message,
		a.medalurl,
		a.ribbonurl,
		a.smallmedalurl,
		COALESCE(a.smallribbonurl, a.smallmedalurl) as SmallRibbonURL,
		a.smallmedalwidth,
		a.smallmedalheight,
		COALESCE(a.smallribbonwidth, a.smallmedalwidth) as SmallRibbonWidth,
		COALESCE(a.smallribbonheight, a.smallmedalheight) as SmallRibbonHeight,
		{databaseSchema}.{objectQualifier}medal_getsortorder(b.sortorder,a.sortorder,a.flags) as "SortOrder",
		{databaseSchema}.{objectQualifier}medal_gethide(b.hide,a.flags) as Hide,
		{databaseSchema}.{objectQualifier}medal_getribbonsetting(a.smallribbonurl,a.flags,b.onlyribbon) as "OnlyRibbon",
		a.flags,
		NULL AS DateAwarded
	FROM
		{databaseSchema}.{objectQualifier}medal a
		INNER JOIN {databaseSchema}.{objectQualifier}groupmedal b ON a.medalid = b.medalid
		INNER JOIN {databaseSchema}.{objectQualifier}usergroup c ON b.groupid = c.groupid
	WHERE
		c.userid = i_userid
	ORDER BY
		"OnlyRibbon" DESC,
		"SortOrder" ASC
LOOP
RETURN NEXT _rec;
END LOOP;

 
END;$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100
  ROWS 1000;   
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}user_medal_delete(
						   i_userid integer, 
						   i_medalid integer)
				  RETURNS void AS
'DELETE FROM {databaseSchema}.{objectQualifier}usermedal 
	WHERE userid=$1 
	AND medalid=$2;' 
  LANGUAGE 'sql' VOLATILE SECURITY DEFINER
  COST 100;  
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}user_medal_list(
						   i_userid integer, 
						   i_medalid integer,
						   i_utctimestamp timestamp)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}user_medal_list_return_type AS
$BODY$DECLARE
			 _rec {databaseSchema}.{objectQualifier}user_medal_list_return_type%ROWTYPE;
BEGIN
FOR _rec IN  
	SELECT 
		a.medalid AS MedalID,
		a.name AS Name,
		a.medalurl AS MedalURL,
		a.ribbonurl AS RibbonURL,
		a.smallmedalurl AS SmallMedalURL,
		a.smallribbonurl AS SmallRibbonURL,
		a.smallmedalwidth AS SmallMedalWidth,
		a.smallmedalheight AS SmallMedalHeight,
		a.smallribbonwidth AS SmallRibbonWidth,
		a.smallribbonheight AS SmallRibbonHeight,
		b.sortorder AS "SortOrder",
		a.flags AS "Flags",
		c.name as UserName,
		c.displayname as DisplayName,
		b.userid AS UserID,
		COALESCE(b.message,a.message) as Message,
		b.message as MessageEx,
		b.hide AS Hide,
		b.onlyribbon AS OnlyRibbon,
		b.sortorder AS CurrentSortOrder,
		b.dateawarded AS DateAwarded
	FROM
		{databaseSchema}.{objectQualifier}medal a
		INNER JOIN {databaseSchema}.{objectQualifier}usermedal b ON b.medalid = a.medalid
		INNER JOIN {databaseSchema}.{objectQualifier}user c ON c.userid = b.userid
	WHERE
		(i_userid IS NULL OR b.userid = i_userid) AND
		(i_medalid IS NULL OR b.medalid = i_medalid)		
	ORDER BY
		c.name ASC,
		b.sortorder ASC
LOOP
RETURN NEXT _rec;
END LOOP; 

END;$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100 ROWS 1000;  
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}user_medal_save(
						   i_userid integer, 
						   i_medalid integer, 
						   i_message varchar, 
						   i_hide boolean, 
						   i_onlyribbon boolean, 
						   i_sortorder integer, 
						   i_dateawarded timestamp,
						   i_utctimestamp timestamp)
				  RETURNS void AS
$BODY$DECLARE
			 ici_DateAwarded timestamp  :=i_dateawarded;
BEGIN
 
	IF EXISTS(SELECT 1 from {databaseSchema}.{objectQualifier}usermedal 
	WHERE userid=i_userid AND 
medalid=i_medalid) THEN
		UPDATE {databaseSchema}.{objectQualifier}usermedal
		SET
			message = i_message,
			hide = i_hide,
			onlyribbon = i_onlyribbon,
			sortorder = i_sortorder
		WHERE
			userid=i_userid AND 
			medalid=i_medalid;
	
	ELSE 
 
		IF (ici_DateAwarded IS NULL) THEN ici_DateAwarded = i_utctimestamp; END IF;
 
		INSERT INTO {databaseSchema}.{objectQualifier}usermedal
(userid,medalid,message,hide,onlyribbon,sortorder,dateawarded)
		VALUES
(i_userid,i_medalid,i_message,i_hide,i_onlyribbon,i_sortorder,ici_DateAwarded);
	END IF; 
 
END;$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100; 
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}user_migrate(
						   i_userid integer, 
						   i_provideruserkey varchar, 
						   i_updateprovider boolean)
				  RETURNS void AS
$BODY$DECLARE
			 ici_Password varchar(255);
			 ici_IsApproved boolean;
			 ici_LastActivity timestamp ;
			 ici_Joined timestamp ;
			 ici_case integer;
			 ici_result boolean:=false;
BEGIN
	UPDATE {databaseSchema}.{objectQualifier}user 
	SET provideruserkey = i_ProviderUserKey WHERE userid = i_UserID;
 
	IF (i_UpdateProvider IS TRUE) THEN 	
				
				  SELECT			
			flags & 2 INTO ici_case 	
				
		  FROM
			{databaseSchema}.{objectQualifier}user
		  WHERE
			userid = i_UserID;
				  IF ici_case =2 THEN ici_result :=TRUE;  END IF;               
				 
				
		SELECT
			  password,
			  ici_result,
			  lastvisit,
			  joined
				INTO ici_Password, ici_IsApproved, ici_LastActivity, ici_Joined
		FROM
			{databaseSchema}.{objectQualifier}user
		WHERE
			userid = i_UserID;
		
		UPDATE
			{databaseSchema}.{objectQualifier}prov_membership
		SET
			password = ici_Password,
			"PasswordFormat" = '1',
			"LastActivity" = ici_LastActivity,
			isapproved = ici_IsApproved,
			joined = ici_Joined
		WHERE
			userid = i_ProviderUserKey;
	END IF;
 END;
$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100; 
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}user_nntp(
						   i_boardid integer, 
						   i_username varchar(255),
						   i_email varchar, 
						   i_timezone integer,
						   i_utctimestamp timestamp)
				  RETURNS integer AS
$BODY$DECLARE
			
			 ici_userid integer;       
			 intnull integer;
			 varcharnull varchar;
			 charnull varchar(10);
BEGIN
	i_username := i_username || ' (NNTP)';
 
	SELECT
		a.userid INTO ici_userid
	FROM
		{databaseSchema}.{objectQualifier}user a
	WHERE
		a.boardid=i_boardid and
		a.name=i_username limit 1;        

	IF ici_userid is null THEN 	 		              
		SELECT {databaseSchema}.{objectQualifier}user_save 
		(intnull,
		i_boardid,
		i_username,
		i_username,
		i_email,
		i_timezone,
		varcharnull,
		varcharnull,
		varcharnull, 
		false, 
		varcharnull, 
		false,
		true,
		false,
		0,
		varcharnull,
		false,
		false, 
		false,
		10,
		10,
		i_utctimestamp)
		INTO ici_userid; 	
	END IF;
RETURN ici_userid;
END;$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100;
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}user_pmcount(
						   i_userid integer)
				  RETURNS {databaseSchema}.{objectQualifier}user_pmcount_return_type AS
$BODY$DECLARE
			 ici_countin integer;
			 ici_countout integer;
			 ici_countarchivedin integer;
			 ici_plimit1 integer;
			 ici_pcount integer;
			 _rec {databaseSchema}.{objectQualifier}user_pmcount_return_type;
BEGIN
	   
	SELECT  c.pmlimit INTO ici_plimit1 FROM {databaseSchema}.{objectQualifier}user a 
						JOIN {databaseSchema}.{objectQualifier}usergroup b
						  ON a.userid = b.userid
							JOIN {databaseSchema}.{objectQualifier}group c                         
							  ON b.groupid = c.groupid WHERE a.userid = i_userid ORDER BY c.pmlimit DESC LIMIT 1;
	SELECT  c.pmlimit INTO  ici_pcount FROM {databaseSchema}.{objectQualifier}rank c 
						JOIN {databaseSchema}.{objectQualifier}user d
						   ON c.rankid = d.rankid WHERE d.userid = i_userid ORDER BY c.pmlimit DESC LIMIT 1;
	  IF (ici_plimit1 > ici_pcount) THEN     
	  ici_pcount := ici_plimit1;      
	  END IF;
	  
	-- get count of pm's in user's sent items
	
	SELECT 
		COUNT(a.pmessageid) INTO  ici_countout 
	FROM 
		{databaseSchema}.{objectQualifier}userpmessage a
	INNER JOIN {databaseSchema}.{objectQualifier}pmessage b ON a.pmessageid=b.pmessageid
	WHERE 
		(a.flags & 2)<>0 AND
		b.fromuserid = i_userid;
	-- get count of pm's in user's received items
	SELECT 
		COUNT(a.pmessageid) INTO ici_countin
	FROM 
		{databaseSchema}.{objectQualifier}userpmessage a
	WHERE 
		userid = i_userid   AND (a.flags & 4) = 0
		AND (a.flags & 8) = 0;
		
		SELECT 
		COUNT(1) INTO ici_countarchivedin
	FROM 
		{databaseSchema}.{objectQualifier}userpmessage a
		WHERE
		(a.flags & 4) <> 0 AND
		a.userid = i_userid;	

	-- return all pm data
	SELECT 
		ici_countin,
		ici_countout,
		ici_countarchivedin,
		ici_countin + ici_countout + ici_countarchivedin,
		ici_pcount INTO _rec; 	
RETURN _rec;
END;$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100; 
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}user_recoverpassword(
						   i_boardid integer, 
						   i_username varchar, 
						   i_email varchar)
				  RETURNS  {databaseSchema}.{objectQualifier}user_recoverpassword_return_type AS
$BODY$DECLARE
			 ici_userid integer;
			 _rec {databaseSchema}.{objectQualifier}user_recoverpassword_return_type;
BEGIN
	
	SELECT  userid INTO ici_userid FROM {databaseSchema}.{objectQualifier}user 
		  WHERE boardid = i_boardid AND name = i_username and email = i_email;
	IF ici_userid IS NULL THEN
		SELECT null INTO _rec;
		RETURN  _rec;
		/*return*/
	ELSE
		SELECT  ici_userid INTO _rec;
	RETURN  _rec;

	END IF;

END;$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100;
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}user_removepoints(
						   i_userid integer,
						   i_fromuserid integer,
						   i_utctimestamp timestamp,
						   i_points integer)
				  RETURNS void AS
$BODY$
DECLARE _votedate timestamp;
BEGIN 
UPDATE {databaseSchema}.{objectQualifier}user
	 SET    points = points - i_points
	 WHERE  userid = i_userid;
	 IF i_fromuserid IS NOT NULL then
	select votedate into _votedate from {databaseSchema}.{objectQualifier}reputationvote where reputationfromuserid=i_fromuserid AND reputationtouserid=i_userid;
	IF _votedate is not null then		     
		  update {databaseSchema}.{objectQualifier}reputationvote set votedate=i_utctimestamp 
		  where votedate = _votedate AND reputationfromuserid=i_fromuserid AND reputationtouserid=i_userid;
	eLSE
	  
		  insert into {databaseSchema}.{objectQualifier}reputationvote(reputationfromuserid,reputationtouserid,votedate)
		  values (i_fromuserid, i_userid, i_utctimestamp);
	  end if;
	END IF;
END;
$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100;
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}user_resetpoints()
				  RETURNS void AS
'UPDATE {databaseSchema}.{objectQualifier}user 
SET points = numposts * 3;'
  LANGUAGE 'sql' VOLATILE SECURITY DEFINER
  COST 100; 
--GO

-- Function: {databaseSchema}.{objectQualifier}user_save(integer, integer, varchar, varchar, integer, varchar, varchar, boolean, boolean, boolean, varchar)

-- DROP FUNCTION {databaseSchema}.{objectQualifier}user_save(integer, integer, varchar, varchar, integer, varchar, varchar, boolean, boolean, boolean, varchar);
/* DROP FUNCTION {databaseSchema}.{objectQualifier}user_save(
				  integer,
				  integer,
				  varchar,
				  varchar,
				  varchar, 
				  integer, 
				  varchar, 
				  char(5),
				  varchar,
				  boolean,
				  varchar,
				  varchar, 
				  boolean,  
				  boolean,
				  boolean,
				  integer, 
				  varchar, 
  boolean,
  boolean, 
  boolean);

DROP FUNCTION {databaseSchema}.{objectQualifier}user_save(
				  integer,integer,varchar,varchar,varchar, integer, varchar, 
				  char(5),varchar,varchar,varchar, boolean,  boolean, integer, varchar, 
  boolean,
  boolean, 
  boolean); */

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}user_save(
						   i_userid integer,
						   i_boardid integer,
						   i_username varchar(255),
						   i_displayname varchar(255),
						   i_email varchar(255),
						   i_timezone integer,
						   i_languagefile varchar(50),
						   i_culture varchar(12),
						   i_themefile varchar(50),
						   i_usesinglesignon boolean,
						   i_texteditor varchar,
						   i_overridedefaulttheme boolean,
						   i_approved boolean,
						   i_pmnotification boolean,
						   i_notificationtype integer,
						   i_provideruserkey varchar,
						   i_autowatchtopics boolean,
						   i_dstuser boolean,
						   i_hideuser boolean,
						   i_topicsperpage integer,
						   i_postsperpage integer,
						   i_utctimestamp timestamp)
				 RETURNS integer AS
$BODY$DECLARE
			 ici_rankid integer;
			 ici_flags integer:=0;
			 ici_PMNotification boolean := COALESCE(i_pmnotification,TRUE);
			 ici_OverrideDefaultTheme boolean :=i_overridedefaulttheme;
			 ici_notificationtype integer = i_notificationtype;
			 ici_email varchar :=i_email;
			 ici_userid integer :=i_userid;
			 ici_autowatchtopics boolean :=i_autowatchtopics;
			 ici_OldDisplayName varchar;			
BEGIN 		
	-- new
	IF i_usesinglesignon IS NULL THEN 
		  i_usesinglesignon:=FALSE; 
	END IF;

	IF ici_OverrideDefaultTheme IS NULL THEN 
		  ici_OverrideDefaultTheme:=FALSE; 
	END IF;
	
	IF ici_autowatchtopics IS NULL THEN 
		  ici_autowatchtopics:=FALSE; 
	END IF;
	-- this is a new user
	IF i_userid IS NULL THEN 	
	   IF i_approved THEN 
	   -- set approve flag
	   ici_flags := ici_flags | 2; 
	   END IF; 
		
		IF ici_email = '' THEN  
		  ici_email := null; 
		END IF;
		
		SELECT rankid INTO ici_rankid FROM {databaseSchema}.{objectQualifier}rank 
		WHERE (flags & 1)<>0 AND boardid=i_boardid;

		INSERT INTO {databaseSchema}.{objectQualifier}user(boardid,rankid,name,displayname,password,email,joined,lastvisit,numposts,timezone,flags,pmnotification,provideruserkey,autowatchtopics, notificationtype,topicsperpage,postsperpage) 
		VALUES(i_boardid,ici_rankid,i_username,i_displayname,'-',ici_email,i_utctimestamp,i_utctimestamp,0,i_timezone,ici_flags,ici_PMNotification,i_provideruserkey,ici_autowatchtopics, i_notificationtype,i_topicsperpage,i_postsperpage);		
		SELECT CURRVAL(pg_get_serial_sequence('{databaseSchema}.{objectQualifier}user','userid')) INTO ici_userid; 		
		INSERT INTO {databaseSchema}.{objectQualifier}usergroup(userid,groupid) 
		SELECT ici_userid,
		groupid FROM {databaseSchema}.{objectQualifier}group 
		where boardid=i_boardid and (flags & 4)<>0;

	ELSE
	   -- the user is being updated, find old user name and existing flags
	   SELECT flags,displayname into ici_flags,ici_OldDisplayName FROM {databaseSchema}.{objectQualifier}user  where userid = ici_userid;

		-- isdirty flag -set only		
		IF ((ici_flags & 64) <> 64) THEN		
		ici_flags := ici_flags | 64;
		END IF;

		-- set/remove DST flag
		IF ((i_dstuser) AND (ici_flags & 32) <> 32) THEN		
		ici_flags := ici_flags | 32;
		ELSEIF ((i_dstuser IS NOT TRUE) AND (ici_flags & 32) = 32) THEN
		ici_flags := ici_flags # 32;
		END IF;

		-- set/remove hide user flag	
		IF ((i_hideuser) AND ((ici_flags & 16) <> 16)) THEN 
		ici_flags := ici_flags | 16;
		ELSEIF ((i_hideuser IS NOT TRUE) AND ((ici_flags & 16) = 16)) THEN
		ici_flags := ici_flags # 16;
		END IF;
			
			UPDATE {databaseSchema}.{objectQualifier}user SET
				   timezone = i_timezone,
				   languagefile = i_languagefile,
				   themefile = i_themefile,
				   texteditor = i_texteditor,
				   overridedefaultthemes = ici_OverrideDefaultTheme,
				   pmnotification = (CASE WHEN (ici_PMNotification is not null) THEN  ici_PMNotification ELSE pmnotification END),
				   autowatchtopics = (CASE WHEN (ici_autowatchtopics is not null) THEN  ici_autowatchtopics ELSE autowatchtopics END),
				   notificationtype =  (CASE WHEN (i_notificationtype is not null) THEN  i_notificationtype ELSE notificationtype END),         
				   culture = i_culture,
				   flags = (CASE WHEN ici_flags<>flags THEN  ici_flags ELSE flags END),
				   displayname = (CASE WHEN (i_displayname  is not null) THEN  i_displayname ELSE displayname END),
				   email = (CASE WHEN (ici_email IS NOT NULL) THEN  ici_email ELSE email END),
				   usesinglesignon = i_usesinglesignon,
				   topicsperpage = i_topicsperpage,
				   postsperpage = i_postsperpage
			WHERE userid = ici_userid;

		   if (i_displayname IS NOT NULL AND COALESCE(ici_OldDisplayName,'') != COALESCE(i_displayname,'')) then
			-- sync display names everywhere - can run a long time on large forums
			update {databaseSchema}.{objectQualifier}forum set LastUserDisplayName = i_displayname where LastUserID = ici_userid;
			update {databaseSchema}.{objectQualifier}topic set LastUserDisplayName = i_displayname where LastUserID = ici_userid;
			update {databaseSchema}.{objectQualifier}topic set UserDisplayName = i_displayname where UserID = ici_userid;
			update {databaseSchema}.{objectQualifier}message set UserDisplayName = i_displayname where UserID = ici_userid;
			update {databaseSchema}.{objectQualifier}shoutboxmessage set UserDisplayName = i_displayname where UseriD = ici_userid;
		  end if;
END IF;
	RETURN ici_userid;
	END;
$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100;
--GO 

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}user_setnotdirty(
						   i_userid integer) 
				  RETURNS void AS
$BODY$
BEGIN	
	 UPDATE {databaseSchema}.{objectQualifier}user set flags = flags # 64 where userid = i_userid AND (flags & 64 = 64);
END;
$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 30;
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}user_repliedtopic(
						   i_messageid integer, 
						   i_userid integer) 
				  RETURNS integer AS
$BODY$DECLARE 
			 ici_topicid integer;
BEGIN       
SELECT topicid INTO ici_topicid FROM {databaseSchema}.{objectQualifier}message WHERE messageid = i_messageid;

		RETURN (SELECT COUNT(1)
		FROM {databaseSchema}.{objectQualifier}message AS t WHERE t.topicid = ici_topicid AND t.userid = i_userid);		
 END;
$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 30;
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}user_thankfromcount(
						   i_userid integer) 
				  RETURNS integer AS
$BODY$
BEGIN
	 RETURN (SELECT COUNT(1)
			 FROM {databaseSchema}.{objectQualifier}thanks AS t WHERE t.thankstouserid = i_userid);		
 END;
$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 20;
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}user_saveavatar(
						   i_userid integer, 
						   i_avatar varchar, 
						   i_avatarimage bytea, 
						   i_avatarimagetype varchar)
				  RETURNS void AS
$BODY$
BEGIN
	IF i_avatar IS NOT NULL  THEN

	UPDATE {databaseSchema}.{objectQualifier}user
	SET avatar = i_avatar,
	avatarimage = null,
	avatarimagetype = null
	WHERE userid = i_userid;

	ELSEIF i_avatarimage IS NOT NULL THEN
	UPDATE {databaseSchema}.{objectQualifier}user
	SET avatarimage = i_avatarimage,
	avatarimagetype = i_avatarimagetype,
	avatar = null WHERE userid = i_userid;
	END IF;
	END;
$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100; 
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}user_savepassword(
						   i_userid integer, 
						   i_password varchar)
				  RETURNS void AS
'UPDATE {databaseSchema}.{objectQualifier}user 
	SET password = $2 
	where userid =$1;'
  LANGUAGE 'sql' VOLATILE SECURITY DEFINER
  COST 100; 
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}user_savesignature(
						   i_userid integer, 
						   i_signature text)
				  RETURNS void AS
'UPDATE {databaseSchema}.{objectQualifier}user
	 SET signature = $2 
	 WHERE userid = $1;'
  LANGUAGE 'sql' VOLATILE SECURITY DEFINER
  COST 100; 
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}user_setpoints(
						   i_userid integer, 
						   i_points integer)
				  RETURNS void AS
'UPDATE {databaseSchema}.{objectQualifier}user 
	SET points = $2 
	WHERE userid = $1;'
  LANGUAGE 'sql' VOLATILE SECURITY DEFINER
  COST 100; 
--GO

-- DROP FUNCTION {databaseSchema}.{objectQualifier}user_simplelist(integer, integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}user_simplelist(
						   i_startid integer, 
						   i_limit integer)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}user_simplelist_return_type AS
$BODY$DECLARE
			 cntr integer:=0;
			 _rec {databaseSchema}.{objectQualifier}user_simplelist_return_type%ROWTYPE;
BEGIN
FOR _rec IN
	SELECT   a.userid,
			 a.name,
			 a.displayname
	FROM     {databaseSchema}.{objectQualifier}user a
	WHERE    a.userid >= i_startid   
	ORDER BY a.userid
		 LOOP                    
		  RETURN NEXT _rec;
		  cntr:=cntr+1;
		  EXIT WHEN  cntr > i_limit;
		 END LOOP;            
		
	 END;$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100
  ROWS 1000;
--GO
COMMENT ON FUNCTION {databaseSchema}.{objectQualifier}user_simplelist(integer, integer) IS 'UserID should be real user id, limit - number of rows. Used in RewriteUrlBuilder.cs only.';
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}user_setrole(
						   i_boardid integer, 
						   i_provideruserkey varchar, 
						   i_role varchar)
				  RETURNS void AS
$BODY$DECLARE
			 ici_userid integer;
			 ici_groupid integer;
BEGIN 
	SELECT userid INTO ici_userid
	FROM {databaseSchema}.{objectQualifier}user
	WHERE boardid=i_boardid 
	AND provideruserkey=i_provideruserkey;

	IF i_role IS NULL THEN
	   DELETE FROM {databaseSchema}.{objectQualifier}usergroup WHERE userid=ici_userid;
	ELSE
	   IF NOT EXISTS(SELECT 1 from {databaseSchema}.{objectQualifier}group 
						WHERE boardid=i_boardid AND name=i_role LIMIT 1) THEN
		  INSERT INTO {databaseSchema}.{objectQualifier}group(name,boardid,flags)
			VALUES (i_role,i_boardid,0);
		  SELECT CURRVAL(pg_get_serial_sequence('{databaseSchema}.{objectQualifier}group','groupid')) INTO ici_groupid;      
	INSERT INTO {databaseSchema}.{objectQualifier}forumaccess(groupid,forumid,accessmaskid)
	SELECT
	ici_groupid AS GroupID,
	a.forumid,
	MIN(a.accessmaskid) AS AccessMaskID
	FROM
	{databaseSchema}.{objectQualifier}forumaccess a
	JOIN {databaseSchema}.{objectQualifier}group b ON b.groupid=a.groupid
	WHERE
	b.boardid=i_boardid AND
	(b.flags & 4)=4
	GROUP BY
	a.forumid;
	ELSE
	SELECT  groupid INTO ici_groupid FROM {databaseSchema}.{objectQualifier}group 
	WHERE boardid=i_boardid AND name=i_role;
	END IF;
	IF NOT EXISTS(SELECT 1 from {databaseSchema}.{objectQualifier}usergroup 
						WHERE userid=ici_userid AND groupid=ici_groupid LIMIT 1) THEN
	INSERT INTO {databaseSchema}.{objectQualifier}usergroup(userid,groupid) 
	VALUES (ici_userid,ici_groupid);
	END IF;
	END IF;
	END;$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100;  
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}user_suspend(
						   i_userid integer, 
						   i_suspend timestamp )
				  RETURNS void AS
'UPDATE {databaseSchema}.{objectQualifier}user 
	SET suspended = $2 WHERE userid=$1;'  
  LANGUAGE 'sql' VOLATILE SECURITY DEFINER
  COST 100;   
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}user_upgrade(
						   i_userid integer)
				  RETURNS void AS
$BODY$DECLARE 
			 ici_rankid	integer;
			 ici_flags	integer;
			 ici_minposts	integer;
			 ici_numposts	integer;
			 ici_boardid		integer;
			 ici_rankboardid	integer;
 BEGIN   
	-- Get user and rank information 
	SELECT  
	   b.flags,
	   b.minposts,
	   a.numposts,
	   a.boardid	
	INTO   
	   ici_flags,
	   ici_minposts,
	   ici_numposts,
	   ici_boardid
	FROM
	   {databaseSchema}.{objectQualifier}user a
	INNER JOIN {databaseSchema}.{objectQualifier}rank b 
	  ON b.rankid = a.rankid
	WHERE   a.userid = i_userid;
	
	-- If user isn't member of a ladder rank, exit
	IF (ici_flags & 2) != 0 THEN
			-- retrieve board current user's rank beling to	
	SELECT boardid
	into ici_rankboardid
	from   {databaseSchema}.{objectQualifier}rank
	where  rankid = ici_rankid;

	-- does user have rank from his board?
	IF (ici_rankboardid <> ici_boardid) THEN
		-- get highest rank user can get
		select 
			   rankid
			   into ici_rankid
		from   {databaseSchema}.{objectQualifier}rank
		where  boardid = ici_boardid
			   and (flags & 2) = 2
			   and minposts <= ici_numposts
		order by
			   minposts desc LIMIT 1;
	
	else 
		-- See IF user got enough posts for next ladder group
		SELECT  rankid INTO  ici_rankid
				  FROM	{databaseSchema}.{objectQualifier}rank
					WHERE boardid = ici_boardid and
					(flags & 2) = 2 
					  AND minposts <= ici_numposts 
						AND	minposts > ici_minposts
				ORDER BY  minposts LIMIT 1;
	END IF;

				
		  IF ici_rankid > 0 THEN
			  UPDATE {databaseSchema}.{objectQualifier}user 
			  SET rankid = ici_rankid WHERE userid = i_userid; 	
		  END IF;
	END IF;
END;$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100;  
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}userforum_delete(
						   i_userid integer, 
						   i_forumid integer)
				  RETURNS void AS
'DELETE FROM {databaseSchema}.{objectQualifier}userforum 
	WHERE userid=$1 AND forumid=$2;'
  LANGUAGE 'sql' VOLATILE SECURITY DEFINER
  COST 100;  
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}userforum_list(
						   i_userid integer, 
						   i_forumid integer)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}userforum_list_return_type AS
$BODY$DECLARE
			 _rec {databaseSchema}.{objectQualifier}userforum_list_return_type%ROWTYPE;
BEGIN
FOR _rec IN
 SELECT
		a.userid,    
			a.boardid,    
			a.provideruserkey,    
			a.name,    
			a.password,    
			a.email,    
			a.joined,    
			a.lastvisit,    
			a.ip,    
			a.numposts,    
			a.timezone,    
			a.avatar,    
			a.signature,    
			a.avatarimage,    
			a.avatarimagetype,    
			a.rankid,    
			a.suspended,    
			a.languagefile,    
			a.themefile,
			a.overridedefaultthemes,    
			a.pmnotification,    
			a.flags,    
			a.points,
			a.autowatchtopics,
			a.displayname,
			a.culture,
			a.dailydigest,
			a.notificationtype,
			a.texteditor,
			a.usesinglesignon,			
		COALESCE(CAST(SIGN(a.flags & 2) AS integer)>0,false) AS isapproved,
		COALESCE(CAST(SIGN(a.flags & 16) AS integer)>0,false) AS isactiveexcluded,
		b.accessmaskid AS AccessMaskID,
		b.accepted AS "Accepted",
		c.name AS "Access" 
	FROM
		{databaseSchema}.{objectQualifier}user a
		JOIN {databaseSchema}.{objectQualifier}userforum b ON b.userid=a.userid
		JOIN {databaseSchema}.{objectQualifier}accessmask c ON c.accessmaskid=b.accessmaskid
	WHERE
		(i_UserID IS NULL OR a.userid=i_userid) AND
		(i_ForumID IS NULL OR b.forumid=i_forumid)
	ORDER BY
		a.name
LOOP
RETURN NEXT _rec;
END LOOP;

END;$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100;  
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}userforum_save(
						   i_userid integer, 
						   i_forumid integer, 
						   i_accessmaskid integer,
						   i_utctimestamp timestamp)
				  RETURNS void AS
$BODY$
BEGIN
	IF EXISTS(SELECT 1 FROM {databaseSchema}.{objectQualifier}userforum 
	WHERE userid=i_userid AND forumid=i_forumid) THEN
		UPDATE {databaseSchema}.{objectQualifier}userforum 
		SET accessmaskid=i_accessmaskid 
		WHERE userid=i_userid AND forumid=i_forumid;
	ELSE
		INSERT INTO {databaseSchema}.{objectQualifier}userforum(userid,forumid,accessmaskid,invited,accepted) 
		VALUES(i_userid,i_forumid,i_accessmaskid,i_utctimestamp,TRUE);
		END IF;
END;
$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100; 
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}usergroup_list(
						   i_userid integer)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}usergroup_list_return_type AS
$BODY$DECLARE
			 _rec {databaseSchema}.{objectQualifier}usergroup_list_return_type%ROWTYPE; 
BEGIN
FOR _rec IN
	SELECT 
		b.groupid AS GroupID,
		b.name AS Name, 		
		b.style
	FROM
		{databaseSchema}.{objectQualifier}usergroup a
		JOIN {databaseSchema}.{objectQualifier}group b ON b.groupid=a.groupid
	WHERE
		a.userid = i_userid
	ORDER BY
		b.name
LOOP
RETURN NEXT _rec;
END LOOP;

END;$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100;
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}usergroup_save(
						   i_userid integer, 
						   i_groupid integer, 
						   i_member boolean)
				  RETURNS void AS
$BODY$
BEGIN
	IF i_member IS FALSE THEN
		DELETE FROM {databaseSchema}.{objectQualifier}usergroup 
		WHERE userid=i_userid AND groupid=i_groupid;
	ELSE
	IF NOT EXISTS(SELECT 1 FROM {databaseSchema}.{objectQualifier}usergroup 
	WHERE userid=i_userid AND groupid=i_groupid LIMIT 1) THEN
		INSERT INTO {databaseSchema}.{objectQualifier}usergroup(userid,groupid)
		VALUES (i_userid,i_groupid);
		-- update style for the user
   UPDATE {databaseSchema}.{objectQualifier}user SET userstyle= COALESCE((SELECT f.style FROM {databaseSchema}.{objectQualifier}userGroup e 
		join {databaseSchema}.{objectQualifier}group f on f.groupid=e.groupid WHERE e.userid=i_userid AND f.style is not null ORDER BY f.sortorder LIMIT 1), (SELECT r.style FROM {databaseSchema}.{objectQualifier}rank r where r.rankid = {databaseSchema}.{objectQualifier}user.rankid LIMIT 1)) 
	   WHERE userid = i_userid;   
	  
 END IF;
		END IF;
END;$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100; 
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}userpmessage_delete(
						   i_userpmessageid integer)
				  RETURNS void AS
'DELETE FROM {databaseSchema}.{objectQualifier}userpmessage 
	WHERE userpmessageid=$1;'
  LANGUAGE 'sql' VOLATILE SECURITY DEFINER
  COST 100;
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}userpmessage_list(
						   i_userpmessageid integer)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}userpmessage_list_return_type AS
$BODY$DECLARE
			 _rec {databaseSchema}.{objectQualifier}userpmessage_list_return_type%ROWTYPE;
BEGIN
FOR _rec IN
	SELECT
		a.pmessageid,
		a.replyto,
		a.fromuserid,
		a.created,
		a.subject,
		a.body,
		a.flags,
		b.name AS FromUser,
		c.userid AS ToUserID,
		c.name AS ToUser,
		d.isread AS IsRead, 
		d.isreply,		
		d.userpmessageid AS UserPMessageID
	FROM
		{databaseSchema}.{objectQualifier}pmessage a
		inner join {databaseSchema}.{objectQualifier}userpmessage d on d.pmessageid = a.pmessageid
		inner join {databaseSchema}.{objectQualifier}user b on b.userid = a.fromuserid
		inner join {databaseSchema}.{objectQualifier}user c on c.userid = d.userid
	WHERE
		d.userpmessageid = i_userpmessageid
LOOP
RETURN NEXT _rec;
END LOOP;

END;$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100; 
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}watchforum_add(
						   i_userid integer, 
						   i_forumid integer,
						   i_utctimestamp timestamp)
				  RETURNS void AS
$BODY$ 
BEGIN
						  
  IF NOT EXISTS (SELECT 1 FROM {databaseSchema}.{objectQualifier}watchforum a
							WHERE a.forumid=i_forumid
							AND a.userid = i_userid LIMIT 1) THEN
							INSERT INTO {databaseSchema}.{objectQualifier}watchforum
							(forumid,
							userid,
							created)
							VALUES (i_forumid,
							i_userid,
							i_utctimestamp);
							END IF;


	
END;
$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100; 
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}watchforum_check(
						   i_userid integer, 
						   i_forumid integer)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}watchforum_check_return_type AS
$BODY$DECLARE
			 _rec {databaseSchema}.{objectQualifier}watchforum_check_return_type%ROWTYPE;
BEGIN 
FOR _rec IN
	SELECT watchforumid
	 FROM {databaseSchema}.{objectQualifier}watchforum
		 WHERE userid = i_userid AND forumid = i_forumid
LOOP
RETURN NEXT _rec;
END LOOP;
RETURN;
END;$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100;
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}watchforum_delete(
						   i_watchforumid integer)
				  RETURNS void AS
	'DELETE FROM {databaseSchema}.{objectQualifier}watchforum WHERE watchforumid = $1;'	
  LANGUAGE 'sql' VOLATILE SECURITY DEFINER
  COST 100; 
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}watchforum_list(
						   i_userid integer)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}watchforum_list_return_type AS
$BODY$DECLARE
			 _rec {databaseSchema}.{objectQualifier}watchforum_list_return_type%ROWTYPE;
BEGIN
FOR _rec IN
	SELECT
		a.watchforumid,
		a.forumid,
		a.userid,
		a.created,
		a.lastmail,
		b.name AS ForumName,
		(SELECT COUNT(1) FROM {databaseSchema}.{objectQualifier}topic x 
		JOIN {databaseSchema}.{objectQualifier}message y ON y.topicid=x.topicid 
		WHERE x.forumid=a.forumid) AS Messages,
		(SELECT COUNT(1) FROM {databaseSchema}.{objectQualifier}topic x 
		WHERE x.forumid=a.forumid AND x.topicmovedid IS NULL) AS Topics,
		b.lastposted AS LastPosted,
		b.lastmessageid AS LastMessageID,
		(SELECT topicid FROM {databaseSchema}.{objectQualifier}message x 
		WHERE x.messageid=b.lastmessageid) AS LastTopicID,
		b.lastuserid AS LastUserID,
		COALESCE(b.lastusername,(select x.name 
		from {databaseSchema}.{objectQualifier}user x 
		where x.userid=b.lastuserid)) AS LastUserName,
		COALESCE(b.lastuserdisplayname,(select x.displayname 
		from {databaseSchema}.{objectQualifier}user x 
		where x.userid=b.lastuserid)) AS LastUserDisplayName
	FROM
		{databaseSchema}.{objectQualifier}watchforum a
		INNER JOIN {databaseSchema}.{objectQualifier}forum b 
		ON b.forumid = a.forumid
	WHERE
		a.userid = i_userid
LOOP
RETURN NEXT _rec;
END LOOP;

END;
$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100;  
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}watchtopic_add(
						   i_userid integer,
						   i_topicid integer,
						   i_utctimestamp timestamp)
				  RETURNS void AS
$BODY$
BEGIN
		IF NOT EXISTS(SELECT 1 FROM {databaseSchema}.{objectQualifier}watchtopic 
		WHERE topicid=i_topicid AND userid=i_userid LIMIT 1) THEN
	INSERT INTO {databaseSchema}.{objectQualifier}watchtopic(topicid,userid,created)
	VALUES (i_topicid, i_userid, i_utctimestamp); 	
		END IF; 
END;
$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100; 
--GO


CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}watchtopic_check(
						   i_userid integer,
						   i_topicid integer)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}watchtopic_check_return_type AS
$BODY$DECLARE
			 _rec {databaseSchema}.{objectQualifier}watchtopic_check_return_type%ROWTYPE;
BEGIN
FOR _rec IN 
	SELECT watchtopicid FROM {databaseSchema}.{objectQualifier}watchtopic 
	WHERE userid = i_userid AND topicid = i_topicid
LOOP
RETURN NEXT _rec;
END LOOP;

END;$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100;  
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}watchtopic_delete(
						   i_watchtopicid integer)
				  RETURNS void 
AS
  'DELETE FROM {databaseSchema}.{objectQualifier}watchtopic WHERE watchtopicid = $1;'

  LANGUAGE 'sql' VOLATILE SECURITY DEFINER
  COST 100;  
--GO

-- Function: {databaseSchema}.{objectQualifier}watchtopic_list(integer)

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}watchtopic_list(
						   i_userid integer)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}watchtopic_list_return_type AS
$BODY$DECLARE
			 _rec {databaseSchema}.{objectQualifier}watchtopic_list_return_type%ROWTYPE;
BEGIN
FOR _rec IN
	SELECT
		a.watchtopicid,
		a.topicid,
		a.userid,
		a.created,
		a.lastmail,
		b.topic AS TopicName,
		(SELECT COUNT(1)-1 from {databaseSchema}.{objectQualifier}message x 
		where x.topicid=b.topicid) AS Replies,
		b.views,
		b.lastposted,
		b.lastmessageid,
		b.lastuserid,
		COALESCE(b.lastusername,(SELECT name FROM {databaseSchema}.{objectQualifier}user y 
		WHERE y.userid=b.lastuserid)) AS LastUserName,
		COALESCE(b.lastuserdisplayname,(SELECT y.displayname FROM {databaseSchema}.{objectQualifier}user y 
		WHERE y.userid=b.lastuserid)) AS LastUserDisplayName
	FROM
		{databaseSchema}.{objectQualifier}watchtopic a
		INNER JOIN {databaseSchema}.{objectQualifier}topic b 
		ON b.topicid = a.topicid
	WHERE
		a.userid = i_userid
LOOP
RETURN NEXT _rec;
END LOOP;

END;

$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100;  
--GO
-- ZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZ    
	
CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}activeaccess_reset()
				  RETURNS void AS
$BODY$
begin
delete from {databaseSchema}.{objectQualifier}active;
delete from {databaseSchema}.{objectQualifier}activeaccess;
end;
$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100;
--GO

DROP FUNCTION IF EXISTS {databaseSchema}.{objectQualifier}pageload(varchar, integer, varchar, varchar, varchar, varchar, varchar, integer, integer, integer, integer, boolean);
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}pageload(                 
						   i_sessionid varchar,
						   i_boardid integer,
						   i_userkey varchar,
						   i_ip varchar,
						   i_location varchar,
						   i_forumpage varchar,
						   i_browser varchar,
						   i_platform varchar,
						   i_categoryid integer,
						   i_forumid integer,
						   i_topicid integer,
						   i_messageid integer,
						   i_iscrawler	boolean,
						   i_ismobiledevice boolean,
						   i_donttrack boolean,
						   i_utctimestamp timestamp)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}pageload_return_type
AS
$BODY$DECLARE
			 ici_userid		integer;
			 ici_userboardid	integer;
			 ici_boardid	integer:=i_boardid;
			 ici_isguest	boolean := true;
			 ici_activeupdate	boolean := false;
			 ici_rowcount	integer;
			 ici_previousvisit	timestamp ;
			 iii_categoryid integer :=i_categoryid;
			 iii_forumid integer :=i_forumid;
			 iii_topicid integer := i_topicid;
			 iii_messageid integer := i_messageid;
			 i_currenttime	timestamp  := i_utctimestamp;
			 ici_activeflags integer := 1;
			 ici_crawler boolean := i_iscrawler;
			 ici_guestid integer;
			 
			 _rec1 {databaseSchema}.{objectQualifier}pageload_return_type;
BEGIN
	 -- find a guest id should do it every time to be sure that guest access rights are in ActiveAccess table
	 SELECT userid INTO ici_guestid
		  FROM {databaseSchema}.{objectQualifier}user 
		  WHERE boardid=ici_boardid AND isguest IS TRUE limit 1;
	/*  BEGIN	
			
		  SELECT  COUNT(1) INTO ici_rowcount
		  FROM {databaseSchema}.{objectQualifier}user 
		  WHERE boardid=ici_boardid AND isguest IS TRUE GROUP BY 1;		
			 EXCEPTION
			 WHEN  OTHERS THEN
			 RAISE NOTICE 'Found too many possible guest users. There should be one and only one user marked as guest.';
	 END;	*/	
	
-- verify that there's not the same session for other board and drop it IF required.Test·code for portals with many boards  
 
-- delete from {databaseSchema}.{objectQualifier}active where (sessionid = i_sessionid and boardid != ici_boardid) ;
 

	IF i_userkey IS NULL THEN	
		   ici_userid := ici_guestid;
		   -- already set ici_isguest := true;
		   ici_userboardid := ici_boardid;
		   -- set IsGuest ActiveFlag  1 | 2
		   ici_activeflags := 3;
		   -- crawlers are always guests 
		  IF i_iscrawler IS TRUE  THEN			
				-- set IsCrawler ActiveFlag
				ici_activeflags :=  ici_activeflags | 8;
		  END IF;
	ELSE	
		SELECT userid, boardid INTO ici_userid,ici_userboardid  
		FROM {databaseSchema}.{objectQualifier}user 
		where boardid=ici_boardid AND provideruserkey=i_userkey;
		ici_isguest := false;
		-- make sure that registered users are not crawlers
		ici_crawler := false;
		-- set IsRegistered ActiveFlag
		ici_activeflags = ici_activeflags | 4;
	END IF;
	-- Check valid ForumID 
	IF iii_forumid IS NOT NULL 
				 AND NOT EXISTS
			(SELECT 1 FROM {databaseSchema}.{objectQualifier}forum 
			WHERE forumid=iii_forumid)
				 THEN 
	   iii_forumid := NULL; 
	END IF;
	
	-- Check valid CategoryID
	IF iii_categoryid IS NOT NULL 
					AND NOT EXISTS
			 (SELECT 1 FROM {databaseSchema}.{objectQualifier}category 
			 WHERE categoryid=iii_categoryid LIMIT 1) THEN 
	   iii_categoryid := NULL;
	END IF;
	-- Check valid MessageID
	IF iii_messageid IS NOT NULL 
				   AND NOT EXISTS
			 (SELECT 1 FROM {databaseSchema}.{objectQualifier}message 
			 WHERE messageid=iii_messageid LIMIT 1)  THEN
		iii_messageid := NULL;
	END IF;
	-- Check valid TopicID
	IF iii_topicid IS NOT NULL 
				 AND NOT EXISTS
			 (SELECT 1 FROM {databaseSchema}.{objectQualifier}topic 
			 WHERE topicid=iii_topicid LIMIT 1) THEN
		iii_topicid := NULL;
	END IF;

	-- find missing ForumID/TopicID
	IF iii_messageid IS NOT NULL and iii_messageid > 0 THEN
		SELECT
			c.categoryid,
			b.forumid,
			b.topicid
				INTO iii_categoryid,iii_forumid,iii_topicid
		FROM
			{databaseSchema}.{objectQualifier}message a
			INNER JOIN {databaseSchema}.{objectQualifier}topic b 
			ON b.topicid = a.topicid
			INNER JOIN {databaseSchema}.{objectQualifier}forum c 
			ON c.forumid = b.forumid
			INNER JOIN {databaseSchema}.{objectQualifier}category d 
			ON d.categoryid = c.categoryid
		WHERE
			a.messageid = iii_messageid AND
			d.boardid = i_boardid;
	ELSEIF iii_topicid IS NOT NULL AND iii_topicid > 0 THEN
		SELECT 
			b.categoryid,
			a.forumid 
				INTO iii_categoryid,iii_forumid
		FROM 
			{databaseSchema}.{objectQualifier}topic a
			inner join {databaseSchema}.{objectQualifier}forum b 
			on b.forumid = a.forumid
			inner join {databaseSchema}.{objectQualifier}category c 
			on c.categoryid = b.categoryid
		WHERE 
			a.topicid = iii_topicid AND
			c.boardid = i_boardid;
	
	ELSEIF iii_forumid IS NOT NULL AND iii_forumid > 0 THEN
		SELECT
			 a.categoryid
		INTO     iii_categoryid
					
		FROM	{databaseSchema}.{objectQualifier}forum a
			inner join {databaseSchema}.{objectQualifier}category b 
			on b.categoryid = a.categoryid
		WHERE
			a.forumid = iii_forumid and
			b.boardid = i_boardid;
	END IF;   

		
			IF not exists (select
			1	
			from {databaseSchema}.{objectQualifier}activeaccess 
			where userid = ici_userid limit 1) THEN				
			insert into {databaseSchema}.{objectQualifier}activeaccess(
			userid,
			boardid,
			forumid,
			isadmin, 
			isforummoderator,
			ismoderator, 
			isguestx,
			lastactive,
			readaccess,
			postaccess,
			replyaccess,
			priorityaccess,
			pollaccess,
			voteaccess,	
			moderatoraccess,
			editaccess,
			deleteaccess,
			uploadaccess,
			downloadaccess,
			userforumaccess)
			select
			/* userid,
			ici_boardid,
			forumid,
			isadmin::boolean, 
			isforummoderator::boolean,
			ismoderator::boolean, 
			(readaccess & 1 = 1)::boolean,
			(postaccess & 2  = 2)::boolean,
			(replyaccess & 4 = 4)::boolean,
			(priorityaccess & 8 = 8)::boolean,
			(pollaccess & 16 = 16)::boolean,
			(voteaccess & 32 = 32)::boolean,	
			(moderatoraccess & 64 = 64)::boolean,
			(editaccess & 128 = 128)::boolean,
			(deleteaccess & 256 = 256)::boolean,
			(uploadaccess & 512 = 512)::boolean,
			(downloadaccess & 1024 = 1024)::boolean */			
			"UserID",
			ici_userboardid,
			"ForumID",
			"IsAdmin"::boolean,
			"IsForumModerator"::boolean,
			"IsModerator"::boolean,
			ici_isguest,
			i_currenttime,
			"ReadAccess"::boolean,
			"PostAccess"::boolean,
			"ReplyAccess"::boolean,
			"PriorityAccess"::boolean,
			"PollAccess"::boolean,
			"VoteAccess"::boolean,
			"ModeratorAccess"::boolean,
			"EditAccess"::boolean,
			"DeleteAccess"::boolean,
			"UploadAccess"::boolean,
			"DownloadAccess"::boolean,
			"UserForumAccess"::boolean 			
			from {databaseSchema}.{objectQualifier}vaccess_combo_rows(ici_userid);
			END IF;

			IF ici_guestid != ici_userid 
			and not exists (select 1 from {databaseSchema}.{objectQualifier}activeaccess 
			where userid = ici_guestid limit 1) THEN				
			insert into {databaseSchema}.{objectQualifier}activeaccess(
			userid,
			boardid,
			forumid,
			isadmin, 
			isforummoderator,
			ismoderator, 
			isguestx,
			lastactive,
			readaccess,
			postaccess,
			replyaccess,
			priorityaccess,
			pollaccess,
			voteaccess,	
			moderatoraccess,
			editaccess,
			deleteaccess,
			uploadaccess,
			downloadaccess,
			userforumaccess)
			select
			/* userid,
			ici_boardid,
			forumid,
			isadmin::boolean, 
			isforummoderator::boolean,
			ismoderator::boolean, 
			(readaccess & 1 = 1)::boolean,
			(postaccess & 2  = 2)::boolean,
			(replyaccess & 4 = 4)::boolean,
			(priorityaccess & 8 = 8)::boolean,
			(pollaccess & 16 = 16)::boolean,
			(voteaccess & 32 = 32)::boolean,	
			(moderatoraccess & 64 = 64)::boolean,
			(editaccess & 128 = 128)::boolean,
			(deleteaccess & 256 = 256)::boolean,
			(uploadaccess & 512 = 512)::boolean,
			(downloadaccess & 1024 = 1024)::boolean */			
			"UserID",
			ici_userboardid,
			"ForumID",
			"IsAdmin"::boolean,
			"IsForumModerator"::boolean,
			"IsModerator"::boolean,
			ici_isguest,
			i_currenttime,
			"ReadAccess"::boolean,
			"PostAccess"::boolean,
			"ReplyAccess"::boolean,
			"PriorityAccess"::boolean,
			"PollAccess"::boolean,
			"VoteAccess"::boolean,
			"ModeratorAccess"::boolean,
			"EditAccess"::boolean,
			"DeleteAccess"::boolean,
			"UploadAccess"::boolean,
			"DownloadAccess"::boolean,
			"UserForumAccess"::boolean 			
			from {databaseSchema}.{objectQualifier}vaccess_combo_rows(ici_guestid);
			END IF;
 IF exists (select
			1	
			from {databaseSchema}.{objectQualifier}activeaccess 
			where userid = ici_userid and forumid = COALESCE(iii_forumid,0) and (COALESCE(iii_forumid,0) = 0 or readaccess) limit 1) THEN		
 -- get previous visit
	IF NOT ici_isguest THEN
		select  lastvisit  into ici_previousvisit from {databaseSchema}.{objectQualifier}user where userid = ici_userid;
	END IF;
	
	-- update last visit
	UPDATE {databaseSchema}.{objectQualifier}user SET 
		lastvisit = i_currenttime,
		ip = i_ip
	WHERE userid = ici_userid; 
			 DELETE FROM {databaseSchema}.{objectQualifier}active
			 WHERE  userid<>ici_userid  
			 AND boardid=ici_boardid 
			 AND sessionid=i_sessionid; 
		
	-- ensure that access right are in place		
	
	IF NOT i_donttrack
	THEN
	 -- user with the same session can be a user from other board or a guest who just logged in or user who just logged out. 
		IF EXISTS(SELECT 1 FROM {databaseSchema}.{objectQualifier}active 
		WHERE (sessionid=i_sessionid  AND boardid=ici_boardid OR ( browser = i_browser AND (flags & 8) = 8 ))  LIMIT 1) THEN
		  IF (CHAR_LENGTH(i_location) > 0) THEN
		  IF ici_crawler THEN
		  UPDATE {databaseSchema}.{objectQualifier}active SET
				userid = ici_userid,
				ip = i_ip,
				lastactive = i_currenttime,
				location = i_location,				
				forumid = iii_forumid,
				topicid = iii_topicid,
				browser = i_browser,
				platform = i_platform,
				forumpage = i_forumpage
			WHERE Browser = i_browser AND IP = i_ip and boardID=i_boardid;
		  ELSE
		  UPDATE {databaseSchema}.{objectQualifier}active SET
				userid = ici_userid,
				ip = i_ip,
				lastactive = i_currenttime,
				location = i_location,				
				forumid = iii_forumid,
				topicid = iii_topicid,
				browser = i_browser,
				platform = i_platform,
				forumpage = i_forumpage
			WHERE sessionid = i_sessionid AND boardid=i_boardid;
			END IF;
			ELSE
			UPDATE {databaseSchema}.{objectQualifier}active SET				
				lastactive = i_currenttime				
			WHERE sessionid = i_sessionid AND boardid=i_boardid;
			END IF;
			IF ici_isguest IS FALSE AND ici_crawler IS FALSE THEN		
				  ici_activeupdate := true;
			END IF;			
		ELSE
			-- we set @ActiveFlags ready flags 	
			INSERT INTO {databaseSchema}.{objectQualifier}active(sessionid,boardid,
			userid,ip,login,lastactive,location,
			forumid,topicid,browser,platform,forumpage, flags)
			VALUES(i_sessionid,ici_userboardid,
			ici_userid,i_ip,i_currenttime,i_currenttime,i_location,
			iii_forumid,iii_topicid,i_browser,i_platform, i_forumpage, ici_activeflags);
			-- parameter to update active users cache IF this is a new user
			IF ici_isguest IS FALSE THEN		
				  ici_activeupdate := true;
			END IF;
		  
			/* returning COUNT(DISTINCT ip) INTO ici_active_return;
			IF NOT EXISTS ( SELECT 1 FROM {databaseSchema}.{objectQualifier}registry 
	WHERE boardid = i_boardid and name = 'maxusers' ) THEN
	INSERT INTO {databaseSchema}.{objectQualifier}registry
(boardid,
name,
value)
VALUES     (i_boardid,
'maxusers',
COALESCE((SELECT  COUNT(DISTINCT a.ip)
FROM   {databaseSchema}.{objectQualifier}active a
WHERE  a.boardid = i_boardid),1)::text);
INSERT INTO {databaseSchema}.{objectQualifier}registry
(boardid,
name,
value)
VALUES     (i_boardid,
'maxuserswhen',
i_utctimestamp::text);
	
	ELSEIF (COALESCE((SELECT  COUNT(DISTINCT a.ip)
FROM   {databaseSchema}.{objectQualifier}active a
WHERE  a.boardid = i_boardid),1) > COALESCE((SELECT value  
FROM   {databaseSchema}.{objectQualifier}registry
WHERE  boardid = i_boardid
AND name = 'maxusers'),'0')::integer)	THEN
	
		 In the case we of course simply update 2 registry values
UPDATE {databaseSchema}.{objectQualifier}registry
SET    value = ici_count::text
WHERE  boardid = i_boardid
AND name = 'maxusers';

UPDATE {databaseSchema}.{objectQualifier}registry
SET    value = i_utctimestamp::text
WHERE  boardid = i_boardid
AND name = 'maxuserswhen';
	END  IF;
*/

			-- see notes to the function
			PERFORM {databaseSchema}.{objectQualifier}active_updatemaxstats(i_boardid, i_utctimestamp);
		END IF;
		-- remove duplicate users
		IF ici_isguest IS FALSE THEN
			DELETE FROM {databaseSchema}.{objectQualifier}active
			 WHERE userid=ici_userid 
			 AND boardid=ici_boardid 
			 AND sessionid<>i_sessionid; 
		END IF;	
	END IF;	
	END IF;	         
	FOR  _rec1 IN SELECT
	 COALESCE(ici_activeupdate,false),
	/* get previous visit */
	(CASE WHEN ici_isguest IS FALSE THEN
		(SELECT  lastvisit
		FROM {databaseSchema}.{objectQualifier}user WHERE userid = ici_userid)  ELSE NULL END) AS PreviousVisit,
		x.userid AS UserID,
		x.forumid,
		x.isadmin::boolean AS IsAdmin,   
		(CASE WHEN (i_userkey IS NULL) THEN true ELSE false END) AS IsGuest,    
		x.isforummoderator::boolean AS IsForumModerator,
		x.ismoderator::boolean AS IsModerator,     
		x.lastactive,
x.readaccess,			
x.postaccess,
x.replyaccess,
x.priorityaccess,
x.pollaccess,
x.voteaccess,
x.moderatoraccess,
x.editaccess,
x.deleteaccess,
x.uploadaccess,		
x.downloadaccess,
x.userforumaccess, 
ici_crawler,
i_ismobiledevice,		
		COALESCE(iii_categoryid,0) AS CategoryID,
		(SELECT name 
		FROM {databaseSchema}.{objectQualifier}category
		 WHERE categoryid = iii_categoryid) AS CategoryName,			
		(SELECT name from {databaseSchema}.{objectQualifier}forum 
		where forumid = iii_forumid) AS ForumName,
		(SELECT themeurl from {databaseSchema}.{objectQualifier}forum 
		where forumid = iii_forumid) AS "ForumTheme",		
		(SELECT iii_topicid) AS TopicID,
		(SELECT topic from {databaseSchema}.{objectQualifier}topic
		 where topicid = iii_topicid) AS TopicName
		FROM
		 {databaseSchema}.{objectQualifier}activeaccess x 
	where
		x.userid = ici_userid and x.forumid=COALESCE(iii_forumid,0)	ORDER BY x.userid LIMIT 1 	
		LOOP		
		RETURN NEXT _rec1;
		END LOOP;
	
/* Is it good */
--SET TRANSACTION ISOLATION LEVEL READ COMMITTED;
 /*SET AUTOCOMMIT=0;*/
-- COMMIT;
-- RETURN;
END;$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100;
 --GO   

-- Function: {databaseSchema}.{objectQualifier}rsstopic_list(integer)
DROP FUNCTION IF EXISTS {databaseSchema}.{objectQualifier}rsstopic_list(
							integer,
							integer,
							integer);
--GO
CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}rsstopic_list(
						   iii_forumid integer,						
						   i_start integer,
						   i_limit integer)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}rsstopic_list_return_type AS
$BODY$DECLARE
			 cntr integer:=0; 
			 _rec {databaseSchema}.{objectQualifier}rsstopic_list_return_type%ROWTYPE;
BEGIN
FOR _rec IN
SELECT 
a.topic, 
a.topicid, 
b.name, 
COALESCE(a.lastposted, a.posted),
COALESCE(a.lastuserid, a.userid),
COALESCE(a.lastmessageid,(select m.messageID 
	  from {databaseSchema}.{objectQualifier}message m where m.TopicID = a.TopicID order by m.Posted desc limit 1)), 
COALESCE(a.lastmessageflags,22) 
FROM {databaseSchema}.{objectQualifier}topic a,
{databaseSchema}.{objectQualifier}forum b 
WHERE a.forumid = iii_forumid AND b.forumid = a.forumid
AND SIGN(a.flags & 8) = 0 AND a.topicmovedid IS NULL
ORDER BY a.posted DESC
LOOP
IF cntr > i_limit THEN EXIT;END IF;
cntr:= cntr+1;
IF cntr between i_start and i_limit THEN RETURN NEXT _rec; END IF;
END LOOP;

END;$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100
  ROWS 1000;
--GO

-- User Ignore Procedures 

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}user_addignoreduser(
						   i_userid INTEGER,						   
						   i_ignoreduserid INTEGER)
				  RETURNS void AS
$BODY$	
BEGIN
	IF NOT EXISTS (SELECT 1 FROM {databaseSchema}.{objectQualifier}ignoreuser WHERE userid = i_userid AND ignoreduserid = i_ignoreduserid LIMIT 1)
	THEN
		INSERT INTO {databaseSchema}.{objectQualifier}ignoreuser (userid, ignoreduserid) VALUES (i_userid, i_ignoreduserid);
	END IF;
END;$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER;
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}user_removeignoreduser(
						   i_userid INTEGER,
						   i_ignoreduserid INTEGER)
				  RETURNS void 
AS
  'DELETE FROM {databaseSchema}.{objectQualifier}ignoreuser 
	WHERE userid = $1 AND ignoreduserid = $2;'
LANGUAGE 'sql' VOLATILE SECURITY DEFINER;
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}user_isuserignored
						   (i_userid       integer,
						   i_ignoreduserid integer)
				  RETURNS boolean AS
$BODY$
BEGIN
	IF EXISTS(SELECT * FROM {databaseSchema}.{objectQualifier}ignoreuser 
	WHERE userid = i_userid AND ignoreduserid = i_ignoreduserid)
	THEN
		RETURN true;	
	ELSE	
		RETURN false;
	END IF;	
END;$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER;
--GO

/*****************************************************************************************************
//  Original code by: DLESKTECH at http://www.dlesktech.com/support.aspx
//  Modifications by: KASL Technologies at www.kasltechnologies.com
//  Modifications for integration into YAF/Conventions by Jaben Cargman
//  Modifications for PostgreSQL by vzrus
*****************************************************************************************************/
CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}shoutbox_getmessages(
						   i_boardid         integer,
						   i_numberofmessages integer,
						   i_stylednicks      boolean)
				  RETURNS SETOF  {databaseSchema}.{objectQualifier}shoutbox_getmessages_return_type AS
$BODY$DECLARE 
			 _rec {databaseSchema}.{objectQualifier}shoutbox_getmessages_return_type%ROWTYPE;
BEGIN
 FOR _rec IN
	SELECT	    
		sh.shoutboxmessageid,
		sh.userid,
		sh.username,
		us.displayname,	
		sh.message,
		sh."date",
		(case when i_stylednicks
			then us.userstyle 
			else ''	 end) 		
	FROM
		{databaseSchema}.{objectQualifier}shoutboxmessage sh
		JOIN {databaseSchema}.{objectQualifier}user us
		ON us.userid = sh.userid
	WHERE sh.boardid = i_boardid and us.userid = sh.userid
	ORDER BY sh."date" DESC
	LOOP
	RETURN NEXT _rec;
	END LOOP;
END;
$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER COST 100 ROWS 1000;
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}shoutbox_savemessage
						   (i_boardid      integer,
						   i_userid        integer,
						   i_username		varchar(255),                         
						   i_message		text,
						   i_date			timestamp,
						   i_ip			    varchar(39),
						   i_utctimestamp timestamp)
				  RETURNS void AS
$BODY$DECLARE ici_OverrideDisplayName boolean;
 BEGIN
	 SELECT EXISTS (SELECT 1 FROM {databaseSchema}.{objectQualifier}user WHERE userid = i_userid and name != i_username) INTO ici_OverrideDisplayName;
   INSERT INTO {databaseSchema}.{objectQualifier}shoutboxmessage(userid, username, userdisplayname, boardid,  message, "date", "ip")
	VALUES (i_userid,i_username,(CASE WHEN ici_OverrideDisplayName is true THEN i_username ELSE (SELECT displayname FROM {databaseSchema}.{objectQualifier}user WHERE userid = i_userid) END), i_boardid, i_message, COALESCE(i_date, i_utctimestamp), i_ip);
END;
$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER COST 100;
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}shoutbox_clearmessages(
						   i_boardid integer,
						   i_utctimestamp timestamp)
				  RETURNS void 
AS
  'DELETE FROM {databaseSchema}.{objectQualifier}shoutboxmessage WHERE boardid = $1 AND EXTRACT(DAY FROM ($2 - "date")) > 1;'
 
   LANGUAGE 'sql' VOLATILE SECURITY DEFINER;
--GO


/* Functions for "Thanks" Mod */
CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}message_addthanks
						   (i_fromuserid integer,
						   i_messageid integer,
						   i_utctimestamp timestamp,
						   i_usedisplayname boolean)
				  RETURNS varchar(128)
AS
$BODY$DECLARE 
ici_touserid integer;
BEGIN
IF NOT EXISTS (
		   SELECT 
		   thanksid 
		   FROM {databaseSchema}.{objectQualifier}thanks 
		   WHERE messageid = i_messageid 
		   AND thanksfromuserid = i_fromuserid 
		   LIMIT 1 FOR SHARE
			  ) THEN
	SELECT userid INTO ici_touserid 
	FROM {databaseSchema}.{objectQualifier}message 
	WHERE messageid = i_messageid;

	INSERT INTO {databaseSchema}.{objectQualifier}thanks(thanksfromuserid, thankstouserid, messageid, thanksdate)
	VALUES (i_fromuserid, ici_touserid, i_messageid, i_utctimestamp);
	RETURN (
			SELECT (CASE WHEN i_usedisplayname THEN displayname ELSE name END)  
			FROM {databaseSchema}.{objectQualifier}user 
			WHERE userid = ici_touserid
		   );
END IF;

RETURN '';

END;
$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER;
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}message_getthanks 
						  (i_messageid integer)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}message_getthanks_return_type
AS
$BODY$DECLARE 
_rec {databaseSchema}.{objectQualifier}message_getthanks_return_type%ROWTYPE;
BEGIN
 FOR _rec IN
	SELECT a.thanksfromuserid as UserID, a.thanksdate, b.name, b.displayname
	FROM {databaseSchema}.{objectQualifier}thanks a 
	INNER JOIN {databaseSchema}.{objectQualifier}user b
	ON a.thanksfromuserid = b.userid WHERE messageid = i_messageid
	ORDER BY a.thanksdate DESC
	LOOP
	RETURN NEXT _rec;
	END LOOP;
END;
$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER COST 100 ROWS 1000;
--GO

DROP FUNCTION IF EXISTS {databaseSchema}.{objectQualifier}message_isthankedbyuser(integer,integer);
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}message_isthankedbyuser(
						   i_userid integer,
						   i_messageid integer)
				  RETURNS boolean AS
$BODY$
BEGIN
	IF NOT EXISTS (SELECT thanksid FROM {databaseSchema}.{objectQualifier}thanks WHERE ThanksFromUserID=i_userid AND MessageID=i_messageid LIMIT 1) THEN
		RETURN FALSE;
	ELSE
		RETURN TRUE;
	END IF;
END;
$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER;
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}message_removethanks(
						   i_fromuserid integer,
						   i_messageid integer,
						   i_usedisplayname boolean)
				  RETURNS varchar(128) AS
$BODY$
BEGIN
	DELETE FROM {databaseSchema}.{objectQualifier}thanks WHERE thanksfromuserid=i_fromuserid AND messageid=i_messageid;
	RETURN (SELECT (CASE WHEN i_usedisplayname THEN displayname ELSE name END) FROM {databaseSchema}.{objectQualifier}user WHERE userid IN (SELECT userid FROM {databaseSchema}.{objectQualifier}message WHERE messageid = i_messageid));
END;
$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER;
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}message_thanksnumber(
						   i_messageid integer)
				  RETURNS integer AS
'SELECT COALESCE(Count(1)::integer, 0::integer) from {databaseSchema}.{objectQualifier}thanks WHERE messageid=$1;'
LANGUAGE 'sql' STABLE SECURITY DEFINER;
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}user_thankedmessage(
						   i_messageid integer,
						   i_userid integer)
				  RETURNS integer AS
'SELECT COALESCE(Count(1)::integer, 0::integer) from {databaseSchema}.{objectQualifier}thanks WHERE messageid=$1 and thanksfromuserid =$2;'
LANGUAGE 'sql' STABLE SECURITY DEFINER;
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}user_getthanks_from(
						   i_userid integer,
						   i_pageuserid integer)
				  RETURNS integer AS
'SELECT Count(1)::integer FROM {databaseSchema}.{objectQualifier}thanks WHERE thanksfromuserid=$1;'
LANGUAGE 'sql' STABLE SECURITY DEFINER;
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}user_getthanks_to(
				  i_userid integer,
				  i_pageuserid integer,
			  out i_thankstonumber integer,
			  out i_thankstopostsnumber integer)
-- RETURNS {databaseSchema}.{objectQualifier}user_getthanksto_return_type
AS
'SELECT 
Count(1)::integer, 
Count(DISTINCT messageid)::integer FROM {databaseSchema}.{objectQualifier}thanks WHERE thankstouserid=$1 GROUP BY thanksid, thanksfromuserid,thankstouserid,messageid;'	
LANGUAGE 'sql' STABLE SECURITY DEFINER;
--GO

  CREATE OR REPLACE FUNCTION  {databaseSchema}.{objectQualifier}user_viewthanksfrom(
							i_userid integer,
							i_pageuserid integer,
							i_pageindex integer,
							i_pagesize integer)
				  RETURNS SETOF  {databaseSchema}.{objectQualifier}user_viewthankstofrom_return_type 
AS
$BODY$DECLARE 
			 ici_totalrows int :=0; 
			 ici_FirstSelectRowID int :=0; 
			 ici_firstselectrownumber int :=0; 
			 _rec {databaseSchema}.{objectQualifier}user_viewthankstofrom_return_type%ROWTYPE;
BEGIN
 i_pageindex := i_pageindex + 1;
		 
		select count(1) into ici_totalrows 
				from
		{databaseSchema}.{objectQualifier}thanks t
		join {databaseSchema}.{objectQualifier}message c on c.messageid = t.messageid
		join {databaseSchema}.{objectQualifier}topic a on a.topicid = c.topicid
		join {databaseSchema}.{objectQualifier}user b on b.userid = c.userid                
		join {databaseSchema}.{objectQualifier}activeaccess ac on (ac.forumid = a.forumid and ac.userid = i_pageuserid)
		where 
			 ac.readaccess and
			 a.topicmovedid IS NULL and	
						 t.thanksfromuserid = i_userid 
				and		    
			-- is approved	
				(c.flags & 2) = 2                
				-- not deleted
				AND COALESCE(a.flags,0) & 8 <> 8
				AND COALESCE(c.flags,0) & 8 <> 8 ;
			
		ici_firstselectrownumber := (i_pageindex - 1) * i_pagesize; 
FOR _rec in
	SELECT 	    t.thankstouserid,
				c.messageid,
				a.forumid,
				a.topicid,
				a.topic,
				b.userid,
				b.displayname,
				b.name,               
				c.posted,
				c.message,
				c.flags,
				ici_totalrows
		from
		{databaseSchema}.{objectQualifier}thanks t
		join {databaseSchema}.{objectQualifier}message c on c.messageid = t.messageid
		join {databaseSchema}.{objectQualifier}topic a on a.topicid = c.topicid
		join {databaseSchema}.{objectQualifier}user b on c.userid = b.userid 
		join {databaseSchema}.{objectQualifier}activeaccess ac on (ac.forumid = a.forumid and ac.userid = i_pageuserid)
		where 
			 ac.readaccess and
			-- is apploved	
				(c.flags & 2) = 2
				AND a.topicmovedid IS NULL
				-- not deleted
				AND COALESCE(a.flags,0) & 8 <> 8
				AND COALESCE(c.flags,0) & 8 <> 8
				and
			 t.thanksfromuserid = i_userid		and		    
			-- is approved	
				(c.flags & 2) = 2                
				-- not deleted
				AND COALESCE(a.flags,0) & 8 <> 8
				AND COALESCE(c.flags,0) & 8 <> 8 	
		ORDER BY c.posted DESC OFFSET ici_firstselectrownumber LIMIT i_pagesize
		loop	
		RETURN NEXT _rec;       
		end loop;   
	END;
$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER COST 100 ROWS 1000;
  --GO

  CREATE OR REPLACE FUNCTION  {databaseSchema}.{objectQualifier}user_viewthanksto(
							i_userid integer,
							i_pageuserid integer,
							i_pageindex integer,
							i_pagesize integer)
				  RETURNS SETOF  {databaseSchema}.{objectQualifier}user_viewthankstofrom_return_type 
AS
$BODY$DECLARE 
			 ici_totalrows int :=0; 
			 ici_FirstSelectRowID int :=0; 
			 ici_firstselectrownumber int :=0; 
			 _rec {databaseSchema}.{objectQualifier}user_viewthankstofrom_return_type%ROWTYPE;
BEGIN
 i_pageindex := i_pageindex + 1;
		 
		select count(1) into ici_totalrows 
				from
		{databaseSchema}.{objectQualifier}thanks t
		join {databaseSchema}.{objectQualifier}message c on c.messageid = t.messageid
		join {databaseSchema}.{objectQualifier}topic a on a.topicid = c.topicid
		join {databaseSchema}.{objectQualifier}user b on c.userid = b.userid                
		join {databaseSchema}.{objectQualifier}activeaccess ac on (ac.forumid = a.forumid and ac.userid = i_pageuserid)
		where 
			 ac.readaccess and
			 a.topicmovedid IS NULL and	
			 t.thankstouserid = i_userid 	and		    
			-- is approved	
				(c.flags & 2) = 2                
				-- not deleted
				AND COALESCE(a.flags,0) & 8 <> 8
				AND COALESCE(c.flags,0) & 8 <> 8 ;
			
		ici_firstselectrownumber := (i_pageindex - 1) * i_pagesize; 
FOR _rec in
	SELECT  t.thanksfromuserid,                
				c.messageid,
				a.forumid,
				a.topicid,
				a.topic,
				b.userid, 
				b.displayName,
				b.name,         
				c.posted,
				c.message,
				c.flags,
				ici_totalrows
		from
		{databaseSchema}.{objectQualifier}thanks t
		join {databaseSchema}.{objectQualifier}message c on c.messageid = t.messageid
		join {databaseSchema}.{objectQualifier}topic a on a.topicid = c.topicid
		join {databaseSchema}.{objectQualifier}user b on c.userid = b.userid 
		join {databaseSchema}.{objectQualifier}activeaccess ac on (ac.forumid = a.forumid and ac.userid = i_pageuserid)
		where 
			 ac.readaccess and
			-- is approved	
				(c.flags & 2) = 2
				AND a.topicmovedid IS NULL
				-- not deleted
				AND COALESCE(a.flags,0) & 8 <> 8
				AND COALESCE(c.flags,0) & 8 <> 8
				and
			t.thankstouserid = i_userid 		
		ORDER BY c.posted DESC OFFSET ici_firstselectrownumber LIMIT i_pagesize
		loop	
		RETURN NEXT _rec;       
		end loop;   
	END;
$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER COST 100 ROWS 1000;
  --GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}message_getallthanks(
						   i_messageids text)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}message_getallthanks_return_type AS
$BODY$
DECLARE 
ici_messageid varchar(11);
ici_messageids text := TRIM(BOTH FROM i_messageids) || ',';
ici_pos integer := POSITION(',' IN ici_messageids);
ici_messagearray int array;
ici_msgcntr int := 0;
_rec {databaseSchema}.{objectQualifier}message_getallthanks_return_type%ROWTYPE;
BEGIN 
-- ici_messageids := TRIM(BOTH FROM i_messageids) || ',';
-- ici_pos := POSITION(',' IN ici_messageids);

IF REPLACE(ici_messageids, ',', '') <> '' THEN

 WHILE ici_pos > 0 
	   LOOP		 
			-- left function replaced by substring 
			ici_messageid := TRIM(BOTH FROM (SUBSTRING(ici_messageids FOR ici_pos - 1)));
			IF ici_messageid <> '' THEN	
				ici_messagearray[ici_msgcntr] := (CAST(ici_messageid AS integer));				
				ici_msgcntr := ici_msgcntr + 1;
				--Use Appropriate conversion
			END IF;
			-- right implementation here we remove first id from string
			ici_messageids := SUBSTRING(ici_messageids from ici_pos+1 for (char_length(ici_messageids) - ici_pos));
			ici_pos := POSITION(',' IN ici_messageids);			
	   END LOOP;
							-- to be sure that last value is inserted
IF (LENGTH(ici_messageids)>0) THEN
ici_msgcntr := ici_msgcntr + 1;
ici_messagearray[ici_msgcntr] := (CAST(ici_messageids AS integer));                          
						   END IF;          

END	IF;	   
   FOR _rec IN	SELECT 
		b.thanksfromuserid AS FromUserID, 
		b.thanksdate, 
		d.messageid, 
		b.thankstouserid AS toUserID,
		(SELECT COUNT(thanksid) FROM {databaseSchema}.{objectQualifier}thanks b WHERE b.thanksfromuserid=d.userid) AS ThanksFromUserNumber,
		(SELECT COUNT(thanksid) FROM {databaseSchema}.{objectQualifier}thanks b WHERE b.thankstouserid=d.userid) AS ThanksToUserNumber,
		(SELECT COUNT(*) FROM (SELECT DISTINCT(messageid) FROM {databaseSchema}.{objectQualifier}thanks b WHERE b.thankstouserid=d.userID) as tmp) AS ThanksToUserPostsNumber
	FROM  {databaseSchema}.{objectQualifier}message d 
	LEFT JOIN {databaseSchema}.{objectQualifier}thanks b ON (b.messageid = d.messageid)
	WHERE d.messageid IN  (SELECT * FROM unnest(ici_messagearray))	
	LOOP
	RETURN NEXT _rec;    
	END LOOP;
END;
$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER COST 100 ROWS 1000;
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}readtopic_addorupdate(
						   i_userid integer, 
						   i_topicid integer,
						   i_utctimestamp timestamp)				 
				  RETURNS void AS
$BODY$
declare ici_lastreadaccess	timestamp;
BEGIN
select lastaccessdate INTO ici_lastreadaccess from {databaseSchema}.{objectQualifier}topicreadtracking where userid=i_userid AND topicid=i_topicid limit 1;

	IF ici_lastreadaccess is not null THEN	
		  update {databaseSchema}.{objectQualifier}topicreadtracking set lastaccessdate=i_utctimestamp where lastaccessdate = ici_lastreadaccess;
 
	ELSE
	  
		  insert into {databaseSchema}.{objectQualifier}topicreadtracking(userid,topicid,lastaccessdate)
		  values (i_userid, i_topicid, i_utctimestamp);
	  END IF;
	END;
$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER COST 100;
  --GO
DROP FUNCTION IF EXISTS  {databaseSchema}.{objectQualifier}readtopic_delete(integer);
--GO
CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}readtopic_delete(
						   i_userid integer)				 
				  RETURNS void AS
$BODY$
BEGIN
		delete from {databaseSchema}.{objectQualifier}topicreadtracking where userid = i_userid;
	END;
$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER COST 100;
  --GO

 DROP FUNCTION  IF EXISTS {databaseSchema}.{objectQualifier}user_lastread(integer) ;
  --GO
  DROP FUNCTION  IF EXISTS {databaseSchema}.{objectQualifier}user_lastread(integer, timestamp ) ;
  --GO
CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}user_lastread(
						   i_userid integer)				 
				  RETURNS timestamp  AS
$BODY$
DECLARE ici_LastForumRead timestamp ;
		ici_LastTopicRead timestamp ;	
BEGIN		
		
		SELECT  lastaccessdate INTO ici_LastForumRead FROM  {databaseSchema}.{objectQualifier}ForumReadTracking WHERE userid = i_userid ORDER BY lastaccessdate DESC LIMIT 1;
		SELECT  lastaccessdate INTO ici_LastTopicRead FROM {databaseSchema}.{objectQualifier}TopicReadTracking WHERE userid = i_userid ORDER BY lastaccessdate DESC LIMIT 1;

		IF ici_LastForumRead is not null AND ici_LastTopicRead is not null THEN
		
		IF ici_LastForumRead > ici_LastTopicRead THEN
		   RETURN ici_LastForumRead;
		ELSE
		   RETURN ici_LastTopicRead;
		END IF;  		   
		ELSEIF ici_LastForumRead is not null THEN
		   RETURN  ici_LastForumRead;
			
		ELSEIF  ici_LastTopicRead is not null THEN
			RETURN ici_LastTopicRead;
		END IF; 
		-- always null
		RETURN NULL;	
	END;
$BODY$
  LANGUAGE 'plpgsql' STABLE  SECURITY DEFINER COST 100 ;
  --GO


  CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}readtopic_lastread(
						   i_userid integer, i_topicid integer)				 
				  RETURNS timestamp  AS
$BODY$
BEGIN
	RETURN (SELECT LastAccessDate FROM  {databaseSchema}.{objectQualifier}topicreadtracking WHERE userid = i_userid AND topicid = i_topicid);
	END;
$BODY$
  LANGUAGE 'plpgsql' STABLE  SECURITY DEFINER COST 100;
  --GO
CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}readforum_addorupdate(
						   i_userid integer, i_forumid integer,i_utctimestamp timestamp)				 
				  RETURNS void AS
$BODY$
declare ici_lastreadaccess	timestamp;	
BEGIN		
select lastaccessdate into ici_lastreadaccess from {databaseSchema}.{objectQualifier}forumreadtracking where userid=i_userid AND forumid=i_forumid limit 1;
	IF ici_lastreadaccess is not null THEN
		  update {databaseSchema}.{objectQualifier}forumreadtracking set lastaccessdate=i_utctimestamp where lastaccessdate = ici_lastreadaccess;
	ELSE	 
		  insert into {databaseSchema}.{objectQualifier}forumreadtracking(userid,forumid,lastaccessdate)
		  values (i_userid, i_forumid, i_utctimestamp);
	END IF;
   

	-- Delete TopicReadTracking for forum
	DELETE
	FROM {databaseSchema}.{objectQualifier}topicreadtracking
	WHERE userid = i_userid
		AND topicid IN (
			SELECT topicid
			FROM {databaseSchema}.{objectQualifier}topic
			WHERE forumid = i_forumid
			);
			END;
$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER COST 100;
 --GO
DROP FUNCTION IF EXISTS  {databaseSchema}.{objectQualifier}readforum_delete(integer);
--GO	
CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}readforum_delete(
						   i_userid integer)				 
				  RETURNS void  AS
$BODY$		
BEGIN
		delete from {databaseSchema}.{objectQualifier}forumreadtracking where userid = i_userid;
	END;
$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER COST 100;

   --GO
CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}readforum_lastread(
						   i_userid integer, i_forumid integer)				 
				  RETURNS timestamp  AS
$BODY$
BEGIN
	RETURN (SELECT LastAccessDate FROM  {databaseSchema}.{objectQualifier}forumreadtracking WHERE userid = i_userid AND forumid = i_forumid);
	END;
$BODY$
  LANGUAGE 'plpgsql' STABLE  SECURITY DEFINER COST 100;
  --GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}topic_favorite_add(
						   i_userid integer,
						   i_topicid integer)
				  RETURNS void AS
$BODY$
BEGIN
	 IF NOT EXISTS (SELECT 1 FROM {databaseSchema}.{objectQualifier}favoritetopic WHERE (userid = i_userid AND topicid=i_topicid) LIMIT 1) THEN
					INSERT INTO {databaseSchema}.{objectQualifier}favoritetopic (userid, topicid) 
					VALUES (i_userid, i_topicid);
	 END IF;
END;
$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER;  
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}topic_favorite_remove(
						   i_userid integer,
						   i_topicid integer)
				  RETURNS void AS
$BODY$
BEGIN
	 DELETE FROM {databaseSchema}.{objectQualifier}favoritetopic WHERE userid=i_userid AND topicid=i_topicid;
END;
$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER;
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}topic_favorite_list(
						   i_userid integer)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}topic_favorite_list_return_type AS
$BODY$DECLARE
			 _rec {databaseSchema}.{objectQualifier}topic_favorite_list_return_type%ROWTYPE;
BEGIN
FOR _rec IN
SELECT topicid FROM {databaseSchema}.{objectQualifier}favoritetopic WHERE userid=i_userid
LOOP
RETURN NEXT _rec;
END LOOP;
END;
$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER COST 100 ROWS 1000;
--GO



CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}topic_favorite_details(
i_boardid integer, 
i_categoryid integer, 
i_pageuserid integer, 
i_sincedate timestamp, 
i_todate timestamp, 
i_pageindex integer, 
i_pagesize integer, 
i_stylednicks boolean,
i_findlastunread boolean,
i_utctimestamp timestamp)
  RETURNS  SETOF {databaseSchema}.{objectQualifier}topic_favorite_details_return_type AS
$BODY$DECLARE 
 ici_topics_totalrowsnumber  integer; 
 ici_firstselectrownum integer;   
 ici_firstselectposted timestamp ;  
 ici_pageindex integer := i_pageindex;
 ici_retcount integer := 0;
 ici_counter integer := 0;
_rec {databaseSchema}.{objectQualifier}topic_favorite_details_return_type%ROWTYPE;
BEGIN  
		-- find total returned count
		select
		COUNT(1) into ici_topics_totalrowsnumber
			from
		{databaseSchema}.{objectQualifier}topic c
		join {databaseSchema}.{objectQualifier}user b on b.UserID=c.UserID
		join {databaseSchema}.{objectQualifier}forum d on d.ForumID=c.ForumID
		join {databaseSchema}.{objectQualifier}activeaccess x on x.ForumID=d.ForumID
		join {databaseSchema}.{objectQualifier}category cat on cat.CategoryID=d.CategoryID
	where
		(c.lastposted between i_sincedate and i_todate) and
		x.userid = i_pageuserid and
		x.readaccess IS TRUE and
		cat.boardid = i_boardid and
		(i_categoryid is null or cat.categoryid=i_categoryid) and
		c.isdeleted IS FALSE;

		 ici_pageindex := ici_pageindex+1;
		 ici_firstselectrownum := (ici_pageindex - 1) * i_pagesize + 1;

	SELECT 		
		c.lastposted
	into ici_firstselectposted
		from
		{databaseSchema}.{objectQualifier}topic c
		join {databaseSchema}.{objectQualifier}user b on b.userid=c.userid
		join {databaseSchema}.{objectQualifier}forum d on d.forumid=c.forumid
		join {databaseSchema}.{objectQualifier}activeaccess x on x.forumid=d.forumid
		join {databaseSchema}.{objectQualifier}category cat on cat.categoryid=d.categoryid
		JOIN {databaseSchema}.{objectQualifier}favoritetopic z ON z.topicid=c.topicid 
		AND z.userid=i_pageuserid
	where
		(c.lastposted between i_sincedate and i_todate) and
		x.userid = i_pageuserid and
		x.readaccess IS TRUE and
		cat.boardid = i_boardid and
		(i_categoryid is null or cat.categoryid=i_categoryid) and
		-- topic is not deleted 
		c.isdeleted IS FALSE
	order by
		c.lastposted desc,
		d.name asc,
		c.priority desc;	

FOR _rec IN
		select
		c.forumid,
		c.topicid,
		c.topicmovedid,
		c.posted,
		COALESCE(c.topicmovedid,c.topicid) AS LinkTopicID,
		c.topic AS Subject,
		c.status,
		c.styles,
		c.description,
		c.userid,
		COALESCE(c.username,b.name) AS Starter,
		COALESCE(c.userdisplayname,b.displayname) AS StarterDisplay,
		(SELECT COUNT(mes.messageid) FROM {databaseSchema}.{objectQualifier}message mes 
		-- isdeleted 
		WHERE mes.topicid = c.topicid AND SIGN(mes.flags & 8) = 1 
		-- IsApproved 
		AND SIGN(mes.flags & 16) = 1 
		AND ((i_pageuserid IS NOT NULL AND mes.userid = i_pageuserid) 
		OR (i_pageuserid IS NULL))) AS NumPostsDeleted,
		(select count(x.messageid) - 1 from {databaseSchema}.{objectQualifier}message x 
		where x.topicid=c.topicid and SIGN(x.flags & 8) = 0) AS Replies,
		c.views AS Views,
		c.lastposted AS LastPosted,
		c.lastuserid AS LastUserID,
		COALESCE(c.lastusername,(select x.name from {databaseSchema}.{objectQualifier}user x where x.userid=c.lastuserid)) AS LastUserName,
		COALESCE(c.lastuserdisplayname,(select x.displayname from {databaseSchema}.{objectQualifier}user x where x.userid=c.lastuserid)) AS LastUserDisplayName,
		c.lastmessageid AS LastMessageID,
		c.lastmessageflags,
		c.topicid AS LastTopicID,
		c.flags AS TopicFlags,
		(SELECT COUNT(id)  FROM {databaseSchema}.{objectQualifier}favoritetopic WHERE topicid = COALESCE(c.topicmovedid,c.topicid)) AS FavoriteCount,
		c.priority,
		c.pollid,
		d.name AS ForumName,		
		d.flags AS ForumFlags,
		(SELECT message 
		FROM {databaseSchema}.{objectQualifier}message mes2 
		where mes2.TopicID = COALESCE(c.topicmovedid,c.topicid) AND mes2.position = 0 LIMIT 1) AS FirstMessage,
		(case(i_stylednicks)
			when true THEN   (SELECT us.userstyle FROM {databaseSchema}.{objectQualifier}user us where us.userid = c.userid)  
			else ''	 end) AS StarterStyle,
		(case(i_stylednicks)
			when true THEN   (SELECT us.userstyle FROM {databaseSchema}.{objectQualifier}user us where us.userid = c.lastuserid)    
			else ''	 end) AS LastUserStyle,
		(case(i_findlastunread)
			 when true THEN
			   COALESCE((SELECT lastaccessdate FROM {databaseSchema}.{objectQualifier}forumreadtracking x WHERE x.forumid=d.forumid AND x.userid = i_pageuserid limit 1), i_utctimestamp)
			 else TIMESTAMP '-infinity' end) AS LastForumAccess,
		(case(i_findlastunread)
			 when true THEN
			   COALESCE((SELECT lastaccessdate FROM {databaseSchema}.{objectQualifier}topicreadtracking y WHERE c.topicid=c.topicid AND y.userid = i_pageuserid limit 1), i_utctimestamp)
			 else TIMESTAMP '-infinity'	 end) AS LastTopicAccess,
		(SELECT string_agg(tag, ',') FROM {databaseSchema}.{objectQualifier}tags tg join {databaseSchema}.{objectQualifier}topictags tt on tt.tagid = tg.tagid where 	  tt.topicid = c.topicid),	
		c.topicimage,
		c.topicimagetype,
		c.topicimagebin,
		0 as HasAttachments,	 	
		ici_topics_totalrowsnumber AS TotalRows,
		ici_pageindex	AS 	PageIndex	  	     
	from
		{databaseSchema}.{objectQualifier}topic c
		join {databaseSchema}.{objectQualifier}user b on b.userid=c.userid
		join {databaseSchema}.{objectQualifier}forum d on d.forumid=c.forumid
		join {databaseSchema}.{objectQualifier}activeaccess x on x.forumid=d.forumid
		join {databaseSchema}.{objectQualifier}category cat on cat.categoryid=d.categoryid
		JOIN {databaseSchema}.{objectQualifier}favoritetopic z ON (z.topicid=c.topicid 
		AND z.userid=i_pageuserid)
	where
		c.lastposted <= ici_firstselectposted and
		x.userid = i_pageuserid and
		x.readaccess IS TRUE and
		cat.boardid = i_boardid and
		(i_categoryid is null or cat.categoryid=i_categoryid) and
		-- topic is not deleted 
		c.isdeleted IS FALSE
	order by
		c.lastposted desc,
		d.name asc,
		c.priority desc			
LOOP
-- RETURN NEXT _rec;
ici_retcount := ici_retcount +1; 
IF (ici_retcount between  ici_firstselectrownum and ici_firstselectrownum+i_pagesize) THEN
RETURN NEXT _rec;
ici_counter := ici_counter + 1;
END IF;
IF (ici_counter >= i_pagesize) THEN
EXIT;
END IF;
END LOOP; 		
END;
$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100
  ROWS 1000; 
--GO

-- Stored procedures for Buddy feature 
CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}buddy_addrequest(
						   i_fromuserid integer,
						   i_touserid integer,
			   i_usedisplayname boolean,
			   i_utctimestamp timestamp)
				  RETURNS  {databaseSchema}.{objectQualifier}buddy_addrequest_rt AS
$BODY$
DECLARE 
					   i_approved boolean;
					   i_paramoutput varchar(255);
					   _rec {databaseSchema}.{objectQualifier}buddy_addrequest_rt%ROWTYPE;
BEGIN 
		IF NOT EXISTS ( SELECT  id
						FROM    {databaseSchema}.{objectQualifier}buddy
						WHERE    fromuserid = i_fromuserid
								  AND touserid = i_touserid                         
								 LIMIT 1)  THEN          
				IF ( NOT EXISTS ( SELECT    id
								  FROM      {databaseSchema}.{objectQualifier}buddy
								  WHERE      fromuserid = i_touserid
											  AND touserid = i_fromuserid LIMIT 1
											)
				   ) THEN
					
						INSERT  INTO {databaseSchema}.{objectQualifier}buddy
								(
								  fromuserid,
								  touserid,
								  approved,
								  requested
								)
						VALUES  (
								  i_fromuserid,
								  i_touserid,
								  false,
								  i_utctimestamp
								);
						SELECT (case when i_usedisplayname then displayname else name end), false INTO i_paramoutput, i_approved 
								 FROM   {databaseSchema}.{objectQualifier}user
											 WHERE  ( userid = i_touserid ) LIMIT 1;
										   
					  
					
					 ELSE                     
						INSERT  INTO {databaseSchema}.{objectQualifier}buddy
								(
								  fromuserid,
								  touserid,
								  approved,
								  requested
								)
						VALUES  (
								  i_fromuserid,
								  i_touserid,
								  true,
								  i_utctimestamp
								);
						UPDATE  {databaseSchema}.{objectQualifier}buddy
						SET     approved = true
						WHERE   fromuserid = i_touserid
								AND touserid = i_fromuserid;
								  
						SELECT   (case when i_usedisplayname then displayname else name end), 
								true 
						INTO    i_paramoutput, 
								i_approved
						FROM   {databaseSchema}.{objectQualifier}user
						WHERE   userid = i_touserid;                        
					
			END	IF;
		ELSE             
				SELECT 
				'',
				false 
				INTO i_paramoutput, 
				i_approved;
				
			END IF;
			 SELECT i_approved, i_paramoutput INTO _rec; 
		  RETURN _rec;
	END;
$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER;
--GO

-- Stored procedures for Buddy feature 
CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}buddy_approverequest(
						   i_fromuserid integer,
						   i_touserid integer,
						   i_mutual boolean,
						   i_usedisplayname boolean,
						   i_utctimestamp timestamp)
				  RETURNS varchar(255) AS
$BODY$
 DECLARE 
						i_paramoutput varchar(255);
	BEGIN
		IF EXISTS ( SELECT  1
					FROM    {databaseSchema}.{objectQualifier}buddy
					WHERE   ( fromuserid = i_fromuserid
							  AND touserid = i_touserid
							) ) THEN 
			
				UPDATE    {databaseSchema}.{objectQualifier}buddy
				SET     approved = true
				WHERE   ( fromuserid = i_fromuserid
						  AND touserid = i_touserid
						);
				 SELECT  (case when i_usedisplayname then displayname else name end) INTO i_paramoutput
									 FROM   {databaseSchema}.{objectQualifier}user
									 WHERE  ( userid = i_fromuserid );
				IF  i_mutual IS TRUE 
					AND ( NOT EXISTS ( SELECT   id
									   FROM       {databaseSchema}.{objectQualifier}buddy
									   WHERE    fromuserid = i_touserid
												AND touserid = i_fromuserid )
						) THEN
					INSERT  INTO   {databaseSchema}.{objectQualifier}buddy
							(
							  fromuserid,
							  touserid,
							  approved,
							  requested
							)
					VALUES  (
							  i_touserid,
							  i_fromuserid,
							  true,
							  i_utctimestamp
							);
			END IF;
			END IF;
			RETURN i_paramoutput;
END;
$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER;
--GO

-- Stored procedures for Buddy feature
CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}buddy_list(
						   i_fromuserid integer)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}buddy_list_return_type AS
$BODY$DECLARE
			 _rec {databaseSchema}.{objectQualifier}buddy_list_return_type%ROWTYPE;
BEGIN
	FOR _rec IN
		 SELECT * FROM (SELECT a.userid,
				a.boardid,
				a.name,
				a.displayname,
				a.joined,
				a.numposts,
				b.name AS RankName,
				c.approved,
				c.fromuserid,
				c.requested
		FROM   {databaseSchema}.{objectQualifier}user a
				JOIN {databaseSchema}.{objectQualifier}rank b ON b.rankid = a.rankid
				JOIN   {databaseSchema}.{objectQualifier}buddy c 
				ON  c.touserid = a.userid
				WHERE  c.fromuserid = i_fromuserid
		UNION
		SELECT  i_fromuserid AS UserID,
				a.boardid,
				a.name,
				a.displayname,
				a.joined,
				a.numposts,
				b.name as RankName,
				c.approved,
				c.fromuserid,
				c.requested
		FROM    {databaseSchema}.{objectQualifier}user a
				JOIN {databaseSchema}.{objectQualifier}rank b ON b.rankid = a.rankid
				JOIN   {databaseSchema}.{objectQualifier}buddy c 
				ON a.userid = c.fromuserid WHERE c.approved IS FALSE  AND  c.touserid = i_fromuserid ) AS x
		ORDER BY x.name
		LOOP
		RETURN NEXT _rec;
		END LOOP;
  END;$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 30
  ROWS 1000;
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}buddy_remove(
						   i_fromuserid integer,
						   i_touserid integer, 
						   i_usedisplayname boolean)
				  RETURNS varchar(128) AS
$BODY$
	BEGIN
		DELETE  FROM   {databaseSchema}.{objectQualifier}buddy
		WHERE   fromuserid = i_fromuserid
				  AND touserid = i_touserid;
	  RETURN  (SELECT  (case when i_usedisplayname then displayname else name end)  
							 FROM   {databaseSchema}.{objectQualifier}user
							 WHERE  userid = i_touserid );
						   
END;
$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER;
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}buddy_denyrequest(
						   i_fromuserid integer,
						   i_touserid integer,
						   i_usedisplayname boolean)                           
				  RETURNS varchar(255) AS
$BODY$
	 BEGIN
		DELETE  FROM   {databaseSchema}.{objectQualifier}buddy
		WHERE   fromuserid = i_fromuserid
				AND touserid = i_touserid;
	RETURN   (SELECT  (case when i_usedisplayname then displayname else name end)                              FROM   {databaseSchema}.{objectQualifier}user
							 WHERE   userid = i_fromuserid );
END;
$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER;
--GO  

-- Albums 
CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}album_save(
						   i_albumid integer,
						   i_userid integer,
						   i_title varchar(255),
						   i_coverimageid integer,
						   i_utctimestamp timestamp)
				  RETURNS integer AS
$BODY$ 
 DECLARE ici_albuminserted integer; 
	BEGIN
	   
		-- Update Cover?
		IF ( i_coverimageid IS NOT NULL AND i_coverimageid > 0) THEN
			UPDATE  {databaseSchema}.{objectQualifier}useralbum
			SET     coverimageid = i_coverimageid
			WHERE    i_albumid IS NOT NULL AND albumid = i_albumid;
		ELSE 
			-- Remove Cover?
			IF ( i_coverimageid  = 0 )  THEN
				UPDATE  {databaseSchema}.{objectQualifier}useralbum
				SET     coverimageid = NULL
				WHERE  i_albumid IS NOT NULL AND albumid = i_albumid;            
			ELSE 
			-- Update Title?
				IF i_albumid is not null THEN
					ici_albuminserted := i_albumid;
					UPDATE  {databaseSchema}.{objectQualifier}useralbum
					SET     title = i_title
					WHERE   i_albumid IS NOT NULL AND  albumid = i_albumid;
				ELSE                    
					-- New album. insert into table.
						INSERT  INTO {databaseSchema}.{objectQualifier}useralbum
								(
								  userid,
								  title,
								  coverimageid,
								  updated
								)
						VALUES  (
								  i_userid,
								  i_title,
								  i_coverimageid,
								  i_utctimestamp
								) RETURNING albumid INTO ici_albuminserted;                             
				END IF;
	   END IF;
	END IF;
			  RETURN COALESCE(ici_albuminserted, i_albumid);       
END;
$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER;
--GO 

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}album_list(
						   i_userid integer,
						   i_albumid integer)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}album_list_return_type AS
$BODY$
DECLARE _rec {databaseSchema}.{objectQualifier}album_list_return_type%ROWTYPE;
	BEGIN
		IF i_userid IS NOT NULL THEN
		  FOR _rec IN   
		  SELECT  albumid,
				  userid,
				  title,
				  coverimageid,
				  updated
			FROM    {databaseSchema}.{objectQualifier}useralbum
			WHERE   userid = i_userid
			ORDER BY updated DESC LIMIT 10000
			LOOP
			RETURN NEXT _rec;
			END LOOP;
		ELSE 
		   FOR _rec IN  SELECT  
				  albumid,
				  userid,
				  title,
				  coverimageid,
				  updated
			FROM    {databaseSchema}.{objectQualifier}useralbum
			WHERE   albumid = i_albumid   LIMIT 10000      
			LOOP
			RETURN NEXT _rec;             
			END LOOP;	
		END IF; 		     
END;
$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER COST 100 ROWS 1000;
--GO 

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}album_delete(
						   i_albumid integer)
				  RETURNS void AS
$BODY$
BEGIN
		DELETE  FROM {databaseSchema}.{objectQualifier}useralbumimage
		WHERE   albumid = i_albumid;
		DELETE  FROM {databaseSchema}.{objectQualifier}useralbum
		WHERE   albumid = i_albumid;       
END;
$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER;
--GO  


CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}album_gettitle(
						   i_albumid integer)
				  RETURNS varchar(255) AS
$BODY$DECLARE
			 ittl varchar(255);
BEGIN
SELECT title into ittl
		  FROM   {databaseSchema}.{objectQualifier}useralbum
		  WHERE  albumid = i_albumid limit 1;
		  return ittl;  
		  return coalesce(ittl,'');
									
END;
$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER;
--GO  

DROP FUNCTION IF EXISTS {databaseSchema}.{objectQualifier}album_getstats(integer,  integer);
--GO
CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}album_getstats(
						   i_userid integer, 
						   i_albumid integer)
				  RETURNS {databaseSchema}.{objectQualifier}album_getstats_return_type AS
$BODY$DECLARE
			 _rec {databaseSchema}.{objectQualifier}album_getstats_return_type%ROWTYPE;
			 ici_albumid integer :=i_albumid;
			 ici_isin boolean;
BEGIN  	

 IF ici_albumid is null THEN  ici_albumid := 0; END IF;
		IF ici_albumid is not null THEN      
			   SELECT 0, COALESCE(COUNT(imageid), 0)  INTO _rec
								 FROM   {databaseSchema}.{objectQualifier}useralbumimage
								 WHERE  albumid = ici_albumid AND imageid is not null;
																
		ELSE            
				/* SELECT COUNT(ua.albumid),COUNT(uai.imageid) INTO _rec.i_albumnumber,_rec.i_imagenumber
									 FROM   {databaseSchema}.{objectQualifier}useralbum ua
									 JOIN {databaseSchema}.{objectQualifier}useralbumimage uai
									 ON ua.albumid = uai.albumid 
									 WHERE  ua.userid = i_userid; */
				  
			   SELECT COALESCE(COUNT(ua.albumid), 0)  INTO _rec."AlbumNumber"
									 FROM   {databaseSchema}.{objectQualifier}useralbum ua                                    
									 WHERE ua.userid = i_userid AND i_userid is not null and ua.albumid is not null;                            
			   SELECT COALESCE(COUNT(uai.imageid), 0) INTO _rec."ImageNumber" 
									 FROM   {databaseSchema}.{objectQualifier}useralbumimage uai
									 WHERE  uai.albumid in (
											SELECT  ua.albumid
											FROM    {databaseSchema}.{objectQualifier}useralbum ua
										   WHERE  ua.userid = i_userid);                                         
																		
				
		 END IF;
	IF  _rec."AlbumNumber" IS NULL THEN
	-- IF NOT FOUND THEN
	_rec."AlbumNumber" = 0;
	END IF;	
	IF  _rec."ImageNumber" IS NULL THEN
	_rec."ImageNumber" = 0;			
	END IF;	
	RETURN _rec;
	
END;
$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER;
--GO 

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}album_image_save(
						   i_imageid integer,
						   i_albumid integer,
						   i_caption varchar(255),
						   i_filename varchar(255),
						   i_bytes integer,
						   i_contenttype varchar(50),
						   i_utctimestamp timestamp)
						   RETURNS void AS
$BODY$
BEGIN  
		IF i_imageid is not null and i_imageid > 0 THEN
			UPDATE  {databaseSchema}.{objectQualifier}useralbumimage
			SET     caption = COALESCE(i_caption,'')
			WHERE   imageid = i_imageid;
		ELSE
			INSERT  INTO {databaseSchema}.{objectQualifier}useralbumimage
					(
					  albumid,
					  caption, 
					  filename,
					  bytes,
					  contenttype,
					  uploaded,
					  downloads
					)
			VALUES  (
					  i_albumid,
					  COALESCE(i_caption,''),
					  i_filename,
					  i_bytes,
					  i_contenttype,
					  i_utctimestamp,
					  0
					);
		END IF;
END;
$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER;
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}album_image_list(
						   i_albumid integer,
						   i_imageid integer)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}album_image_list_return_type AS
$BODY$
DECLARE 
_rec {databaseSchema}.{objectQualifier}album_image_list_return_type%ROWTYPE;
_icount integer :=0;
	BEGIN      
		IF i_albumid IS NOT NULL AND i_albumid > 0 THEN
		FOR _rec IN
			SELECT  a.imageid as "ImageID", 
					a.albumid as "AlbumID", 
					a.caption as "Caption", 
					a.filename as "FileName",
					a.bytes as "Bytes", 
					a.contenttype as "ContentType", 
					a.uploaded as "Uploaded", 
					a.downloads as "Downloads",
					0 as "UserID"            
			FROM    {databaseSchema}.{objectQualifier}useralbumimage a                         
			WHERE   a.albumid = i_albumid
			ORDER BY a.uploaded DESC
			LOOP
			RETURN NEXT _rec; 
			END LOOP;
		ELSE 
		FOR _rec IN
			SELECT  a.imageid as "ImageID", 
					a.albumid as "AlbumID", 
					COALESCE(a.caption,'') as "Caption", 
					a.filename as "FileName",
					a.bytes as "Bytes", 
					a.contenttype as "ContentType", 
					a.uploaded as "Uploaded", 
					a.downloads as "Downloads", 
					b.userid as "UserID"
			FROM    {databaseSchema}.{objectQualifier}useralbumimage a
					INNER JOIN {databaseSchema}.{objectQualifier}useralbum b ON b.albumid = a.albumid
			WHERE   imageid = i_imageid
			LOOP
			RETURN NEXT _rec;
			END LOOP;
		END IF;
		IF NOT FOUND THEN
		return next _rec;
		END IF;
END;
$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER COST 100 ROWS 1000;
--GO  

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}album_images_byuser(
						   i_userid integer)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}album_image_list_return_type AS
$BODY$
DECLARE 
_rec {databaseSchema}.{objectQualifier}album_image_list_return_type%ROWTYPE;
_icount integer :=0;
	BEGIN
			FOR _rec IN
			SELECT  a.imageid as "ImageID", 
					a.albumid as "AlbumID", 
					COALESCE(a.caption,'') as "Caption", 
					a.filename as "FileName",
					a.bytes as "Bytes", 
					a.contenttype as "ContentType", 
					a.uploaded as "Uploaded", 
					a.downloads as "Downloads", 
					b.userid as "UserID"
			FROM    {databaseSchema}.{objectQualifier}useralbumimage a
					INNER JOIN {databaseSchema}.{objectQualifier}useralbum b ON b.albumid = a.albumid
			WHERE   userid = i_userid
			LOOP
			RETURN NEXT _rec;
			END LOOP;
	  
		IF NOT FOUND THEN
		return next _rec;
		END IF;
END;
$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER COST 100 ROWS 1000;
--GO  

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}album_image_delete(
						   i_imageid integer)
				  RETURNS void AS
$BODY$
	BEGIN
		DELETE  FROM {databaseSchema}.{objectQualifier}useralbumimage
		WHERE   imageid = i_imageid;
		UPDATE  {databaseSchema}.{objectQualifier}useralbum
		SET     coverimageid = NULL
		WHERE   coverimageid = i_imageid;
		UPDATE  {databaseSchema}.{objectQualifier}useralbum
		SET     coverimageid = NULL
		WHERE   coverimageid = i_imageid;
	  END;
$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER;
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}album_image_download(
						   i_imageid integer)
				  RETURNS void AS
$BODY$
BEGIN
		UPDATE  {databaseSchema}.{objectQualifier}useralbumimage
		SET     downloads = COALESCE(downloads,0) + 1
		WHERE   imageid = i_imageid;
END;
$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER;
--GO  

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}user_getsignaturedata(
						   i_boardid integer, 
						   i_userid integer)
				  RETURNS {databaseSchema}.{objectQualifier}user_getsignaturedata_return_type  AS
$BODY$DECLARE 
			 R_UsrSigChars int:=0;
			 R_UsrSigBBCodes varchar(4000);
			 R_UsrSigHTMLTags varchar(4000);
			 G_UsrSigChars int:=0;
			 G_UsrSigBBCodes varchar(4000);
			 G_UsrSigHTMLTags varchar(4000);
			 TG_UsrSigChars integer;
			 TG_UsrSigBBCodes varchar(4000);
			 TG_UsrSigHTMLTags varchar(4000);
			 _rec {databaseSchema}.{objectQualifier}user_getsignaturedata_return_type%ROWTYPE;
			 _rec1 {databaseSchema}.{objectQualifier}user_getsignaturedata_return_type%ROWTYPE;  
 BEGIN
	
	-- first find rank settings
	SELECT COALESCE(c.usrsigchars,0), COALESCE(c.usrsigbbcodes,''), COALESCE(c.usrsightmltags,'')
	INTO  R_UsrSigChars,  R_UsrSigBBCodes ,  R_UsrSigHTMLTags   
	FROM {databaseSchema}.{objectQualifier}rank c 
								JOIN {databaseSchema}.{objectQualifier}user d
								  ON c.rankid = d.rankid 
								  WHERE d.userid = i_userid 
								  AND c.boardid = i_boardid 
								  ORDER BY c.RankID DESC LIMIT 1;

	-- loop thru all groups and add permitted 
	FOR _rec1 IN 
	SELECT  
	COALESCE(c.usrsigchars,0), 
	COALESCE(c.usrsigbbcodes,''), 
	COALESCE(c.usrsightmltags,'')    
	FROM {databaseSchema}.{objectQualifier}user a 
						JOIN {databaseSchema}.{objectQualifier}usergroup b
						  ON a.userid = b.userid
							JOIN {databaseSchema}.{objectQualifier}group c                         
							  ON b.groupid = c.groupid 
							  WHERE a.userid = i_userid AND c.boardid = i_boardid
							  LOOP  
												
		G_UsrSigChars := 
		(CASE WHEN R_UsrSigChars > _rec1."UsrSigChars"
		THEN R_UsrSigChars ELSE _rec1."UsrSigChars" END); 

		G_UsrSigBBCodes := 
		(COALESCE(_rec1."UsrSigBBCodes" || ',','') 
		|| COALESCE(G_UsrSigBBCodes,'')) ;

		G_UsrSigHTMLTags := (COALESCE(_rec1."UsrSigHTMLTags" || ',','') 
		|| COALESCE(G_UsrSigHTMLTags, '')) ;	
		
		END LOOP;
   

								  
	   SELECT 
		G_UsrSigChars AS UsrSigChars,
		G_UsrSigBBCodes AS UsrSigBBCodes, 
		G_UsrSigHTMLTags  AS UsrSigHTMLTags
		into _rec1;
		return _rec1;
		END;
$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER;
--GO
 
 CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}user_getalbumsdata(
							i_boardid integer, 
							i_userid integer)
				   RETURNS {databaseSchema}.{objectQualifier}user_getalbumsdata_return_type  AS
$BODY$DECLARE
			 ici_cnt integer;
			 OR_UsrAlbums integer;
			 OR_UsrAlbumImages integer;
			 OG_UsrAlbums integer;
			 OG_UsrAlbumImages integer;
			 _rec {databaseSchema}.{objectQualifier}user_getalbumsdata_return_type%ROWTYPE ;           
BEGIN   
  
	SELECT  COALESCE(c.usralbums,0), COALESCE(c.usralbumimages,0) 
	INTO  OG_UsrAlbums, OG_UsrAlbumImages 
	FROM {databaseSchema}.{objectQualifier}user a 
						JOIN {databaseSchema}.{objectQualifier}usergroup b
						  ON b.userid = a.userid
							JOIN {databaseSchema}.{objectQualifier}group c                         
							  ON c.groupid = b.groupid 
							  WHERE a.userid = i_userid 
								AND c.boardid = i_boardid 
								  ORDER BY c.sortorder ASC LIMIT 1;
	 
	 SELECT  COALESCE(c.usralbums,0), COALESCE(c.usralbumimages,0)  
	 INTO  OR_UsrAlbums, OR_UsrAlbumImages 
	 FROM {databaseSchema}.{objectQualifier}rank c 
								JOIN {databaseSchema}.{objectQualifier}user d
								  ON c.rankid = d.rankid 
									WHERE d.userid = i_userid 
									 AND c.boardid = i_boardid LIMIT 1;      
	   
					 
	  
	  SELECT
	   (SELECT COALESCE(COUNT(ua.albumid), 0) FROM {databaseSchema}.{objectQualifier}useralbum ua
	   WHERE ua.userID = i_userid) AS NumAlbums,
	   (SELECT COALESCE(COUNT(uai.imageid),0) FROM  {databaseSchema}.{objectQualifier}useralbumimage uai
	   INNER JOIN {databaseSchema}.{objectQualifier}useralbum ua
	   ON ua.albumid = uai.albumid
	   WHERE ua.userID = i_userid) AS NumImages, 
	   (CASE WHEN OR_UsrAlbums < OG_UsrAlbums THEN OG_UsrAlbums ELSE OR_UsrAlbums END) AS UsrAlbums, 
	   (CASE WHEN OR_UsrAlbumImages < OG_UsrAlbumImages THEN OG_UsrAlbumImages ELSE OR_UsrAlbumImages END) AS UsrAlbumImages 
	   INTO  _rec;
	   RETURN _rec;    
	
END;
$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER;
--GO   

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}messagehistory_list(
						   i_messageid integer, 
						   i_daystoclean integer,
						   i_utctimestamp timestamp)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}messagehistory_list_return_type  AS
$BODY$DECLARE
			 _rec {databaseSchema}.{objectQualifier}messagehistory_list_return_type%ROWTYPE;
BEGIN  	  
	  -- delete all message variants older THEN DaysToClean days Flags reserved for possible pms   
	 delete from {databaseSchema}.{objectQualifier}messagehistory
	 where date_part('day', i_utctimestamp) - date_part('day', edited)  > i_daystoclean;     
				
	 FOR _rec IN SELECT mh.messageid,
						mh.message,
						mh.ip,
						mh.edited,
						mh.editreason,
						mh.ismoderatorchanged,
						mh.flags,
						m.editedby, 
						m.userid, 
						m.username, 
						COALESCE(m.userdisplayname, u.name) as Name, 
						t.forumid, 
						t.topicid, 
						t.topic,						
						m.posted
	 FROM {databaseSchema}.{objectQualifier}messagehistory mh
	 LEFT JOIN {databaseSchema}.{objectQualifier}message m 
	 ON m.messageid = mh.messageid
	 LEFT JOIN {databaseSchema}.{objectQualifier}topic t 
	 ON t.topicid = m.topicid
	 LEFT JOIN {databaseSchema}.{objectQualifier}user u 
	 ON u.userid = t.userid
	 WHERE mh.messageid = i_messageid 
	 ORDER by mh.edited, mh.messageid
		 LOOP
			RETURN NEXT _rec;
		 END LOOP;   
END;
$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER;
--GO 

CREATE OR REPLACE FUNCTION  {databaseSchema}.{objectQualifier}user_lazydata(
							i_userid  integer,
							i_boardid integer,
							i_showpendingmails boolean,
							i_showpendingbuddies boolean,
							i_showunreadpms boolean,
							i_showuseralbums boolean,
							i_showuserstyle boolean) 
				  RETURNS SETOF {databaseSchema}.{objectQualifier}user_lazydata_return_type AS
$BODY$DECLARE
			 G_UsrAlbums integer :=0;
			 R_UsrAlbums integer :=0;         
			 ici_grouppersonalforums integer :=0;			
			 ici_grouppersonalmasks integer :=0;
			 ici_grouppersonalgroups integer :=0;
			 _rec {databaseSchema}.{objectQualifier}user_lazydata_return_type%ROWTYPE;
			 _recgr RECORD;
BEGIN  
	SELECT  COALESCE(MAX(c.usrpersonalgroups),0),COALESCE(MAX(c.usrpersonalmasks),0),COALESCE(MAX(c.usrpersonalforums),0)
	INTO  ici_grouppersonalgroups,ici_grouppersonalmasks,ici_grouppersonalforums
	FROM {databaseSchema}.{objectQualifier}user a 
						JOIN {databaseSchema}.{objectQualifier}usergroup b
						  ON a.userid = b.userid
							JOIN {databaseSchema}.{objectQualifier}group c                         
							  ON c.groupid = b.groupid
							  WHERE a.userid = i_userid AND a.boardid = i_boardid 
							  LIMIT 1;

	IF (i_showuseralbums is true) THEN
	SELECT COALESCE(MAX(c.usralbums),0)
	INTO G_UsrAlbums
	FROM {databaseSchema}.{objectQualifier}user a 
						JOIN {databaseSchema}.{objectQualifier}usergroup b
						  ON a.userid = b.userid
							JOIN {databaseSchema}.{objectQualifier}group c                         
							  ON c.groupid = b.groupid
							  WHERE a.userid = i_userid AND a.boardid = i_boardid;
	SELECT COALESCE(MAX(c.usralbums),0)
	INTO  R_UsrAlbums
	FROM {databaseSchema}.{objectQualifier}Rank c 
								JOIN {databaseSchema}.{objectQualifier}user d
								  ON c.rankid = d.rankid WHERE d.userid = i_userid 
								  AND d.boardid = i_boardid;
	END IF;                                                              
   
	-- return information
	FOR _rec in SELECT	
		a.provideruserkey,
		a.flags AS UserFlags,
		a.name AS UserName,		
		a.displayname AS DisplayName,
		a.suspended AS Suspended,
		a.themefile AS ThemeFile,
		a.languagefile AS LanguageFile,
		a.usesinglesignon,
		((a.flags & 64) = 64) as IsDirty,
		a.texteditor,
		a.timezone AS TimeZoneUser,
		a.culture AS CultureUser,
		a.isfacebookuser,	
		a.istwitteruser,	
		((flags & 4) = 4) AS IsGuest,
		(CASE WHEN i_showpendingmails  is true THEN (select count(1) from {databaseSchema}.{objectQualifier}mail WHERE tousername = a.name) ELSE 0 END) as MailsPending,
		(CASE WHEN i_showunreadpms  is true THEN (select count(1) from {databaseSchema}.{objectQualifier}userpmessage where userid=i_userid and isread is false and isdeleted  is false and isarchived  is false) ELSE 0 END) as UnreadPrivate,
		(CASE WHEN i_showunreadpms  is true THEN (SELECT created FROM {databaseSchema}.{objectQualifier}pmessage pm INNER JOIN {databaseSchema}.{objectQualifier}userpmessage upm ON pm.pmessageid = upm.pmessageid WHERE upm.userid=i_userid and upm.isread is false  and upm.isdeleted  is false and upm.isarchived  is false ORDER BY pm.created DESC LIMIT 1) ELSE NULL END) as LastUnreadPm,		
		(CASE WHEN i_showpendingbuddies  is true THEN (SELECT COUNT(1) FROM {databaseSchema}.{objectQualifier}buddy WHERE touserid = i_userid AND approved is false) ELSE 0 END) as PendingBuddies,
		(CASE WHEN i_showpendingbuddies  is true THEN (SELECT Requested FROM {databaseSchema}.{objectQualifier}buddy WHERE touserID=i_userid and approved is false ORDER BY requested LIMIT 1) ELSE NULL END) as LastPendingBuddies,       
		(case(i_showuserstyle)
			when true THEN  coalesce((select userstyle from {databaseSchema}.{objectQualifier}user where userid = i_userid),'')  
			else ''	 end) as UserStyle,
	   (SELECT COUNT(ua.albumid) FROM {databaseSchema}.{objectQualifier}useralbum ua
	   WHERE ua.userid = i_userid) as  NumAlbums,
	   (CASE WHEN i_showuseralbums  is true THEN(CASE WHEN G_UsrAlbums > R_UsrAlbums THEN G_UsrAlbums ELSE R_UsrAlbums END) else 0 end) as UsrAlbums,
	   (CASE WHEN i_showpendingbuddies is true THEN 
	   SIGN(coalesce((SELECT 1 FROM {databaseSchema}.{objectQualifier}buddy 
	   WHERE fromuserid = i_userid OR touserid = i_userid LIMIT 1),0))::integer::boolean ELSE false END) AS UserHasBuddies,
		   -- Guest can't vote in polls attached to boards, we need some temporary access check by a criteria 
		(CASE WHEN a.Flags & 4 > 0 THEN 0 ELSE 1 END) AS BoardVoteAccess,
		a.points,
		(SELECT COUNT(1) FROM {databaseSchema}.{objectQualifier}forum f   WHERE f.createdbyuserid = i_userid and f.isuserforum),
		(SELECT COUNT(1) FROM {databaseSchema}.{objectQualifier}accessmask am   WHERE am.createdbyuserid = i_userid and am.isusermask),
		(SELECT COUNT(1) FROM {databaseSchema}.{objectQualifier}group gr   WHERE gr.createdbyuserid = i_userid and gr.isusergroup),
		ici_grouppersonalgroups,
		ici_grouppersonalmasks,
		ici_grouppersonalforums,		
		a.commonviewtype,
		a.topicsperpage,
		a.postsperpage
		from
		   {databaseSchema}.{objectQualifier}user a		
		where
		a.userid = i_userid LIMIT 1
		loop
		   return next _rec;
		end loop; 
END;$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100
  ROWS 1000;  
 --GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}message_gettextbyids(
						   i_messageids text)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}message_gettextbyids_return_type AS
$BODY$DECLARE 
			 ici_messageid varchar(11);
			 ici_messageids text := TRIM(BOTH FROM i_messageids) || ',';
			 ici_pos integer:= POSITION(',' IN ici_messageids);
			 ici_messagearray int array;
			 ici_msgcntr int := 0;
			 _rec {databaseSchema}.{objectQualifier}message_gettextbyids_return_type%ROWTYPE;
BEGIN 
-- ici_messageids := TRIM(BOTH FROM i_messageids) || ',';
-- ici_pos := POSITION(',' IN ici_messageids);
	
IF REPLACE(ici_messageids, ',', '') <> '' THEN

WHILE ici_pos > 0 
	   LOOP		 
			-- left function replaced by substring 
			ici_messageid := TRIM(BOTH FROM (SUBSTRING(ici_messageids FOR ici_pos - 1)));
			IF ici_messageid <> '' THEN	
				ici_messagearray[ici_msgcntr] = (CAST(ici_messageid AS integer));				
				--Use Appropriate conversion
			END IF;
			-- right implementation here we remove first id from string
			ici_messageids := SUBSTRING(ici_messageids from ici_pos+1 for (char_length(ici_messageids) - ici_pos));
			ici_pos := POSITION(',' IN ici_messageids);			
	   END LOOP;
							-- to be sure that last value is inserted
IF (LENGTH(ici_messageids)>0) THEN
ici_messagearray[ici_msgcntr] = (CAST(ici_messageids AS integer));                          
						   END IF;					   
									   

END	IF;	
   
	FOR _rec IN	SELECT d.messageid, d.message
				FROM  {databaseSchema}.{objectQualifier}message d 
				WHERE d.messageid IN  (SELECT * FROM unnest(ici_messagearray))		
	LOOP
	RETURN NEXT _rec;    
	END LOOP;
END;
$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER COST 100 ROWS 1000;
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}recent_users(
						   i_boardid integer,
						   i_timesincelastlogin integer,
						   i_stylednicks boolean,
						   i_utctimestamp timestamp) 
				  RETURNS SETOF {databaseSchema}.{objectQualifier}recent_users_rt AS
$BODY$
DECLARE  
_rec {databaseSchema}.{objectQualifier}recent_users_rt%ROWTYPE;
BEGIN
	FOR _rec IN SELECT u.userid,
	u.Name as UserName,
	u.DisplayName as UserDisplayName,
	u.lastvisit,
	0 AS IsCrawler,	
	1 AS UserCount,
	-- IsActiveExcluded 
	CASE ((u.flags & 16) = 16) WHEN TRUE THEN 1 ELSE 0 END AS IsHidden,
	(CASE(i_stylednicks)
				WHEN TRUE THEN
						 u.userstyle  ELSE '' END) AS Style            
	FROM {databaseSchema}.{objectQualifier}user AS u               
	WHERE u.isapproved IS TRUE AND
	 u.boardid = i_boardid AND
	 u.lastvisit > (i_utctimestamp - (i_timesincelastlogin::varchar(11) || ' minute')::interval)
	 AND
				--Excluding guests
				NOT EXISTS(             
					SELECT 1 
						FROM {databaseSchema}.{objectQualifier}usergroup x
							inner join {databaseSchema}.{objectQualifier}group y ON y.groupid=x.groupid 
						WHERE x.userID=u.userid and (y.flags & 2)=2 LIMIT 1
					)
	ORDER BY u.lastvisit
	 LOOP
	RETURN NEXT _rec;    
	END LOOP;
	END;
$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER COST 100 ROWS 1000;
--GO

 CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}topics_byuser(
							i_boardid integer,
							i_categoryid integer,
							i_pageuserid integer,
							i_sincedate timestamp,
							i_todate timestamp,
							i_pageindex integer,
							i_pagesize integer,
							i_stylednicks boolean,
							i_findlastunread boolean,
							i_utctimestamp timestamp)
				   RETURNS SETOF {databaseSchema}.{objectQualifier}topics_byuser_return_type AS
 $BODY$DECLARE 
			  ici_topics_totalrowsnumber  integer;
			  ici_firstselectrownum integer;
			  ici_firstselectposted timestamp ;
			  ici_pageindex integer := i_pageindex;
			  ici_retcount integer := 0;
			  ici_counter integer := 0;
			  i_gettags boolean :=true;
			  _rec {databaseSchema}.{objectQualifier}topics_byuser_return_type%ROWTYPE;
BEGIN  
		-- find total returned count
		select
		COUNT(1) into ici_topics_totalrowsnumber
	from
		{databaseSchema}.{objectQualifier}topic c
		join {databaseSchema}.{objectQualifier}user b on b.UserID=c.UserID
		join {databaseSchema}.{objectQualifier}forum d on d.ForumID=c.ForumID
		join {databaseSchema}.{objectQualifier}activeaccess x on x.ForumID=d.ForumID
		join {databaseSchema}.{objectQualifier}category cat on cat.CategoryID=d.CategoryID
	where
		(c.lastposted between i_sincedate and i_todate) AND
		x.userid = i_pageuserid and
		x.readaccess IS TRUE and
		cat.boardid = i_boardid and
		(i_categoryid is null or cat.categoryid=i_categoryid) and
		c.isdeleted IS FALSE and
		c.topicmovedid is null
		and c.topicid IN 
		(SELECT mess.topicid FROM {databaseSchema}.{objectQualifier}message mess WHERE mess.userid=i_pageuserid AND mess.topicid=c.topicid LIMIT 1);
  
		 ici_pageindex := ici_pageindex+1;
		 ici_firstselectrownum := (ici_pageindex - 1) * i_pagesize + 1;

	SELECT 		
		c.lastposted
	into ici_firstselectposted
	from
		{databaseSchema}.{objectQualifier}topic c
		join {databaseSchema}.{objectQualifier}user b on b.UserID=c.UserID
		join {databaseSchema}.{objectQualifier}forum d on d.ForumID=c.ForumID
		join {databaseSchema}.{objectQualifier}activeaccess x on x.ForumID=d.ForumID
		join {databaseSchema}.{objectQualifier}category cat on cat.CategoryID=d.CategoryID
	where
		(c.lastposted between i_sincedate and i_todate) AND
		x.userid = i_pageuserid and
		x.readaccess IS TRUE and
		cat.boardid = i_boardid and
		(i_categoryid is null or cat.categoryid=i_categoryid) and
		c.isdeleted IS FALSE and
		c.topicmovedid is null
		and c.topicid IN 
		(SELECT mess.topicid FROM {databaseSchema}.{objectQualifier}message mess WHERE mess.userid=i_pageuserid AND mess.topicid=c.topicid LIMIT 1)
	ORDER BY
	c.lastposted DESC,
	cat.sortorder,
	d.sortorder,
	d.name DESC,
	c.priority DESC	;

	FOR _rec IN	select
		c.forumid,
		c.topicid,
		c.topicmovedid,
		c.posted,
		COALESCE(c.topicmovedid,c.topicid) AS LinkTopicID,
		c.topic as Subject,
		c.description AS Description,
		c.status,
		c.styles,
		c.userid,
		COALESCE(c.username,b.name) AS Starter,
		COALESCE(c.userdisplayname,b.displayname) AS StarterDisplay,
		(SELECT COUNT(1) FROM {databaseSchema}.{objectQualifier}message mes 
			 WHERE mes.topicid = c.topicid AND (mes.flags & 8) = 8  AND (mes.flags & 16) = 16 
			 AND ((i_pageuserid IS NOT NULL AND mes.userid = i_pageuserid) OR (i_pageuserid IS NULL))) 
			 AS NumPostsDeleted,
		(select count(1) -1 from {databaseSchema}.{objectQualifier}message x where x.topicid=c.topicid and (x.flags & 8)<> 8)  AS Replies,
		c.views,
		c.lastposted,
		c.lastuserid,
		COALESCE(c.lastusername,
		(select x.name from {databaseSchema}.{objectQualifier}user x where x.userid=c.lastuserid)) as LastUserName ,
		COALESCE(c.lastuserdisplayname,
		(select x.displayname from {databaseSchema}.{objectQualifier}user x where x.userid=c.lastuserid)) as LastUserName ,
		c.lastmessageid,
		c.lastmessageflags,
		c.topicid as LastTopicID, 
		c.flags as TopicFlags,
		(SELECT COUNT(ID) 		
		FROM {databaseSchema}.{objectQualifier}favoritetopic 
		WHERE topicid = COALESCE(c.topicmovedid,c.topicid)) as FavoriteCount,
		c.priority,
		c.pollid,
		d.name as ForumName,		
		d.flags as ForumFlags,
		(SELECT CAST(message as varchar(4000)) FROM {databaseSchema}.{objectQualifier}message mes2 where mes2.topicid = COALESCE(c.topicmovedid,c.topicid) AND mes2.position = 0 limit 1) as FirstMessage,
		(case(i_stylednicks)
			when true THEN   (SELECT us.userstyle FROM {databaseSchema}.{objectQualifier}user us where us.userid = c.userid)    
			else ''	 end) as StarterStyle,
		(case(i_stylednicks)
			when true THEN   (SELECT us.userstyle FROM {databaseSchema}.{objectQualifier}user us where us.userid = c.lastuserid)  
			else ''	 end) as LastUserStyle,
		(case(i_findlastunread)
			 when true THEN
			   (SELECT lastaccessdate FROM {databaseSchema}.{objectQualifier}ForumReadTracking x WHERE x.ForumID=d.ForumID AND x.UserID = i_pageuserid LIMIT 1 )
			 else i_utctimestamp end) as LastForumAccess,
		(case(i_findlastunread)
			 when true THEN
			   (SELECT lastaccessdate FROM {databaseSchema}.{objectQualifier}TopicReadTracking y WHERE y.TopicID=c.TopicID AND y.UserID = i_pageuserid LIMIT 1 )
			 else i_utctimestamp	 end) as LastTopicAccess,
		(case(i_gettags)
			 when true THEN
		(SELECT string_agg(tag, ',') FROM {databaseSchema}.{objectQualifier}tags tg join {databaseSchema}.{objectQualifier}topictags tt on tt.tagid = tg.tagid where 	  tt.topicid = c.topicid)
		 else '' end),	
		c.topicimage,
		c.topicimagetype,
		c.topicimagebin,
		0 as HasAttachments, 
		ici_topics_totalrowsnumber  AS TotalRows,
		ici_pageindex	AS 	PageIndex			 	 
	from
		{databaseSchema}.{objectQualifier}topic c
		join {databaseSchema}.{objectQualifier}user b on b.UserID=c.UserID
		join {databaseSchema}.{objectQualifier}forum d on d.ForumID=c.ForumID
		join {databaseSchema}.{objectQualifier}activeaccess x on x.ForumID=d.ForumID
		join {databaseSchema}.{objectQualifier}category cat on cat.CategoryID=d.CategoryID
	where
		c.lastposted <= ici_firstselectposted AND
		x.userid = i_pageuserid and
		x.readaccess IS TRUE and
		cat.boardid = i_boardid and
		(i_categoryid is null or cat.categoryid=i_categoryid) and
		c.isdeleted IS FALSE and
		c.topicmovedid is null
		and c.topicid IN 
		(SELECT mess.topicid FROM {databaseSchema}.{objectQualifier}message mess WHERE mess.userid=i_pageuserid AND mess.topicid=c.topicid LIMIT 1)
	ORDER BY
	c.lastposted DESC,
	cat.sortorder,
	d.sortorder,
	d.name DESC,
	c.priority DESC			
LOOP
-- RETURN NEXT _rec;
ici_retcount := ici_retcount +1; 
IF (ici_retcount between  ici_firstselectrownum and ici_firstselectrownum+i_pagesize) THEN
RETURN NEXT _rec;
ici_counter := ici_counter + 1;
END IF;
IF (ici_counter >= i_pagesize) THEN
EXIT;
END IF;
END LOOP; 		
END;
$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100
  ROWS 1000; 
--GO

 CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}topicstatus_delete(
						   i_topicstatusid integer)
				   RETURNS void AS
$BODY$
BEGIN 
   delete from {databaseSchema}.{objectQualifier}topicstatus 
	where topicstatusid = i_topicstatusid;
END;$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100;  
 --GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}topicstatus_edit(
						   i_topicstatusid integer) 
				  RETURNS SETOF {databaseSchema}.{objectQualifier}topicstatus_list_return_type AS
$BODY$DECLARE
			 _rec {databaseSchema}.{objectQualifier}topicstatus_list_return_type%ROWTYPE;
BEGIN
	FOR _rec IN SELECT  
				 topicstatusid,
				 topicstatusname,
				 boardid,
				 defaultdescription 
	FROM {databaseSchema}.{objectQualifier}topicstatus 
	WHERE 
		topicstatusid = i_topicstatusid
	LOOP
	RETURN NEXT _rec;
	END LOOP;
END;$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100 ROWS 1000;  
 --GO

 CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}topicstatus_list(
							i_boardid integer) 
				   RETURNS SETOF {databaseSchema}.{objectQualifier}topicstatus_list_return_type AS
$BODY$DECLARE
			 _rec {databaseSchema}.{objectQualifier}topicstatus_list_return_type%ROWTYPE;
BEGIN
	FOR _rec IN SELECT 
				 topicstatusid,
				 topicstatusname,
				 boardid,
				 defaultdescription
			FROM
				{databaseSchema}.{objectQualifier}topicstatus
			WHERE
				boardid = i_boardid	
			ORDER BY
				topicstatusid
LOOP
	RETURN NEXT _rec;
	END LOOP;
END;$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100 ROWS 1000;  
 --GO

 CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}topicstatus_save(
						   i_topicstatusid integer, 
						   i_boardid integer, 
						   i_topicstatusname varchar,
						   i_defaultdescription varchar)
				   RETURNS void AS
$BODY$
begin
		IF i_topicstatusid is null or i_topicstatusid = 0 THEN
		insert into {databaseSchema}.{objectQualifier}topicstatus (boardid,topicstatusname,defaultdescription) 
		values(i_boardid,i_topicstatusname,i_defaultdescription);
	
	else 
		update {databaseSchema}.{objectQualifier}topicstatus
		set topicstatusname = i_topicstatusname, 
			defaultdescription = i_defaultdescription
		where topicstatusid = i_topicstatusid;
	END IF;
END;$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100;  
 --GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}dbinfo_usertype_fields_info(
						   i_namespace varchar, 
						   i_typename varchar) 
				  RETURNS SETOF {databaseSchema}.{objectQualifier}dbinfo_usertype_fields_info_rt AS
$BODY$DECLARE
			 _rec {databaseSchema}.{objectQualifier}dbinfo_usertype_fields_info_rt%ROWTYPE;
BEGIN
	FOR _rec IN SELECT	
	a.attname,
	pg_catalog.format_type(a.atttypid, a.atttypmod),
	a.attnotnull,
	a.atthasdef,	
	nspacl
FROM pg_catalog.pg_class c
LEFT JOIN pg_catalog.pg_namespace n ON n.oid = c.relnamespace
LEFT JOIN pg_catalog.pg_attribute a ON a.attrelid = c.relfilenode
WHERE pg_catalog.pg_table_is_visible(c.oid)
AND c.relkind = 'c' and n.nspname =  i_namespace AND c.relname = i_typename
ORDER BY
n.nspname,
c.relname,
a.attnum
LOOP
	RETURN NEXT _rec;
	END LOOP;
END;$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100 ROWS 1000;  
 --GO


CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}user_update_ssn_status(
						   i_userid integer,
						   i_isfacebookuser boolean,
						   i_istwitteruser boolean)
				  RETURNS void AS
$BODY$
begin
		update {databaseSchema}.{objectQualifier}user
		set isfacebookuser = i_isfacebookuser,
			istwitteruser = i_istwitteruser
		where userid = i_userid;
	
END;$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100;  
 --GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}topic_unanswered(
						   i_boardid integer,
						   i_categoryid integer,
						   i_pageuserid integer,
						   i_sincedate timestamp,
						   i_todate timestamp,
						   i_pageindex integer,
						   i_pagesize integer,						   
						   i_stylednicks boolean,
						   i_findlastunread boolean,
						   i_utctimestamp timestamp)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}topic_unanswered_rt AS
$BODY$DECLARE
			 ici_topics_totalrowsnumber  integer;
			 ici_firstselectrownum integer;
			 ici_firstselectposted timestamp ;
			 ici_pageindex integer := i_pageindex;
			 ici_retcount integer := 0;
			 ici_counter integer := 0;
			 _rec {databaseSchema}.{objectQualifier}topic_unanswered_rt%ROWTYPE;
BEGIN  
		-- find total returned count
		select
		COUNT(1) into ici_topics_totalrowsnumber
	FROM
		{databaseSchema}.{objectQualifier}topic c
		JOIN {databaseSchema}.{objectQualifier}user b ON b.userid=c.userid
		JOIN {databaseSchema}.{objectQualifier}forum d ON d.forumid=c.forumid
		JOIN {databaseSchema}.{objectQualifier}activeaccess x ON x.forumid=d.forumid 
		JOIN {databaseSchema}.{objectQualifier}category cat ON cat.categoryid=d.categoryid
	WHERE
		(c.lastposted between i_sincedate and i_todate) AND
		 x.userid = i_pageuserid AND
		x.readaccess IS TRUE AND
	cat.boardid = i_boardid AND
	(i_categoryid IS NULL OR cat.categoryid=i_categoryid) AND
	c.isdeleted IS FALSE AND
	c.topicmovedid is null and
	c.numposts = 1;	 

		 ici_pageindex := ici_pageindex+1;
		 ici_firstselectrownum := (ici_pageindex - 1) * i_pagesize + 1;

	SELECT 		
		c.lastposted
	into ici_firstselectposted
	FROM
		{databaseSchema}.{objectQualifier}topic c
		JOIN {databaseSchema}.{objectQualifier}user b ON b.userid=c.userid
		JOIN {databaseSchema}.{objectQualifier}forum d ON d.forumid=c.forumid
		JOIN {databaseSchema}.{objectQualifier}activeaccess x ON x.forumid=d.forumid 
		JOIN {databaseSchema}.{objectQualifier}category cat ON cat.categoryid=d.categoryid
	WHERE
		(c.lastposted between i_sincedate and i_todate) AND
		 x.userid = i_pageuserid AND
		x.readaccess IS TRUE AND
	cat.boardid = i_boardid AND
	(i_categoryid IS NULL OR cat.categoryid=i_categoryid) AND
	c.isdeleted IS FALSE AND
	c.topicmovedid is null and
	c.numposts = 1	 
	ORDER BY
	c.lastposted DESC,
	cat.sortorder,
	d.sortorder,
	d.name DESC,
	c.priority DESC;

 FOR _rec IN
	SELECT 	
				c.forumid,
		c.topicid,
		c.topicmovedid,
		c.posted,
		COALESCE(c.topicmovedid,c.topicid) AS LinkTopicID,
		c.topic as Subject,
		c.description AS Description,
		c.status,
		c.styles,
		c.userid,
		COALESCE(c.username,b.name) AS Starter,
		COALESCE(c.userdisplayname,b.displayname) AS StarterDisplay,
		(SELECT COUNT(1) FROM {databaseSchema}.{objectQualifier}message mes 
			 WHERE mes.topicid = c.topicid AND (mes.flags & 8) = 8  AND (mes.flags & 16) = 16 
			 AND ((i_pageuserid IS NOT NULL AND mes.userid = i_pageuserid) OR (i_pageuserid IS NULL))) 
			 AS NumPostsDeleted,
		(select count(1) -1 from {databaseSchema}.{objectQualifier}message x where x.topicid=c.topicid and (x.flags & 8)<> 8)  AS Replies,
		c.views,
		c.lastposted,
		c.lastuserid,
		COALESCE(c.lastusername,
		(select x.name from {databaseSchema}.{objectQualifier}user x where x.userid=c.lastuserid)) as LastUserName ,
		COALESCE(c.lastuserdisplayname,
		(select x.displayname from {databaseSchema}.{objectQualifier}user x where x.userid=c.lastuserid)) as LastUserName ,
		c.lastmessageid,
		c.lastmessageflags,
		c.topicid as LastTopicID, 
		c.flags as TopicFlags,
		(SELECT COUNT(ID) 		
		FROM {databaseSchema}.{objectQualifier}favoritetopic 
		WHERE topicid = COALESCE(c.topicmovedid,c.topicid)) as FavoriteCount,
		c.priority,
		c.pollid,
		d.name as ForumName,		
		d.flags as ForumFlags,
		(SELECT CAST(message as varchar(4000)) FROM {databaseSchema}.{objectQualifier}message mes2 where mes2.topicid = COALESCE(c.topicmovedid,c.topicid) AND mes2.position = 0 limit 1) as FirstMessage,
		(case(i_stylednicks)
			when true THEN   (SELECT us.userstyle FROM {databaseSchema}.{objectQualifier}user us where us.userid = c.userid)    
			else ''	 end) as StarterStyle,
		(case(i_stylednicks)
			when true THEN   (SELECT us.userstyle FROM {databaseSchema}.{objectQualifier}user us where us.userid = c.lastuserid)  
			else ''	 end) as LastUserStyle,
		(case(i_findlastunread)
			 when true THEN
			   (SELECT lastaccessdate FROM {databaseSchema}.{objectQualifier}ForumReadTracking x WHERE x.ForumID=d.ForumID AND x.UserID = i_pageuserid LIMIT 1 )
			 else i_utctimestamp end) as LastForumAccess,
		(case(i_findlastunread)
			 when true THEN
			   (SELECT lastaccessdate FROM {databaseSchema}.{objectQualifier}TopicReadTracking y WHERE y.TopicID=c.TopicID AND y.UserID = i_pageuserid LIMIT 1 )
			 else i_utctimestamp	 end) as LastTopicAccess,
		(SELECT string_agg(tag, ',') FROM {databaseSchema}.{objectQualifier}tags tg join {databaseSchema}.{objectQualifier}topictags tt on tt.tagid = tg.tagid where 	  tt.topicid = c.topicid),	
		c.topicimage,
		c.topicimagetype,
		c.topicimagebin,
		0 as HasAttachments,	
		ici_topics_totalrowsnumber  AS TotalRows,
		ici_pageindex	AS 	PageIndex			 	 
	from
		{databaseSchema}.{objectQualifier}topic c
		join {databaseSchema}.{objectQualifier}user b on b.UserID=c.UserID
		join {databaseSchema}.{objectQualifier}forum d on d.ForumID=c.ForumID
		join {databaseSchema}.{objectQualifier}activeaccess x on x.ForumID=d.ForumID
		join {databaseSchema}.{objectQualifier}category cat on cat.CategoryID=d.CategoryID
	where
		c.lastposted <= ici_firstselectposted AND
		x.userid = i_pageuserid and
		x.readaccess IS TRUE and
		cat.boardid = i_boardid and
		(i_categoryid is null or cat.categoryid=i_categoryid) and
		c.isdeleted IS FALSE and
	c.topicmovedid is null and
	c.numposts = 1	
	ORDER BY
	c.lastposted DESC,
	cat.sortorder,
	d.sortorder,
	d.name DESC,
	c.priority DESC			
LOOP
-- RETURN NEXT _rec;
ici_retcount := ici_retcount +1; 
IF (ici_retcount between  ici_firstselectrownum and ici_firstselectrownum+i_pagesize) THEN
RETURN NEXT _rec;
ici_counter := ici_counter + 1;
END IF;
IF (ici_counter >= i_pagesize) THEN
EXIT;
END IF;
END LOOP; 		
END;
$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100
  ROWS 1000; 
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}topic_unread(
						   i_boardid integer,
						   i_categoryid integer,
						   i_pageuserid integer,
						   i_sincedate timestamp,
						   i_todate timestamp,
						   i_pageindex integer,
						   i_pagesize integer,
						   i_stylednicks boolean,
						   i_findlastunread boolean,
						   i_utctimestamp timestamp)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}topic_unread_return_type AS
$BODY$DECLARE
			 ici_topics_totalrowsnumber  integer;
			 ici_firstselectrownum integer;
			 ici_firstselectposted timestamp;
			 ici_pageindex integer := i_pageindex;
			 ici_retcount integer := 0;
			 ici_counter integer := 0;
			 _rec {databaseSchema}.{objectQualifier}topic_unread_return_type%ROWTYPE;               
BEGIN  
		-- find total returned count
	 select
	 COUNT(1) into ici_topics_totalrowsnumber
	 from {databaseSchema}.{objectQualifier}topic c
		  join {databaseSchema}.{objectQualifier}user b on b.UserID=c.UserID
		  join {databaseSchema}.{objectQualifier}forum d on d.ForumID=c.ForumID
		  join {databaseSchema}.{objectQualifier}activeaccess x on x.ForumID=d.ForumID
		  join {databaseSchema}.{objectQualifier}category cat on cat.CategoryID=d.CategoryID
	 where (c.lastposted between i_sincedate and i_todate) and
			x.userid = i_pageuserid and
			x.readaccess IS TRUE and
			cat.boardid = i_boardid and
			(i_categoryid is null or cat.categoryid=i_categoryid) and
			c.isdeleted IS FALSE and
			c.topicmovedid IS NULL;	

			ici_pageindex := ici_pageindex+1;
			ici_firstselectrownum := (ici_pageindex - 1) * i_pagesize + 1;

	 SELECT 		
		   c.lastposted
	 into ici_firstselectposted
	 from {databaseSchema}.{objectQualifier}topic c
		  join {databaseSchema}.{objectQualifier}user b on b.UserID=c.UserID
		  join {databaseSchema}.{objectQualifier}forum d on d.ForumID=c.ForumID
		  join {databaseSchema}.{objectQualifier}activeaccess x on x.ForumID=d.ForumID
		  join {databaseSchema}.{objectQualifier}category cat on cat.CategoryID=d.CategoryID
	 where
		  (c.lastposted between i_sincedate and i_todate) and
		   x.userid = i_pageuserid and
		   x.readaccess IS TRUE and
		   cat.boardid = i_boardid and
		   (i_categoryid is null or cat.categoryid=i_categoryid) and
		   c.isdeleted IS FALSE and
		   c.topicmovedid IS NULL	
	 ORDER BY
			 c.lastposted DESC,
			 cat.sortorder,
			 d.sortorder,
			 d.name DESC,
			 c.priority DESC;	

	 FOR _rec IN SELECT 	
					   c.forumid AS ForumID,
					   c.topicid AS TopicID,
					   c.topicmovedid,
					   c.posted AS Posted,
					   COALESCE(c.topicmovedid,c.topicid) AS LinkTopicID,
					   c.topic AS Subject,
					   c.status,
					   c.styles,
					   c.description,
					   c.userid,
					   COALESCE(c.username,b.name) AS Starter,
					   COALESCE(c.userdisplayname,b.displayname) AS StarterDisplay,
					   (SELECT COUNT(1) FROM {databaseSchema}.{objectQualifier}message mes 
										WHERE mes.topicid = c.topicid 
											  AND mes.isdeleted IS TRUE 
											  AND mes.isapproved IS TRUE 
											  AND ((i_pageuserid IS NOT NULL AND mes.userid = i_pageuserid) 
											  OR (i_pageuserid IS NULL)) ) AS NumPostsDeleted,
					   ((SELECT COUNT(1) FROM {databaseSchema}.{objectQualifier}message x 
										 WHERE x.topicid=c.topicid and (x.flags & 8)=0) - 1) AS Replies,
					   c.views AS Views,
					   c.lastposted AS LastPosted ,
					   c.lastuserid AS LastUserID,
					   COALESCE(c.lastusername,(SELECT x.name FROM {databaseSchema}.{objectQualifier}user x 
															  where x.userid=c.lastuserid)) AS   LastUserName,
					   COALESCE(c.lastuserdisplayname,(SELECT x.displayname FROM {databaseSchema}.{objectQualifier}user x 
															  where x.userid=c.lastuserid)) AS   LastUserDisplayName,
					   c.lastmessageid AS LastMessageID,
					   c.lastmessageflags,
					   c.topicid AS LastTopicID,
					   c.flags AS TopicFlags,
					   (SELECT COUNT(id) FROM {databaseSchema}.{objectQualifier}favoritetopic WHERE topicid = COALESCE(c.topicmovedid,c.topicid)),		
					   c.priority,
					   c.pollid,
					   d.name AS ForumName, 		
					   d.flags AS ForumFlags, 
					   (SELECT CAST(message AS text) FROM {databaseSchema}.{objectQualifier}message mes2 
														  where mes2.topicid = COALESCE(c.topicmovedid,c.topicid) 
																AND mes2.position = 0 LIMIT 1) AS FirstMessage,
					   (case(i_stylednicks)  when true THEN  
					   (SELECT us.userstyle FROM {databaseSchema}.{objectQualifier}user us where us.userid = c.userid)  
											 else ''	 end),
					   (case(i_stylednicks)  when true THEN 
					   (SELECT us.userstyle FROM {databaseSchema}.{objectQualifier}user us where us.userid = c.lastuserid)    
											 else ''	 end),
					   (case(i_findlastunread) when true THEN
					   COALESCE((SELECT lastaccessdate FROM {databaseSchema}.{objectQualifier}forumreadtracking x WHERE x.forumid=d.forumid AND x.userid = i_pageuserid), i_utctimestamp)
											 else TIMESTAMP '-infinity' end) AS LastForumAccess,
					   (case(i_findlastunread) when true THEN
					   COALESCE((SELECT lastaccessdate FROM {databaseSchema}.{objectQualifier}topicreadtracking y WHERE c.topicid=c.topicid AND y.userid = i_pageuserid), i_utctimestamp)
											   else TIMESTAMP '-infinity'	 end) AS LastTopicAccess,
		(SELECT string_agg(tag, ',') FROM {databaseSchema}.{objectQualifier}tags tg join {databaseSchema}.{objectQualifier}topictags tt on tt.tagid = tg.tagid where 	  tt.topicid = c.topicid),	
		c.topicimage,
		c.topicimagetype,
		c.topicimagebin,
		0 as HasAttachments,	 	
					   ici_topics_totalrowsnumber AS TotalRows,
					   ici_pageindex	AS 	PageIndex	  	     
	FROM
		{databaseSchema}.{objectQualifier}topic c
		JOIN {databaseSchema}.{objectQualifier}user b ON b.userid=c.userid
		JOIN {databaseSchema}.{objectQualifier}forum d ON d.forumid=c.forumid
		JOIN {databaseSchema}.{objectQualifier}activeaccess x ON x.forumid=d.forumid
		JOIN {databaseSchema}.{objectQualifier}category cat ON cat.categoryid=d.categoryid
	WHERE
		c.lastposted <= ici_firstselectposted AND
		x.userid = i_pageuserid AND
		x.readaccess IS TRUE AND 
	cat.boardid = i_boardid AND
	(i_categoryid IS NULL OR cat.categoryid=i_categoryid) AND
	c.isdeleted IS FALSE AND
	c.topicmovedid IS NULL
	ORDER BY
	c.lastposted DESC,
	cat.sortorder,
	d.sortorder,
	d.name DESC,
	c.priority DESC			
LOOP
-- RETURN NEXT _rec;
ici_retcount := ici_retcount +1; 
IF (ici_retcount between  ici_firstselectrownum and ici_firstselectrownum+i_pagesize) THEN
RETURN NEXT _rec;
ici_counter := ici_counter + 1;
END IF;
IF (ici_counter >= i_pagesize) THEN
EXIT;
END IF;
END LOOP; 		
END;
$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100
  ROWS 1000; 
--GO	

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}album_images_by_user(
						   i_userid integer) 
				  RETURNS SETOF {databaseSchema}.{objectQualifier}album_images_by_user_rt AS
$BODY$DECLARE 
			 _rec {databaseSchema}.{objectQualifier}album_images_by_user_rt%ROWTYPE;
BEGIN
	 FOR _rec IN SELECT 
					   a.imageid,
					   a.albumid,
					   a.caption,
					   a.filename,
					   a.bytes,
					   a.contenttype,
					   a.uploaded,
					   a.downloads
				 FROM  {databaseSchema}.{objectQualifier}useralbumimage a
					   INNER JOIN {databaseSchema}.{objectQualifier}useralbum b 
					   ON b.AlbumID = a.AlbumID
				 WHERE b.userid = i_userid 
				 ORDER BY a.albumid, 
						  a.imageid DESC
	LOOP
		RETURN NEXT _rec;
	END LOOP;
END;$BODY$
	LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
	COST 100 ROWS 1000;  
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}forum_maxid
						   (
						   i_boardid integer)
				  RETURNS integer AS
$BODY$
BEGIN 
	 RETURN (select a.forumid 
			 from {databaseSchema}.{objectQualifier}forum a 
				  join {databaseSchema}.{objectQualifier}category b 
				  on b.categoryid =a.categoryid 
				  where b.boardid= i_boardid 
				  order by a.forumid desc limit 1);	
END;
$BODY$
	LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
	COST 100;  
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}adminpageaccess_save
						   (
						   i_userid integer,
						   i_pagename varchar(128)
						   )
				  RETURNS void AS
$BODY$
begin
	if not exists (select 1 from {databaseSchema}.{objectQualifier}adminpageuseraccess where userid = i_userid and pagename = i_pagename limit 1) then		
		insert into {databaseSchema}.{objectQualifier}adminpageuseraccess (userid,pagename) 
		values(i_userid,i_pagename);
	end if;	
	
END;$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100; 
   
 --GO
 CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}adminpageaccess_delete(
						   i_userid integer,
						   i_pagename varchar(128))
				  RETURNS void AS
$BODY$
begin    	
		delete from  {databaseSchema}.{objectQualifier}adminpageuseraccess  where userid = i_userid AND (i_pagename IS NULL OR pagename = i_pagename);	
END;$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100;  
 --GO


 CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}adminpageaccess_list(
						   i_userid integer,
						   i_pagename varchar(128)) 
				  RETURNS SETOF {databaseSchema}.{objectQualifier}adminpageaccess_list_rt AS
$BODY$DECLARE 
			 _rec {databaseSchema}.{objectQualifier}adminpageaccess_list_rt%ROWTYPE;
BEGIN

 if (i_userid > 0  and i_pagename IS NOT NULL) then 
		for  _rec IN  select ap.userid,			                		
							 ap.pagename,			                
		u.name as UserName, 
		u.displayname as UserDisplayName, 
		b.name as BoardName 
		from {databaseSchema}.{objectQualifier}adminpageuseraccess ap 
		JOIN  {databaseSchema}.{objectQualifier}User u on ap.userid = u.userid 
		JOIN {databaseSchema}.{objectQualifier}Board b ON b.boardid = u.boardid 
		where u.userid = i_userid and pagename = i_pagename and (u.flags & 1) <> 1 
		order by  b.boardid,u.displayname,ap.pagename
		LOOP
		RETURN NEXT _rec;
		END LOOP;
		elseif (i_userid > 0 and i_pagename IS  NULL) then
		for  _rec IN  select ap.userid,			                			
							 ap.pagename, 
		u.name as UserName, 
		u.displayname as UserDisplayName, 
		b.name as BoardName 
		 from {databaseSchema}.{objectQualifier}adminpageuseraccess ap 
		JOIN  {databaseSchema}.{objectQualifier}user u on ap.userid = u.userid 
		JOIN {databaseSchema}.{objectQualifier}board b ON b.boardid = u.boardid 
		where u.userid = i_userid and (u.Flags & 1) <> 1 
		order by  b.boardid,u.displayname,ap.pagename 
		LOOP
		RETURN NEXT _rec;
		END LOOP; 
		else
		for  _rec IN  select ap.userid,			              		
							 ap.pagename,			                
		u.name as UserName, 
		u.displayname as UserDisplayName, 
		b.name as BoardName 
		from {databaseSchema}.{objectQualifier}adminpageuseraccess ap 
		JOIN  {databaseSchema}.{objectQualifier}user u on ap.userid = u.userid 
		JOIN {databaseSchema}.{objectQualifier}board b ON b.boardid = u.boardid 
		where (u.flags & 1) <> 1
		order by  b.boardid,u.displayname,ap.pagename 
		LOOP
		RETURN NEXT _rec;
		END LOOP;
	END IF;

END;$BODY$
	LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
	COST 100 ROWS 1000;  
--GO

 CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}group_eventlogaccesslist(
						   i_boardid integer) 
				  RETURNS SETOF {databaseSchema}.{objectQualifier}group_eventlogaccesslist_rt AS
$BODY$DECLARE 
			 _rec {databaseSchema}.{objectQualifier}group_eventlogaccesslist_rt%ROWTYPE;
BEGIN
		if i_boardid is null then
	for _rec in select g.groupid,
			 g.boardid,
			 g.name,
			 g.flags,
			 g.pmlimit,
			 g.style,
			 g.sortorder,
			 g.description,
			 g.usrsigchars,
			 g.usrsigbbcodes,
			 g.usrsightmltags,
			 g.usralbums,
			 g.usralbumimages,
			 b.Name as BoardName 
			 from {databaseSchema}.{objectQualifier}group g
		join {databaseSchema}.{objectQualifier}board b on b.boardid = g.boardid order by g.sortorder
		LOOP
		RETURN NEXT _rec;
		END LOOP; 
	else
	for _rec in	select g.groupid,
			 g.boardid,
			 g.name,
			 g.flags,
			 g.pmlimit,
			 g.style,
			 g.sortorder,
			 g.description,
			 g.usrsigchars,
			 g.usrsigbbcodes,
			 g.usrsightmltags,
			 g.usralbums,
			 g.usralbumimages,
			 b.Name as BoardName 
			 from {databaseSchema}.{objectQualifier}group g
		join {databaseSchema}.{objectQualifier}board b on b.boardid = g.boardid where g.boardid=i_boardid  order by g.sortorder
		LOOP
		RETURN NEXT _rec;
		END LOOP;
		end if;
END;$BODY$
	LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
	COST 100 ROWS 1000;  
--GO

 CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}eventlog_deletebyuser(
						   i_boardid integer,
						   i_pageuserid integer) 
				  RETURNS void AS
$BODY$
BEGIN
-- hostadmin - delete all
if (exists (select 1 from {databaseSchema}.{objectQualifier}user where ((flags & 1) = 1 and userid = i_pageuserid) limit 1)) then
delete from {databaseSchema}.{objectQualifier}eventlog where
			(userid is null or
			userid in (select userid from {databaseSchema}.{objectQualifier}user where boardid=i_boardid));
else	
		delete from {databaseSchema}.{objectQualifier}eventlog
		where EventLogID in (select a.eventlogid from {databaseSchema}.{objectQualifier}eventlog a
		left join {databaseSchema}.{objectQualifier}eventloggroupaccess e on e.eventtypeid = a.type 
		join {databaseSchema}.{objectQualifier}usergroup ug on (ug.userid =  i_pageuserid and ug.groupid = e.groupid)
		left join {databaseSchema}.{objectQualifier}user b on b.userid=a.userid
		where e.deleteaccess is true);
		end if;
END;$BODY$
	LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
	COST 100;  
--GO

 CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}eventloggroupaccess_save
							(
							i_groupid integer, 
							i_eventtypeid integer, 
							i_eventtypename varchar(128), 
							i_deleteaccess boolean
							) 
				   RETURNS void AS
$BODY$
BEGIN
	if not exists (select 1 from {databaseSchema}.{objectQualifier}eventloggroupaccess where groupid = i_groupid and eventtypename = i_eventtypename limit 1) then
		insert into {databaseSchema}.{objectQualifier}eventloggroupaccess  (groupid,eventtypeid,eventtypename,deleteaccess) 
		values(i_groupid,i_eventtypeid,i_eventtypename,i_deleteaccess);
	else
		update {databaseSchema}.{objectQualifier}eventloggroupaccess  set deleteaccess = i_deleteaccess
		where groupid = i_groupid and eventtypeid = i_eventtypeid;
	end if;
END;$BODY$
	LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
	COST 100;  
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}eventloggroupaccess_delete
							(
							i_groupid integer, 
							i_eventtypeid integer, 
							i_eventtypename varchar(128)
							) 
				   RETURNS void AS
$BODY$
BEGIN
	if i_eventtypename is not null then
		delete from {databaseSchema}.{objectQualifier}eventloggroupaccess  where groupid = i_groupid and eventtypeid = i_eventtypeid;
		else
	-- delete all access rights
		delete from {databaseSchema}.{objectQualifier}eventloggroupaccess  where groupid = i_groupid; 
	end if;
END;$BODY$
	LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
	COST 100;  
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}eventloggroupaccess_list
							(
							i_groupid integer, 
							i_eventtypeid integer
							) 
				   RETURNS SETOF {databaseSchema}.{objectQualifier}eventloggroupaccess_list_rt AS
$BODY$DECLARE 
			 _rec {databaseSchema}.{objectQualifier}eventloggroupaccess_list_rt%ROWTYPE;
BEGIN
-- TODO - exclude host admins from list   
if i_eventtypeid is null   then
		for _rec in select 
		e.groupid,	
		e.eventtypeid,  	
		e.eventtypename,
		e.deleteaccess,    
		g.Name as GroupName from {databaseSchema}.{objectQualifier}eventloggroupaccess e 
		join {databaseSchema}.{objectQualifier}group g on g.groupid = e.groupid where  e.groupid = i_groupid
		LOOP
		RETURN NEXT _rec;
		END LOOP;
		else
		for _rec in select e.groupid,	
		e.eventtypeid,  	
		e.eventtypename,
		e.deleteaccess,   
		g.Name as GroupName from {databaseSchema}.{objectQualifier}eventloggroupaccess e 
		join {databaseSchema}.{objectQualifier}group g on g.groupid = e.groupid where  e.groupid = i_groupid and e.eventtypeid = i_eventtypeid
		LOOP
		RETURN NEXT _rec;
		END LOOP;
		end if;
END;$BODY$
	LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
	COST 100 ROWS 1000;  
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}forum_cataccess_actuser(i_boardid integer, i_userid integer)
				   RETURNS SETOF {databaseSchema}.{objectQualifier}forum_cataccess_actuser_rt AS
$BODY$DECLARE
_rec {databaseSchema}.{objectQualifier}forum_cataccess_actuser_rt%ROWTYPE;
_nid integer; 
BEGIN

FOR _rec IN SELECT DISTINCT(f.categoryid) as CategoryID, c.name as CategoryName, c.sortorder 
FROM {databaseSchema}.{objectQualifier}forum f 
JOIN {databaseSchema}.{objectQualifier}category c on c.categoryid = f.categoryid
JOIN {databaseSchema}.{objectQualifier}activeaccess access ON (f.forumid = access.forumid and access.userid = i_userid)  
WHERE c.boardid = i_boardid and f.parentid IS NULL 
and  (access.readaccess is true or (access.readaccess is false and f.flags & 2 <> 2)) 
ORDER BY c.sortorder, CategoryID, c.name
LOOP
RETURN NEXT _rec;
END LOOP;
END;
$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER CALLED ON NULL INPUT
  COST 100;
--GO
COMMENT ON FUNCTION {databaseSchema}.{objectQualifier}forum_cataccess_actuser(integer,integer) IS 'Returns categories where a user has access.';
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}topic_tags(i_boardid integer, i_pageuserid integer, i_topicid integer)
				   RETURNS SETOF {databaseSchema}.{objectQualifier}topic_tags_rt AS
$BODY$DECLARE
_rec {databaseSchema}.{objectQualifier}topic_tags_rt%ROWTYPE;
_maxcount integer;
BEGIN
SELECT MAX(tg.tagcount) INTO _maxcount
			FROM {databaseSchema}.{objectQualifier}tags tg 
			JOIN  {databaseSchema}.{objectQualifier}topictags tt ON tt.TagID = tg.TagID 
			JOIN  {databaseSchema}.{objectQualifier}topic t ON t.TopicID = tt.TopicID
			JOIN  {databaseSchema}.{objectQualifier}activeaccess aa ON (aa.ForumID = t.ForumID AND aa.userid = i_pageuserid)
	WHERE aa.boardid=i_boardid and t.topicid=i_topicid; 

FOR _rec IN SELECT tg.tagid,tg.tag,tg.tagcount,_maxcount AS MaxTagCount 
			FROM {databaseSchema}.{objectQualifier}tags tg 
			JOIN  {databaseSchema}.{objectQualifier}topictags tt ON tt.TagID = tg.TagID 
			JOIN  {databaseSchema}.{objectQualifier}topic t ON t.TopicID = tt.TopicID
			JOIN  {databaseSchema}.{objectQualifier}activeaccess aa ON (aa.ForumID = t.ForumID AND aa.userid = i_pageuserid)
	WHERE aa.boardid=i_boardid and t.topicid=i_topicid 	
	ORDER BY tg.tag
LOOP
RETURN NEXT _rec;
END LOOP;
END;
$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER CALLED ON NULL INPUT
  COST 100;
--GO
COMMENT ON FUNCTION {databaseSchema}.{objectQualifier}topic_tags(integer,integer,integer) IS 'Returns tags list for a topic with tags count.';
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}forum_tags(i_boardid integer, i_pageuserid integer, i_forumid integer, i_pageindex integer, i_pagesize integer,i_searchtext varchar(255), i_beginswith boolean)
				   RETURNS SETOF {databaseSchema}.{objectQualifier}forum_tags_rt AS
$BODY$DECLARE
			 ici_firstselectrownumber int :=0; 
			 _maxcount integer;		
			 ici_tags_totalrowsnumber  integer :=0; 
			 ici_pageindex integer := i_pageindex;
			 _rec {databaseSchema}.{objectQualifier}forum_tags_rt%ROWTYPE;

BEGIN
SELECT MAX(tmpm.tagcount) FROM (select distinct(tg.tagcount) 
			FROM {databaseSchema}.{objectQualifier}tags tg 
			JOIN  {databaseSchema}.{objectQualifier}topictags tt ON tt.TagID = tg.TagID 
			JOIN  {databaseSchema}.{objectQualifier}topic t ON t.TopicID = tt.TopicID
			JOIN  {databaseSchema}.{objectQualifier}activeaccess aa ON (aa.ForumID = t.ForumID AND aa.userid = i_pageuserid)
	WHERE aa.boardid=i_boardid and (i_forumid is null OR t.forumid=i_forumid) 
	 AND (t.flags & 8) <> 8 
	 AND
	  tg.tag ILIKE CASE 
			WHEN (not i_beginswith and i_searchtext IS NOT NULL AND LENGTH(i_searchtext) > 0) THEN ('%' || i_searchtext || '%') 
			WHEN (i_beginswith and i_searchtext IS NOT NULL AND LENGTH(i_searchtext) > 0) THEN (i_searchtext  || '%')  
			ELSE '%' END) as tmpm INTO _maxcount; 

   -- find total returned count
		select
		COUNT(*) from (select DISTINCT(tt.tagid) 
   FROM {databaseSchema}.{objectQualifier}tags tg 
			JOIN  {databaseSchema}.{objectQualifier}topictags tt ON tt.TagID = tg.TagID 
			JOIN  {databaseSchema}.{objectQualifier}topic t ON t.TopicID = tt.TopicID
			JOIN  {databaseSchema}.{objectQualifier}activeaccess aa ON (aa.ForumID = t.ForumID AND aa.userid = i_pageuserid)
	WHERE aa.boardid=i_boardid and (i_forumid is null OR t.forumid=i_forumid)
	AND (t.flags & 8) <> 8
	AND
	  tg.tag ILIKE CASE 
			WHEN (not i_beginswith and i_searchtext IS NOT NULL AND LENGTH(i_searchtext) > 0) THEN ('%' || i_searchtext || '%') 
			WHEN (i_beginswith and i_searchtext IS NOT NULL AND LENGTH(i_searchtext) > 0) THEN (i_searchtext  || '%')  
			ELSE '%' END) as tmpc into ici_tags_totalrowsnumber; 

		 ici_pageindex := ici_pageindex+1;
		 ici_firstselectrownumber := (ici_pageindex - 1) * i_pagesize;

FOR _rec IN SELECT DISTINCT(tt.tagid),tg.tag,tg.tagcount,_maxcount AS MaxTagCount, ici_tags_totalrowsnumber AS TotalCount
			FROM {databaseSchema}.{objectQualifier}tags tg 
			JOIN  {databaseSchema}.{objectQualifier}topictags tt ON tt.TagID = tg.TagID 
			JOIN  {databaseSchema}.{objectQualifier}topic t ON t.TopicID = tt.TopicID
			JOIN  {databaseSchema}.{objectQualifier}activeaccess aa ON (aa.forumid = t.forumid AND aa.userid = i_pageuserid)
	WHERE aa.boardid=i_boardid and (i_forumid is null OR t.forumid=i_forumid) 
	AND (t.flags & 8) <> 8 AND
	  tg.tag ILIKE CASE 
			WHEN (not i_beginswith and i_searchtext IS NOT NULL AND LENGTH(i_searchtext) > 0) THEN ('%' || i_searchtext || '%') 
			WHEN (i_beginswith and i_searchtext IS NOT NULL AND LENGTH(i_searchtext) > 0) THEN (i_searchtext  || '%')  
			ELSE '%' END
	ORDER BY tg.tag limit i_pagesize offset ici_firstselectrownumber
LOOP
RETURN NEXT _rec;
END LOOP; 	
END;
$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER CALLED ON NULL INPUT
  COST 100;
--GO
COMMENT ON FUNCTION {databaseSchema}.{objectQualifier}forum_tags(integer,integer,integer,integer,integer,varchar,boolean) IS 'Returns tags list for a forum or board with tags count.';
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}topic_imagesave(
						   i_topicid integer,
						   i_imageurl varchar(255),
						   i_stream bytea,
						   i_topicimagetype varchar(255))
				  RETURNS void AS
$BODY$
begin
		update {databaseSchema}.{objectQualifier}topic
		set topicimage = i_imageurl,
			topicimagetype = i_topicimagetype,
			topicimagebin = i_stream
		where topicid = i_topicid;
	
END;$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100;  
 --GO

 CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}user_listtodaysbirthdays(
	i_boardid 	        integer,
	i_stylednicks       boolean,
	i_currentyear	    integer,
	i_currentutc	    timestamp,
	i_currentutc1	    timestamp,
	i_currentutc2	    timestamp
 ) 
					RETURNS SETOF {databaseSchema}.{objectQualifier}user_listtodaysbirthdays_rt
					AS
 $BODY$DECLARE
  _rec {databaseSchema}.{objectQualifier}user_listtodaysbirthdays_rt%ROWTYPE;
 BEGIN
		FOR _rec IN  SELECT up.Birthday, 
				  up.UserID,
				  u.TimeZone, 
				  u.name as UserName,
				  u.DisplayName AS UserDisplayName,
				  (case when (i_stylednicks) then u.userstyle else '' end) AS Style 
				  FROM {databaseSchema}.{objectQualifier}userprofile up 
				  JOIN {databaseSchema}.{objectQualifier}user
				  u ON u.userid = up.userid where u.boardid = i_boardid 
				  AND (up.birthday + (i_currentyear - extract(year  from up.birthday))*interval '1 year' between (i_currentutc - interval '24 hours') and (i_currentutc + interval '24 hours'))
				  LOOP
RETURN NEXT _rec;
END LOOP;
END;
$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER CALLED ON NULL INPUT
  COST 100 ROWS 1000;
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}db_size(
						   i_dbscheme varchar(255), i_dbname varchar(255))
				  RETURNS integer AS
$BODY$
BEGIN
RETURN (select cast(pg_database_size(current_database())/1024/1024 as integer));
END;
$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100; 
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}forum_save_prntchck(i_forumid integer, i_parentid integer)
  RETURNS integer AS
$BODY$
begin
	return (SELECT {databaseSchema}.{objectQualifier}forum_save_parentschecker(i_forumid, i_parentid));
END$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER CALLED ON NULL INPUT
  COST 100; 
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}pollgroup_save(
	i_topicid 	     integer,
	i_forumid        integer,
	i_categoryid     integer,
	i_userid 	     integer,
	i_flags          integer,
	i_utctimestamp   timestamp
 ) 
					RETURNS {databaseSchema}.{objectQualifier}pollgroup_save_rt	as		
 $BODY$DECLARE
 ici_pollgroupid integer;
 ici_newpollgroupid integer;
 _rec {databaseSchema}.{objectQualifier}pollgroup_save_rt%ROWTYPE;
 BEGIN
  if (i_topicid is not null)  then
				select pollid into ici_pollgroupid  from {databaseSchema}.{objectQualifier}topic WHERE topicid = i_topicid;
	end if;			               
  if (i_forumid is not null) then               
				select pollgroupid  into ici_pollgroupid from {databaseSchema}.{objectQualifier}forum WHERE forumid = i_forumid; 
	end if;			               
  if (i_categoryid is not null) then
				select pollgroupid  into ici_pollgroupid  from {databaseSchema}.{objectQualifier}category
				WHERE categoryid = i_categoryid;
  end if;

			if (ici_pollgroupid	is null) then		
				insert into {databaseSchema}.{objectQualifier}pollgroupcluster(userid,flags) values (i_userid, i_flags)
				returning pollgroupid into ici_newpollgroupid; 				
			 end if;

				select ici_pollgroupid, ici_newpollgroupid into _rec; 
				return _rec;
END;
$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER 
  COST 100;
--GO

 CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}poll_save(
	i_question	     varchar(255),
	i_closes	     timestamp,
	i_userid	     integer,
	i_pollgroupid	 integer,	
	i_objectpath	 varchar(255),
	i_mimetype	     varchar(50),
	i_flags	         integer,
	i_utctimestamp   timestamp
 ) 
					RETURNS integer					
					AS
 $BODY$DECLARE 
 i_pollid integer;
 BEGIN
 insert into {databaseSchema}.{objectQualifier}poll
 (question,closes,pollgroupid,userid,objectpath,mimetype,flags)
 values(i_question,i_closes,i_pollgroupid,i_userid,i_objectpath,i_mimetype,i_flags) 
 returning pollid into i_pollid;
 return i_pollid;
END;
$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER 
  COST 100;
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}choice_save(
	i_pollid integer,
	i_choice varchar(255),
	i_votes integer,
	i_objectpath varchar(255),
	i_mimetype varchar(50),
	i_utctimestamp timestamp) 
					returns void					
					AS
 $BODY$
 BEGIN
		   insert into {databaseSchema}.{objectQualifier}choice(pollid,choice,votes,objectpath,mimetype) 
		   values(i_pollid,i_choice,i_votes,i_objectpath,i_mimetype);        
 END;
 $BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER 
  COST 100;
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}pollgroup_setlinks(
	i_topicid 	     integer,
	i_forumid        integer,
	i_categoryid     integer,
	i_pollgroupid 	 integer,
	i_utctimestamp   timestamp
 ) 
					returns void					
					AS
 $BODY$
 BEGIN
				-- fill a pollgroup field - double work if a poll exists 
				if (i_topicid is not null) then
					update {databaseSchema}.{objectQualifier}topic set pollid = i_pollgroupid where topicid = i_topicid; 
				-- fill a pollgroup field in Forum Table if the call comes from a forum's topic list 
				elseif (i_forumid is not null)  then               
					update {databaseSchema}.{objectQualifier}forum set pollgroupid = i_pollgroupid where forumid = i_forumid; 
				-- fill a pollgroup field in Category Table if the call comes from a category's topic list 
				elseif (i_categoryid is not null) then             
					update {databaseSchema}.{objectQualifier}category set pollgroupid = i_pollgroupid where categoryid= i_categoryid;  
				end if;               

				-- fill a pollgroup field in Board Table if the call comes from the main page poll               
END;
$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER 
  COST 100;
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}topic_restore(
						   i_topicid integer, 
						   i_userid integer)
				  RETURNS void AS
$BODY$
DECLARE ici_forumid integer;
BEGIN   
		UPDATE {databaseSchema}.{objectQualifier}topic 
		SET flags = flags # 8 
		WHERE topicid = i_topicid and (flags & 8) = 8; 
		  UPDATE {databaseSchema}.{objectQualifier}topic 
		SET flags = flags # 8
		WHERE topicmovedid = i_topicid and (flags & 8) = 8; 
		UPDATE {databaseSchema}.{objectQualifier}message 
		SET flags = flags # 8
		WHERE topicid = i_topicid and (flags & 8) = 8;

		UPDATE  {databaseSchema}.{objectQualifier}topic 
		SET lastmessageid = (SELECT m.messageid from  {databaseSchema}.{objectQualifier}message m
		   where m.topicid = i_topicid and (m.Flags & 8) != 8 ORDER BY m.posted desc LIMIT 1)
		where topicid = i_topicid;   

		select forumid into ici_forumid from {databaseSchema}.{objectQualifier}topic where topicid = i_topicid;
		PERFORM  {databaseSchema}.{objectQualifier}forum_updatelastpost (ici_forumid);  
		PERFORM  {databaseSchema}.{objectQualifier}forum_updatestats(ici_forumid); 
END;$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100; 
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}digest_topicnew(
				 i_boardid integer,
				 i_pageuserid integer,
				 i_sincedate timestamp,
				 i_todate timestamp,              
				 i_stylednicks boolean,               
				 i_utctimestamp timestamp)
		
RETURNS SETOF {databaseSchema}.{objectQualifier}digest_topicactivenew_rt
					AS
 $BODY$DECLARE
  _rec {databaseSchema}.{objectQualifier}digest_topicactivenew_rt%ROWTYPE;
 BEGIN
 for _rec in SELECT
		d.name AS "ForumName",
		c.topic AS "Subject",
		c.userdisplayname AS "StartedUserName",		
		c.lastuserdisplayname as "LastUserName" ,
		c.lastmessageid as "LastMessageID",
		(SELECT x.message FROM {databaseSchema}.{objectQualifier}message x 
		  WHERE x.topicid=c.topicid and x.messageid = c.lastmessageid) as "LastMessage",
		(SELECT COUNT(1) - 1 FROM {databaseSchema}.{objectQualifier}message x 
		  WHERE x.topicid=c.topicid and (x.flags & 8) = 0) as "Replies"    
	FROM
		{databaseSchema}.{objectQualifier}topic c  
		JOIN {databaseSchema}.{objectQualifier}forum d ON d.forumid=c.forumid 
		JOIN {databaseSchema}.{objectQualifier}category cat ON cat.categoryid = d.categoryid     
		join {databaseSchema}.{objectQualifier}vaccess x on (x.forumid=d.forumid AND x.userid = i_pageuserid AND x.readaccess <> 0)      
	WHERE
	   cat.boardid = i_boardid AND  
	   c.posted > i_sincedate   and      
	  (c.flags & 8) = 0
	ORDER BY
	d.sortorder,
	c.lastposted desc 
	loop
	return next _rec;
	end loop;
END;$BODY$
 LANGUAGE 'plpgsql' STABLE SECURITY DEFINER 
 COST 100 ROWS 1000; 
 --GO

CREATE OR REPLACE FUNCTION  {databaseSchema}.{objectQualifier}digest_topicactive(
				 i_boardid integer,
				 i_pageuserid integer,
				 i_sincedate timestamp,
				 i_todate timestamp,              
				 i_stylednicks boolean,               
				 i_utctimestamp timestamp)
		
RETURNS SETOF {databaseSchema}.{objectQualifier}digest_topicactivenew_rt
					AS
 $BODY$DECLARE
  _rec {databaseSchema}.{objectQualifier}digest_topicactivenew_rt%ROWTYPE;
 BEGIN
 for _rec in SELECT
		d.name AS "ForumName",
		c.topic AS "Subject",
		c.userdisplayname AS "StartedUserName",		
		c.lastuserdisplayname as "LastUserName" ,
		c.lastmessageid as "LastMessageID",
		(SELECT x.message FROM {databaseSchema}.{objectQualifier}message x 
		  WHERE x.topicid=c.topicid and x.messageid = c.lastmessageid) as "LastMessage",
		(SELECT COUNT(1)-1 FROM {databaseSchema}.{objectQualifier}message x 
		  WHERE x.topicid=c.topicid and (x.flags & 8) = 0) as "Replies"    
	FROM
		{databaseSchema}.{objectQualifier}topic c  
		JOIN {databaseSchema}.{objectQualifier}forum d ON d.forumid=c.forumid 
		JOIN {databaseSchema}.{objectQualifier}category cat ON cat.categoryid = d.categoryid     
		join {databaseSchema}.{objectQualifier}vaccess x on (x.forumid=d.forumid AND x.userid = i_pageuserid AND x.readaccess <> 0)      
	WHERE
	   cat.boardid = i_boardid AND    
	   c.lastposted > i_sincedate and
	   c.lastposted < i_todate   and  
	  (c.flags & 8) = 0
	ORDER BY
	d.sortorder,
	c.lastposted desc 
	loop
	return next _rec;
	end loop;
END;$BODY$
   LANGUAGE 'plpgsql' STABLE SECURITY DEFINER 
   COST 100 ROWS 1000;   
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}topic_tagsave(i_topicid integer,
						   i_messageidsstr text)
				  RETURNS void AS
$BODY$
DECLARE 
i_messageids varchar[];
i integer :=1;
i_tagid INT;
i_tagids int[];
BEGIN
 -- first we should reset tag count for a topic
 UPDATE {databaseSchema}.{objectQualifier}tags SET tagcount = tagcount - 1 WHERE tagid IN (SELECT tagid FROM  {databaseSchema}.{objectQualifier}topictags where topicid = i_topicid);
   -- delete tags
	DELETE FROM {databaseSchema}.{objectQualifier}topictags where topicid = i_topicid;

 i_messageids = string_to_array(i_messageidsstr,',');
 -- i := 1; 
  WHILE i_messageids[i] IS NOT NULL LOOP
		UPDATE {databaseSchema}.{objectQualifier}tags SET tag = i_messageids[i] where lower(tag) = lower(i_messageids[i]) returning tagid into i_tagid ;
		IF NOT found THEN
		BEGIN
			INSERT INTO {databaseSchema}.{objectQualifier}tags(tag) VALUES (i_messageids[i]) returning tagid into i_tagid ;
		EXCEPTION WHEN unique_violation THEN
			UPDATE {databaseSchema}.{objectQualifier}tags SET tag = i_messageids[i] where lower(tag) = lower(i_messageids[i]) returning tagid into i_tagid ;
		END;
		END IF;

		UPDATE {databaseSchema}.{objectQualifier}topictags SET topicid = i_topicid where tagid = i_tagid and topicid = i_topicid;
		IF NOT found THEN
		BEGIN
			INSERT INTO {databaseSchema}.{objectQualifier}topictags(tagid, topicid) VALUES (i_tagid, i_topicid);
			-- increase tag count
			
		EXCEPTION WHEN unique_violation THEN
			UPDATE {databaseSchema}.{objectQualifier}topictags SET topicid = i_topicid where tagid = i_tagid and topicid = i_topicid;
			
		END;
		END IF;
			UPDATE {databaseSchema}.{objectQualifier}tags SET tagcount = (SELECT Count(tagid) from {databaseSchema}.{objectQualifier}topictags WHERE tagid = i_tagid) where tagid = i_tagid;	
  i := i + 1; 
 END LOOP;

RETURN;
END;
$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER COST 100;
--GO