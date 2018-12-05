IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Answer3]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Answer3](
	[AnswerID] [bigint] IDENTITY(1,1) NOT NULL,
	[QuestionID] [bigint] NOT NULL,
	[Answer] [varchar](max) NOT NULL,
 CONSTRAINT [PK__Answer3__1ED998B2] PRIMARY KEY CLUSTERED 
(
	[AnswerID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO