USE [master]
GO
/****** Object:  Database [Nacheku]    Script Date: 19.06.2015 20:51:16 ******/
CREATE DATABASE [Nacheku]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Nacheku', FILENAME = N'F:\Материалы\Microsoft SQL ServerData\MSSQL12.MSSQLSERVER\MSSQL\DATA\Nacheku.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Nacheku_log', FILENAME = N'F:\Материалы\Microsoft SQL ServerData\MSSQL12.MSSQLSERVER\MSSQL\DATA\Nacheku_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Nacheku] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Nacheku].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Nacheku] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Nacheku] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Nacheku] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Nacheku] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Nacheku] SET ARITHABORT OFF 
GO
ALTER DATABASE [Nacheku] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Nacheku] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Nacheku] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Nacheku] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Nacheku] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Nacheku] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Nacheku] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Nacheku] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Nacheku] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Nacheku] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Nacheku] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Nacheku] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Nacheku] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Nacheku] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Nacheku] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Nacheku] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Nacheku] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Nacheku] SET RECOVERY FULL 
GO
ALTER DATABASE [Nacheku] SET  MULTI_USER 
GO
ALTER DATABASE [Nacheku] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Nacheku] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Nacheku] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Nacheku] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [Nacheku] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'Nacheku', N'ON'
GO
USE [Nacheku]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 19.06.2015 20:51:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Account](
	[Login] [nvarchar](50) NOT NULL,
	[Password] [varchar](70) NOT NULL,
	[UserId] [int] NOT NULL,
	[LastActivityDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Message]    Script Date: 19.06.2015 20:51:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Message](
	[Id] [uniqueidentifier] NOT NULL,
	[UserIdFrom] [int] NOT NULL,
	[UserIdTo] [int] NOT NULL,
	[Body] [nvarchar](50) NOT NULL,
	[TimeStamp] [datetime] NOT NULL,
	[Unread] [bit] NOT NULL,
	[Type] [tinyint] NOT NULL,
 CONSTRAINT [PK_Message] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Profile]    Script Date: 19.06.2015 20:51:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Profile](
	[UserId] [int] NOT NULL,
	[FirstName] [nvarchar](20) NOT NULL,
	[MiddleName] [nvarchar](20) NULL,
	[LastName] [nvarchar](20) NOT NULL,
	[BirthDay] [datetime] NOT NULL,
	[Location] [nvarchar](50) NULL,
	[AvatarId] [uniqueidentifier] NULL,
	[Skill] [nvarchar](50) NULL,
 CONSTRAINT [PK_Profile] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Relationships]    Script Date: 19.06.2015 20:51:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Relationships](
	[FromUserId] [int] NOT NULL,
	[ToUserId] [int] NOT NULL,
	[Relation] [tinyint] NOT NULL,
	[Confirmed] [bit] NOT NULL,
 CONSTRAINT [PK_Relationships] PRIMARY KEY CLUSTERED 
(
	[FromUserId] ASC,
	[ToUserId] ASC,
	[Relation] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Roles]    Script Date: 19.06.2015 20:51:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [tinyint] NOT NULL,
	[Role] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
INSERT [dbo].[Account] ([Login], [Password], [UserId], [LastActivityDate]) VALUES (N'ma3471@mail.ru', N'1000:BYlwYMxDlhQIjdRM0HBTOCtMdqd/UEZ0:Rg3WtslB1Xf0sFlxB21+NFKkE28vmFxF', 666736839, CAST(N'2015-06-19 20:27:43.360' AS DateTime))
INSERT [dbo].[Profile] ([UserId], [FirstName], [MiddleName], [LastName], [BirthDay], [Location], [AvatarId], [Skill]) VALUES (666736839, N'мсм', N'ывы', N'ывыв', CAST(N'2007-02-01 00:00:00.000' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Roles] ([Id], [Role]) VALUES (1, N'Admin')
INSERT [dbo].[Roles] ([Id], [Role]) VALUES (2, N'User')
/****** Object:  Index [IX_Profile]    Script Date: 19.06.2015 20:51:17 ******/
CREATE NONCLUSTERED INDEX [IX_Profile] ON [dbo].[Profile]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[AddAccount]    Script Date: 19.06.2015 20:51:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AddAccount]
	-- Add the parameters for the stored procedure here
	@Login nvarchar(50),
	@Password varchar(70),
	@UserId int,
	@LastActivityDate datetime
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	--SET NOCOUNT ON;

    INSERT into dbo.Account
	(Login, Password, UserId, LastActivityDate)
	VALUES
	(@Login, @Password, @UserId, @LastActivityDate)
END

GO
/****** Object:  StoredProcedure [dbo].[AddProfile]    Script Date: 19.06.2015 20:51:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AddProfile] 
	-- Add the parameters for the stored procedure here
	@UserId int,
	@FirstName nvarchar(20),
	@MiddleName nvarchar(20),
	@LastName nvarchar(20),
	@BirthDay datetime,
	@Location nvarchar(50),
	@AvatarId uniqueidentifier,
	@Skill nvarchar(50)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	--SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT into dbo.[Profile] 
	(UserId, FirstName, MiddleName, LastName, BirthDay, Location, AvatarId, Skill) 
	VALUES 
	(@UserId, @FirstName, @MiddleName, @LastName, @BirthDay, @Location, @AvatarId, @Skill)
END

GO
/****** Object:  StoredProcedure [dbo].[ChangeLogin]    Script Date: 19.06.2015 20:51:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ChangeLogin] 
	-- Add the parameters for the stored procedure here
	@Login nvarchar,
	@UserId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	--SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE dbo.[Account] SET Login=@Login WHERE UserId = @UserId
END

GO
/****** Object:  StoredProcedure [dbo].[GetAccountById]    Script Date: 19.06.2015 20:51:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetAccountById] 
	-- Add the parameters for the stored procedure here
	@UserId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	--SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT Login, Password, LastActivityDate  FROM dbo.[Account] WHERE UserId = @UserId
END

GO
/****** Object:  StoredProcedure [dbo].[GetAccountByLogin]    Script Date: 19.06.2015 20:51:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetAccountByLogin] 
	-- Add the parameters for the stored procedure here
	@Login nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	--SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT Password, UserId, LastActivityDate  FROM dbo.[Account] WHERE Login = @Login
END

GO
/****** Object:  StoredProcedure [dbo].[GetProfileById]    Script Date: 19.06.2015 20:51:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetProfileById] 
	-- Add the parameters for the stored procedure here
	@UserId int
	--@FirstName nvarchar(20),
	--@MiddleName nvarchar(20),
	--@LastName nvarchar(20),
	--@BirthDay datetime,
	--@Location nvarchar(50),
	--@AvatarId uniqueidentifier,
	--@Skill nvarchar(50)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	--SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT FirstName, MiddleName, LastName, BirthDay, Location, AvatarId, Skill From dbo.[Profile] 
	
	WHERE UserId = @UserId 
	--(@UserId, @FirstName, @MiddleName, @LastName, @BirthDay, @Location, @AvatarId, @Skill)
END

GO
/****** Object:  StoredProcedure [dbo].[UpdateLastActivityDate]    Script Date: 19.06.2015 20:51:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateLastActivityDate]
	-- Add the parameters for the stored procedure here
	@Login nvarchar(50),
	@LastActivityDate datetime
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE [dbo].[Account] SET LastActivityDate=@LastActivityDate WHERE Login = @Login
END

GO
/****** Object:  StoredProcedure [dbo].[UpdatePassword]    Script Date: 19.06.2015 20:51:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Batch submitted through debugger: SQLQuery26.sql|7|0|C:\TEMP\~vsE065.sql
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdatePassword] 
	-- Add the parameters for the stored procedure here
	@Login nvarchar(50),
	@Password varchar(70)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	--SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE [dbo].[Account] SET Password=@Password WHERE Login = @Login
END

GO
/****** Object:  StoredProcedure [dbo].[UpdateProfile]    Script Date: 19.06.2015 20:51:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateProfile] 
	-- Add the parameters for the stored procedure here
	@UserId int,
	@FirstName nvarchar(20),
	@MiddleName nvarchar(20),
	@LastName nvarchar(20),
	@BirthDay datetime,
	@Location nvarchar(50),
	@AvatarId uniqueidentifier,
	@Skill nvarchar(50)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	--SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE dbo.[Profile] SET FirstName=@FirstName, MiddleName=@MiddleName, LastName=@LastName, BirthDay=@BirthDay, Location=@Location, AvatarId=@AvatarId, Skill=@Skill WHERE UserId = @UserId
END

GO
USE [master]
GO
ALTER DATABASE [Nacheku] SET  READ_WRITE 
GO
