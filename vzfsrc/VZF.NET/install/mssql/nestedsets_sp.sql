
/* ******************************************************************************************************************************
*********************************************************************************************************************************
SELECT FUNCTIONS
*********************************************************************************************************************************
******************************************************************************************************************************** */
IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}forum_ns_getchildren]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}forum_ns_getchildren]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}forum_ns_getch_actuser]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}forum_ns_getch_actuser]
GO
IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}forum_ns_getch_anyuser]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}forum_ns_getch_anyuser]
GO
IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}forum_ns_getpath]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}forum_ns_getpath]
GO
IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}forum_ns_recreate]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}forum_ns_recreate]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}forum_ns_listpath]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}forum_ns_listpath]
GO


CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}forum_ns_getchildren](@boardid int, @categoryid int,@forumid int, @notincluded bit,@immediateonly bit)
AS
BEGIN
DECLARE @ici_nid int;
if (@forumid > 0) 
SELECT top 1 @ici_nid = ns.nid 
FROM [{databaseSchema}].[{objectQualifier}forum_ns] ns
WHERE ns.forumid = @forumid;
else if  (@categoryid > 0) 
SELECT top 1 @ici_nid = ns.nid 
FROM [{databaseSchema}].[{objectQualifier}forum_ns] ns
WHERE ns.forumid = 0 and ns.categoryid = @categoryid;
else
SELECT @ici_nid = ns.nid 
FROM [{databaseSchema}].[{objectQualifier}forum_ns] ns
WHERE ns.forumid = 0 and ns.categoryid = 0 and boardid =@boardid;


SELECT n1.forumid as ForumID, n1.parentid as ParentID, n1.[level] as [Level]
FROM [{databaseSchema}].[{objectQualifier}forum_ns]  n1,
[{databaseSchema}].[{objectQualifier}forum_ns]  n2   WHERE  ( n2.nid = @ici_nid
AND  n1.left_key BETWEEN n2.left_key + CAST(@notincluded as int) AND n2.right_key
and (@immediateonly = 0  OR n1.parentid = n2.nid)) ORDER BY n1.left_key;
END;
go

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}forum_ns_getch_actuser](@boardid int, @categoryid int,@forumid int ,@userid int,@notincluded bit,@immediateonly bit)
AS
BEGIN
declare @ici_nid integer; 

SELECT @ici_nid = ns.nid 
FROM [{databaseSchema}].[{objectQualifier}forum_ns] ns
WHERE ns.forumid = 0 and ns.categoryid = 0 and boardid = @boardid;

if (@forumid > 0) 
SELECT @ici_nid = ns.nid 
FROM [{databaseSchema}].[{objectQualifier}forum_ns] ns
WHERE ns.forumid = @forumid;
else if  (@categoryid > 0) 
SELECT @ici_nid = ns.nid 
FROM [{databaseSchema}].[{objectQualifier}forum_ns] ns
WHERE ns.forumid = 0 and ns.categoryid = @categoryid;
else
SELECT @ici_nid = ns.nid 
FROM [{databaseSchema}].[{objectQualifier}forum_ns] ns
WHERE ns.forumid = 0 and ns.categoryid = 0 and boardid = @boardid;


SELECT b.boardid, b.name as BoardName, c.categoryid, c.name as CategoryName, f.name as Title, (CASE WHEN access.readaccess = 0 THEN 1 ELSE 0 END) as NoAccess, 
n1.forumid, n1.parentid, n1.[level] as [Level],SIGN(n1.right_key-n1.left_key - 1) as HasChildren
FROM [{databaseSchema}].[{objectQualifier}forum] f 
JOIN [{databaseSchema}].[{objectQualifier}category] c on c.categoryid = f.categoryid
JOIN [{databaseSchema}].[{objectQualifier}board] b on b.boardid = c.boardid 
JOIN [{databaseSchema}].[{objectQualifier}activeaccess] access ON (f.forumid = access.forumid and access.userid =@userid)  
JOIN [{databaseSchema}].[{objectQualifier}forum_ns]  n1 ON (n1.forumid = f.forumid)
CROSS JOIN
[{databaseSchema}].[{objectQualifier}forum_ns]  n2   WHERE  (access.readaccess > 0 or (access.readaccess = 0 and (f.flags & 2) != 2)) and ( n2.nid = @ici_nid
AND  n1.left_key BETWEEN n2.left_key + cast(@notincluded as int) AND n2.right_key
and (@immediateonly = 0  OR n1.parentid = n2.nid)) ORDER BY n1.left_key;
END;
go

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}forum_ns_getch_anyuser](@boardid int, @categoryid int,@forumid int ,@userid int,@notincluded bit,@immediateonly bit)				 
AS
BEGIN
declare @ici_nid int;

if (@forumid > 0)
SELECT @ici_nid = ns.nid
FROM [{databaseSchema}].[{objectQualifier}forum_ns] ns
WHERE ns.forumid = @forumid;
else if  (@categoryid > 0) 
SELECT @ici_nid = ns.nid
FROM [{databaseSchema}].[{objectQualifier}forum_ns] ns
WHERE ns.forumid = 0 and ns.categoryid = @categoryid;
else
SELECT @ici_nid = ns.nid
FROM [{databaseSchema}].[{objectQualifier}forum_ns] ns
WHERE ns.forumid = 0 and ns.categoryid = 0 and boardid = @boardid;


SELECT b.boardid, b.name as BoardName, c.categoryid, c.name as CategoryName, f.name as Title, (CASE WHEN access.readaccess = 0 THEN 1 ELSE 0 END) as NoAccess, 
n1.forumid, n1.parentid, n1.[level] as [Level], SIGN(n1.right_key-n1.left_key - 1) as HasChildren
FROM [{databaseSchema}].[{objectQualifier}forum] f 
JOIN [{databaseSchema}].[{objectQualifier}category] c on c.categoryid = f.categoryid 
JOIN [{databaseSchema}].[{objectQualifier}board] b on b.boardid = c.boardid 
JOIN [{databaseSchema}].[{objectQualifier}vaccess_combo] access ON (f.forumid = access.ForumID and access.UserID = @userid)  
JOIN [{databaseSchema}].[{objectQualifier}forum_ns]  n1 ON n1.forumid = f.forumid
CROSS JOIN
[{databaseSchema}].[{objectQualifier}forum_ns]  n2   
WHERE (access.readaccess > 0 or (access.readaccess = 0 and (f.flags & 2) != 2)) and ( n2.nid = @ici_nid
AND  n1.left_key BETWEEN n2.left_key + CAST(@notincluded as INT) AND n2.right_key
and (@immediateonly = 0  OR n1.parentid = n2.nid)) ORDER BY n1.left_key;
END;
go
   
CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}forum_ns_getpath](@forumid int,@parentincluded bit)	
AS		
BEGIN
declare @ici_nid int;

SELECT @ici_nid = ns.nid
FROM [{databaseSchema}].[{objectQualifier}forum_ns] ns
WHERE ns.forumid = @forumid;

 SELECT n1.forumid, n1.parentid, n1.[level]
FROM [{databaseSchema}].[{objectQualifier}forum] f
join [{databaseSchema}].[{objectQualifier}forum_ns] n1 
on n1.forumid = f.forumid 
order by n1.left_key,f.categoryid, f.sortorder;
END; 
GO

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}forum_ns_listpath](
						   @ForumID integer)
AS 
BEGIN				 
DECLARE @ici_left_key integer;
DECLARE @ici_right_key integer;

SELECT @ici_left_key = left_key, @ici_right_key = right_key
FROM [{databaseSchema}].[{objectQualifier}forum_ns] where forumid =@ForumID;
SELECT f.forumid,
	   f.name,
	   -- we don't return board and category nodes here
	   (ns.[level] - 2) as [Level] 
	   FROM [{databaseSchema}].[{objectQualifier}forum_ns] ns 
	   JOIN [{databaseSchema}].[{objectQualifier}forum] f on f.forumid = ns.forumid
	   WHERE ns.left_key <= @ici_left_key AND ns.right_key >= @ici_right_key ORDER BY ns.left_key;					 
END;
GO


-- Initialize all this
CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}forum_ns_recreate]
AS
BEGIN
exec [{databaseSchema}].[{objectQualifier}create_or_check_ns_tables];
exec [{databaseSchema}].[{objectQualifier}fillin_or_check_ns_tables];
END;  
go 

exec ('[{databaseSchema}].[{objectQualifier}forum_ns_recreate]');
go
