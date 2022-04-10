DROP PROCEDURE sp_categoryUpdate
GO

CREATE PROCEDURE [dbo].[sp_categoryUpdate]
	@ShortName VARCHAR (50),
	@LongName VARCHAR (50),
	@Id int
AS
	UPDATE dbo.Category SET ShortName = @ShortName, LongName = @LongName WHERE Id = @Id
GO



DROP PROCEDURE sp_categorySelectAll
GO

CREATE PROCEDURE [dbo].[sp_categorySelectAll]
AS
	SELECT * FROM Category
GO


DROP PROCEDURE sp_categoryAdd
GO

CREATE PROCEDURE [dbo].[sp_categoryAdd]
	@ShortName VARCHAR (50),
	@LongName VARCHAR (50),
	@CategoryID int OUTPUT
AS
	INSERT INTO Category (shortname, longname) VALUES (@ShortName , @LongName)
	SET @CategoryID = @@IDENTITY
GO


DROP PROCEDURE sp_categoryDelete
GO

CREATE PROCEDURE [dbo].[sp_categoryDelete]
	@Id int
AS
	DELETE FROM dbo.Category WHERE Id = @Id
GO


DROP PROCEDURE sp_categoryGetItem
GO

CREATE PROCEDURE [dbo].[sp_categoryGetItem]
	@Id int
AS
	SELECT * FROM Category WHERE Id = @Id