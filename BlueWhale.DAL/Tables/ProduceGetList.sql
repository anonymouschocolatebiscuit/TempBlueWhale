USE [BlueWhale_ERP]
GO

/****** Object:  Table [dbo].[produceGetList]    Script Date: 2025/1/11 16:34:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[produceGetList](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[shopId] [int] NULL,
	[number] [varchar](100) NULL,
	[deptId] [int] NULL,
	[planNumber] [varchar](100) NULL,
	[goodsId] [int] NULL,
	[num] [float] NULL,
	[makeId] [int] NULL,
	[makeDate] [datetime] NULL,
	[bizId] [int] NULL,
	[bizDate] [datetime] NULL,
	[checkId] [int] NULL,
	[checkDate] [datetime] NULL,
	[remarks] [varchar](1000) NULL,
	[flag] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO