
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

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}forum_ns_getch_accgroup]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}forum_ns_getch_accgroup]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}forum_ns_recreate]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}forum_ns_recreate]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}forum_ns_recreate_ini]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}forum_ns_recreate_ini]
GO

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}forum_ns_listpath]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}forum_ns_listpath]
GO


CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}forum_ns_getchildren](@boardid int, @categoryid int,@forumid int, @notincluded bit,@immediateonly bit)
AS
BEGIN
if (@forumid > 0) 
begin
SELECT b.boardid, b.name as BoardName, c.categoryid, c.name as CategoryName, n1.name as Title,  
n1.forumid, n1.parentid, n1.[level] as [Level],(CONVERT([bit],sign(n1.right_key-n1.left_key-1),(0))) as HasChildren
FROM 
[{databaseSchema}].[{objectQualifier}board] b 
join [{databaseSchema}].[{objectQualifier}category] c on b.boardid = c.boardid
join [{databaseSchema}].[{objectQualifier}forum] n1 on c.categoryid = n1.categoryid 
CROSS JOIN
[{databaseSchema}].[{objectQualifier}forum]  n2   WHERE  n2.ForumID = @ForumID
AND  n1.left_key BETWEEN n2.left_key + cast(@notincluded as int) AND n2.right_key
and (@immediateonly = 0  OR n1.ParentID = n2.ForumID) ORDER BY n1.left_key;
end
else
SELECT b.boardid, b.name as BoardName, c.categoryid, c.name as CategoryName, f.name as Title,  
f.forumid, f.parentid, f.[level] as [Level],(CONVERT([bit],sign(f.right_key-f.left_key-1),(0))) as HasChildren
FROM 
[{databaseSchema}].[{objectQualifier}board] b 
join [{databaseSchema}].[{objectQualifier}category] c on b.boardid = c.boardid  
JOIN [{databaseSchema}].[{objectQualifier}Forum]  f ON (c.categoryid = f.categoryid and f.CategoryID = @CategoryID and f.parentid is null)
ORDER BY f.left_key;
END;
go

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}forum_ns_getch_accgroup](@boardid int, @categoryid int,@forumid int, @GroupID int, @notincluded bit,@immediateonly bit)
AS
BEGIN
if (@forumid > 0) 
begin
SELECT b.BoardID, b.Name as BoardName, c.CategoryID, c.name as CategoryName, n1.name as Title, fa.AccessMaskID, 
n1.ForumID, n1.ParentID, n1.[level] as [Level],SIGN(n1.right_key-n1.left_key - 1) as HasChildren
FROM 
[{databaseSchema}].[{objectQualifier}Board] b  
JOIN [{databaseSchema}].[{objectQualifier}Category] c on b.BoardID = c.BoardID
JOIN [{databaseSchema}].[{objectQualifier}Forum]  n1 on c.CategoryID = n1.CategoryID 
CROSS JOIN
[{databaseSchema}].[{objectQualifier}Forum]  n2  
join [{databaseSchema}].[{objectQualifier}ForumAccess] fa  on (fa.ForumID = n1.ForumID and fa.GroupID = @GroupID) 
WHERE   n2.ForumID = @ForumID
AND  n1.left_key BETWEEN n2.left_key + cast(@notincluded as int) AND n2.right_key
and (@immediateonly = 0  OR n1.ParentID = n2.ForumID) ORDER BY n1.left_key;
end
else
SELECT b.BoardID, b.Name as BoardName, c.CategoryID, c.name as CategoryName, f.Name as Title, fa.AccessMaskID, 
f.ForumID, f.ParentID, f.[level] as [Level],SIGN(f.right_key-f.left_key - 1) as HasChildren
FROM 
[{databaseSchema}].[{objectQualifier}board] b 
join [{databaseSchema}].[{objectQualifier}category] c on b.boardid = c.boardid  
JOIN [{databaseSchema}].[{objectQualifier}Forum]  f ON (c.categoryid = f.categoryid and f.CategoryID = @CategoryID and f.parentid is null)
join [{databaseSchema}].[{objectQualifier}ForumAccess] fa  on (fa.ForumID = f.ForumID and fa.GroupID = @GroupID)
ORDER BY f.left_key;
END;
go

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}forum_ns_getch_actuser](@boardid int, @categoryid int,@forumid int ,@userid int,@notincluded bit,@immediateonly bit)
AS
BEGIN
declare @ici_nid integer; 

if (@forumid > 0) 
begin
SELECT b.boardid, b.name as BoardName, c.categoryid, c.name as CategoryName, n1.name as Title, (CASE WHEN access.readaccess = 0 THEN 1 ELSE 0 END) as NoAccess, 
n1.forumid, n1.parentid, n1.[level] as [Level],SIGN(n1.right_key-n1.left_key - 1) as HasChildren
FROM 
[{databaseSchema}].[{objectQualifier}Board] b  
JOIN [{databaseSchema}].[{objectQualifier}Category] c on b.BoardID = c.BoardID
JOIN [{databaseSchema}].[{objectQualifier}Forum]  n1 on c.CategoryID = n1.CategoryID 
CROSS JOIN
[{databaseSchema}].[{objectQualifier}Forum]  n2   
JOIN [{databaseSchema}].[{objectQualifier}activeaccess] access ON (n1.forumid = access.forumid and access.userid =@userid)  
WHERE  (access.readaccess > 0 or (access.readaccess = 0 and (n2.flags & 2) != 2)) and ( n2.ForumID = @ForumID
AND  n1.left_key BETWEEN n2.left_key + cast(@notincluded as int) AND n2.right_key
and (@immediateonly = 0  OR n1.parentid = n2.ForumID)) ORDER BY n1.left_key;
end
else
SELECT b.boardid, b.name as BoardName, c.categoryid, c.name as CategoryName, f.name as Title, (CASE WHEN access.readaccess = 0 THEN 1 ELSE 0 END) as NoAccess, 
f.forumid, f.parentid, f.[level] as [Level],SIGN(f.right_key-f.left_key - 1) as HasChildren
FROM 
[{databaseSchema}].[{objectQualifier}board] b 
join [{databaseSchema}].[{objectQualifier}category] c on b.boardid = c.boardid  
JOIN [{databaseSchema}].[{objectQualifier}Forum]  f ON (c.CategoryID = f.CategoryID and f.CategoryID = @CategoryID and f.parentid is null)
JOIN [{databaseSchema}].[{objectQualifier}ActiveAccess] access ON (f.forumid = access.forumid and access.userid =@userid)  
ORDER BY f.left_key;
END;
go


CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}forum_ns_getch_anyuser](@boardid int, @categoryid int,@forumid int ,@userid int,@notincluded bit,@immediateonly bit)				 
AS
BEGIN
declare @ici_nid int;

if (@forumid > 0)
begin
SELECT b.boardid, b.name as BoardName, c.categoryid, c.name as CategoryName, n1.name as Title, (CASE WHEN access.readaccess = 0 THEN 1 ELSE 0 END) as NoAccess, 
n1.forumid, n1.parentid, n1.[level] as [Level],SIGN(n1.right_key-n1.left_key - 1) as HasChildren
FROM 
[{databaseSchema}].[{objectQualifier}Board] b  
JOIN [{databaseSchema}].[{objectQualifier}Category] c on b.BoardID = c.BoardID
JOIN [{databaseSchema}].[{objectQualifier}Forum]  n1 on c.CategoryID = n1.CategoryID 
CROSS JOIN
[{databaseSchema}].[{objectQualifier}Forum]  n2   
JOIN [{databaseSchema}].[{objectQualifier}vaccess_combo] access ON (n1.forumid = access.forumid and access.userid =@userid)  
WHERE  (access.readaccess > 0 or (access.readaccess = 0 and (n2.flags & 2) != 2)) and ( n2.ForumID = @ForumID
AND  n1.left_key BETWEEN n2.left_key + cast(@notincluded as int) AND n2.right_key
and (@immediateonly = 0  OR n1.parentid = n2.ForumID)) ORDER BY n1.left_key;
end
else
SELECT b.boardid, b.name as BoardName, c.categoryid, c.name as CategoryName, f.name as Title, (CASE WHEN access.readaccess = 0 THEN 1 ELSE 0 END) as NoAccess, 
f.forumid, f.parentid, f.[level] as [Level],SIGN(f.right_key-f.left_key - 1) as HasChildren
FROM 
[{databaseSchema}].[{objectQualifier}board] b 
join [{databaseSchema}].[{objectQualifier}category] c on b.boardid = c.boardid  
JOIN [{databaseSchema}].[{objectQualifier}Forum]  f ON (c.CategoryID = f.CategoryID and f.CategoryID = @CategoryID and f.parentid is null)
JOIN [{databaseSchema}].[{objectQualifier}vaccess_combo] access ON (f.forumid = access.forumid and access.userid =@userid)  
ORDER BY f.left_key;
END;
go
   
CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}forum_ns_getpath](@forumid int,@parentincluded bit)	
AS		
BEGIN

DECLARE @ici_categoryid int;
SELECT  @ici_categoryid = categoryid
FROM [{databaseSchema}].[{objectQualifier}forum] 
WHERE forumid = @forumid;

 SELECT forumid, parentid, [level]
FROM [{databaseSchema}].[{objectQualifier}Forum] 
where CategoryID = @ici_categoryid 
order by left_key,categoryid, sortorder;
END; 
GO

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}forum_ns_listpath](
						   @ForumID int)
AS 
BEGIN				 
DECLARE @ici_left_key int;
DECLARE @ici_right_key int;
DECLARE @ici_categoryid int;

SELECT @ici_left_key = left_key, @ici_right_key = right_key, @ici_categoryid = categoryid
FROM [{databaseSchema}].[{objectQualifier}forum] where forumid = @ForumID;
SELECT forumid,
	   [name],
	   -- we don't return board and category nodes here
	   ([level] - 2) as [Level] 
	   FROM 
	  [{databaseSchema}].[{objectQualifier}Forum]  
	   WHERE CategoryID = @ici_categoryid and left_key <= @ici_left_key AND right_key >= @ici_right_key ORDER BY left_key;					 
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

CREATE PROCEDURE [{databaseSchema}].[{objectQualifier}forum_ns_recreate_ini]
AS
BEGIN
-- if  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}forum_ns]') and type in (N'U'))
-- DROP TABLE [{databaseSchema}].[{objectQualifier}forum_ns];


-- if (select count(1) from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}forum_ns]') 
-- and type in (N'U')) <= 0 or ( ( select count(1) from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}forum_ns]') and type in (N'U')) > 0 and (select count(1) from [{databaseSchema}].[{objectQualifier}forum_ns]) < (select count(1) from [{databaseSchema}].[{objectQualifier}forum]))
if exists (select top 1 1 from [{databaseSchema}].[{objectQualifier}Forum] where left_key is null or left_key= 0)
exec ('[{databaseSchema}].[{objectQualifier}forum_ns_recreate]');

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}create_or_check_ns_tables]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}create_or_check_ns_tables]

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}lock_ns_tree]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}lock_ns_tree]

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}forum_ns_before_insert_func]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}forum_ns_before_insert_func]

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}forum_ns_after_delete_2_func]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}forum_ns_after_delete_2_func]

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}forum_ns_after_delete_func]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}forum_ns_after_delete_func]

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}forum_ns_before_update_func]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}forum_ns_before_update_func]

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}_tmp_ns_sort]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}_tmp_ns_sort]

IF  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}fillin_or_check_ns_tables]') and type in (N'P', N'PC'))
DROP PROCEDURE [{databaseSchema}].[{objectQualifier}fillin_or_check_ns_tables]

if  exists (select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}forum_ns]') and type in (N'U'))
DROP TABLE [{databaseSchema}].[{objectQualifier}forum_ns];

END;  
go 

exec [{databaseSchema}].[{objectQualifier}forum_ns_recreate_ini];
