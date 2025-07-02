SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
           
CREATE VIEW [dbo].[viewOtherOut]             
AS              
SELECT a.*,   
shop.Names shopName,                 
v.Names wlName,              
make.names makeName,              
checks.names checkName,              
biz.names bizName,      
sumNum,      
sumPrice     
           
FROM OtherOut   a        
LEFT JOIN shop shop ON a.shopId=shop.id         
LEFT JOIN client v ON a.wlId=v.id             
LEFT JOIN users make ON a.makeId=make.Id           
LEFT JOIN users checks ON a.checkId=checks.id              
LEFT JOIN users biz ON a.bizId=biz.id                   
left JOIN viewOtherOutItemSum item ON a.id=item.pId
GO
