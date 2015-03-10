
/* ******************************************************************************************************************************
*********************************************************************************************************************************
SELECT FUNCTIONS
*********************************************************************************************************************************
******************************************************************************************************************************** */
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}forum_ns_getchildren;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}forum_ns_getch_actuser;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}forum_ns_getch_anyuser;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}forum_ns_getch_accgroup;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}forum_ns_getpath;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}forum_ns_listpath;
--GO


CREATE PROCEDURE {databaseSchema}.{objectQualifier}forum_ns_getchildren(i_boardid int,  i_categoryid int, i_forumid int,  i_notincluded tinyint(1), i_immediateonly tinyint(1))
BEGIN
DECLARE ici_nid int;

if (i_forumid > 0) then
SELECT b.boardid, b.name as BoardName, c.categoryid, c.name as CategoryName, n1.name as Title,  
n1.forumid, n1.parentid, n1.`level` as `Level`,{databaseSchema}.{objectQualifier}biginttobool(n1.right_key-n1.left_key - 1) as HasChildren
FROM 
{databaseSchema}.{objectQualifier}board b 
join {databaseSchema}.{objectQualifier}category c on b.boardid = c.boardid
join {databaseSchema}.{objectQualifier}forum n1 on c.categoryid = n1.categoryid 
CROSS JOIN
{databaseSchema}.{objectQualifier}forum  n2   WHERE  n2.ForumID = i_ForumID
AND  n1.left_key BETWEEN n2.left_key + cast(i_notincluded as SIGNED) AND n2.right_key
and (i_immediateonly = 0  OR n1.ParentID = n2.ForumID) ORDER BY n1.left_key;
else
SELECT b.boardid, b.name as BoardName, c.categoryid, c.name as CategoryName, f.name as Title,  
f.forumid, f.parentid, f.`level` as `Level`,{databaseSchema}.{objectQualifier}biginttobool(f.right_key-f.left_key - 1) as HasChildren
FROM 
{databaseSchema}.{objectQualifier}board b 
join {databaseSchema}.{objectQualifier}category c on b.boardid = c.boardid  
JOIN {databaseSchema}.{objectQualifier}Forum  f ON (c.categoryid = f.categoryid and f.CategoryID = i_CategoryID and f.parentid is null)
ORDER BY f.left_key;
end if;
END;
--GO

CREATE PROCEDURE {databaseSchema}.{objectQualifier}forum_ns_getch_accgroup(i_boardid int,  i_categoryid int, i_forumid int, i_GroupID int, i_notincluded tinyint(1), i_immediateonly tinyint(1))
BEGIN
DECLARE ici_nid int;

if (i_forumid > 0) then
SELECT b.BoardID, b.Name as BoardName, c.CategoryID, c.name as CategoryName, n1.name as Title, fa.AccessMaskID, 
n1.ForumID, n1.ParentID, n1.`level` as `Level`,{databaseSchema}.{objectQualifier}biginttobool(n1.right_key-n1.left_key - 1) as HasChildren
FROM 
{databaseSchema}.{objectQualifier}Board b  
JOIN {databaseSchema}.{objectQualifier}Category c on b.BoardID = c.BoardID
JOIN {databaseSchema}.{objectQualifier}Forum  n1 on c.CategoryID = n1.CategoryID 
CROSS JOIN
{databaseSchema}.{objectQualifier}Forum  n2  
join {databaseSchema}.{objectQualifier}ForumAccess fa  on (fa.ForumID = n1.ForumID and fa.GroupID = i_GroupID) 
WHERE   n2.ForumID = i_ForumID
AND  n1.left_key BETWEEN n2.left_key + cast(i_notincluded as SIGNED) AND n2.right_key
and (i_immediateonly = 0  OR n1.ParentID = n2.ForumID) ORDER BY n1.left_key;
else
SELECT b.BoardID, b.Name as BoardName, c.CategoryID, c.name as CategoryName, f.Name as Title, fa.AccessMaskID, 
f.ForumID, f.ParentID, f.`level` as `Level`,{databaseSchema}.{objectQualifier}biginttobool(f.right_key-f.left_key - 1) as HasChildren
FROM 
{databaseSchema}.{objectQualifier}board b 
join {databaseSchema}.{objectQualifier}category c on b.boardid = c.boardid  
JOIN {databaseSchema}.{objectQualifier}Forum  f ON (c.categoryid = f.categoryid and f.CategoryID = i_CategoryID and f.parentid is null)
join {databaseSchema}.{objectQualifier}ForumAccess fa  on (fa.ForumID = f.ForumID and fa.GroupID = i_GroupID)
ORDER BY f.left_key;
end if;
END;
--GO

CREATE PROCEDURE {databaseSchema}.{objectQualifier}forum_ns_getch_actuser(i_boardid int,  i_categoryid int, i_forumid int , i_userid int, i_notincluded tinyint(1), i_immediateonly tinyint(1))
BEGIN
if (i_forumid > 0) then

SELECT b.boardid, b.name as BoardName, c.categoryid, c.name as CategoryName, n1.name as Title, (CASE WHEN access.readaccess = 0 THEN 1 ELSE 0 END) as NoAccess, 
n1.forumid, n1.parentid, n1.`level` as `Level`,{databaseSchema}.{objectQualifier}biginttobool(n1.right_key-n1.left_key - 1) as HasChildren
FROM 
{databaseSchema}.{objectQualifier}Board b  
JOIN {databaseSchema}.{objectQualifier}Category c on b.BoardID = c.BoardID
JOIN {databaseSchema}.{objectQualifier}Forum  n1 on c.CategoryID = n1.CategoryID 
CROSS JOIN
{databaseSchema}.{objectQualifier}Forum  n2   
JOIN {databaseSchema}.{objectQualifier}activeaccess access ON (n1.forumid = access.forumid and access.userid =i_userid)  
WHERE  (access.readaccess > 0 or (access.readaccess = 0 and (n2.flags & 2) != 2)) and ( n2.ForumID = i_ForumID
AND  n1.left_key BETWEEN n2.left_key + cast(i_notincluded as SIGNED) AND n2.right_key
and (i_immediateonly = 0  OR n1.parentid = n2.ForumID)) ORDER BY n1.left_key;
else
SELECT b.boardid, b.name as BoardName, c.categoryid, c.name as CategoryName, f.name as Title, (CASE WHEN access.readaccess = 0 THEN 1 ELSE 0 END) as NoAccess, 
f.forumid, f.parentid, f.`level` as `Level`,{databaseSchema}.{objectQualifier}biginttobool(f.right_key-f.left_key - 1) as HasChildren
FROM 
{databaseSchema}.{objectQualifier}board b 
join {databaseSchema}.{objectQualifier}category c on b.boardid = c.boardid  
JOIN {databaseSchema}.{objectQualifier}Forum  f ON (c.CategoryID = f.CategoryID and f.CategoryID = i_CategoryID and f.parentid is null)
JOIN {databaseSchema}.{objectQualifier}ActiveAccess access ON (f.forumid = access.forumid and access.userid = i_userid) 
WHERE  (access.readaccess > 0 or (access.readaccess = 0 and (f.flags & 2) != 2))
ORDER BY f.left_key;
end if;
END;
--GO

CREATE PROCEDURE {databaseSchema}.{objectQualifier}forum_ns_getch_anyuser(i_boardid int,  i_categoryid int, i_forumid int , i_userid int, i_notincluded tinyint(1), i_immediateonly tinyint(1))				 
BEGIN
if (i_forumid > 0) then
SELECT b.boardid, b.name as BoardName, c.categoryid, c.name as CategoryName, n1.name as Title, (CASE WHEN access.readaccess = 0 THEN 1 ELSE 0 END) as NoAccess, 
n1.forumid, n1.parentid, n1.`level` as `Level`,{databaseSchema}.{objectQualifier}biginttobool(n1.right_key-n1.left_key - 1) as HasChildren
FROM 
{databaseSchema}.{objectQualifier}Board b  
JOIN {databaseSchema}.{objectQualifier}Category c on b.BoardID = c.BoardID
JOIN {databaseSchema}.{objectQualifier}Forum  n1 on c.CategoryID = n1.CategoryID 
CROSS JOIN
{databaseSchema}.{objectQualifier}Forum  n2   
JOIN {databaseSchema}.{objectQualifier}vaccess_combo access ON (n1.forumid = access.forumid and access.userid = i_userid)  
WHERE  (access.readaccess > 0 or (access.readaccess = 0 and (n2.flags & 2) != 2)) and ( n2.ForumID = i_ForumID
AND  n1.left_key BETWEEN n2.left_key + cast(i_notincluded as SIGNED) AND n2.right_key
and (i_immediateonly = 0  OR n1.parentid = n2.ForumID)) ORDER BY n1.left_key;

else
SELECT b.boardid, b.name as BoardName, c.categoryid, c.name as CategoryName, f.name as Title, (CASE WHEN access.readaccess = 0 THEN 1 ELSE 0 END) as NoAccess, 
f.forumid, f.parentid, f.`level` as `Level`,{databaseSchema}.{objectQualifier}biginttobool(f.right_key-f.left_key - 1) as HasChildren
FROM 
{databaseSchema}.{objectQualifier}board b 
join {databaseSchema}.{objectQualifier}category c on b.boardid = c.boardid  
JOIN {databaseSchema}.{objectQualifier}Forum  f ON (c.CategoryID = f.CategoryID and f.CategoryID = i_CategoryID and f.parentid is null)
JOIN {databaseSchema}.{objectQualifier}vaccess_combo access ON (f.forumid = access.forumid and access.userid =i_userid)
WHERE  (access.readaccess > 0 or (access.readaccess = 0 and (f.flags & 2) != 2))  
ORDER BY f.left_key;
end if;
END;
--GO
   
CREATE PROCEDURE {databaseSchema}.{objectQualifier}forum_ns_getpath(i_forumid int, i_parentincluded tinyint(1))			
BEGIN
DECLARE ici_categoryid int;
SELECT  categoryid
into ici_categoryid
FROM {databaseSchema}.{objectQualifier}forum 
WHERE forumid = i_forumid;


 SELECT f.forumid, f.parentid, f.level
FROM {databaseSchema}.{objectQualifier}forum f
where f.CategoryID = ici_categoryid
order by f.left_key,f.categoryid, f.sortorder;
END; 
--GO

CREATE PROCEDURE {databaseSchema}.{objectQualifier}forum_ns_listpath(
						   i_ForumID integer) 
BEGIN				 
DECLARE ici_left_key integer;
DECLARE ici_right_key integer;
DECLARE ici_categoryid integer;

SELECT left_key, right_key, categoryid
into  ici_left_key, ici_right_key, ici_categoryid
FROM {databaseSchema}.{objectQualifier}forum_ns where ForumID = i_ForumID;

SELECT f.ForumID,
	   f.Name,
	   -- we don't return board and category nodes here
	   (f.`level` - 2) as `Level` 
	   FROM   {databaseSchema}.{objectQualifier}forum f  
	   WHERE f.CategoryID = ici_categoryid and f.left_key <= ici_left_key AND f.right_key >= ici_right_key 
	   ORDER BY f.left_key;					 
END;
--GO

