SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create view [dbo].[viewGoodsType]   
as        
select a.*,        
p.Names parentName        
from goodsType a        
left join goodsType p on a.parentId=p.id
GO