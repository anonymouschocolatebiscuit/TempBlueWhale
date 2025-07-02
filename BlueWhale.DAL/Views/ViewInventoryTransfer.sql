
USE [BlueWhale_ERP]
GO

/****** Object:  View [dbo].[viewInventoryTransfer]    Script Date: 2025/01/11 13:07:23 ******/
/*
** Description £º Create Inventory Transfer View
** Changes £º 2024-01-09 dev100  222#457 - Create [InventoryTransfer] & [InventoryTransferDetails] Datatable, DAL
*/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
DROP VIEW IF EXISTS viewInventoryTransfer 
GO
create view [dbo].[viewInventoryTransfer]   
as    
select a.*,  
shop.names shopName, 
b.code,    
spec,    
goodsName,    
num,    
unitName,    
ckNameIn,    
ckNameOut,    
mk.names makeName,    
ck.names checkName    
from inventoryTransfer a    
left join shop shop on a.shopId=shop.Id
left join inventoryTransferDetails b on a.Id=b.pId    
left join users mk on a.makeId=mk.Id    
left join users ck on a.checkId=ck.Id
GO