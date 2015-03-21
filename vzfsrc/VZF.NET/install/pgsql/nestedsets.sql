/* ******************************************************************************************************************************
*********************************************************************************************************************************
CREATE NS TABLE AND INDEXES FUNCTIONS
*********************************************************************************************************************************
******************************************************************************************************************************** */

drop function if exists {databaseSchema}.{objectQualifier}forum_ns_after_delete_2_func();
--go
drop function if exists  {databaseSchema}.{objectQualifier}forum_ns_after_delete_func();
--go
drop function if exists {databaseSchema}.{objectQualifier}forum_ns_before_insert_func();
--go 
drop function if exists {databaseSchema}.{objectQualifier}forum_ns_before_insert_func();
--go
drop function if exists {databaseSchema}.{objectQualifier}forum_ns_before_update_func();
--go

DO 
$$
BEGIN
IF NOT EXISTS (select 1 from pg_tables 
			   where schemaname='{databaseSchema}' 
				 AND tablename='{objectQualifier}forum_ns' limit 1) THEN
CREATE TABLE {databaseSchema}.{objectQualifier}forum_ns
(
  nid serial NOT NULL,
  boardid integer NOT NULL,
  categoryid integer NOT NULL,
  forumid integer NOT NULL,
  left_key integer NOT NULL,
  right_key integer NOT NULL,
  "level" integer NOT NULL DEFAULT 0,
  tree integer NOT NULL DEFAULT 0,
  parentid integer NOT NULL DEFAULT 0,  
  _trigger_lock_update boolean NOT NULL DEFAULT false,
  _trigger_for_delete boolean NOT NULL DEFAULT false, 
  sortorder integer NOT NULL DEFAULT 0,
  path_cache character varying(1024),
  CONSTRAINT pk_{databaseSchema}_{objectQualifier}ns_tree_pkey PRIMARY KEY (nid)
)
WITH 
  (OIDS={withOIDs}); 
END IF;
END
$$;
--GO
DO $$
BEGIN
IF EXISTS (SELECT 1 FROM pg_constraint 
			   where contype='p' 
				 and conname ='databaseschema_{objectQualifier}_ns_tree_pkey' LIMIT 1) THEN
   ALTER TABLE ONLY {databaseSchema}.{objectQualifier}forum_ns
   DROP CONSTRAINT databaseschema_{objectQualifier}_ns_tree_pkey cascade;
END IF;
END
$$;
--GO


DO $$
BEGIN
IF EXISTS (SELECT 1 FROM pg_constraint 
			   where contype='p' 
				 and conname ='{databaseSchema}_{objectQualifier}ns_tree_pkey' LIMIT 1) THEN
   ALTER TABLE ONLY {databaseSchema}.{objectQualifier}forum_ns
   DROP CONSTRAINT {databaseSchema}_{objectQualifier}ns_tree_pkey cascade;
END IF;
END
$$;
--GO


DO $$
BEGIN
IF NOT EXISTS (SELECT 1 FROM pg_constraint 
			   where contype='p' 
				 and conname ='pk_{databaseSchema}_{objectQualifier}ns_tree_pkey' LIMIT 1) THEN
   ALTER TABLE ONLY {databaseSchema}.{objectQualifier}forum_ns
   ADD CONSTRAINT pk_{databaseSchema}_{objectQualifier}ns_tree_pkey PRIMARY KEY (nid);
END IF;
END
$$;
--GO
DO
$$
BEGIN
 IF NOT EXISTS (SELECT 1 FROM pg_indexes WHERE tablename='{objectQualifier}forum' AND indexname='{databaseSchema}_{objectQualifier}forum_lkey_rkey_level_tree_idx') THEN
CREATE INDEX {databaseSchema}_{objectQualifier}forum_lkey_rkey_level_tree_idx
  ON {databaseSchema}.{objectQualifier}forum
  USING btree
  (left_key, right_key, level, categoryid);
END IF;

IF NOT EXISTS (SELECT 1 FROM pg_indexes WHERE tablename='{objectQualifier}forum' AND indexname='{databaseSchema}_{objectQualifier}forum_parentid_idx') THEN
CREATE INDEX {databaseSchema}_{objectQualifier}forum_parentid_idx
  ON {databaseSchema}.{objectQualifier}forum
  USING btree
  (parentid);
END IF;
END
$$;
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}create_or_check_ns_tables()
				  RETURNS void AS
$BODY$
BEGIN
-- td EXECUTE 'ALTER TABLE {databaseSchema}.{objectQualifier}forum_ns disable trigger user;';
truncate table {databaseSchema}.{objectQualifier}forum_ns;
-- td EXECUTE 'ALTER TABLE {databaseSchema}.{objectQualifier}forum_ns enable trigger user;';
END;
$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER CALLED ON NULL INPUT
  COST 100;
	-- GRANT EXECUTE ON FUNCTION {databaseSchema}.{objectQualifier}create_or_check_tables() TO public;
	--GO
  
--    DROP FUNCTION {databaseSchema}.{objectQualifier}create_or_check_ns_tables();
-- GO


/* ******************************************************************************************************************************
*********************************************************************************************************************************
CORE NS TRIGGER FUNCTIONS
*********************************************************************************************************************************
******************************************************************************************************************************** */
CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}lock_tree(i_categoryid integer)
  RETURNS boolean AS
$BODY$
DECLARE _id INTEGER;
BEGIN
	SELECT forumid
		INTO _id
		FROM {databaseSchema}.{objectQualifier}forum
		WHERE categoryid = i_categoryid FOR UPDATE;
	RETURN TRUE;
END;
$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100;
--GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}lock_ns_tree(tree_id integer)
  RETURNS boolean AS
$BODY$
DECLARE	_id INTEGER;
BEGIN
	SELECT nid
		INTO _id
		FROM {databaseSchema}.{objectQualifier}forum_ns
		WHERE tree = tree_id FOR UPDATE;
	RETURN TRUE;
END;
$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100;
--GO


-- here we don't delete children but move them one level higher and remove keys gap.
CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}forum_after_del_2_func()
  RETURNS trigger AS
$BODY$
DECLARE
BEGIN
	PERFORM {databaseSchema}.{objectQualifier}lock_tree(OLD.categoryid);
-- Убираем разрыв в ключах и сдвигаем дочерние узлы:
   UPDATE {databaseSchema}.{objectQualifier}forum
		SET left_key = CASE WHEN left_key < OLD.left_key
							THEN left_key
							ELSE CASE WHEN right_key < OLD.right_key
									  THEN left_key - 1 
									  ELSE left_key - 2
								 END
					   END,
			"level" = CASE WHEN right_key < OLD.right_key
						   THEN "level" - 1 
						   ELSE "level"
					  END,
			parentid = CASE WHEN right_key < OLD.right_key AND "level" = OLD.level + 1
						   THEN OLD.parentid
						   ELSE parentid
						END,
			right_key = CASE WHEN right_key < OLD.right_key
							 THEN right_key - 1 
							 ELSE right_key - 2
						END,
			_trigger_lock_update = TRUE
		WHERE (right_key > OLD.right_key OR
			(left_key > OLD.left_key AND right_key < OLD.right_key)) AND
			categoryid = OLD.categoryid;
	RETURN OLD;
END;
$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100;
  --GO


-- Here we delete subtrees no need here 


CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}forum_after_del_func()
  RETURNS trigger AS
$BODY$
DECLARE
	_skew_tree INTEGER;
BEGIN
	PERFORM {databaseSchema}.{objectQualifier}lock_tree(OLD.categoryid);
-- should we trigger it?
	IF OLD._trigger_for_delete = TRUE THEN RETURN OLD; END IF;
-- tick children to delete
	UPDATE {databaseSchema}.{objectQualifier}forum
		SET _trigger_for_delete = TRUE,
			_trigger_lock_update = TRUE
		WHERE
			categoryid = OLD.categoryid AND
			left_key > OLD.left_key AND
			right_key < OLD.right_key;
