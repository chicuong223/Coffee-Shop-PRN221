USE [CoffeeShopDB]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 18/03/2022 22:37:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Bill]    Script Date: 18/03/2022 22:37:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bill](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[BillDate] [date] NULL,
	[StaffUsername] [varchar](32) NULL,
	[Discount] [float] NULL,
	[VoucherID] [nvarchar](450) NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK_Bill] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BillDetail]    Script Date: 18/03/2022 22:37:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BillDetail](
	[BillID] [int] NOT NULL,
	[ProductID] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[UnitPrice] [money] NULL,
	[SubTotal] [money] NULL,
 CONSTRAINT [PK__BillDeta__DAB230248977E860] PRIMARY KEY CLUSTERED 
(
	[BillID] ASC,
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 18/03/2022 22:37:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](max) NOT NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Notification]    Script Date: 18/03/2022 22:37:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notification](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Sender] [varchar](32) NULL,
	[IsRead] [bit] NULL,
	[NotificationDate] [date] NULL,
 CONSTRAINT [PK_Notification] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NotificationDetail]    Script Date: 18/03/2022 22:37:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NotificationDetail](
	[NotificationID] [int] NOT NULL,
	[ProductID] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
 CONSTRAINT [PK_NotificationDetail] PRIMARY KEY CLUSTERED 
(
	[NotificationID] ASC,
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 18/03/2022 22:37:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [nvarchar](100) NOT NULL,
	[Price] [money] NOT NULL,
	[CategoryID] [int] NOT NULL,
	[Stock] [int] NOT NULL,
	[Status] [bit] NULL,
	[ImageURL] [nvarchar](256) NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Staff]    Script Date: 18/03/2022 22:37:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Staff](
	[Username] [varchar](32) NOT NULL,
	[Password] [nvarchar](max) NULL,
	[Email] [nvarchar](256) NULL,
	[AvatarUrl] [nvarchar](max) NULL,
	[FullName] [nvarchar](max) NULL,
	[Phone] [varchar](14) NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK__Staff__536C85E5B1F859B6] PRIMARY KEY CLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Supplier]    Script Date: 18/03/2022 22:37:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Supplier](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Phone] [varchar](14) NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK_Supplier] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Supply]    Script Date: 18/03/2022 22:37:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Supply](
	[ProductID] [int] NOT NULL,
	[SupplierID] [int] NOT NULL,
	[SupplyDate] [date] NULL,
	[Quantity] [int] NULL,
 CONSTRAINT [PK__Supply__EA10D413C91BF00F] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC,
	[SupplierID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Voucher]    Script Date: 18/03/2022 22:37:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Voucher](
	[ID] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Percentage] [float] NOT NULL,
	[ExpirationDate] [date] NOT NULL,
	[UsageCount] [int] NOT NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK_Voucher] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220317175719_ChangeVoucherIdToString', N'5.0.12')
GO
SET IDENTITY_INSERT [dbo].[Bill] ON 

INSERT [dbo].[Bill] ([ID], [BillDate], [StaffUsername], [Discount], [VoucherID], [Status]) VALUES (1, CAST(N'2022-03-18' AS Date), N'chicuong', 0.5, NULL, 1)
SET IDENTITY_INSERT [dbo].[Bill] OFF
GO
INSERT [dbo].[BillDetail] ([BillID], [ProductID], [Quantity], [UnitPrice], [SubTotal]) VALUES (1, 4, 10, 123.0000, 1230.0000)
GO
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([ID], [CategoryName], [Status]) VALUES (1, N'Phone', 1)
INSERT [dbo].[Category] ([ID], [CategoryName], [Status]) VALUES (2, N'Laptop', 1)
INSERT [dbo].[Category] ([ID], [CategoryName], [Status]) VALUES (3, N'Electrical', 1)
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([ID], [ProductName], [Price], [CategoryID], [Stock], [Status], [ImageURL]) VALUES (1, N'Nokia 6', 3525000.0000, 1, 150, 0, N'phone-wallpaper-whatspaper-12.jpg')
INSERT [dbo].[Product] ([ID], [ProductName], [Price], [CategoryID], [Stock], [Status], [ImageURL]) VALUES (2, N'Nokia 6', 15000.0000, 2, 123, 1, N'phone-wallpaper-whatspaper-12.jpg')
INSERT [dbo].[Product] ([ID], [ProductName], [Price], [CategoryID], [Stock], [Status], [ImageURL]) VALUES (3, N'123', 123.0000, 2, 123, 0, NULL)
INSERT [dbo].[Product] ([ID], [ProductName], [Price], [CategoryID], [Stock], [Status], [ImageURL]) VALUES (4, N'Nokia 8.1', 123.0000, 2, 123, 1, N'rintezuka.jpg')
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
INSERT [dbo].[Staff] ([Username], [Password], [Email], [AvatarUrl], [FullName], [Phone], [Status]) VALUES (N'abc', N'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', N'abc@gmail.com', N'cartoon-hand-drown-orange-seamless-brick-wall-texture-vector-illustration-72245335.jpg', N'Chi Cuong Tang', N'0123375897', 1)
INSERT [dbo].[Staff] ([Username], [Password], [Email], [AvatarUrl], [FullName], [Phone], [Status]) VALUES (N'chicuong', N'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', N'chicuong.tang01@gmail.com', NULL, N'Chi Cuong Tang', N'0908436393', 1)
GO
ALTER TABLE [dbo].[Bill]  WITH CHECK ADD  CONSTRAINT [FK__Bill__StaffUsern__3B75D760] FOREIGN KEY([StaffUsername])
REFERENCES [dbo].[Staff] ([Username])
GO
ALTER TABLE [dbo].[Bill] CHECK CONSTRAINT [FK__Bill__StaffUsern__3B75D760]
GO
ALTER TABLE [dbo].[Bill]  WITH CHECK ADD  CONSTRAINT [FK__Bill__VoucherID__3D5E1FD2] FOREIGN KEY([VoucherID])
REFERENCES [dbo].[Voucher] ([ID])
GO
ALTER TABLE [dbo].[Bill] CHECK CONSTRAINT [FK__Bill__VoucherID__3D5E1FD2]
GO
ALTER TABLE [dbo].[BillDetail]  WITH CHECK ADD  CONSTRAINT [FK__BillDetai__BillI__403A8C7D] FOREIGN KEY([BillID])
REFERENCES [dbo].[Bill] ([ID])
GO
ALTER TABLE [dbo].[BillDetail] CHECK CONSTRAINT [FK__BillDetai__BillI__403A8C7D]
GO
ALTER TABLE [dbo].[BillDetail]  WITH CHECK ADD  CONSTRAINT [FK__BillDetai__Produ__412EB0B6] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([ID])
GO
ALTER TABLE [dbo].[BillDetail] CHECK CONSTRAINT [FK__BillDetai__Produ__412EB0B6]
GO
ALTER TABLE [dbo].[Notification]  WITH CHECK ADD  CONSTRAINT [FK__Notificat__Sende__32E0915F] FOREIGN KEY([Sender])
REFERENCES [dbo].[Staff] ([Username])
GO
ALTER TABLE [dbo].[Notification] CHECK CONSTRAINT [FK__Notificat__Sende__32E0915F]
GO
ALTER TABLE [dbo].[NotificationDetail]  WITH CHECK ADD  CONSTRAINT [FK_NotificationDetail_Notification] FOREIGN KEY([NotificationID])
REFERENCES [dbo].[Notification] ([ID])
GO
ALTER TABLE [dbo].[NotificationDetail] CHECK CONSTRAINT [FK_NotificationDetail_Notification]
GO
ALTER TABLE [dbo].[NotificationDetail]  WITH CHECK ADD  CONSTRAINT [FK_NotificationDetail_Product] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([ID])
GO
ALTER TABLE [dbo].[NotificationDetail] CHECK CONSTRAINT [FK_NotificationDetail_Product]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK__Product__Categor__38996AB5] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Category] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK__Product__Categor__38996AB5]
GO
ALTER TABLE [dbo].[Supply]  WITH CHECK ADD  CONSTRAINT [FK__Supply__Supplier__300424B4] FOREIGN KEY([SupplierID])
REFERENCES [dbo].[Supplier] ([ID])
GO
ALTER TABLE [dbo].[Supply] CHECK CONSTRAINT [FK__Supply__Supplier__300424B4]
GO
ALTER TABLE [dbo].[Supply]  WITH CHECK ADD  CONSTRAINT [FK_Supply_Product] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([ID])
GO
ALTER TABLE [dbo].[Supply] CHECK CONSTRAINT [FK_Supply_Product]
GO
