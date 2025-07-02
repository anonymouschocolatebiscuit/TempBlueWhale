USE [BlueWhale_ERP]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ReceivableAccountItem](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[pId] [int] NULL,
	[bkId] [int] NULL,
	[payPrice] [decimal](18, 2) NULL,
	[payTypeId] [int] NULL,
	[payNumber] [varchar](100) NULL,
	[remarks] [varchar](1000) NULL,
	[flag] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO