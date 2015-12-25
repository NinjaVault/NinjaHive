-- Alper Aslan 07/18/2015: Limit the nvarchar sizes of names and add unique contraints

--change nvarchar sizes to 255 of every Name column in all table
ALTER TABLE [dbo].[GameItems]
ALTER COLUMN [Name] nvarchar(255)
GO

ALTER TABLE [dbo].[Classes]
ALTER COLUMN [Name] nvarchar(255)
GO

ALTER TABLE [dbo].[Races]
ALTER COLUMN [Name] nvarchar(255)
GO

ALTER TABLE [dbo].[Skills]
ALTER COLUMN [Name] nvarchar(255)
GO

ALTER TABLE [dbo].[Specials]
ALTER COLUMN [Name] nvarchar(255)
GO

--add unique constraint to the Name columns
ALTER TABLE [dbo].[GameItems]
ADD CONSTRAINT [UQ_GameItems_Name]
UNIQUE ([Name])
GO

ALTER TABLE [dbo].[Classes]
ADD CONSTRAINT [UQ_Classes_Name]
UNIQUE ([Name])
GO

ALTER TABLE [dbo].[Races]
ADD CONSTRAINT [UQ_Races_Name]
UNIQUE ([Name])
GO

ALTER TABLE [dbo].[Skills]
ADD CONSTRAINT [UQ_Skills_Name]
UNIQUE ([Name])
GO

ALTER TABLE [dbo].[Specials]
ADD CONSTRAINT [UQ_Specials_Name]
UNIQUE ([Name])
GO

-- Alper Aslan 12/25/2015: EditInfo to all tables
ALTER TABLE	[dbo].[GameItems]
ADD     [CreatedOn] datetime NULL,
        [CreatedBy] nvarchar(255) NULL,
        [EditedOn] datetime NULL,
        [EditedBy] nvarchar(255) NULL
ALTER TABLE	[dbo].[Races]
ADD     [CreatedOn] datetime NULL,
        [CreatedBy] nvarchar(255) NULL,
        [EditedOn] datetime NULL,
        [EditedBy] nvarchar(255) NULL
ALTER TABLE	[dbo].[Classes]
ADD     [CreatedOn] datetime NULL,
        [CreatedBy] nvarchar(255) NULL,
        [EditedOn] datetime NULL,
        [EditedBy] nvarchar(255) NULL
ALTER TABLE	[dbo].[Tiers]
ADD     [CreatedOn] datetime NULL,
        [CreatedBy] nvarchar(255) NULL,
        [EditedOn] datetime NULL,
        [EditedBy] nvarchar(255) NULL
ALTER TABLE	[dbo].[Levels]
ADD     [CreatedOn] datetime NULL,
        [CreatedBy] nvarchar(255) NULL,
        [EditedOn] datetime NULL,
        [EditedBy] nvarchar(255) NULL
ALTER TABLE	[dbo].[Skills]
ADD     [CreatedOn] datetime NULL,
        [CreatedBy] nvarchar(255) NULL,
        [EditedOn] datetime NULL,
        [EditedBy] nvarchar(255) NULL
ALTER TABLE	[dbo].[Specials]
ADD     [CreatedOn] datetime NULL,
        [CreatedBy] nvarchar(255) NULL,
        [EditedOn] datetime NULL,
        [EditedBy] nvarchar(255) NULL
ALTER TABLE	[dbo].[StatInfo]
ADD     [CreatedOn] datetime NULL,
        [CreatedBy] nvarchar(255) NULL,
        [EditedOn] datetime NULL,
        [EditedBy] nvarchar(255) NULL
GO

DECLARE @now datetime = GETDATE()
UPDATE [dbo].[GameItems]
SET     [CreatedOn] = @now,
        [CreatedBy] = 'system',
        [EditedOn] = @now,
        [EditedBy] = 'system'
UPDATE [dbo].[Races]
SET     [CreatedOn] = @now,
        [CreatedBy] = 'system',
        [EditedOn] = @now,
        [EditedBy] = 'system'
UPDATE [dbo].[Classes]
SET     [CreatedOn] = @now,
        [CreatedBy] = 'system',
        [EditedOn] = @now,
        [EditedBy] = 'system'
UPDATE [dbo].[Tiers]
SET     [CreatedOn] = @now,
        [CreatedBy] = 'system',
        [EditedOn] = @now,
        [EditedBy] = 'system'
UPDATE [dbo].[Levels]
SET     [CreatedOn] = @now,
        [CreatedBy] = 'system',
        [EditedOn] = @now,
        [EditedBy] = 'system'
UPDATE [dbo].[Skills]
SET     [CreatedOn] = @now,
        [CreatedBy] = 'system',
        [EditedOn] = @now,
        [EditedBy] = 'system'
UPDATE [dbo].[Specials]
SET     [CreatedOn] = @now,
        [CreatedBy] = 'system',
        [EditedOn] = @now,
        [EditedBy] = 'system'
UPDATE [dbo].[StatInfo]
SET     [CreatedOn] = @now,
        [CreatedBy] = 'system',
        [EditedOn] = @now,
        [EditedBy] = 'system'
GO

ALTER TABLE [dbo].[GameItems]
ALTER COLUMN [CreatedOn] datetime NOT NULL
ALTER TABLE [dbo].[GameItems]
ALTER COLUMN [CreatedBy] nvarchar(255) NOT NULL
ALTER TABLE [dbo].[GameItems]
ALTER COLUMN [EditedOn] datetime NOT NULL
ALTER TABLE [dbo].[GameItems]
ALTER COLUMN [EditedBy] nvarchar(255) NOT NULL

ALTER TABLE [dbo].[Races]
ALTER COLUMN [CreatedOn] datetime NOT NULL
ALTER TABLE [dbo].[Races]
ALTER COLUMN [CreatedBy] nvarchar(255) NOT NULL
ALTER TABLE [dbo].[Races]
ALTER COLUMN [EditedOn] datetime NOT NULL
ALTER TABLE [dbo].[Races]
ALTER COLUMN [EditedBy] nvarchar(255) NOT NULL

ALTER TABLE [dbo].[Classes]
ALTER COLUMN [CreatedOn] datetime NOT NULL
ALTER TABLE [dbo].[Classes]
ALTER COLUMN [CreatedBy] nvarchar(255) NOT NULL
ALTER TABLE [dbo].[Classes]
ALTER COLUMN [EditedOn] datetime NOT NULL
ALTER TABLE [dbo].[Classes]
ALTER COLUMN [EditedBy] nvarchar(255) NOT NULL

ALTER TABLE [dbo].[Tiers]
ALTER COLUMN [CreatedOn] datetime NOT NULL
ALTER TABLE [dbo].[Tiers]
ALTER COLUMN [CreatedBy] nvarchar(255) NOT NULL
ALTER TABLE [dbo].[Tiers]
ALTER COLUMN [EditedOn] datetime NOT NULL
ALTER TABLE [dbo].[Tiers]
ALTER COLUMN [EditedBy] nvarchar(255) NOT NULL

ALTER TABLE [dbo].[Levels]
ALTER COLUMN [CreatedOn] datetime NOT NULL
ALTER TABLE [dbo].[Levels]
ALTER COLUMN [CreatedBy] nvarchar(255) NOT NULL
ALTER TABLE [dbo].[Levels]
ALTER COLUMN [EditedOn] datetime NOT NULL
ALTER TABLE [dbo].[Levels]
ALTER COLUMN [EditedBy] nvarchar(255) NOT NULL

ALTER TABLE [dbo].[Skills]
ALTER COLUMN [CreatedOn] datetime NOT NULL
ALTER TABLE [dbo].[Skills]
ALTER COLUMN [CreatedBy] nvarchar(255) NOT NULL
ALTER TABLE [dbo].[Skills]
ALTER COLUMN [EditedOn] datetime NOT NULL
ALTER TABLE [dbo].[Skills]
ALTER COLUMN [EditedBy] nvarchar(255) NOT NULL

ALTER TABLE [dbo].[Specials]
ALTER COLUMN [CreatedOn] datetime NOT NULL
ALTER TABLE [dbo].[Specials]
ALTER COLUMN [CreatedBy] nvarchar(255) NOT NULL
ALTER TABLE [dbo].[Specials]
ALTER COLUMN [EditedOn] datetime NOT NULL
ALTER TABLE [dbo].[Specials]
ALTER COLUMN [EditedBy] nvarchar(255) NOT NULL

ALTER TABLE [dbo].[StatInfo]
ALTER COLUMN [CreatedOn] datetime NOT NULL
ALTER TABLE [dbo].[StatInfo]
ALTER COLUMN [CreatedBy] nvarchar(255) NOT NULL
ALTER TABLE [dbo].[StatInfo]
ALTER COLUMN [EditedOn] datetime NOT NULL
ALTER TABLE [dbo].[StatInfo]
ALTER COLUMN [EditedBy] nvarchar(255) NOT NULL
GO

-- [name] [date]: [description]