USE [master]
GO
/****** Object:  Database [CoffeeShopDB]    Script Date: 23-Mar-22 10:06:15 AM ******/
CREATE DATABASE [CoffeeShopDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'CoffeeShopDB', FILENAME = N'D:\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\CoffeeShopDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'CoffeeShopDB_log', FILENAME = N'D:\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\CoffeeShopDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [CoffeeShopDB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CoffeeShopDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CoffeeShopDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CoffeeShopDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CoffeeShopDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CoffeeShopDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CoffeeShopDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [CoffeeShopDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [CoffeeShopDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CoffeeShopDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CoffeeShopDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CoffeeShopDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [CoffeeShopDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CoffeeShopDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CoffeeShopDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CoffeeShopDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CoffeeShopDB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [CoffeeShopDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CoffeeShopDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CoffeeShopDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CoffeeShopDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CoffeeShopDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CoffeeShopDB] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [CoffeeShopDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CoffeeShopDB] SET RECOVERY FULL 
GO
ALTER DATABASE [CoffeeShopDB] SET  MULTI_USER 
GO
ALTER DATABASE [CoffeeShopDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CoffeeShopDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CoffeeShopDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CoffeeShopDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [CoffeeShopDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [CoffeeShopDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'CoffeeShopDB', N'ON'
GO
ALTER DATABASE [CoffeeShopDB] SET QUERY_STORE = OFF
GO
USE [CoffeeShopDB]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 23-Mar-22 10:06:15 AM ******/
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
/****** Object:  Table [dbo].[Bill]    Script Date: 23-Mar-22 10:06:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bill](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[BillDate] [datetime] NULL,
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
/****** Object:  Table [dbo].[BillDetail]    Script Date: 23-Mar-22 10:06:15 AM ******/
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
/****** Object:  Table [dbo].[Category]    Script Date: 23-Mar-22 10:06:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](max) NOT NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Notification]    Script Date: 23-Mar-22 10:06:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notification](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Sender] [varchar](32) NULL,
	[IsRead] [bit] NOT NULL,
	[NotificationDate] [datetime] NULL,
	[IsSent] [bit] NOT NULL,
 CONSTRAINT [PK_Notification] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NotificationDetail]    Script Date: 23-Mar-22 10:06:15 AM ******/
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
/****** Object:  Table [dbo].[Product]    Script Date: 23-Mar-22 10:06:15 AM ******/
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
/****** Object:  Table [dbo].[Staff]    Script Date: 23-Mar-22 10:06:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Staff](
	[Username] [varchar](32) NOT NULL,
	[Password] [nvarchar](256) NOT NULL,
	[Email] [nvarchar](256) NOT NULL,
	[AvatarUrl] [nvarchar](max) NULL,
	[FullName] [nvarchar](max) NOT NULL,
	[Phone] [varchar](14) NOT NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK__Staff__536C85E5B1F859B6] PRIMARY KEY CLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Supplier]    Script Date: 23-Mar-22 10:06:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Supplier](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Phone] [varchar](14) NOT NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_Supplier] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Supply]    Script Date: 23-Mar-22 10:06:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Supply](
	[ProductID] [int] NOT NULL,
	[SupplierID] [int] NOT NULL,
	[SupplyDate] [datetime] NOT NULL,
	[Quantity] [int] NULL,
 CONSTRAINT [PK__Supply__EA10D413C91BF00F] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC,
	[SupplierID] ASC,
	[SupplyDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Voucher]    Script Date: 23-Mar-22 10:06:15 AM ******/
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
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_Voucher] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220320155630_Initial', N'5.0.12')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220321035055_SupplyKeyUpdate', N'5.0.12')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220321042952_AddIsSentInNotification', N'5.0.12')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220321104359_ChangeNotificationDateToDateTime', N'5.0.12')
GO
SET IDENTITY_INSERT [dbo].[Bill] ON 

INSERT [dbo].[Bill] ([ID], [BillDate], [StaffUsername], [Discount], [VoucherID], [Status]) VALUES (16, CAST(N'2022-03-23T09:21:05.943' AS DateTime), N'dung', NULL, NULL, 1)
INSERT [dbo].[Bill] ([ID], [BillDate], [StaffUsername], [Discount], [VoucherID], [Status]) VALUES (19, CAST(N'2022-03-23T09:33:12.570' AS DateTime), N'dung', NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[Bill] OFF
GO
INSERT [dbo].[BillDetail] ([BillID], [ProductID], [Quantity], [UnitPrice], [SubTotal]) VALUES (16, 3, 1, 300000.0000, 300000.0000)
INSERT [dbo].[BillDetail] ([BillID], [ProductID], [Quantity], [UnitPrice], [SubTotal]) VALUES (19, 3, 1, 300000.0000, 300000.0000)
GO
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([ID], [CategoryName], [Status]) VALUES (1, N'Powdered Coffee', 1)
INSERT [dbo].[Category] ([ID], [CategoryName], [Status]) VALUES (2, N'Coffee beans', 1)
INSERT [dbo].[Category] ([ID], [CategoryName], [Status]) VALUES (3, N'Equipments', 1)
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([ID], [ProductName], [Price], [CategoryID], [Stock], [Status], [ImageURL]) VALUES (1, N'Cà phê hoà tan VinaCafe', 150000.0000, 1, 208, 1, N'vina-cafe.jfif')
INSERT [dbo].[Product] ([ID], [ProductName], [Price], [CategoryID], [Stock], [Status], [ImageURL]) VALUES (2, N'Phin Inox pha cà phê', 20000.0000, 3, 128, 1, N'phin.jfif')
INSERT [dbo].[Product] ([ID], [ProductName], [Price], [CategoryID], [Stock], [Status], [ImageURL]) VALUES (3, N'Cà phê chồn Buôn Mê Thuột', 300000.0000, 2, 219, 1, N'caphe-chon.jfif')
INSERT [dbo].[Product] ([ID], [ProductName], [Price], [CategoryID], [Stock], [Status], [ImageURL]) VALUES (4, N'Cà phê hạt Robusta', 240000.0000, 2, 45, 1, N'robusta.webp')
INSERT [dbo].[Product] ([ID], [ProductName], [Price], [CategoryID], [Stock], [Status], [ImageURL]) VALUES (5, N'Cà phê Trung Nguyên Premium', 400000.0000, 1, 14, 1, N'trungnguyen.jfif')
INSERT [dbo].[Product] ([ID], [ProductName], [Price], [CategoryID], [Stock], [Status], [ImageURL]) VALUES (6, N'Máy pha cà phê Hario', 450000.0000, 3, 8, 1, N'mayphacaphe.jpg')
INSERT [dbo].[Product] ([ID], [ProductName], [Price], [CategoryID], [Stock], [Status], [ImageURL]) VALUES (7, N'Cà phê hoà tan VinaCafe 2', 20000.0000, 1, 123, 1, N'vina-cafe.jfif')
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
INSERT [dbo].[Staff] ([Username], [Password], [Email], [AvatarUrl], [FullName], [Phone], [Status]) VALUES (N'dung', N'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3', N'dungnguyenquang221@gmail.com', NULL, N'Nguyen Quang Dung', N'0377162315', 1)
GO
INSERT [dbo].[Voucher] ([ID], [Name], [Description], [Percentage], [ExpirationDate], [UsageCount], [Status]) VALUES (N'giam30', N'Voucher giảm 30%', N'giảm 30% tổng bill', 30, CAST(N'2022-03-25' AS Date), 97, 1)
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Bill_StaffUsername]    Script Date: 23-Mar-22 10:06:15 AM ******/
CREATE NONCLUSTERED INDEX [IX_Bill_StaffUsername] ON [dbo].[Bill]
(
	[StaffUsername] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Bill_VoucherID]    Script Date: 23-Mar-22 10:06:15 AM ******/
CREATE NONCLUSTERED INDEX [IX_Bill_VoucherID] ON [dbo].[Bill]
(
	[VoucherID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_BillDetail_ProductID]    Script Date: 23-Mar-22 10:06:15 AM ******/
CREATE NONCLUSTERED INDEX [IX_BillDetail_ProductID] ON [dbo].[BillDetail]
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Notification_Sender]    Script Date: 23-Mar-22 10:06:15 AM ******/
CREATE NONCLUSTERED INDEX [IX_Notification_Sender] ON [dbo].[Notification]
(
	[Sender] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_NotificationDetail_ProductID]    Script Date: 23-Mar-22 10:06:15 AM ******/
CREATE NONCLUSTERED INDEX [IX_NotificationDetail_ProductID] ON [dbo].[NotificationDetail]
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Product_CategoryID]    Script Date: 23-Mar-22 10:06:15 AM ******/
CREATE NONCLUSTERED INDEX [IX_Product_CategoryID] ON [dbo].[Product]
(
	[CategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Staff__A9D1053468E2B7DC]    Script Date: 23-Mar-22 10:06:15 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ__Staff__A9D1053468E2B7DC] ON [dbo].[Staff]
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Supply_SupplierID]    Script Date: 23-Mar-22 10:06:15 AM ******/
CREATE NONCLUSTERED INDEX [IX_Supply_SupplierID] ON [dbo].[Supply]
(
	[SupplierID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Category] ADD  DEFAULT (CONVERT([bit],(0))) FOR [Status]
GO
ALTER TABLE [dbo].[Notification] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsRead]
GO
ALTER TABLE [dbo].[Notification] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsSent]
GO
ALTER TABLE [dbo].[Supply] ADD  DEFAULT ('0001-01-01T00:00:00.000') FOR [SupplyDate]
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
USE [master]
GO
ALTER DATABASE [CoffeeShopDB] SET  READ_WRITE 
GO
