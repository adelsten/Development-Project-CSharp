CREATE PROCEDURE [dbo].[CreateProduct]
	@Name varchar(256),
	@Description varchar(256),
	@ProductImageUris varchar(MAX),
	@ValidSkus varchar(MAX)
AS
	INSERT INTO [Instances].[Products] (Name, Description, ProductImageUris, ValidSkus)
    OUTPUT INSERTED.InstanceId 
	VALUES (@Name, @Description,@ProductImageUris, @ValidSkus)
