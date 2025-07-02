USE [BlueWhale_ERP]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--drop view viewPurReceipt                    
create view [dbo].[viewPurReceipt]                     
as                      
select a.*,                           
shop.Names shopName,     
v.Names wlName,                      
make.names makeName,                      
checks.names checkName,                      
biz.names bizName,              
sumNum,              
sumPriceNow,              
sumPriceDis,              
sumPriceTax,              
sumPriceAll,        
isnull(priceCheckNowSum,0)+payNow  priceCheckNowSum               
                   
from PurReceipt  a                           
left join company shop on a.shopId=shop.id ---Branch    
left join vender v on a.wlId=v.id ---Vender                     
left join users make on a.makeId=make.Id --Created By                      
left join users checks on a.checkId=checks.id --Reviewed By                      
left join users biz on a.bizId=biz.id --Business Representative                           
left join viewPurReceiptItemSum item on a.id=item.pId --Entry Summary        
left join viewPayMentSourceBillPriceCheckNowSumSum p on a.number=p.sourceNumber --Summary of source documents for payment
GO