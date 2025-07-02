USE [BlueWhale_ERP]
Go

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
                    
create  view [dbo].[viewSalesReceiptItem]                       
as                          
select 
	a.*,  
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
	g.brandName,    
	g.typeId,              
	g.typeName,                        
	ck.names ckName,            
	types*num nums                          
from SalesReceiptItem a                          
left join viewgoods g on a.goodsId=g.id                          
left join cangku ck on a.ckId=ck.id                
left join viewSalesReceipt b on a.pId=b.id
GO