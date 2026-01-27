USE [BlueWhale_ERP]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[venderLinkMan](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[pId] [int] NULL,
	[names] [varchar](100) NULL,
	[phone] [varchar](100) NULL,
	[tel] [varchar](100) NULL,
	[qq] [varchar](100) NULL,
	[moren] [int] NULL,
 CONSTRAINT [PK__venderLinkMan__239E4DCF] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO