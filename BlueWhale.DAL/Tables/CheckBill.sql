USE [BlueWhale_ERP]
GO

/****** Object:  Table [dbo].[CheckBill]    Script Date: 2025/1/11 11:07:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CheckBill](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[shopId] [int] NULL,
	[number] [varchar](100) NULL,
	[clientIdA] [int] NULL,
	[venderIdA] [int] NULL,
	[clientIdB] [int] NULL,
	[venderIdB] [int] NULL,
	[bizType] [int] NULL,
	[bizDate] [datetime] NULL,
	[makeId] [int] NULL,
	[makeDate] [datetime] NULL,
	[checkId] [int] NULL,
	[checkDate] [datetime] NULL,
	[checkPrice] [decimal](18, 2) NULL,
	[flag] [varchar](100) NULL,
	[remarks] [varchar](1000) NULL,
 CONSTRAINT [PK__CheckBill__2E3BD7D3] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[CheckBill] ADD  CONSTRAINT [DF__CheckBill__makeD__2F2FFC0C]  DEFAULT (getdate()) FOR [makeDate]
GO

ALTER TABLE [dbo].[CheckBill] ADD  CONSTRAINT [DF__CheckBill__flag__30242045]  DEFAULT ('±£´æ') FOR [flag]
GO

