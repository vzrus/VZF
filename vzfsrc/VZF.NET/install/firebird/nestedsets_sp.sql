/* ******************************************************************************************************************************
*********************************************************************************************************************************
SELECT FUNCTIONS
*********************************************************************************************************************************
******************************************************************************************************************************** */


CREATE PROCEDURE {objectQualifier}forum_ns_getchildren(i_boardid integer,  i_categoryid integer, i_forumid integer,  i_notincluded bool, i_immediateonly bool)
				   RETURNS (
"BoardID" integer,
"BoardName" varchar(255),
"CategoryID" integer,
"CategoryName" varchar(255),
"Title" varchar(255),
"ForumID"  integer,
"ParentID" integer,
"Level" integer,
"HasChildren"  integer
) AS
DECLARE ici_nid integer;
BEGIN
if (:i_forumid > 0) then
begin
SELECT ns.nid
FROM {objectQualifier}forum_ns ns
WHERE ns.forumid = :i_forumid
INTO :ici_nid;

FOR SELECT
b.boardid, b.name as BoardName, c.categoryid, c.name as CategoryName, 
f.name as Title,  
n1.forumid, n1.parentid, n1."LEVEL" ,CAST(SIGN(n1.right_key-n1.left_key - 1) as smallint)
FROM 
{objectQualifier}forum f  
JOIN {objectQualifier}category c on c.categoryid = f.categoryid
JOIN {objectQualifier}board b on b.boardid = c.boardid
JOIN {objectQualifier}forum_ns n1 ON (n1.forumid = f.forumid and n1.tree = :i_CategoryID)
cross join
{objectQualifier}forum_ns  n2 
WHERE  (n2.nid = :ici_nid
 AND  (n1.left_key BETWEEN n2.left_key + cast(:i_notincluded as integer) AND n2.right_key)
 and (:i_immediateonly = 0  OR n1.parentid = n2.nid)) ORDER BY n1.left_key
INTO
:"BoardID",
:"BoardName",
:"CategoryID",
:"CategoryName",
:"Title",
:"ForumID",
:"ParentID",
:"Level",
:"HasChildren"
DO SUSPEND;
end
else
begin
FOR SELECT
b.boardid, b.name as BoardName, c.categoryid, c.name as CategoryName, 
f.name as Title,  
n1.forumid, n1.parentid, n1."LEVEL" ,CAST(SIGN(n1.right_key-n1.left_key - 1) as smallint)
FROM 
{objectQualifier}forum f  
JOIN {objectQualifier}category c on c.categoryid = f.categoryid
JOIN {objectQualifier}board b on b.boardid = c.boardid
JOIN {objectQualifier}forum_ns n1 ON (n1.forumid = f.forumid and f.CategoryID = :i_CategoryID and n1.parentid = 0 and n1.tree = :i_CategoryID)
ORDER BY n1.left_key
INTO
:"BoardID",
:"BoardName",
:"CategoryID",
:"CategoryName",
:"Title",
:"ForumID",
:"ParentID",
:"Level",
:"HasChildren"
DO SUSPEND;
end
END;
--GO

