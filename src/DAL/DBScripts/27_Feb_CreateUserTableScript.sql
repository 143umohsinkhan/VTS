CREATE TABLE [dbo].[VTSUSER] (
    [UserID]             BIGINT         IDENTITY (1, 1) NOT NULL,
    [Email]              VARCHAR (MAX)  NOT NULL,
    [FirstName]          VARCHAR (MAX)  NOT NULL,
    [LastName]           VARCHAR (MAX)  NOT NULL,
    [Sex]                BIT            NOT NULL,
    [IsActive]           BIT            NOT NULL DEFAULT 0,
    [Password]           VARCHAR (MAX)  NOT NULL,
    [LastUpdatePassword] DATETIME       NULL,
    [DOB]                DATETIME       NOT NULL,
    [City]               VARCHAR (100)  NULL,
    [State]              VARCHAR (100)  NULL,
    [Address]            VARCHAR (MAX)  NULL,
    [Country]            VARCHAR (100)  NULL,
    [Pincode]            VARCHAR (100)  NULL,
    [Phone]              VARCHAR (100)  NULL,
    [Photo]              VARCHAR (5000) NULL,
    [IsAdmin]            BIT            NOT NULL DEFAULT 0,
    [CreatedOn]          DATETIME       NULL DEFAULT GetDate(),
    PRIMARY KEY CLUSTERED ([UserID] ASC)
);

