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
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE sp_userGetNotifs
	-- Add the parameters for the stored procedure here
	@user_id int, @offset int, @limit int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
SELECT * FROM (
	SELECT *, ROW_NUMBER() OVER(PARTITION BY notif_id ORDER BY last_modified DESC) AS RowNum FROM (
		(SELECT target_id, type, is_read, count, u.firstname as last_modified_name, u.avatar as last_modified_avatar, last_modified, notif_id, ROW_NUMBER() OVER(PARTITION BY notif_id ORDER BY last_modified DESC) AS rn
			FROM
				(SELECT n.post_id as target_id, n.user_id,  n.type, n.is_read, COUNT(distinct r.user_id) as count, last_modified_user, last_modified, n.id as notif_id
					FROM Notif n JOIN React r on n.post_id=r.post_id 
						JOIN (SELECT distinct n.post_id, n.user_id, r.user_id as last_modified_user, r.created_date as last_modified
								FROM Notif n join React r on n.post_id=r.post_id
								WHERE r.user_id != @user_id) as sub
						ON r.post_id=sub.post_id
					WHERE n.user_id = @user_id AND n.type = 'REACT_POST' AND r.user_id != @user_id
					GROUP BY n.post_id, n.user_id, n.type, n.is_read
					, last_modified_user, last_modified, n.id) AS A
			JOIN dbo.[User] u on A.last_modified_user=u.id)
		UNION
		(SELECT target_id, type, is_read, count, u.firstname as last_modified_name, u.avatar as last_modified_avatar, last_modified, notif_id, ROW_NUMBER() OVER(PARTITION BY notif_id ORDER BY last_modified DESC) AS rn
			FROM
				(SELECT n.post_id as target_id, n.user_id,  n.type, n.is_read, COUNT(distinct c.user_id) as count, last_modified_user, last_modified, n.id as notif_id
					FROM Notif n JOIN Comment c on n.post_id=c.post_id 
						JOIN (SELECT distinct n.post_id, n.user_id, c.user_id as last_modified_user, c.created_date as last_modified
								FROM Notif n join Comment c on n.post_id=c.post_id
								WHERE c.user_id != @user_id) as sub
						ON c.post_id=sub.post_id
					WHERE n.user_id = @user_id AND n.type = 'COMMENT_POST' AND c.user_id != @user_id
					GROUP BY n.post_id, n.user_id,  n.type, n.is_read, last_modified_user, last_modified, n.id) AS A
			JOIN dbo.[User] u on A.last_modified_user=u.id)
		UNION
        (SELECT target_id, type, is_read, count, u.firstname as last_modified_name, u.avatar as last_modified_avatar, last_modified, notif_id, ROW_NUMBER() OVER(PARTITION BY notif_id ORDER BY last_modified DESC) AS rn
			FROM
				(SELECT n.comment_id as target_id, n.user_id, n.type, n.is_read, COUNT(distinct r.user_id) as count, last_modified_user, last_modified, n.id as notif_id
					FROM Notif n JOIN React r on n.comment_id=r.comment_id 
						JOIN (SELECT distinct n.comment_id, n.user_id, r.user_id as last_modified_user, r.created_date as last_modified
								FROM Notif n join React r on n.comment_id=r.comment_id
								WHERE r.user_id != @user_id) as sub
						ON r.comment_id=sub.comment_id
					WHERE n.user_id = @user_id AND n.type = 'REACT_COMMENT' AND r.user_id != @user_id
					GROUP BY n.comment_id, n.user_id, n.type, n.is_read, last_modified, last_modified_user, n.id) AS A
			JOIN dbo.[User] u on A.last_modified_user=u.id)
		UNION
        (SELECT target_id, type, is_read, count, u.firstname as last_modified_name, u.avatar as last_modified_avatar, last_modified, notif_id, ROW_NUMBER() OVER(PARTITION BY notif_id ORDER BY last_modified DESC) AS rn
		FROM
			(SELECT n.comment_id as target_id, n.user_id, n.type, n.is_read, COUNT(distinct c.user_id) as count, last_modified_user, last_modified, n.id as notif_id
				FROM Notif n JOIN Comment c on n.comment_id=c.parent_id 
					JOIN (SELECT distinct n.comment_id, n.user_id, c.user_id as last_modified_user, c.created_date as last_modified
							FROM Notif n join Comment c on n.comment_id=c.parent_id
							WHERE c.user_id != @user_id) as sub
					ON c.parent_id=sub.comment_id
				WHERE n.user_id = @user_id AND n.type = 'REPLY_COMMENT' AND c.user_id != @user_id
				GROUP BY n.comment_id, n.user_id, n.type, n.is_read, last_modified_user, last_modified, n.id) AS A
		JOIN dbo.[User] u on A.last_modified_user=u.id)
	) AS R
	WHERE R.rn = 1
) AS RS
WHERE RS.RowNum BETWEEN @offset AND @limit
ORDER BY RS.last_modified DESC

END
GO
