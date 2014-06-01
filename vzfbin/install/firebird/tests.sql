/* integrity checks */
EXECUTE BLOCK
AS
BEGIN
IF (EXISTS(SELECT 1 
FROM RDB$PROCEDURES a WHERE a.RDB$PROCEDURE_NAME='{objectQualifier}CHECK_GUESTINUSERTABLE')) THEN
EXECUTE STATEMENT 'DROP PROCEDURE {objectQualifier}CHECK_GUESTINUSERTABLE';
END
--GO

EXECUTE BLOCK
AS
BEGIN
IF (EXISTS(SELECT 1 
FROM RDB$PROCEDURES a WHERE a.RDB$PROCEDURE_NAME='{objectQualifier}CHECK_GUESTINGUESTGROUP')) THEN
EXECUTE STATEMENT 'DROP PROCEDURE {objectQualifier}CHECK_GUESTINGUESTGROUP';
END
--GO

CREATE PROCEDURE  {objectQualifier}CHECK_GUESTINUSERTABLE(I_BOARDID INTEGER) 
 RETURNS
 (
"UserID" integer,
"UserName" varchar(255),
"DisplayName" varchar(255),
"Count" integer
)
 AS
 BEGIN
    FOR SELECT USERID, 
               NAME, 
               DISPLAYNAME, 
               COUNT(USERID)
               FROM  {objectQualifier}USER 
        WHERE
           BIN_AND(FLAGS,4) = 4 AND BOARDID=:I_BOARDID
		   GROUP BY USERID, NAME, DISPLAYNAME          
        INTO
            :"UserID",
			:"UserName",
			:"DisplayName"
			:"Count"
DO SUSPEND;
 END;
--GO

CREATE PROCEDURE  {objectQualifier}CHECK_GUESTINGUESTGROUP(I_BOARDID INTEGER) 
 RETURNS
 (
"UserID" integer,
"UserName" varchar(255),
"DisplayName" varchar(255),
"RealGuest" bool,
"Count" integer
)
 AS
 BEGIN
    FOR SELECT u.USERID, 
               u.NAME, 
               u.DISPLAYNAME,
			   u.ISGUEST, 
               COUNT(u.USERID)
               FROM  {objectQualifier}USER u
        JOIN
             {objectQualifier}USERGROUP ug ON ug.USERID = u.USERID
        JOIN
             {objectQualifier}GROUP g ON g.GROUPID = ug.GROUPID
        WHERE
            BIN_AND(g.FLAGS,2) = 2 and
            b.BOARDID=:I_BOARDID 
			 GROUP BY u.USERID, u.NAME,u. DISPLAYNAME, u.ISGUEST     
            INTO
            :"UserID",
			:"UserName",
			:"DisplayName"
			:"RealGuest",
			:"Count"
DO SUSPEND;
 END;
--GO

