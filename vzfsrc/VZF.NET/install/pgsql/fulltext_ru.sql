CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}fts_column_add1(culture2 varchar(50),createIndex boolean )
                  RETURNS void AS
$BODY$
DECLARE _rec RECORD;
BEGIN
IF (NOT column_exists('{databaseSchema}.{objectQualifier}message','message_fts_' || culture2)) THEN
--create a new column 
EXECUTE 'ALTER TABLE {databaseSchema}.{objectQualifier}message ADD COLUMN message_fts_' || culture2 || ' tsvector;';
-- fill the column with existing ts_vectors
FOR _rec IN select * from {databaseSchema}.{objectQualifier}message 
LOOP   
EXECUTE 'UPDATE {databaseSchema}.{objectQualifier}message SET message_fts_' || culture2 || ' =
     to_tsvector((''' || culture2 || ''')::regconfig, ''' || _rec.message || ''') where messageid = ' || _rec.messageid || ' ;';
END LOOP;
if (createIndex is true AND 
 (NOT EXISTS (SELECT 1 FROM pg_indexes WHERE tablename='{objectQualifier}message' AND indexname='idx_{databaseSchema}_{objectQualifier}_message_' || culture2 || ''))) then
EXECUTE 'CREATE INDEX idx_{databaseSchema}_{objectQualifier}_message_' || culture2 || ' ON {databaseSchema}.{objectQualifier}message USING gin(message_fts_' || culture2 || ');';
end if;
end if;
end;
$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER STRICT
  COST 100;   
--GO 
SELECT  {databaseSchema}.{objectQualifier}fts_column_add1('russian', true);
--GO
DROP TRIGGER IF EXISTS objectQualifier_tr_message_fts_ru_update ON {databaseSchema}.{objectQualifier}message;
--GO
CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}message_fts_ru_update() RETURNS TRIGGER AS 
$BODY$
BEGIN
    IF TG_OP = 'INSERT' THEN
        new.message_fts_russian = to_tsvector('pg_catalog.russian', COALESCE(NEW.message, ''));
    END IF;
    IF TG_OP = 'UPDATE' THEN
        IF NEW.message <> OLD.message THEN
            new.message_fts_russian = to_tsvector('pg_catalog.russian', COALESCE(NEW.message, ''));
        END IF;
    END IF;
    RETURN NEW;
END
$BODY$ LANGUAGE 'plpgsql';
--GO

CREATE TRIGGER objectQualifier_tr_message_fts_ru_update AFTER INSERT OR UPDATE ON {databaseSchema}.{objectQualifier}message
FOR EACH ROW EXECUTE PROCEDURE {databaseSchema}.{objectQualifier}message_fts_ru_update();
--GO