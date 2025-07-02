create  view [dbo].[viewGoodsBomListItem]
as
select a.*,
goods.names goodsName,
goods.code,
goods.spec,
goods.unitName,
goods.ckId,
goods.ckName
from goodsBomListItem a
left join viewGoods goods on a.goodsId=goods.id
GO


