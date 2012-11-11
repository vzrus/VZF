DO $$
BEGIN
IF NOT EXISTS (select 1 from pg_tables 
			   where schemaname='myscheme' 
			   AND tablename='rrr_message_fts' limit 1) THEN
CREATE TABLE myscheme.rrr_message_fts
			 (
			 messageid                    integer NOT NULL			
			 ) 
	   WITH (OIDS=false);
END IF;
END$$;

DO $$





END$$;