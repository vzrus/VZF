/* ******************************************************************************************************************************
*********************************************************************************************************************************
SELECT FUNCTIONS
*********************************************************************************************************************************
******************************************************************************************************************************** */


CREATE PROCEDURE {objectQualifier}forum_ns_getchildren(i_boardid integer,  i_categoryid integer, i_forumid integer,  i_notincluded bool, i_immediateonly bool)
				   RETURNS (
"ForumID"  integer,
"ParentID" integer,
"Level" integer
) AS
DECLARE ici_nid integer;
BEGIN
if (:i_forumid > 0) then
SELECT ns.nid
FROM {objectQualifier}forum_ns ns
WHERE ns.forumid = :i_forumid
INTO :ici_nid;
else if  (:i_categoryid > 0) then
SELECT ns.nid
FROM {objectQualifier}forum_ns ns
WHERE ns.forumid = 0 and ns.categoryid = :i_categoryid
INTO :ici_nid;
else
SELECT ns.nid
FROM {objectQualifier}forum_ns ns
WHERE ns.forumid = 0 and ns.categoryid = 0 and boardid = :i_boardid
INTO :ici_nid;

FOR SELECT n1.forumid, n1.parentid, n1."LEVEL" 
FROM {objectQualifier}forum_ns  n1
join
{objectQualifier}forum_ns  n2
on (n1.forumid = n2.forumid and n2.forumid > 0) WHERE  ( n2.nid = :ici_nid
 AND  n1.left_key BETWEEN n2.left_key + cast(:i_notincluded as integer) AND n2.right_key
 and (:i_immediateonly = 0  OR n1.parentid = n2.nid)) ORDER BY n1.left_key
INTO
:"ForumID",
:"ParentID",
:"Level"
DO SUSPEND;
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
"HasChildren" bool 
) AS
DECLARE ici_nid integer; 
BEGIN
SELECT ns.nid
FROM {objectQualifier}forum_ns ns
WHERE ns.forumid = 0 and ns.categoryid = 0 and boardid = :i_boardid
INTO :ici_nid;

if (:i_forumid > 0) then
SELECT ns.nid
FROM {objectQualifier}forum_ns ns
WHERE ns.forumid = :i_forumid
INTO :ici_nid;
else if  (i_categoryid > 0) then
SELECT ns.nid
FROM {objectQualifier}forum_ns ns
WHERE ns.forumid = 0 and ns.categoryid = :i_categoryid
INTO :ici_nid;
else
SELECT ns.nid
FROM {objectQualifier}forum_ns ns
WHERE ns.forumid = 0 and ns.categoryid = 0 and boardid = :i_boardid
INTO :ici_nid;

FOR SELECT c.categoryid, c.name , f.name, CAST(access.readaccess as smallint), n1.forumid, n1.parentid, n1."LEVEL" ,CAST((n1.right_key-n1.left_key - 1) as smallint)
FROM {objectQualifier}forum f 
JOIN {objectQualifier}category c on c.categoryid = f.categoryid 
JOIN {objectQualifier}activeaccess access ON (f.forumid = access.forumid and access.userid = :i_userid)  
JOIN {objectQualifier}forum_ns  n1 ON (n1.forumid = f.forumid)
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
"HasChildren" bool 
)
AS
DECLARE ici_nid integer;
BEGIN
if (:i_forumid > 0) then
SELECT ns.nid
FROM {objectQualifier}forum_ns ns
WHERE ns.forumid = :i_forumid
INTO :ici_nid;
else if  (:i_categoryid > 0) then
SELECT ns.nid
FROM {objectQualifier}forum_ns ns
WHERE ns.forumid = 0 and ns.categoryid = :i_categoryid
INTO :ici_nid;
else
SELECT ns.nid
FROM {objectQualifier}forum_ns ns
WHERE ns.forumid = 0 and ns.categoryid = 0 and boardid = :i_boardid
INTO :ici_nid;

FOR SELECT b.boardid, b.name, c.categoryid, c.name , f.name,
CAST(access.readaccess as smallint), n1.forumid, n1.parentid, n1."LEVEL" ,CAST((n1.right_key-n1.left_key - 1) as smallint)
FROM {objectQualifier}forum f 
JOIN {objectQualifier}category c on c.categoryid = f.categoryid 
JOIN {objectQualifier}board b on b.boardid = c.boardid 
JOIN {objectQualifier}vaccess access ON (f.forumid = access.ForumID and access.UserID = :i_userid)  
JOIN {objectQualifier}forum_ns  n1 ON n1.forumid = f.forumid
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
BEGIN
SELECT ns.nid
FROM {objectQualifier}forum_ns ns
WHERE ns.forumid = :i_forumid
INTO :ici_nid;

FOR SELECT n1.forumid, n1.parentid, n1.level
FROM {objectQualifier}forum f
join {objectQualifier}forum_ns n1 
on n1.forumid = f.forumid 
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
BEGIN
SELECT left_key,right_key  
FROM {objectQualifier}forum_ns where forumid = :i_forumid
INTO :ici_left_key,:ici_right_key;

FOR SELECT f.forumid,
	   f.name,
	   -- we don't return board and category nodes here
	   (ns.level - 2)  
	   FROM {objectQualifier}forum_ns ns 
	   JOIN {objectQualifier}forum f on f.forumid = ns.forumid
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

CREATE PROCEDURE {objectQualifier}forum_ns_recreate
				AS
BEGIN
EXECUTE PROCEDURE  {objectQualifier}CR_OR_CHCK_NS_TABLES;
EXECUTE PROCEDURE  {objectQualifier}FILLIN_OR_CHECK_NS_TABLE;
END;
--GO 
EXECUTE PROCEDURE {objectQualifier}forum_ns_recreate;
-- GO

