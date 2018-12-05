CREATE TABLE [dbo].[TestQuestion]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [QuestionID] INT NOT NULL, 
    [QuetionType] VARCHAR(MAX) NOT NULL, 
    [TestPaperID] INT NOT NULL
	Foreign key (TestPaperID) References TestPaper(Id)
)
