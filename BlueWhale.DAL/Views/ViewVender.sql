USE [BlueWhale_ERP]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
      
CREATE VIEW [dbo].[viewVender]          
AS            
SELECT 
	a.*,         
	c.names shopName,       
	payNeed-payReady as Balance,          
	l.names linkMan,          
	l.phone,          
	l.tel,          
	l.qq,          
	t.names typeName          
FROM vender a          
left join company c ON a.shopId=c.id      
left join venderLinkMan l ON a.id=l.pId and l.moren=1          
left join venderType t ON a.typeId=t.id
GO
