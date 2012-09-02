/******************************************************************************/
/*                Автор Е.Виноградный (ssdi)                                  */
/******************************************************************************/
/****    Процедуры для обслуживания индексов БД                            ****/
/******************************************************************************/
 
SET TERM ^ ;
 
-- Расчитывает селективность всех индексов
CREATE OR ALTER PROCEDURE INDICES_REBUILD_SELECTIVITY
AS
DECLARE VARIABLE S VARCHAR(200);
BEGIN
  /*Процедура для перерасчета селективности индексов, запускать переодически (если не было ресторинга БД),
  либо в случае больших изменений в БД.*/
  FOR SELECT RDB$INDEX_NAME FROM RDB$INDICES INTO :S DO
  BEGIN
    S = 'SET statistics INDEX ' || S || ';';
    EXECUTE STATEMENT :S;
  END
  SUSPEND;
END^
 
-- Включает/выключает индексы в БД
CREATE OR ALTER PROCEDURE INDICES_SWITCH (
    ENABLE_FLAG INTEGER)
AS
DECLARE VARIABLE RELATION_NAME VARCHAR(256);
DECLARE VARIABLE INDEX_INACTIVE INTEGER;
DECLARE VARIABLE ACTION_NAME VARCHAR(50);
DECLARE VARIABLE SQL VARCHAR(256);
BEGIN
  /* Переключает состояние индексов */
  /* source SQL
  SELECT R.RDB$CONSTRAINT_NAME, R.RDB$INDEX_NAME AS REFINDEXNAME, I.RDB$INDEX_NAME AS REALINDEX, I.RDB$RELATION_NAME, I.RDB$INDEX_INACTIVE 
  FROM RDB$INDICES I RIGHT JOIN RDB$RELATION_CONSTRAINTS R ON I.RDB$INDEX_NAME = R.RDB$INDEX_NAME 
  WHERE R.RDB$CONSTRAINT_TYPE = 'FOREIGN KEY' OR R.RDB$CONSTRAINT_TYPE = 'PRIMARY KEY'
  ORDER BY R.RDB$CONSTRAINT_NAME
  */
  --Все активные переводим в неактивные (default)
  INDEX_INACTIVE = 0;
  ACTION_NAME = 'INACTIVE';
  IF (ENABLE_FLAG > 0) THEN
  BEGIN
    --Перевод в активное состояние
    INDEX_INACTIVE = 3;
    ACTION_NAME = 'ACTIVE';
  END
 
  FOR SELECT I.RDB$INDEX_NAME
  FROM RDB$INDICES I
  RIGHT JOIN RDB$RELATION_CONSTRAINTS R ON I.RDB$INDEX_NAME = R.RDB$INDEX_NAME 
  WHERE (R.RDB$CONSTRAINT_TYPE = 'FOREIGN KEY' OR R.RDB$CONSTRAINT_TYPE = 'PRIMARY KEY')
        AND (I.RDB$INDEX_INACTIVE = :INDEX_INACTIVE)
  INTO :RELATION_NAME DO
  BEGIN
    SQL = 'ALTER INDEX ' || RELATION_NAME || ' ' || ACTION_NAME;
    IF (SQL IS NOT NULL) THEN EXECUTE STATEMENT SQL;
  END
END^
 
-- Переактивация индексов через выключение-включение
CREATE OR ALTER PROCEDURE INDICES_REACTIVATE
AS
BEGIN
  EXECUTE PROCEDURE INDICES_SWITCH(0);
  EXECUTE PROCEDURE INDICES_SWITCH(1);
END^
 
SET TERM ; ^