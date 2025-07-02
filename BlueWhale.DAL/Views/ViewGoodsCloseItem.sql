SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- drop view viewGoodsCloseItem           
create  view [dbo].[viewGoodsCloseItem]          
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
from GoodsCloseItem a          
left join viewgoods g on a.goodsId=g.id          
left join inventory ck on a.ckId=ck.id  
left join GoodsClose b on a.pId=b.id
GO