CREATE TABLE [dbo].[goodsBomListItem](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[pId] [int] NULL,
	[itemId] [int] NULL,
	[goodsId] [int] NULL,
	[num] [float] NULL,
	[rate] [float] NULL,
	[remarks] [varchar](1000) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


