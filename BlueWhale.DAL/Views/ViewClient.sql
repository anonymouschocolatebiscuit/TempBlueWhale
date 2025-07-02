USE [BlueWhale_ERP]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
           
CREATE VIEW [dbo].[viewClient]              
AS              
SELECT 
    a.*,            
    c.names shopName,            
    payNeed-payReady AS Balance,              
    l.names linkMan,              
    l.phone,              
    l.tel,              
    l.qq,              
    l.address,              
    t.names typeName        
FROM client a            
LEFT JOIN company c ON a.shopId=c.id            
LEFT JOIN clientLinkMan l ON a.id=l.pId AND l.moren=1              
LEFT JOIN clientType t ON a.typeId=t.id 
GO