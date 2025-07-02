SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
      
      
--drop view viewGoodsNumStartView --------联查仓库、商品信息，为了合并出入库记录做库存查询    
create view [dbo].[viewGoodsNumStartView]      
as      
select a.*,    
'2000-1-1' bizDate,
g.shopId,       
ck.names ckName, 
g.names goodsName,    
g.code,    
g.unitName,  
g.spec,  
g.typeId,  
g.typeName    
from goodsNumStart a    
left join inventory ck on a.ckId=ck.id    
left join viewGoods g on a.goodsId=g.id
GO