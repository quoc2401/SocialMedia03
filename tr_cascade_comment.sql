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
-- Description:	cascade delete comment
-- =============================================
CREATE TRIGGER dbo.tr_cascade_comment
   ON dbo.Comment 
   INSTEAD OF DELETE
AS 
BEGIN
	BEGIN TRANSACTION
	--DELETE reply
		DELETE Comment
		WHERE parent_id IN (SELECT id FROM deleted)
	--DELETE react
		DELETE React
		WHERE comment_id IN (SELECT id FROM deleted)
	--DELETE notif
		DELETE Notif
		WHERE comment_id IN (SELECT id FROM deleted)
	--DELETE itselft after delete all fk
		DELETE Comment
		WHERE id IN (SELECT id FROM deleted)
	COMMIT TRANSACTION
END
GO