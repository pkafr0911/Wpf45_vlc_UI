USE [master]
GO
/****** Object:  Database [QuanLyCamera]    Script Date: 4/18/2022 3:52:11 PM ******/
CREATE DATABASE [QuanLyCamera]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'QuanLyCamera', FILENAME = N'F:\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\QuanLyCamera.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'QuanLyCamera_log', FILENAME = N'F:\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\QuanLyCamera_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [QuanLyCamera] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [QuanLyCamera].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [QuanLyCamera] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [QuanLyCamera] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [QuanLyCamera] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [QuanLyCamera] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [QuanLyCamera] SET ARITHABORT OFF 
GO
ALTER DATABASE [QuanLyCamera] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [QuanLyCamera] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [QuanLyCamera] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [QuanLyCamera] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [QuanLyCamera] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [QuanLyCamera] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [QuanLyCamera] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [QuanLyCamera] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [QuanLyCamera] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [QuanLyCamera] SET  DISABLE_BROKER 
GO
ALTER DATABASE [QuanLyCamera] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [QuanLyCamera] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [QuanLyCamera] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [QuanLyCamera] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [QuanLyCamera] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [QuanLyCamera] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [QuanLyCamera] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [QuanLyCamera] SET RECOVERY FULL 
GO
ALTER DATABASE [QuanLyCamera] SET  MULTI_USER 
GO
ALTER DATABASE [QuanLyCamera] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [QuanLyCamera] SET DB_CHAINING OFF 
GO
ALTER DATABASE [QuanLyCamera] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [QuanLyCamera] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [QuanLyCamera] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [QuanLyCamera] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'QuanLyCamera', N'ON'
GO
ALTER DATABASE [QuanLyCamera] SET QUERY_STORE = OFF
GO
USE [QuanLyCamera]
GO
/****** Object:  User [admin2]    Script Date: 4/18/2022 3:52:12 PM ******/
CREATE USER [admin2] FOR LOGIN [admin2] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [admin1]    Script Date: 4/18/2022 3:52:12 PM ******/
CREATE USER [admin1] FOR LOGIN [admin1] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [admin]    Script Date: 4/18/2022 3:52:12 PM ******/
CREATE USER [admin] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [admin2]
GO
ALTER ROLE [db_datareader] ADD MEMBER [admin2]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [admin2]
GO
ALTER ROLE [db_owner] ADD MEMBER [admin1]
GO
ALTER ROLE [db_accessadmin] ADD MEMBER [admin1]
GO
ALTER ROLE [db_securityadmin] ADD MEMBER [admin1]
GO
ALTER ROLE [db_ddladmin] ADD MEMBER [admin1]
GO
ALTER ROLE [db_backupoperator] ADD MEMBER [admin1]
GO
ALTER ROLE [db_datareader] ADD MEMBER [admin1]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [admin1]
GO
ALTER ROLE [db_denydatareader] ADD MEMBER [admin1]
GO
ALTER ROLE [db_denydatawriter] ADD MEMBER [admin1]
GO
ALTER ROLE [db_owner] ADD MEMBER [admin]
GO
ALTER ROLE [db_securityadmin] ADD MEMBER [admin]
GO
/****** Object:  Table [dbo].[DB_Account]    Script Date: 4/18/2022 3:52:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DB_Account](
	[UID] [int] IDENTITY(1,1) NOT NULL,
	[DB_UserName] [nvarchar](50) NOT NULL,
	[DB_PassWord] [varchar](50) NOT NULL,
	[DB_Roles] [int] NULL,
	[DB_UserGroupID] [int] NULL,
	[DB_CamQuantity] [int] NULL,
	[DB_UserQuantity] [int] NULL,
	[DB_Wallet] [int] NULL,
 CONSTRAINT [PK_DB_Account] PRIMARY KEY CLUSTERED 
(
	[UID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DB_CamURL]    Script Date: 4/18/2022 3:52:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DB_CamURL](
	[CamID] [int] IDENTITY(1,1) NOT NULL,
	[DB_CamURL] [varchar](200) NOT NULL,
	[UID] [int] NOT NULL,
	[DB_CamName] [varchar](100) NULL,
 CONSTRAINT [PK_DB_CamURL] PRIMARY KEY CLUSTERED 
(
	[CamID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[DB_Account] ON 

INSERT [dbo].[DB_Account] ([UID], [DB_UserName], [DB_PassWord], [DB_Roles], [DB_UserGroupID], [DB_CamQuantity], [DB_UserQuantity], [DB_Wallet]) VALUES (1, N'admin', N'admin', 1, 1, 111111, 1111111, 199999999)
INSERT [dbo].[DB_Account] ([UID], [DB_UserName], [DB_PassWord], [DB_Roles], [DB_UserGroupID], [DB_CamQuantity], [DB_UserQuantity], [DB_Wallet]) VALUES (2, N'client', N'client', 2, 1, 11, 11, 1000000)
INSERT [dbo].[DB_Account] ([UID], [DB_UserName], [DB_PassWord], [DB_Roles], [DB_UserGroupID], [DB_CamQuantity], [DB_UserQuantity], [DB_Wallet]) VALUES (4, N'client2', N'client2', 2, 2, 11, 11, 11111111)
INSERT [dbo].[DB_Account] ([UID], [DB_UserName], [DB_PassWord], [DB_Roles], [DB_UserGroupID], [DB_CamQuantity], [DB_UserQuantity], [DB_Wallet]) VALUES (5, N'user2', N'user2', 2, 1, 11, 11, 1111111)
INSERT [dbo].[DB_Account] ([UID], [DB_UserName], [DB_PassWord], [DB_Roles], [DB_UserGroupID], [DB_CamQuantity], [DB_UserQuantity], [DB_Wallet]) VALUES (8, N'user3', N'user3', 3, 2, 11, 11, 111111)
INSERT [dbo].[DB_Account] ([UID], [DB_UserName], [DB_PassWord], [DB_Roles], [DB_UserGroupID], [DB_CamQuantity], [DB_UserQuantity], [DB_Wallet]) VALUES (9, N'user4', N'user4', 3, 1, 11, 11, 11111)
INSERT [dbo].[DB_Account] ([UID], [DB_UserName], [DB_PassWord], [DB_Roles], [DB_UserGroupID], [DB_CamQuantity], [DB_UserQuantity], [DB_Wallet]) VALUES (12, N'admin2', N'admin2', 1, 2, 11, 11, 1111111)
INSERT [dbo].[DB_Account] ([UID], [DB_UserName], [DB_PassWord], [DB_Roles], [DB_UserGroupID], [DB_CamQuantity], [DB_UserQuantity], [DB_Wallet]) VALUES (13, N'userr', N'userr', 3, 1, 11, 11, 1111111)
INSERT [dbo].[DB_Account] ([UID], [DB_UserName], [DB_PassWord], [DB_Roles], [DB_UserGroupID], [DB_CamQuantity], [DB_UserQuantity], [DB_Wallet]) VALUES (15, N'manager', N'manager', 2, 3, 93, 58, 9471999)
INSERT [dbo].[DB_Account] ([UID], [DB_UserName], [DB_PassWord], [DB_Roles], [DB_UserGroupID], [DB_CamQuantity], [DB_UserQuantity], [DB_Wallet]) VALUES (17, N'testuser', N'testuser', 3, 3, 11, 11, 111111)
INSERT [dbo].[DB_Account] ([UID], [DB_UserName], [DB_PassWord], [DB_Roles], [DB_UserGroupID], [DB_CamQuantity], [DB_UserQuantity], [DB_Wallet]) VALUES (34, N'a', N'a', 3, 3, 0, 0, 1111111)
INSERT [dbo].[DB_Account] ([UID], [DB_UserName], [DB_PassWord], [DB_Roles], [DB_UserGroupID], [DB_CamQuantity], [DB_UserQuantity], [DB_Wallet]) VALUES (1035, N'testmanager', N'testmanager', 2, 4, 1, 1, 1000000)
SET IDENTITY_INSERT [dbo].[DB_Account] OFF
GO
SET IDENTITY_INSERT [dbo].[DB_CamURL] ON 

INSERT [dbo].[DB_CamURL] ([CamID], [DB_CamURL], [UID], [DB_CamName]) VALUES (1, N'https://media23.cloudcam.vn/cloud-camera/IDhiF16938868_record/chunklist_w680833415.m3u8', 1, N'Cam1')
INSERT [dbo].[DB_CamURL] ([CamID], [DB_CamURL], [UID], [DB_CamName]) VALUES (2, N'rtsp://admin:viettel123@nemsonla.vddns.vn:554/Streaming/Channels/602', 1, N'nemsonla')
INSERT [dbo].[DB_CamURL] ([CamID], [DB_CamURL], [UID], [DB_CamName]) VALUES (1071, N'rtsp://admin:hd543211@aeonbinhtan.vddns.vn:554/Streaming/Channels/201', 1, N'aeonbinhtan')
INSERT [dbo].[DB_CamURL] ([CamID], [DB_CamURL], [UID], [DB_CamName]) VALUES (2071, N'rtsp://admin:Viettel123@125.212.227.210:7650/Streaming/Channels/102', 1, N'viettel')
INSERT [dbo].[DB_CamURL] ([CamID], [DB_CamURL], [UID], [DB_CamName]) VALUES (3084, N'rtsp://ta12.vddns.vn:554/av0_0', 2, N'Cam1')
INSERT [dbo].[DB_CamURL] ([CamID], [DB_CamURL], [UID], [DB_CamName]) VALUES (3085, N'rtsp://admin:viettel123@nemsonla.vddns.vn:554/Streaming/Channels/602', 2, N'Cam2')
INSERT [dbo].[DB_CamURL] ([CamID], [DB_CamURL], [UID], [DB_CamName]) VALUES (3086, N'rtsp://admin:viettel123@nemsonla.vddns.vn:554/Streaming/Channels/602', 2, N'nemsonla')
INSERT [dbo].[DB_CamURL] ([CamID], [DB_CamURL], [UID], [DB_CamName]) VALUES (3087, N'rtsp://admin:Viettel123@125.212.227.210:7650/Streaming/Channels/102', 2, N'viettel')
INSERT [dbo].[DB_CamURL] ([CamID], [DB_CamURL], [UID], [DB_CamName]) VALUES (4084, N'rtsp://ta12.vddns.vn:554/av0_0', 4, N'Cam1')
INSERT [dbo].[DB_CamURL] ([CamID], [DB_CamURL], [UID], [DB_CamName]) VALUES (4085, N'rtsp://admin:viettel123@nemsonla.vddns.vn:554/Streaming/Channels/602', 4, N'nemsonla')
INSERT [dbo].[DB_CamURL] ([CamID], [DB_CamURL], [UID], [DB_CamName]) VALUES (4086, N'rtsp://ta12.vddns.vn:554/av0_0', 4, N'Cam6')
INSERT [dbo].[DB_CamURL] ([CamID], [DB_CamURL], [UID], [DB_CamName]) VALUES (4087, N'rtsp://admin:Viettel123@125.212.227.210:7650/Streaming/Channels/102', 4, N'viettel')
INSERT [dbo].[DB_CamURL] ([CamID], [DB_CamURL], [UID], [DB_CamName]) VALUES (4088, N'rtsp://admin:hd543211@aeonbinhtan.vddns.vn:554/Streaming/Channels/201', 4, N'aeonbinhtan')
INSERT [dbo].[DB_CamURL] ([CamID], [DB_CamURL], [UID], [DB_CamName]) VALUES (4089, N'rtsp://ta11.vddns.vn:554/av0_011', 4, N'cam8')
INSERT [dbo].[DB_CamURL] ([CamID], [DB_CamURL], [UID], [DB_CamName]) VALUES (4111, N'https://media20.cloudcam.vn/cloud-camera/demoidc1603266402290.stream/chunklist_w2045548587.m3u8', 1, N'cam9')
INSERT [dbo].[DB_CamURL] ([CamID], [DB_CamURL], [UID], [DB_CamName]) VALUES (4112, N'https://media10.cloudcam.vn/cloud-camera/demoidc1603266947167.stream/chunklist_w1044548222.m3u8', 1, N'Server')
INSERT [dbo].[DB_CamURL] ([CamID], [DB_CamURL], [UID], [DB_CamName]) VALUES (4113, N'rtsp://ta12.vddns.vn:554/av0_0', 5, N'Cam1')
INSERT [dbo].[DB_CamURL] ([CamID], [DB_CamURL], [UID], [DB_CamName]) VALUES (4114, N'rtsp://admin:Viettel123@125.212.227.210:7650/Streaming/Channels/102', 5, N'viettel')
INSERT [dbo].[DB_CamURL] ([CamID], [DB_CamURL], [UID], [DB_CamName]) VALUES (4115, N'rtsp://admin:hd543211@aeonbinhtan.vddns.vn:554/Streaming/Channels/201', 5, N'aeonbinhtan')
INSERT [dbo].[DB_CamURL] ([CamID], [DB_CamURL], [UID], [DB_CamName]) VALUES (4116, N'https://media24.cloudcam.vn/cloud-camera/demoidc1603266947167.stream/chunklist_w1641134218.m3u8', 5, N'Server')
INSERT [dbo].[DB_CamURL] ([CamID], [DB_CamURL], [UID], [DB_CamName]) VALUES (4117, N'rtsp://ta12.vddns.vn:554/av0_0', 9, N'Cam1')
INSERT [dbo].[DB_CamURL] ([CamID], [DB_CamURL], [UID], [DB_CamName]) VALUES (4118, N'rtsp://admin:Viettel123@125.212.227.210:7650/Streaming/Channels/102', 9, N'viettel')
INSERT [dbo].[DB_CamURL] ([CamID], [DB_CamURL], [UID], [DB_CamName]) VALUES (4119, N'rtsp://admin:hd543211@aeonbinhtan.vddns.vn:554/Streaming/Channels/201', 9, N'aeonbinhtan')
INSERT [dbo].[DB_CamURL] ([CamID], [DB_CamURL], [UID], [DB_CamName]) VALUES (4120, N'rtsp://admin:viettel123@nemsonla.vddns.vn:554/Streaming/Channels/602', 9, N'Cam2')
INSERT [dbo].[DB_CamURL] ([CamID], [DB_CamURL], [UID], [DB_CamName]) VALUES (4121, N'rtsp://ta12.vddns.vn:554/av0_0', 12, N'Cam1')
INSERT [dbo].[DB_CamURL] ([CamID], [DB_CamURL], [UID], [DB_CamName]) VALUES (4122, N'rtsp://ta12.vddns.vn:554/av0_0', 12, N'Cam6')
INSERT [dbo].[DB_CamURL] ([CamID], [DB_CamURL], [UID], [DB_CamName]) VALUES (4123, N'rtsp://admin:viettel123@nemsonla.vddns.vn:554/Streaming/Channels/602', 12, N'nemsonla')
INSERT [dbo].[DB_CamURL] ([CamID], [DB_CamURL], [UID], [DB_CamName]) VALUES (4124, N'rtsp://admin:hd543211@aeonbinhtan.vddns.vn:554/Streaming/Channels/201', 12, N'aeonbinhtan')
INSERT [dbo].[DB_CamURL] ([CamID], [DB_CamURL], [UID], [DB_CamName]) VALUES (4125, N'rtsp://ta11.vddns.vn:554/av0_011', 12, N'cam8')
INSERT [dbo].[DB_CamURL] ([CamID], [DB_CamURL], [UID], [DB_CamName]) VALUES (4126, N'rtsp://admin:Viettel123@125.212.227.210:7650/Streaming/Channels/102', 12, N'viettel')
INSERT [dbo].[DB_CamURL] ([CamID], [DB_CamURL], [UID], [DB_CamName]) VALUES (4127, N'https://media04.cloudcam.vn/cloud-camera/demoidc1603266947167.stream/chunklist_w2083068289.m3u8', 12, N'Server')
INSERT [dbo].[DB_CamURL] ([CamID], [DB_CamURL], [UID], [DB_CamName]) VALUES (4136, N'rtsp://ta12.vddns.vn:554/av0_0', 1, N'camtest')
INSERT [dbo].[DB_CamURL] ([CamID], [DB_CamURL], [UID], [DB_CamName]) VALUES (4170, N'rtsp://admin:viettel123@nemsonla.vddns.vn:554/Streaming/Channels/602', 17, N'nemsonla')
INSERT [dbo].[DB_CamURL] ([CamID], [DB_CamURL], [UID], [DB_CamName]) VALUES (4171, N'rtsp://admin:hd543211@aeonbinhtan.vddns.vn:554/Streaming/Channels/201', 17, N'aeonbinhtan')
INSERT [dbo].[DB_CamURL] ([CamID], [DB_CamURL], [UID], [DB_CamName]) VALUES (4196, N'https://media23.cloudcam.vn/cloud-camera/IDhiF16938868_record/chunklist_w680833415.m3u8', 15, N'Cam1')
INSERT [dbo].[DB_CamURL] ([CamID], [DB_CamURL], [UID], [DB_CamName]) VALUES (4197, N'rtsp://admin:viettel123@nemsonla.vddns.vn:554/Streaming/Channels/602', 15, N'nemsonla')
INSERT [dbo].[DB_CamURL] ([CamID], [DB_CamURL], [UID], [DB_CamName]) VALUES (4198, N'rtsp://admin:hd543211@aeonbinhtan.vddns.vn:554/Streaming/Channels/201', 15, N'aeonbinhtan')
INSERT [dbo].[DB_CamURL] ([CamID], [DB_CamURL], [UID], [DB_CamName]) VALUES (4199, N'rtsp://ta11.vddns.vn:554/av0_011', 15, N'cam8')
INSERT [dbo].[DB_CamURL] ([CamID], [DB_CamURL], [UID], [DB_CamName]) VALUES (4200, N'rtsp://admin:Viettel123@125.212.227.210:7650/Streaming/Channels/102', 15, N'viettel')
INSERT [dbo].[DB_CamURL] ([CamID], [DB_CamURL], [UID], [DB_CamName]) VALUES (4201, N'https://media20.cloudcam.vn/cloud-camera/demoidc1603266402290.stream/chunklist_w2045548587.m3u8', 15, N'cam9')
INSERT [dbo].[DB_CamURL] ([CamID], [DB_CamURL], [UID], [DB_CamName]) VALUES (4202, N'rtsp://ta12.vddns.vn:554/av0_0', 15, N'camtest')
INSERT [dbo].[DB_CamURL] ([CamID], [DB_CamURL], [UID], [DB_CamName]) VALUES (4203, N'https://media10.cloudcam.vn/cloud-camera/demoidc1603266947167.stream/chunklist_w510344387.m3u8', 15, N'Server')
SET IDENTITY_INSERT [dbo].[DB_CamURL] OFF
GO
/****** Object:  StoredProcedure [dbo].[UPS_DeleteAccountInfo]    Script Date: 4/18/2022 3:52:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UPS_DeleteAccountInfo]
@UId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	delete DB_Account where UID = @UId;
END
GO
/****** Object:  StoredProcedure [dbo].[UPS_DeleteALLCameraInfo]    Script Date: 4/18/2022 3:52:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UPS_DeleteALLCameraInfo]
@UId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DELETE DB_CamURL WHERE UID =  @UId;
END
GO
/****** Object:  StoredProcedure [dbo].[UPS_DeleteCameraInfo]    Script Date: 4/18/2022 3:52:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UPS_DeleteCameraInfo]
@CamId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	delete DB_CamURL where CamID = @CamId;
END
GO
/****** Object:  StoredProcedure [dbo].[USP_CamsFilter]    Script Date: 4/18/2022 3:52:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_CamsFilter]
@Uid int, @CamName varchar(100), @CamUrl varchar(200)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    -- Insert statements for procedure here
				SELECT * FROM DB_CamURL WHERE DB_CamURL.UID = @Uid
				and (DB_CamName like '%'+@CamName+'%' 
				or DB_CamURL like '%'+@CamUrl+'%') 
END
GO
/****** Object:  StoredProcedure [dbo].[USP_GetLowerAccountInfor]    Script Date: 4/18/2022 3:52:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_GetLowerAccountInfor] 
@Roles int, @UserGroupID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	BEGIN
    -- Insert statements for procedure here
	IF (@Roles = 1)
				SELECT * FROM DB_Account WHERE DB_Roles >= 1 order by DB_UserGroupID,UID; 
			ELSE
				SELECT * FROM DB_Account WHERE DB_UserGroupID = @UserGroupID and DB_Roles > @Roles;
		END
END
GO
/****** Object:  StoredProcedure [dbo].[USP_LoadAccount]    Script Date: 4/18/2022 3:52:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_LoadAccount] 
@Username nvarchar(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM DB_Account WHERE DB_UserName= @Username
END
GO
/****** Object:  StoredProcedure [dbo].[USP_LoadCam]    Script Date: 4/18/2022 3:52:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
CREATE PROCEDURE [dbo].[USP_LoadCam]
@Username nvarchar(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select DB_CamURL.CamID, DB_CamURL.DB_CamURL,DB_CamURL.UID, DB_CamURL.DB_CamName 
	from DB_CamURL,DB_Account 
	where DB_CamURL.UID = DB_Account.UID and DB_Account.DB_UserName = @Username
END
GO
/****** Object:  StoredProcedure [dbo].[USP_LoadCamUid]    Script Date: 4/18/2022 3:52:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_LoadCamUid]
@Uid int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select *
	from DB_CamURL
	where  UID = @Uid
END
GO
/****** Object:  StoredProcedure [dbo].[USP_LoadUserGroupID]    Script Date: 4/18/2022 3:52:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_LoadUserGroupID]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT  DB_UserGroupID from DB_Account group by DB_UserGroupID
END
GO
/****** Object:  StoredProcedure [dbo].[USP_Login]    Script Date: 4/18/2022 3:52:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_Login]
@userName nvarchar(50), @passWord varchar(50)
as
begin
	select * from DB_Account where DB_UserName = @userName and DB_PassWord = @passWord
end
GO
/****** Object:  StoredProcedure [dbo].[USP_SaveCameralInfo]    Script Date: 4/18/2022 3:52:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_SaveCameralInfo] 
@CamId int, @CamUrl varchar(200) , @UId int, @CamName varchar(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	BEGIN
	
	DECLARE @KtraCamInfo INT
	SELECT @KtraCamInfo = CamID FROM DB_CamURL WHERE CamID = @CamId -- coi có idBill chưa và xem có thức ăn nào như v đã có trong bill chưa
	IF (@KtraCamInfo >0)
				UPDATE DB_CamURL SET DB_CamURL = @CamUrl,DB_CamName = @CamName WHERE CamID = @CamId 
			ELSE
				INSERT INTO DB_CamURL
			VALUES (@CamUrl, @UId, @CamName)
		END
END

GO
/****** Object:  StoredProcedure [dbo].[USP_SaveUserCameralInfo]    Script Date: 4/18/2022 3:52:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_SaveUserCameralInfo] 
@CamUrl varchar(200) , @UId int, @CamName varchar(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	BEGIN
	
	DECLARE @KtraCamInfo INT
	SELECT @KtraCamInfo = Count(DB_CamName) FROM DB_CamURL WHERE DB_CamName = @CamName and UID =  @UId-- coi có idBill chưa và xem có thức ăn nào như v đã có trong bill chưa
	IF (@KtraCamInfo >0)
				UPDATE DB_CamURL SET DB_CamURL = @CamUrl WHERE DB_CamName = @CamName and UID =  @UId
			ELSE
				INSERT INTO DB_CamURL
			VALUES (@CamUrl, @UId, @CamName)
		END
END
GO
/****** Object:  StoredProcedure [dbo].[USP_SaveUserInfor]    Script Date: 4/18/2022 3:52:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_SaveUserInfor]
@Uid int, @UserName nvarchar(50) , @PassWord varchar(50), @Roles int, @UserGroupID int, @CamQuantity int, @UserQuantity int, @Wallet int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	BEGIN
	
	DECLARE @KtraUserInfo INT
	SELECT @KtraUserInfo = UID FROM DB_Account WHERE UID = @Uid -- coi có idBill chưa và xem có thức ăn nào như v đã có trong bill chưa
	IF (@KtraUserInfo >0)
				UPDATE DB_Account 
				SET DB_UserName = @UserName,DB_PassWord = @PassWord, DB_Roles = @Roles, DB_UserGroupID = @UserGroupID, DB_CamQuantity = @CamQuantity, DB_UserQuantity = @UserQuantity, DB_Wallet = @Wallet 
				WHERE UID = @Uid 
			ELSE
				INSERT INTO DB_Account
			VALUES (@UserName, @PassWord, @Roles, @UserGroupID,@CamQuantity,@UserQuantity,@Wallet)
		END
END
GO
/****** Object:  StoredProcedure [dbo].[USP_UserFilter]    Script Date: 4/18/2022 3:52:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_UserFilter]
@Roles int, @UserGroupID int, @UserName nvarchar(100), @GroupID nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	BEGIN
    -- Insert statements for procedure here
	IF (@Roles = 1)
				SELECT * FROM DB_Account WHERE DB_Roles >= 1 
				and (DB_UserName like '%'+@UserName+'%' 
				or DB_UserGroupID like '%'+@GroupID+'%')
				order by DB_UserGroupID,UID 
			ELSE
				SELECT * FROM DB_Account WHERE DB_UserGroupID = @UserGroupID and DB_Roles >= @Roles and DB_UserName like '%'+@UserName+'%' ;
		END
END
GO
USE [master]
GO
ALTER DATABASE [QuanLyCamera] SET  READ_WRITE 
GO
