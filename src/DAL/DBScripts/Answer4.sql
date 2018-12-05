IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Answer4]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Answer4](
	[AnswerID] [bigint] IDENTITY(1,1) NOT NULL,
	[QuestionID] [bigint] NOT NULL,
	[Answer] [varchar](max) NOT NULL,
 CONSTRAINT [PK__Answer4__2B3F6F97] PRIMARY KEY CLUSTERED 
(
	[AnswerID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO