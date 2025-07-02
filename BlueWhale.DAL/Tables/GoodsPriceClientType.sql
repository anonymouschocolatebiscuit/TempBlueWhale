USE [BlueWhale_ERP]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[goodsPriceClientType](
	[goodsId] [int] NULL,
	[typeId] [int] NULL,
	[price] [decimal](18, 2) NULL
) ON [PRIMARY]
GO