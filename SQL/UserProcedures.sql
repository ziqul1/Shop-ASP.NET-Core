DROP PROCEDURE userAdd
GO

CREATE PROCEDURE [dbo].[userAdd]
	@Login VARCHAR (90),
	@Password VARCHAR (90),
	@UserID INT OUTPUT
AS
	INSERT INTO User1 (Login, Password) VALUES (@Login, @Password)
	SET @UserID = @@IDENTITY
GO



DROP PROCEDURE usersSelectAll
GO

CREATE PROCEDURE [dbo].[usersSelectAll]
AS
	SELECT * FROM User1
GO



DROP PROCEDURE userGetByName
GO

CREATE PROCEDURE [dbo].[userGetByName]
	@Login VARCHAR (90)
AS
	SELECT * FROM User1 WHERE Login = @Login
GO