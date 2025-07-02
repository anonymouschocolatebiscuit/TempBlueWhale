USE [BlueWhale_ERP]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[clientAddress](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[openId] [varchar](1000) NULL,
	[clientId] [int] NULL,
	[proId] [int] NULL,
	[ctId] [int] NULL,
	[areaId] [int] NULL,
	[address] [varchar](1000) NULL,
	[postCode] [varchar](1000) NULL,
	[names] [varchar](1000) NULL,
	[phone] [varchar](1000) NULL,
	[tel] [varchar](1000) NULL,
	[default] [int] NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO