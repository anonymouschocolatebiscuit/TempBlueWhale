USE [BlueWhale_ERP]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[purOrderItem](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[pId] [int] NULL,
	[goodsId] [int] NULL,
	[num] [decimal](18, 3) NULL,
	[price] [decimal](18, 4) NULL,
	[dis] [decimal](18, 2) NULL,
	[sumPriceDis] [decimal](18, 2) NULL,
	[priceNow] [decimal](18, 4) NULL,
	[sumPriceNow] [decimal](18, 3) NULL,
	[tax] [int] NULL,
	[priceTax] [decimal](18, 4) NULL,
	[sumPriceTax] [decimal](18, 3) NULL,
	[sumPriceAll] [decimal](18, 3) NULL,
	[ckId] [int] NULL,
	[remarks] [varchar](1000) NULL,
	[ItemId] [int] NULL,
	[sourceNumber] [varchar](100) NULL,
 CONSTRAINT [PK__purOrderItem__48CFD27E] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO