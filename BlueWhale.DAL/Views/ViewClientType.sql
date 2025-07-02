USE [BlueWhale_ERP]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[viewClientType]
AS 

SELECT	a.*,  
		isnull(count(c.id),0) num  
FROM clientType		AS a WITH(NOLOCK)
LEFT JOIN client	AS c WITH(NOLOCK) ON a.id=c.typeId  
GROUP BY a.id, a.names, a.flag, a.shopId, a.dis
