DROP  PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}add_fts;
--GO
CREATE PROCEDURE {databaseSchema}.{objectQualifier}add_fts()
BEGIN

SET foreign_key_checks=0; 
alter table {databaseSchema}.{objectQualifier}Message ADD FULLTEXT KEY {objectQualifier}fts_messagekey (`Message`,`Description`);
alter table {databaseSchema}.{objectQualifier}Topic ADD FULLTEXT KEY {objectQualifier}fts_topickey (`Topic`,`Description`);

/* ADD  INDEXES AT FIRST INSTALL */


IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.STATISTICS S WHERE S.TABLE_SCHEMA='{databaseSchema}' AND S.INDEX_NAME='IX_{databaseSchema}_{objectQualifier}Message_Fts' AND S.TABLE_NAME='{objectQualifier}Message' LIMIT 1) THEN
 
ALTER TABLE {databaseSchema}.{objectQualifier}Message
CREATE FULLTEXT INDEX  `IX_{databaseSchema}_{objectQualifier}Message_Fts` (`Message`,`Description`);
SET foreign_key_checks=1; 
END IF;
IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.STATISTICS S WHERE S.TABLE_SCHEMA='{databaseSchema}' AND S.INDEX_NAME='IX_{databaseSchema}_{objectQualifier}Topic_Fts' AND S.TABLE_NAME='{objectQualifier}Topic' LIMIT 1) THEN
 
ALTER TABLE {databaseSchema}.{objectQualifier}Message
CREATE FULLTEXT INDEX  `IX_{databaseSchema}_{objectQualifier}Topic_Fts` (`Topic`,`Description`);
SET foreign_key_checks=1; 
END IF;

END;
--GO
CALL add_fts();
--GO

