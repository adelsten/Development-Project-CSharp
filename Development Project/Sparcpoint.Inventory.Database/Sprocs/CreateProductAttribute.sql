CREATE PROCEDURE [dbo].[CreateProductAttribute]
	@InstanceId  int,
	@Key varchar(64),
	@Value varchar(512)
AS
	INSERT INTO [Instances].[ProductAttributes] (InstanceId, [Key],[Value])
	VALUES (@InstanceId, @Key, @Value)
