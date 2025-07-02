USE [BlueWhale_ERP]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
                    
create view [dbo].[viewProduceIn]                           
as                              
select a.*,                                                                    
make.names makeName,                              
checks.names checkName,                              
biz.names bizName,                      
sumNum,                      
isnull(sumPriceAll,0) sumPriceItem, 
sumPriceNow,                     
sumPriceDis,                      
sumPriceTax,                      
sumPriceAll                    
                           
from produceIn  a                                                                  
left join users make on a.makeId=make.Id                            
left join users checks on a.checkId=checks.id                               
left join users biz on a.bizId=biz.id    
left join viewProduceInItemSum item on a.id=item.pId          
GO