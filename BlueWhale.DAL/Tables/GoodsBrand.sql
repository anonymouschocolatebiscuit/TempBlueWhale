USE [BlueWhale_ERP]
GO

/****** Object:  Table [dbo].[goodsBrand]    Script Date: 2025-3-13 14:43:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[GoodsBrand](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[shopId] [int] NULL,
	[names] [varchar](100) NULL,
	[flag] [int] NULL,
 CONSTRAINT [PK__GoodsBrand__3EC74557] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


