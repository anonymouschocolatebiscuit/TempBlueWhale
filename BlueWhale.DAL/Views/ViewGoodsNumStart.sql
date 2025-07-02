SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


--drop view viewGoodsNumStart -------- sum goods initial quantity and price
create view [dbo].[viewGoodsNumStart]
as
select goodsId,
sum(num) sumNumStart,
sum(sumPrice) sumPriceStart
from goodsNumStart
group by goodsId

GO