CREATE PROCEDURE [dbo].[uspDeleteCountry]
	@Id int 
AS
BEGIN
DECLARE @Count INT = 0 
SELECT @Count = COUNT(*) FROM Country WHERE Id = @Id
IF @Count > 0
	BEGIN
		DELETE Country  WHERE Id = @Id
		RETURN 1
	END
ELSE	 
	RETURN 0 
END
