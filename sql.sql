USE [master]
GO
/****** Object:  Database [WorkManagement]    Script Date: 3/13/2020 11:58:12 AM ******/
CREATE DATABASE [WorkManagement]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'WorkManagement', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\WorkManagement.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'WorkManagement_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\WorkManagement_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [WorkManagement] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [WorkManagement].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [WorkManagement] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [WorkManagement] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [WorkManagement] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [WorkManagement] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [WorkManagement] SET ARITHABORT OFF 
GO
ALTER DATABASE [WorkManagement] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [WorkManagement] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [WorkManagement] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [WorkManagement] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [WorkManagement] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [WorkManagement] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [WorkManagement] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [WorkManagement] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [WorkManagement] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [WorkManagement] SET  DISABLE_BROKER 
GO
ALTER DATABASE [WorkManagement] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [WorkManagement] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [WorkManagement] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [WorkManagement] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [WorkManagement] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [WorkManagement] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [WorkManagement] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [WorkManagement] SET RECOVERY FULL 
GO
ALTER DATABASE [WorkManagement] SET  MULTI_USER 
GO
ALTER DATABASE [WorkManagement] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [WorkManagement] SET DB_CHAINING OFF 
GO
ALTER DATABASE [WorkManagement] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [WorkManagement] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [WorkManagement] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'WorkManagement', N'ON'
GO
ALTER DATABASE [WorkManagement] SET QUERY_STORE = OFF
GO
USE [WorkManagement]
GO
/****** Object:  Table [dbo].[Groups]    Script Date: 3/13/2020 11:58:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Groups](
	[GroupID] [int] IDENTITY(1,1) NOT NULL,
	[GroupName] [varchar](50) NULL,
 CONSTRAINT [PK_Groups] PRIMARY KEY CLUSTERED 
(
	[GroupID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 3/13/2020 11:58:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[RoleID] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [varchar](50) NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Status]    Script Date: 3/13/2020 11:58:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Status](
	[StatusID] [int] IDENTITY(1,1) NOT NULL,
	[StatusName] [varchar](15) NULL,
 CONSTRAINT [PK_Status] PRIMARY KEY CLUSTERED 
(
	[StatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tasks]    Script Date: 3/13/2020 11:58:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tasks](
	[TaskID] [int] IDENTITY(1,1) NOT NULL,
	[TaskName] [varchar](50) NULL,
	[CloneFrom] [int] NULL,
	[ContentAssigned] [varchar](200) NULL,
	[ContentHandlingWork] [varchar](200) NULL,
	[Description] [varchar](200) NULL,
	[Mark] [int] NULL,
	[TimeManagerCommented] [datetime] NULL,
	[TimeStart] [datetime] NULL,
	[TimeEnd] [datetime] NULL,
	[DueDate] [datetime] NULL,
	[Status] [int] NULL,
	[CreationTime] [datetime] NULL,
	[Creator] [int] NULL,
	[Processor] [int] NOT NULL,
	[Acceptance] [bit] NULL,
	[ConfirmationEnded] [bit] NULL,
	[ImageConfirmation] [varchar](100) NULL,
	[Updater] [int] NULL,
	[TimeUpdated] [datetime] NULL,
 CONSTRAINT [PK_Tasks] PRIMARY KEY CLUSTERED 
(
	[TaskID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 3/13/2020 11:58:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](50) NOT NULL,
	[Password] [varchar](50) NULL,
	[RoleID] [int] NULL,
	[Fullname] [varchar](20) NULL,
	[Address] [varchar](100) NULL,
	[Phone] [varchar](10) NULL,
	[Email] [varchar](20) NULL,
	[QRCode] [varchar](100) NULL,
	[DoB] [date] NULL,
	[GroupID] [int] NULL,
	[Active] [bit] NULL,
 CONSTRAINT [PK_Users_1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([RoleID], [RoleName]) VALUES (1, N'Admin')
INSERT [dbo].[Roles] ([RoleID], [RoleName]) VALUES (2, N'Manager')
INSERT [dbo].[Roles] ([RoleID], [RoleName]) VALUES (3, N'Employee')
SET IDENTITY_INSERT [dbo].[Roles] OFF
SET IDENTITY_INSERT [dbo].[Status] ON 

INSERT [dbo].[Status] ([StatusID], [StatusName]) VALUES (1, N'Unstarted')
INSERT [dbo].[Status] ([StatusID], [StatusName]) VALUES (2, N'Processing')
INSERT [dbo].[Status] ([StatusID], [StatusName]) VALUES (3, N'Finished')
INSERT [dbo].[Status] ([StatusID], [StatusName]) VALUES (4, N'Overdue')
INSERT [dbo].[Status] ([StatusID], [StatusName]) VALUES (5, N'Unachievable')
SET IDENTITY_INSERT [dbo].[Status] OFF
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([ID], [Username], [Password], [RoleID], [Fullname], [Address], [Phone], [Email], [QRCode], [DoB], [GroupID], [Active]) VALUES (1, N'admin', N'21232f297a57a5a743894a0e4a801fc3', 1, N'Hoang', N'aaaa', N'3241', N'1234', N'1324', NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Users] OFF
ALTER TABLE [dbo].[Tasks]  WITH CHECK ADD  CONSTRAINT [FK_Tasks_Status] FOREIGN KEY([Status])
REFERENCES [dbo].[Status] ([StatusID])
GO
ALTER TABLE [dbo].[Tasks] CHECK CONSTRAINT [FK_Tasks_Status]
GO
ALTER TABLE [dbo].[Tasks]  WITH CHECK ADD  CONSTRAINT [FK_Tasks_Users] FOREIGN KEY([Creator])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Tasks] CHECK CONSTRAINT [FK_Tasks_Users]
GO
ALTER TABLE [dbo].[Tasks]  WITH CHECK ADD  CONSTRAINT [FK_Tasks_Users1] FOREIGN KEY([Processor])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Tasks] CHECK CONSTRAINT [FK_Tasks_Users1]
GO
ALTER TABLE [dbo].[Tasks]  WITH CHECK ADD  CONSTRAINT [FK_Tasks_Users2] FOREIGN KEY([Updater])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Tasks] CHECK CONSTRAINT [FK_Tasks_Users2]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Groups] FOREIGN KEY([GroupID])
REFERENCES [dbo].[Groups] ([GroupID])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Groups]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Roles] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Roles] ([RoleID])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Roles]
GO
USE [master]
GO
ALTER DATABASE [WorkManagement] SET  READ_WRITE 
GO
