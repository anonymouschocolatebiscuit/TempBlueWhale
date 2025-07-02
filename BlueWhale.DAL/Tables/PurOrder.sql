USE [BlueWhale_ERP]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[purOrder](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[shopId] [int] NULL,
	[number] [varchar](100) NULL,
	[wlId] [int] NULL,
	[bizDate] [datetime] NULL,
	[sendDate] [datetime] NULL,
	[remarks] [varchar](1000) NULL,
	[makeId] [int] NULL,
	[makeDate] [datetime] NULL,
	[bizId] [int] NULL,
	[checkId] [int] NULL,
	[checkDate] [datetime] NULL,
	[flag] [varchar](100) NULL,
 CONSTRAINT [PK__purOrder__4BAC3F29] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[purOrder] ADD  CONSTRAINT [DF__purOrder__bizDat__4CA06362]  DEFAULT (getdate()) FOR [bizDate]
GO

ALTER TABLE [dbo].[purOrder] ADD  CONSTRAINT [DF__purOrder__sendDa__4D94879B]  DEFAULT (getdate()) FOR [sendDate]
GO