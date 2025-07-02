USE [BlueWhale_ERP]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[users](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[loginName] [varchar](100) NULL,
	[names] [varchar](100) NULL,
	[shopId] [int] NULL,
	[deptId] [int] NULL,
	[roleId] [int] NULL,
	[tel] [varchar](100) NULL,
	[phone] [varchar](100) NULL,
	[email] [varchar](100) NULL,
	[address] [varchar](1000) NULL,
	[brithDay] [datetime] NULL,
	[comeDate] [datetime] NULL,
	[pwd] [varchar](100) NULL,
	[pwds] [varchar](100) NULL,
	[makeDate] [datetime] NULL,
	[flag] [varchar](100) NULL,
	[openId] [varchar](1000) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO