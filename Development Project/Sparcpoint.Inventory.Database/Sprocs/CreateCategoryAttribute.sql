CREATE PROCEDURE [dbo].[CreateCategoryAttribute]
	@InstanceId  int,
	@Key varchar(64),
	@Value varchar(512)
AS
	INSERT INTO [Instances].[CategoryAttributes] ([InstanceId],[Key],[Value])
	VALUES (@InstanceId, @Key, @Value)

