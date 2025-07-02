SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GoodsOpen](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[shopId] [int] NULL,
	[number] [varchar](100) NULL,
	[bizDate] [datetime] NULL,
	[fee] [decimal](18, 2) NULL,
	[remarks] [varchar](1000) NULL,
	[makeId] [int] NULL,
	[makeDate] [datetime] NULL,
	[bizId] [int] NULL,
	[checkId] [int] NULL,
	[checkDate] [datetime] NULL,
	[flag] [varchar](100) NULL,
 CONSTRAINT [PK__GoodsOpen__4C8B54C9] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO