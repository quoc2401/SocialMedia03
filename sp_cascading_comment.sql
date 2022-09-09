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
-- Description:	delete react cascade for comment
-- =============================================
CREATE PROCEDURE sp_cascading_comment
	@commentId int
AS
BEGIN
	DECLARE @count int, @I int, @cur int;
	SET @count = (SELECT COUNT(id) FROM Comment WHERE parent_id=@commentId)
	SET @I = 1;

		WHILE(@I < @count)
		BEGIN
			--DELETE REACT 
			--WHERE comment_id IN (SELECT id FROM Comment where parent_id=@commentId)
			
			SET @cur = (SELECT id FROM (
			  SELECT
				ROW_NUMBER() OVER (ORDER BY id ASC) AS rownumber, id
			  FROM Comment
			  WHERE parent_id=@commentId
			) AS foo
			WHERE rownumber = @I)
			IF (@cur >= 1)
				EXEC sp_cascading_comment @cur

			SET @I = @I +1
		END

		UPDATE React
		SET comment_id = NULL
		WHERE comment_id = @commentId

		UPDATE Comment
		SET parent_id = NULL
		WHERE parent_id = @commentId

		UPDATE Notif
		SET comment_id = NULL
		WHERE comment_id = @commentId



	--DELETE react
		DELETE React
		WHERE comment_id = NULL AND post_id = NULL
	--DELETE reply
		DELETE Comment
		WHERE parent_id = NULL AND post_id = NULL
	--DELETE notif
		DELETE Notif
		WHERE comment_id = NULL AND post_id = NULL
	
END
GO
