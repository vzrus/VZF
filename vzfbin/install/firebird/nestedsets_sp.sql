/* ******************************************************************************************************************************
*********************************************************************************************************************************
SELECT FUNCTIONS
*********************************************************************************************************************************
******************************************************************************************************************************** */


CREATE PROCEDURE {objectQualifier}forum_ns_getchildren(i_boardid integer,  i_categoryid integer, I_FORUMID integer,  I_NOTINCLUDED bool, i_immediateonly bool)
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
BEGIN
if (:i_forumid > 0) then
begin
FOR SELECT b.boardid, b.name as BoardName, c.categoryid, c.name as CategoryName, n1.name as Title,  
n1.forumid, n1.parentid, n1."LEVEL" ,CAST(SIGN(n1.right_key-n1.left_key - 1) as smallint)
FROM 
{objectQualifier}board b 
join {objectQualifier}category c on b.boardid = c.boardid
join {objectQualifier}forum n1 on c.categoryid = n1.categoryid 
CROSS JOIN
{objectQualifier}forum  n2   WHERE  n2.ForumID = :I_FORUMID
AND  n1.left_key BETWEEN n2.left_key + cast(:I_NOTINCLUDED as int) AND n2.right_key
and (:i_immediateonly = 0  OR n1.ParentID = n2.ForumID) ORDER BY n1.left_key
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
f.forumid, f.parentid, f."LEVEL" ,CAST(SIGN(f.right_key-f.left_key - 1) as smallint)
FROM 
{objectQualifier}board b 
join {objectQualifier}category c on b.boardid = c.boardid  
JOIN {objectQualifier}Forum  f 
ON (c.categoryid = f.categoryid and f.CategoryID = :i_CategoryID and f.parentid is null)
ORDER BY f.left_key
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
BEGIN
if (:i_forumid > 0) then
begin
FOR SELECT
b.boardid, b.name as BoardName, c.categoryid, c.name as CategoryName, n1.name as Title, fa.AccessMaskID,  
n1.forumid, n1.parentid, n1."LEVEL" ,CAST(SIGN(n1.right_key-n1.left_key - 1) as smallint)
FROM 
{objectQualifier}Board b  
JOIN {objectQualifier}Category c on b.BoardID = c.BoardID
JOIN {objectQualifier}Forum  n1 on c.CategoryID = n1.CategoryID 
CROSS JOIN
{objectQualifier}Forum  n2  
join {objectQualifier}ForumAccess fa  on (fa.ForumID = n1.ForumID and fa.GroupID = :I_GROUPID) 
WHERE   n2.ForumID = :i_ForumID
AND  n1.left_key BETWEEN n2.left_key + cast(:i_notincluded as int) AND n2.right_key
and (:i_immediateonly = 0  OR n1.ParentID = n2.ForumID) 
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
else
begin
FOR SELECT
b.boardid, b.name as BoardName, c.categoryid, c.name as CategoryName, f.name as Title, fa.AccessMaskID,  
f.forumid, f.parentid, f."LEVEL" ,CAST(SIGN(f.right_key-f.left_key - 1) as smallint)
FROM 
{objectQualifier}board b 
join {objectQualifier}category c on b.boardid = c.boardid  
JOIN {objectQualifier}Forum  f ON (c.categoryid = f.categoryid and f.CategoryID = :i_CategoryID and f.parentid is null)
join {objectQualifier}ForumAccess fa  on (fa.ForumID = f.ForumID and fa.GroupID = :I_GROUPID)
ORDER BY f.left_key
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
BEGIN
if (:i_forumid > 0) then
begin
FOR SELECT c.categoryid, c.name , n1.name,
(CASE WHEN access.readaccess = 0 THEN 1 ELSE 0 END) , 
n1.forumid, n1.parentid, n1."LEVEL" ,CAST(SIGN(n1.right_key-n1.left_key - 1) as smallint)
FROM 
{objectQualifier}Board b  
JOIN {objectQualifier}Category c on b.BoardID = c.BoardID
JOIN {objectQualifier}Forum  n1 on c.CategoryID = n1.CategoryID 
CROSS JOIN
{objectQualifier}Forum  n2   
JOIN {objectQualifier}activeaccess access 
ON (n1.forumid = access.forumid and access.userid = :i_userid)  
WHERE  (access.readaccess > 0 or (access.readaccess = 0 and BIN_AND(n2.flags,2) != 2)) and ( n2.ForumID = :i_ForumID
AND  (n1.left_key BETWEEN n2.left_key + cast(:i_notincluded as int) AND n2.right_key)
and (:i_immediateonly = 0  OR n1.parentid = n2.ForumID)) ORDER BY n1.left_key
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
FOR SELECT c.categoryid, c.name , f.name,(CASE WHEN access.readaccess = 0 THEN 1 ELSE 0 END),
f.forumid, f.parentid, f."LEVEL" ,CAST(SIGN(f.right_key-f.left_key - 1) as smallint)
FROM {objectQualifier}board b 
join {objectQualifier}category c on b.boardid = c.boardid  
JOIN {objectQualifier}Forum  f ON (c.CategoryID = f.CategoryID and f.CategoryID = :i_CategoryID and f.parentid is null)
JOIN {objectQualifier}ActiveAccess access ON (f.forumid = access.forumid and access.userid =:i_userid) 
WHERE  (access.readaccess > 0 or (access.readaccess = 0 and BIN_AND(f.flags,2) != 2))
ORDER BY f.left_key
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
BEGIN
if (:i_forumid > 0) then
begin
FOR SELECT b.boardid, b.name, c.categoryid, c.name , n1.name,
(CASE WHEN access.readaccess = 0 THEN 1 ELSE 0 END), n1.forumid, n1.parentid, n1."LEVEL" ,CAST(SIGN(n1.right_key-n1.left_key - 1) as smallint)
FROM {objectQualifier}Board b  
JOIN {objectQualifier}Category c on b.BoardID = c.BoardID
JOIN {objectQualifier}Forum  n1 on c.CategoryID = n1.CategoryID 
CROSS JOIN
{objectQualifier}Forum  n2   
JOIN {objectQualifier}vaccess access ON (n1.forumid = access.forumid and access.userid = :i_userid)  
WHERE  (access.readaccess > 0 or (access.readaccess = 0 and BIN_AND(n2.flags,2) != 2)) and ( n2.ForumID = :i_ForumID
AND  n1.left_key BETWEEN n2.left_key + cast(:i_notincluded as int) AND n2.right_key
and (:i_immediateonly = 0  OR n1.parentid = n2.ForumID)) ORDER BY n1.left_key
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
(CASE WHEN access.readaccess = 0 THEN 1 ELSE 0 END), f.forumid, f.parentid, 
f."LEVEL" ,CAST(SIGN(f.right_key-f.left_key - 1) as smallint)
FROM {objectQualifier}board b 
join {objectQualifier}category c on b.boardid = c.boardid  
JOIN {objectQualifier}Forum  f ON (c.CategoryID = f.CategoryID and f.CategoryID = :i_CategoryID and f.parentid is null)
JOIN {objectQualifier}vaccess access ON (f.forumid = access.forumid and access.userid = :i_userid) 
where  (access.readaccess > 0 or (access.readaccess = 0 and BIN_AND(f.flags,2) != 2))
ORDER BY f.left_key
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
DECLARE ici_categoryid integer;
BEGIN
SELECT CATEGORYID
FROM {objectQualifier}FORUM 
WHERE FORUMID = :i_forumid
INTO :ici_categoryid;

FOR SELECT FORUMID, PARENTID, "LEVEL"
FROM {objectQualifier}FORUM 
 WHERE CATEGORYID = :ici_categoryid 
 order by left_key, sortorder
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
FROM {objectQualifier}FORUM where FORUMID = :i_forumid
INTO :ici_left_key,:ici_right_key,:ici_categoryid;


FOR SELECT forumid,
	   name,
	   -- we don't return board and category nodes here
	   ("LEVEL" - 2)  
	   FROM {objectQualifier}FORUM  
	   WHERE 
	   CATEGORYID= :ici_categoryid AND left_key <= :ici_left_key AND right_key >= :ici_right_key 
	   ORDER BY left_key
	   INTO
	   :"ForumID",
		:"Name",
		:"Level"
DO 
SUSPEND;						 
END;
 --GO



