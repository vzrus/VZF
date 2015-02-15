
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