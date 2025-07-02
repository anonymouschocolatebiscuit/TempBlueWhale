USE [BlueWhale_ERP]
GO

/****** Object:  View [dbo].[viewProduceGetListItemSum]    Script Date: 2025/1/11 16:42:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create view [dbo].[viewProduceGetListItemSum]
as
select pId,sum(num) sumNum,sum(num*price) sumPrice
from produceGetListItem
group by pId

GO
