DROP  PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}add_fts;
--GO
CREATE PROCEDURE {databaseSchema}.{objectQualifier}add_fts()
BEGIN
DECLARE majorV INT; 
DECLARE minorV INT; 
select cast(substring(version(),1,cast( LOCATE('.',version()) -1 as char)) as signed) into majorV ;
select cast(substring(version(), cast( LOCATE('.',version()) + 1 as signed),cast( LOCATE('.',version()) -1 as char)) as signed) into minorV;
if majorV > 4 and minorV > 5  then
SET foreign_key_checks=0; 
-- requires super or a process privileges in the current version. Gives an error if the check is commented. Can't be resolved in the current mysql.
-- if not exists (SELECT t.name, i.index_id, i.table_id, i.name FROM INFORMATION_SCHEMA.INNODB_SYS_INDEXES i,INFORMATION_SCHEMA.INNODB_SYS_TABLES t  WHERE t.name like '%{objectQualifier}Message' and i.name like 'fts_doc_id_index%')
-- then 
alter table {databaseSchema}.{objectQualifier}Message ADD FULLTEXT KEY {objectQualifier}fts_messagekey (`Message`,`Description`);
-- end if;
-- if not exists (SELECT t.name, i.index_id, i.table_id, i.name FROM INFORMATION_SCHEMA.INNODB_SYS_INDEXES i,INFORMATION_SCHEMA.INNODB_SYS_TABLES t  WHERE t.name like '%{objectQualifier}Topic' and i.name like 'fts_doc_id_index%')
-- then 
alter table {databaseSchema}.{objectQualifier}Topic ADD FULLTEXT KEY {objectQualifier}fts_topickey (`Topic`,`Description`);
-- end if;
/* ADD  INDEXES AT FIRST INSTALL */

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.STATISTICS S WHERE S.TABLE_SCHEMA='{databaseSchema}' AND S.INDEX_NAME='IX_{databaseSchema}_{objectQualifier}Message_Fts' AND S.TABLE_NAME='{objectQualifier}Message' LIMIT 1) THEN
CREATE FULLTEXT INDEX  `IX_{databaseSchema}_{objectQualifier}Message_Fts` ON {databaseSchema}.{objectQualifier}Message(`Message`,`Description`);
END IF;
 IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.STATISTICS S WHERE S.TABLE_SCHEMA='{databaseSchema}' AND S.INDEX_NAME='IX_{databaseSchema}_{objectQualifier}Topic_Fts' AND S.TABLE_NAME='{objectQualifier}Topic' LIMIT 1) THEN
CREATE FULLTEXT INDEX  `IX_{databaseSchema}_{objectQualifier}Topic_Fts` ON {databaseSchema}.{objectQualifier}Topic(`Topic`,`Description`);

END IF;
SET foreign_key_checks=1;
END IF;
END;
--GO
CALL {databaseSchema}.{objectQualifier}add_fts();
--GO

