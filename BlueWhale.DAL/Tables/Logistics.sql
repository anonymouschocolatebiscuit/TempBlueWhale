USE [BlueWhale_ERP]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[logistics](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[shopId] [int] NULL,
	[code] [varchar](100) NULL,
	[names] [varchar](100) NULL,
	[linkMan] [varchar](100) NULL,
	[tel] [varchar](100) NULL,
	[phone] [varchar](100) NULL,
	[fax] [varchar](100) NULL,
	[address] [varchar](1000) NULL,
	[mall] [varchar](100) NULL,
	[printModel] [varchar](100) NULL,
	[makeId] [int] NULL,
	[makeDate] [datetime] NULL,
 CONSTRAINT [PK__logistics__658C0CBD] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[logistics] ADD  CONSTRAINT [DF__logistics__makeDate__668030F6]  DEFAULT (getdate()) FOR [makeDate]
GO


