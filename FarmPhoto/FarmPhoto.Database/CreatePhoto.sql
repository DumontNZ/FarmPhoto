USE [FarmPhoto]
GO

/****** Object:  Table [dbo].[Photo]    Script Date: 27/10/2013 8:09:21 p.m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Photo](
	[PhotoId] [int] identity(1,1) primary key,
	[Title] [varchar](255) NOT NULL,
	[Description] [varchar](255) NOT NULL,
	[Approved] [bit] NOT NULL,
	[Width] [int] NOT NULL,
	[Height] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[FileName] [varchar](255) NOT NULL,
	[CreatedOnDateUTC] [datetime] NOT NULL,
	[DeletedOnDateUTC] [datetime] NULL,
	[ApprovedOnDateUTC] [datetime] NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

