

-- here we don't delete children but move them one level higher and remove keys gap.
CREATE PROCEDURE {databaseSchema}.{objectQualifier}forum_ns_after_delete_2_func
(inout old_tree int, inout old_left_key int, inout old_right_key int,inout old_level int, inout old_parentid int
)
BEGIN
DECLARE is_locked tinyint(1) default 1;
-- here we should lock table
	CALL {databaseSchema}.{objectQualifier}lock_ns_tree(old_tree, is_locked);
-- remove break in keys and shift child nodes
   UPDATE {databaseSchema}.{objectQualifier}forum_ns
		SET left_key = CASE WHEN left_key < old_left_key
							THEN left_key
							ELSE CASE WHEN right_key < old_right_key
									  THEN left_key - 1 
									  ELSE left_key - 2
								 END
					   END,
			`level` = CASE WHEN right_key < old_right_key
						   THEN "level" - 1 
						   ELSE "level"
					  END,
			parentid = CASE WHEN right_key < old_right_key AND "level" = old_level + 1
						   THEN old_parentid
						   ELSE parentid
						END,
			right_key = CASE WHEN right_key < old_right_key
							 THEN right_key - 1 
							 ELSE right_key - 2
						END,
			trigger_lock_update = 1
		WHERE (right_key > old_right_key OR
			(left_key > old_left_key AND right_key < old_right_key)) AND
			tree = old_tree;	
END;
  --GO


-- Here we delete subtrees no need here 


CREATE PROCEDURE {databaseSchema}.{objectQualifier}forum_ns_after_delete_func(inout old_tree int, inout old_left_key int, inout old_right_key int,inout old_level int, inout old_parentid int)
 
proc_label:BEGIN
DECLARE	ici_skew_tree INTEGER;

	CALL {databaseSchema}.{objectQualifier}lock_ns_tree(old_tree);
-- Проверяем, стоит ли выполнять триггер:
	IF old_trigger_for_delete = 1 THEN LEAVE proc_label; END IF;
-- Помечаем на удаление дочерние узлы:
	UPDATE {databaseSchema}.{objectQualifier}forum_ns
		SET trigger_for_delete = 1,
			trigger_lock_update = 1
		WHERE
			tree = old_tree AND
			left_key > old_left_key AND
			right_key < old_right_key;
-- Удаляем помеченные узлы:
	DELETE FROM {databaseSchema}.{objectQualifier}forum_ns
		WHERE
			tree = old_tree AND
			left_key > old_left_key AND
			right_key < old_right_key;
-- Убираем разрыв в ключах:
	set ici_skew_tree = old_right_key - old_left_key + 1;
	UPDATE {databaseSchema}.{objectQualifier}forum_ns
		SET left_key = CASE WHEN left_key > old_left_key
							THEN left_key - ici_skew_tree
							ELSE left_key
					   END,
			right_key = right_key - ici_skew_tree,
			trigger_lock_update = TRUE
		WHERE right_key > old_right_key AND
			tree = old_tree;
END;
--GO
  






CREATE PROCEDURE {databaseSchema}.{objectQualifier}forum_ns_before_update_func(inout new_nid int, 
inout new_tree int, inout new_left_key int, inout new_right_key int,inout new_level int, inout new_parentid int, inout new_trigger_lock_update tinyint(1), inout new_trigger_for_delete tinyint(1),
inout old_nid int, inout old_tree int, inout old_left_key int, inout old_right_key int,inout old_level int, inout old_parentid int

)
proc_label:BEGIN    
DECLARE	ici_left_key       INT;
DECLARE	ici_level          INT;
DECLARE	ici_skew_tree      INT;
DECLARE	ici_skew_level     INT;
DECLARE	ici_skew_edit      INT;
DECLARE	ici_tmp_left_key   INT;
DECLARE	ici_tmp_right_key  INT;
DECLARE	ici_tmp_level      INT;
DECLARE	ici_tmp_id         INT;
DECLARE	ici_tmp_parent_id  INT;
	CALL {databaseSchema}.{objectQualifier}lock_ns_tree(old_tree);
    -- maybe we should do nothing	
	IF new_trigger_lock_update = 1 THEN
		set new_trigger_lock_update = 0;
		IF new_trigger_for_delete = 1 THEN
			-- NEW = OLD;
			set new_tree =  old_tree;
			set new_left_key = old_left_key; 
			set new_right_key = old_right_key;
			set new_level = old_level; 
			set new_parentid = old_parentid;   

			set new_trigger_for_delete = 1;
			LEAVE proc_label;
		END IF;
		LEAVE proc_label;
	END IF;

-- reset field values that can't be changed by user
	set new_trigger_for_delete = 0;
	set new_tree = old_tree;
	set new_right_key = old_right_key;
	set new_level = old_level;

	IF new_parentid IS NULL THEN 
	set new_parentid = 0; 
	END IF;

-- Check if there are changes related to the tree structure. 
	IF new_parentid = old_parentid AND new_left_key = old_left_key
	THEN
		LEAVE proc_label;
	END IF;
	-- Alas we should rebuild it, let's begin 

	set ici_left_key = 0;
	set ici_level = 0;
	set ici_skew_tree = old_right_key - old_left_key + 1;
	-- where to move it? Let's guess.

-- If parentid was changed
	IF new_parentid != old_parentid THEN
-- if submission to other boss
		IF new_parentid > 0 THEN
			SELECT right_key, `level` + 1
				INTO ici_left_key, ici_level
				FROM {databaseSchema}.{objectQualifier}forum_ns
				WHERE nid = new_parentid AND tree = new_tree;
				-- otherwise move it to tree root 
		ELSE
			SELECT MAX(right_key) + 1 
				INTO ici_left_key
				FROM {databaseSchema}.{objectQualifier}forum_ns
				WHERE tree = new_tree;
			set ici_level = 0;
		END IF;
		-- check if the parent is in the scope of the node being moved
		IF ici_left_key IS NOT NULL AND 
		   ici_left_key > 0 AND
		   ici_left_key > old_left_key AND
		   ici_left_key <= old_right_key 
		THEN
		 set new_parentid = old_parentid;
		 set  new_left_key = old_left_key;
		   LEAVE proc_label;
		END IF;
	END IF;
	-- left key was supplied, but parentid not 
   IF ici_left_key IS NULL OR ici_left_key = 0 THEN
		SELECT nid, left_key, right_key, `level`, parentid 
			INTO ici_tmp_id, ici_tmp_left_key, ici_tmp_right_key, ici_tmp_level, ici_tmp_parent_id
			FROM {databaseSchema}.{objectQualifier}forum_ns
			WHERE tree = new_tree AND (right_key = new_left_key OR right_key = new_left_key - 1)
			LIMIT 1;
		IF (ici_tmp_left_key IS NOT NULL AND ici_tmp_left_key > 0 AND new_left_key - 1 = ici_tmp_right_key) THEN
			set new_parentid = ici_tmp_parent_id;
			set ici_left_key = new_left_key;
			set ici_level = ici_tmp_level;
		ELSEIF (ici_tmp_left_key IS NOT NULL AND ici_tmp_left_key > 0 AND new_left_key = ici_tmp_right_key) THEN
			set new_parentid = _tmp_id;
			set ici_left_key = new_left_key;
			set ici_level = _tmp_level + 1;
		ELSEIF new_left_key = 1 THEN
			set new_parentid = 0;
			set ici_left_key = new_left_key;
			set ici_level = 0;
		ELSE
		  set  new_parentid = old_parentid;
		   set new_left_key = old_left_key;
		   LEAVE proc_label;
		END IF;
	END IF;
	-- we know now where to move the tree
	set	ici_skew_level = ici_level - old_level;
	IF ici_left_key > old_left_key THEN
	-- move up the tree
	set	ici_skew_edit = ici_left_key - old_left_key - ici_skew_tree;
		UPDATE {databaseSchema}.{objectQualifier}forum_ns
			SET left_key =  CASE WHEN right_key <= old_right_key
								 THEN left_key + ici_skew_edit
								 ELSE CASE WHEN left_key > old_right_key
										   THEN left_key - ici_skew_tree
										   ELSE left_key
									  END
							END,
				`level` =   CASE WHEN right_key <= old_right_key 
								 THEN `level` + ici_skew_level
								 ELSE `level`
							END,
				right_key = CASE WHEN right_key <= old_right_key 
								 THEN right_key + ici_skew_edit
								 ELSE CASE WHEN right_key < ici_left_key
										   THEN right_key - ici_skew_tree
										   ELSE right_key
									  END
							END,
				ici_trigger_lock_update = 1
			WHERE tree = old_tree AND
				  right_key > old_left_key AND
				  left_key < ici_left_key AND
				  nid <> old_nid;
		set ici_left_key = ici_left_key - ici_skew_tree;
	ELSE
	-- moving it down the tree
		set ici_skew_edit = ici_left_key - old_left_key;
		UPDATE {databaseSchema}.{objectQualifier}forum_ns
			SET
				right_key = CASE WHEN left_key >= old_left_key
								 THEN right_key + ici_skew_edit
								 ELSE CASE WHEN right_key < old_left_key
										   THEN right_key + ici_skew_tree
										   ELSE right_key
									  END
							END,
				`level` =   CASE WHEN left_key >= old_left_key
								 THEN `level` + ici_skew_level
								 ELSE `level`
							END,
				left_key =  CASE WHEN left_key >= old_left_key
								 THEN left_key + ici_skew_edit
								 ELSE CASE WHEN left_key >= ici_left_key
										   THEN left_key + ici_skew_tree
										   ELSE left_key
									  END
							END,
				 ici_trigger_lock_update = 1
			WHERE tree = old_tree AND
				  right_key >= ici_left_key AND
				  left_key < old_right_key AND
				  nid <> old_nid;
	END IF;
-- the tree was rebuilt now we heav only our current node
	set new_left_key = ici_left_key;
	set new_level = ici_level;
	set new_right_key = ici_left_key + ici_skew_tree - 1;
	
END;
--GO 

DROP TRIGGER IF EXISTS {databaseSchema}_{objectQualifier}_forum_ns_after_delete_2_tr;
--GO
CREATE TRIGGER {databaseSchema}_{objectQualifier}_forum_ns_after_delete_2_tr
  AFTER DELETE
  ON {databaseSchema}.{objectQualifier}forum_ns
  FOR EACH ROW
  -- inout
  CALL {databaseSchema}.{objectQualifier}forum_ns_after_delete_2_func(OLD.tree, OLD.left_key, OLD.right_key, OLD.level, OLD.parentid);
--GO

/* DROP TRIGGER IF EXISTS {databaseSchema}_{objectQualifier}_forum_ns_after_delete_tr;

CREATE TRIGGER {databaseSchema}_{objectQualifier}_forum_ns_after_delete_tr
  AFTER DELETE
  ON {databaseSchema}.{objectQualifier}forum_ns
  FOR EACH ROW
  -- inout
  CALL {databaseSchema}.{objectQualifier}forum_ns_after_delete_func(OLD.tree, OLD.left_key, OLD.right_key, OLD.level, OLD.parentid);
*/


DROP TRIGGER IF EXISTS {databaseSchema}_{objectQualifier}_forum_ns_before_update_tr;
--
CREATE TRIGGER {databaseSchema}_{objectQualifier}_forum_ns_before_update_tr
  BEFORE UPDATE
  ON {databaseSchema}.{objectQualifier}forum_ns
  FOR EACH ROW
  -- inout
  CALL {databaseSchema}.{objectQualifier}forum_ns_before_update_func(NEW.nid,NEW.tree,NEW.left_key,NEW.right_key,NEW.level, NEW.parentid,
 OLD.nid,OLD.tree,OLD.left_key,OLD.right_key,OLD.level,OLD.parentid );
--GO 
