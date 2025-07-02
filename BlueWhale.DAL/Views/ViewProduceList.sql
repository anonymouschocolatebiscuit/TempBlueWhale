GO
/****** Object:  View [dbo].[viewProduceList]    Script Date: 2025/1/11 15:49:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--Production plan view, associated query of production process and production completion quantity 
--drop view viewProduceList  
create view [dbo].[viewProduceList]    
as    
select a.*,    
make.names makeName,    
checks.names checkName,    
isnull(process.countNum,0) processNum,
isnull(finish.sumNum,0) finishNum,
num-isnull(finish.sumNum,0) finishNumNo,
g.names goodsName,    
g.spec,    
g.code,    
g.unitName,    
o.wlId,    
o.wlName,
o.sendDate    
from ProduceList a    
left join users make on a.makeId=make.id    
left join users checks on a.checkId=checks.id    
left join viewGoods g on a.goodsId=g.id    
left join viewSalesOrder o on a.orderNumber=o.number and a.shopId=o.shopId  
left join viewProduceListItemCount process on a.id=process.pId  
left join viewProduceInItemSumByGoodsId finish on a.goodsId=finish.goodsId and a.number=finish.sourceNumber
