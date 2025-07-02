SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
      
--drop view [viewClientNeedPayFlow]            
create view [dbo].[viewClientNeedPayFlow]            
as            
               
select shopId,id wlId,names wlName,'' bizDate,'' number,'Opening Balance' bizType,0 payNeed,0 payReady from client         
union all
             
select shopId,wlId,wlName,bizDate,number,'Sales' bizType,sumPriceAll payNeed,payNow payReady from viewSalesReceipt where types=1 and flag='Review'      
union all
              
select shopId,wlId,wlName,bizDate,number,'Sales Refund' bizType,sumPriceAll payNeed,payNow payReady from viewSalesReceipt where types=-1   and flag='Review'      
  
union all
              
select shopId,wlId,wlName,bizDate,number,'Sales Payment' bizType,-priceCheckNowSum payNeed,payPriceNowMore payReady from viewReceivable       
      
union all
          
select shopId,wlId,wlName,bizDate,number,'Other Payment' bizType,0 payNeed,sumPrice payReady from viewOtherGet        
      
--select * from viewOtherGet 
GO