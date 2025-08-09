USE [BlueWhale_ERP]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[viewReceivableSourceBillItem]  
AS  
SELECT a.*,  
		b.bizDate,  
		'Sales' bizType,  
		b.sumPrice sumPriceBill,--Transaction Amount  
		isnull(b.priceCheckNowSum,0) sumPriceCheck, --Written-off Amount  
		sumPrice-isnull(priceCheckNowSum,0) sumPriceCheckNo --Remaining Balance  
		FROM ReceivableSourceBillItem a  
			left join viewSalesReceipt b ON a.sourceNumber=b.number and a.pId=b.id
GO