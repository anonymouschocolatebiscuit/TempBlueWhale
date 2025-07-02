SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--drop view viewPayMentAccountItem  
create view [dbo].[viewPayMentAccountItem]  
as  
select a.*,
b.names bkName,
t.names payTypeName,  
p.number, 
p.wlName, 
p.bizDate,  
p.flag pFlag  
from PayMentAccountItem a  
left join viewPayMent p on a.pId=p.id  
left join account b on a.bkId=b.id  
left join payType t on a.payTypeId=t.id  
GO