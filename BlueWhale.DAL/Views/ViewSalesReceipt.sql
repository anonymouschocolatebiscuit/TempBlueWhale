USE [BlueWhale_ERP]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO                       
CREATE VIEW [dbo].[viewSalesReceipt]                         
as                          
select a.*,        
        shop.Names shopName,                              
        v.Names wlName,                          
        make.names makeName,                          
        checks.names checkName,                          
        biz.names bizName,     
        wl.names sendCompany,                 
        sumNum,                  
        isnull(item.sumPriceAll,0) sumPriceItem,                  
        sumPriceDis,                  
        sumPriceTax,                  
        sumPriceAll,          
isnull(priceCheckNowSum,0)+payNow priceCheckNowSum                 
                       
from SalesReceipt a                     
left join company shop on a.shopId=shop.id --Branch                
left join client v on a.wlId=v.id --Client                        
left join users make on a.makeId=make.Id --Create By                       
left join users checks on a.checkId=checks.id --Reviewed By                         
left join users biz on a.bizId=biz.id --Business Representative      
left join logistics wl on a.sendId=wl.id --Logistics           
left join viewSalesReceiptItemSum item on a.id=item.pId --Entry Summary    
left join viewReceivableSourceBillPriceCheckNowSum p on a.number=p.sourceNumber
GO