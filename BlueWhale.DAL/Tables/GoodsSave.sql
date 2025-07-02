SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[goodsSave](
	[clientId] [int] NULL,
	[openId] [varchar](1000) NULL,
	[goodsId] [int] NULL,
	[makeDate] [datetime] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO