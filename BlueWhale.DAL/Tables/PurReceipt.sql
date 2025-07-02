CREATE TABLE [dbo].[purReceipt](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[shopId] [int] NULL,
	[number] [varchar](100) NULL,
	[wlId] [int] NULL,
	[bizDate] [datetime] NULL,
	[types] [int] NULL,
	[remarks] [varchar](1000) NULL,
	[dis] [decimal](18, 2) NULL,
	[disPrice] [decimal](18, 2) NULL,
	[sumPrice] [decimal](18, 2) NULL,
	[payNow] [decimal](18, 2) NULL,
	[payNowNo] [decimal](18, 2) NULL,
	[bkId] [int] NULL,
	[makeId] [int] NULL,
	[makeDate] [datetime] NULL,
	[bizId] [int] NULL,
	[checkId] [int] NULL,
	[checkDate] [datetime] NULL,
	[flag] [varchar](100) NULL,
 CONSTRAINT [PK__purReceipt__628FA481] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[purReceipt] ADD  CONSTRAINT [DF__purReceip__bizDa__6383C8BA]  DEFAULT (getdate()) FOR [bizDate]
GO