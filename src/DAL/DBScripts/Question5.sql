IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Question5]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Question5](
	[QuestionID] [bigint] IDENTITY(1,1) NOT NULL,
	[QuestionText] [varchar](max) NOT NULL,
 CONSTRAINT [PK__Question5__2E1BDC42] PRIMARY KEY CLUSTERED 
(
	[QuestionID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO