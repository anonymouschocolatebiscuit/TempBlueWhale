USE [BlueWhale_ERP]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create view [dbo].[viewProduceInItemSum]       
as        
select pId,          
sum(num) sumNum,        
sum(sumPriceNow) sumPriceNow,        
sum(sumPriceDis) sumPriceDis,        
sum(sumPriceTax) sumPriceTax,        
sum(sumPriceAll) sumPriceAll        
        
from produceInItem        
        
group by pId
GO
