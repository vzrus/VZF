
/* ******************************************************************************************************************************
*********************************************************************************************************************************
SELECT FUNCTIONS
*********************************************************************************************************************************
******************************************************************************************************************************** */


CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}forum_ns_getchildren(i_boardid integer,  i_categoryid integer, i_forumid integer,  i_notincluded boolean, i_immediateonly boolean)
				   RETURNS SETOF {databaseSchema}.{objectQualifier}forum_ns_getsubtree_rt AS
$BODY$DECLARE
_rec {databaseSchema}.{objectQualifier}forum_ns_getsubtree_rt%ROWTYPE;
_nid integer;
BEGIN
if (i_forumid > 0) then
SELECT ns.nid
INTO _nid
FROM {databaseSchema}.{objectQualifier}forum_ns ns
WHERE ns.forumid = i_forumid limit 1;
elseif  (i_categoryid > 0) then
SELECT ns.nid
INTO _nid
FROM {databaseSchema}.{objectQualifier}forum_ns ns
WHERE ns.forumid = 0 and ns.categoryid = i_categoryid limit 1;
else
SELECT ns.nid
INTO _nid
FROM {databaseSchema}.{objectQualifier}forum_ns ns
WHERE ns.forumid = 0 and ns.categoryid = 0 and boardid = i_boardid limit 1;
end if;

FOR _rec IN SELECT n1.forumid, n1.parentid, n1.level
FROM {databaseSchema}.{objectQualifier}forum_ns  n1,
{databaseSchema}.{objectQualifier}forum_ns  n2   WHERE  ( n2.nid = _nid
AND  n1.left_key BETWEEN n2.left_key + i_notincluded::integer AND n2.right_key
and (i_immediateonly IS FALSE  OR n1.parentid = n2.nid)) ORDER BY n1.left_key
LOOP
RETURN NEXT _rec;
END LOOP;
END;
$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER STRICT
  COST 100;
--GO
COMMENT ON FUNCTION {databaseSchema}.{objectQualifier}forum_ns_getchildren (integer,integer,integer, boolean, boolean) IS 'If i_forumid is null returns all nodes on the first level and their children.';
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}forum_ns_getchildren_activeuser(i_boardid integer,  i_categoryid integer, i_forumid integer , i_userid integer, i_notincluded boolean, i_immediateonly boolean)
				   RETURNS SETOF {databaseSchema}.{objectQualifier}forum_ns_getchildren_rt AS
$BODY$DECLARE
_rec {databaseSchema}.{objectQualifier}forum_ns_getchildren_rt%ROWTYPE;
_nid integer; 
BEGIN
SELECT ns.nid
INTO _nid
FROM {databaseSchema}.{objectQualifier}forum_ns ns
WHERE ns.forumid = 0 and ns.categoryid = 0 and boardid = i_boardid;

if (i_forumid > 0) then
SELECT ns.nid
INTO _nid
FROM {databaseSchema}.{objectQualifier}forum_ns ns
WHERE ns.forumid = i_forumid;
elseif  (i_categoryid > 0) then
SELECT ns.nid
INTO _nid
FROM {databaseSchema}.{objectQualifier}forum_ns ns
WHERE ns.forumid = 0 and ns.categoryid = i_categoryid;
else
SELECT ns.nid
INTO _nid
FROM {databaseSchema}.{objectQualifier}forum_ns ns
WHERE ns.forumid = 0 and ns.categoryid = 0 and boardid = i_boardid;
end if;

FOR _rec IN SELECT b.boardid, b.name, c.categoryid, c.name , f.name, (access.readaccess IS FALSE) as NoAccess, n1.forumid, n1.parentid, n1.level,(n1.right_key-n1.left_key > 1) 
FROM {databaseSchema}.{objectQualifier}forum f 
JOIN {databaseSchema}.{objectQualifier}category c on c.categoryid = f.categoryid
JOIN {databaseSchema}.{objectQualifier}board b on b.boardid = c.boardid 
JOIN {databaseSchema}.{objectQualifier}activeaccess access ON (f.forumid = access.forumid and access.userid = i_userid)  
JOIN {databaseSchema}.{objectQualifier}forum_ns  n1 ON (n1.forumid = f.forumid)
CROSS JOIN
{databaseSchema}.{objectQualifier}forum_ns  n2   WHERE  (access.readaccess is true or (not access.readaccess and (f.flags & 2) != 2)) and ( n2.nid = _nid
AND  n1.left_key BETWEEN n2.left_key + i_notincluded::integer AND n2.right_key
and (i_immediateonly IS FALSE  OR n1.parentid = n2.nid)) ORDER BY n1.left_key
LOOP
RETURN NEXT _rec;
END LOOP;
END;
$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER STRICT
  COST 100;
--GO
COMMENT ON FUNCTION {databaseSchema}.{objectQualifier}forum_ns_getchildren_activeuser(integer,integer,integer, integer, boolean, boolean) IS 'If i_forumid is null returns all nodes on the first level and their children.';
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}forum_ns_getchildren_anyuser(i_boardid integer,  i_categoryid integer, i_forumid integer , i_userid integer, i_notincluded boolean, i_immediateonly boolean)
				   RETURNS SETOF {databaseSchema}.{objectQualifier}forum_ns_getchildren_rt AS
$BODY$DECLARE
_rec {databaseSchema}.{objectQualifier}forum_ns_getchildren_rt%ROWTYPE;
_nid integer;
BEGIN
if (i_forumid > 0) then
SELECT ns.nid
INTO _nid
FROM {databaseSchema}.{objectQualifier}forum_ns ns
WHERE ns.forumid = i_forumid;
elseif  (i_categoryid > 0) then
SELECT ns.nid
INTO _nid
FROM {databaseSchema}.{objectQualifier}forum_ns ns
WHERE ns.forumid = 0 and ns.categoryid = i_categoryid;
else
SELECT ns.nid
INTO _nid
FROM {databaseSchema}.{objectQualifier}forum_ns ns
WHERE ns.forumid = 0 and ns.categoryid = 0 and boardid = i_boardid;
end if;

FOR _rec IN SELECT b.boardid, b.name, c.categoryid, c.name , f.name, (access.readaccess IS FALSE) as NoAccess, n1.forumid, n1.parentid, n1.level,(n1.right_key-n1.left_key > 1) 
FROM {databaseSchema}.{objectQualifier}forum f 
JOIN {databaseSchema}.{objectQualifier}category c on c.categoryid = f.categoryid 
JOIN {databaseSchema}.{objectQualifier}board b on b.boardid = c.boardid 
JOIN {databaseSchema}.{objectQualifier}vaccess_combo access ON (f.forumid = access."ForumID" and access."UserID" = i_userid)  
JOIN {databaseSchema}.{objectQualifier}forum_ns  n1 ON n1.forumid = f.forumid
CROSS JOIN
{databaseSchema}.{objectQualifier}forum_ns  n2   WHERE (access.readaccess is true or (not access.readaccess and (f.flags & 2) != 2)) and ( n2.nid = _nid
AND  n1.left_key BETWEEN n2.left_key + i_notincluded::integer AND n2.right_key
and (i_immediateonly IS FALSE  OR n1.parentid = n2.nid)) ORDER BY n1.left_key
LOOP
RETURN NEXT _rec;
END LOOP;
END;
$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER STRICT
  COST 100;
--GO
COMMENT ON FUNCTION {databaseSchema}.{objectQualifier}forum_ns_getchildren_anyuser(integer,integer,integer,integer, boolean, boolean) IS 'If i_forumid is null returns all nodes on the first level and their children.';
--GO


   
CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}forum_ns_getpath(i_forumid integer, i_parentincluded boolean)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}forum_ns_getsubtree_rt AS 
$BODY$DECLARE
_rec {databaseSchema}.{objectQualifier}forum_ns_getsubtree_rt%ROWTYPE;
_nid integer;
BEGIN
SELECT ns.nid
INTO _nid
FROM {databaseSchema}.{objectQualifier}forum_ns ns
WHERE ns.forumid = i_forumid;

FOR _rec IN  SELECT n1.forumid, n1.parentid, n1.level
FROM {databaseSchema}.{objectQualifier}forum f
join {databaseSchema}.{objectQualifier}forum_ns n1 
on n1.forumid = f.forumid 
order by n1.left_key,f.categoryid, f.sortorder
LOOP
RETURN NEXT _rec;
END LOOP;
END;
$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER STRICT
  COST 100;  
--GO


CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}forum_ns_listpath(
						   i_forumid integer)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}forum_listpath_return_type AS
$BODY$DECLARE
_rec {databaseSchema}.{objectQualifier}forum_listpath_return_type%ROWTYPE;
_left_key integer;
_right_key integer;
BEGIN
SELECT left_key,right_key INTO _left_key,_right_key 
FROM {databaseSchema}.{objectQualifier}forum_ns where forumid = i_forumid;

FOR _rec IN
SELECT f.forumid,
	   f.name,
	   -- we don't return board and category nodes here
	   (ns.level - 2)  
	   FROM {databaseSchema}.{objectQualifier}forum_ns ns 
	   JOIN {databaseSchema}.{objectQualifier}forum f on f.forumid = ns.forumid
	   WHERE ns.left_key <= _left_key AND ns.right_key >= _right_key ORDER BY ns.left_key
LOOP
RETURN NEXT _rec;
END LOOP;
						 
END; $BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100 ROWS 1000;
  --GO



-- Initialize all this

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}forum_ns_recreate()
				  RETURNS void AS
$BODY$
BEGIN
PERFORM {databaseSchema}.{objectQualifier}forum_ns_drop_triggers();
PERFORM {databaseSchema}.{objectQualifier}forum_ns_dropbridge_triggers();
PERFORM {databaseSchema}.{objectQualifier}create_or_check_ns_tables();
PERFORM {databaseSchema}.{objectQualifier}forum_ns_create_triggers();
PERFORM {databaseSchema}.{objectQualifier}fillin_or_check_ns_tables();
END;
$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER STRICT
  COST 100;  
	--GO 
SELECT {databaseSchema}.{objectQualifier}forum_ns_recreate();
-- GO