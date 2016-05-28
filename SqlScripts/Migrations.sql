-- Alper Aslan, 30-04-2016: Rename of MiscItems to SkillItems (including all objects like keys and constraints)
EXEC sp_rename '[dbo].[MiscItems]', 'SkillItems';
EXEC sp_rename '[dbo].[PK_MiscItems]', 'PK_SkillItems';
EXEC sp_rename '[dbo].[FK_MiscItems_GameItems]', 'FK_SkillItems_GameItems';
EXEC sp_rename '[dbo].[FK_MiscITems_Skills]', 'FK_SkillItems_Skills';
EXEC sp_rename N'[dbo].[SkillItems].[IX_FK_MiscItems_Skills]', N'IX_FK_SkillItems_Skills', N'INDEX';
GO

-- Alper Aslan, 15-05-2016: ON CASCADE DELETE on tiers when deleting an equipment item
ALTER TABLE [dbo].[Tiers] DROP CONSTRAINT [FK_EquipmentItems_Tiers];
GO
ALTER TABLE [dbo].[Tiers] WITH CHECK ADD CONSTRAINT [FK_EquipmentItems_Tiers] FOREIGN KEY([FKEquipmentItemsId])
REFERENCES [dbo].[EquipmentItems] ([Id]) ON DELETE CASCADE;
GO
ALTER TABLE [dbo].[Tiers] CHECK CONSTRAINT [FK_EquipmentItems_Tiers];
GO

-- Alper Aslan, 28-05-2016: Redesign of EquipmentItems/Tiers
-- NOTE: This change of the database does not migrate old data!

--first drop everything related with the Tiers table and drop the table itself
DROP INDEX [IX_FK_Tiers_Levels] ON [dbo].[Levels];
GO
ALTER TABLE [dbo].[Levels] DROP CONSTRAINT [Fk_Tiers_Levels];
GO
ALTER TABLE [dbo].[Levels] DROP COLUMN [FkTiersId];
GO

DROP TABLE [dbo].[TiersSkillsAssociation];
DROP TABLE [dbo].[TiersCraftingItemsAssociation];
DROP TABLE [dbo].[TiersUpgradeItemsAssociation];
DROP TABLE [dbo].[Tiers];
GO

--redesign of the EquipmentItems table (merged with Tiers table)
DELETE FROM [dbo].[EquipmentItems];
GO

ALTER TABLE [dbo].[EquipmentItems] ADD [FkParentTier] uniqueidentifier NULL;
GO
ALTER TABLE [dbo].[EquipmentItems] WITH CHECK ADD CONSTRAINT [Fk_EquipmentItem_ParentTier] FOREIGN KEY([FkParentTier]) REFERENCES [dbo].[EquipmentItems](Id);
ALTER TABLE [dbo].[EquipmentItems] CHECK CONSTRAINT [Fk_EquipmentItem_ParentTier];
GO

ALTER TABLE [dbo].[EquipmentItems] ADD [Tier] int NOT NULL;
GO

--reintroduce mtm tables
CREATE TABLE [dbo].[EquipmentItemSkills](
    [FkEquipmentItem] [uniqueidentifier] NOT NULL,
    [FkSkill] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Tiers_Skills] PRIMARY KEY CLUSTERED 
(
    [FkEquipmentItem] ASC,
    [FkSkill] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];

CREATE TABLE [dbo].[EquipmentItemCraftIngredients](
    [FkEquipmentItem] [uniqueidentifier] NOT NULL,
    [FKCraftIngredient] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Tiers_CraftingItems] PRIMARY KEY CLUSTERED 
(
    [FkEquipmentItem] ASC,
    [FKCraftIngredient] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];

CREATE TABLE [dbo].[EquipmentItemUpgrades](
    [FkEquipmentItem] [uniqueidentifier] NOT NULL,
    [FKUpgrade] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Tiers_UpgradeItems] PRIMARY KEY CLUSTERED 
(
    [FkEquipmentItem] ASC,
    [FKUpgrade] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];

ALTER TABLE [dbo].[EquipmentItemSkills] WITH CHECK ADD CONSTRAINT [Fk_EquipmentItemSkills_EquipmentItems] FOREIGN KEY([FkEquipmentItem]) REFERENCES [dbo].[EquipmentItems] ([Id]);
ALTER TABLE [dbo].[EquipmentItemSkills] CHECK CONSTRAINT [Fk_EquipmentItemSkills_EquipmentItems];
GO
ALTER TABLE [dbo].[EquipmentItemSkills] WITH CHECK ADD CONSTRAINT [Fk_EquipmentItemSkills_Skills] FOREIGN KEY([FkSkill]) REFERENCES [dbo].[Skills] ([Id]);
ALTER TABLE [dbo].[EquipmentItemSkills] CHECK CONSTRAINT [Fk_EquipmentItemSkills_Skills];
GO

ALTER TABLE [dbo].[EquipmentItemCraftIngredients] WITH CHECK ADD CONSTRAINT [Fk_EquipmentItemCraftIngredients_EquipmentItems] FOREIGN KEY([FkEquipmentItem]) REFERENCES [dbo].[EquipmentItems] ([Id]);
ALTER TABLE [dbo].[EquipmentItemCraftIngredients] CHECK CONSTRAINT [Fk_EquipmentItemCraftIngredients_EquipmentItems];
GO
ALTER TABLE [dbo].[EquipmentItemCraftIngredients] WITH CHECK ADD CONSTRAINT [Fk_EquipmentItemCraftIngredients_GameItems] FOREIGN KEY([FKCraftIngredient]) REFERENCES [dbo].[GameItems] ([Id]);
ALTER TABLE [dbo].[EquipmentItemCraftIngredients] CHECK CONSTRAINT [Fk_EquipmentItemCraftIngredients_GameItems];
GO

ALTER TABLE [dbo].[EquipmentItemUpgrades] WITH CHECK ADD CONSTRAINT [Fk_EquipmentItemUpgrades_EquipmentItems] FOREIGN KEY([FkEquipmentItem]) REFERENCES [dbo].[EquipmentItems] ([Id]);
ALTER TABLE [dbo].[EquipmentItemUpgrades] CHECK CONSTRAINT [Fk_EquipmentItemUpgrades_EquipmentItems];
GO
ALTER TABLE [dbo].[EquipmentItemUpgrades] WITH CHECK ADD CONSTRAINT [Fk_EquipmentItemUpgrades_GameItems] FOREIGN KEY([FKUpgrade]) REFERENCES [dbo].[GameItems] ([Id]);
ALTER TABLE [dbo].[EquipmentItemUpgrades] CHECK CONSTRAINT [Fk_EquipmentItemUpgrades_GameItems];
GO

--and lastly attach levels to equipmentitems
ALTER TABLE [dbo].[Levels] ADD [FkEquipmentTier] uniqueidentifier NOT NULL;
GO

ALTER TABLE [dbo].[Levels] WITH CHECK ADD CONSTRAINT [Fk_Levels_EquipmentItems] FOREIGN KEY([FkEquipmentTier]) REFERENCES [dbo].[EquipmentItems] ([Id]);
ALTER TABLE [dbo].[Levels] CHECK CONSTRAINT [Fk_Levels_EquipmentItems];
GO

CREATE NONCLUSTERED INDEX [IX_FK_EquipmentItems_Levels] ON [dbo].[Levels]
(
    [FkEquipmentTier] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY];
GO