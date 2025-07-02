SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[goodsPriceClient](
	[goodsId] [int] NULL,
	[clientId] [int] NULL,
	[price] [decimal](18, 2) NULL
) ON [PRIMARY]

GO