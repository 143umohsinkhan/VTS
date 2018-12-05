IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Answer1]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Answer1](
	[AnswerID] [bigint] IDENTITY(1,1) NOT NULL,
	[QuestionID] [bigint] NOT NULL,
	[AnswerText] [varchar](max) NOT NULL,
 CONSTRAINT [PK__Answer1__0519C6AF] PRIMARY KEY CLUSTERED 
(
	[AnswerID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO