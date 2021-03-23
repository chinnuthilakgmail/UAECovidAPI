CREATE PROCEDURE [dbo].[uspAddCountry]
	@Country nvarchar(500),
	@Slug nvarchar(500),
	@Code  nvarchar(10),
	@CountryId INT OUTPUT
AS
BEGIN
DECLARE @Count INT = 0
SET @CountryId = 0
SELECT @Count = COUNT(*) FROM Country WHERE Country = @Country OR Slug = @Slug OR Code = @Code
IF @Count = 0
	BEGIN
		INSERT INTO Country (Country,Slug,Code) VALUES (@Country,@Slug,@Code)
		SET @CountryId = Scope_Identity()
		RETURN 1
	END
ELSE
	BEGIN
		SELECT  @CountryId = Id FROM Country WHERE Country = @Country OR Slug = @Slug OR Code = @Code
		RETURN 0
	END

END
