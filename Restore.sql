
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 07/18/2015 14:36:06
-- Generated from EDMX file: D:\Dev\NinjaVault\NinjaHive\NinjaHive.Domain\NinjaHiveEntities.edmx
-- Customized by: Alper Aslan
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [NinjaHive];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_EquipmentItems_Tiers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Tiers] DROP CONSTRAINT [FK_EquipmentItems_Tiers];
GO
IF OBJECT_ID(N'[dbo].[FK_Tiers_Levels]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Levels] DROP CONSTRAINT [FK_Tiers_Levels];
GO
IF OBJECT_ID(N'[dbo].[FK_TiersSkillsAssociation_Tiers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TiersSkillsAssociation] DROP CONSTRAINT [FK_TiersSkillsAssociation_Tiers];
GO
IF OBJECT_ID(N'[dbo].[FK_TiersSkillsAssociation_Skills]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TiersSkillsAssociation] DROP CONSTRAINT [FK_TiersSkillsAssociation_Skills];
GO
IF OBJECT_ID(N'[dbo].[FK_SkillsSpecialsAssociation_Skills]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SkillsSpecialsAssociation] DROP CONSTRAINT [FK_SkillsSpecialsAssociation_Skills];
GO
IF OBJECT_ID(N'[dbo].[FK_SkillsSpecialsAssociation_Specials]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SkillsSpecialsAssociation] DROP CONSTRAINT [FK_SkillsSpecialsAssociation_Specials];
GO
IF OBJECT_ID(N'[dbo].[FK_Levels_StatInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Levels] DROP CONSTRAINT [FK_Levels_StatInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_Skills_StatInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Skills] DROP CONSTRAINT [FK_Skills_StatInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_OtherItems_StatInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OtherItems] DROP CONSTRAINT [FK_OtherItems_StatInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_MiscItems_Skills]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MiscItems] DROP CONSTRAINT [FK_MiscItems_Skills];
GO
IF OBJECT_ID(N'[dbo].[FK_EquipmentItemsRacesAssociation_EquipmentItems]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EquipmentItemsRacesAssociation] DROP CONSTRAINT [FK_EquipmentItemsRacesAssociation_EquipmentItems];
GO
IF OBJECT_ID(N'[dbo].[FK_EquipmentItemsRacesAssociation_Races]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EquipmentItemsRacesAssociation] DROP CONSTRAINT [FK_EquipmentItemsRacesAssociation_Races];
GO
IF OBJECT_ID(N'[dbo].[FK_EquipmentItemsClassesAssociation_EquipmentItems]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EquipmentItemsClassesAssociation] DROP CONSTRAINT [FK_EquipmentItemsClassesAssociation_EquipmentItems];
GO
IF OBJECT_ID(N'[dbo].[FK_EquipmentItemsClassesAssociation_Classes]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EquipmentItemsClassesAssociation] DROP CONSTRAINT [FK_EquipmentItemsClassesAssociation_Classes];
GO
IF OBJECT_ID(N'[dbo].[FK_EquipmentItemsEnhancersAssociation_EquipmentItems]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EquipmentItemsEnhancersAssociation] DROP CONSTRAINT [FK_EquipmentItemsEnhancersAssociation_EquipmentItems];
GO
IF OBJECT_ID(N'[dbo].[FK_EquipmentItemsEnhancersAssociation_Enhancers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EquipmentItemsEnhancersAssociation] DROP CONSTRAINT [FK_EquipmentItemsEnhancersAssociation_Enhancers];
GO
IF OBJECT_ID(N'[dbo].[FK_OtherItemsCraftingItemsAssociation_OtherItems]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OtherItemsCraftingItemsAssociation] DROP CONSTRAINT [FK_OtherItemsCraftingItemsAssociation_OtherItems];
GO
IF OBJECT_ID(N'[dbo].[FK_OtherItemsCraftingItemsAssociation_CraftingIngredients]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OtherItemsCraftingItemsAssociation] DROP CONSTRAINT [FK_OtherItemsCraftingItemsAssociation_CraftingIngredients];
GO
IF OBJECT_ID(N'[dbo].[FK_TiersUpgradeItemsAssociation_Tiers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TiersUpgradeItemsAssociation] DROP CONSTRAINT [FK_TiersUpgradeItemsAssociation_Tiers];
GO
IF OBJECT_ID(N'[dbo].[FK_TiersUpgradeItemsAssociation_UpgradeIngredients]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TiersUpgradeItemsAssociation] DROP CONSTRAINT [FK_TiersUpgradeItemsAssociation_UpgradeIngredients];
GO
IF OBJECT_ID(N'[dbo].[FK_TiersCraftingItemsAssociation_Tiers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TiersCraftingItemsAssociation] DROP CONSTRAINT [FK_TiersCraftingItemsAssociation_Tiers];
GO
IF OBJECT_ID(N'[dbo].[FK_TiersCraftingItemsAssociation_CraftingIngredients]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TiersCraftingItemsAssociation] DROP CONSTRAINT [FK_TiersCraftingItemsAssociation_CraftingIngredients];
GO
IF OBJECT_ID(N'[dbo].[FK_EquipmentItems_GameItems]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EquipmentItems] DROP CONSTRAINT [FK_EquipmentItems_GameItems];
GO
IF OBJECT_ID(N'[dbo].[FK_OtherItems_GameItems]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OtherItems] DROP CONSTRAINT [FK_OtherItems_GameItems];
GO
IF OBJECT_ID(N'[dbo].[FK_MiscItems_GameItems]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MiscItems] DROP CONSTRAINT [FK_MiscItems_GameItems];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[GameItems]', 'U') IS NOT NULL
    DROP TABLE [dbo].[GameItems];
GO
IF OBJECT_ID(N'[dbo].[Races]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Races];
GO
IF OBJECT_ID(N'[dbo].[Classes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Classes];
GO
IF OBJECT_ID(N'[dbo].[Tiers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Tiers];
GO
IF OBJECT_ID(N'[dbo].[Levels]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Levels];
GO
IF OBJECT_ID(N'[dbo].[Skills]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Skills];
GO
IF OBJECT_ID(N'[dbo].[StatInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StatInfo];
GO
IF OBJECT_ID(N'[dbo].[Specials]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Specials];
GO
IF OBJECT_ID(N'[dbo].[EquipmentItems]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EquipmentItems];
GO
IF OBJECT_ID(N'[dbo].[OtherItems]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OtherItems];
GO
IF OBJECT_ID(N'[dbo].[MiscItems]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MiscItems];
GO
IF OBJECT_ID(N'[dbo].[TiersSkillsAssociation]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TiersSkillsAssociation];
GO
IF OBJECT_ID(N'[dbo].[SkillsSpecialsAssociation]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SkillsSpecialsAssociation];
GO
IF OBJECT_ID(N'[dbo].[EquipmentItemsRacesAssociation]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EquipmentItemsRacesAssociation];
GO
IF OBJECT_ID(N'[dbo].[EquipmentItemsClassesAssociation]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EquipmentItemsClassesAssociation];
GO
IF OBJECT_ID(N'[dbo].[EquipmentItemsEnhancersAssociation]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EquipmentItemsEnhancersAssociation];
GO
IF OBJECT_ID(N'[dbo].[OtherItemsCraftingItemsAssociation]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OtherItemsCraftingItemsAssociation];
GO
IF OBJECT_ID(N'[dbo].[TiersUpgradeItemsAssociation]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TiersUpgradeItemsAssociation];
GO
IF OBJECT_ID(N'[dbo].[TiersCraftingItemsAssociation]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TiersCraftingItemsAssociation];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

CREATE TABLE [dbo].[GameItems] (
    [Id] uniqueidentifier DEFAULT (NEWID()) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Category] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [Craftable] bit  NOT NULL,
    [IsUpgrader] bit  NOT NULL,
    [IsCrafter] bit  NOT NULL,
    [IsQuestItem] bit  NOT NULL,
    [Value] int  NOT NULL
);
GO

CREATE TABLE [dbo].[EquipmentItems] (
	[Id] uniqueidentifier  NOT NULL,
    [Durability] int  NOT NULL,
    [NumberOfSlots] int  NOT NULL,
    [BodySlot] int  NOT NULL
);
GO

CREATE TABLE [dbo].[OtherItems] (
    [IsEnhancer] bit  NOT NULL,
    [Id] uniqueidentifier  NOT NULL,
    [FKStatInfoId] uniqueidentifier  NOT NULL
);
GO

CREATE TABLE [dbo].[MiscItems] (
    [Id] uniqueidentifier  NOT NULL,
    [FKSkillsId] uniqueidentifier  NOT NULL
);
GO

CREATE TABLE [dbo].[Races] (
    [Id] uniqueidentifier DEFAULT (NEWID()) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

CREATE TABLE [dbo].[Classes] (
    [Id] uniqueidentifier DEFAULT (NEWID()) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

CREATE TABLE [dbo].[Tiers] (
    [Id] uniqueidentifier DEFAULT (NEWID()) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Tier] int  NOT NULL,
    [FKEquipmentItemsId] uniqueidentifier  NOT NULL
);
GO

CREATE TABLE [dbo].[Levels] (
    [Id] uniqueidentifier DEFAULT (NEWID()) NOT NULL,
    [Level] int  NOT NULL,
    [Threshold] int  NOT NULL,
    [FKTiersId] uniqueidentifier  NOT NULL,
    [FKStatInfoId] uniqueidentifier  NOT NULL
);
GO

CREATE TABLE [dbo].[Skills] (
    [Id] uniqueidentifier DEFAULT (newid()) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Range] int  NOT NULL,
    [Radius] int  NOT NULL,
    [Targets] int  NOT NULL,
    [Target] int  NOT NULL,
    [Friendly] bit  NOT NULL,
    [FKStatInfoId] uniqueidentifier  NOT NULL
);
GO

CREATE TABLE [dbo].[StatInfo] (
    [Id] uniqueidentifier DEFAULT (NEWID()) NOT NULL,
    [Health] int  NOT NULL,
    [Magic] int  NOT NULL,
    [Attack] int  NOT NULL,
    [Defense] int  NOT NULL,
    [Stamina] int  NOT NULL,
    [Hunger] int  NOT NULL,
    [Agility] int  NOT NULL,
    [Intelligence] int  NOT NULL,
    [Resistance] float  NOT NULL
);
GO

CREATE TABLE [dbo].[Specials] (
    [Id] uniqueidentifier DEFAULT (NEWID()) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO


CREATE TABLE [dbo].[TiersSkillsAssociation] (
    [FKTiersId] uniqueidentifier  NOT NULL,
    [FKSkillsId] uniqueidentifier  NOT NULL
);
GO

CREATE TABLE [dbo].[SkillsSpecialsAssociation] (
    [FKSkillsId] uniqueidentifier  NOT NULL,
    [FKSpecialsId] uniqueidentifier  NOT NULL
);
GO

CREATE TABLE [dbo].[EquipmentItemsRacesAssociation] (
    [FKEquipmentItemsId] uniqueidentifier  NOT NULL,
    [FKRacesId] uniqueidentifier  NOT NULL
);
GO

CREATE TABLE [dbo].[EquipmentItemsClassesAssociation] (
    [FKEquipmentItemsId] uniqueidentifier  NOT NULL,
    [FKClassesId] uniqueidentifier  NOT NULL
);
GO

CREATE TABLE [dbo].[EquipmentItemsEnhancersAssociation] (
    [FKEquipmentItemsId] uniqueidentifier  NOT NULL,
    [FKEnhancersId] uniqueidentifier  NOT NULL
);
GO

CREATE TABLE [dbo].[OtherItemsCraftingItemsAssociation] (
    [FKOtherItemsId] uniqueidentifier  NOT NULL,
    [FKCraftingIngredientsId] uniqueidentifier  NOT NULL
);
GO

CREATE TABLE [dbo].[TiersUpgradeItemsAssociation] (
    [FKTiersId] uniqueidentifier  NOT NULL,
    [FKUpgradeIngredientsId] uniqueidentifier  NOT NULL
);
GO

CREATE TABLE [dbo].[TiersCraftingItemsAssociation] (
    [FKTiersId] uniqueidentifier  NOT NULL,
    [FKCraftingIngredientsId] uniqueidentifier  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

ALTER TABLE [dbo].[GameItems]
ADD CONSTRAINT [PK_GameItems]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

ALTER TABLE [dbo].[Races]
ADD CONSTRAINT [PK_Races]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

ALTER TABLE [dbo].[Classes]
ADD CONSTRAINT [PK_Classes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

ALTER TABLE [dbo].[Tiers]
ADD CONSTRAINT [PK_Tiers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

ALTER TABLE [dbo].[Levels]
ADD CONSTRAINT [PK_Levels]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

ALTER TABLE [dbo].[Skills]
ADD CONSTRAINT [PK_Skills]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

ALTER TABLE [dbo].[StatInfo]
ADD CONSTRAINT [PK_StatInfo]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

ALTER TABLE [dbo].[Specials]
ADD CONSTRAINT [PK_Specials]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

ALTER TABLE [dbo].[EquipmentItems]
ADD CONSTRAINT [PK_EquipmentItems]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

ALTER TABLE [dbo].[OtherItems]
ADD CONSTRAINT [PK_OtherItems]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

ALTER TABLE [dbo].[MiscItems]
ADD CONSTRAINT [PK_MiscItems]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

ALTER TABLE [dbo].[TiersSkillsAssociation]
ADD CONSTRAINT [PK_Tiers_Skills]
    PRIMARY KEY CLUSTERED ([FKTiersId], [FKSkillsId] ASC);
GO

ALTER TABLE [dbo].[SkillsSpecialsAssociation]
ADD CONSTRAINT [PK_Skills_Specials]
    PRIMARY KEY CLUSTERED ([FKSkillsId], [FKSpecialsId] ASC);
GO

ALTER TABLE [dbo].[EquipmentItemsRacesAssociation]
ADD CONSTRAINT [PK_EquipmentItems_Races]
    PRIMARY KEY CLUSTERED ([FKEquipmentItemsId], [FKRacesId] ASC);
GO

ALTER TABLE [dbo].[EquipmentItemsClassesAssociation]
ADD CONSTRAINT [PK_EquipmentItems_Classes]
    PRIMARY KEY CLUSTERED ([FKEquipmentItemsId], [FKClassesId] ASC);
GO

ALTER TABLE [dbo].[EquipmentItemsEnhancersAssociation]
ADD CONSTRAINT [PK_EquipmentItems_Enhancers]
    PRIMARY KEY CLUSTERED ([FKEquipmentItemsId], [FKEnhancersId] ASC);
GO

ALTER TABLE [dbo].[OtherItemsCraftingItemsAssociation]
ADD CONSTRAINT [PK_OtherItems_CraftingItems]
    PRIMARY KEY CLUSTERED ([FKOtherItemsId], [FKCraftingIngredientsId] ASC);
GO

ALTER TABLE [dbo].[TiersUpgradeItemsAssociation]
ADD CONSTRAINT [PK_Tiers_UpgradeItems]
    PRIMARY KEY CLUSTERED ([FKTiersId], [FKUpgradeIngredientsId] ASC);
GO

ALTER TABLE [dbo].[TiersCraftingItemsAssociation]
ADD CONSTRAINT [PK_Tiers_CraftingItems]
    PRIMARY KEY CLUSTERED ([FKTiersId], [FKCraftingIngredientsId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

ALTER TABLE [dbo].[Tiers]
ADD CONSTRAINT [FK_EquipmentItems_Tiers]
    FOREIGN KEY ([FKEquipmentItemsId])
    REFERENCES [dbo].[EquipmentItems]
        ([Id])
    ON DELETE NO ACTION
	ON UPDATE NO ACTION;
GO

CREATE INDEX [IX_FK_EquipmentItems_Tiers]
ON [dbo].[Tiers]
    ([FKEquipmentItemsId]);
GO

ALTER TABLE [dbo].[Levels]
ADD CONSTRAINT [FK_Tiers_Levels]
    FOREIGN KEY ([FKTiersId])
    REFERENCES [dbo].[Tiers]
        ([Id])
    ON DELETE NO ACTION
	ON UPDATE NO ACTION;
GO

CREATE INDEX [IX_FK_Tiers_Levels]
ON [dbo].[Levels]
    ([FKTiersId]);
GO

ALTER TABLE [dbo].[TiersSkillsAssociation]
ADD CONSTRAINT [FK_TiersSkillsAssociation_Tiers]
    FOREIGN KEY ([FKTiersId])
    REFERENCES [dbo].[Tiers]
        ([Id])
    ON DELETE NO ACTION
	ON UPDATE NO ACTION;
GO

ALTER TABLE [dbo].[TiersSkillsAssociation]
ADD CONSTRAINT [FK_TiersSkillsAssociation_Skills]
    FOREIGN KEY ([FKSkillsId])
    REFERENCES [dbo].[Skills]
        ([Id])
    ON DELETE NO ACTION
	ON UPDATE NO ACTION;
GO

CREATE INDEX [IX_FK_TiersSkillsAssociation_Skills]
ON [dbo].[TiersSkillsAssociation]
    ([FKSkillsId]);
GO

ALTER TABLE [dbo].[SkillsSpecialsAssociation]
ADD CONSTRAINT [FK_SkillsSpecialsAssociation_Skills]
    FOREIGN KEY ([FKSkillsId])
    REFERENCES [dbo].[Skills]
        ([Id])
    ON DELETE NO ACTION
	ON UPDATE NO ACTION;
GO

ALTER TABLE [dbo].[SkillsSpecialsAssociation]
ADD CONSTRAINT [FK_SkillsSpecialsAssociation_Specials]
    FOREIGN KEY ([FKSpecialsId])
    REFERENCES [dbo].[Specials]
        ([Id])
    ON DELETE NO ACTION
	ON UPDATE NO ACTION;
GO

CREATE INDEX [IX_FK_SkillsSpecialsAssociation_Specials]
ON [dbo].[SkillsSpecialsAssociation]
    ([FKSpecialsId]);
GO

ALTER TABLE [dbo].[Levels]
ADD CONSTRAINT [FK_Level_StatInfo]
    FOREIGN KEY ([FKStatInfoId])
    REFERENCES [dbo].[StatInfo]
        ([Id])
    ON DELETE NO ACTION
	ON UPDATE NO ACTION;
GO

CREATE INDEX [IX_FK_Level_StatInfo]
ON [dbo].[Levels]
    ([FKStatInfoId]);
GO

ALTER TABLE [dbo].[Skills]
ADD CONSTRAINT [FK_Skills_StatInfo]
    FOREIGN KEY ([FKStatInfoId])
    REFERENCES [dbo].[StatInfo]
        ([Id])
    ON DELETE NO ACTION
	ON UPDATE NO ACTION;
GO

CREATE INDEX [IX_FK_Skills_StatInfo]
ON [dbo].[Skills]
    ([FKStatInfoId]);
GO

ALTER TABLE [dbo].[OtherItems]
ADD CONSTRAINT [FK_OtherItems_StatInfo]
    FOREIGN KEY ([FKStatInfoId])
    REFERENCES [dbo].[StatInfo]
        ([Id])
    ON DELETE NO ACTION
	ON UPDATE NO ACTION;
GO

CREATE INDEX [IX_FK_OtherItems_StatInfo]
ON [dbo].[OtherItems]
    ([FKStatInfoId]);
GO

ALTER TABLE [dbo].[MiscItems]
ADD CONSTRAINT [FK_MiscItems_Skills]
    FOREIGN KEY ([FKSkillsId])
    REFERENCES [dbo].[Skills]
        ([Id])
    ON DELETE NO ACTION
	ON UPDATE NO ACTION;
GO

CREATE INDEX [IX_FK_MiscItems_Skills]
ON [dbo].[MiscItems]
    ([FKSkillsId]);
GO

ALTER TABLE [dbo].[EquipmentItemsRacesAssociation]
ADD CONSTRAINT [FK_EquipmentItemsRacesAssociation_EquipmentItems]
    FOREIGN KEY ([FKEquipmentItemsId])
    REFERENCES [dbo].[EquipmentItems]
        ([Id])
    ON DELETE NO ACTION
	ON UPDATE NO ACTION;
GO

ALTER TABLE [dbo].[EquipmentItemsRacesAssociation]
ADD CONSTRAINT [FK_EquipmentItemsRacesAssociation_Races]
    FOREIGN KEY ([FKRacesId])
    REFERENCES [dbo].[Races]
        ([Id])
    ON DELETE NO ACTION
	ON UPDATE NO ACTION;
GO

CREATE INDEX [IX_FK_EquipmentItemsRacesAssociation_Races]
ON [dbo].[EquipmentItemsRacesAssociation]
    ([FKRacesId]);
GO

ALTER TABLE [dbo].[EquipmentItemsClassesAssociation]
ADD CONSTRAINT [FK_EquipmentItemsClassesAssociation_EquipmentItems]
    FOREIGN KEY ([FKEquipmentItemsId])
    REFERENCES [dbo].[EquipmentItems]
        ([Id])
    ON DELETE NO ACTION
	ON UPDATE NO ACTION;
GO

ALTER TABLE [dbo].[EquipmentItemsClassesAssociation]
ADD CONSTRAINT [FK_EquipmentItemsClassesAssociation_Classes]
    FOREIGN KEY ([FKClassesId])
    REFERENCES [dbo].[Classes]
        ([Id])
    ON DELETE NO ACTION
	ON UPDATE NO ACTION;
GO

CREATE INDEX [IX_FK_EquipmentItemsClassesAssociation_Classes]
ON [dbo].[EquipmentItemsClassesAssociation]
    ([FKClassesId]);
GO

ALTER TABLE [dbo].[EquipmentItemsEnhancersAssociation]
ADD CONSTRAINT [FK_EquipmentItemsEnhancersAssociation_EquipmentItems]
    FOREIGN KEY ([FKEquipmentItemsId])
    REFERENCES [dbo].[EquipmentItems]
        ([Id])
    ON DELETE NO ACTION
	ON UPDATE NO ACTION;
GO

ALTER TABLE [dbo].[EquipmentItemsEnhancersAssociation]
ADD CONSTRAINT [FK_EquipmentItemsEnhancersAssociation_Enhancers]
    FOREIGN KEY ([FKEnhancersId])
    REFERENCES [dbo].[OtherItems]
        ([Id])
    ON DELETE NO ACTION
	ON UPDATE NO ACTION;
GO

CREATE INDEX [IX_FK_EquipmentItemsEnhancersAssociation_Enhancers]
ON [dbo].[EquipmentItemsEnhancersAssociation]
    ([FKEnhancersId]);
GO

ALTER TABLE [dbo].[OtherItemsCraftingItemsAssociation]
ADD CONSTRAINT [FK_OtherItemsCraftingItemsAssociation_OtherItems]
    FOREIGN KEY ([FKOtherItemsId])
    REFERENCES [dbo].[OtherItems]
        ([Id])
    ON DELETE NO ACTION
	ON UPDATE NO ACTION;
GO

ALTER TABLE [dbo].[OtherItemsCraftingItemsAssociation]
ADD CONSTRAINT [FK_OtherItemsCraftingItemsAssociation_CraftingIngredients]
    FOREIGN KEY ([FKCraftingIngredientsId])
    REFERENCES [dbo].[GameItems]
        ([Id])
    ON DELETE NO ACTION
	ON UPDATE NO ACTION;
GO

CREATE INDEX [IX_FK_OtherItemsCraftingItemsAssociation_CraftingIngredients]
ON [dbo].[OtherItemsCraftingItemsAssociation]
    ([FKCraftingIngredientsId]);
GO

ALTER TABLE [dbo].[TiersUpgradeItemsAssociation]
ADD CONSTRAINT [FK_TiersUpgradeItemsAssociation_Tiers]
    FOREIGN KEY ([FKTiersId])
    REFERENCES [dbo].[Tiers]
        ([Id])
    ON DELETE NO ACTION
	ON UPDATE NO ACTION;
GO

ALTER TABLE [dbo].[TiersUpgradeItemsAssociation]
ADD CONSTRAINT [FK_TiersUpgradeItemsAssociation_UpgradeIngredients]
    FOREIGN KEY ([FKUpgradeIngredientsId])
    REFERENCES [dbo].[GameItems]
        ([Id])
    ON DELETE NO ACTION
	ON UPDATE NO ACTION;
GO

CREATE INDEX [IX_FK_TiersUpgradeItemsAssociation_UpgradeIngredients]
ON [dbo].[TiersUpgradeItemsAssociation]
    ([FKUpgradeIngredientsId]);
GO

ALTER TABLE [dbo].[TiersCraftingItemsAssociation]
ADD CONSTRAINT [FK_TiersCraftingItemsAssociation_Tiers]
    FOREIGN KEY ([FKTiersId])
    REFERENCES [dbo].[Tiers]
        ([Id])
    ON DELETE NO ACTION
	ON UPDATE NO ACTION;
GO

ALTER TABLE [dbo].[TiersCraftingItemsAssociation]
ADD CONSTRAINT [FK_TiersCraftingItemsAssociation_CraftingIngredients]
    FOREIGN KEY ([FKCraftingIngredientsId])
    REFERENCES [dbo].[GameItems]
        ([Id])
    ON DELETE NO ACTION
	ON UPDATE NO ACTION;
GO

CREATE INDEX [IX_FK_TiersCraftingItemsAssociation_CraftingIngredients]
ON [dbo].[TiersCraftingItemsAssociation]
    ([FKCraftingIngredientsId]);
GO

ALTER TABLE [dbo].[EquipmentItems]
ADD CONSTRAINT [FK_EquipmentItems_GameItems]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[GameItems]
        ([Id])
    ON DELETE CASCADE
	ON UPDATE NO ACTION;
GO

ALTER TABLE [dbo].[OtherItems]
ADD CONSTRAINT [FK_OtherItems_GameItems]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[GameItems]
        ([Id])
    ON DELETE CASCADE
	ON UPDATE NO ACTION;
GO

ALTER TABLE [dbo].[MiscItems]
ADD CONSTRAINT [FK_MiscItems_GameItems]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[GameItems]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------