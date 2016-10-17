USE [master]
GO

IF DB_ID('PDMDB') IS NOT NULL
DROP DATABASE PDMDB;
GO

CREATE DATABASE PDMDB
GO

USE [PDMDB]
GO

CREATE SCHEMA Production
GO

USE [PDMDB]
GO
CREATE TABLE Production.ProductModel
(
	[ProductModelID] [int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[rowguid] [uniqueidentifier] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
)
GO

USE [PDMDB]
GO
CREATE TABLE [Production].[ProductPhoto](
	[PhotoID] [int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Image] [varbinary](max) NOT NULL,
	[rowguid] [uniqueidentifier] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL
)
GO

USE [PDMDB]
GO
CREATE TABLE [Production].[ProductCategory](
	[ProductCategoryID] [int] PRIMARY KEY  IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[rowguid] [uniqueidentifier] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL
)
GO

USE [PDMDB]
GO
CREATE TABLE [Production].[ProductSubCategory](
	[ProductSubCategoryID] [int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[rowguid] [uniqueidentifier] NOT NULL,
	[ProductCategoryID] [int] FOREIGN KEY REFERENCES [Production].[ProductCategory](ProductCategoryID) NOT NULL,
	[ModifiedDate] [datetime] NOT NULL
)
GO

USE [PDMDB]
GO
CREATE TABLE [Production].[Product](
	[ProductID] [int] PRIMARY KEY  IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[ProductNumber] [nvarchar](25) NOT NULL,
	[MakeFlag] [bit] NOT NULL,
	[Color] [nvarchar](15) NOT NULL,
	[StandardCost] [money] NOT NULL,
	[ListPrice] [money] NOT NULL,
	[Size] [nvarchar](5) NOT NULL,
	[Weight] [decimal](8,2) NOT NULL,
	[Style] [nchar](2) NOT NULL,
	[SellStartDate] [datetime] NOT NULL,
	[rowguid] [uniqueidentifier] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[ProductSubCategoryID] [int] FOREIGN KEY REFERENCES [Production].[ProductSubCategory](ProductSubCategoryID) NOT NULL,
	[ProductModelID] [int] FOREIGN KEY REFERENCES [Production].[ProductModel](ProductModelID) NOT NULL,
	[PhotoID] [int] FOREIGN KEY REFERENCES [Production].[ProductPhoto](PhotoID) NOT NULL
)
GO


USE [PDMDB]
GO

IF OBJECT_ID('usp_InsertProductModel') IS NOT NULL
DROP PROCEDURE usp_InsertProductModel;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

USE [PDMDB]
GO

CREATE PROCEDURE usp_InsertProductModel
@name NVARCHAR(50),
@modifiedDate DATETIME,
@productModelID INT OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO [Production].[ProductModel](Name, rowguid,ModifiedDate)
	VALUES(@name, NEWID(), @modifiedDate)
	SELECT @productModelID =@@IDENTITY

END
GO


USE [PDMDB]
GO

IF OBJECT_ID('usp_DeleteProductModel') IS NOT NULL
DROP PROCEDURE usp_DeleteProductModel;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

USE [PDMDB]
GO

CREATE PROCEDURE usp_DeleteProductModel
@productModelID INT,
@rowseffected INT OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	DELETE FROM [Production].[ProductModel] WHERE ProductModelID = @productModelID
	
	SELECT @rowseffected =@@ROWCOUNT

END
GO

USE [PDMDB]
GO

IF OBJECT_ID('usp_UpdateProductModel') IS NOT NULL
DROP PROCEDURE usp_UpdateProductModel;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

USE [PDMDB]
GO

CREATE PROCEDURE usp_UpdateProductModel
@productModelID INT,
@name NVARCHAR(50),
@modifiedDate DATETIME,
@rowseffected INT OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE [Production].[ProductModel] 
	SET Name = @name, ModifiedDate = @modifiedDate
	WHERE ProductModelID = @productModelID
	
	SELECT @rowseffected =@@ROWCOUNT

END
GO

USE [PDMDB]
GO

IF OBJECT_ID('usp_GetProductModelByID') IS NOT NULL
DROP PROCEDURE usp_GetProductModelByID;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

USE [PDMDB]
GO

CREATE PROCEDURE usp_GetProductModelByID
@productModelID INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		[ProductModelID],
		[Name],
		[ModifiedDate]
	FROM [Production].[ProductModel]
	WHERE ProductModelID =@productModelID
END
GO

USE [PDMDB]
GO

IF OBJECT_ID('usp_GetAllProductModel') IS NOT NULL
DROP PROCEDURE usp_GetAllProductModel;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

USE [PDMDB]
GO

CREATE PROCEDURE usp_GetAllProductModel
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		[ProductModelID],
		[Name],
		[ModifiedDate]
	FROM [Production].[ProductModel]
END
GO








USE [PDMDB]
GO

IF OBJECT_ID('usp_InsertProductPhoto') IS NOT NULL
DROP PROCEDURE usp_InsertProductPhoto;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

USE [PDMDB]
GO

CREATE PROCEDURE usp_InsertProductPhoto
@name NVARCHAR(50),
@image varbinary(max),
@modifiedDate DATETIME,
@photoID INT OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO [Production].[ProductPhoto](Name,Image,rowguid,ModifiedDate)
	VALUES(@name,@image, NEWID(),@modifiedDate)
	SELECT @photoID =@@IDENTITY

END
GO


USE [PDMDB]
GO

IF OBJECT_ID('usp_DeleteProductPhoto') IS NOT NULL
DROP PROCEDURE usp_DeleteProductPhoto;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

USE [PDMDB]
GO

CREATE PROCEDURE usp_DeleteProductPhoto
@photoID INT,
@rowseffected INT OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	DELETE FROM [Production].[ProductPhoto] WHERE PhotoID = @photoID
	
	SELECT @rowseffected =@@ROWCOUNT

END
GO

USE [PDMDB]
GO

IF OBJECT_ID('usp_UpdateProductPhoto') IS NOT NULL
DROP PROCEDURE usp_UpdateProductPhoto;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

USE [PDMDB]
GO

CREATE PROCEDURE usp_UpdateProductPhoto
@photoID INT,
@name NVARCHAR(50),
@image varbinary(max),
@modifiedDate DATETIME,
@rowseffected INT OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE [Production].[ProductPhoto] 
	SET Name = @name, Image=@image, ModifiedDate = @modifiedDate
	WHERE PhotoID = @photoID
	
	SELECT @rowseffected =@@ROWCOUNT

END
GO

USE [PDMDB]
GO

IF OBJECT_ID('usp_GetProductPhotoByID') IS NOT NULL
DROP PROCEDURE usp_GetProductPhotoByID;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

USE [PDMDB]
GO

CREATE PROCEDURE usp_GetProductPhotoByID
@photoID INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		[PhotoID],
		[Name],
		[Image],
		[ModifiedDate]
	FROM [Production].[ProductPhoto]
	WHERE PhotoID =@photoID
END
GO

USE [PDMDB]
GO

IF OBJECT_ID('usp_GetAllProductPhoto') IS NOT NULL
DROP PROCEDURE usp_GetAllProductPhoto;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

USE [PDMDB]
GO

CREATE PROCEDURE usp_GetAllProductPhoto
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		[PhotoID],
		[Name],
		[Image],
		[ModifiedDate]
	FROM [Production].[ProductPhoto]
END
GO


USE [PDMDB]
GO

IF OBJECT_ID('usp_InsertProductSubCategory') IS NOT NULL
DROP PROCEDURE usp_InsertProductSubCategory;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

USE [PDMDB]
GO

CREATE PROCEDURE usp_InsertProductSubCategory
@name NVARCHAR(50),
@modifiedDate DATETIME,
@productCategoryID INT,
@productSubCategoryID INT OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO [Production].[ProductSubCategory](Name, rowguid,ProductCategoryID,ModifiedDate)
	VALUES(@name, NEWID(), @productCategoryID,@modifiedDate)
	SELECT @productSubCategoryID =@@IDENTITY

END
GO


USE [PDMDB]
GO

IF OBJECT_ID('usp_DeleteProductSubCategory') IS NOT NULL
DROP PROCEDURE usp_DeleteProductSubCategory;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

USE [PDMDB]
GO

CREATE PROCEDURE usp_DeleteProductSubCategory
@productSubCategoryID INT,
@rowseffected INT OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	DELETE FROM [Production].[ProductSubCategory] WHERE ProductSubCategoryID = @productSubCategoryID
	
	SELECT @rowseffected =@@ROWCOUNT

END
GO

USE [PDMDB]
GO

IF OBJECT_ID('usp_UpdateProductSubCategory') IS NOT NULL
DROP PROCEDURE usp_UpdateProductSubCategory;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

USE [PDMDB]
GO

CREATE PROCEDURE usp_UpdateProductSubCategory
@productSubCategoryID INT,
@name NVARCHAR(50),
@modifiedDate DATETIME,
@rowseffected INT OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE [Production].[ProductSubCategory] 
	SET Name = @name, ModifiedDate = @modifiedDate
	WHERE ProductSubCategoryID = @productSubCategoryID
	
	SELECT @rowseffected =@@ROWCOUNT

END
GO

USE [PDMDB]
GO

IF OBJECT_ID('usp_GetProductSubCategoryByID') IS NOT NULL
DROP PROCEDURE usp_GetProductSubCategoryByID;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

USE [PDMDB]
GO

CREATE PROCEDURE usp_GetProductSubCategoryByID
@productSubCategoryID INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		[ProductCategoryID],
		[Name],
		[ModifiedDate],
		[ProductSubCategoryID]
	FROM [Production].[ProductSubCategory]
	WHERE ProductSubCategoryID =@productSubCategoryID
END
GO

USE [PDMDB]
GO

IF OBJECT_ID('usp_GetAllProductSubCategory') IS NOT NULL
DROP PROCEDURE usp_GetAllProductSubCategory;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

USE [PDMDB]
GO

CREATE PROCEDURE usp_GetAllProductSubCategory
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		[ProductCategoryID],
		[Name],
		[ModifiedDate],
		[ProductSubCategoryID]
	FROM [Production].[ProductSubCategory]
END
GO


USE [PDMDB]
GO

IF OBJECT_ID('usp_InsertProductCategory') IS NOT NULL
DROP PROCEDURE usp_InsertProductCategory;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

USE [PDMDB]
GO

CREATE PROCEDURE usp_InsertProductCategory
@name NVARCHAR(50),
@modifiedDate DATETIME,
@productCategoryID INT OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO [Production].[ProductCategory](Name, rowguid,ModifiedDate)
	VALUES(@name, NEWID(), @modifiedDate)
	SELECT @productCategoryID =@@IDENTITY

END
GO


USE [PDMDB]
GO

IF OBJECT_ID('usp_DeleteProductCategory') IS NOT NULL
DROP PROCEDURE usp_DeleteProductCategory;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

USE [PDMDB]
GO

CREATE PROCEDURE usp_DeleteProductCategory
@productCategoryID INT,
@rowseffected INT OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	DELETE FROM [Production].[ProductCategory] WHERE ProductCategoryID = @productCategoryID
	
	SELECT @rowseffected =@@ROWCOUNT

END
GO

USE [PDMDB]
GO

IF OBJECT_ID('usp_UpdateProductCategory') IS NOT NULL
DROP PROCEDURE usp_UpdateProductCategory;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

USE [PDMDB]
GO

CREATE PROCEDURE usp_UpdateProductCategory
@productCategoryID INT,
@name NVARCHAR(50),
@modifiedDate DATETIME,
@rowseffected INT OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE [Production].[ProductCategory] 
	SET Name = @name, ModifiedDate = @modifiedDate
	WHERE ProductCategoryID = @productCategoryID
	
	SELECT @rowseffected =@@ROWCOUNT

END
GO

USE [PDMDB]
GO

IF OBJECT_ID('usp_GetProductCategoryByID') IS NOT NULL
DROP PROCEDURE usp_GetProductCategoryByID;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

USE [PDMDB]
GO

CREATE PROCEDURE usp_GetProductCategoryByID
@productCategoryID INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		[ProductCategoryID],
		[Name],
		[ModifiedDate]
	FROM [Production].[ProductCategory]
	WHERE ProductCategoryID =@productCategoryID
END
GO

USE [PDMDB]
GO

IF OBJECT_ID('usp_GetAllProductCategory') IS NOT NULL
DROP PROCEDURE usp_GetAllProductCategory;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

USE [PDMDB]
GO

CREATE PROCEDURE usp_GetAllProductCategory
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		[ProductCategoryID],
		[Name],
		[ModifiedDate]
	FROM [Production].[ProductCategory]
END
GO


USE [PDMDB]
GO

IF OBJECT_ID('usp_InsertProduct') IS NOT NULL
DROP PROCEDURE usp_InsertProduct
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

USE [PDMDB]
GO

CREATE PROCEDURE usp_InsertProduct
@name NVARCHAR(50),
@productNumber NVARCHAR(25),
@makeFlag BIT,
@color NVARCHAR(15),
@standardCost MONEY,
@listPrice MONEY,
@size  NVARCHAR(5),
@weight DECIMAL(8,2),
@style NCHAR(2),
@sellStartDate  DATETIME,
@modifiedDate DATETIME,
@productSubCategoryID INT,
@productModelID INT,
@photoID INT,
@productID INT OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO [Production].[Product]
	(
	Name,
	ProductNumber,
	MakeFlag,
	Color,
	StandardCost,
	ListPrice,
	Size,
	Weight,
	Style,
	SellStartDate,
	rowguid,
	ModifiedDate,
	ProductSubCategoryID,
	ProductModelID,
	PhotoID
	)
	VALUES(
	@name,
	@productNumber,
	@MakeFlag,
	@color,
	@standardCost,
	@listPrice,
	@size,
	@weight,
	@style,
	@sellStartDate,
	NEWID(),
	@modifiedDate,
	@productSubCategoryID,
	@productModelID,
	@photoID
	)
	SELECT @productID =@@IDENTITY

END
GO

USE [PDMDB]
GO

IF OBJECT_ID('usp_DeleteProduct') IS NOT NULL
DROP PROCEDURE usp_DeleteProduct;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

USE [PDMDB]
GO

CREATE PROCEDURE usp_DeleteProduct
@productID INT,
@rowseffected INT OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	DELETE FROM [Production].[Product] where ProductID =@productID
	SELECT @rowseffected =@@ROWCOUNT

END
GO

IF OBJECT_ID('usp_GetAllProduct') IS NOT NULL
DROP PROCEDURE usp_GetAllProduct;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

USE [PDMDB]
GO

CREATE PROCEDURE usp_GetAllProduct

AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
	ProductID,
	Name,
	ProductNumber,
	MakeFlag,
	Color,
	StandardCost,
	ListPrice,
	Size,
	Weight,
	Style,
	SellStartDate,
	ModifiedDate,
	ProductSubCategoryID,
	ProductModelID,
	PhotoID
	FROM [Production].[Product]
END
GO

IF OBJECT_ID('usp_GetProductByID') IS NOT NULL
DROP PROCEDURE usp_GetProductByID;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

USE [PDMDB]
GO

CREATE PROCEDURE usp_GetProductByID
@productID INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
	ProductID,
	Name,
	ProductNumber,
	MakeFlag,
	Color,
	StandardCost,
	ListPrice,
	Size,
	Weight,
	Style,
	SellStartDate,
	ModifiedDate,
	ProductSubCategoryID,
	ProductModelID,
	PhotoID
	FROM [Production].[Product]
	WHERE ProductID =@productID
END
GO


IF OBJECT_ID('usp_UpdateProduct') IS NOT NULL
DROP PROCEDURE usp_UpdateProduct;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

USE [PDMDB]
GO

CREATE PROCEDURE usp_UpdateProduct
@productID INT,
@name NVARCHAR(50),
@productNumber NVARCHAR(25),
@makeFlag BIT,
@color NVARCHAR(15),
@standardCost MONEY,
@listPrice MONEY,
@size  NVARCHAR(5),
@weight DECIMAL(8,2),
@style NCHAR(2),
@sellStartDate  DATETIME,
@modifiedDate DATETIME,
@productSubCategoryID INT,
@productModelID INT,
@photoID INT,
@rowseffected INT OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE [Production].[Product]
	SET
	Name =@name,
	ProductNumber=@productNumber,
	MakeFlag =@makeFlag,
	Color =@color,
	StandardCost =@standardCost,
	ListPrice =@listPrice,
	Size =@size,
	Weight =@weight,
	Style =@size,
	SellStartDate =@sellStartDate,
	ModifiedDate =@modifiedDate,
	ProductSubCategoryID =@productSubCategoryID,
	ProductModelID =@productModelID,
	PhotoID =@photoID
	WHERE ProductID =@productID

	SELECT @rowseffected =@@ROWCOUNT
	
END
GO


