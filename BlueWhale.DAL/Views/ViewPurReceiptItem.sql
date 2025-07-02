USE [BlueWhale_ERP]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- drop view viewPurReceiptItem                
create  view [dbo].[viewPurReceiptItem]                 
as                    
select a.*, 
b.shopId,           
b.number,            
b.bizDate,            
b.flag,              
b.wlName,           
b.types,               
g.names goodsName,                  
g.unitName,                  
g.code code,                
g.spec,            
g.typeId,          
g.typeName,                
i.names ckName,        
types*num nums                    
from PurReceiptItem a                    
left join viewgoods g on a.goodsId=g.id                    
left join inventory i on a.ckId=ck.id            
left join viewPurReceipt b on a.pId=b.id
GO

