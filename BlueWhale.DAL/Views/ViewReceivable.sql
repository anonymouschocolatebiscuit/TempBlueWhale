USE [BlueWhale_ERP]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER view [dbo].[viewReceivable]
as
    select a.*,
    v.Names wlName,
    make.names makeName,
    checks.names checkName,
    payPriceSum,  --receivablePriceSum
    priceCheckNowSum --receivableSourcePriceSum
    from Receivable  a
        left join client v on a.wlId=v.id --client
        left join users make on a.makeId=make.Id --createdBy
        left join users checks on a.checkId=checks.id --reviewer
        left join viewReceivableAccountItemSum item on a.id=item.pId --receivableSum
        left join viewReceivableSourceBillItemSum itemBill on a.id=itemBill.pId --receivableSourceSum
GO