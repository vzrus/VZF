-- Forum Nested Set



CREATE PROCEDURE {objectQualifier}CR_OR_CHCK_NS_TABLES
				  AS
BEGIN
IF (EXISTS(SELECT 1 
			   FROM RDB$RELATIONS a 
			   WHERE a.RDB$RELATION_NAME='{objectQualifier}FORUM_NS' 
			   ROWS 1)) THEN
	EXECUTE STATEMENT '
DELETE FROM {objectQualifier}FORUM_NS where NID >=0;';
END;
--GO 




-- Source primary key: PRIMARY
EXECUTE BLOCK
AS
BEGIN 
IF (NOT EXISTS( SELECT 1
FROM RDB$INDICES a WHERE a.RDB$INDEX_NAME ='PK_{objectQualifier}FORUM_NS' ROWS 1)) THEN
EXECUTE STATEMENT 'ALTER TABLE {objectQualifier}FORUM_NS ADD CONSTRAINT PK_{objectQualifier}FORUM_NS PRIMARY KEY (NID)';
END
--GO

EXECUTE BLOCK
AS
BEGIN 
IF (NOT EXISTS (
	SELECT 1 FROM RDB$INDICES I
	WHERE (I.RDB$INDEX_NAME = 'IX_{objectQualifier}FRM_NS_LK_RK_L_T') AND (I.RDB$RELATION_NAME = '{objectQualifier}FORUM')
   )) THEN
EXECUTE STATEMENT 'CREATE INDEX IX_{objectQualifier}FRM_NS_LK_RK_L_T ON {objectQualifier}FORUM(CATEGORYID, LEFT_KEY, RIGHT_KEY, LEVEL);';


 IF (NOT EXISTS (
	SELECT 1 FROM RDB$INDICES I
	WHERE (I.RDB$INDEX_NAME = 'IX_{objectQualifier}FRM_NS_PARENTID') AND (I.RDB$RELATION_NAME = '{objectQualifier}FORUM')
   )) THEN
  EXECUTE STATEMENT 'CREATE INDEX IX_{objectQualifier}FRM_NS_PARENTID ON {objectQualifier}FORUM(PARENTID);';
END;
--GO 


EXECUTE PROCEDURE {objectQualifier}CR_OR_CHCK_NS_TABLES;
--GO 

/* ******************************************************************************************************************************
*********************************************************************************************************************************
CORE NS TRIGGER FUNCTIONS
*********************************************************************************************************************************
******************************************************************************************************************************** */


CREATE PROCEDURE {objectQualifier}LOCK_NS_TREE(tree_id integer)
  RETURNS (ret BOOL) AS
DECLARE ici_id INTEGER;
BEGIN
    ret = 0;
	SELECT forumid        
		FROM {objectQualifier}forum_ns
		WHERE tree = :tree_id FOR UPDATE WITH LOCK
		INTO :ici_id;
	ret = 1;
	SUSPEND;
END;
--GO

CREATE PROCEDURE {objectQualifier}forum_ns_before_insert_func(new_boardid integer, new_categoryid integer, new_forumid integer,
new_tree integer, new_left_key integer, new_right_key integer, new_level integer,  new_parentid integer, new_sortorder integer,
new_trigger_for_delete BOOL, new_trigger_lock_update BOOL
)
RETURNS 
(
	ici_newid integer
)
AS
DECLARE	ici_left_key       INTEGER;
DECLARE	ici_level          INTEGER;
DECLARE ici_tmp_left_key   INTEGER;
DECLARE	ici_tmp_right_key  INTEGER;
DECLARE	ici_tmp_level      INTEGER;
DECLARE	ici_tmp_id         INTEGER;
DECLARE	ici_tmp_parent_id  INTEGER;
BEGIN

    -- CALL {objectQualifier}lock_ns_tree(new_tree);
-- These fields should not be set by hand
	new_trigger_for_delete = 0;
	new_trigger_lock_update = 0;
	ici_left_key = 0;
	ici_level = 0;

-- a parent was supplied
	IF (:new_parentid IS NOT NULL AND :new_parentid > 0) THEN
		 SELECT right_key,  "LEVEL" + 1			
			FROM {objectQualifier}forum_ns
			WHERE nid = :new_parentid AND
				  tree = :new_tree
				  INTO :ici_left_key, :ici_level;

-- a left key was supplied
	IF ((:new_left_key IS NOT NULL AND
	   :new_left_key > 0) AND 
	   (:ici_left_key IS NULL OR :ici_left_key = 0)) THEN
	   BEGIN
		SELECT nid, left_key, right_key,  "LEVEL", parentid 			
			FROM {objectQualifier}forum_ns
			WHERE tree = :new_tree AND (left_key = :new_left_key OR right_key = :new_left_key)
			INTO :ici_tmp_id, :ici_tmp_left_key, :ici_tmp_right_key, :ici_tmp_level, :ici_tmp_parent_id;
		IF (:ici_tmp_left_key IS NOT NULL AND :ici_tmp_left_key > 0 AND :new_left_key = :ici_tmp_left_key) THEN
		BEGIN
			new_parentid = :ici_tmp_parent_id;
			ici_left_key = :new_left_key;
			ici_level = :ici_tmp_level;
        END
		ELSE IF (:ici_tmp_left_key IS NOT NULL AND :ici_tmp_left_key > 0 AND :new_left_key = :ici_tmp_right_key) THEN
		BEGIN
			new_parentid = :ici_tmp_id;
			ici_left_key = :new_left_key;
			ici_level = :ici_tmp_level + 1;
		END
	END
-- if a parent or a left key was not supplied, we found nothing
IF (:ici_left_key IS NULL OR :ici_left_key = 0) THEN
	BEGIN
		SELECT MAX(right_key) + 1			
			FROM {objectQualifier}forum_ns
			WHERE tree = :new_tree
			INTO :ici_left_key;
		IF (:ici_left_key IS NULL OR :ici_left_key = 0) THEN
			ici_left_key = 1;
	
		ici_level = 0;
		new_parentid = 0; 
	END
-- setting figured out keys for the node
	new_left_key = :ici_left_key;
	new_right_key = :ici_left_key + 1;
	new_level = :ici_level;
-- creating gap in the tree where the new node was inserted
	UPDATE {objectQualifier}forum_ns
		SET left_key = left_key + 
			CASE WHEN left_key >= :ici_left_key 
			  THEN 2 
			  ELSE 0 
			END,
			right_key = right_key + 2,
			trigger_lock_update = 1
		WHERE tree = :new_tree AND right_key >= :ici_left_key;
		INSERT INTO {objectQualifier}forum_ns(parentid,boardid, categoryid,forumid, left_key, right_key,  "LEVEL", sortorder, tree, path_cache) 
					values (:new_parentid,:new_boardid,:new_categoryid,:new_forumid,
					:new_left_key,:new_right_key,:new_level,:new_sortorder, :new_tree, '')
					RETURNING nid INTO :ici_newid;    
		SUSPEND;
END;
--GO

CREATE PROCEDURE {objectQualifier}FORUM_BEFORE_INSERT_FUNC
(new_categoryid integer, new_forumid integer,
new_tree integer, new_left_key integer, 
new_right_key integer, new_level integer,  
new_parentid integer, new_sortorder integer,
new_trigger_for_delete BOOL, new_trigger_lock_update BOOL
)
AS
DECLARE	ici_left_key       INTEGER;
DECLARE	ici_level          INTEGER;
DECLARE ici_tmp_left_key   INTEGER;
DECLARE	ici_tmp_right_key  INTEGER;
DECLARE	ici_tmp_level      INTEGER;
DECLARE	ici_tmp_id         INTEGER;
DECLARE	ici_tmp_parent_id  INTEGER;
BEGIN

    -- CALL {objectQualifier}lock_ns_tree(new_tree);
-- These fields should not be set by hand
	new_trigger_for_delete = 0;
	new_trigger_lock_update = 0;
	ici_left_key = 0;
	ici_level = 0;

-- a parent was supplied
	IF (:new_parentid IS NOT NULL AND :new_parentid > 0) THEN
		 SELECT right_key,  "LEVEL" + 1			
			FROM {objectQualifier}FORUM
			WHERE FORUMID = :new_parentid AND
				  CATEGORYID = :new_tree
				  INTO :ici_left_key, :ici_level;

-- a left key was supplied
	IF ((:new_left_key IS NOT NULL AND
	   :new_left_key > 0) AND 
	   (:ici_left_key IS NULL OR :ici_left_key = 0)) THEN
	   BEGIN
		SELECT FORUMID, left_key, right_key,  "LEVEL", PARENTID 			
			FROM {objectQualifier}FORUM
			WHERE CATEGORYID = :new_tree AND (left_key = :new_left_key OR right_key = :new_left_key)
			INTO :ici_tmp_id, :ici_tmp_left_key, :ici_tmp_right_key, :ici_tmp_level, :ici_tmp_parent_id;
		IF (:ici_tmp_left_key IS NOT NULL AND :ici_tmp_left_key > 0 AND :new_left_key = :ici_tmp_left_key) THEN
		BEGIN
			new_parentid = :ici_tmp_parent_id;
			ici_left_key = :new_left_key;
			ici_level = :ici_tmp_level;
        END
		ELSE IF (:ici_tmp_left_key IS NOT NULL AND :ici_tmp_left_key > 0 AND :new_left_key = :ici_tmp_right_key) THEN
		BEGIN
			new_parentid = :ici_tmp_id;
			ici_left_key = :new_left_key;
			ici_level = :ici_tmp_level + 1;
		END
	END
-- if a parent or a left key was not supplied, we found nothing
IF (:ici_left_key IS NULL OR :ici_left_key = 0) THEN
	BEGIN
		SELECT MAX(right_key) + 1			
			FROM {objectQualifier}FORUM
			WHERE CATEGORYID = :new_tree
			INTO :ici_left_key;
		IF (:ici_left_key IS NULL OR :ici_left_key = 0) THEN
			ici_left_key = 1;
	
		ici_level = 0;
		new_parentid = NULL; 
	END
-- setting figured out keys for the node
	new_left_key = :ici_left_key;
	new_right_key = :ici_left_key + 1;
	new_level = :ici_level;
-- creating gap in the tree where the new node was inserted
	UPDATE {objectQualifier}FORUM
		SET left_key = left_key + 
			(CASE WHEN left_key >= :ici_left_key 
			  THEN 2 
			  ELSE 0 
			END),
			right_key = right_key + 2,
			trigger_lock_update = 1
		WHERE CATEGORYID = :new_tree AND right_key >= :ici_left_key;

		-- update our forum
		UPDATE {objectQualifier}FORUM
		set left_key = :new_left_key, right_key = :new_right_key, "LEVEL" = :new_level 
		where FORUMID = :new_forumid;		
END;
--GO

-- here we don't delete children but move them one level higher and remove keys gap.
CREATE PROCEDURE {objectQualifier}FORUM_AFTER_DEL2_FUNC
(i_old_tree INTEGER, i_old_left_key INTEGER, i_old_right_key INTEGER, i_old_level INTEGER, i_old_parentid INTEGER
)
AS
DECLARE i_is_locked SMALLINT = 1;
BEGIN
-- here we should lock table
--	EXECUTE {databaseSchema}.{objectQualifier}lock_ns_tree i_old_tree, i_is_locked;
-- remove break in keys and shift child nodes
   UPDATE {objectQualifier}FORUM
		SET left_key = (CASE WHEN left_key < :i_old_left_key
							THEN left_key
							ELSE (CASE WHEN right_key < :i_old_right_key
									  THEN left_key - 1 
									  ELSE left_key - 2
								 END)
					   END),
			"LEVEL" = (CASE WHEN right_key < :i_old_right_key
						   THEN "LEVEL" - 1 
						   ELSE "LEVEL"
					  END),
			PARENTID = (CASE WHEN right_key < :i_old_right_key AND "LEVEL" = :i_old_level + 1
						   THEN :i_old_parentid
						   ELSE parentid
						END),
			right_key = (CASE WHEN right_key < :i_old_right_key
							 THEN right_key - 1 
							 ELSE right_key - 2
						END),
			trigger_lock_update = 1
		WHERE (right_key > :i_old_right_key OR
			(left_key > :i_old_left_key AND right_key < :i_old_right_key)) AND
			CATEGORYID = :i_old_tree;	
END;
--GO


-- Here we delete subtrees no need here 
CREATE PROCEDURE {objectQualifier}FORUM_AFTER_DEL_FUNC(i_old_tree INTEGER, i_old_left_key INTEGER, i_old_right_key INTEGER, i_old_level INTEGER, i_old_parentid INTEGER)
AS
DECLARE	i_ici_skew_tree INTEGER;
BEGIN

--	EXECUTE PROCEDURE {objectQualifier}lock_ns_tree i_old_tree;
-- Should we to make it:
--	IF old_trigger_for_delete = 1 THEN LEAVE proc_label; END IF;
-- tick children nodes to delete:
	UPDATE {objectQualifier}FORUM
		SET trigger_for_delete = 1,
			trigger_lock_update = 1
		WHERE
			CATEGORYID = :i_old_tree AND
			left_key > :i_old_left_key AND
			right_key < :i_old_right_key;
-- remove ticked nodes:
	DELETE FROM {objectQualifier}FORUM
		WHERE
			CATEGORYID = :i_old_tree AND
			left_key > :i_old_left_key AND
			right_key < :i_old_right_key;
-- remove key gap:
	i_ici_skew_tree = :i_old_right_key - :i_old_left_key + 1;
	UPDATE {objectQualifier}FORUM
		SET left_key = (CASE WHEN left_key > :i_old_left_key
							THEN left_key - :i_ici_skew_tree
							ELSE left_key
					   END),
			right_key = right_key - :i_ici_skew_tree,
			trigger_lock_update = 1
		WHERE right_key > :i_old_right_key AND
			CATEGORYID = :i_old_tree;
	-- reset triggers (extra add on)
		UPDATE {objectQualifier}FORUM
		SET trigger_for_delete = 0,
			trigger_lock_update = 0
		WHERE
			CATEGORYID = :i_old_tree; 
END;
--go 

CREATE PROCEDURE {objectQualifier}FORUM_BEFORE_UPDATE_FUNC(i_new_nid INTEGER, 
i_new_tree INTEGER, i_new_left_key INTEGER, i_new_right_key INTEGER,i_new_level INTEGER, i_new_parentid INTEGER, i_new_trigger_lock_update smallint, 
i_new_trigger_for_delete smallint, i_old_nid INTEGER, i_old_tree INTEGER, i_old_left_key INTEGER, i_old_right_key INTEGER,i_old_level INTEGER, i_old_parentid INTEGER
)
AS
DECLARE	ici_left_key       INTEGER;
DECLARE	ici_level          INTEGER;
DECLARE	ici_skew_tree      INTEGER;
DECLARE	ici_skew_level     INTEGER;
DECLARE	ici_skew_edit      INTEGER;
DECLARE	ici_tmp_left_key   INTEGER;
DECLARE	ici_tmp_right_key  INTEGER;
DECLARE	ici_tmp_level      INTEGER;
DECLARE	ici_tmp_id         INTEGER;
DECLARE	ici_tmp_parent_id  INTEGER;
BEGIN    

	-- execute [{databaseSchema}].[{objectQualifier}lock_ns_tree] i_old_tree;
    -- maybe we should do nothing	
/*	IF i_new_trigger_lock_update = 1 BEGIN
		select i_new_trigger_lock_update = 0;
		IF i_new_trigger_for_delete = 1 BEGIN
			-- NEW = OLD;
			select i_new_tree =  i_old_tree;
			select i_new_left_key = i_old_left_key; 
			select i_new_right_key = i_old_right_key;
			select i_new_level = i_old_level; 
			select i_new_parentid = i_old_parentid;   

			select i_new_trigger_for_delete = 1;
			RETURN;
		END;
		RETURN;
	END; */

-- reset field values that can't be changed by user
/*	select i_new_trigger_for_delete = 0;
	select i_new_tree = i_old_tree;
	select i_new_right_key = i_old_right_key;
	select i_new_level = i_old_level; */	
	

-- Check if there are changes related to the tree structure. 
	IF (:i_new_parentid = :i_old_parentid AND :i_new_left_key = :i_old_left_key) THEN	
		EXIT;
	
	-- Alas, we should rebuild it, let's begin 

	ici_left_key = 0;
	ici_level = 0;
	ici_skew_tree = :i_old_right_key - :i_old_left_key + 1;
	-- where to move it? Let's guess.

-- If parentid was changed
	IF (:i_new_parentid != :i_old_parentid) THEN 
	BEGIN
-- if submission to other boss
		IF (:i_new_parentid > 0) THEN
			SELECT right_key, "LEVEL" + 1				
				FROM {objectQualifier}FORUM
				WHERE FORUMID = :i_new_parentid AND CATEGORYID = :i_new_tree
				INTO :ici_left_key, :ici_level;
				-- otherwise move it to tree root 
		ELSE
		BEGIN
			SELECT MAX(right_key) + 1 				
				FROM {objectQualifier}FORUM
				WHERE CATEGORYID = :i_new_tree
				INTO :ici_left_key;
			ici_level = 0;
		END
		
		-- check if the parent is in the scope of the node being moved
		IF (:ici_left_key IS NOT NULL AND 
		   :ici_left_key > 0 AND
		   :ici_left_key > :i_old_left_key AND
		   :ici_left_key <= :i_old_right_key) THEN 
		BEGIN
		 i_new_parentid = :i_old_parentid;
		 i_new_left_key = :i_old_left_key;
		 /* UPDATE {objectQualifier}forum_ns
		   SET
				parentid = :i_new_parentid,
				left_key = :i_new_left_key
			WHERE CATEGORYID = :i_old_tree;
		   EXIT; */
		END
	END
	-- left key was supplied, but parentid not 
   IF (:ici_left_key IS NULL OR :ici_left_key = 0) THEN 
   BEGIN
		SELECT first 1 FORUMID, left_key, right_key, "LEVEL", PARENTID 
			FROM {objectQualifier}FORUM
			WHERE CATEGORYID = :i_new_tree AND (right_key = :i_new_left_key OR right_key = :i_new_left_key - 1)
			INTO :ici_tmp_id, :ici_tmp_left_key, 
			:ici_tmp_right_key, :ici_tmp_level, :ici_tmp_parent_id;

		IF (:ici_tmp_left_key IS NOT NULL AND :ici_tmp_left_key > 0 AND (:i_new_left_key - 1 = :ici_tmp_right_key)) THEN 
		BEGIN
			i_new_parentid = :ici_tmp_parent_id;
			ici_left_key = :i_new_left_key;
			ici_level = :ici_tmp_level;
		END
		ELSE IF (:ici_tmp_left_key IS NOT NULL AND :ici_tmp_left_key > 0 AND :i_new_left_key = :ici_tmp_right_key) THEN 
		BEGIN
			i_new_parentid = :ici_tmp_id;
			ici_left_key = :i_new_left_key;
			ici_level = :ici_tmp_level + 1;
		END
		ELSE IF (:i_new_left_key = 1) THEN
		BEGIN
			i_new_parentid = 0;
			ici_left_key = :i_new_left_key;
			ici_level = 0;
		END
		ELSE
		  BEGIN
		  i_new_parentid = :i_old_parentid;
		  i_new_left_key = :i_old_left_key;
		  /* UPDATE {objectQualifier}FORUM
			SET
				parentid = :i_new_parentid,
				left_key = :i_new_left_key
			WHERE CATEGORYID = :i_old_tree;
		   EXIT;	*/
		  END		 	
	END
	-- we know now where to move the tree
	ici_skew_level = :ici_level - :i_old_level;
	IF (:ici_left_key > :i_old_left_key) THEN
	BEGIN
	-- move up the tree
	 ici_skew_edit = :ici_left_key - :i_old_left_key - :ici_skew_tree;
		UPDATE {objectQualifier}FORUM
			SET left_key =  (CASE WHEN right_key <= :i_old_right_key
								 THEN left_key + :ici_skew_edit
								 ELSE (CASE WHEN left_key > :i_old_right_key
										   THEN left_key - :ici_skew_tree
										   ELSE left_key
									  END)
							END),
				"LEVEL" =   (CASE WHEN right_key <= :i_old_right_key 
								 THEN "LEVEL" + :ici_skew_level
								 ELSE "LEVEL"
							END),
				right_key = (CASE WHEN right_key <= :i_old_right_key 
								 THEN right_key + :ici_skew_edit
								 ELSE (CASE WHEN right_key < :ici_left_key
										   THEN right_key - :ici_skew_tree
										   ELSE right_key
									  END)
							END),
				trigger_lock_update = 1
			WHERE CATEGORYID = :i_old_tree AND
				  right_key > :i_old_left_key AND
				  left_key < :ici_left_key AND
				  FORUMID <> :i_old_nid;
		ici_left_key = :ici_left_key - :ici_skew_tree;
		END
	ELSE
	BEGIN
	-- moving it down the tree
		ici_skew_edit = :ici_left_key - :i_old_left_key;
		UPDATE {objectQualifier}FORUM
			SET
				right_key = (CASE WHEN left_key >= :i_old_left_key
								 THEN right_key + :ici_skew_edit
								 ELSE (CASE WHEN right_key < :i_old_left_key
										   THEN right_key + :ici_skew_tree
										   ELSE right_key
									  END)
							END),
				"LEVEL" =   (CASE WHEN left_key >= :i_old_left_key
								 THEN "LEVEL" + :ici_skew_level
								 ELSE "LEVEL"
							END),
				left_key =  (CASE WHEN left_key >= :i_old_left_key
								 THEN left_key + :ici_skew_edit
								 ELSE (CASE WHEN left_key >= :ici_left_key
										   THEN left_key + :ici_skew_tree
										   ELSE left_key
									  END)
							END),
				 trigger_lock_update = 1
			WHERE CATEGORYID = :i_old_tree AND
				  right_key >= :ici_left_key AND
				  left_key < :i_old_right_key AND
				  FORUMID <> :i_old_nid;
	END
-- the tree was rebuilt now we heave only our current node to handle
	i_new_left_key = :ici_left_key;
	i_new_level = :ici_level;
	i_new_right_key = :ici_left_key + :ici_skew_tree - 1;	
		UPDATE {objectQualifier}FORUM
			SET
				right_key = :i_new_right_key,
				"LEVEL" =   :i_new_level,
				left_key =  :i_new_left_key,
				trigger_lock_update = 1
			WHERE CATEGORYID = :i_old_tree AND
			 FORUMID = :i_old_nid;
END;
--GO


/* ******************************************************************************************************************************
*********************************************************************************************************************************
HELER AND INITIALIZE FUNCTIONS
*********************************************************************************************************************************
******************************************************************************************************************************** */

CREATE PROCEDURE {objectQualifier}FILLIN_OR_CHECK_NS_TABLE(I_BOARDID INTEGER, I_CATEGORYID INTEGER)			
AS 
DECLARE ici_bid INTEGER;
DECLARE ici_cid INTEGER;
DECLARE ici_cs INTEGER;
 DECLARE fc_fid INTEGER;
 DECLARE fc_pid INTEGER; 
 DECLARE fc_p INTEGER; 
 DECLARE fc_cid INTEGER;
 DECLARE fc_s INTEGER;  
 DECLARE   ndfpTmp integer DEFAULT 0;
 DECLARE  frmTmp integer DEFAULT 0; 
 DECLARE  brdTmp integer DEFAULT 0; 
 DECLARE  catTmp integer DEFAULT 0; 
 DECLARE ici_fr int;
 DECLARE ici_lkey int;
 DECLARE ici_rkey int; 
 DECLARE ici_lv int;
 DECLARE ici_so int;
BEGIN
-- DELETE FROM {objectQualifier}forum_ns;
-- if ((select count(nid) from {objectQualifier}forum_ns) = 0) THEN
-- fill in boards as root (level = 0) nodes
FOR SELECT boardid
		   from  {objectQualifier}board 
		   WHERE :I_BOARDID IS NULL OR BOARDID = :I_BOARDID
		   ORDER by boardid
		   INTO :ici_bid
	   DO BEGIN
	--   EXECUTE PROCEDURE {objectQualifier}forum_ns_before_insert_func :ici_bid , 0, 0,
-- 0, null, null,null, null, 0,null, null RETURNING_VALUES :brdTmp;
	   -- fill in categories as level = 1 nodes
		 FOR SELECT c.categoryid, c.sortorder
				from  {objectQualifier}category c  
				JOIN {objectQualifier}board b 
				on b.boardid = c.boardid 
				WHERE (:I_CATEGORYID IS NULL OR CATEGORYID = :I_CATEGORYID) AND c.BOARDID = :ici_bid
				ORDER by c.boardid,c.sortorder	
				INTO :ici_cid,:ici_cs			
		 DO BEGIN
			--   EXECUTE PROCEDURE {objectQualifier}forum_ns_before_insert_func :ici_bid,:ici_cid,0,
-- 0, null, null,null, :brdTmp, :ici_cs,null, null RETURNING_VALUES :catTmp;

				--	UPDATE {objectQualifier}forum_ns SET parentid  = :ici_bid where categoryid = :ici_cid;
			 
				 -- loop through forums
					   FOR  SELECT f.forumid, f.parentid,coalesce(f.parentid, 0) parent0 ,f.categoryid, f.sortorder 
							from  {objectQualifier}forum f 
							JOIN {objectQualifier}category c on f.categoryid = c.categoryid
							 JOIN {objectQualifier}board b on b.boardid = c.boardid
							 WHERE f.categoryid = :ici_cid
							  ORDER BY parent0, f.sortorder, f.forumid
							  INTO :fc_fid,:fc_pid,:fc_p,:fc_cid,:fc_s						
				 DO BEGIN				  
											
					IF (:fc_pid IS NOT NULL) THEN				
					SELECT nid  FROM {objectQualifier}forum_ns WHERE forumid = :fc_pid into  :ndfpTmp;
										
						   -- it's a forum						  
						   EXECUTE PROCEDURE {objectQualifier}forum_ns_before_insert_func 
						   :ici_bid,:ici_cid,:fc_fid,:ici_cid, 
						   null, null,null, :ndfpTmp, :fc_s,null, null RETURNING_VALUES :frmTmp;
ndfpTmp = NULL;
		-- end of forum loop 					
		END	
	-- end of category loop
   END
	-- end of board loop
END
   -- update the real table
   FOR  SELECT f.forumid, f.LEFT_KEY,f.right_key, f."LEVEL", f.sortorder 
							from  {objectQualifier}forum_ns f 												
							INTO :ici_fr,:ici_lkey,:ici_rkey,:ici_lv,:ici_so						
				 DO 
				 BEGIN	
				 UPDATE {objectQualifier}Forum
				 SET left_key = :ici_lkey,
				     right_key = :ici_rkey,
					 "LEVEL" = :ici_lv,
					 SortOrder = :ici_so,
					 trigger_for_delete = 0,
					 trigger_lock_update = 0
				 where forumid = :ici_fr;
				 END
END;
--GO

-- Initialize all this
CREATE PROCEDURE {objectQualifier}FORUM_NS_RECREATE(I_BOARDID INTEGER, I_CATEGORYID INTEGER)
				AS				
BEGIN

EXECUTE PROCEDURE  {objectQualifier}CR_OR_CHCK_NS_TABLES;
EXECUTE PROCEDURE  {objectQualifier}FILLIN_OR_CHECK_NS_TABLE :I_BOARDID, :I_CATEGORYID;
EXECUTE PROCEDURE  {objectQualifier}CR_OR_CHCK_NS_TABLES;
END;
--GO 
CREATE PROCEDURE {objectQualifier}FORUM_NS_RECREATE_INI
				AS				
BEGIN
IF (EXISTS(SELECT 1 
               FROM {objectQualifier}FORUM WHERE LEFT_KEY IS NULL OR LEFT_KEY <= 0
               ROWS 1)) THEN 
			   BEGIN		
			   EXECUTE PROCEDURE {objectQualifier}FORUM_NS_RECREATE NULL, NULL;
			   END
END;
--GO
EXECUTE PROCEDURE {objectQualifier}FORUM_NS_RECREATE_INI;
--GO


