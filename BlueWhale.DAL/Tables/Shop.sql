USE [BlueWhale_ERP]
GO

/****** Object:  Table [dbo].[shop]    Script Date: 2025-3-13 14:41:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Shop](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[code] [varchar](100) NULL,
	[names] [varchar](100) NULL,
	[address] [varchar](1000) NULL,
	[tel] [varchar](100) NULL,
	[fax] [varchar](100) NULL,
	[flag] [varchar](100) NULL,
	[makeDate] [datetime] NULL,
 CONSTRAINT [PK__Shop__2BE97B0D] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


