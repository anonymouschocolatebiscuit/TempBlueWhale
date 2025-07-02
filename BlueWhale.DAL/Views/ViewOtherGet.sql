SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--drop view viewOtherGet                   
CREATE VIEW [dbo].[viewOtherGet]                   
AS                    
SELECT a.*,                         
v.Names wlName,                    
make.names makeName,                    
checks.names checkName,                    
biz.names bizName,                 
sumPrice,      
bk.code code,    
bk.names bkName           
                 
FROM OtherGet   a                         
LEFT JOIN client v ON a.wlId=v.id ---客户        
LEFT JOIN account bk ON a.bkId=bk.id ---银行账户                  
LEFT JOIN users make ON a.makeId=make.Id --制单人                    
LEFT JOIN users checks ON a.checkId=checks.id --审核人                    
LEFT JOIN users biz ON a.bizId=biz.id --经办人                         
LEFT JOIN viewOtherGetItemSum item ON a.id=item.pId --分录汇总
GO