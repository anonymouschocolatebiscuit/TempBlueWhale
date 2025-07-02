USE [BlueWhale_ERP]
GO

/****** Object:  Table [dbo].[OtherOut]    Script Date: 2025/1/11 16:34:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OtherOut](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[shopId] [int] NULL,
	[number] [varchar](100) NULL,
	[wlId] [int] NULL,
	[bizDate] [datetime] NULL,
	[types] [int] NULL,
	[remarks] [varchar](1000) NULL,
	[makeId] [int] NULL,
	[makeDate] [datetime] NULL,
	[bizId] [int] NULL,
	[checkId] [int] NULL,
	[checkDate] [datetime] NULL,
	[flag] [varchar](100) NULL,
 CONSTRAINT [PK__OtherOut__2EDAF651] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[OtherOut] ADD  CONSTRAINT [DF__OtherOut__bizDat__2FCF1A8A]  DEFAULT (getdate()) FOR [bizDate]
GO


