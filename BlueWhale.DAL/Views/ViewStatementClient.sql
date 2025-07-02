USE [BlueWhale_ERP]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[viewStatementClient]          
AS        
          
SELECT
    shopId,     
    id wlId,    
    names wlName,    
    '2000-01-01' bizDate,    
    '' number,    
    'Opening Balance' bizType,    
    0 sumPrice,    
    0 disPrice,    
    0 payReady,    
    0 payNeed     
FROM client       
    
UNION   
   
SELECT   
    shopId,    
    wlId,    
    wlName,    
    bizDate,    
    number,    
    'Sales' bizType,    
    sumPriceAll sumPrice,    
    disPrice,    
    payNow payReady,    
    payNowNo payNeed    
FROM viewSalesReceipt where types=1      
    
UNION      
  
SELECT   
    shopId,    
    wlId,    
    wlName,    
    bizDate,    
    number,    
    'Sales Refund' bizType,    
    sumPriceAll sumPrice,    
    disPrice,    
    payNow payReady,    
    payNowNo payNeed    
FROM viewSalesReceipt where types=-1      
    
UNION      
        
SELECT 
    shopId,      
    wlId,    
    wlName,    
    bizDate,    
    number,'Sales Payment' bizType,    
    0 sumPrice,    
    disPrice,    
    payPriceSum payReady,    
    -disPrice payNeed    
FROM viewReceivable  
  
UNION   
    
SELECT  
    shopId,     
    wlId,    
    wlName,    
    bizDate,    
    number,'Other Payment' bizType,    
    sumPrice,    
    0 disPrice,    
    0 payReady,    
    0 payNeed    
FROM viewOtherGet  