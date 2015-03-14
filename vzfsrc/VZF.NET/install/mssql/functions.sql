/*
  YAF SQL Functions File Created 09/07/2007
	

  Remove Comments RegEx: \/\*(.*)\*\/
  Remove Extra Stuff: SET ANSI_NULLS ON\nGO\nSET QUOTED_IDENTIFIER ON\nGO\n\n\n 
*/

-- scalar functions
IF  exists(select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}registry_value]') and type in (N'FN', N'IF', N'TF'))
DROP FUNCTION [{databaseSchema}].[{objectQualifier}registry_value]
GO

IF  exists(select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}bitset]') and type in (N'FN', N'IF', N'TF'))
DROP FUNCTION [{databaseSchema}].[{objectQualifier}bitset]
GO

IF  exists(select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}forum_posts]') and type in (N'FN', N'IF', N'TF'))
DROP FUNCTION [{databaseSchema}].[{objectQualifier}forum_posts]
GO

IF  exists(select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}forum_ns_posts]') and type in (N'FN', N'IF', N'TF'))
DROP FUNCTION [{databaseSchema}].[{objectQualifier}forum_ns_posts]
GO

IF  exists(select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}forum_topics]') and type in (N'FN', N'IF', N'TF'))
DROP FUNCTION [{databaseSchema}].[{objectQualifier}forum_topics]
GO

IF  exists(select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}forum_ns_topics]') and type in (N'FN', N'IF', N'TF'))
DROP FUNCTION [{databaseSchema}].[{objectQualifier}forum_ns_topics]
GO

IF  exists(select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}forum_subforums]') and type in (N'FN', N'IF', N'TF'))
DROP FUNCTION [{databaseSchema}].[{objectQualifier}forum_subforums]
GO

IF  exists(select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}forum_lasttopic]') and type in (N'FN', N'IF', N'TF'))
DROP FUNCTION [{databaseSchema}].[{objectQualifier}forum_lasttopic]
GO
IF  exists(select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}forum_ns_lasttopic]') and type in (N'FN', N'IF', N'TF'))
DROP FUNCTION [{databaseSchema}].[{objectQualifier}forum_ns_lasttopic]
GO

IF  exists(select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}medal_getribbonsetting]') and type in (N'FN', N'IF', N'TF'))
DROP FUNCTION [{databaseSchema}].[{objectQualifier}medal_getribbonsetting]

GO

IF  exists(select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}medal_getsortorder]') and type in (N'FN', N'IF', N'TF'))
DROP FUNCTION [{databaseSchema}].[{objectQualifier}medal_getsortorder]

GO

IF  exists(select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}medal_gethide]') and type in (N'FN', N'IF', N'TF'))
DROP FUNCTION [{databaseSchema}].[{objectQualifier}medal_gethide]

GO

IF  exists(select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}get_userstyle]') and type in (N'FN', N'IF', N'TF'))
DROP FUNCTION [{databaseSchema}].[{objectQualifier}get_userstyle]

GO

IF  exists(select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}message_getthanksinfo]') and type in (N'FN', N'IF', N'TF'))
DROP FUNCTION [{databaseSchema}].[{objectQualifier}message_getthanksinfo]

GO

IF  exists(select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}forum_save_parentschecker]') and type in (N'FN', N'IF', N'TF'))
DROP FUNCTION [{databaseSchema}].[{objectQualifier}forum_save_parentschecker]

GO

IF  exists(select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}table_intfromdelimitedstr]') and type in (N'FN', N'IF', N'TF'))
DROP FUNCTION [{databaseSchema}].[{objectQualifier}table_intfromdelimitedstr]
GO

IF  exists(select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}table_strfromdelimitedstr]') and type in (N'FN', N'IF', N'TF'))
DROP FUNCTION [{databaseSchema}].[{objectQualifier}table_strfromdelimitedstr]
GO

IF  exists(select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}topic_gettags_str]') and type in (N'FN', N'IF', N'TF'))
DROP FUNCTION [{databaseSchema}].[{objectQualifier}topic_gettags_str]
GO
 
CREATE FUNCTION [{databaseSchema}].[{objectQualifier}registry_value] (
	@Name NVARCHAR(64)
	,@BoardID INT = NULL
	)
RETURNS NVARCHAR(MAX)
AS
BEGIN
	DECLARE @returnValue NVARCHAR(MAX)

	IF @BoardID IS NOT NULL AND EXISTS(SELECT 1 FROM [{databaseSchema}].[{objectQualifier}Registry] WHERE LOWER([Name]) = LOWER(@Name) AND [BoardID] = @BoardID)
	BEGIN
		SET @returnValue = (
			SELECT CAST([Value] AS NVARCHAR(MAX))
			FROM [{databaseSchema}].[{objectQualifier}Registry]
			WHERE LOWER([Name]) = LOWER(@Name) AND [BoardID] = @BoardID)
	END
	ELSE
	BEGIN
		SET @returnValue = (
			SELECT CAST([Value] AS NVARCHAR(MAX))
			FROM [{databaseSchema}].[{objectQualifier}Registry]
			WHERE LOWER([Name]) = LOWER(@Name) AND [BoardID] IS NULL)
	END

	RETURN @returnValue
END
GO

create function [{databaseSchema}].[{objectQualifier}forum_posts](@ForumID int) returns int as
begin
	declare @NumPosts int
	declare @tmp int

	select @NumPosts=NumPosts from [{databaseSchema}].[{objectQualifier}Forum] where ForumID=@ForumID


	if exists(select 1 from [{databaseSchema}].[{objectQualifier}Forum] where ParentID=@ForumID)

	begin
		declare c cursor for
		select ForumID from [{databaseSchema}].[{objectQualifier}Forum]

		where ParentID = @ForumID
		
		open c
		
		fetch next from c into @tmp
		while @@FETCH_STATUS = 0
		begin
			set @NumPosts=@NumPosts+[{databaseSchema}].[{objectQualifier}forum_posts](@tmp)

			fetch next from c into @tmp
		end
		close c
		deallocate c
	end

	return @NumPosts
end
GO

create function [{databaseSchema}].[{objectQualifier}forum_ns_posts](@lk int, @rk int, @CategoryID int) returns int as
begin
	return (select sum(NumPosts) from [{databaseSchema}].[{objectQualifier}Forum] 
	where CategoryID = @CategoryID and left_key >= @lk and right_key <= @rk);	
end
GO

create function [{databaseSchema}].[{objectQualifier}forum_topics](@ForumID int) returns int as

begin
	declare @NumTopics int
	declare @tmp int

	select @NumTopics=NumTopics from [{databaseSchema}].[{objectQualifier}Forum] where ForumID=@ForumID


	if exists(select 1 from [{databaseSchema}].[{objectQualifier}Forum] where ParentID=@ForumID)

	begin
		declare c cursor for
		select ForumID from [{databaseSchema}].[{objectQualifier}Forum]

		where ParentID = @ForumID
		
		open c
		
		fetch next from c into @tmp
		while @@FETCH_STATUS = 0
		begin
			set @NumTopics=@NumTopics+[{databaseSchema}].[{objectQualifier}forum_topics](@tmp)

			fetch next from c into @tmp
		end
		close c
		deallocate c
	end

	return @NumTopics
end
GO

create function [{databaseSchema}].[{objectQualifier}forum_ns_topics](@lk int, @rk int, @CategoryID int) returns int as

begin
	return (select sum(NumTopics) from [{databaseSchema}].[{objectQualifier}Forum] with(nolock)
	where CategoryID = @CategoryID and left_key >= @lk and right_key <= @rk);	
end
GO

CREATE FUNCTION [{databaseSchema}].[{objectQualifier}forum_lasttopic] 

(	
	@ForumID int,
	@UserID int = null,
	@LastTopicID int = null,
	@LastPosted datetime = null
) RETURNS int AS
BEGIN
	-- local variables for temporary values
	declare @SubforumID int
	declare @TopicID int
	declare @Posted datetime

	-- try to retrieve last direct topic posed in forums if not supplied as argument 
	if (@LastTopicID is null or @LastPosted is null) BEGIN
		IF (@UserID IS NULL)
		BEGIN	
				SELECT TOP 1 
					@LastTopicID=a.LastTopicID,
					@LastPosted=a.LastPosted
				FROM
					[{databaseSchema}].[{objectQualifier}Forum] a WITH(NOLOCK)
					INNER JOIN [{databaseSchema}].[{objectQualifier}ActiveAccess] x WITH(NOLOCK) ON a.ForumID=x.ForumID
				WHERE
					a.ForumID = @ForumID AND a.IsHidden = 0
		END			
		ELSE
		BEGIN	
				SELECT TOP 1
					@LastTopicID=a.LastTopicID,
					@LastPosted=a.LastPosted
				FROM
					[{databaseSchema}].[{objectQualifier}Forum] a WITH(NOLOCK)
					INNER JOIN [{databaseSchema}].[{objectQualifier}ActiveAccess] x WITH(NOLOCK) ON a.ForumID=x.ForumID
				WHERE
					(a.IsHidden = 0 or x.ReadAccess <> 0) AND a.ForumID=@ForumID and x.UserID=@UserID
		END	
	END

	-- look for newer topic/message in subforums
	if exists(select 1 from [{databaseSchema}].[{objectQualifier}Forum] where ParentID=@ForumID)
	begin
		declare c cursor FORWARD_ONLY READ_ONLY for
			SELECT
				a.ForumID,
				a.LastTopicID,
				a.LastPosted
			FROM
				[{databaseSchema}].[{objectQualifier}Forum] a WITH(NOLOCK)
				JOIN [{databaseSchema}].[{objectQualifier}ActiveAccess] x WITH(NOLOCK) ON a.ForumID=x.ForumID
			WHERE
				a.ParentID=@ForumID and
				(					
					(x.UserID=@UserID and ((a.Flags & 2)=0 or x.ReadAccess<>0))
				)	
			UNION			
			SELECT
				a.ForumID,
				a.LastTopicID,
				a.LastPosted
			FROM
				[{databaseSchema}].[{objectQualifier}Forum] a WITH(NOLOCK)
				JOIN [{databaseSchema}].[{objectQualifier}ActiveAccess]x WITH(NOLOCK) ON a.ForumID=x.ForumID
			WHERE
				a.ParentID=@ForumID and
				(					
					(@UserID is null and (a.Flags & 2)=0)
				)
			
		open c
		
		-- cycle through subforums
		fetch next from c into @SubforumID, @TopicID, @Posted
		while @@FETCH_STATUS = 0
		begin
			-- get last topic/message info for subforum
			SELECT 
				@TopicID = LastTopicID,
				@Posted = LastPosted
			FROM
				[{databaseSchema}].[{objectQualifier}forum_lastposted](@SubforumID, @UserID, @TopicID, @Posted)


			-- if subforum has newer topic/message, make it last for parent forum
			if (@TopicID is not null and @Posted is not null and @LastPosted < @Posted) begin
				SET @LastTopicID = @TopicID
				SET @LastPosted = @Posted
			end
			-- workaround to avoid logical expressions with NULL possible differences through SQL server versions. 
			if (@TopicID is not null and @Posted is not null and @LastPosted is null) begin
				SET @LastTopicID = @TopicID
				SET @LastPosted = @Posted
			end	

			fetch next from c into @SubforumID, @TopicID, @Posted
		end
		close c
		deallocate c
	end

	-- return id of topic with last message in this forum or its subforums
	RETURN @LastTopicID
END
GO

CREATE FUNCTION [{databaseSchema}].[{objectQualifier}forum_ns_lasttopic] 

(
    @lk int,
	@rk int,
    @CategoryID int,
	@UserID int = null,
	@LastTopicID int = null,
	@LastPosted datetime = null
) RETURNS int AS
BEGIN	

		IF (@UserID IS NULL) 		
		    select top 1 @LastTopicID = f.LastTopicID from [{databaseSchema}].[{objectQualifier}Forum] f with(nolock)
			        inner join [{databaseSchema}].[{objectQualifier}ActiveAccess] x with(nolock) on f.ForumID=x.ForumID
		             where f.CategoryID = @CategoryID and f.left_key >= @lk  and f.right_key <= @rk and f.IsHidden = 0		 
                    order by f.LastPosted desc;
		else
			select top 1 @LastTopicID = f.LastTopicID from [{databaseSchema}].[{objectQualifier}Forum] f with(nolock)
			        inner join [{databaseSchema}].[{objectQualifier}ActiveAccess] x with(nolock) on f.ForumID=x.ForumID
		            where f.CategoryID = @CategoryID and f.left_key >= @lk and f.right_key <= @rk 
		            and (f.IsHidden = 0 or x.ReadAccess <> 0) 
		             and x.UserID=@UserID 		 
                      order by f.LastPosted desc;
      return  @LastTopicID
END
GO

-- table-valued functions

IF  exists(select top 1 1 from sys.objects where object_id = object_id(N'[{databaseSchema}].[{objectQualifier}forum_lastposted]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [{databaseSchema}].[{objectQualifier}forum_lastposted]
GO

CREATE FUNCTION [{databaseSchema}].[{objectQualifier}forum_lastposted] 

(	
	@ForumID int,
	@UserID int = null,
	@LastTopicID int = null,
	@LastPosted datetime = null
)
RETURNS @LastPostInForum TABLE 
(
	LastTopicID int,
	LastPosted datetime
)
AS
BEGIN
	-- local variables for temporary values
	declare @SubforumID int
	declare @TopicID int
	declare @Posted datetime

	-- try to retrieve last direct topic posed in forums if not supplied as argument 
	if (@LastTopicID is null or @LastPosted is null) BEGIN
		IF (@UserID IS NULL)
		BEGIN	
				SELECT TOP 1 
					@LastTopicID=a.LastTopicID,
					@LastPosted=a.LastPosted
				FROM
					[{databaseSchema}].[{objectQualifier}Forum] a WITH(NOLOCK)
					INNER JOIN [{databaseSchema}].[{objectQualifier}ActiveAccess] x WITH(NOLOCK) ON a.ForumID=x.ForumID
				WHERE
					a.ForumID = @ForumID AND a.IsHidden = 0
		END			
		ELSE
		BEGIN	
				SELECT TOP 1
					@LastTopicID=a.LastTopicID,
					@LastPosted=a.LastPosted
				FROM
					[{databaseSchema}].[{objectQualifier}Forum] a WITH(NOLOCK)
					INNER JOIN [{databaseSchema}].[{objectQualifier}ActiveAccess] x WITH(NOLOCK) ON a.ForumID=x.ForumID
				WHERE
					(a.IsHidden = 0 or x.ReadAccess <> 0) AND a.ForumID=@ForumID and x.UserID=@UserID
		END	
	END

	-- look for newer topic/message in subforums
	if exists(select 1 from [{databaseSchema}].[{objectQualifier}Forum] where ParentID=@ForumID)

	begin
		declare c cursor FORWARD_ONLY READ_ONLY for
			SELECT
				a.ForumID,
				a.LastTopicID,
				a.LastPosted
			FROM
				[{databaseSchema}].[{objectQualifier}Forum] a WITH(NOLOCK)
				JOIN [{databaseSchema}].[{objectQualifier}ActiveAccess] x WITH(NOLOCK) ON a.ForumID=x.ForumID
			WHERE
				a.ParentID=@ForumID and
				(					
					(x.UserID=@UserID and ((a.Flags & 2)=0 or x.ReadAccess<>0))
				)	
			UNION			
			SELECT
				a.ForumID,
				a.LastTopicID,
				a.LastPosted
			FROM
				[{databaseSchema}].[{objectQualifier}Forum] a WITH(NOLOCK)
				JOIN [{databaseSchema}].[{objectQualifier}ActiveAccess]x WITH(NOLOCK) ON a.ForumID=x.ForumID
			WHERE
				a.ParentID=@ForumID and
				(					
					(@UserID is null and (a.Flags & 2)=0)
				)
			
		open c
		
		-- cycle through subforums
		fetch next from c into @SubforumID, @TopicID, @Posted
		while @@FETCH_STATUS = 0
		begin
			-- get last topic/message info for subforum
			SELECT 
				@TopicID = LastTopicID,
				@Posted = LastPosted
			FROM
				[{databaseSchema}].[{objectQualifier}forum_lastposted](@SubforumID, @UserID, @TopicID, @Posted)


			-- if subforum has newer topic/message, make it last for parent forum
			if (@TopicID is not null and @Posted is not null and @LastPosted < @Posted) begin
				SET @LastTopicID = @TopicID
				SET @LastPosted = @Posted
			end

			fetch next from c into @SubforumID, @TopicID, @Posted
		end
		close c
		deallocate c
	end

	-- return vector
	INSERT @LastPostInForum
	SELECT 
		@LastTopicID,
		@LastPosted
	RETURN
END
GO

CREATE FUNCTION [{databaseSchema}].[{objectQualifier}medal_getribbonsetting]
(
	@RibbonURL nvarchar(250),
	@Flags int,
	@OnlyRibbon bit
)
RETURNS bit
AS
BEGIN

	if ((@RibbonURL is null) or ((@Flags & 2) = 0)) set @OnlyRibbon = 0

	return @OnlyRibbon

END
GO

CREATE FUNCTION [{databaseSchema}].[{objectQualifier}medal_getsortorder]
(
	@SortOrder tinyint,
	@DefaultSortOrder tinyint,
	@Flags int
)
RETURNS tinyint
AS
BEGIN

	if ((@Flags & 8) = 0) set @SortOrder = @DefaultSortOrder

	return @SortOrder

END
GO

CREATE FUNCTION [{databaseSchema}].[{objectQualifier}medal_gethide]
(
	@Hide bit,
	@Flags int
)
RETURNS bit
AS
BEGIN

	if ((@Flags & 4) = 0) set @Hide = 0

	return @Hide

END
GO

-- Gets the Thanks info which will be formatted and then placed in "dvThanksInfo" Div Tag in displaypost.ascx.
create function [{databaseSchema}].[{objectQualifier}message_getthanksinfo]
(
@MessageID INT,
@ShowThanksDate bit
) returns NVARCHAR(MAX)
BEGIN
	DECLARE @Output NVARCHAR(MAX)
		SELECT @Output = COALESCE(@Output+',', '') + CAST(i.ThanksFromUserID AS varchar) + 
	CASE @ShowThanksDate WHEN 1 THEN ',' + CAST (i.ThanksDate AS varchar)  ELSE '' end
			FROM	[{databaseSchema}].[{objectQualifier}Thanks] i
			WHERE	i.MessageID = @MessageID	ORDER BY i.ThanksDate
	-- Add the last comma if @Output has data.
	IF @Output <> ''
		SELECT @Output = @Output + ','
	RETURN @Output
END
GO

create function [{databaseSchema}].[{objectQualifier}forum_save_parentschecker](@ForumID int, @ParentID int) returns int as

begin
-- Checks if the forum is already referenced as a parent 
	declare @dependency int
	declare @haschildren int
	declare @frmtmp int
	declare @prntmp int
	
	set @dependency = 0
	set @haschildren = 0
	
	select @dependency=ForumID from [{databaseSchema}].[{objectQualifier}Forum] where ParentID=@ForumID AND ForumID = @ParentID;
	if @dependency > 0
	begin
	return @ParentID
	end

	if exists(select 1 from [{databaseSchema}].[{objectQualifier}Forum] where ParentID=@ForumID)
		begin        
		declare c cursor for
		select ForumID,ParentID from [{databaseSchema}].[{objectQualifier}Forum]
		where ParentID = @ForumID
		
		open c
		
		fetch next from c into @frmtmp,@prntmp
		while @@FETCH_STATUS = 0
		begin
		if @frmtmp > 0 AND @frmtmp IS NOT NULL
		 begin        
			set @haschildren= [{databaseSchema}].[{objectQualifier}forum_save_parentschecker](@frmtmp,@ParentID)            
			if  @prntmp = @ParentID
			begin
			set @dependency= @ParentID
			end    
			else if @haschildren > 0
			begin
			set @dependency= @haschildren
			end        
		end
		fetch next from c into @frmtmp,@prntmp
		end
		close c
		deallocate c    
	end
	return @dependency
end
GO

CREATE FUNCTION [{databaseSchema}].[{objectQualifier}table_intfromdelimitedstr]
(
   @sInputList VARCHAR(MAX), -- List of delimited items
   @sDelimiter CHAR(1) = ',' -- delimiter that separates items
) RETURNS @List TABLE (item int)

	BEGIN
	DECLARE @sItem int
	WHILE CHARINDEX(@sDelimiter,@sInputList,0) <> 0
	 BEGIN
	 SELECT
	  @sItem=CONVERT(int,RTRIM(LTRIM(SUBSTRING(@sInputList,1,CHARINDEX(@sDelimiter,@sInputList,0)-1)))),
	  @sInputList=RTRIM(LTRIM(SUBSTRING(@sInputList,CHARINDEX(@sDelimiter,@sInputList,0)+LEN(@sDelimiter),LEN(@sInputList))))
 
	 IF LEN(@sItem) > 0
	  INSERT INTO @List SELECT CONVERT(int,@sItem)
	 END

	IF LEN(@sInputList) > 0
	 INSERT INTO @List SELECT @sInputList -- Put the last item in
	RETURN
	END
GO

CREATE FUNCTION [{databaseSchema}].[{objectQualifier}table_strfromdelimitedstr]
(
   @sInputList NVARCHAR(MAX), -- List of delimited items
   @sDelimiter CHAR(1) = ',' -- delimiter that separates items
) RETURNS @List TABLE (ind INT,item NVARCHAR(255))

	BEGIN
	DECLARE @sItem int;
	DECLARE @ind int = 1;
	
	WHILE CHARINDEX(@sDelimiter,@sInputList,0) <> 0
	 BEGIN
	 SELECT
	  @sItem=RTRIM(LTRIM(SUBSTRING(@sInputList,1,CHARINDEX(@sDelimiter,@sInputList,0)-1))),
	  @sInputList=RTRIM(LTRIM(SUBSTRING(@sInputList,CHARINDEX(@sDelimiter,@sInputList,0)+LEN(@sDelimiter),LEN(@sInputList))))
 
	 IF LEN(@sItem) > 0
	 begin
	  INSERT INTO @List SELECT @ind, @sItem
	  select @ind = @ind + 1;
	 end
	 END

	IF LEN(@sInputList) > 0
	begin
	 INSERT INTO @List SELECT @ind, @sInputList -- Put the last item in
	end
	RETURN
	END
GO

CREATE FUNCTION [{databaseSchema}].[{objectQualifier}topic_gettags_str] 
	(@TopicID int)
RETURNS NVARCHAR(MAX)
AS
BEGIN
DECLARE @listStr NVARCHAR(MAX)
SELECT
@listStr =
COALESCE (COALESCE(@listStr+',' ,'') + Tag , @listStr)
FROM [{databaseSchema}].[{objectQualifier}Tags] tg JOIN  [{databaseSchema}].[{objectQualifier}TopicTags] tt on tt.TagID = tg.TagID where tt.TopicID = @TopicID
RETURN @listStr
END
GO