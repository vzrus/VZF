-- Forum Nested Set

EXECUTE BLOCK
AS
BEGIN
IF (EXISTS(SELECT FIRST 1 1 
FROM RDB$PROCEDURES a WHERE a.RDB$PROCEDURE_NAME='objQual_create_or_check_ns_tables')) THEN
EXECUTE STATEMENT 'DROP PROCEDURE objQual_create_or_check_ns_tables';
END
--GO

EXECUTE BLOCK
AS
BEGIN
IF (EXISTS(SELECT FIRST 1 1 
FROM RDB$PROCEDURES a WHERE a.RDB$PROCEDURE_NAME='objQual_forum_ns_dropbridge_triggers')) THEN
EXECUTE STATEMENT 'DROP PROCEDURE objQual_forum_ns_dropbridge_triggers';
END
--GO

EXECUTE BLOCK
AS
BEGIN
IF (EXISTS(SELECT FIRST 1 1 
FROM RDB$PROCEDURES a WHERE a.RDB$PROCEDURE_NAME='objQual_forum_ns_drop_triggers')) THEN
EXECUTE STATEMENT 'DROP PROCEDURE objQual_forum_ns_drop_triggers';
END
--GO

EXECUTE BLOCK
AS
BEGIN
IF (EXISTS(SELECT FIRST 1 1 
FROM RDB$PROCEDURES a WHERE a.RDB$PROCEDURE_NAME='objQual_LOCK_NS_TREE')) THEN
EXECUTE STATEMENT 'DROP PROCEDURE objQual_LOCK_NS_TREE';
END
--GO

EXECUTE BLOCK
AS
BEGIN
IF (EXISTS(SELECT FIRST 1 1 
FROM RDB$PROCEDURES a WHERE a.RDB$PROCEDURE_NAME='objQual_forum_ns_after_delete_2_func')) THEN
EXECUTE STATEMENT 'DROP PROCEDURE objQual_forum_ns_after_delete_2_func';
END
--GO

EXECUTE BLOCK
AS
BEGIN
IF (EXISTS(SELECT FIRST 1 1 
FROM RDB$PROCEDURES a WHERE a.RDB$PROCEDURE_NAME='objQual_forum_ns_before_insert_func')) THEN
EXECUTE STATEMENT 'DROP PROCEDURE objQual_forum_ns_before_insert_func';
END
--GO

EXECUTE BLOCK
AS
BEGIN
IF (EXISTS(SELECT FIRST 1 1 
FROM RDB$PROCEDURES a WHERE a.RDB$PROCEDURE_NAME='objQual_forum_ns_before_update_func')) THEN
EXECUTE STATEMENT 'DROP PROCEDURE objQual_forum_ns_before_update_func';
END
--GO

EXECUTE BLOCK
AS
BEGIN
IF (EXISTS(SELECT FIRST 1 1 
FROM RDB$PROCEDURES a WHERE a.RDB$PROCEDURE_NAME='objQual_forum_ns_create_triggers')) THEN
EXECUTE STATEMENT 'DROP PROCEDURE objQual_forum_ns_create_triggers';
END
--GO

EXECUTE BLOCK
AS
BEGIN
IF (EXISTS(SELECT FIRST 1 1 
FROM RDB$PROCEDURES a WHERE a.RDB$PROCEDURE_NAME='objQual_forum_ns_forumsavegetparent')) THEN
EXECUTE STATEMENT 'DROP PROCEDURE objQual_forum_ns_forumsavegetparent';
END
--GO

EXECUTE BLOCK
AS
BEGIN
IF (EXISTS(SELECT FIRST 1 1 
FROM RDB$PROCEDURES a WHERE a.RDB$PROCEDURE_NAME='objQual_fillin_or_check_ns_tables')) THEN
EXECUTE STATEMENT 'DROP PROCEDURE objQual_fillin_or_check_ns_tables';
END
--GO

EXECUTE BLOCK
AS
BEGIN
IF (EXISTS(SELECT FIRST 1 1 
FROM RDB$PROCEDURES a WHERE a.RDB$PROCEDURE_NAME='objQual_forum_ns_getchildren')) THEN
EXECUTE STATEMENT 'DROP PROCEDURE objQual_forum_ns_getchildren';
END
--GO

EXECUTE BLOCK
AS
BEGIN
IF (EXISTS(SELECT FIRST 1 1 
FROM RDB$PROCEDURES a WHERE a.RDB$PROCEDURE_NAME='objQual_forum_ns_getchildren_activeuser')) THEN
EXECUTE STATEMENT 'DROP PROCEDURE objQual_forum_ns_getchildren_activeuser';
END
--GO

EXECUTE BLOCK
AS
BEGIN
IF (EXISTS(SELECT FIRST 1 1 
FROM RDB$PROCEDURES a WHERE a.RDB$PROCEDURE_NAME='objQual_forum_ns_getchildren_anyuser')) THEN
EXECUTE STATEMENT 'DROP PROCEDURE objQual_forum_ns_getchildren_anyuser';
END
--GO

EXECUTE BLOCK
AS
BEGIN
IF (EXISTS(SELECT FIRST 1 1 
FROM RDB$PROCEDURES a WHERE a.RDB$PROCEDURE_NAME='objQual_forum_ns_getpath')) THEN
EXECUTE STATEMENT 'DROP PROCEDURE objQual_forum_ns_getpath';
END
--GO

EXECUTE BLOCK
AS
BEGIN
IF (EXISTS(SELECT FIRST 1 1 
FROM RDB$PROCEDURES a WHERE a.RDB$PROCEDURE_NAME='objQual_forum_ns_listpath')) THEN
EXECUTE STATEMENT 'DROP PROCEDURE objQual_forum_ns_listpath';
END
--GO

EXECUTE BLOCK
AS
BEGIN
IF (EXISTS(SELECT FIRST 1 1 
FROM RDB$PROCEDURES a WHERE a.RDB$PROCEDURE_NAME='objQual_forum_ns_recreate')) THEN
EXECUTE STATEMENT 'DROP PROCEDURE objQual_forum_ns_recreate';
END
--GO

CREATE PROCEDURE objQual_create_or_check_ns_tables()
				  AS
BEGIN
IF (EXISTS(SELECT 1 
			   FROM RDB$RELATIONS a 
			   WHERE a.RDB$RELATION_NAME='objQual_FORUM_NS' 
			   ROWS 1)) THEN
	EXECUTE STATEMENT '
DELETE FROM objQual_FORUM_NS;';
END;
--GO 

EXECUTE BLOCK
AS
BEGIN
IF (NOT EXISTS(SELECT 1 
			   FROM RDB$RELATIONS a 
			   WHERE a.RDB$RELATION_NAME='objQual_FORUM_NS' 
			   ROWS 1)) THEN
	EXECUTE STATEMENT 'CREATE TABLE objQual_FORUM_NS
(
  NID serial NOT NULL,
  BOARDID integer NOT NULL,
  CATEGORYID integer NOT NULL,
  FORUMID integer NOT NULL,
  LEFT_KEY integer NOT NULL,
  RIGHT_KEY integer NOT NULL,
  "LEVEL" integer  DEFAULT 0 NOT NULL,
  TREE integer DEFAULT 0 NOT NULL ,
  PARENTID integer  DEFAULT 0 NOT NULL,  
  _TRIGGER_LOCK_UPDATE BOOL DEFAULT 0 NOT NULL ,
  _TRIGGER_FOR_DELETE BOOL DEFAULT 0 NOT NULL , 
  SORTORDER integer NOT NULL DEFAULT 0,
  PATH_CACHE varchar(1024)); ';
  END;
--GO


-- Source primary key: PRIMARY
EXECUTE BLOCK
AS
BEGIN 
IF (NOT EXISTS( SELECT 1
FROM RDB$INDICES a WHERE a.RDB$INDEX_NAME ='PK_objQual_FORUM_NS' ROWS 1)) THEN
EXECUTE STATEMENT 'ALTER TABLE objQual_FORUM_NS ADD CONSTRAINT PK_objQual_FORUM_NS PRIMARY KEY (NID)';
END
--GO

EXECUTE BLOCK
AS
BEGIN 
IF (NOT EXISTS (
	SELECT 1 FROM RDB$INDICES I
	WHERE (I.RDB$INDEX_NAME = 'IX_objQual_FORUM_NS_LK_RK_L_T') AND (I.RDB$RELATION_NAME = 'objQual_FORUM')
   )) THEN
