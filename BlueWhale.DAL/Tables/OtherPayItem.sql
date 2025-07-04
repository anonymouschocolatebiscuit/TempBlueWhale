﻿USE [BlueWhale_ERP]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO        
CREATE TABLE [dbo].[OtherPayItem](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[pId] [int] NULL,
	[typeId] [int] NULL,
	[price] [float] NULL,
	[remarks] [varchar](1000) NULL,
 CONSTRAINT [PK__OtherPayItem__48CFD27E] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO