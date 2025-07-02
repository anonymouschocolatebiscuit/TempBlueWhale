USE [BlueWhale_ERP]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--drop view [viewPurReceiptItemSum]  
create view [dbo].[viewPurReceiptItemSum] --Retrieve entry summary, grouped by receipt order ID  
as    
select pId,    
sourceNumber,   
sum(num) sumNum,    
sum(sumPriceNow) sumPriceNow,    
sum(sumPriceDis) sumPriceDis,    
sum(sumPriceTax) sumPriceTax,    
sum(sumPriceAll) sumPriceAll    
from PurReceiptItem    
group by pId,sourceNumber
GO


