﻿SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GoodsOpenItem](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[pId] [int] NULL,
	[types] [int] NULL,
	[goodsId] [int] NULL,
	[num] [decimal](18, 2) NULL,
	[price] [decimal](18, 5) NULL,
	[sumPrice] [decimal](18, 2) NULL,
	[ckId] [int] NULL,
	[remarks] [varchar](1000) NULL,
 CONSTRAINT [PK__GoodsOpenItem__48CFD27E] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO