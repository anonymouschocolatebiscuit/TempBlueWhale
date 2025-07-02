USE [BlueWhale_ERP]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Storage](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[shopId] [int] NULL,
	[code] [varchar](100) NULL,
	[names] [varchar](100) NULL,
	[makeDate] [datetime] NULL,
	[flag] [int] NULL,
 CONSTRAINT [PK__Storage__164452B1] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Storage] ADD  CONSTRAINT [DF__Storage__makeDate__173876EA]  DEFAULT (getdate()) FOR [makeDate]
GO

ALTER TABLE [dbo].[Storage] ADD  CONSTRAINT [DF__Storage__flag__182C9B23]  DEFAULT ((1)) FOR [flag]
GO


