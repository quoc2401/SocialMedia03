-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Quoc Duong
-- Create date: 2022-08-27
-- Description:	update is read for notif	
-- =============================================
CREATE PROCEDURE sp_updateNotif 
	@targetId int, @type nchar(20) 
AS
BEGIN
	DECLARE @ownerId int
	IF @type = 'REACT_POST' OR @type = 'COMMENT_POST'
	BEGIN
		SET @ownerId = (SELECT TOP 1 user_id FROM Post WHERE id=@targetId)
		IF NOT EXISTS (SELECT post_id FROM Notif n WHERE post_id=@targetId AND n.type=@type)
			INSERT INTO Notif(post_id, user_id, type, is_read) VALUES(@targetId, @ownerId, @type, 0);
		ELSE 
			UPDATE Notif
			SET is_read=0
			WHERE post_id=@targetId AND type=@type
	END
	ELSE IF @type = 'REACT_COMMENT' OR @type = 'REPLY_COMMENT'
	BEGIN
		SET @ownerId = (SELECT TOP 1 user_id FROM Comment WHERE id=@targetId)
		IF NOT EXISTS (SELECT comment_id FROM Notif n WHERE comment_id=@targetId AND type=@type)
			INSERT INTO Notif(comment_id, user_id, type, is_read) VALUES(@targetId, @ownerId, @type, 0);
		ELSE 
			UPDATE Notif
			SET is_read=0
			WHERE comment_id=@targetId AND type=@type
	END
END
GO