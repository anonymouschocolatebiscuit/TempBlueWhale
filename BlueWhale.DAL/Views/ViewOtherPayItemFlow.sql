SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
-- drop view viewOtherPayItemFlow             
create  view [dbo].[viewOtherPayItemFlow]              
as              
select a.*,    
b.code,      
b.bkId,    
b.bkName,       
b.number,      
b.bizDate,      
b.flag,      
b.wlName,    
b.shopId,      
b.bizId,    
b.bizName,    
b.remarks remarksMain,            
g.names typeName,  
g.types bizType           
from OtherPayItem a              
left join payGet g on a.typeId=g.id              
left join viewOtherPay b on a.pId=b.id 
GO