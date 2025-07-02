SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--drop view viewGoodsOpen             
create view [dbo].[viewGoodsOpen]             
as              
select a.*,   
shop.names shopName,                           
make.names makeName,              
checks.names checkName,              
biz.names bizName,    
types,    
goodsId,  
item.code,  
goodsName,  
spec,  
unitName,  
item.remarks remarksItem,  
ckId,  
ckName,  
num,   
price,     
sumPrice     
  
from GoodsOpen   a     
left join shop shop on a.shopId=shop.id --shop                           
left join users make on a.makeId=make.Id --creator              
left join users checks on a.checkId=checks.id --reviewer              
left join users biz on a.bizId=biz.id                   
left join viewGoodsOpenItem item on a.id=item.pId  
where item.types=-1
GO