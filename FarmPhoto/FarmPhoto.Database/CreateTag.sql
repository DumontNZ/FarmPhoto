USE [FarmPhoto]
GO

/****** Object:  Table [dbo].[Tag]    Script Date: 27/10/2013 8:08:37 p.m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Tag](
	[TagId] [int] identity(1,1) primary key,
	[Description] [varchar](255) NOT NULL,
	[PhotoId] [int] NOT NULL,
	[CreatedOnDateUTC] [datetime] NOT NULL,
	[DeletedOnDateUTC] [datetime] NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

