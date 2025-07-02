USE [BlueWhale_ERP]
GO
/****** Object:  Table [dbo].[systemSet]    Script Date: 2020/2/23 20:12:07 ******/

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[systemSet](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[shopId] [int] NOT NULL,
	[company] [varchar](100) NULL,
	[address] [varchar](100) NULL,
	[tel] [varchar](100) NULL,
	[fax] [varchar](100) NULL,
	[postCode] [varchar](100) NULL,
	[dateStart] [datetime] NULL,
	[dateEnd] [datetime] NULL,
	[userNum] [int] NULL,
	[msgNum] [int] NULL,
	[bwb] [varchar](100) NULL,
	[num] [int] NULL,
	[price] [int] NULL,
	[priceType] [int] NULL,
	[checkNum] [int] NULL,
	[useCheck] [int] NULL,
	[useTax] [int] NULL,
	[Tax] [int] NULL,
	[logoURL] [varchar](100) NULL,
	[zhangURL] [varchar](100) NULL,
	[fieldA] [varchar](100) NULL,
	[fieldB] [varchar](100) NULL,
	[fieldC] [varchar](100) NULL,
	[fieldD] [varchar](100) NULL,
	[weixinId] [varchar](100) NULL,
	[appId] [varchar](1000) NULL,
	[appSecret] [varchar](1000) NULL,
	[mchId] [varchar](1000) NULL,
	[appKey] [varchar](1000) NULL,
	[sendUrl] [varchar](1000) NULL,
	[payUrl] [varchar](1000) NULL,
	[notifyUrl] [varchar](1000) NULL,
	[appName] [varchar](100) NULL,
	[corpIdQY] [varchar](1000) NULL,
	[corpSecretQY] [varchar](1000) NULL,
	[corpIdDD] [varchar](1000) NULL,
	[corpSecretDD] [varchar](1000) NULL,
	[permanentCodeQY] [varchar](1000) NULL,
	[permanentCodeDD] [varchar](1000) NULL,
	[Location_X] [decimal](18, 6) NULL,
	[Location_Y] [decimal](18, 6) NULL,
	[RemarksPurOrder] [text] NULL,
	[RemarksSalesOrder] [text] NULL,
	[quteRemarks] [text] NULL,
	[printLogo] [varchar](50) NULL,
	[printZhang] [varchar](50) NULL,
	[userSecret] [varchar](1000) NULL,
	[checkInSecret] [varchar](1000) NULL,
	[applySecret] [varchar](1000) NULL,
	[SecretBuy] [varchar](1000) NULL,
	[SecretSales] [varchar](1000) NULL,
	[SecretStore] [varchar](1000) NULL,
	[SecretFee] [varchar](1000) NULL,
	[SecretReport] [varchar](1000) NULL,
	[SecretCheckIn] [varchar](1000) NULL,
	[SecretApply] [varchar](1000) NULL,
 CONSTRAINT [PK_systemSet] PRIMARY KEY CLUSTERED 
(
	[shopId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO