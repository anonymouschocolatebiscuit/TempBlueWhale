USE [BlueWhale_ERP]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--drop view viewPurOrderList                         
create view [dbo].[viewPurOrderList]                         
as                          
select a.*,       
shop.names shopName,                              
v.Names wlName,                          
make.names makeName,                          
checks.names checkName,                          
biz.names bizName,                  
item.sumNum,                  
item.sumPriceNow,                  
item.sumPriceDis,                  
item.sumPriceTax,                  
item.sumPriceAll             
                       
from PurOrder a          
left join company shop on a.shopId=shop.id --Branch                         
left join vender v on a.wlId=v.id --Vender                 
left join users make on a.makeId=make.Id --Create By                  
left join users checks on a.checkId=checks.id --Review By                          
left join users biz on a.bizId=biz.id --Business Representative                            
left join           
(          
    select pId,            
    sum(num) sumNum,      
    sum(sumPriceNow) sumPriceNow,        
    sum(sumPriceDis) sumPriceDis,                    
    sum(sumPriceTax) sumPriceTax,            
    sum(sumPriceAll) sumPriceAll            
    from PurOrderItem              
    group by pId          
) item on a.id=item.pId --Entry Summary
GO