EXECUTE STATEMENT 'CREATE INDEX IX_objQual_FORUM_NS_LK_RK_L_T ON objQual_FORUM_NS(LEFT_KEY, RIGHT_KEY, LEVEL, TREE);';


 IF (NOT EXISTS (
	SELECT 1 FROM RDB$INDICES I
	WHERE (I.RDB$INDEX_NAME = 'IX_objQual_FORUM_NS_PARENTID') AND (I.RDB$RELATION_NAME = 'objQual_FORUM')
   )) THEN
  EXECUTE STATEMENT 'CREATE INDEX IX_objQual_FORUM_NS_PARENTID ON objQual_FORUM_NS(PARENTID);';
END;
--GO 


EXECUTE PROCEDURE objQual_create_or_check_ns_tables;
--GO

/* ******************************************************************************************************************************
*********************************************************************************************************************************
BRIDGE FUNCTIONS
*********************************************************************************************************************************
******************************************************************************************************************************** */



CREATE PROCEDURE objQual_forum_ns_dropbridge_triggers()
				  AS
BEGIN
-- ALTER TRIGGER objQual_tr_forum_ns_forum_update INACTIVE;
if (exists select first 1 1 from rdb$triggers 
	where (rdb$system_flag = 0 or rdb$system_flag is null and rdb$trigger_name='objQual_tr_forum_ns_forum_update')) THEN
  EXECUTE STATEMENT 'DROP TRIGGER objQual_tr_forum_ns_forum_update ON objQual_forum;'; 
if (exists select first 1 1 from rdb$triggers 
	where (rdb$system_flag = 0 or rdb$system_flag is null and rdb$trigger_name='objQual_tr_forum_ns_forum_insert')) THEN
  EXECUTE STATEMENT 'DROP TRIGGER objQual_tr_forum_ns_forum_insert ON objQual_forum;'; 
if (exists select first 1 1 from rdb$triggers 
	where (rdb$system_flag = 0 or rdb$system_flag is null and rdb$trigger_name='objQual_tr_forum_ns_forum_delete')) THEN
  EXECUTE STATEMENT 'DROP TRIGGER objQual_tr_forum_ns_forum_delete ON objQual_forum;'; 
  
  if (exists select first 1 1 from rdb$triggers 
	where (rdb$system_flag = 0 or rdb$system_flag is null and rdb$trigger_name='objQual_tr_forum_ns_category_insert')) THEN
  EXECUTE STATEMENT 'DROP TRIGGER objQual_tr_forum_ns_category_insert ON objQual_category;';   
  if (exists select first 1 1 from rdb$triggers 
	where (rdb$system_flag = 0 or rdb$system_flag is null and rdb$trigger_name='objQual_tr_forum_ns_category_update')) THEN
  EXECUTE STATEMENT 'DROP TRIGGER objQual_tr_forum_ns_category_update ON objQual_category;'; 
	if (exists select first 1 1 from rdb$triggers 
	where (rdb$system_flag = 0 or rdb$system_flag is null and rdb$trigger_name='objQual_tr_forum_ns_category_delete')) THEN
  EXECUTE STATEMENT 'DROP TRIGGER objQual_tr_forum_ns_category_delete ON objQual_category;'; 

	if (exists select first 1 1 from rdb$triggers 
	where (rdb$system_flag = 0 or rdb$system_flag is null and rdb$trigger_name='objQual_tr_forum_ns_board_insert')) THEN
  EXECUTE STATEMENT 'DROP TRIGGER objQual_tr_forum_ns_board_insert ON objQual_board;'; 
   if (exists select first 1 1 from rdb$triggers 
	where (rdb$system_flag = 0 or rdb$system_flag is null and rdb$trigger_name='objQual_tr_forum_ns_board_delete')) THEN
  EXECUTE STATEMENT 'DROP TRIGGER objQual_tr_forum_ns_board_delete ON objQual_board;'; 

END;
--GO 

CREATE PROCEDURE objQual_forum_ns_drop_triggers()
				  AS
BEGIN
 if (exists select first 1 1 from rdb$triggers 
	where (rdb$system_flag = 0 or rdb$system_flag is null and rdb$trigger_name='objQual_forum_ns_after_delete_2_tr')) THEN
  EXECUTE STATEMENT 'DROP TRIGGER objQual_forum_ns_after_delete_2_tr ON objQual_forum_ns;'; 
