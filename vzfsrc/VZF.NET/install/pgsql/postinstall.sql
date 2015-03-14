-- This scripts for PostgreSQL Yet Another Forum https://github.com/vzrus/YetAnotherForumExtraDataLayers http://sourceforge.net/projects/yafdotnet/
-- were created by vzrus from vz-team  https://github.com/vzrus
-- They are distributed under terms of GPLv2 licence only as in http://www.fsf.org/licensing/licenses/gpl.html
-- Copyright vzrus(c) 2009-2012

-- We use the script file to ensure that all temporary install things are wiped out. ---
DROP FUNCTION {databaseSchema}.{objectQualifier}drop_type
(
	varchar(100), --: schemaname
	varchar(100) --: typename
);
--GO

DROP FUNCTION {databaseSchema}.{objectQualifier}check_or_create_keys
(
	varchar(100), 
	varchar(100),
	varchar(100),
	varchar(100),
	varchar(100),
	varchar(100)
);
--GO

DROP FUNCTION {databaseSchema}.{objectQualifier}create_or_replace_index
(
	varchar(100), 
	varchar(100),
	varchar(100),
	varchar(100),
	varchar(100)
);
--GO

/* since 9.1 SELECT
  schemaname, relname,
  last_vacuum, last_autovacuum,
  vacuum_count, autovacuum_count  -- not available on 9.0 and earlier
FROM pg_stat_user_tables; */

