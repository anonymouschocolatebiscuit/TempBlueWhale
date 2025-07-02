USE [BlueWhale_ERP]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Company](
	[Id] [int] IDENTITY(80001,1) NOT NULL,
	[names] [varchar](1000) NULL,
	[typeName] [varchar](100) NULL,
	[dateStart] [datetime] NULL,
	[dateEnd] [datetime] NULL,
	[phone] [varchar](100) NULL,
	[proId] [int] NULL,
	[ctId] [int] NULL,
	[makeId] [int] NULL,
	[makeDate] [datetime] NULL,
	[checkId] [int] NULL,
	[checkDate] [datetime] NULL,
	[remarks] [varchar](500) NULL,
	[flag] [varchar](100) NULL
) ON [PRIMARY]
GO