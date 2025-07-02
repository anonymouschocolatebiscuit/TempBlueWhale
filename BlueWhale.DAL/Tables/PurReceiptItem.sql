CREATE TABLE [dbo].[purReceiptItem](
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
	[itemId] [int] NULL,
	[sourceNumber] [varchar](100) NULL,
 CONSTRAINT [PK__purReceiptItem__656C112C] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO