USE [FarmPhoto]
GO

/****** Object:  Table [dbo].[Users]    Script Date: 15/12/2013 9:55:00 p.m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Users](
	[FirstName] [varchar](255) NOT NULL,
	[Surname] [varchar](255) NOT NULL,
	[UserName] [varchar](255) NOT NULL,
	[Email] [varchar](255) NOT NULL,
	[Password] [varchar](255) NOT NULL,
	[PasswordSalt] [varchar](255) NOT NULL,
	[CreatedOnDateUTC] [datetime] NOT NULL,
	[DeletedOnDateUTC] [datetime] NULL,
	[DisplayName] [varchar](255) NOT NULL,
	[Country] [varchar](255) NULL,
	[TokenExpiry] [datetime] null,
	[Token] [varchar] (255) null, 
	[UserId] [int] IDENTITY(1,1) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


