
USE [BlueWhale_ERP]

GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[produceList](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[shopId] [int] NULL,
	[number] [varchar](100) NULL,
	[typeName] [varchar](100) NULL,
	[orderNumber] [varchar](100) NULL,
	[goodsId] [int] NULL,
	[num] [float] NULL,
	[remarks] [varchar](1000) NULL,
	[dateStart] [datetime] NULL,
	[dateEnd] [datetime] NULL,
	[finishDate] [datetime] NULL,
	[finishFlag] [varchar](100) NULL,
	[makeId] [int] NULL,
	[makeDate] [datetime] NULL,
	[checkId] [int] NULL,
	[checkDate] [datetime] NULL,
	[flag] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
