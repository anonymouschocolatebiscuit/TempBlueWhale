SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
         
CREATE VIEW [dbo].[viewOtherIn]             
AS              
SELECT a.*,                   
shop.Names shopName, 
v.Names wlName,              
make.names makeName,              
checks.names checkName,              
biz.names bizName,      
sumNum,      
sumPrice     
           
FROM OtherIn   a                   
LEFT JOIN shop shop on a.shopId=shop.id
LEFT JOIN vender v on a.wlId=v.id           
LEFT JOIN users make on a.makeId=make.Id            
LEFT JOIN users checks on a.checkId=checks.id             
LEFT JOIN users biz on a.bizId=biz.id                  
LEFT JOIN viewOtherInItemSum item on a.id=item.pId
GO