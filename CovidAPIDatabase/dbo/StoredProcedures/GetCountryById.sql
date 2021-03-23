CREATE PROCEDURE [dbo].[GetCountryById]
	@Id int  
AS
	SELECT Id,Country,Slug,Code FROM Country WHERE Id = @Id
RETURN 1
