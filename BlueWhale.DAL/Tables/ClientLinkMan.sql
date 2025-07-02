USE [BlueWhale_ERP]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[clientLinkMan](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[pId] [int] NULL,
	[names] [varchar](100) NULL,
	[phone] [varchar](100) NULL,
	[tel] [varchar](100) NULL,
	[qq] [varchar](100) NULL,
	[address] [varchar](1000) NULL,
	[moren] [int] NULL,
	[openId] [varchar](1000) NULL,
 CONSTRAINT [PK__clientLinkMan__2B3F6F97] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


