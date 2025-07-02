USE [BlueWhale_ERP]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[viewReceivableSourceBillPriceCheckNowSum] 
AS    
SELECT sourceNumber,       
SUM(priceCheckNow) priceCheckNowSum
FROM ReceivableSourceBillItem  b

LEFT JOIN Receivable a ON b.pId=a.id
WHERE a.flag='Review'
 
GROUP BY sourceNumber
GO


