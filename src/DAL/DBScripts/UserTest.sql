CREATE TABLE [dbo].[UserTest] (
    [Id]         INT    NOT NULL IDENTITY,
    [UserID]     BIGINT NOT NULL,
    [TestID]     INT    NOT NULL,
    [TotalScore] BIGINT NULL,
    [CreatedOn]  DATE   DEFAULT (getdate()) NULL,
	TestStatus Bit Default 0
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([UserID]) REFERENCES [dbo].[VTSUSER] ([UserID]),
    FOREIGN KEY ([TestID]) REFERENCES [dbo].[TestPaper] ([Id])
);

