IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Answer2]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Answer2](
	[AnswerID] [bigint] NOT NULL,
	[QuestionID] [bigint] NOT NULL,
	[Answer] [varchar](max) NOT NULL,
 CONSTRAINT [PK__Answer2__07F6335A] PRIMARY KEY CLUSTERED 
(
	[AnswerID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO