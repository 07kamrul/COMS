USE [master]
GO
/****** Object:  Database [COMS]    Script Date: 9/22/2022 3:39:49 PM ******/
CREATE DATABASE [COMS]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'COMS', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVERS\MSSQL\DATA\COMS.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'COMS_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVERS\MSSQL\DATA\COMS_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [COMS] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [COMS].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [COMS] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [COMS] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [COMS] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [COMS] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [COMS] SET ARITHABORT OFF 
GO
ALTER DATABASE [COMS] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [COMS] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [COMS] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [COMS] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [COMS] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [COMS] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [COMS] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [COMS] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [COMS] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [COMS] SET  DISABLE_BROKER 
GO
ALTER DATABASE [COMS] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [COMS] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [COMS] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [COMS] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [COMS] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [COMS] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [COMS] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [COMS] SET RECOVERY FULL 
GO
ALTER DATABASE [COMS] SET  MULTI_USER 
GO
ALTER DATABASE [COMS] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [COMS] SET DB_CHAINING OFF 
GO
ALTER DATABASE [COMS] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [COMS] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [COMS] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [COMS] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'COMS', N'ON'
GO
ALTER DATABASE [COMS] SET QUERY_STORE = OFF
GO
USE [COMS]
GO
/****** Object:  Table [dbo].[Accounts]    Script Date: 9/22/2022 3:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accounts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AccountName] [nvarchar](50) NOT NULL,
	[MemberId] [int] NOT NULL,
	[ProjectId] [int] NOT NULL,
	[OpaningDate] [date] NOT NULL,
	[ClosingDate] [date] NULL,
	[DueAmounts] [numeric](18, 2) NULL,
	[PayableAmounts] [numeric](18, 2) NOT NULL,
	[TotalAmounts] [numeric](18, 2) NULL,
	[IsVerified] [bit] NOT NULL,
	[VerifiedBy] [int] NULL,
	[VerificationDate] [date] NULL,
	[CreatedBy] [int] NOT NULL,
	[CreationDate] [date] NOT NULL,
	[ModifiedBy] [int] NULL,
	[ModificationDate] [date] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Accounts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Attachments]    Script Date: 9/22/2022 3:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Attachments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MemberId] [int] NOT NULL,
	[AttachmentTypeId] [int] NOT NULL,
	[FileName] [nvarchar](250) NOT NULL,
	[FileGUID] [nvarchar](50) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreationDate] [date] NOT NULL,
	[ModifiedBy] [int] NULL,
	[ModificationDate] [date] NULL,
 CONSTRAINT [PK_Attachment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AttachmentTypes]    Script Date: 9/22/2022 3:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AttachmentTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](250) NOT NULL,
	[IsActive] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreationDate] [date] NOT NULL,
	[ModifiedBy] [int] NULL,
	[ModificationDate] [date] NULL,
 CONSTRAINT [PK_AttachmentType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AuditLogs]    Script Date: 9/22/2022 3:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AuditLogs](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserID] [nvarchar](100) NOT NULL,
	[EventDateUTC] [datetime2](7) NOT NULL,
	[EventType] [varchar](1) NOT NULL,
	[TableName] [nvarchar](100) NOT NULL,
	[RecordID] [nvarchar](100) NOT NULL,
	[ColumnName] [nvarchar](100) NOT NULL,
	[OriginalValue] [text] NULL,
	[NewValue] [text] NOT NULL,
 CONSTRAINT [PK_AuditLogs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Members]    Script Date: 9/22/2022 3:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Members](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[FatherName] [nvarchar](50) NULL,
	[MotherName] [nvarchar](50) NULL,
	[JoiningDate] [date] NOT NULL,
	[DateOfBirth] [date] NOT NULL,
	[Nid] [bigint] NOT NULL,
	[Phone] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Gender] [nvarchar](50) NOT NULL,
	[Nationality] [nvarchar](50) NOT NULL,
	[ProjectId] [int] NULL,
	[NumberOfAccount] [int] NULL,
	[TotalAmounts] [numeric](18, 2) NULL,
	[PresentAddress] [nvarchar](250) NULL,
	[PermanentAddress] [nvarchar](250) NULL,
	[MaritalStatus] [nvarchar](50) NULL,
	[Religion] [nvarchar](50) NULL,
	[Occupation] [nvarchar](50) NULL,
	[Designation] [nvarchar](50) NULL,
	[Company] [nvarchar](50) NULL,
	[CreatedBy] [int] NOT NULL,
	[CreationDate] [date] NOT NULL,
	[ModifiedBy] [int] NULL,
	[ModificationDate] [date] NULL,
	[IsVerified] [bit] NOT NULL,
	[VerifiedBy] [int] NULL,
	[VerificationDate] [date] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Members] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Projects]    Script Date: 9/22/2022 3:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Projects](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProjectName] [nvarchar](100) NOT NULL,
	[ProjectSize] [nvarchar](100) NOT NULL,
	[Location] [nvarchar](100) NOT NULL,
	[NumberOfShare] [int] NOT NULL,
	[TotalCost] [int] NOT NULL,
	[StartDate] [date] NOT NULL,
	[EndDate] [date] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreationDate] [date] NOT NULL,
	[ModifiedBy] [int] NULL,
	[ModificationDate] [date] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Projects] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 9/22/2022 3:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [int] NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreationDate] [date] NOT NULL,
	[ModifiedBy] [int] NULL,
	[ModificationDate] [date] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoleUser]    Script Date: 9/22/2022 3:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleUser](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreationDate] [date] NOT NULL,
	[ModifiedBy] [int] NULL,
	[ModificationDate] [date] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_RoleUser] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transactions]    Script Date: 9/22/2022 3:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transactions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MemberId] [int] NOT NULL,
	[ProjectId] [int] NOT NULL,
	[AccountId] [int] NOT NULL,
	[InstallmentNo] [int] NOT NULL,
	[DueAmounts] [numeric](18, 2) NOT NULL,
	[PayableAmounts] [numeric](18, 2) NOT NULL,
	[TransactionAmounts] [numeric](18, 2) NOT NULL,
	[TransactionType] [int] NOT NULL,
	[TransactionDate] [date] NOT NULL,
	[IsVerified] [bit] NOT NULL,
	[VerifiedBy] [int] NULL,
	[VerificationDate] [date] NULL,
	[CreatedBy] [int] NOT NULL,
	[CreationDate] [date] NOT NULL,
	[ModifiedBy] [int] NULL,
	[ModificationDate] [date] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Transactions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 9/22/2022 3:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](200) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NULL,
	[Phone] [nvarchar](20) NULL,
	[Password] [nvarchar](50) NULL,
	[RefreshToken] [nvarchar](100) NULL,
	[MemberId] [int] NULL,
	[IsActive] [bit] NOT NULL,
	[CreationDate] [date] NOT NULL,
	[ModificationDate] [date] NULL,
	[CreatedBy] [int] NOT NULL,
	[ModifiedBy] [int] NULL,
	[GroupId] [int] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Accounts] ADD  CONSTRAINT [DF_Accounts_CreatedDate]  DEFAULT (getdate()) FOR [CreationDate]
GO
ALTER TABLE [dbo].[Accounts] ADD  CONSTRAINT [DF_Accounts_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Attachments] ADD  CONSTRAINT [DF_Attachment_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Attachments] ADD  CONSTRAINT [DF_Attachment_CreationDate]  DEFAULT (getdate()) FOR [CreationDate]
GO
ALTER TABLE [dbo].[AttachmentTypes] ADD  CONSTRAINT [DF_AttachmentType_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[AttachmentTypes] ADD  CONSTRAINT [DF_AttachmentType_CreationDate]  DEFAULT (getdate()) FOR [CreationDate]
GO
ALTER TABLE [dbo].[Projects] ADD  CONSTRAINT [DF_Projects_CreatedDate]  DEFAULT (getdate()) FOR [CreationDate]
GO
ALTER TABLE [dbo].[Projects] ADD  CONSTRAINT [DF_Projects_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Roles] ADD  CONSTRAINT [DF_Role_CreatedDate]  DEFAULT (getdate()) FOR [CreationDate]
GO
ALTER TABLE [dbo].[Roles] ADD  CONSTRAINT [DF_Role_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[RoleUser] ADD  CONSTRAINT [DF_RoleUser_CreatedDate]  DEFAULT (getdate()) FOR [CreationDate]
GO
ALTER TABLE [dbo].[RoleUser] ADD  CONSTRAINT [DF_RoleUser_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Transactions] ADD  CONSTRAINT [DF_Transactions_CreatedDate]  DEFAULT (getdate()) FOR [CreationDate]
GO
ALTER TABLE [dbo].[Transactions] ADD  CONSTRAINT [DF_Transactions_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_CreationDate]  DEFAULT (getdate()) FOR [CreationDate]
GO
USE [master]
GO
ALTER DATABASE [COMS] SET  READ_WRITE 
GO
