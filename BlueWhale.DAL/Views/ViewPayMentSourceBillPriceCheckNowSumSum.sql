USE [BlueWhale_ERP]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Summarize the current write-off amount based on the source document, for linking with the purchase receipt order query ----- Paid
--drop view [viewPayMentSourceBillPriceCheckNowSumSum]
create view [dbo].[viewPayMentSourceBillPriceCheckNowSumSum] --Summarize source document entries, grouped by source document number    
as    
select sourceNumber,       
sum(priceCheckNow) priceCheckNowSum
from PayMentSourceBillItem    a
left join PayMent b on a.pId=b.id
where b.flag='Check'
group by sourceNumber
GO


