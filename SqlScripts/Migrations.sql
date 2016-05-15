-- Alper Aslan, 30-04-2016: Rename of MiscItems to SkillItems (including all objects like keys and constraints)
EXEC sp_rename '[dbo].[MiscItems]', 'SkillItems';
EXEC sp_rename '[dbo].[PK_MiscItems]', 'PK_SkillItems';
EXEC sp_rename '[dbo].[FK_MiscItems_GameItems]', 'FK_SkillItems_GameItems';
EXEC sp_rename '[dbo].[FK_MiscITems_Skills]', 'FK_SkillItems_Skills';
EXEC sp_rename N'[dbo].[SkillItems].[IX_FK_MiscItems_Skills]', N'IX_FK_SkillItems_Skills', N'INDEX';
GO

-- Alper Aslan,  15-05-2016: ON CASCADE DELETE on tiers when deleting an equipment item

ALTER TABLE [dbo].[Tiers] DROP CONSTRAINT [FK_EquipmentItems_Tiers];
GO
ALTER TABLE [dbo].[Tiers] WITH CHECK ADD CONSTRAINT [FK_EquipmentItems_Tiers] FOREIGN KEY([FKEquipmentItemsId])
REFERENCES [dbo].[EquipmentItems] ([Id]) ON DELETE CASCADE;
GO
ALTER TABLE [dbo].[Tiers] CHECK CONSTRAINT [FK_EquipmentItems_Tiers];
GO