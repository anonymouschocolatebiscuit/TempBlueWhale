USE [BlueWhale_ERP]
GO

/****** Object:  Table [dbo].[inventoryTransfer]    Script Date: 2025/01/09 10:17:27 ******/
/*
** Description :  Create Inventory Transfer Table
** Changes :  2024-01-09 dev100  222#457 - Create [InventoryTransfer] & [InventoryTransferDetails] Datatable, DAL
*/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[inventoryTransfer](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[shopId] [int] NULL,
	[number] [varchar](100) NULL,
	[bizDate] [datetime] NULL,
	[bizId] [int] NULL,
	[makeId] [int] NULL,
	[makeDate] [datetime] NULL,
	[checkId] [int] NULL,
	[checkDate] [datetime] NULL,
	[remarks] [varchar](1000) NULL,
	[flag] [varchar](100) NULL,
 CONSTRAINT [PK__inventoryTransfer__18EBB532] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[diaobo] ADD  CONSTRAINT [DF__inventoryTransfer__bizDate__19DFD96B]  DEFAULT (getdate()) FOR [bizDate]
GO
ALTER TABLE [dbo].[diaobo] ADD  CONSTRAINT [DF__inventoryTransfer__makeDate__1AD3FDA4]  DEFAULT (getdate()) FOR [makeDate]
GO
