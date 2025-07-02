SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--drop view viewOtherGetPayFlow          
create view [dbo].[viewOtherGetPayFlow]          
as          
                
      
select shopId,bkId,code,bkName,wlName,number,'OtherIncome' bizType,typeId,typeName,bizId,bizName,bizDate,price priceIn,0 priceOut,remarks,remarksMain,flag        
        
from viewOtherGetItemFlow  --其他收入          
        
union all        
          
select shopId,bkId,code,bkName,wlName,number,'OtherExpenses' bizType,typeId,typeName,bizId,bizName,bizDate,0 priceIn,price priceOut,remarks,remarksMain,flag          
        
from viewOtherPayItemFlow  --其他支出 


GO