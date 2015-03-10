
/* ******************************************************************************************************************************
*********************************************************************************************************************************
SELECT FUNCTIONS
*********************************************************************************************************************************
******************************************************************************************************************************** */


CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}forum_ns_getchildren(i_boardid integer,  i_categoryid integer, i_forumid integer, i_notincluded boolean, i_immediateonly boolean)
				   RETURNS SETOF {databaseSchema}.{objectQualifier}forum_ns_getchildren_rt AS
$BODY$DECLARE
_rec {databaseSchema}.{objectQualifier}forum_ns_getchildren_rt%ROWTYPE;
_nid integer;
BEGIN
if (i_forumid > 0) then
FOR _rec IN select b.boardid, b.name as BoardName, c.categoryid, c.name as CategoryName, n1.name as Title,  
n1.forumid, n1.parentid, n1.level as "Level", (n1.right_key-n1.left_key > 1) as HasChildren
FROM 
{databaseSchema}.{objectQualifier}board b 
JOIN {databaseSchema}.{objectQualifier}category c on b.boardid = c.boardid 
join {databaseSchema}.{objectQualifier}forum  n1 ON (c.categoryid = n1.categoryid)
CROSS JOIN
{databaseSchema}.{objectQualifier}forum  n2   WHERE  ( n2.forumid = i_forumid
AND  n1.left_key BETWEEN n2.left_key + i_notincluded::integer AND n2.right_key
and (i_immediateonly IS FALSE  OR n1.parentid = n2.forumid)) ORDER BY n1.left_key
LOOP
RETURN NEXT _rec;
END LOOP;
else
FOR _rec IN select b.boardid, b.name as BoardName, c.categoryid, c.name as CategoryName, f.name as Title,  
f.forumid, f.parentid, f.level as "Level", (f.right_key-f.left_key > 1) as HasChildren
FROM 
{databaseSchema}.{objectQualifier}board b 
JOIN {databaseSchema}.{objectQualifier}category c on b.boardid = c.boardid
join {databaseSchema}.{objectQualifier}forum f  on (c.categoryid = f.categoryid and f.categoryid = i_categoryid  and f.parentid is null)
ORDER BY f.left_key
LOOP
RETURN NEXT _rec;
END LOOP;
end if;


END;
$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER CALLED ON NULL INPUT
  COST 100;
--GO
COMMENT ON FUNCTION {databaseSchema}.{objectQualifier}forum_ns_getchildren (integer,integer,integer, boolean, boolean) IS 'If i_forumid is null returns all nodes on the first level and their children.';
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}forum_ns_getch_accgroup(i_boardid integer,  i_categoryid integer, i_forumid integer,i_groupid integer,  i_notincluded boolean, i_immediateonly boolean)
				   RETURNS SETOF {databaseSchema}.{objectQualifier}forum_ns_getch_accgroup_rt AS
$BODY$DECLARE
_rec {databaseSchema}.{objectQualifier}forum_ns_getch_accgroup_rt%ROWTYPE;
_nid integer;
BEGIN
if (i_forumid > 0) then
FOR _rec IN select b.boardid, b.name as BoardName, c.categoryid, c.name as CategoryName, n1.name as Title,  
n1.forumid, fa.AccessMaskID, n1.parentid, n1.level as "Level", (n1.right_key-n1.left_key > 1) as HasChildren
FROM 
{databaseSchema}.{objectQualifier}board b 
JOIN {databaseSchema}.{objectQualifier}category c on b.boardid = c.boardid 
join {databaseSchema}.{objectQualifier}forum  n1 ON (c.categoryid = n1.categoryid)
CROSS JOIN
{databaseSchema}.{objectQualifier}forum  n2  
join {databaseSchema}.{objectQualifier}forumaccess fa  
on (fa.forumid = n1.forumid and fa.groupid = i_groupid)
WHERE  ( n2.forumid = i_forumid
AND  n1.left_key BETWEEN n2.left_key + i_notincluded::integer AND n2.right_key
and (i_immediateonly IS FALSE  OR n1.parentid = n2.forumid)) ORDER BY n1.left_key
LOOP
RETURN NEXT _rec;
END LOOP;
else
FOR _rec IN select b.boardid, b.name as BoardName, c.categoryid, c.name as CategoryName, f.name as Title,  
f.forumid, fa.AccessMaskID, f.parentid, f.level as "Level", (f.right_key-f.left_key > 1) as HasChildren
FROM 
{databaseSchema}.{objectQualifier}board b 
JOIN {databaseSchema}.{objectQualifier}category c on b.boardid = c.boardid 
join {databaseSchema}.{objectQualifier}forum  f on (c.categoryid = f.categoryid and f.categoryid = i_categoryid  and f.parentid is null)
join {databaseSchema}.{objectQualifier}forumaccess fa  
on (fa.forumid = f.forumid and fa.groupid = i_groupid)
 ORDER BY f.left_key
