USE [BlueWhale_ERP]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[viewProcessList]
AS 
SELECT	a.*,
		t.names AS typeName,
		u.names AS unitName
FROM processList		AS a WITH(NOLOCK)
LEFT JOIN processType	AS t WITH(NOLOCK) ON a.typeId=t.id
LEFT JOIN unit			AS u WITH(NOLOCK) ON a.unitId=u.id