-- delete ticked children
	DELETE FROM {databaseSchema}.{objectQualifier}forum
		WHERE
			categoryid = OLD.categoryid AND
			left_key > OLD.left_key AND
			right_key < OLD.right_key;
-- delete a key gap
	_skew_tree := OLD.right_key - OLD.left_key + 1;
	UPDATE {databaseSchema}.{objectQualifier}forum
		SET left_key = CASE WHEN left_key > OLD.left_key
							THEN left_key - _skew_tree
							ELSE left_key
					   END,
			right_key = right_key - _skew_tree,
			_trigger_lock_update = TRUE
		WHERE right_key > OLD.right_key AND
			categoryid = OLD.categoryid;
	RETURN OLD;
END;
$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100;
 --GO 

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}forum_ns_before_insert_func()
  RETURNS trigger AS
$BODY$
DECLARE
	_left_key       INTEGER;
	_level          INTEGER;
	_tmp_left_key   INTEGER;
	_tmp_right_key  INTEGER;
	_tmp_level      INTEGER;
	_tmp_id         INTEGER;
	_tmp_parent_id  INTEGER;
BEGIN
	PERFORM {databaseSchema}.{objectQualifier}lock_ns_tree(NEW.tree);
-- Нельзя эти поля ручками ставить:
	NEW._trigger_for_delete := FALSE;
	NEW._trigger_lock_update := FALSE;
	_left_key := 0;
	_level := 0;
-- Если мы указали родителя:
	IF NEW.parentid IS NOT NULL AND NEW.parentid > 0 THEN
		SELECT right_key, "level" + 1
			INTO _left_key, _level
			FROM {databaseSchema}.{objectQualifier}forum_ns
			WHERE nid = NEW.parentid AND
				  tree = NEW.tree;
	END IF;
-- Если мы указали левый ключ:
	IF NEW.left_key IS NOT NULL AND
	   NEW.left_key > 0 AND 
	   (_left_key IS NULL OR _left_key = 0) THEN
		SELECT nid, left_key, right_key, "level", parentid 
			INTO _tmp_id, _tmp_left_key, _tmp_right_key, _tmp_level, _tmp_parent_id
			FROM {databaseSchema}.{objectQualifier}forum_ns
			WHERE tree = NEW.tree AND (left_key = NEW.left_key OR right_key = NEW.left_key);
		IF _tmp_left_key IS NOT NULL AND _tmp_left_key > 0 AND NEW.left_key = _tmp_left_key THEN
			NEW.parentid := _tmp_parent_id;
			_left_key := NEW.left_key;
			_level := _tmp_level;
		ELSIF _tmp_left_key IS NOT NULL AND _tmp_left_key > 0 AND NEW.left_key = _tmp_right_key THEN
			NEW.parentid := _tmp_id;
			_left_key := NEW.left_key;
			_level := _tmp_level + 1;
		END IF;
	END IF;
-- Если родитель или левый ключ не указан, или мы ничего не нашли:
	IF _left_key IS NULL OR _left_key = 0 THEN
		SELECT MAX(right_key) + 1
			INTO _left_key
			FROM {databaseSchema}.{objectQualifier}forum_ns
			WHERE tree = NEW.tree;
		IF _left_key IS NULL OR _left_key = 0 THEN
			_left_key := 1;
		END IF;
		_level := 0;
		NEW.parentid := 0; 
	END IF;
-- Устанавливаем полученные ключи для узла:
	NEW.left_key := _left_key;
	NEW.right_key := _left_key + 1;
	NEW."level" := _level;
-- Формируем развыв в дереве на месте вставки:
	UPDATE {databaseSchema}.{objectQualifier}forum_ns
		SET left_key = left_key + 
			(CASE WHEN left_key >= _left_key 
			  THEN 2 
			  ELSE 0 
			END),
			right_key = right_key + 2,
			_trigger_lock_update = TRUE
		WHERE tree = NEW.tree AND right_key >= _left_key;
	RETURN NEW;
END;
$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100;
  --GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}forum_before_insert_func()
  RETURNS trigger AS
$BODY$
DECLARE
	_left_key       INTEGER;
	_level          INTEGER;
	_tmp_left_key   INTEGER;
	_tmp_right_key  INTEGER;
	_tmp_level      INTEGER;
	_tmp_id         INTEGER;
	_tmp_parent_id  INTEGER;
BEGIN    
	PERFORM {databaseSchema}.{objectQualifier}lock_tree(NEW.categoryid);
    -- Нельзя эти поля ручками ставить:
	NEW._trigger_for_delete := FALSE;
	NEW._trigger_lock_update := FALSE;
	_left_key := 0;
	_level := 0;
-- Если мы указали родителя:
	IF NEW.parentid IS NOT NULL AND NEW.parentid > 0 THEN
		SELECT right_key, "level" + 1
			INTO _left_key, _level
			FROM {databaseSchema}.{objectQualifier}forum
			WHERE forumid = NEW.parentid AND
				  categoryid = NEW.categoryid;
	END IF;
-- Если мы указали левый ключ:
	IF NEW.left_key IS NOT NULL AND
	   NEW.left_key > 0 AND 
	   (_left_key IS NULL OR _left_key = 0) THEN
		SELECT forumid, left_key, right_key, "level", parentid 
			INTO _tmp_id, _tmp_left_key, _tmp_right_key, _tmp_level, _tmp_parent_id
			FROM {databaseSchema}.{objectQualifier}forum
			WHERE categoryid = NEW.categoryid AND (left_key = NEW.left_key OR right_key = NEW.left_key);
		IF _tmp_left_key IS NOT NULL AND _tmp_left_key > 0 AND NEW.left_key = _tmp_left_key THEN
			NEW.parentid := _tmp_parent_id;
			_left_key := NEW.left_key;
			_level := _tmp_level;
		ELSIF _tmp_left_key IS NOT NULL AND _tmp_left_key > 0 AND NEW.left_key = _tmp_right_key THEN
			NEW.parentid := _tmp_id;
			_left_key := NEW.left_key;
			_level := _tmp_level + 1;
		END IF;
	END IF;
-- Если родитель или левый ключ не указан, или мы ничего не нашли:
	IF _left_key IS NULL OR _left_key = 0 THEN
		SELECT MAX(right_key) + 1
			INTO _left_key
			FROM {databaseSchema}.{objectQualifier}forum
			WHERE categoryid = NEW.categoryid;
		IF _left_key IS NULL OR _left_key = 0 THEN
			_left_key := 1;
		END IF;
		_level := 0;
		NEW.parentid := 0; 
	END IF;
-- Устанавливаем полученные ключи для узла:
	NEW.left_key := _left_key;
	NEW.right_key := _left_key + 1;
	NEW."level" := _level;
-- Формируем развыв в дереве на месте вставки:
	UPDATE {databaseSchema}.{objectQualifier}forum
		SET left_key = left_key + 
			(CASE WHEN left_key >= _left_key 
			  THEN 2 
			  ELSE 0 
			END),
			right_key = right_key + 2,
			_trigger_lock_update = TRUE
		WHERE categoryid = NEW.categoryid AND right_key >= _left_key;
RETURN NEW;
END;
$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100;
  --GO

CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}forum_before_update_func()
  RETURNS trigger AS
$BODY$
DECLARE
    _left_key       INTEGER;
    _level          INTEGER;
    _skew_tree      INTEGER;
    _skew_level     INTEGER;
    _skew_edit      INTEGER;
    _tmp_left_key   INTEGER;
    _tmp_right_key  INTEGER;
    _tmp_level      INTEGER;
    _tmp_id         INTEGER;
    _tmp_parent_id  INTEGER;
BEGIN
    PERFORM {databaseSchema}.{objectQualifier}lock_tree(OLD.categoryid);
-- А стоит ли нам вообще что либо делать:
    IF NEW._trigger_lock_update = TRUE THEN
        NEW._trigger_lock_update := FALSE;
        IF NEW._trigger_for_delete = TRUE THEN
            NEW = OLD;
            NEW._trigger_for_delete = TRUE;
            RETURN NEW;
        END IF;
        RETURN NEW;
    END IF;
-- Сбрасываем значения полей, которые пользователь менять не может:
    NEW._trigger_for_delete := FALSE;
    NEW.categoryid := OLD.categoryid;
    NEW.right_key := OLD.right_key;
    NEW."level" := OLD."level";

-- Проверяем, а есть ли изменения связанные со структурой дерева
    IF NEW.parentid = OLD.parentid AND NEW.left_key = OLD.left_key
    THEN
        RETURN NEW;
    END IF;
-- Дерево таки перестраиваем, что ж, приступим:
    _left_key := 0;
    _level := 0;
    _skew_tree := OLD.right_key - OLD.left_key + 1;
-- Определяем куда мы его переносим:
-- Если сменен parentid:
    IF NEW.parentid <> OLD.parentid THEN
-- Если в подчинение другому злу:
        IF NEW.parentid is not null THEN
            SELECT right_key, "level" + 1
                INTO _left_key, _level
                FROM {databaseSchema}.{objectQualifier}forum
                WHERE forumid = NEW.parentid AND categoryid = NEW.categoryid;
-- Иначе в корень дерева переносим:
        ELSE
            SELECT MAX(right_key) + 1 
                INTO _left_key
                FROM {databaseSchema}.{objectQualifier}forum
                WHERE categoryid = NEW.categoryid;
            _level := 0;
        END IF;
-- Если вдруг родитель в диапазоне перемещаемого узла, проверка:
        IF _left_key IS NOT NULL AND 
           _left_key > 0 AND
           _left_key > OLD.left_key AND
           _left_key <= OLD.right_key 
        THEN
           NEW.parentid := OLD.parentid;
           NEW.left_key := OLD.left_key;
           RETURN NEW;
        END IF;
    END IF;
-- Если же указан left_key, а parentid - нет
   IF _left_key IS NULL OR _left_key = 0 THEN
        SELECT forumid, left_key, right_key, "level", parentid 
            INTO _tmp_id, _tmp_left_key, _tmp_right_key, _tmp_level, _tmp_parent_id
            FROM {databaseSchema}.{objectQualifier}forum
            WHERE categoryid = NEW.categoryid AND (right_key = NEW.left_key OR right_key = NEW.left_key - 1)
            LIMIT 1;
        IF _tmp_left_key IS NOT NULL AND _tmp_left_key > 0 AND NEW.left_key - 1 = _tmp_right_key THEN
            NEW.parentid := _tmp_parent_id;
            _left_key := NEW.left_key;
            _level := _tmp_level;
        ELSIF _tmp_left_key IS NOT NULL AND _tmp_left_key > 0 AND NEW.left_key = _tmp_right_key THEN
            NEW.parentid := _tmp_id;
            _left_key := NEW.left_key;
            _level := _tmp_level + 1;
        ELSIF NEW.left_key = 1 THEN
            NEW.parentid := null;
            _left_key := NEW.left_key;
            _level := 0;
        ELSE
           NEW.parentid := OLD.parentid;
           NEW.left_key := OLD.left_key;
           RETURN NEW;
        END IF;
    END IF; 
-- Теперь мы знаем куда мы перемещаем дерево
        _skew_level := _level - OLD."level";
    IF _left_key > OLD.left_key THEN
