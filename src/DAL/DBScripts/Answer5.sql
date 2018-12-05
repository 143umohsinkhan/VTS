IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Answer5]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Answer5](
	[AnswerID] [bigint] IDENTITY(1,1) NOT NULL,
	[QuestionID] [bigint] NOT NULL,
	[Answer] [varchar](max) NOT NULL,
 CONSTRAINT [PK__Answer5__300424B4] PRIMARY KEY CLUSTERED 
(
	[AnswerID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO