-- ================================================
-- Template generated from Template Explorer using:
-- Create Trigger (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- See additional Create Trigger templates for more
-- examples of different Trigger statements.
--
-- This block of comments will not be included in
-- the definition of the function.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Quoc Duong
-- Create date: 2022-08-27
-- Description:	<Description,,>
-- =============================================
CREATE TRIGGER tr_CU_comment 
   ON  Comment 
   AFTER INSERT, UPDATE
AS 
BEGIN
	DECLARE @ownerId int, @targetId int
	--CREATE or UPDATE Notif after insert
	IF (SELECT parent_id FROM inserted d) IS NULL
	BEGIN
		SET @targetId = (SELECT post_id FROM inserted)
		SET @ownerId = (SELECT user_id FROM Post where id=@targetId)
		IF @ownerId NOT IN (SELECT user_id FROM inserted) 
			EXEC sp_updateNotif @targetId, 'COMMENT_POST'
	END
	ELSE
	BEGIN
		SET @targetId = (SELECT parent_id FROM inserted)
		SET @ownerId = (SELECT user_id FROM Comment where id=@targetId)
		IF @ownerId NOT IN (SELECT user_id FROM inserted) 
			EXEC sp_updateNotif @targetId, 'REPLY_COMMENT'
	END
	--
END
GO
