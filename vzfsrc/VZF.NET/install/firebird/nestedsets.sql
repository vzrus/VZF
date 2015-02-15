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
	WHERE (I.RDB$INDEX_NAME = 'IX_{objectQualifier}FORUM_NS_LK_RK_L_T') AND (I.RDB$RELATION_NAME = '{objectQualifier}FORUM_NS')
   )) THEN
EXECUTE STATEMENT 'CREATE INDEX IX_{objectQualifier}FORUM_NS_LK_RK_L_T ON {objectQualifier}FORUM_NS(LEFT_KEY, RIGHT_KEY, LEVEL, TREE);';


 IF (NOT EXISTS (
	SELECT 1 FROM RDB$INDICES I
	WHERE (I.RDB$INDEX_NAME = 'IX_{objectQualifier}FORUM_NS_PARENTID') AND (I.RDB$RELATION_NAME = '{objectQualifier}FORUM_NS')
   )) THEN
  EXECUTE STATEMENT 'CREATE INDEX IX_{objectQualifier}FORUM_NS_PARENTID ON {objectQualifier}FORUM_NS(PARENTID);';
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



/* ******************************************************************************************************************************
*********************************************************************************************************************************
HELER AND INITIALIZE FUNCTIONS
*********************************************************************************************************************************
******************************************************************************************************************************** */

CREATE PROCEDURE {objectQualifier}FILLIN_OR_CHECK_NS_TABLE			
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
BEGIN
-- DELETE FROM {objectQualifier}forum_ns;
-- if ((select count(nid) from {objectQualifier}forum_ns) = 0) THEN
-- fill in boards as root (level = 0) nodes
FOR SELECT boardid
		   from  {objectQualifier}board 
		   ORDER by boardid
		   INTO :ici_bid
	   DO BEGIN
	   EXECUTE PROCEDURE {objectQualifier}forum_ns_before_insert_func :ici_bid , 0, 0,
 0, null, null,null, null, 0,null, null RETURNING_VALUES :brdTmp;
	   -- fill in categories as level = 1 nodes
		 FOR SELECT c.categoryid, c.sortorder
				from  {objectQualifier}category c  
				JOIN {objectQualifier}board b 
				on b.boardid = c.boardid 
				WHERE c.boardid = :ici_bid
				ORDER by c.boardid,c.sortorder	
				INTO :ici_cid,:ici_cs			
		 DO BEGIN
			   EXECUTE PROCEDURE {objectQualifier}forum_ns_before_insert_func :ici_bid,:ici_cid,0,
 0, null, null,null, :brdTmp, :ici_cs,null, null RETURNING_VALUES :catTmp;

					UPDATE {objectQualifier}forum_ns SET parentid  = :ici_bid where categoryid = :ici_cid;
			 
				 -- loop through forums
					   FOR  SELECT f.forumid, f.parentid,coalesce(f.parentid, 0) parent0 ,f.categoryid, f.sortorder 
							from  {objectQualifier}forum f 
							JOIN {objectQualifier}category c on f.categoryid = c.categoryid
							 JOIN {objectQualifier}board b on b.boardid = c.boardid
							 WHERE f.categoryid = :ici_cid
							  ORDER by c.boardid, f.categoryid, parent0,f.sortorder, f.forumid
							  INTO :fc_fid,:fc_pid,:fc_p,:fc_cid,:fc_s						
				 DO BEGIN				  
											
					IF (:fc_p IS NULL) THEN
					SELECT nid FROM {objectQualifier}forum_ns WHERE categoryid = :fc_cid and forumid = 0  into :ndfpTmp;
						ELSE
					SELECT nid  FROM {objectQualifier}forum_ns WHERE forumid = :fc_pid into  :ndfpTmp;
										
						   -- it's a forum						  
						   EXECUTE PROCEDURE {objectQualifier}forum_ns_before_insert_func :ici_bid,:ici_cid,COALESCE(:fc_fid,0),
 0, null, null,null, :catTmp, :fc_s,null, null RETURNING_VALUES :frmTmp;
		-- end of forum loop 					
		END	
	-- end of category loop
   END
	-- end of board loop
END
END;
-- GO


