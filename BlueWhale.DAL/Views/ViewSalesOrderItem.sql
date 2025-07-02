USE [BlueWhale_ERP]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
           
CREATE VIEW [dbo].[viewSalesOrderItem]            
AS            
SELECT 
    a.*,            
    g.names goodsName,          
    g.unitName,          
    g.code code,       
    g.barcode barcode,         
    g.spec,          
    g.imagePath,      
    g.imagesPathMoren,     
    i.names iName            
FROM salesOrderItem a            
LEFT JOIN viewgoods g on a.goodsId=g.id            
LEFT JOIN inventory i on a.iId=i.id
GO