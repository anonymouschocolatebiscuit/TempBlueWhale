USE [BlueWhale_ERP]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[vender](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[shopId] [int] NULL,
	[code] [varchar](100) NULL,
	[names] [varchar](100) NULL,
	[typeId] [int] NULL,
	[dueDate] [datetime] NULL,
	[payNeed] [decimal](18, 2) NULL,
	[payReady] [decimal](18, 2) NULL,
	[tax] [int] NULL,
	[remarks] [varchar](1000) NULL,
	[makeId] [int] NULL,
	[makeDate] [datetime] NULL,
	[checkId] [int] NULL,
	[checkDate] [datetime] NULL,
	[taxNumber] [varchar](100) NULL,
	[bankName] [varchar](1000) NULL,
	[bankNumber] [varchar](1000) NULL,
	[address] [varchar](1000) NULL,
	[flag] [varchar](100) NULL,
 CONSTRAINT [PK__vender__1ED998B2] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[vender] ADD  CONSTRAINT [DF__vender__makeDate__1FCDBCEB]  DEFAULT (getdate()) FOR [makeDate]
GO