if (exists select first 1 1 from rdb$triggers 
	where (rdb$system_flag = 0 or rdb$system_flag is null and rdb$trigger_name='objQual_forum_ns_before_insert_tr')) THEN
  EXECUTE STATEMENT 'DROP TRIGGER objQual_forum_ns_before_insert_tr ON objQual_forum_ns;'; 
  if (exists select first 1 1 from rdb$triggers 
	where (rdb$system_flag = 0 or rdb$system_flag is null and rdb$trigger_name='objQual_forum_ns_after_delete_tr')) THEN
  EXECUTE STATEMENT 'DROP TRIGGER objQual_forum_ns_after_delete_tr ON objQual_forum_ns;';
	if (exists select first 1 1 from rdb$triggers 
	where (rdb$system_flag = 0 or rdb$system_flag is null and rdb$trigger_name='objQual_forum_ns_before_update_tr')) THEN
  EXECUTE STATEMENT 'DROP TRIGGER objQual_forum_ns_before_update_tr ON objQual_forum_ns;';
END;
--GO 

/* ******************************************************************************************************************************
*********************************************************************************************************************************
CORE NS TRIGGER FUNCTIONS
*********************************************************************************************************************************
******************************************************************************************************************************** */


CREATE PROCEDURE objQual_LOCK_NS_TREE(tree_id integer)
  RETURNS (ret BOOL) AS
DECLARE ici_id INTEGER;
DECLARE ret INTEGER DEFAULT 0;
BEGIN

	SELECT forumid        
		FROM objQual_forum_ns
		WHERE tree = :tree_id FOR UPDATE WITH LOCK
		INTO :ici_id;
	ret = 1;
	RETURN ret;
END;
--GO

-- here we don't delete children but move them one level higher and remove keys gap.
create trigger objQual_forum_ns_after_delete_2_func for objQual_forum_ns
  AFTER DELETE
as
begin
PERFORM objQual_lock_ns_tree(OLD.tree);
-- Убираем разрыв в ключах и сдвигаем дочерние узлы:
   UPDATE objQual_forum_ns
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
			tree = OLD.tree;
	RETURN OLD;
end
--GO


/* Here we delete subtrees no need here 


CREATE OR REPLACE FUNCTION objQual_forum_ns_after_delete_func()
  RETURNS trigger AS
$BODY$
DECLARE
	_skew_tree INTEGER;
BEGIN
	PERFORM objQual_lock_ns_tree(OLD.tree);
-- Проверяем, стоит ли выполнять триггер:
	IF OLD._trigger_for_delete = TRUE THEN RETURN OLD; END IF;
-- Помечаем на удаление дочерние узлы:
	UPDATE objQual_forum_ns
		SET _trigger_for_delete = TRUE,
			_trigger_lock_update = TRUE
		WHERE
			tree = OLD.tree AND
			left_key > OLD.left_key AND
			right_key < OLD.right_key;
-- Удаляем помеченные узлы:
	DELETE FROM objQual_forum_ns
		WHERE
			tree = OLD.tree AND
			left_key > OLD.left_key AND
			right_key < OLD.right_key;
-- Убираем разрыв в ключах:
	_skew_tree := OLD.right_key - OLD.left_key + 1;
	UPDATE objQual_forum_ns
		SET left_key = CASE WHEN left_key > OLD.left_key
							THEN left_key - _skew_tree
							ELSE left_key
					   END,
			right_key = right_key - _skew_tree,
			_trigger_lock_update = TRUE
		WHERE right_key > OLD.right_key AND
			tree = OLD.tree;
	RETURN OLD;
END;
$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100;
  

CREATE TRIGGER objQual_forum_ns_after_delete_tr
  AFTER DELETE
  ON objQual_forum_ns
  FOR EACH ROW
  EXECUTE PROCEDURE objQual_forum_ns_after_delete_func(); */



CREATE OR REPLACE FUNCTION objQual_forum_ns_before_insert_func()
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
	PERFORM objQual_lock_ns_tree(NEW.tree);
-- Нельзя эти поля ручками ставить:
	NEW._trigger_for_delete := FALSE;
	NEW._trigger_lock_update := FALSE;
	_left_key := 0;
	_level := 0;
-- Если мы указали родителя:
	IF NEW.parentid IS NOT NULL AND NEW.parentid > 0 THEN
		SELECT right_key, "level" + 1
			INTO _left_key, _level
			FROM objQual_forum_ns
			WHERE nid = NEW.parentid AND
				  tree = NEW.tree;
	END IF;
