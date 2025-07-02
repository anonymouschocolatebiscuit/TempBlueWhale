USE [BlueWhale_ERP]
GO

/****** Object:  View [dbo].[viewProduceProcessList]   Script Date: 2025/1/11 16:42:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--drop view viewProduceProcessList  
  
create view [dbo].[viewProduceProcessList]    
as    
select a.*,    
p.names processName,    
p.typeId,
p.typeName,    
p.unitName,    
make.names makeName,    
biz.names bizName,    
checks.names checkName,  
produce.number,  
produce.orderNumber,  
produce.wlName,  
produce.goodsName,  
produce.spec  
from ProduceProcessList a    
left join viewProcessList p on a.processId=p.id    
left join viewProduceList produce on a.pId=produce.id    
left join users make on a.makeId=make.id    
left join users biz on a.bizId=biz.id    
left join users checks on a.checkId=checks.id    
GO
