﻿-- This scripts for PostgreSQL Yet Another Forum https://github.com/vzrus/YetAnotherForumExtraDataLayers http://sourceforge.net/projects/yafdotnet/
-- were created by vzrus from vz-team  https://github.com/vzrus
-- They are distributed under terms of GPLv2 licence only as in http://www.fsf.org/licensing/licenses/gpl.html
-- Copyright vzrus(c) 2009-2012

-- CREATE FUNCTION plpgsql_call_handler () RETURNS OPAQUE AS '/usr/local/pgsql/lib/plpgsql' LANGUAGE C;
-- CREATE TRUSTED LANGUAGE plpgsql HANDLER plpgsql_call_handler;

/* DO $$
declare r record;
begin
for r in
select constraint_name, table_schema, table_name from INFORMATION_SCHEMA.TABLE_CONSTRAINTS where constraint_schema ='myscheme' and constraint_type ='FOREIGN KEY'
loop
execute 'ALTER table ' || r.table_schema ||'.' || r.table_name || ' DROP CONSTRAINT ' || r.constraint_name || ' CASCADE;';
end loop;
end$$;

 DO $$
declare r record;
begin
for r in
select constraint_name, table_schema, table_name from INFORMATION_SCHEMA.TABLE_CONSTRAINTS where constraint_schema ='myscheme' and constraint_type ='PRIMARY KEY'
loop
execute 'ALTER table ' || r.table_schema ||'.' || r.table_name || ' DROP CONSTRAINT ' || r.constraint_name || ' CASCADE;';
end loop;
end$$;

 DO $$
declare r record;
begin
for r in
select constraint_name, table_schema, table_name from INFORMATION_SCHEMA.TABLE_CONSTRAINTS where constraint_schema ='myscheme' and constraint_type ='PRIMARY KEY'
loop
execute 'DROP INDEX [ CONCURRENTLY ] [ IF EXISTS ] name [, ...] [ CASCADE | RESTRICT ]ALTER table ' || r.table_schema ||'.' || r.table_name || ' DROP CONSTRAINT ' || r.constraint_name || ' CASCADE;';
end loop;
end$$; 

 --DO $$
declare r record;
begin
for r in
SELECT i.relname as indname,
       i.relowner as indowner,
       cast(idx.indrelid::regclass as varchar) as objectname,
       am.amname as indam,
       idx.indkey,
       ARRAY(
       SELECT pg_get_indexdef(idx.indexrelid, k + 1, true)
       FROM generate_subscripts(idx.indkey, 1) as k
       ORDER BY k
       ) as indkey_names,
       idx.indexprs IS NOT NULL as indexprs,
       idx.indpred IS NOT NULL as indpred
FROM   pg_index as idx
JOIN   pg_class as i
ON     i.oid = idx.indexrelid
JOIN   pg_am as am
ON     i.relam = am.oid
where cast(idx.indrelid::regclass as varchar) like 'myscheme.%'
loop
execute 'DROP INDEX IF EXISTS myscheme.' || r.indname || ' CASCADE;';
end loop;
end$$; */

CREATE OR REPLACE FUNCTION {databaseSchema}.f_delfunc(_schema text, _del text)
 RETURNS text AS
$BODY$
DECLARE
    _sql   text;
    _ct    text;

BEGIN
   SELECT INTO _sql, _ct
          string_agg('DROP '
                   || CASE p.proisagg WHEN true THEN 'AGGREGATE '
                                                ELSE 'FUNCTION ' END
                   || quote_ident(n.nspname) || '.' || quote_ident(p.proname)
                   || '('
                   || pg_catalog.pg_get_function_identity_arguments(p.oid)
                   || ') CASCADE;'
                  ,E'\n'
          )
          ,count(*)::text
   FROM   pg_catalog.pg_proc p
   LEFT   JOIN pg_catalog.pg_namespace n ON n.oid = p.pronamespace
   WHERE  n.nspname = _schema;
   -- AND p.proname ~~* 'f_%';                     -- Only selected funcs?
   -- AND pg_catalog.pg_function_is_visible(p.oid) -- Only visible funcs?
IF  _sql is not null THEN   
IF lower(_del) = 'del' THEN                        -- Actually delete!
   EXECUTE _sql;
   RETURN _ct || E' functions deleted:\n' || _sql;
ELSE                                               -- Else only show SQL.
   RETURN _ct || E' functions to delete:\n' || _sql;
END IF;
END IF;
RETURN NULL;
END;
$BODY$ LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER COST 100
--GO
SELECT {databaseSchema}.f_delfunc('{databaseSchema}','del');
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.f_deltype(_schema text, _del text)
 RETURNS text AS
$BODY$
DECLARE
    _sql   text;
    _ct    text;

BEGIN
   SELECT INTO _sql, _ct
          string_agg('DROP TYPE '   
                   || quote_ident(n.nspname) || '.' || quote_ident(t.typname)
                   || ' CASCADE;'                  
                  ,E'\n'
          )
          ,count(*)::text
FROM        pg_type t 
LEFT JOIN   pg_catalog.pg_namespace n ON n.oid = t.typnamespace 
WHERE       (t.typrelid = 0 OR (SELECT c.relkind = 'c' FROM pg_catalog.pg_class c WHERE c.oid = t.typrelid)) 
AND     NOT EXISTS(SELECT 1 FROM pg_catalog.pg_type el WHERE el.oid = t.typelem AND el.typarray = t.oid)
AND     n.nspname NOT IN ('pg_catalog', 'information_schema');
IF  _sql is not null THEN   
IF lower(_del) = 'del' THEN                        -- Actually delete!
   EXECUTE _sql;
   RETURN _ct || E' functions deleted:\n' || _sql;
ELSE                                               -- Else only show SQL.
   RETURN _ct || E' functions to delete:\n' || _sql;
END IF;
END IF;
RETURN NULL;
END;
$BODY$ LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER COST 100
--GO
SELECT {databaseSchema}.f_deltype('{databaseSchema}','del');
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}int_to_bool_helper(reqvalue double precision)
  RETURNS boolean AS
$BODY$
BEGIN
IF  reqvalue >= 1 THEN RETURN TRUE;
ELSE RETURN FALSE;
END IF;
END;$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100;
--GO

-- This is helper function which drops all types. Don't forget to remove it in the end 
-- It was put here to ensure that it will be ready before any type creation

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}drop_type
(
	varchar(100), 
	varchar(100)
) returns int4 
as '
declare
	_schemename alias for $1;
	_typename alias for $2;
	_rowcount int4;

begin

_rowcount := 0;
	
if exists(select 1 FROM pg_type INNER JOIN pg_namespace ON pg_type.typnamespace =pg_namespace.oid where pg_namespace.nspname=_schemename AND typname=_typename)  then
	EXECUTE ''DROP TYPE '' || _schemename ||''.'' || _typename || '' CASCADE;'';
	GET DIAGNOSTICS _rowcount = ROW_COUNT;
end if;
return _rowcount; 
end'
 LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100;
GRANT EXECUTE ON FUNCTION {databaseSchema}.{objectQualifier}drop_type(varchar,varchar) TO public;

--GO

-- This is helper function which checks if pkeys and fkeys exist. Don't forget to remove it in the end 
-- It was put here to ensure that we don't create double constraints.

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}check_or_create_keys
(
	varchar(100), 
	varchar(100),
	varchar(100),
	varchar(100),
	varchar(100),
	varchar(100)
) returns int4 
as '
declare
	_conschema alias for $1;
	_contable alias for $2;
	_altertable alias for $3;
	_conname alias for $4; 
	_concolumn alias for $5;       
	_contype alias for $6;
	
	_rowcount int4;

begin

_rowcount := 0;

IF _contype = ''p'' THEN
IF NOT EXISTS (SELECT * FROM pg_constraint where contype=_contype and conname =_conname ) THEN
EXECUTE ''ALTER TABLE ONLY  '' || _conschema || ''.'' || _altertable ||
	 '' ADD CONSTRAINT  '' || _conname || '' PRIMARY KEY  ( '' || _concolumn || '');'';
END IF;
END IF;

IF _contype = ''f'' THEN
IF NOT EXISTS(SELECT 1 FROM pg_constraint where contype=_contype and conname =_conname) THEN
EXECUTE ''ALTER TABLE ONLY  '' || _conschema || ''.'' || _altertable ||
	 '' ADD CONSTRAINT '' || _conname || 
	'' FOREIGN KEY ( '' || _concolumn || '') '' ||  
	'' REFERENCES '' || _conschema || ''.'' || _contable || '' ( '' || _concolumn || '');'';
END IF;
END IF;
GET DIAGNOSTICS _rowcount = ROW_COUNT;
RETURN _rowcount; 
END'
 LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100;
  --GO
GRANT EXECUTE ON FUNCTION {databaseSchema}.{objectQualifier}check_or_create_keys(varchar,varchar,varchar,varchar,varchar,varchar) TO public;

--GO


-- This is helper function which check if indexes exists and creates them if not. 
-- It was put here to ensure that we don't create double indexes.
-- Don't forget to remove it in the end 
 

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}create_or_replace_index
(
	varchar(100), 
	varchar(100),
	varchar(100), 
	varchar(100),
	varchar(100)
) returns int4 
as '
declare
	_schemaname alias for $1;
	_tablename alias for $2;
	_indexname alias for $3;
	_indexuniue alias for $4;
	_indexcolumn alias for $5;
	_rowcount int4;

begin

_rowcount := 0;
	
IF NOT EXISTS (SELECT 1 FROM pg_indexes WHERE tablename=_tablename AND indexname=_indexname) THEN
IF _indexuniue=''unique'' THEN
EXECUTE ''CREATE UNIQUE INDEX '' || _indexname || '' ON '' || _schemaname || ''.'' || _tablename || '' USING btree ('' || _indexcolumn || '');'';
ELSE
EXECUTE ''CREATE INDEX '' || _indexname || '' ON '' || _schemaname || ''.'' || _tablename || '' USING btree ('' || _indexcolumn || '');'';
END IF;
END IF;

GET DIAGNOSTICS _rowcount = ROW_COUNT;
RETURN _rowcount; 
END'

 LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100;
  --GO
GRANT EXECUTE ON FUNCTION {databaseSchema}.{objectQualifier}create_or_replace_index(varchar, 
	varchar,
	varchar, 
	varchar,
	varchar) TO public;
--GO


CREATE OR REPLACE FUNCTION column_exists(tablename text, colname text)
RETURNS boolean AS
$BODY$
DECLARE
_myrow record;
BEGIN
FOR _myrow in (SELECT attname FROM pg_attribute WHERE attrelid = tablename::regclass)
LOOP
IF _myrow.attname = colname THEN
RETURN true;
EXIT;
END IF;
END LOOP;
RETURN false;
END;
$BODY$
LANGUAGE 'plpgsql' VOLATILE
--GO

SELECT {databaseSchema}.{objectQualifier}drop_type('{databaseSchema}','{objectQualifier}prov_profileusernamemigrate');
--GO
CREATE TYPE {databaseSchema}.{objectQualifier}prov_profileusernamemigrate AS
(
userid uuid,
username character varying(255)
);
--GO
-- Function: objectQualifier_board_delete(integer)

-- DROP FUNCTION objectQualifier_board_delete(integer);

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}fill_in_profileusername()
				  RETURNS void AS
$BODY$DECLARE 
userid_cursor uuid;
board_cursor refcursor; 
CurUserName character varying(255);
_rec {databaseSchema}.{objectQualifier}prov_profileusernamemigrate%ROWTYPE;
BEGIN
  DROP TABLE IF EXISTS tmp_migr_prof;
  CREATE TEMPORARY TABLE tmp_migr_prof(userid uuid, username varchar(255)) ON COMMIT DROP;
  INSERT INTO tmp_migr_prof (username,userid)
  SELECT   username,userid 
  FROM     {databaseSchema}.{objectQualifier}prov_membership
  WHERE    username ISNULL;

  OPEN board_cursor  FOR
  SELECT   userid
  FROM     {databaseSchema}.{objectQualifier}prov_profile 
  WHERE    username IS NULL; 

 LOOP
  FETCH board_cursor  INTO userid_cursor ;
  EXIT WHEN NOT FOUND;
 INSERT INTO tmp_migr_prof (username,userid)
  SELECT   username,userid 
  FROM     {databaseSchema}.{objectQualifier}prov_membership
  WHERE    userid = userid_cursor;
  EXIT WHEN NOT FOUND;
END LOOP;
  CLOSE board_cursor;
  FOR _rec IN SELECT * FROM tmp_migr_prof
  LOOP
  /*DEALLOCATE board_cursor;*/
   UPDATE {databaseSchema}.{objectQualifier}prov_profile SET username = _rec.username where userid = _rec.userid;
   END LOOP;
  END;
$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER
  COST 100; 
  --GO


