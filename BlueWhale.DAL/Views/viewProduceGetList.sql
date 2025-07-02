USE [BlueWhale_ERP]
GO

/****** Object:  View [dbo].[viewProduceGetList]    Script Date: 2025/1/11 16:42:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--drop view viewProduceGetList  

create view [dbo].[viewProduceGetList]    
as    
select a.*, make.names makeName, biz.names bizName,
checks.names checkName,    
isnull(s.sumNum,0) sumNum,--数量  
isnull(s.sumPrice,0) sumPrice,--金额
g.names goodsName, g.spec, g.code, g.unitName 
from ProduceGetList  a    
left join users make on a.makeId=make.id    
left join users biz on a.bizId=biz.id    
left join users checks on a.checkId=checks.id    
left join viewGoods g on a.goodsId=g.id    
left join viewProduceGetListItemSum s on a.Id=s.pId
GO