
USE [BlueWhale_ERP]
GO

/****** Object:  View [dbo].[viewInventoryTransferDetails]    Script Date: 2025/01/11 14:34:12 ******/
/*
** Description ： Create Inventory Transfer Details View
** Changes ： 2024-01-09 dev100  222#457 - Create [InventoryTransfer] & [InventoryTransferDetails] Datatable, DAL
*/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
DROP VIEW IF EXISTS viewInventoryTransferDetails 
GO
create view [dbo].[viewInventoryTransferDetails]  
as  
select a.*,  
p.shopId,
p.number,  
p.bizDate,  
p.flag,  
g.code,  
g.names goodsName,  
g.typeId,  
g.typeName,  
spec,  
unitName,  
ckIn.names ckNameIn,  
ckOut.names ckNameOut  
from inventoryTransferDetails a  
left join viewGoods g on a.goodsId=g.id  
left join cangku ckIn on a.ckIdIn=ckIn.id  
left join cangku ckOut on a.ckIdOut=ckOut.id  
left join inventoryTransfer p on a.pId=p.id
GO