IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Question1]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Question1](
	[QuestionID] [bigint] IDENTITY(1,1) NOT NULL,
	[OriginalSentence] [varchar](max) NOT NULL,
	[QuestionSentence] [varchar](max) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[QuestionID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO