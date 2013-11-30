USE [FarmPhoto]
GO

/****** Object:  Table [dbo].[Users]    Script Date: 27/10/2013 8:08:02 p.m. ******/
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
	[UserId] [int] identity(1,1) primary key
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

