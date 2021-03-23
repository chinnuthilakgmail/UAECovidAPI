CREATE PROCEDURE [dbo].[uspUpdateCountry]
	@Id int,
	@Country nvarchar(500),
	@Slug nvarchar(500),
	@Code  nvarchar(10)
AS
BEGIN
DECLARE @Count INT = 0 
SELECT @Count = COUNT(*) FROM Country WHERE Id = @Id
IF @Count > 0
	BEGIN
		UPDATE Country SET Country=@Country,Slug=@Slug, Code = @Code WHERE Id = @Id
		RETURN 1
	END
ELSE	 
	RETURN 0 
END

