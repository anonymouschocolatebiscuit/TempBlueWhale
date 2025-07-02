USE [BlueWhale_ERP]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
    
create view [dbo].[viewClientAddress]                          
as                                  
select 
    c.*,                          
    pro.names proName,                          
    ct.names ctName,                          
    a.names areaName                                              
from clientAddress c                               
left join province pro on c.proId=pro.id                          
left join city ct on c.ctId=ct.id                           
left join Area a on c.areaId=a.id 
GO