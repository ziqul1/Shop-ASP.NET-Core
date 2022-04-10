DROP PROCEDURE sp_productUpdate
GO

CREATE PROCEDURE [dbo].[sp_productUpdate]
	@Name VARCHAR (50),
	@Price MONEY,
	@CategoryId INT,
	@Id INT
AS
	UPDATE dbo.Product SET Name = @Name, Price = @Price, CategoryId = @CategoryId WHERE Id = @Id
GO



DROP PROCEDURE sp_productSelectAll
GO

CREATE PROCEDURE [dbo].[sp_productSelectAll]
AS
	SELECT * FROM Product
GO


DROP PROCEDURE sp_productAdd
GO

CREATE PROCEDURE [dbo].[sp_productAdd]
	@Name VARCHAR (50),
	@Price MONEY,
	@CategoryId INT,
	@ProductID INT OUTPUT
AS
	INSERT INTO Product (Name, Price, CategoryId) VALUES (@Name, @Price, @CategoryId)
	SET @productID = @@IDENTITY
GO


DROP PROCEDURE sp_productDelete
GO

CREATE PROCEDURE [dbo].[sp_productDelete]
	@Id INT
AS
	DELETE FROM dbo.Product WHERE Id = @Id
GO


DROP PROCEDURE sp_productGetItem
GO

CREATE PROCEDURE [dbo].[sp_productGetItem]
	@Id INT
AS
	SELECT * FROM Product WHERE Id = @Id