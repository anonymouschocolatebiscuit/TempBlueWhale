USE [BlueWhale_ERP]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[viewVenderNeedPayFlow]
AS  

--初始余额
SELECT	
	shopId, 
	id AS wlId,
	names wlName,
	'' AS bizDate,
	'' AS number,
	'期初余额' AS bizType,
	payNeed,
	payReady 
FROM vender

UNION ALL

--采购入库
SELECT 
	shopId,
	wlId,
	wlName,
	bizDate,
	number,
	'普通采购' AS bizType,
	sumPriceAll AS payNeed,
	payNow AS payReady 
FROM viewPurReceipt
WHERE types = 1 AND flag = '审核'

UNION all

--采购退货
SELECT 
	shopId,
	wlId,
	wlName,
	bizDate,
	number,
	'采购退货' AS bizType,
	sumPriceAll AS payNowNo,
	payNow 
FROM viewPurReceipt
WHERE types = -1 
	AND flag = '审核'

UNION ALL

--采购付款
SELECT 
	shopId,
	wlId,
	wlName,
	bizDate,
	number,
	'采购付款' AS bizType,
	0 AS payNeed,
	payPriceSum AS payReady
FROM viewPayMent

UNION ALL 

--其他付款
SELECT
	shopId,
	wlId,
	wlName,
	bizDate,
	number,
	'其他付款' AS bizType,
	0 AS payNeed,
	sumPrice AS payReady
FROM viewOtherPay

GO
