USE [BlueWhale_ERP]
GO

/****** Object:  Table [dbo].[CheckBillItemPayMent]    Script Date: 2025/1/11 11:07:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CheckBillItemPayMent](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[pId] [int] NULL,
	[sourceNumber] [varchar](100) NULL,
	[priceCheckNow] [decimal](18, 2) NULL,
	[flag] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
