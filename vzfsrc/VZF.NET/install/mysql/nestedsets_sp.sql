
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
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}forum_ns_recreate;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}forum_ns_recreate_ini;
--GO


CREATE PROCEDURE {databaseSchema}.{objectQualifier}forum_ns_getchildren(i_boardid int,  i_categoryid int, i_forumid int,  i_notincluded tinyint(1), i_immediateonly tinyint(1))
BEGIN
DECLARE ici_nid int;

if (i_forumid > 0) then
SELECT ns.nid
INTO ici_nid
FROM {databaseSchema}.{objectQualifier}forum_ns ns
WHERE ns.forumid = i_forumid limit 1;

SELECT b.boardid as BoardID, b.name as BoardName, c.categoryid as CategoryID, c.name as CategoryName, f.name as Title,  
n1.forumid as ForumID, n1.parentid as ParentID, n1.`level` as `Level`,{databaseSchema}.{objectQualifier}biginttobool(n1.right_key-n1.left_key - 1) as HasChildren
FROM 
{databaseSchema}.{objectQualifier}Forum f  
JOIN {databaseSchema}.{objectQualifier}Category c on c.CategoryID = f.CategoryID
JOIN {databaseSchema}.{objectQualifier}Board b on b.BoardID = c.BoardID
JOIN {databaseSchema}.{objectQualifier}forum_ns  n1 on (n1.forumid = f.ForumID  and n1.tree= i_CategoryID)
CROSS JOIN
{databaseSchema}.{objectQualifier}forum_ns  n2   WHERE  ( n2.nid = ici_nid
AND  n1.left_key BETWEEN n2.left_key + CAST(i_notincluded as SIGNED) AND n2.right_key
and (i_immediateonly = 0  OR n1.parentid = n2.nid)) ORDER BY n1.left_key;
else
SELECT b.boardid as BoardID, b.name as BoardName, c.categoryid as CategoryID, c.name as CategoryName, f.name as Title,  
n1.forumid as ForumID, n1.parentid as ParentID, n1.`level` as `Level`,{databaseSchema}.{objectQualifier}biginttobool(n1.right_key-n1.left_key - 1) as HasChildren
FROM 
{databaseSchema}.{objectQualifier}Forum f  
JOIN {databaseSchema}.{objectQualifier}Category c on c.CategoryID = f.CategoryID
JOIN {databaseSchema}.{objectQualifier}Board b on b.BoardID = c.BoardID
JOIN {databaseSchema}.{objectQualifier}forum_ns  n1 on (n1.forumid = f.ForumID and f.CategoryID = i_CategoryID and n1.parentid = 0 and n1.tree= i_CategoryID)
ORDER BY n1.left_key;
end if;


END;
--GO

CREATE PROCEDURE {databaseSchema}.{objectQualifier}forum_ns_getch_accgroup(i_boardid int,  i_categoryid int, i_forumid int, i_GroupID int, i_notincluded tinyint(1), i_immediateonly tinyint(1))
BEGIN
DECLARE ici_nid int;

if (i_forumid > 0) then
SELECT ns.nid
INTO ici_nid
FROM {databaseSchema}.{objectQualifier}forum_ns ns
WHERE ns.forumid = i_forumid;

SELECT b.boardid as BoardID, b.name as BoardName, c.categoryid as CategoryID, fa.AccessMaskID, c.name as CategoryName, f.name as Title,  
n1.forumid as ForumID, n1.parentid as ParentID, n1.`level` as `Level`,{databaseSchema}.{objectQualifier}biginttobool(n1.right_key-n1.left_key - 1) as HasChildren
FROM 
{databaseSchema}.{objectQualifier}Forum f  
join {databaseSchema}.{objectQualifier}ForumAccess fa  on (fa.ForumID = f.ForumID and fa.GroupID = i_GroupID)
JOIN {databaseSchema}.{objectQualifier}Category c on c.CategoryID = f.CategoryID
JOIN {databaseSchema}.{objectQualifier}Board b on b.BoardID = c.BoardID
JOIN {databaseSchema}.{objectQualifier}forum_ns  n1 on (n1.forumid = f.ForumID  and n1.tree= i_CategoryID)
CROSS JOIN
{databaseSchema}.{objectQualifier}forum_ns  n2   WHERE  ( n2.nid = ici_nid
AND  n1.left_key BETWEEN n2.left_key + CAST(i_notincluded as SIGNED) AND n2.right_key
and (i_immediateonly = 0  OR n1.parentid = n2.nid)) ORDER BY n1.left_key;
else
SELECT b.boardid as BoardID, b.name as BoardName, c.categoryid as CategoryID, fa.AccessMaskID, c.name as CategoryName, f.name as Title,  
n1.forumid as ForumID, n1.parentid as ParentID, n1.`level` as `Level`,{databaseSchema}.{objectQualifier}biginttobool(n1.right_key-n1.left_key - 1) as HasChildren
FROM 
{databaseSchema}.{objectQualifier}Forum f  
join {databaseSchema}.{objectQualifier}ForumAccess fa  on (fa.ForumID = f.ForumID and fa.GroupID = i_GroupID)
JOIN {databaseSchema}.{objectQualifier}Category c on c.CategoryID = f.CategoryID
JOIN {databaseSchema}.{objectQualifier}Board b on b.BoardID = c.BoardID
JOIN {databaseSchema}.{objectQualifier}forum_ns  n1 on (n1.forumid = f.ForumID and f.CategoryID = i_CategoryID and n1.parentid = 0 and n1.tree= i_CategoryID)
ORDER BY n1.left_key;
end if;
END;
--GO