-- Если мы указали левый ключ:
	IF NEW.left_key IS NOT NULL AND
	   NEW.left_key > 0 AND 
	   (_left_key IS NULL OR _left_key = 0) THEN
		SELECT nid, left_key, right_key, "level", parentid 
			INTO _tmp_id, _tmp_left_key, _tmp_right_key, _tmp_level, _tmp_parent_id
			FROM objQual_forum_ns
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
			FROM objQual_forum_ns
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
	UPDATE objQual_forum_ns
		SET left_key = left_key + 
			CASE WHEN left_key >= _left_key 
			  THEN 2 
			  ELSE 0 
			END,
			right_key = right_key + 2,
			_trigger_lock_update = TRUE
		WHERE tree = NEW.tree AND right_key >= _left_key;
	RETURN NEW;
END;
$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100;
  --GO

CREATE OR REPLACE FUNCTION objQual_forum_ns_before_update_func()
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
	PERFORM objQual_lock_ns_tree(OLD.tree);
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
	NEW.tree := OLD.tree;
	NEW.right_key := OLD.right_key;
	NEW."level" := OLD."level";
	IF NEW.parentid IS NULL THEN NEW.parentid := 0; END IF;
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
		IF NEW.parentid > 0 THEN
			SELECT right_key, level + 1
				INTO _left_key, _level
				FROM objQual_forum_ns
				WHERE nid = NEW.parentid AND tree = NEW.tree;
-- Иначе в корень дерева переносим:
		ELSE
			SELECT MAX(right_key) + 1 
				INTO _left_key
				FROM objQual_forum_ns
				WHERE tree = NEW.tree;
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
		SELECT nid, left_key, right_key, "level", parentid 
			INTO _tmp_id, _tmp_left_key, _tmp_right_key, _tmp_level, _tmp_parent_id
			FROM objQual_forum_ns
			WHERE tree = NEW.tree AND (right_key = NEW.left_key OR right_key = NEW.left_key - 1)
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
			NEW.parentid := 0;
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
		UPDATE objQual_forum_ns
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
			WHERE tree = OLD.tree AND
				  right_key > OLD.left_key AND
				  left_key < _left_key AND
				  nid <> OLD.nid;
		_left_key := _left_key - _skew_tree;
	ELSE
-- Перемещение вниз по дереву:
		_skew_edit := _left_key - OLD.left_key;
		UPDATE objQual_forum_ns
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
			WHERE tree = OLD.tree AND
				  right_key >= _left_key AND
				  left_key < OLD.right_key AND
				  nid <> OLD.nid;
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
CREATE OR REPLACE FUNCTION objQual_forum_ns_create_triggers()
				  RETURNS void AS
$BODY$
BEGIN
CREATE TRIGGER objQual_forum_ns_after_delete_2_tr
  AFTER DELETE
  ON objQual_forum_ns
  FOR EACH ROW
  EXECUTE PROCEDURE objQual_forum_ns_after_delete_2_func();

CREATE TRIGGER objQual_forum_ns_before_insert_tr
  BEFORE INSERT
  ON objQual_forum_ns
  FOR EACH ROW
  EXECUTE PROCEDURE objQual_forum_ns_before_insert_func();

CREATE TRIGGER objQual_forum_ns_before_update_tr
  BEFORE UPDATE
  ON objQual_forum_ns
  FOR EACH ROW
  EXECUTE PROCEDURE objQual_forum_ns_before_update_func();
END;
$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER STRICT
  COST 100;  
	--GO 



/* ******************************************************************************************************************************
*********************************************************************************************************************************
HELER AND INITIALIZE FUNCTIONS
*********************************************************************************************************************************
******************************************************************************************************************************** */
CREATE OR REPLACE FUNCTION objQual_forum_ns_forumsavegetparent(i_boardid integer, i_forumid integer, i_parentid integer, i_categoryid integer)
  RETURNS integer AS
$BODY$DECLARE
_rec RECORD;
_recTmp RECORD;
_nid integer;
_nidParent integer;
_notincluded boolean := false;
_thisforum_sortorder integer := 0;
_thisforum_nid integer := 0;
_arrforums integer array;
_arrsortorders integer array; 

_thisCurrentSortOrder integer;
_testOut varchar;
_leftNodePrevious integer;
BEGIN

	 -- we have a forum in category and getting a category nid as a parent nid.
						IF 	(i_parentid IS NULL OR i_parentid <= 0) THEN		
						SELECT nid INTO _nidParent FROM objQual_forum_ns WHERE categoryid = i_categoryid and forumid = 0;
						ELSE				
						 SELECT parentid INTO _nidParent FROM objQual_forum_ns WHERE forumid = i_forumid;
						END IF;	

 -- move node into it's subtree
--  UPDATE objQual_forum_ns SET parentid = _nidParent WHERE forumid = i_forumid;
 -- range a node among it's siblings by sort order 


					   

-- _nidParent this is nid of a parent node if i_parentid is  null it's a category
CREATE  TEMPORARY TABLE objQual_tmp_ns_sort
	(nid integer, left_key integer, right_key integer, level integer, parentid integer, forumid integer, sortorder integer) 
	WITHOUT OIDS 
	ON COMMIT  DROP;
-- current forum sort order to compare in loop
SELECT sortorder
INTO _thisforum_sortorder
FROM objQual_forum f
WHERE f.forumid = i_forumid;

-- add it to temporary table to sort
FOR _rec IN SELECT n1.nid, n1.left_key, n1.right_key, n1.level, n1.forumid, n1.parentid, n1.level, n1.sortorder
FROM objQual_forum_ns  n1,
objQual_forum_ns  n2   WHERE  ( n2.nid = _nidParent
AND  n1.left_key BETWEEN n2.left_key + _notincluded::integer AND n2.right_key
and (TRUE IS FALSE  OR n1.parentid = n2.nid)) ORDER BY n1.left_key
LOOP

INSERT INTO objQual_tmp_ns_sort(nid,left_key,right_key, level,parentid, forumid,sortorder)
VALUES  (_rec.nid, _rec.left_key, _rec.right_key, _rec.level, _rec.parentid, _rec.forumid,_rec.sortorder);

END LOOP;
-- loop through sorted  nodes and return previous value as a parent node
FOR _recTmp IN SELECT nid,forumid,sortorder FROM objQual_tmp_ns_sort  ORDER by sortorder, forumid
LOOP
_testOut := COALESCE(_testOut,'') || ',' ||  _recTmp.nid::varchar;
if (_recTmp.forumid = i_forumid AND _thisforum_sortorder >= _recTmp.sortorder) then
EXIT;
end if; 
_nid := _recTmp.nid;
END LOOP;  
/* if _nid is NULL then
 SELECT nid INTO _nid FROM objQual_forum_ns WHERE categoryid = i_categoryid and forumid = i_forumid;
end if; */
-- at last we've found previous node
SELECT left_key into _leftNodePrevious
FROM objQual_forum_ns    WHERE nid = _nid;

UPDATE objQual_forum_ns SET left_key  = _leftNodePrevious WHERE forumid = i_forumid;

return _leftNodePrevious;
-- return _nidParent AS II;
-- return (SELECT COUNT(1) FROM objQual_tmp_ns_sort);

END;
$BODY$
  LANGUAGE 'plpgsql' VOLATILE STRICT SECURITY DEFINER
  COST 100;
--GO

CREATE OR REPLACE FUNCTION objQual_fillin_or_check_ns_tables()
				  RETURNS void AS
$BODY$DECLARE _rec_b RECORD;
 _rec_c RECORD;
  _rec_f RECORD; 
   _ndfpTmp integer :=0;
   _frmTmp integer :=0; 
BEGIN
-- DELETE FROM objQual_forum_ns;
-- if ((select count(nid) from objQual_forum_ns) = 0) THEN
-- fill in boards as root (level = 0) nodes
FOR _rec_b IN 
		   SELECT boardid
		   from  objQual_board 
		   ORDER by boardid
	   LOOP
	   INSERT INTO objQual_forum_ns(boardid,categoryid,forumid) values (_rec_b.boardid,0,0);
	   -- fill in categories as level = 1 nodes
		 FOR _rec_c IN 
				SELECT c.categoryid,c.boardid, c.sortorder
				from  objQual_category c  
				JOIN objQual_board b 
				on b.boardid = c.boardid 
				WHERE c.boardid = _rec_b.boardid 
				ORDER by c.boardid,c.sortorder				
		 LOOP
			   INSERT INTO objQual_forum_ns(boardid, categoryid,forumid, sortorder) values (_rec_b.boardid,_rec_c.categoryid,0,_rec_c.sortorder );

					UPDATE objQual_forum_ns SET parentid  = _rec_b.boardid where categoryid = _rec_c.categoryid;
			 
				 -- loop through forums
					   FOR _rec_f IN 
						  SELECT f.forumid, f.parentid,coalesce(f.parentid, 0) parent0 ,f.categoryid, f.sortorder 
							from  objQual_forum f 
							JOIN objQual_category c on f.categoryid = c.categoryid
							 JOIN objQual_board b on b.boardid = c.boardid
							 WHERE f.categoryid = _rec_c.categoryid
							  ORDER by c.boardid, f.categoryid, parent0,f.sortorder, f.forumid							
					  LOOP				  
											
					IF (_rec_f.parentid IS NULL) THEN
					SELECT nid into _ndfpTmp FROM objQual_forum_ns WHERE categoryid =_rec_f.categoryid and forumid = 0;
						ELSE
					SELECT nid into  _ndfpTmp FROM objQual_forum_ns WHERE forumid = _rec_f.parentid;
					END IF;
					SELECT _rec_f.forumid  into  _frmTmp;
						   -- it's right in the category
						   INSERT INTO objQual_forum_ns(parentid,boardid, categoryid,forumid, sortorder, tree, path_cache) 
						   values (_ndfpTmp,_rec_c.boardid,_rec_f.categoryid,COALESCE(_frmTmp,0),_rec_f.sortorder, 0, ''); 
		-- end of forum loop 					
		END LOOP;	
	-- end of category loop
   END LOOP;
	-- end of board loop
