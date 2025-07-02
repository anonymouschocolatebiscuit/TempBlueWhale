USE [BlueWhale_ERP]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
          
--drop view viewPurReceiptDetail          
create view [dbo].[viewPurReceiptDetail]          
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
from viewPurReceipt a          
left join viewPurReceiptItem b on a.id=b.pId         
where a.flag='Check' 
GO