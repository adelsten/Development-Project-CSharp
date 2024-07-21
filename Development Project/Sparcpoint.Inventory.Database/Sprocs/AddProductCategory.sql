CREATE PROCEDURE [dbo].[AddProductCategory]
	@InstanceId int, 
	@CategoryInstanceId int
AS
	INSERT INTO [Instances].[ProductCategories] (InstanceId, CategoryInstanceId)
	VALUES (@InstanceId, @CategoryInstanceId)
