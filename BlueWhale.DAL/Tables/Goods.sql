USE [BlueWhale_ERP]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[goods](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[shopId] [int] NULL,
	[code] [varchar](100) NULL,
	[barcode] [varchar](100) NULL,
	[names] [varchar](100) NULL,
	[typeId] [int] NULL,
	[brandId] [int] NULL,
	[spec] [varchar](100) NULL,
	[unitId] [int] NULL,
	[ckId] [int] NULL,
	[place] [varchar](1000) NULL,
	[priceCost] [float] NULL,
	[priceSalesWhole] [float] NULL,
	[priceSalesRetail] [float] NULL,
	[numMin] [int] NULL,
	[numMax] [int] NULL,
	[bzDays] [int] NULL,
	[isWeight] [int] NULL,
	[tichengRate] [float] NULL,
	[remarks] [text] NULL,
	[makeDate] [datetime] NULL,
	[imagePath] [varchar](100) NULL,
	[flag] [varchar](100) NULL,
	[isShow] [int] NULL,
	[showType] [varchar](100) NULL,
	[fieldA] [text] NULL,
	[fieldB] [text] NULL,
	[fieldC] [text] NULL,
	[fieldD] [text] NULL,
CONSTRAINT [PK__goods__3D5E1FD2] PRIMARY KEY CLUSTERED 
(
	[id] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO