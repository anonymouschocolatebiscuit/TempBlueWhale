USE [BlueWhale_ERP]
GO

/****** Object:  Table [dbo].[produceGetListItem]    Script Date: 2025/1/11 16:34:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[produceGetListItem](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[pId] [int] NULL,
	[goodsId] [int] NULL,
	[ckId] [int] NULL,
	[pihao] [varchar](100) NULL,
	[numApply] [float] NULL,
	[num] [float] NULL,
	[price] [float] NULL,
	[sumPrice] [float] NULL,
	[remarks] [varchar](1000) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


