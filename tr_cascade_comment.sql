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
   ON [dbo].[Comment] 
   INSTEAD OF DELETE
AS
BEGIN
	DECLARE @deletions table (id int not null);

    WITH IDs as (
       select id from deleted
       union all
       select c.id
       from Comment c
              inner join
            IDs i
              on
                 c.parent_id = i.id
    )
	INSERT INTO @deletions(id)
	SELECT id FROM IDs

	DELETE React
	WHERE comment_id in (select id from @deletions)

	DELETE Notif
	WHERE comment_id in (select id from @deletions)

    DELETE FROM Comment
    WHERE id in (select id from @deletions)
END
GO