-- Перемещение вверх по дереву
        _skew_edit := _left_key - OLD.left_key - _skew_tree;
        UPDATE {databaseSchema}.{objectQualifier}forum
            SET left_key =  CASE WHEN right_key <= OLD.right_key
                                 THEN left_key + _skew_edit
                                 ELSE CASE WHEN left_key > OLD.right_key
                                           THEN left_key - _skew_tree
                                           ELSE left_key
                                      END
                            END,
                "level" =   CASE WHEN right_key <= OLD.right_key 
                                 THEN "level" + _skew_level
                                 ELSE "level"
                            END,
                right_key = CASE WHEN right_key <= OLD.right_key 
                                 THEN right_key + _skew_edit
                                 ELSE CASE WHEN right_key < _left_key
                                           THEN right_key - _skew_tree
                                           ELSE right_key
                                      END
                            END,
                _trigger_lock_update = TRUE
            WHERE categoryid = OLD.categoryid AND
                  right_key > OLD.left_key AND
                  left_key < _left_key AND
                  forumid <> OLD.forumid;
        _left_key := _left_key - _skew_tree;
    ELSE
-- Перемещение вниз по дереву:
        _skew_edit := _left_key - OLD.left_key;
        UPDATE {databaseSchema}.{objectQualifier}forum
            SET
                right_key = CASE WHEN left_key >= OLD.left_key
                                 THEN right_key + _skew_edit
                                 ELSE CASE WHEN right_key < OLD.left_key
                                           THEN right_key + _skew_tree
                                           ELSE right_key
                                      END
                            END,
                "level" =   CASE WHEN left_key >= OLD.left_key
                                 THEN "level" + _skew_level
                                 ELSE "level"
                            END,
                left_key =  CASE WHEN left_key >= OLD.left_key
                                 THEN left_key + _skew_edit
                                 ELSE CASE WHEN left_key >= _left_key
                                           THEN left_key + _skew_tree
                                           ELSE left_key
                                      END
                            END,
                 _trigger_lock_update = TRUE
            WHERE categoryid = OLD.categoryid AND
                  right_key >= _left_key AND
                  left_key < OLD.right_key AND
                  forumid <> OLD.forumid;
    END IF;
-- Дерево перестроили, остался только наш текущий узел
    NEW.left_key := _left_key;
    NEW."level" := _level;
    NEW.right_key := _left_key + _skew_tree - 1;
    RETURN NEW;
END;
$BODY$
  LANGUAGE 'plpgsql' VOLATILE
  COST 100;
--GO  


-- nested sets
DROP TRIGGER IF EXISTS {databaseSchema}_{objectQualifier}forum_after_del_tr ON {databaseSchema}.{objectQualifier}forum;
--GO
  CREATE TRIGGER {databaseSchema}_{objectQualifier}forum_after_del_tr
  AFTER DELETE
  ON {databaseSchema}.{objectQualifier}forum
  FOR EACH ROW
  EXECUTE PROCEDURE {databaseSchema}.{objectQualifier}forum_after_del_func();
--GO 
DROP TRIGGER IF EXISTS {databaseSchema}_{objectQualifier}forum_ns_after_delete_2_tr ON {databaseSchema}.{objectQualifier}forum;
-- GO
-- we don't use the trigger with moving children a level higher
 /* CREATE TRIGGER {databaseSchema}_{objectQualifier}forum_after_del2_tr
  AFTER DELETE
  ON {databaseSchema}.{objectQualifier}forum
  FOR EACH ROW
  EXECUTE PROCEDURE {databaseSchema}.{objectQualifier}forum_after_del2_func();*/
-- GO 

DROP TRIGGER IF EXISTS {databaseSchema}_{objectQualifier}_forum_ns_before_insert_tr ON {databaseSchema}.{objectQualifier}forum;
--GO
CREATE TRIGGER {databaseSchema}_{objectQualifier}_forum_ns_before_insert_tr
  BEFORE INSERT
  ON {databaseSchema}.{objectQualifier}forum_ns
  FOR EACH ROW
  EXECUTE PROCEDURE {databaseSchema}.{objectQualifier}forum_ns_before_insert_func();
--GO 

DROP TRIGGER IF EXISTS {databaseSchema}_{objectQualifier}forum_before_insert_tr ON {databaseSchema}.{objectQualifier}forum;
--GO
CREATE TRIGGER {databaseSchema}_{objectQualifier}forum_before_insert_tr
  BEFORE INSERT
  ON {databaseSchema}.{objectQualifier}forum
  FOR EACH ROW
  EXECUTE PROCEDURE {databaseSchema}.{objectQualifier}forum_before_insert_func();
--GO 

/* ******************************************************************************************************************************
*********************************************************************************************************************************
HELER AND INITIALIZE FUNCTIONS
*********************************************************************************************************************************
******************************************************************************************************************************** */


CREATE OR REPLACE FUNCTION {databaseSchema}.{objectQualifier}fillin_or_check_ns_tables()
				  RETURNS void AS
$BODY$DECLARE _rec_b RECORD;
 _rec_c RECORD;
  _rec_f RECORD; 
  _brdTmp integer;
  _catTmp integer;
   _ndfpTmp integer :=0;
   _frmTmp integer :=0; 
BEGIN
-- DELETE FROM {databaseSchema}.{objectQualifier}forum_ns where i_categoryid is null or categoryid = i_categoryid;
FOR _rec_b IN 
		   SELECT boardid
		   from  {databaseSchema}.{objectQualifier}board 
		   ORDER by boardid
	   LOOP	
		 FOR _rec_c IN 
				SELECT c.categoryid,c.boardid, c.sortorder
				from  {databaseSchema}.{objectQualifier}category c  
				JOIN {databaseSchema}.{objectQualifier}board b 
				on b.boardid = c.boardid 
				WHERE c.boardid = _rec_b.boardid 
				ORDER by c.boardid,c.sortorder				
		 LOOP			 
				 -- loop through forums
					   FOR _rec_f IN 
						  SELECT f.forumid, f.parentid,coalesce(f.parentid, 0) parent0 ,f.categoryid, f.sortorder 
							from  {databaseSchema}.{objectQualifier}forum f 
							JOIN {databaseSchema}.{objectQualifier}category c on f.categoryid = c.categoryid
							 JOIN {databaseSchema}.{objectQualifier}board b on b.boardid = c.boardid
							 WHERE f.categoryid = _rec_c.categoryid
							  ORDER by c.boardid, f.categoryid, parent0,f.sortorder, f.forumid							
					  LOOP												
				    
					IF (_rec_f.parentid IS NOT NULL) THEN
					SELECT nid into  _ndfpTmp FROM {databaseSchema}.{objectQualifier}forum_ns WHERE forumid = _rec_f.parentid;
					END IF;
						   -- it's right in the category. tree will be individual for each category
						   INSERT INTO {databaseSchema}.{objectQualifier}forum_ns(parentid,boardid, categoryid,forumid, sortorder, tree, path_cache) 
						   values (_ndfpTmp,_rec_c.boardid,_rec_f.categoryid,COALESCE(_rec_f.forumid,0),_rec_f.sortorder, _rec_f.categoryid, null); 
                    _ndfpTmp :=NULL;
		-- end of forum loop 					
		END LOOP;	
	-- end of category loop
   END LOOP;
	-- end of board loop
END LOOP;
          -- sync tables in the loop
          FOR _rec_f IN 
					SELECT f.forumid, f.parentid,coalesce(f.parentid, 0) parent0 ,f.left_key, f.right_key, f."level" 
							from  {databaseSchema}.{objectQualifier}forum_ns f 							
							  ORDER by parent0, f.sortorder, f.forumid							
			LOOP				  
					UPDATE	{databaseSchema}.{objectQualifier}forum
					set left_key = _rec_f.left_key,
					    right_key = _rec_f.right_key,
						"level"  =  _rec_f."level"
						where forumid = _rec_f.forumid;			
		    -- end of sync loop 					
		   END LOOP;
		   -- clean up temporary table
		  TRUNCATE {databaseSchema}.{objectQualifier}forum_ns;
-- END IF;
END;
$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER CALLED ON NULL INPUT
  COST 100;   
-- GO