END LOOP;
-- END IF;
END;
$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER STRICT
  COST 100;   
-- GO


/* ******************************************************************************************************************************
*********************************************************************************************************************************
SELECT FUNCTIONS
*********************************************************************************************************************************
******************************************************************************************************************************** */


CREATE OR REPLACE FUNCTION objQual_forum_ns_getchildren(i_boardid integer,  i_categoryid integer, i_forumid integer,  i_notincluded boolean, i_immediateonly boolean)
				   RETURNS SETOF objQual_forum_ns_getsubtree_rt AS
$BODY$DECLARE
_rec objQual_forum_ns_getsubtree_rt%ROWTYPE;
_nid integer;
BEGIN
if (i_forumid > 0) then
SELECT ns.nid
INTO _nid
FROM objQual_forum_ns ns
WHERE ns.forumid = i_forumid;
elseif  (i_categoryid > 0) then
SELECT ns.nid
INTO _nid
FROM objQual_forum_ns ns
WHERE ns.forumid = 0 and ns.categoryid = i_categoryid;
else
SELECT ns.nid
INTO _nid
FROM objQual_forum_ns ns
WHERE ns.forumid = 0 and ns.categoryid = 0 and boardid = i_boardid;
end if;

FOR _rec IN SELECT n1.forumid, n1.parentid, n1.level
FROM objQual_forum_ns  n1,
objQual_forum_ns  n2   WHERE  ( n2.nid = _nid
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
COMMENT ON FUNCTION objQual_forum_ns_getchildren (integer,integer,integer, boolean, boolean) IS 'If i_forumid is null returns all nodes on the first level and their children.';
--GO

CREATE OR REPLACE FUNCTION objQual_forum_ns_getchildren_activeuser(i_boardid integer,  i_categoryid integer, i_forumid integer , i_userid integer, i_notincluded boolean, i_immediateonly boolean)
				   RETURNS SETOF objQual_forum_ns_getchildren_rt AS
$BODY$DECLARE
_rec objQual_forum_ns_getchildren_rt%ROWTYPE;
_nid integer; 
BEGIN
SELECT ns.nid
INTO _nid
FROM objQual_forum_ns ns
WHERE ns.forumid = 0 and ns.categoryid = 0 and boardid = i_boardid;

if (i_forumid > 0) then
SELECT ns.nid
INTO _nid
FROM objQual_forum_ns ns
WHERE ns.forumid = i_forumid;
elseif  (i_categoryid > 0) then
SELECT ns.nid
INTO _nid
FROM objQual_forum_ns ns
WHERE ns.forumid = 0 and ns.categoryid = i_categoryid;
else
SELECT ns.nid
INTO _nid
FROM objQual_forum_ns ns
WHERE ns.forumid = 0 and ns.categoryid = 0 and boardid = i_boardid;
end if;

FOR _rec IN SELECT c.categoryid, c.name , f.name, n1.forumid, n1.parentid, n1.level
FROM objQual_forum f 
JOIN objQual_category c on c.categoryid = f.categoryid 
JOIN objQual_activeaccess access ON (f.forumid = access.forumid and access.userid = i_userid)  
JOIN objQual_forum_ns  n1 ON (n1.forumid = f.forumid)
CROSS JOIN
objQual_forum_ns  n2   WHERE ( n2.nid = _nid
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
COMMENT ON FUNCTION objQual_forum_ns_getchildren_activeuser(integer,integer,integer, integer, boolean, boolean) IS 'If i_forumid is null returns all nodes on the first level and their children.';
--GO

CREATE OR REPLACE FUNCTION objQual_forum_ns_getchildren_anyuser(i_boardid integer,  i_categoryid integer, i_forumid integer , i_userid integer, i_notincluded boolean, i_immediateonly boolean)
				   RETURNS SETOF objQual_forum_ns_getchildren_rt AS
$BODY$DECLARE
_rec objQual_forum_ns_getchildren_rt%ROWTYPE;
_nid integer;
BEGIN
if (i_forumid > 0) then
SELECT ns.nid
INTO _nid
FROM objQual_forum_ns ns
WHERE ns.forumid = i_forumid;
elseif  (i_categoryid > 0) then
SELECT ns.nid
INTO _nid
FROM objQual_forum_ns ns
WHERE ns.forumid = 0 and ns.categoryid = i_categoryid;
else
SELECT ns.nid
INTO _nid
FROM objQual_forum_ns ns
WHERE ns.forumid = 0 and ns.categoryid = 0 and boardid = i_boardid;
end if;

FOR _rec IN SELECT c.categoryid, c.name , f.name, n1.forumid, n1.parentid, n1.level
FROM objQual_forum f 
JOIN objQual_category c on c.categoryid = f.categoryid 
JOIN objQual_vaccess_combo access ON (f.forumid = access."ForumID" and access."UserID" = i_userid)  
JOIN objQual_forum_ns  n1 ON n1.forumid = f.forumid
CROSS JOIN
objQual_forum_ns  n2   WHERE  ( n2.nid = _nid
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
COMMENT ON FUNCTION objQual_forum_ns_getchildren_anyuser(integer,integer,integer,integer, boolean, boolean) IS 'If i_forumid is null returns all nodes on the first level and their children.';
--GO
   
CREATE OR REPLACE FUNCTION objQual_forum_ns_getpath(i_forumid integer, i_parentincluded boolean)
				  RETURNS SETOF objQual_forum_ns_getsubtree_rt AS 
$BODY$DECLARE
_rec objQual_forum_ns_getsubtree_rt%ROWTYPE;
_nid integer;
BEGIN
SELECT ns.nid
INTO _nid
FROM objQual_forum_ns ns
WHERE ns.forumid = i_forumid;

FOR _rec IN  SELECT n1.forumid, n1.parentid, n1.level
FROM objQual_forum f
join objQual_forum_ns n1 
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


CREATE OR REPLACE FUNCTION objQual_forum_ns_listpath(
						   i_forumid integer)
				  RETURNS SETOF objQual_forum_listpath_return_type AS
$BODY$DECLARE
_rec objQual_forum_listpath_return_type%ROWTYPE;
_left_key integer;
_right_key integer;
BEGIN
SELECT left_key,right_key INTO _left_key,_right_key 
FROM objQual_forum_ns where forumid = i_forumid;

FOR _rec IN
SELECT f.forumid,
	   f.name,
	   -- we don't return board and category nodes here
	   (ns.level - 2)  
	   FROM objQual_forum_ns ns 
	   JOIN objQual_forum f on f.forumid = ns.forumid
	   WHERE ns.left_key <= _left_key AND ns.right_key >= _right_key ORDER BY ns.left_key
LOOP
RETURN NEXT _rec;
END LOOP;
						 
END; $BODY$
  LANGUAGE 'plpgsql' STABLE SECURITY DEFINER
  COST 100 ROWS 1000;
  --GO



-- Initialize all this

CREATE OR REPLACE FUNCTION objQual_forum_ns_recreate()
				  RETURNS void AS
$BODY$
BEGIN
PERFORM objQual_forum_ns_drop_triggers();
PERFORM objQual_forum_ns_dropbridge_triggers();
PERFORM objQual_create_or_check_ns_tables();
PERFORM objQual_forum_ns_create_triggers();
PERFORM objQual_fillin_or_check_ns_tables();
END;
$BODY$
  LANGUAGE 'plpgsql' VOLATILE SECURITY DEFINER STRICT
  COST 100;  
	--GO 
SELECT objQual_forum_ns_recreate();
-- GO

