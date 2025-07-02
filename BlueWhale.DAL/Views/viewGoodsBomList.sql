create view [dbo].[viewGoodsBomList]
as
select a.*,
t.names typeName,
goods.names goodsName,
goods.code,
goods.spec,
goods.unitName,
make.names makeName,
checks.names checkName
from goodsBomList a
left join viewGoods goods on a.goodsId=goods.id
left join goodsBomListType t on a.typeId=t.id
left join users make on a.makeId=make.id
left join users checks on a.checkId=checks.id
GO


