USE [BlueWhale_ERP]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[produceProcessList](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[shopId] [int] NULL,
	[pId] [int] NULL,
	[processId] [int] NULL,
	[num] [float] NULL,
	[price] [float] NULL,
	[sumPrice] [float] NULL,
	[remarks] [varchar](1000) NULL,
	[makeId] [int] NULL,
	[makeDate] [datetime] NULL,
	[bizId] [int] NULL,
	[bizDate] [datetime] NULL,
	[checkId] [int] NULL,
	[checkDate] [datetime] NULL,
	[flag] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO