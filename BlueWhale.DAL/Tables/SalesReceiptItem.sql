USE [BlueWhale_ERP]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[salesReceiptItem](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[pId] [int] NULL,
	[goodsId] [int] NULL,
	[num] [decimal](18, 0) NULL,
	[price] [decimal](18, 2) NULL,
	[dis] [decimal](18, 2) NULL,
	[sumPriceDis] [decimal](18, 2) NULL,
	[priceNow] [decimal](18, 2) NULL,
	[sumPriceNow] [decimal](18, 2) NULL,
	[tax] [int] NULL,
	[priceTax] [decimal](18, 2) NULL,
	[sumPriceTax] [decimal](18, 2) NULL,
	[sumPriceAll] [decimal](18, 2) NULL,
	[ckId] [int] NULL,
	[remarks] [varchar](1000) NULL,
	[ItemId] [int] NULL,
	[sourceNumber] [varchar](100) NULL,
 CONSTRAINT [PK__salesReceiptItem__04E4BC85] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


