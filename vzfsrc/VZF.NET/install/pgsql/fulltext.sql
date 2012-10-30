

CREATE OR REPLACE FUNCTION databaseSchema.objectQualifier_fts_column_add1(culture2 varchar(50),createIndex boolean )
                  RETURNS void AS
$BODY$
DECLARE _rec RECORD;
BEGIN
IF (NOT column_exists('databaseSchema.objectQualifier_message','message_fts_' || culture2)) THEN
--create a new column 
EXECUTE 'ALTER TABLE databaseSchema.objectQualifier_message ADD COLUMN message_fts_' || culture2 || ' tsvector;';
-- fill the column with existing ts_vectors
FOR _rec IN select * from databaseSchema.objectQualifier_message 
LOOP   
EXECUTE 'UPDATE databaseSchema.objectQualifier_message SET message_fts_' || culture2 || ' =
     to_tsvector((''' || culture2 || ''')::regconfig, ''' || _rec.message || ''') where messageid = ' || _rec.messageid || ' ;';
END LOOP;
if (createIndex is true AND 
 (NOT EXISTS (SELECT 1 FROM pg_indexes WHERE tablename='objectQualifier_message' AND indexname='idx_databaseSchema_objectQualifier_message_' || culture2 || ''))) then
EXECUTE 'CREATE INDEX idx_databaseSchema_objectQualifier_message_' || culture2 || ' ON databaseSchema.objectQualifier_message USING gin(message_fts_' || culture2 || ');';
end if;
end if;
end;
$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER STRICT
  COST 100;   
--GO 
SELECT  databaseSchema.objectQualifier_fts_column_add1('russian', true);
--GO

/* CREATE OR REPLACE FUNCTION databaseSchema.objectQualifier_fts_column_add(culture2 varchar(50),createIndex boolean )
                  RETURNS void AS
$BODY$
DECLARE _rec RECORD;
BEGIN
EXECUTE 'DROP TRIGGER IF EXISTS databaseSchema_objectQualifier_tsvectorupdate_' || culture2 || ' ON databaseSchema.objectQualifier_message;';
EXECUTE 'CREATE FUNCTION tsvector_update_trigger_' || culture2 || ' RETURNS TRIGGER AS $$
BEGIN
    IF TG_OP = ''INSERT'' THEN
        new.search_vector = to_tsvector(''pg_catalog.'' || culture2 || ', COALESCE(NEW.name, ''''));
    END IF;
    IF TG_OP = 'UPDATE' THEN
        IF NEW.name <> OLD.name THEN
            new.search_vector = to_tsvector('pg_catalog.' || culture2 || ', COALESCE(NEW.name, ''''));
        END IF;
    END IF;
    RETURN NEW;
END;
$$ LANGUAGE ''plpgsql'';';

EXECUTE 'CREATE TRIGGER databaseSchema_objectQualifier_tsvectorupdate_' || culture2 || '  BEFORE INSERT OR UPDATE
ON databaseSchema.objectQualifier_message FOR EACH ROW EXECUTE PROCEDURE
tsvector_update_trigger_' || culture2 || '(message_fts_' || culture2 || ', ' || 'pg_catalog.' || culture2 || ', message);'; 
END;
$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER STRICT
  COST 100;   

SELECT  databaseSchema.objectQualifier_fts_column_add('english', true);

 SELECT title
FROM pgweb
WHERE textsearchable_index_col @@ to_tsquery('create & table')
ORDER BY last_mod_date DESC
LIMIT 10; */