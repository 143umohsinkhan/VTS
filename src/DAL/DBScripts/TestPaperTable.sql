CREATE TABLE [dbo].[TestPaper]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] VARCHAR(MAX) NOT NULL, 
    [IsActive] BIT NOT NULL, 
    [CreatedOn] DATETIME NOT NULL DEFAULT Getdate()
)