CREATE PROCEDURE {databaseSchema}.{objectQualifier}forum_ns_getch_actuser(i_boardid int,  i_categoryid int, i_forumid int , i_userid int, i_notincluded tinyint(1), i_immediateonly tinyint(1))
BEGIN
declare ici_nid integer; 

SELECT ns.nid
INTO ici_nid
FROM {databaseSchema}.{objectQualifier}forum_ns ns
WHERE ns.forumid = 0 and ns.categoryid = 0 and boardid = i_boardid;

if (i_forumid > 0) then
SELECT ns.nid
INTO ici_nid
FROM {databaseSchema}.{objectQualifier}forum_ns ns
WHERE ns.forumid = i_forumid;

SELECT b.boardid, b.name as BoardName, c.categoryid, c.name as CategoryName, f.name as Title, (access.readaccess = 0) as NoAccess, 
n1.forumid, n1.parentid, n1.level,{databaseSchema}.{objectQualifier}biginttobool(n1.right_key-n1.left_key - 1) as HasChildren
FROM {databaseSchema}.{objectQualifier}forum f 
JOIN {databaseSchema}.{objectQualifier}category c on c.categoryid = f.categoryid
JOIN {databaseSchema}.{objectQualifier}board b on b.boardid = c.boardid 
JOIN {databaseSchema}.{objectQualifier}activeaccess access ON (f.forumid = access.forumid and access.userid = i_userid)  
JOIN {databaseSchema}.{objectQualifier}forum_ns  n1 on (n1.forumid = f.ForumID  and n1.tree= i_CategoryID)
CROSS JOIN
{databaseSchema}.{objectQualifier}forum_ns  n2   WHERE  (access.readaccess > 0 or (access.readaccess = 0 and (f.flags & 2) != 2)) and ( n2.nid = ici_nid
AND  n1.left_key BETWEEN n2.left_key + cast(i_notincluded as signed) AND n2.right_key
and (i_immediateonly = 0  OR n1.parentid = n2.nid)) ORDER BY n1.left_key;
else
SELECT b.boardid, b.name as BoardName, c.categoryid, c.name as CategoryName, f.name as Title, (access.readaccess = 0) as NoAccess, 
n1.forumid, n1.parentid, n1.level,{databaseSchema}.{objectQualifier}biginttobool(n1.right_key-n1.left_key - 1) as HasChildren
FROM {databaseSchema}.{objectQualifier}forum f 
JOIN {databaseSchema}.{objectQualifier}category c on c.categoryid = f.categoryid
JOIN {databaseSchema}.{objectQualifier}board b on b.boardid = c.boardid 
JOIN {databaseSchema}.{objectQualifier}activeaccess access ON (f.forumid = access.forumid and access.userid = i_userid)  
JOIN {databaseSchema}.{objectQualifier}forum_ns  n1 on (n1.forumid = f.ForumID and f.CategoryID = i_CategoryID and n1.parentid = 0 and n1.tree= i_CategoryID)
ORDER BY n1.left_key;
end if;
END;
--GO

CREATE PROCEDURE {databaseSchema}.{objectQualifier}forum_ns_getch_anyuser(i_boardid int,  i_categoryid int, i_forumid int , i_userid int, i_notincluded tinyint(1), i_immediateonly tinyint(1))				 
BEGIN
declare ici_nid int;

if (i_forumid > 0) then
SELECT ns.nid
INTO ici_nid
FROM {databaseSchema}.{objectQualifier}forum_ns ns
WHERE ns.forumid = i_forumid;

