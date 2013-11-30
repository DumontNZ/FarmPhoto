USE [FarmPhoto]
GO

/****** Object:  Table [dbo].[Rating]    Script Date: 27/10/2013 8:09:08 p.m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Rating](
	[RatingId] [int] identity(1,1) primary key,
	[Score] [float] NOT NULL,
	[PhotoId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[CreatedOnDateUTC] [datetime] NOT NULL,
	[DeletedOnDateUTC] [datetime] NULL
) ON [PRIMARY]

GO

