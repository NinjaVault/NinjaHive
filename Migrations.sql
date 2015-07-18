-- Alper Aslan 18/07/2015: Limit the nvarchar sizes of names and add unique contraints

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

-- [name] [date]: [description]