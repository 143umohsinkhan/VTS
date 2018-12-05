IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Question3]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Question3](
	[QuestionID] [bigint] IDENTITY(1,1) NOT NULL,
	[QuestionText] [varchar](max) NOT NULL,
 CONSTRAINT [PK__Question3__1CF15040] PRIMARY KEY CLUSTERED 
(
	[QuestionID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO