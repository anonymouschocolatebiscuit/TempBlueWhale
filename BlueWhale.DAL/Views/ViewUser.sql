USE [BlueWhale_ERP]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[viewUsers]
AS

Select
u.id,
u.loginName,
u.ShopId,
c.names shopName,
u.names,
r.roleName,
d.deptName,
u.phone,
u.flag,
u.roleId,
u.pwd

from users u
LEFT JOIN company c ON u.shopId=c.Id
LEFT JOIN dept d ON u.deptId=d.deptId
LEFT JOIN roles r ON  u.roleId=r.roleId
GO