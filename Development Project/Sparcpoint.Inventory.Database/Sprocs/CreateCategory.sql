CREATE PROCEDURE [dbo].[CreateCategory]
	@Name varchar(64),
	@Description varchar(256)
AS
	INSERT INTO [Instances].Categories ([Name],[Description])
    OUTPUT INSERTED.InstanceId 
	VALUES(@Name, @Description)
