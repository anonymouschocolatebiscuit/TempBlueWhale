USE [BlueWhale_ERP]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[viewVenderType]
AS  
SELECT 
	a.*,
	ISNULL(COUNT(c.id),0) AS num  
FROM venderType a
LEFT JOIN vender c ON a.id=c.typeId

GROUP BY
	a.id,
	a.names,
	a.flag,
	a.shopId
GO