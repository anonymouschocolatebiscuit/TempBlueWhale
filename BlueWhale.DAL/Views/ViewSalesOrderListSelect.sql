USE [BlueWhale_ERP]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[viewSalesOrderListSelect]
AS
SELECT 
	item.*,
	g.names goodsName,
	g.spec,
	g.unitName,
	g.code,
	g.barcode,
	g.priceCost,
	p.shopId,
	p.number,
	p.makeDate,
	p.bizDate,
	p.sendDate,
	p.wlName
FROM
salesOrderItem item
LEFT JOIN viewGoods g on item.goodsId=g.id
LEFT JOIN viewSalesOrder p on item.pId=p.id 
GO