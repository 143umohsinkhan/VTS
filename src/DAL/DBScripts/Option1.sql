IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Option1]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Option1](
	[OptionID] [bigint] IDENTITY(1,1) NOT NULL,
	[QuestionID] [bigint] NOT NULL,
	[OptionText] [varchar](max) NOT NULL,
 CONSTRAINT [PK__Option1__0AD2A005] PRIMARY KEY CLUSTERED 
(
	[OptionID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO