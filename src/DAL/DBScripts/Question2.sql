IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Question2]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Question2](
	[QuestionID] [bigint] IDENTITY(1,1) NOT NULL,
	[QuestionText] [varchar](max) NOT NULL,
 CONSTRAINT [PK__Question2__03317E3D] PRIMARY KEY CLUSTERED 
(
	[QuestionID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO