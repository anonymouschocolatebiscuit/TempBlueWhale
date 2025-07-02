SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
    
--drop view viewGoodsShow    
create view [dbo].[viewGoodsShow]      
as      
select a.*,      
isnull(tj,0) tj,      
isnull(xp,0) xp,      
isnull(cx,0) cx      
from viewGoods a      
left join goodsShow s on a.id=s.goodsId
GO