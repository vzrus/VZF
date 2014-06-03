-- helper functions

DROP  PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}add_or_check_pkeys;
--GO

CREATE PROCEDURE {databaseSchema}.{objectQualifier}add_or_check_pkeys(
pk_t_name VARCHAR(99),
pk_s_modify VARCHAR(99),
pk_c_name VARCHAR(99),
pk_c2_name VARCHAR(99)
)
BEGIN

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS T WHERE T.CONSTRAINT_TYPE = 'PRIMARY KEY' AND
T.CONSTRAINT_SCHEMA = '{databaseSchema}'
AND LOWER(T.TABLE_NAME) = LOWER(CONCAT('{objectQualifier}',pk_t_name))
AND T.CONSTRAINT_TYPE = 'PRIMARY KEY') THEN

set @pk_t_name = pk_t_name ;
set @pk_c_name = pk_c_name ;
set @pk_c2_name = pk_c2_name ;

if pk_c2_name is null then
set @fk_create_string = concat('ALTER TABLE {databaseSchema}.{objectQualifier}',
@pk_t_name,' ADD PRIMARY KEY  (`',@pk_c_name,'`);');
else
set @fk_create_string = concat('ALTER TABLE {databaseSchema}.{objectQualifier}',
@pk_t_name,' ADD PRIMARY KEY  (`',@pk_c_name,'`,`',@pk_c2_name,'`);');
end if;

prepare fk_check_statement from @fk_create_string ;
execute fk_check_statement ;
deallocate prepare fk_check_statement ;

if pk_s_modify is not null then
set @pk_s_modify = pk_s_modify ;

set @pk_modify_string = concat('ALTER TABLE {databaseSchema}.{objectQualifier}',
@pk_t_name,' MODIFY COLUMN  `',@pk_c_name, ' `',@pk_s_modify, '`;');

prepare pk_modify_statement from @pk_modify_string ;
execute pk_modify_statement ;
deallocate prepare pk_modify_statement ;
end if;

END IF;
END;
--GO



DROP  PROCEDURE IF EXISTS {databaseSchema}.{objectQualifier}add_or_check_ukeys;
--GO
CREATE PROCEDURE {databaseSchema}.{objectQualifier}add_or_check_ukeys(
pk_t_name VARCHAR(99),
pk_s_modify VARCHAR(99),
pk_c_name VARCHAR(99),
pk_c2_name VARCHAR(99)
)
BEGIN

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS T WHERE T.CONSTRAINT_TYPE = 'PRIMARY KEY' AND
T.CONSTRAINT_SCHEMA = '{databaseSchema}'
AND LOWER(T.TABLE_NAME) = LOWER(CONCAT('{objectQualifier}',pk_t_name))
AND T.CONSTRAINT_NAME = 'PRIMARY') THEN

set @pk_t_name = pk_t_name ;
set @pk_c_name = pk_c_name ;


if pk_c2_name is null then
set @fk_create_string = concat('ALTER TABLE {databaseSchema}.{objectQualifier}',
@pk_t_name,' ADD PRIMARY KEY  (`',@pk_c_name,'`);');
else
set @fk_create_string = concat('ALTER TABLE {databaseSchema}.{objectQualifier}',
@pk_t_name,' ADD PRIMARY KEY  (`',@pk_c_name,'`,`',@pk_c_name,'`);');
end if;

prepare fk_check_statement from @fk_create_string ;
execute fk_check_statement ;
deallocate prepare fk_check_statement ;

if pk_s_modify is not null then
set @pk_s_modify = pk_s_modify ;

set @pk_modify_string = concat('ALTER TABLE {databaseSchema}.{objectQualifier}',
@pk_t_name,' MODIFY COLUMN  `',@pk_c_name, ' `',@pk_s_modify, '`;');

prepare pk_modify_statement from @pk_modify_string ;
execute pk_modify_statement ;
deallocate prepare pk_modify_statement ;
end if;

END IF;
END;
--GO

