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
-- Create date: 2022-25-08
-- Description:	cascade delete post
-- =============================================
CREATE TRIGGER tr_cascade_post 
   ON  dbo.Post
   INSTEAD OF DELETE
AS 
BEGIN
	BEGIN TRANSACTION
	--DELETE FK
	DELETE Comment
	WHERE post_id IN (SELECT id FROM deleted)

	DELETE React
	WHERE post_id IN (SELECT id FROM deleted)

	DELETE Notif
	WHERE post_id IN (SELECT id FROM deleted)

	DELETE Report
	WHERE target_post_id IN (SELECT id FROM deleted)


	--DELETE ITSELFT AFTER DELETE ALL FK
	DELETE Post
	WHERE id IN (SELECT id FROM deleted)
	COMMIT TRANSACTION
END
GO
