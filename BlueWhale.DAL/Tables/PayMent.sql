USE [BlueWhale_ERP]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[payMent](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[shopId] [int] NULL,
	[number] [varchar](100) NULL,
	[wlId] [int] NULL,
	[bizDate] [datetime] NULL,
	[makeId] [int] NULL,
	[makeDate] [datetime] NULL,
	[checkId] [int] NULL,
	[checkDate] [datetime] NULL,
	[disPrice] [decimal](18, 2) NULL,
	[payPriceNowMore] [decimal](18, 2) NULL,
	[flag] [varchar](100) NULL,
	[remarks] [varchar](1000) NULL,
	[orderNumber] [varchar](100) NULL,
 CONSTRAINT [PK__payMent__382F5661] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[payMent] ADD  CONSTRAINT [DF__payMent__makeDat__39237A9A]  DEFAULT (getdate()) FOR [makeDate]
GO
ALTER TABLE [dbo].[payMent] ADD  CONSTRAINT [DF__payMent__flag__3A179ED3]  DEFAULT ('保存') FOR [flag]
GO