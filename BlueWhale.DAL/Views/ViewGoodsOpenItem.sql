SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- drop view viewGoodsOpenItem             
create  view [dbo].[viewGoodsOpenItem]            
as            
select a.*,          
g.names goodsName,          
g.unitName,          
g.code code,         
g.spec,      
g.typeId,    
g.typeName,         
ck.names ckName,    
a.types*a.num nums,     
b.shopId,
b.bizDate,    
b.number,    
b.flag            
from GoodsOpenItem a            
left join viewGoods g on a.goodsId=g.id            
left join inventory ck on a.ckId=ck.id    
left join GoodsOpen b on a.pId=b.id
GO