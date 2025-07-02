USE [BlueWhale_ERP]
Go

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
                        
create view [dbo].[viewSalesReceiptDetail]            
as            
select             
    a.id,            
    a.bizDate,        
    a.shopId,        
    a.number,            
    a.types,            
    a.wlId,            
    a.wlName,            
    goodsId,            
    code,            
    goodsName,            
    spec,            
    unitName,        
    sourceNumber,            
    ckId,            
    ckName,            
    num,            
    price,  
    b.dis,  
    tax,  
    priceNow,  
    priceTax,            
    b.sumPriceNow,    
    b.sumPriceDis,  
    b.sumPriceTax,    
    b.sumPriceAll                     
from viewSalesReceipt a            
left join viewSalesReceiptItem b on a.id=b.pId           
where a.flag='Review' 
GO