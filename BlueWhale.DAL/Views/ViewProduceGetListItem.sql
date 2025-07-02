USE [BlueWhale_ERP]
GO

/****** Object:  View [dbo].[viewProduceGetListItem]    Script Date: 2025/1/11 16:42:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create view [dbo].[viewProduceGetListItem]  
as 

select a.*, g.code, g.names goodsName, 
g.unitName,g.spec,g.typeId,g.typeName,
ck.names ckName, p.number, p.bizDate, p.shopId
from ProduceGetListItem a  
left join viewGoods g on a.goodsId=g.id
left join cangku ck on a.ckId=ck.id
left join ProduceGetList p on a.pId=p.id

GO