SELECT b.boardid, b.name as BoardName, c.categoryid, c.name as CategoryName, f.name as Title, (access.readaccess = 0) as NoAccess, 
n1.forumid, n1.parentid, n1.`level`,{databaseSchema}.{objectQualifier}biginttobool(n1.right_key-n1.left_key - 1) as HasChildren
FROM {databaseSchema}.{objectQualifier}forum f 
JOIN {databaseSchema}.{objectQualifier}category c on c.categoryid = f.categoryid 
JOIN {databaseSchema}.{objectQualifier}board b on b.boardid = c.boardid 
JOIN {databaseSchema}.{objectQualifier}vaccess_combo access ON (f.forumid = access.ForumID and access.UserID = i_userid)  
JOIN {databaseSchema}.{objectQualifier}forum_ns  n1 on (n1.forumid = f.ForumID  and n1.tree= i_CategoryID)
CROSS JOIN
{databaseSchema}.{objectQualifier}forum_ns  n2   
WHERE (access.readaccess > 0 or (access.readaccess = 0 and (f.flags & 2) != 2)) and ( n2.nid = ici_nid
AND  n1.left_key BETWEEN n2.left_key + CAST(i_notincluded as SIGNED) AND n2.right_key
and (i_immediateonly = 0  OR n1.parentid = n2.nid)) ORDER BY n1.left_key;

else
SELECT b.boardid, b.name as BoardName, c.categoryid, c.name as CategoryName, f.name as Title, (access.readaccess = 0) as NoAccess, 
n1.forumid, n1.parentid, n1.`level`,{databaseSchema}.{objectQualifier}biginttobool(n1.right_key-n1.left_key - 1) as HasChildren
FROM {databaseSchema}.{objectQualifier}forum f 
JOIN {databaseSchema}.{objectQualifier}category c on c.categoryid = f.categoryid 
JOIN {databaseSchema}.{objectQualifier}board b on b.boardid = c.boardid 
JOIN {databaseSchema}.{objectQualifier}vaccess_combo access ON (f.forumid = access.ForumID and access.UserID = i_userid)  
JOIN {databaseSchema}.{objectQualifier}forum_ns  n1 on (n1.forumid = f.ForumID and f.CategoryID = i_CategoryID and n1.parentid = 0 and n1.tree= i_CategoryID)
ORDER BY n1.left_key;
end if;
END;
--GO
   
CREATE PROCEDURE {databaseSchema}.{objectQualifier}forum_ns_getpath(i_forumid int, i_parentincluded tinyint(1))			
BEGIN
declare ici_nid int;
DECLARE ici_categoryid integer;
SELECT ns.nid,ns.categoryid
INTO ici_nid,ici_categoryid
FROM {databaseSchema}.{objectQualifier}forum_ns ns
WHERE ns.forumid = i_forumid;

 SELECT n1.forumid, n1.parentid, n1.level
FROM {databaseSchema}.{objectQualifier}forum f
join {databaseSchema}.{objectQualifier}forum_ns n1 
on (f.forumid = ns.forumid and ns.tree= ici_categoryid)
order by n1.left_key,f.categoryid, f.sortorder;
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
	   (ns.`level` - 2) as `Level` 
	   FROM {databaseSchema}.{objectQualifier}forum_ns ns 
	   JOIN {databaseSchema}.{objectQualifier}forum f on (f.forumid = ns.forumid and ns.tree= ici_categoryid)
	   WHERE ns.left_key <= ici_left_key AND ns.right_key >= ici_right_key 
	   ORDER BY ns.left_key;					 
END;
--GO

-- Initialize all this
CREATE PROCEDURE {databaseSchema}.{objectQualifier}forum_ns_recreate()
BEGIN
CALL {databaseSchema}.{objectQualifier}create_or_check_ns_tables();
CALL {databaseSchema}.{objectQualifier}fillin_or_check_ns_tables();
END;  
--GO 
CREATE PROCEDURE {databaseSchema}.{objectQualifier}forum_ns_recreate_ini()
BEGIN
DECLARE i int;
DECLARE j int;

if ((SELECT COUNT(*) FROM information_schema.TABLES 
WHERE CONCAT(table_schema,".",table_name)="{databaseSchema}.{objectQualifier}forum_ns") > 0) then 
if ((select count(1) from {databaseSchema}.{objectQualifier}forum_ns) <  
(select count(1) from {databaseSchema}.{objectQualifier}forum)) then 
CALL {databaseSchema}.{objectQualifier}forum_ns_recreate();
end if;
end if;
END;  
--GO 
CALL {databaseSchema}.{objectQualifier}forum_ns_recreate_ini();
--GO
