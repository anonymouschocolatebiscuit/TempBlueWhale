USE [BlueWhale_ERP]
GO

/****** Object:  Table [dbo].[inventoryTransferDetails]    Script Date: 2025/01/09 11:32:43 ******/
/*
** Description :  Create Inventory Transfer Details Table
** Changes :  2024-01-09 dev100  222#457 - Create [InventoryTransfer] & [InventoryTransferDetails] Datatable, DAL
*/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[inventoryTransferDetails](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[pId] [int] NULL,
	[goodsId] [int] NULL,
	[num] [decimal](18, 0) NULL,
	[ckIdOut] [int] NULL,
	[ckIdIn] [int] NULL,
	[remarks] [varchar](1000) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
