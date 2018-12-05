CREATE TABLE [dbo].[UserTestAnswer]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserID] BIGINT NOT NULL, 
    [TestID] INT NOT NULL, 
    [QuestionID] INT NOT NULL, 
	UserTestID INT NOT NULL,
    [QuestionType] VARCHAR(MAX) NOT NULL ,
	[AnswerID] INT NULL, 
    [AnswerText] VARCHAR(MAX) NULL, 
    [IsCorrect] BIT NOT NULL, 
    [IsAttempt] BIT NOT NULL DEFAULT 0, 
    [Point] INT NOT NULL DEFAULT 0, 
    Foreign Key(UserID) References VTSUSER (UserID),
	Foreign Key(TestID) References TestPaper (Id),
	Foreign Key(UserTestID) References UserTest (Id)
)