CREATE PROCEDURE {objectQualifier}FORUM_NS_GETCH_ACCGROUP(i_boardid integer,  i_categoryid integer, i_forumid integer, I_GROUPID INTEGER,  i_notincluded bool, i_immediateonly bool)
				   RETURNS (
"BoardID" integer,
"BoardName" varchar(255),
"CategoryID" integer,
"CategoryName" varchar(255),
"Title" varchar(255),
"AccessMaskID" INTEGER,
"ForumID"  integer,
"ParentID" integer,
"Level" integer,
"HasChildren"  integer
) AS
DECLARE ici_nid integer;
BEGIN
if (:i_forumid > 0) then
begin
SELECT ns.nid
FROM {objectQualifier}forum_ns ns
WHERE ns.forumid = :i_forumid
INTO :ici_nid;
FOR SELECT
b.boardid, b.name as BoardName, c.categoryid, c.name as CategoryName, f.name as Title, fa.AccessMaskID,  
n1.forumid, n1.parentid, n1."LEVEL" ,CAST(SIGN(n1.right_key-n1.left_key - 1) as smallint)
FROM 
{objectQualifier}forum f  
join {objectQualifier}ForumAccess fa  on (fa.ForumID = f.ForumID and fa.GroupID = :I_GROUPID)
JOIN {objectQualifier}category c on c.categoryid = f.categoryid
JOIN {objectQualifier}board b on b.boardid = c.boardid
JOIN {objectQualifier}forum_ns n1 ON (n1.forumid = f.forumid and n1.tree = :i_CategoryID)
cross join
{objectQualifier}forum_ns  n2 
 WHERE  ( n2.nid = :ici_nid
 AND  n1.left_key BETWEEN n2.left_key + cast(:i_notincluded as integer) AND n2.right_key
 and (:i_immediateonly = 0  OR n1.parentid = n2.nid)) ORDER BY n1.left_key
INTO
:"BoardID",
:"BoardName",
:"CategoryID",
:"CategoryName",
:"Title",
:"AccessMaskID",
:"ForumID",
:"ParentID",
:"Level",
:"HasChildren"
DO SUSPEND;
end
else
begin
FOR SELECT
b.boardid, b.name as BoardName, c.categoryid, c.name as CategoryName, f.name as Title, fa.AccessMaskID,  
n1.forumid, n1.parentid, n1."LEVEL" ,CAST(SIGN(n1.right_key-n1.left_key - 1) as smallint)
FROM 
{objectQualifier}forum f  
join {objectQualifier}ForumAccess fa  on (fa.ForumID = f.ForumID and fa.GroupID = :I_GROUPID)
JOIN {objectQualifier}category c on c.categoryid = f.categoryid
JOIN {objectQualifier}board b on b.boardid = c.boardid
JOIN {objectQualifier}forum_ns n1 on (n1.forumid = f.forumid and f.CategoryID = :i_CategoryID and n1.parentid = 0 and n1.tree = :i_CategoryID)
 ORDER BY n1.left_key
INTO
:"BoardID",
:"BoardName",
:"CategoryID",
:"CategoryName",
:"Title",
:"AccessMaskID",
:"ForumID",
:"ParentID",
:"Level",
:"HasChildren"
DO SUSPEND;
end

END;
--GO

CREATE PROCEDURE {objectQualifier}forum_ns_getch_actuser(i_boardid integer,  i_categoryid integer, i_forumid integer , i_userid integer, i_notincluded bool, i_immediateonly bool)
				  RETURNS (
"CategoryID"  integer,
"CategoryName" varchar(255),
"Title" varchar(255),
"NoAccess" bool,
"ForumID"  integer,
"ParentID" integer,
"Level" integer,
"HasChildren"  integer
) AS
DECLARE ici_nid integer; 
BEGIN


if (:i_forumid > 0) then
begin
SELECT ns.nid
FROM {objectQualifier}forum_ns ns
WHERE ns.forumid = :i_forumid
INTO :ici_nid;
FOR SELECT c.categoryid, c.name , f.name,(CASE WHEN access.readaccess = 0 THEN 1 ELSE 0 END) , n1.forumid, n1.parentid, n1."LEVEL" ,CAST(SIGN(n1.right_key-n1.left_key - 1) as smallint)
FROM {objectQualifier}forum f 
JOIN {objectQualifier}category c on c.categoryid = f.categoryid 
JOIN {objectQualifier}activeaccess access ON (f.forumid = access.forumid and access.userid = :i_userid)  
JOIN {objectQualifier}forum_ns  n1 ON (n1.forumid = f.forumid and n1.tree = :i_CategoryID)
CROSS JOIN
{objectQualifier}forum_ns  n2   WHERE ( n2.nid = :ici_nid
AND  n1.left_key BETWEEN n2.left_key + CAST(:i_notincluded as integer) AND n2.right_key
and (:i_immediateonly = 0  OR n1.parentid = n2.nid)) ORDER BY n1.left_key
INTO
:"CategoryID",
:"CategoryName",
:"Title",
:"NoAccess",
:"ForumID",
:"ParentID",
:"Level",
:"HasChildren"
DO SUSPEND;
end
else
begin
FOR SELECT c.categoryid, c.name , f.name,(CASE WHEN access.readaccess = 0 THEN 1 ELSE 0 END) , n1.forumid, n1.parentid, n1."LEVEL" ,CAST(SIGN(n1.right_key-n1.left_key - 1) as smallint)
FROM {objectQualifier}forum f 
JOIN {objectQualifier}category c on c.categoryid = f.categoryid 
JOIN {objectQualifier}activeaccess access ON (f.forumid = access.forumid and access.userid = :i_userid)  
JOIN {objectQualifier}forum_ns  n1 on (n1.forumid = f.forumid and f.CategoryID = :i_CategoryID and n1.parentid = 0 and n1.tree = :i_CategoryID)
ORDER BY n1.left_key
INTO
:"CategoryID",
:"CategoryName",
:"Title",
:"NoAccess",
:"ForumID",
:"ParentID",
:"Level",
:"HasChildren"
DO SUSPEND;
end
END;
--GO

CREATE PROCEDURE {objectQualifier}forum_ns_getch_anyuser(i_boardid integer,  i_categoryid integer, i_forumid integer , i_userid integer, i_notincluded bool, i_immediateonly bool)
				   RETURNS (
"BoardID" integer,
"BoardName" varchar(255),
"CategoryID"  integer,
"CategoryName" varchar(255),
"Title" varchar(255),
"NoAccess" bool,
"ForumID"  integer,
"ParentID" integer,
"Level" integer,
"HasChildren"  integer
)
AS
DECLARE ici_nid integer;
BEGIN
if (:i_forumid > 0) then
begin
SELECT ns.nid
FROM {objectQualifier}forum_ns ns
WHERE ns.forumid = :i_forumid
INTO :ici_nid;
FOR SELECT b.boardid, b.name, c.categoryid, c.name , f.name,
(CASE WHEN access.readaccess = 0 THEN 1 ELSE 0 END), n1.forumid, n1.parentid, n1."LEVEL" ,CAST(SIGN(n1.right_key-n1.left_key - 1) as smallint)
FROM {objectQualifier}forum f 
JOIN {objectQualifier}category c on c.categoryid = f.categoryid 
JOIN {objectQualifier}board b on b.boardid = c.boardid 
JOIN {objectQualifier}vaccess access ON (f.forumid = access.ForumID and access.UserID = :i_userid)  
JOIN {objectQualifier}forum_ns  n1 ON (n1.forumid = f.forumid and n1.tree = :i_CategoryID)
CROSS JOIN
{objectQualifier}forum_ns  n2   WHERE  ( n2.nid = :ici_nid
AND  n1.left_key BETWEEN n2.left_key + CAST(:i_notincluded AS integer) AND n2.right_key
and (:i_immediateonly = 0  OR n1.parentid = n2.nid)) ORDER BY n1.left_key
INTO
:"BoardID",
:"BoardName",
:"CategoryID",
:"CategoryName",
:"Title",
:"NoAccess",
:"ForumID",
:"ParentID",
:"Level",
:"HasChildren"
DO 
SUSPEND;
end
else
begin
FOR SELECT b.boardid, b.name, c.categoryid, c.name , f.name,
(CASE WHEN access.readaccess = 0 THEN 1 ELSE 0 END), n1.forumid, n1.parentid, n1."LEVEL" ,CAST(SIGN(n1.right_key-n1.left_key - 1) as smallint)
FROM {objectQualifier}forum f 
JOIN {objectQualifier}category c on c.categoryid = f.categoryid 
JOIN {objectQualifier}board b on b.boardid = c.boardid 
JOIN {objectQualifier}vaccess access ON (f.forumid = access.ForumID and access.UserID = :i_userid)  
JOIN {objectQualifier}forum_ns  n1  on (n1.forumid = f.forumid and f.CategoryID = :i_CategoryID and n1.parentid = 0 and n1.tree = :i_CategoryID)
ORDER BY n1.left_key
INTO
:"BoardID",
:"BoardName",
:"CategoryID",
:"CategoryName",
:"Title",
:"NoAccess",
:"ForumID",
:"ParentID",
:"Level",
:"HasChildren"
DO 
SUSPEND;
end
END;
--GO
   
CREATE PROCEDURE {objectQualifier}forum_ns_getpath(i_forumid integer, i_parentincluded BOOL)
				  RETURNS (
"ForumID"  integer,
"ParentID" integer,
"Level" integer 
)
 AS 
DECLARE ici_nid integer;
DECLARE ici_categoryid integer;
BEGIN
SELECT ns.nid, ns.categoryid
FROM {objectQualifier}forum_ns ns
WHERE ns.forumid = :i_forumid
INTO :ici_nid, :ici_categoryid;

FOR SELECT n1.forumid, n1.parentid, n1.level
FROM {objectQualifier}forum f
join {objectQualifier}forum_ns n1 
 on (f.forumid = n1.forumid and n1.tree= :ici_categoryid) 
 order by n1.left_key,f.categoryid, f.sortorder
INTO
:"ForumID",
:"ParentID",
:"Level"
DO SUSPEND;
END;  
--GO

CREATE PROCEDURE {objectQualifier}forum_ns_listpath(
						   i_forumid integer)
         		   RETURNS
						   ("ForumID" integer,
						   "Name" varchar(255),
						   "Level" integer
						   )

				  AS
DECLARE ici_left_key integer;
DECLARE ici_right_key integer;
DECLARE ici_categoryid integer;
BEGIN
SELECT left_key,right_key,categoryid  
FROM {objectQualifier}forum_ns where forumid = :i_forumid
INTO :ici_left_key,:ici_right_key,:ici_categoryid;


FOR SELECT f.forumid,
	   f.name,
	   -- we don't return board and category nodes here
	   (ns.level - 2)  
	   FROM {objectQualifier}forum_ns ns 
	   JOIN {objectQualifier}forum f on (f.forumid = ns.forumid and ns.tree= :ici_categoryid)
	   WHERE ns.left_key <= :ici_left_key AND ns.right_key >= :ici_right_key ORDER BY ns.left_key
	   INTO
	   :"ForumID",
		:"Name",
		:"Level"
DO 
SUSPEND;						 
END;
 --GO

-- Initialize all this

CREATE PROCEDURE {objectQualifier}FORUM_NS_RECREATE
				AS				
BEGIN

EXECUTE PROCEDURE  {objectQualifier}CR_OR_CHCK_NS_TABLES;
EXECUTE PROCEDURE  {objectQualifier}FILLIN_OR_CHECK_NS_TABLE;
END;
--GO 
CREATE PROCEDURE {objectQualifier}FORUM_NS_RECREATE_INI
				AS	
declaRE I INTEGER;	
declaRE J INTEGER;				
BEGIN
I = 0;
J = 0;
IF (NOT EXISTS(SELECT 1 
               FROM RDB$RELATIONS a 
               WHERE a.RDB$RELATION_NAME=upper('{objectQualifier}FORUM_NS') 
               ROWS 1)) THEN EXECUTE PROCEDURE {objectQualifier}FORUM_NS_RECREATE;
IF (EXISTS(SELECT 1 
               FROM RDB$RELATIONS a 
               WHERE a.RDB$RELATION_NAME=upper('{objectQualifier}FORUM_NS') 
               ROWS 1)) THEN 
			   BEGIN
			   SELECT COUNT(1) FROM {objectQualifier}FORUM_NS into :I;
			   SELECT COUNT(1) FROM {objectQualifier}FORUM into :J;
			   IF (:I < :J) THEN
			   EXECUTE PROCEDURE {objectQualifier}FORUM_NS_RECREATE;
			   END
END;
--GO
EXECUTE PROCEDURE {objectQualifier}FORUM_NS_RECREATE_INI;
-- GO

