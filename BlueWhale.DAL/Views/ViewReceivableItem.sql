USE [BlueWhale_ERP]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO 
CREATE VIEW [dbo].[viewReceivableAccountItem]    
as    
    select a.*, 
    b.names bkName,  
    t.names payTypeName,  
    p.wlId,
    p.number,    
    p.wlName,  
    p.bizDate,    
    p.flag pFlag,
    p.orderNumber,
    p.checkDate
    from ReceivableAccountItem a    
        left join viewReceivable p on a.pId=p.id    
        left join account b on a.bkId=b.id    
        left join payType t on a.payTypeId=t.id 
GO