USE [BlueWhale_ERP]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[produceInItem](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[pId] [int] NULL,
	[goodsId] [int] NULL,
	[num] [decimal](18, 2) NULL,
	[price] [decimal](18, 4) NULL,
	[dis] [decimal](18, 2) NULL,
	[sumPriceDis] [decimal](18, 2) NULL,
	[priceNow] [decimal](18, 4) NULL,
	[sumPriceNow] [decimal](18, 2) NULL,
	[tax] [int] NULL,
	[priceTax] [decimal](18, 4) NULL,
	[sumPriceTax] [decimal](18, 2) NULL,
	[sumPriceAll] [decimal](18, 2) NULL,
	[ckId] [int] NULL,
	[remarks] [varchar](1000) NULL,
	[ItemId] [int] NULL,
	[sourceNumber] [varchar](100) NULL
) ON [PRIMARY]