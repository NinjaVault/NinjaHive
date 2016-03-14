--!!WARNING!! When running this script, edit the PATH for the .mdf and .ldf files!
--NOTE: Don't forget to undo your edit to prevent a commit to source control!

USE [master]
GO
/****** Object:  Database [NinjaHive]    Script Date: 1/9/2016 11:42:14 AM ******/
CREATE DATABASE [NinjaHive]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'NinjaHive', FILENAME = N'C:\Users\[USERNAME]\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB\NinjaHive.mdf' , SIZE = 3136KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'NinjaHive_log', FILENAME = N'C:\Users\[USERNAME]\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB\NinjaHive_log.ldf' , SIZE = 784KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [NinjaHive] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [NinjaHive].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [NinjaHive] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [NinjaHive] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [NinjaHive] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [NinjaHive] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [NinjaHive] SET ARITHABORT OFF 
GO
ALTER DATABASE [NinjaHive] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [NinjaHive] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [NinjaHive] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [NinjaHive] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [NinjaHive] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [NinjaHive] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [NinjaHive] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [NinjaHive] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [NinjaHive] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [NinjaHive] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [NinjaHive] SET  ENABLE_BROKER 
GO
ALTER DATABASE [NinjaHive] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [NinjaHive] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [NinjaHive] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [NinjaHive] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [NinjaHive] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [NinjaHive] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [NinjaHive] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [NinjaHive] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [NinjaHive] SET  MULTI_USER 
GO
ALTER DATABASE [NinjaHive] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [NinjaHive] SET DB_CHAINING OFF 
GO
ALTER DATABASE [NinjaHive] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [NinjaHive] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [NinjaHive]
GO
/****** Object:  Table [dbo].[Classes]    Script Date: 1/9/2016 11:42:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Classes](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](255) NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](255) NOT NULL,
	[EditedOn] [datetime] NOT NULL,
	[EditedBy] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_Classes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ_Classes_Name] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EquipmentItems]    Script Date: 1/9/2016 11:42:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EquipmentItems](
	[Id] [uniqueidentifier] NOT NULL,
	[Durability] [int] NOT NULL,
	[NumberOfSlots] [int] NOT NULL,
	[BodySlot] [int] NOT NULL,
 CONSTRAINT [PK_EquipmentItems] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EquipmentItemsClassesAssociation]    Script Date: 1/9/2016 11:42:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EquipmentItemsClassesAssociation](
	[FKEquipmentItemsId] [uniqueidentifier] NOT NULL,
	[FKClassesId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_EquipmentItems_Classes] PRIMARY KEY CLUSTERED 
(
	[FKEquipmentItemsId] ASC,
	[FKClassesId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EquipmentItemsEnhancersAssociation]    Script Date: 1/9/2016 11:42:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EquipmentItemsEnhancersAssociation](
	[FKEquipmentItemsId] [uniqueidentifier] NOT NULL,
	[FKEnhancersId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_EquipmentItems_Enhancers] PRIMARY KEY CLUSTERED 
(
	[FKEquipmentItemsId] ASC,
	[FKEnhancersId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EquipmentItemsRacesAssociation]    Script Date: 1/9/2016 11:42:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EquipmentItemsRacesAssociation](
	[FKEquipmentItemsId] [uniqueidentifier] NOT NULL,
	[FKRacesId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_EquipmentItems_Races] PRIMARY KEY CLUSTERED 
(
	[FKEquipmentItemsId] ASC,
	[FKRacesId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Errors]    Script Date: 1/9/2016 11:42:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Errors](
	[Id] [uniqueidentifier] NOT NULL,
	[Message] [nvarchar](max) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_Errors] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[GameItems]    Script Date: 1/9/2016 11:42:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GameItems](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](255) NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Craftable] [bit] NOT NULL,
	[IsUpgrader] [bit] NOT NULL,
	[IsCrafter] [bit] NOT NULL,
	[IsQuestItem] [bit] NOT NULL,
	[Value] [int] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](255) NOT NULL,
	[EditedOn] [datetime] NOT NULL,
	[EditedBy] [nvarchar](255) NOT NULL,
	[Category] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_GameItems] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ_GameItems_Name] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Levels]    Script Date: 1/9/2016 11:42:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Levels](
	[Id] [uniqueidentifier] NOT NULL,
	[Level] [int] NOT NULL,
	[Threshold] [int] NOT NULL,
	[FKTiersId] [uniqueidentifier] NOT NULL,
	[FKStatInfoId] [uniqueidentifier] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](255) NOT NULL,
	[EditedOn] [datetime] NOT NULL,
	[EditedBy] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_Levels] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MainCategories]    Script Date: 1/9/2016 11:42:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MainCategories](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](255) NOT NULL,
	[EditedOn] [datetime] NOT NULL,
	[EditedBy] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_MainCategory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MiscItems]    Script Date: 1/9/2016 11:42:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MiscItems](
	[Id] [uniqueidentifier] NOT NULL,
	[FKSkillsId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_MiscItems] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[OtherItems]    Script Date: 1/9/2016 11:42:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OtherItems](
	[IsEnhancer] [bit] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[FKStatInfoId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_OtherItems] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[OtherItemsCraftingItemsAssociation]    Script Date: 1/9/2016 11:42:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OtherItemsCraftingItemsAssociation](
	[FKOtherItemsId] [uniqueidentifier] NOT NULL,
	[FKCraftingIngredientsId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_OtherItems_CraftingItems] PRIMARY KEY CLUSTERED 
(
	[FKOtherItemsId] ASC,
	[FKCraftingIngredientsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Races]    Script Date: 1/9/2016 11:42:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Races](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](255) NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](255) NOT NULL,
	[EditedOn] [datetime] NOT NULL,
	[EditedBy] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_Races] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ_Races_Name] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Skills]    Script Date: 1/9/2016 11:42:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Skills](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](255) NULL,
	[Range] [int] NOT NULL,
	[Radius] [int] NOT NULL,
	[Targets] [int] NOT NULL,
	[Target] [int] NOT NULL,
	[Friendly] [bit] NOT NULL,
	[FKStatInfoId] [uniqueidentifier] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](255) NOT NULL,
	[EditedOn] [datetime] NOT NULL,
	[EditedBy] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_Skills] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ_Skills_Name] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SkillsSpecialsAssociation]    Script Date: 1/9/2016 11:42:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SkillsSpecialsAssociation](
	[FKSkillsId] [uniqueidentifier] NOT NULL,
	[FKSpecialsId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Skills_Specials] PRIMARY KEY CLUSTERED 
(
	[FKSkillsId] ASC,
	[FKSpecialsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Specials]    Script Date: 1/9/2016 11:42:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Specials](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](255) NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](255) NOT NULL,
	[EditedOn] [datetime] NOT NULL,
	[EditedBy] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_Specials] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ_Specials_Name] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StatInfo]    Script Date: 1/9/2016 11:42:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StatInfo](
	[Id] [uniqueidentifier] NOT NULL,
	[Health] [int] NOT NULL,
	[Magic] [int] NOT NULL,
	[Attack] [int] NOT NULL,
	[Defense] [int] NOT NULL,
	[Stamina] [int] NOT NULL,
	[Hunger] [int] NOT NULL,
	[Agility] [int] NOT NULL,
	[Intelligence] [int] NOT NULL,
	[Resistance] [float] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](255) NOT NULL,
	[EditedOn] [datetime] NOT NULL,
	[EditedBy] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_StatInfo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SubCategories]    Script Date: 1/9/2016 11:42:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubCategories](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](255) NOT NULL,
	[EditedOn] [datetime] NOT NULL,
	[EditedBy] [nvarchar](255) NOT NULL,
	[MainCategory] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_SubCategories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Tiers]    Script Date: 1/9/2016 11:42:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tiers](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Tier] [int] NOT NULL,
	[FKEquipmentItemsId] [uniqueidentifier] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](255) NOT NULL,
	[EditedOn] [datetime] NOT NULL,
	[EditedBy] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_Tiers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TiersCraftingItemsAssociation]    Script Date: 1/9/2016 11:42:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TiersCraftingItemsAssociation](
	[FKTiersId] [uniqueidentifier] NOT NULL,
	[FKCraftingIngredientsId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Tiers_CraftingItems] PRIMARY KEY CLUSTERED 
(
	[FKTiersId] ASC,
	[FKCraftingIngredientsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TiersSkillsAssociation]    Script Date: 1/9/2016 11:42:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TiersSkillsAssociation](
	[FKTiersId] [uniqueidentifier] NOT NULL,
	[FKSkillsId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Tiers_Skills] PRIMARY KEY CLUSTERED 
(
	[FKTiersId] ASC,
	[FKSkillsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TiersUpgradeItemsAssociation]    Script Date: 1/9/2016 11:42:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TiersUpgradeItemsAssociation](
	[FKTiersId] [uniqueidentifier] NOT NULL,
	[FKUpgradeIngredientsId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Tiers_UpgradeItems] PRIMARY KEY CLUSTERED 
(
	[FKTiersId] ASC,
	[FKUpgradeIngredientsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Index [IX_FK_EquipmentItemsClassesAssociation_Classes]    Script Date: 1/9/2016 11:42:14 AM ******/
CREATE NONCLUSTERED INDEX [IX_FK_EquipmentItemsClassesAssociation_Classes] ON [dbo].[EquipmentItemsClassesAssociation]
(
	[FKClassesId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FK_EquipmentItemsEnhancersAssociation_Enhancers]    Script Date: 1/9/2016 11:42:14 AM ******/
CREATE NONCLUSTERED INDEX [IX_FK_EquipmentItemsEnhancersAssociation_Enhancers] ON [dbo].[EquipmentItemsEnhancersAssociation]
(
	[FKEnhancersId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FK_EquipmentItemsRacesAssociation_Races]    Script Date: 1/9/2016 11:42:14 AM ******/
CREATE NONCLUSTERED INDEX [IX_FK_EquipmentItemsRacesAssociation_Races] ON [dbo].[EquipmentItemsRacesAssociation]
(
	[FKRacesId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FK_Level_StatInfo]    Script Date: 1/9/2016 11:42:14 AM ******/
CREATE NONCLUSTERED INDEX [IX_FK_Level_StatInfo] ON [dbo].[Levels]
(
	[FKStatInfoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FK_Tiers_Levels]    Script Date: 1/9/2016 11:42:14 AM ******/
CREATE NONCLUSTERED INDEX [IX_FK_Tiers_Levels] ON [dbo].[Levels]
(
	[FKTiersId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FK_MiscItems_Skills]    Script Date: 1/9/2016 11:42:14 AM ******/
CREATE NONCLUSTERED INDEX [IX_FK_MiscItems_Skills] ON [dbo].[MiscItems]
(
	[FKSkillsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FK_OtherItems_StatInfo]    Script Date: 1/9/2016 11:42:14 AM ******/
CREATE NONCLUSTERED INDEX [IX_FK_OtherItems_StatInfo] ON [dbo].[OtherItems]
(
	[FKStatInfoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FK_OtherItemsCraftingItemsAssociation_CraftingIngredients]    Script Date: 1/9/2016 11:42:14 AM ******/
CREATE NONCLUSTERED INDEX [IX_FK_OtherItemsCraftingItemsAssociation_CraftingIngredients] ON [dbo].[OtherItemsCraftingItemsAssociation]
(
	[FKCraftingIngredientsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FK_Skills_StatInfo]    Script Date: 1/9/2016 11:42:14 AM ******/
CREATE NONCLUSTERED INDEX [IX_FK_Skills_StatInfo] ON [dbo].[Skills]
(
	[FKStatInfoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FK_SkillsSpecialsAssociation_Specials]    Script Date: 1/9/2016 11:42:14 AM ******/
CREATE NONCLUSTERED INDEX [IX_FK_SkillsSpecialsAssociation_Specials] ON [dbo].[SkillsSpecialsAssociation]
(
	[FKSpecialsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FK_EquipmentItems_Tiers]    Script Date: 1/9/2016 11:42:14 AM ******/
CREATE NONCLUSTERED INDEX [IX_FK_EquipmentItems_Tiers] ON [dbo].[Tiers]
(
	[FKEquipmentItemsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FK_TiersCraftingItemsAssociation_CraftingIngredients]    Script Date: 1/9/2016 11:42:14 AM ******/
CREATE NONCLUSTERED INDEX [IX_FK_TiersCraftingItemsAssociation_CraftingIngredients] ON [dbo].[TiersCraftingItemsAssociation]
(
	[FKCraftingIngredientsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FK_TiersSkillsAssociation_Skills]    Script Date: 1/9/2016 11:42:14 AM ******/
CREATE NONCLUSTERED INDEX [IX_FK_TiersSkillsAssociation_Skills] ON [dbo].[TiersSkillsAssociation]
(
	[FKSkillsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FK_TiersUpgradeItemsAssociation_UpgradeIngredients]    Script Date: 1/9/2016 11:42:14 AM ******/
CREATE NONCLUSTERED INDEX [IX_FK_TiersUpgradeItemsAssociation_UpgradeIngredients] ON [dbo].[TiersUpgradeItemsAssociation]
(
	[FKUpgradeIngredientsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Classes] ADD  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[Errors] ADD  CONSTRAINT [DF_Errors_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[GameItems] ADD  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[Levels] ADD  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[MainCategories] ADD  CONSTRAINT [DF_MainCategory_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[Races] ADD  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[Skills] ADD  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[Specials] ADD  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[StatInfo] ADD  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[SubCategories] ADD  CONSTRAINT [DF_SubCategories_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[Tiers] ADD  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[EquipmentItems]  WITH CHECK ADD  CONSTRAINT [FK_EquipmentItems_GameItems] FOREIGN KEY([Id])
REFERENCES [dbo].[GameItems] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[EquipmentItems] CHECK CONSTRAINT [FK_EquipmentItems_GameItems]
GO
ALTER TABLE [dbo].[EquipmentItemsClassesAssociation]  WITH CHECK ADD  CONSTRAINT [FK_EquipmentItemsClassesAssociation_Classes] FOREIGN KEY([FKClassesId])
REFERENCES [dbo].[Classes] ([Id])
GO
ALTER TABLE [dbo].[EquipmentItemsClassesAssociation] CHECK CONSTRAINT [FK_EquipmentItemsClassesAssociation_Classes]
GO
ALTER TABLE [dbo].[EquipmentItemsClassesAssociation]  WITH CHECK ADD  CONSTRAINT [FK_EquipmentItemsClassesAssociation_EquipmentItems] FOREIGN KEY([FKEquipmentItemsId])
REFERENCES [dbo].[EquipmentItems] ([Id])
GO
ALTER TABLE [dbo].[EquipmentItemsClassesAssociation] CHECK CONSTRAINT [FK_EquipmentItemsClassesAssociation_EquipmentItems]
GO
ALTER TABLE [dbo].[EquipmentItemsEnhancersAssociation]  WITH CHECK ADD  CONSTRAINT [FK_EquipmentItemsEnhancersAssociation_Enhancers] FOREIGN KEY([FKEnhancersId])
REFERENCES [dbo].[OtherItems] ([Id])
GO
ALTER TABLE [dbo].[EquipmentItemsEnhancersAssociation] CHECK CONSTRAINT [FK_EquipmentItemsEnhancersAssociation_Enhancers]
GO
ALTER TABLE [dbo].[EquipmentItemsEnhancersAssociation]  WITH CHECK ADD  CONSTRAINT [FK_EquipmentItemsEnhancersAssociation_EquipmentItems] FOREIGN KEY([FKEquipmentItemsId])
REFERENCES [dbo].[EquipmentItems] ([Id])
GO
ALTER TABLE [dbo].[EquipmentItemsEnhancersAssociation] CHECK CONSTRAINT [FK_EquipmentItemsEnhancersAssociation_EquipmentItems]
GO
ALTER TABLE [dbo].[EquipmentItemsRacesAssociation]  WITH CHECK ADD  CONSTRAINT [FK_EquipmentItemsRacesAssociation_EquipmentItems] FOREIGN KEY([FKEquipmentItemsId])
REFERENCES [dbo].[EquipmentItems] ([Id])
GO
ALTER TABLE [dbo].[EquipmentItemsRacesAssociation] CHECK CONSTRAINT [FK_EquipmentItemsRacesAssociation_EquipmentItems]
GO
ALTER TABLE [dbo].[EquipmentItemsRacesAssociation]  WITH CHECK ADD  CONSTRAINT [FK_EquipmentItemsRacesAssociation_Races] FOREIGN KEY([FKRacesId])
REFERENCES [dbo].[Races] ([Id])
GO
ALTER TABLE [dbo].[EquipmentItemsRacesAssociation] CHECK CONSTRAINT [FK_EquipmentItemsRacesAssociation_Races]
GO
ALTER TABLE [dbo].[GameItems]  WITH CHECK ADD  CONSTRAINT [FK_GameItems_Category] FOREIGN KEY([Category])
REFERENCES [dbo].[SubCategories] ([Id])
GO
ALTER TABLE [dbo].[GameItems] CHECK CONSTRAINT [FK_GameItems_Category]
GO
ALTER TABLE [dbo].[Levels]  WITH CHECK ADD  CONSTRAINT [FK_Level_StatInfo] FOREIGN KEY([FKStatInfoId])
REFERENCES [dbo].[StatInfo] ([Id])
GO
ALTER TABLE [dbo].[Levels] CHECK CONSTRAINT [FK_Level_StatInfo]
GO
ALTER TABLE [dbo].[Levels]  WITH CHECK ADD  CONSTRAINT [FK_Tiers_Levels] FOREIGN KEY([FKTiersId])
REFERENCES [dbo].[Tiers] ([Id])
GO
ALTER TABLE [dbo].[Levels] CHECK CONSTRAINT [FK_Tiers_Levels]
GO
ALTER TABLE [dbo].[MiscItems]  WITH CHECK ADD  CONSTRAINT [FK_MiscItems_GameItems] FOREIGN KEY([Id])
REFERENCES [dbo].[GameItems] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MiscItems] CHECK CONSTRAINT [FK_MiscItems_GameItems]
GO
ALTER TABLE [dbo].[MiscItems]  WITH CHECK ADD  CONSTRAINT [FK_MiscItems_Skills] FOREIGN KEY([FKSkillsId])
REFERENCES [dbo].[Skills] ([Id])
GO
ALTER TABLE [dbo].[MiscItems] CHECK CONSTRAINT [FK_MiscItems_Skills]
GO
ALTER TABLE [dbo].[OtherItems]  WITH CHECK ADD  CONSTRAINT [FK_OtherItems_GameItems] FOREIGN KEY([Id])
REFERENCES [dbo].[GameItems] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OtherItems] CHECK CONSTRAINT [FK_OtherItems_GameItems]
GO
ALTER TABLE [dbo].[OtherItems]  WITH CHECK ADD  CONSTRAINT [FK_OtherItems_StatInfo] FOREIGN KEY([FKStatInfoId])
REFERENCES [dbo].[StatInfo] ([Id])
GO
ALTER TABLE [dbo].[OtherItems] CHECK CONSTRAINT [FK_OtherItems_StatInfo]
GO
ALTER TABLE [dbo].[OtherItemsCraftingItemsAssociation]  WITH CHECK ADD  CONSTRAINT [FK_OtherItemsCraftingItemsAssociation_CraftingIngredients] FOREIGN KEY([FKCraftingIngredientsId])
REFERENCES [dbo].[GameItems] ([Id])
GO
ALTER TABLE [dbo].[OtherItemsCraftingItemsAssociation] CHECK CONSTRAINT [FK_OtherItemsCraftingItemsAssociation_CraftingIngredients]
GO
ALTER TABLE [dbo].[OtherItemsCraftingItemsAssociation]  WITH CHECK ADD  CONSTRAINT [FK_OtherItemsCraftingItemsAssociation_OtherItems] FOREIGN KEY([FKOtherItemsId])
REFERENCES [dbo].[OtherItems] ([Id])
GO
ALTER TABLE [dbo].[OtherItemsCraftingItemsAssociation] CHECK CONSTRAINT [FK_OtherItemsCraftingItemsAssociation_OtherItems]
GO
ALTER TABLE [dbo].[Skills]  WITH CHECK ADD  CONSTRAINT [FK_Skills_StatInfo] FOREIGN KEY([FKStatInfoId])
REFERENCES [dbo].[StatInfo] ([Id])
GO
ALTER TABLE [dbo].[Skills] CHECK CONSTRAINT [FK_Skills_StatInfo]
GO
ALTER TABLE [dbo].[SkillsSpecialsAssociation]  WITH CHECK ADD  CONSTRAINT [FK_SkillsSpecialsAssociation_Skills] FOREIGN KEY([FKSkillsId])
REFERENCES [dbo].[Skills] ([Id])
GO
ALTER TABLE [dbo].[SkillsSpecialsAssociation] CHECK CONSTRAINT [FK_SkillsSpecialsAssociation_Skills]
GO
ALTER TABLE [dbo].[SkillsSpecialsAssociation]  WITH CHECK ADD  CONSTRAINT [FK_SkillsSpecialsAssociation_Specials] FOREIGN KEY([FKSpecialsId])
REFERENCES [dbo].[Specials] ([Id])
GO
ALTER TABLE [dbo].[SkillsSpecialsAssociation] CHECK CONSTRAINT [FK_SkillsSpecialsAssociation_Specials]
GO
ALTER TABLE [dbo].[SubCategories]  WITH CHECK ADD  CONSTRAINT [FK_SubCategory_MainCategory] FOREIGN KEY([MainCategory])
REFERENCES [dbo].[MainCategories] ([Id])
GO
ALTER TABLE [dbo].[SubCategories] CHECK CONSTRAINT [FK_SubCategory_MainCategory]
GO
ALTER TABLE [dbo].[Tiers]  WITH CHECK ADD  CONSTRAINT [FK_EquipmentItems_Tiers] FOREIGN KEY([FKEquipmentItemsId])
REFERENCES [dbo].[EquipmentItems] ([Id])
GO
ALTER TABLE [dbo].[Tiers] CHECK CONSTRAINT [FK_EquipmentItems_Tiers]
GO
ALTER TABLE [dbo].[TiersCraftingItemsAssociation]  WITH CHECK ADD  CONSTRAINT [FK_TiersCraftingItemsAssociation_CraftingIngredients] FOREIGN KEY([FKCraftingIngredientsId])
REFERENCES [dbo].[GameItems] ([Id])
GO
ALTER TABLE [dbo].[TiersCraftingItemsAssociation] CHECK CONSTRAINT [FK_TiersCraftingItemsAssociation_CraftingIngredients]
GO
ALTER TABLE [dbo].[TiersCraftingItemsAssociation]  WITH CHECK ADD  CONSTRAINT [FK_TiersCraftingItemsAssociation_Tiers] FOREIGN KEY([FKTiersId])
REFERENCES [dbo].[Tiers] ([Id])
GO
ALTER TABLE [dbo].[TiersCraftingItemsAssociation] CHECK CONSTRAINT [FK_TiersCraftingItemsAssociation_Tiers]
GO
ALTER TABLE [dbo].[TiersSkillsAssociation]  WITH CHECK ADD  CONSTRAINT [FK_TiersSkillsAssociation_Skills] FOREIGN KEY([FKSkillsId])
REFERENCES [dbo].[Skills] ([Id])
GO
ALTER TABLE [dbo].[TiersSkillsAssociation] CHECK CONSTRAINT [FK_TiersSkillsAssociation_Skills]
GO
ALTER TABLE [dbo].[TiersSkillsAssociation]  WITH CHECK ADD  CONSTRAINT [FK_TiersSkillsAssociation_Tiers] FOREIGN KEY([FKTiersId])
REFERENCES [dbo].[Tiers] ([Id])
GO
ALTER TABLE [dbo].[TiersSkillsAssociation] CHECK CONSTRAINT [FK_TiersSkillsAssociation_Tiers]
GO
ALTER TABLE [dbo].[TiersUpgradeItemsAssociation]  WITH CHECK ADD  CONSTRAINT [FK_TiersUpgradeItemsAssociation_Tiers] FOREIGN KEY([FKTiersId])
REFERENCES [dbo].[Tiers] ([Id])
GO
ALTER TABLE [dbo].[TiersUpgradeItemsAssociation] CHECK CONSTRAINT [FK_TiersUpgradeItemsAssociation_Tiers]
GO
ALTER TABLE [dbo].[TiersUpgradeItemsAssociation]  WITH CHECK ADD  CONSTRAINT [FK_TiersUpgradeItemsAssociation_UpgradeIngredients] FOREIGN KEY([FKUpgradeIngredientsId])
REFERENCES [dbo].[GameItems] ([Id])
GO
ALTER TABLE [dbo].[TiersUpgradeItemsAssociation] CHECK CONSTRAINT [FK_TiersUpgradeItemsAssociation_UpgradeIngredients]
GO
USE [master]
GO
ALTER DATABASE [NinjaHive] SET  READ_WRITE 
GO
