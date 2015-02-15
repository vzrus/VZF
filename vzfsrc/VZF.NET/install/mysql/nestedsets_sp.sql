
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
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}forum_ns_getpath;
--GO
DROP PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}forum_ns_recreate;
--GO

CREATE PROCEDURE {databaseSchema}.{objectQualifier}forum_ns_getchildren(i_boardid int,  i_categoryid int, i_forumid int,  i_notincluded tinyint(1), i_immediateonly tinyint(1))
BEGIN
DECLARE ici_nid int;
if (i_forumid > 0) then
SELECT ns.nid
INTO ici_nid
FROM {databaseSchema}.{objectQualifier}forum_ns ns
WHERE ns.forumid = i_forumid limit 1;
elseif  (i_categoryid > 0) then
SELECT ns.nid
INTO ici_nid
FROM {databaseSchema}.{objectQualifier}forum_ns ns
WHERE ns.forumid = 0 and ns.categoryid = i_categoryid limit 1;
else
SELECT ns.nid
INTO ici_nid
FROM {databaseSchema}.{objectQualifier}forum_ns ns
WHERE ns.forumid = 0 and ns.categoryid = 0 and boardid = i_boardid limit 1;
end if;

SELECT n1.forumid as ForumID, n1.parentid as ParentID, n1.level as `Level`
FROM {databaseSchema}.{objectQualifier}forum_ns  n1,
{databaseSchema}.{objectQualifier}forum_ns  n2   WHERE  ( n2.nid = ici_nid
AND  n1.left_key BETWEEN n2.left_key + CAST(i_notincluded as SIGNED) AND n2.right_key
and (i_immediateonly = 0  OR n1.parentid = n2.nid)) ORDER BY n1.left_key;
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
elseif  (i_categoryid > 0) then
SELECT ns.nid
INTO ici_nid
FROM {databaseSchema}.{objectQualifier}forum_ns ns
WHERE ns.forumid = 0 and ns.categoryid = i_categoryid;
else
SELECT ns.nid
INTO ici_nid
FROM {databaseSchema}.{objectQualifier}forum_ns ns
WHERE ns.forumid = 0 and ns.categoryid = 0 and boardid = i_boardid;
end if;

SELECT b.boardid, b.name as BoardName, c.categoryid, c.name as CategoryName, f.name as Title, (access.readaccess = 0) as NoAccess, 
n1.forumid, n1.parentid, n1.level,(n1.right_key-n1.left_key > 1) as HasChildren
FROM {databaseSchema}.{objectQualifier}forum f 
JOIN {databaseSchema}.{objectQualifier}category c on c.categoryid = f.categoryid
JOIN {databaseSchema}.{objectQualifier}board b on b.boardid = c.boardid 
JOIN {databaseSchema}.{objectQualifier}activeaccess access ON (f.forumid = access.forumid and access.userid = i_userid)  
JOIN {databaseSchema}.{objectQualifier}forum_ns  n1 ON (n1.forumid = f.forumid)
CROSS JOIN
{databaseSchema}.{objectQualifier}forum_ns  n2   WHERE  (access.readaccess > 0 or (access.readaccess = 0 and (f.flags & 2) != 2)) and ( n2.nid = ici_nid
AND  n1.left_key BETWEEN n2.left_key + cast(i_notincluded as signed) AND n2.right_key
and (i_immediateonly = 0  OR n1.parentid = n2.nid)) ORDER BY n1.left_key;
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
elseif  (i_categoryid > 0) then
SELECT ns.nid
INTO ici_nid
FROM {databaseSchema}.{objectQualifier}forum_ns ns
WHERE ns.forumid = 0 and ns.categoryid = i_categoryid;
else
SELECT ns.nid
INTO ici_nid
FROM {databaseSchema}.{objectQualifier}forum_ns ns
WHERE ns.forumid = 0 and ns.categoryid = 0 and boardid = i_boardid;
end if;

SELECT b.boardid, b.name as BoardName, c.categoryid, c.name as CategoryName, f.name as Title, (access.readaccess = 0) as NoAccess, 
n1.forumid, n1.parentid, n1.level,(n1.right_key-n1.left_key > 1) as HasChildren
FROM {databaseSchema}.{objectQualifier}forum f 
JOIN {databaseSchema}.{objectQualifier}category c on c.categoryid = f.categoryid 
JOIN {databaseSchema}.{objectQualifier}board b on b.boardid = c.boardid 
JOIN {databaseSchema}.{objectQualifier}vaccess_combo access ON (f.forumid = access.ForumID and access.UserID = i_userid)  
JOIN {databaseSchema}.{objectQualifier}forum_ns  n1 ON n1.forumid = f.forumid
CROSS JOIN
{databaseSchema}.{objectQualifier}forum_ns  n2   
WHERE (access.readaccess > 0 or (access.readaccess = 0 and (f.flags & 2) != 2)) and ( n2.nid = ici_nid
AND  n1.left_key BETWEEN n2.left_key + CAST(i_notincluded as SIGNED) AND n2.right_key
and (i_immediateonly = 0  OR n1.parentid = n2.nid)) ORDER BY n1.left_key;
END;
--GO
   
CREATE PROCEDURE {databaseSchema}.{objectQualifier}forum_ns_getpath(i_forumid int, i_parentincluded tinyint(1))			
BEGIN
declare ici_nid int;

SELECT ns.nid
INTO ici_nid
FROM {databaseSchema}.{objectQualifier}forum_ns ns
WHERE ns.forumid = i_forumid;

 SELECT n1.forumid, n1.parentid, n1.level
FROM {databaseSchema}.{objectQualifier}forum f
join {databaseSchema}.{objectQualifier}forum_ns n1 
on n1.forumid = f.forumid 
order by n1.left_key,f.categoryid, f.sortorder;
END; 
--GO

-- Initialize all this
CREATE PROCEDURE {databaseSchema}.{objectQualifier}forum_ns_recreate()
BEGIN
CALL {databaseSchema}.{objectQualifier}create_or_check_ns_tables();
CALL {databaseSchema}.{objectQualifier}fillin_or_check_ns_tables();
END;  
--GO 
CALL {databaseSchema}.{objectQualifier}forum_ns_recreate();
-- GO
