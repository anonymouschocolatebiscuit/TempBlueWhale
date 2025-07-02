USE [BlueWhale_ERP]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[salesReceipt](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[shopId] [int] NULL,
	[number] [varchar](100) NULL,
	[wlId] [int] NULL,
	[bizDate] [datetime] NULL,
	[types] [int] NULL,
	[remarks] [varchar](1000) NULL,
	[dis] [decimal](18, 2) NULL,
	[disPrice] [decimal](18, 2) NULL,
	[sumPrice] [decimal](18, 2) NULL,
	[payNow] [decimal](18, 2) NULL,
	[payNowNo] [decimal](18, 2) NULL,
	[bkId] [int] NULL,
	[makeId] [int] NULL,
	[makeDate] [datetime] NULL,
	[bizId] [int] NULL,
	[checkId] [int] NULL,
	[checkDate] [datetime] NULL,
	[sendId] [int] NULL,
	[sendNumber] [varchar](100) NULL,
	[sendPayType] [varchar](100) NULL,
	[sendPrice] [decimal](18, 2) NULL,
	[getName] [varchar](100) NULL,
	[phone] [varchar](100) NULL,
	[address] [varchar](1000) NULL,
	[flag] [varchar](100) NULL,
 CONSTRAINT [PK__salesReceipt__02084FDA] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[salesReceipt] ADD  CONSTRAINT [DF__salesRece__bizDa__02FC7413]  DEFAULT (getdate()) FOR [bizDate]
GO


