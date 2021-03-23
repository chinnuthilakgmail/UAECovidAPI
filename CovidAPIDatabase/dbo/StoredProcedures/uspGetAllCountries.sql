CREATE PROCEDURE [dbo].[uspGetAllCountries]
	 
AS
	SELECT Id,Country,Slug,Code FROM Country 
RETURN 1
