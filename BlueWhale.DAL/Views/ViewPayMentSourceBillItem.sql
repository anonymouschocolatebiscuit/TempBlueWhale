SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--drop view [viewPayMentSourceBillItem]  
create view [dbo].[viewPayMentSourceBillItem]  
as  
select a.*,  
b.bizDate,  
'Regular Purchase' bizType,  
b.sumPrice sumPriceBill, 
isnull(b.priceCheckNowSum,0) sumPriceCheck,
sumPrice-isnull(priceCheckNowSum,0) sumPriceCheckNo
from PayMentSourceBillItem a  
left join viewPurReceipt b on a.sourceNumber=b.number and a.pId=b.id
GO