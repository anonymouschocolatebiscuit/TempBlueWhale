CREATE TABLE [dbo].[goodsBomList](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[shopId] [int] NULL,
	[typeId] [int] NULL,
	[number] [varchar](100) NULL,
	[edition] [varchar](100) NULL,
	[flagUse] [varchar](100) NULL,
	[flagCheck] [varchar](100) NULL,
	[tuhao] [varchar](100) NULL,
	[goodsId] [int] NULL,
	[num] [float] NULL,
	[rate] [float] NULL,
	[remarks] [varchar](1000) NULL,
	[makeId] [int] NULL,
	[makeDate] [datetime] NULL,
	[checkId] [int] NULL,
	[checkDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


