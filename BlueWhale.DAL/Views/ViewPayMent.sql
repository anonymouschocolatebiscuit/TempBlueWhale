SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
           
create view [dbo].[viewPayMent]            
as                  
select a.*,                       
v.Names wlName,                  
make.names makeName,                  
checks.names checkName,                         
payPriceSum,      
priceCheckNowSum           
from PayMent  a                       
left join vender v on a.wlId=v.id        
left join users make on a.makeId=make.Id                
left join users checks on a.checkId=checks.id                                    
left join viewPayMentAccountItemSum item on a.id=item.pId
left join viewPayMentSourceBillItemSum itemBill on a.id=itemBill.pId
GO