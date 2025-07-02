USE [BlueWhale_ERP]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[client](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[shopId] [int] NULL,
	[code] [varchar](100) NULL,
	[names] [varchar](100) NULL,
	[typeId] [int] NULL,
	[yueDate] [datetime] NULL,
	[payNeed] [decimal](18, 2) NULL,
	[payReady] [decimal](18, 2) NULL,
	[tax] [int] NULL,
	[remarks] [varchar](1000) NULL,
	[makeId] [int] NULL,
	[makeDate] [datetime] NULL,
	[checkId] [int] NULL,
	[checkDate] [datetime] NULL,
	[loginName] [varchar](100) NULL,
	[pwd] [varchar](100) NULL,
	[pwds] [varchar](100) NULL,
	[taxNumber] [varchar](100) NULL,
	[bankName] [varchar](100) NULL,
	[bankNumber] [varchar](100) NULL,
	[address] [varchar](1000) NULL,
	[flag] [varchar](100) NULL,
	[openId] [varchar](1000) NULL,
	[nickname] [varchar](100) NULL,
	[province] [varchar](100) NULL,
	[city] [varchar](100) NULL,
	[country] [varchar](100) NULL,
	[headimgurl] [varchar](1000) NULL
) ON [PRIMARY]
GO