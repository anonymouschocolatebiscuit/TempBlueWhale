SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--drop  view viewGoodsTypeList
create view [dbo].[viewGoodsTypeList]
as
select a.*,p.isShowXCX,p.isShowGZH,p.picUrl
from viewGoodsType a
left join goodsTypePicList p on a.id=p.typeId
GO