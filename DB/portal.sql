USE [portal]
GO
/****** Object:  Table [dbo].[User]    Script Date: 11/06/2013 10:13:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[User](
	[UserID] [varchar](32) NOT NULL,
	[UserName] [varchar](50) NULL,
	[UserPassword] [varchar](50) NULL,
	[Supplier] [varchar](50) NULL,
	[Created] [datetime] NULL,
	[CeateBy] [varchar](50) NULL,
	[Updated] [datetime] NULL,
	[UpdateBy] [varchar](50) NULL,
	[Status] [varchar](50) NULL,
	[UserType] [varchar](32) NULL,
	[Phone] [varchar](32) NULL,
	[Email] [varchar](50) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Supplier]    Script Date: 11/06/2013 10:13:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Supplier](
	[SupplierID] [varchar](32) NOT NULL,
	[SupplierName] [varchar](255) NULL,
	[ContactName] [varchar](50) NULL,
	[CountryCode] [varchar](50) NULL,
	[SupplierAddress] [varchar](255) NULL,
	[SupplierEmail] [varchar](50) NULL,
	[SupplierPhone] [varchar](50) NULL,
	[DUNS] [varchar](50) NULL,
	[Status] [varchar](50) NULL,
	[Created] [datetime] NULL,
	[CreateBy] [varchar](50) NULL,
	[Updated] [datetime] NULL,
	[UpdateBy] [varchar](50) NULL,
 CONSTRAINT [PK_SupplierInfor] PRIMARY KEY CLUSTERED 
(
	[SupplierID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Site]    Script Date: 11/06/2013 10:13:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Site](
	[SiteID] [varchar](32) NOT NULL,
	[SupplierID] [varchar](32) NULL,
	[SiteName] [varchar](50) NULL,
 CONSTRAINT [PK_Site] PRIMARY KEY CLUSTERED 
(
	[SiteID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RosLines]    Script Date: 11/06/2013 10:13:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RosLines](
	[RosLineID] [varchar](32) NOT NULL,
	[RosDemandDate] [date] NULL,
	[RosDemandQuantity] [varchar](50) NULL,
	[RosEtaQty] [varchar](50) NULL,
	[RosEtdQty] [varchar](50) NULL,
	[RosDio] [varchar](50) NULL,
	[RosShortageQty] [varchar](50) NULL,
	[RosHeaderID] [varchar](32) NULL,
 CONSTRAINT [PK_RosLines] PRIMARY KEY CLUSTERED 
(
	[RosLineID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RosHeader]    Script Date: 11/06/2013 10:13:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RosHeader](
	[RosHeaderID] [varchar](32) NOT NULL,
	[RosItemNO] [varchar](50) NULL,
	[RosDesc] [varchar](50) NULL,
	[RosModel] [varchar](50) NULL,
	[RosCategory] [varchar](50) NULL,
	[RosBuyer] [varchar](50) NULL,
	[RosAllocationPercent] [varchar](50) NULL,
	[RosLeadTime] [varchar](50) NULL,
	[RosStockQty] [varchar](50) NULL,
	[RosSafeStock] [varchar](50) NULL,
	[PoNumber] [varchar](50) NULL,
	[OpenPoQty] [varchar](50) NULL,
	[messageBodyID] [varchar](32) NULL,
 CONSTRAINT [PK_RosHeader] PRIMARY KEY CLUSTERED 
(
	[RosHeaderID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[POLine]    Script Date: 11/06/2013 10:13:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[POLine](
	[POLineID] [varchar](32) NOT NULL,
	[POHeaderID] [varchar](32) NULL,
	[lineNo] [varchar](50) NULL,
	[item_no] [varchar](50) NULL,
	[request_qty] [varchar](50) NULL,
	[request_delivery_date] [date] NULL,
	[unit_price] [varchar](50) NULL,
	[desc] [varchar](50) NULL,
	[price_unit] [varchar](50) NULL,
	[line_item_tatoal_amount] [varchar](50) NULL,
	[schedule_delivery_date] [date] NULL,
	[schedule_delivery_qty] [varchar](50) NULL,
 CONSTRAINT [PK_POLine] PRIMARY KEY CLUSTERED 
(
	[POLineID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[POHeader]    Script Date: 11/06/2013 10:13:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[POHeader](
	[POHeaderID] [varchar](32) NOT NULL,
	[messageBodyID] [varchar](32) NULL,
	[buyer] [varchar](50) NULL,
	[po_date] [date] NULL,
	[po_number] [varchar](50) NULL,
	[po_type] [varchar](50) NULL,
	[your_reference] [varchar](50) NULL,
	[desc] [varchar](50) NULL,
	[delivery_location] [varchar](50) NULL,
	[currency] [varchar](50) NULL,
	[terms_of_delivery] [varchar](50) NULL,
 CONSTRAINT [PK_POHeader] PRIMARY KEY CLUSTERED 
(
	[POHeaderID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Message]    Script Date: 11/06/2013 10:13:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Message](
	[messageID] [varchar](32) NOT NULL,
	[messageName] [varchar](50) NULL,
	[key] [varchar](50) NULL,
	[status] [varchar](50) NULL,
	[notes] [varchar](50) NULL,
	[confirmDateTime] [date] NULL,
	[creationDateTime] [date] NULL,
	[edi_location_code] [varchar](50) NULL,
	[ship_from] [varchar](50) NULL,
	[messageType] [varchar](50) NULL,
	[vender_name] [varchar](50) NULL,
	[vender_num] [varchar](50) NULL,
	[vender_site] [varchar](50) NULL,
	[vender_site_num] [varchar](50) NULL,
	[duns_num] [varchar](50) NULL,
	[contact_name] [varchar](50) NULL,
	[email] [varchar](255) NULL,
	[phone] [varchar](50) NULL,
	[address] [varchar](255) NULL,
	[street] [varchar](50) NULL,
	[city] [varchar](50) NULL,
	[postal_code] [varchar](50) NULL,
	[segment1] [varchar](50) NULL,
	[segment2] [varchar](50) NULL,
	[segment3] [varchar](50) NULL,
	[segment4] [varchar](50) NULL,
	[segment5] [varchar](50) NULL,
	[segment6] [varchar](50) NULL,
	[segment7] [varchar](50) NULL,
	[segment8] [varchar](50) NULL,
	[segment9] [varchar](50) NULL,
	[segment10] [varchar](50) NULL,
 CONSTRAINT [PK_MessageBody] PRIMARY KEY CLUSTERED 
(
	[messageID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[History]    Script Date: 11/06/2013 10:13:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[History](
	[HistoryID] [varchar](32) NOT NULL,
	[FileName] [varchar](50) NULL,
	[MessageType] [varchar](50) NULL,
	[Operater] [varchar](50) NULL,
	[segment1] [varchar](50) NULL,
	[segment2] [varchar](50) NULL,
	[segment3] [varchar](50) NULL,
	[segment4] [varchar](50) NULL,
	[segment5] [varchar](50) NULL,
	[Created] [date] NULL,
	[CreateBy] [varchar](50) NULL,
	[Updated] [date] NULL,
	[UpdateBy] [varchar](50) NULL,
	[Status] [varchar](20) NULL,
 CONSTRAINT [PK_Message] PRIMARY KEY CLUSTERED 
(
	[HistoryID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
