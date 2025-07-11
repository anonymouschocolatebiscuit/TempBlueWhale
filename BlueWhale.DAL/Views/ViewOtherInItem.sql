USE [BlueWhale_ERP]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
       
CREATE VIEW [dbo].[viewOtherInItem]          
AS              
SELECT a.*,   
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
ck.names ckName          
FROM OtherInItem a          
LEFT JOIN viewgoods g on a.goodsId=g.id          
LEFT JOIN inventory ck on a.ckId=ck.id  
LEFT JOIN viewOtherIn b on a.pId=b.id  
GO


