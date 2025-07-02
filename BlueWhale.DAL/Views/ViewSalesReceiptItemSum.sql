USE [BlueWhale_ERP]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[ViewSalesReceiptItemSum]    
AS      
SELECT pId,    
sourceNumber,    
SUM(num) sumNum,      
SUM(sumPriceNow) sumPriceNow,      
SUM(sumPriceDis) sumPriceDis,      
SUM(sumPriceTax) sumPriceTax,      
SUM(sumPriceAll) sumPriceAll      
      
FROM SalesReceiptItem      
      
GROUP BY pId,sourceNumber
GO