LOOP
RETURN NEXT _rec;
END LOOP;
end if;
END;
$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER CALLED ON NULL INPUT
  COST 100;
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}forum_ns_getch_actuser(i_boardid integer, i_categoryid integer, i_forumid integer , i_userid integer, i_notincluded boolean, i_immediateonly boolean)
				   RETURNS SETOF {databaseSchema}.{objectQualifier}forum_ns_getch_user_rt AS
$BODY$DECLARE
_rec {databaseSchema}.{objectQualifier}forum_ns_getch_user_rt%ROWTYPE;
_nid integer; 
BEGIN


if (i_forumid > 0) then
FOR _rec IN SELECT b.boardid, b.name, c.categoryid, c.name , n1.name, 
(not access.readaccess) as NoAccess, n1.forumid, n1.parentid, n1.level,(n1.right_key-n1.left_key > 1) 
FROM 
{databaseSchema}.{objectQualifier}board b 
JOIN {databaseSchema}.{objectQualifier}category c on b.boardid = c.boardid 
JOIN {databaseSchema}.{objectQualifier}forum n1 ON (c.categoryid = n1.categoryid)
CROSS JOIN
{databaseSchema}.{objectQualifier}forum n2  
JOIN {databaseSchema}.{objectQualifier}activeaccess access ON (n1.forumid = access.forumid and access.userid = i_userid)  
WHERE  (access.readaccess is true or (not access.readaccess and (n1.flags & 2) != 2)) and ( n2.forumid = i_forumid
AND  n1.left_key BETWEEN n2.left_key + i_notincluded::integer AND n2.right_key
and (i_immediateonly IS FALSE  OR n1.parentid = n2.forumid)) ORDER BY n1.left_key
LOOP
RETURN NEXT _rec;
END LOOP;
else
FOR _rec IN SELECT b.boardid, b.name, c.categoryid, c.name , f.name, (not access.readaccess) as NoAccess, f.forumid, f.parentid, f.level,(f.right_key-f.left_key > 1) 
FROM 
{databaseSchema}.{objectQualifier}category c 
JOIN {databaseSchema}.{objectQualifier}board b on b.boardid = c.boardid 
JOIN {databaseSchema}.{objectQualifier}forum  f 
on (c.categoryid = f.categoryid and f.categoryid = i_categoryid  and f.parentid is null)
JOIN {databaseSchema}.{objectQualifier}activeaccess access 
ON (f.forumid = access.forumid and access.userid = i_userid)  
WHERE  (access.readaccess is true or (not access.readaccess and (f.flags & 2) != 2))
  ORDER BY f.left_key
LOOP
RETURN NEXT _rec;
END LOOP;
end if;


END;
$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER CALLED ON NULL INPUT
  COST 100;
--GO
COMMENT ON FUNCTION {databaseSchema}.{objectQualifier}forum_ns_getch_actuser(integer,integer,integer, integer, boolean, boolean) IS 'If i_forumid is null returns all nodes on the first level and their children.';
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}forum_ns_getch_anyuser(i_boardid integer,  i_categoryid integer, i_forumid integer , i_userid integer, i_notincluded boolean, i_immediateonly boolean)
				   RETURNS SETOF {databaseSchema}.{objectQualifier}forum_ns_getch_user_rt AS
$BODY$DECLARE
_rec {databaseSchema}.{objectQualifier}forum_ns_getch_user_rt%ROWTYPE;
_nid integer;
BEGIN
if (i_forumid > 0) then
SELECT ns.nid
INTO _nid
FROM {databaseSchema}.{objectQualifier}forum_ns ns
WHERE ns.forumid = i_forumid;

FOR _rec IN SELECT b.boardid, b.name, c.categoryid, c.name , n1.name, (not access.readaccess) as NoAccess, n1.forumid, n1.parentid, n1.level,(n1.right_key-n1.left_key > 1) 
FROM {databaseSchema}.{objectQualifier}category c 
JOIN {databaseSchema}.{objectQualifier}board b on b.boardid = c.boardid 
JOIN {databaseSchema}.{objectQualifier}forum  n1 ON (c.categoryid = n1.categoryid)
CROSS JOIN
{databaseSchema}.{objectQualifier}forum  n2  
JOIN {databaseSchema}.{objectQualifier}vaccess_combo access ON (n1.forumid = access."ForumID" and access."UserID" = i_userid)  
 WHERE (access.readaccess or (not access.readaccess and (n1.flags & 2) != 2)) and ( n2.categoryid = i_categoryid
AND  n1.left_key BETWEEN n2.left_key + i_notincluded::integer AND n2.right_key
and (not i_immediateonly  OR n1.parentid = n2.forumid)) ORDER BY n1.left_key
LOOP
RETURN NEXT _rec;
END LOOP;
else

FOR _rec IN SELECT b.boardid, b.name, c.categoryid, c.name , f.name, (not access.readaccess) as NoAccess, f.forumid, f.parentid, f.level,(f.right_key-f.left_key > 1) 
FROM  {databaseSchema}.{objectQualifier}category c  
JOIN {databaseSchema}.{objectQualifier}board b on b.boardid = c.boardid 
JOIN {databaseSchema}.{objectQualifier}forum  f on (c.categoryid = f.categoryid and f.categoryid = i_categoryid  and f.parentid is null)
JOIN {databaseSchema}.{objectQualifier}vaccess_combo access ON (f.forumid = access."ForumID" and access."UserID" = i_userid)  
WHERE (access.readaccess or (not access.readaccess and (f.flags & 2) != 2))
ORDER BY f.left_key
LOOP
RETURN NEXT _rec;
END LOOP;
end if;


END;
$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER CALLED ON NULL INPUT
  COST 100;
--GO
COMMENT ON FUNCTION {databaseSchema}.{objectQualifier}forum_ns_getch_anyuser(integer,integer,integer,integer, boolean, boolean) IS 'If i_forumid is null returns all nodes on the first level and their children.';
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

FOR _rec IN  SELECT forumid, parentid, "level"
FROM {databaseSchema}.{objectQualifier}forum f
where f.categoryid= ici_categoryid 
order by left_key,categoryid ,sortorder
LOOP
RETURN NEXT _rec;
END LOOP;
END;
$BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER CALLED ON NULL INPUT
  COST 100;  
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}forum_ns_listpath(
						   i_forumid integer)
				  RETURNS SETOF {databaseSchema}.{objectQualifier}forum_listpath_return_type AS
$BODY$DECLARE
_rec {databaseSchema}.{objectQualifier}forum_listpath_return_type%ROWTYPE;
_left_key integer;
_right_key integer;
ici_categoryid integer;
BEGIN
SELECT left_key,right_key, categoryid INTO _left_key,_right_key, ici_categoryid
FROM {databaseSchema}.{objectQualifier}forum where forumid = i_forumid;

FOR _rec IN
SELECT f.forumid,
	   f.name,
	   -- we don't return board and category nodes here
	   (f.level - 2)  
	   FROM {databaseSchema}.{objectQualifier}forum f 
	   WHERE f.categoryid= ici_categoryid and f.left_key <= _left_key AND f.right_key >= _right_key ORDER BY f.left_key
LOOP
RETURN NEXT _rec;
END LOOP;
						 
END; $BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100 ROWS 1000;
  --GO

DO $$ 
BEGIN  
ALTER TABLE {databaseSchema}.{objectQualifier}forum_ns disable trigger user;
END$$;
--GO
DROP TRIGGER IF EXISTS {databaseSchema}_{objectQualifier}_forum_ns_before_insert_tr ON {databaseSchema}.{objectQualifier}forum_ns;
--GO
DROP TRIGGER IF EXISTS {databaseSchema}_{objectQualifier}forum_before_update_tr ON {databaseSchema}.{objectQualifier}forum;
--GO
-- Initialize all this
CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}forum_ns_recreate()
				  RETURNS void AS
$BODY$
BEGIN
DROP TRIGGER IF EXISTS {databaseSchema}_{objectQualifier}_forum_ns_before_insert_tr ON {databaseSchema}.{objectQualifier}forum_ns;

CREATE TRIGGER {databaseSchema}_{objectQualifier}_forum_ns_before_insert_tr
  BEFORE INSERT
  ON {databaseSchema}.{objectQualifier}forum_ns
  FOR EACH ROW
  EXECUTE PROCEDURE {databaseSchema}.{objectQualifier}forum_ns_before_insert_func();
PERFORM {databaseSchema}.{objectQualifier}create_or_check_ns_tables();
PERFORM {databaseSchema}.{objectQualifier}fillin_or_check_ns_tables();

END;
$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER CALLED ON NULL INPUT
  COST 100;  
  	--GO 


-- Initialize all this
CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}forum_ns_recreate_ini()
				  RETURNS void AS
$BODY$
BEGIN
IF EXISTS (select 1 from {databaseSchema}.{objectQualifier}forum where left_key is null  limit 1) THEN
PERFORM {databaseSchema}.{objectQualifier}forum_ns_recreate();
END IF;
END;
$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER CALLED ON NULL INPUT
  COST 100;  
--GO 
SELECT {databaseSchema}.{objectQualifier}forum_ns_recreate_ini();
--GO
 
/* CREATE TRIGGER {databaseSchema}_{objectQualifier}forum_before_update_tr
  BEFORE UPDATE
  ON {databaseSchema}.{objectQualifier}forum
  FOR EACH ROW
  EXECUTE PROCEDURE {databaseSchema}.{objectQualifier}forum_before_update_func();


 DO $$ 
BEGIN  
ALTER TABLE {databaseSchema}.{objectQualifier}forum_ns enable trigger user;
END$$;
-- GO */