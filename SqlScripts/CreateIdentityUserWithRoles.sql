--!!!Perform on the Identity database!!!
--This scripts creates an admin user. Manually enter the username and password hash.
--For password hash, see: http://stackoverflow.com/questions/20621950/asp-net-identity-default-password-hasher-how-does-it-work-and-is-it-secure

DECLARE @username nvarchar(256) = '';
DECLARE @passwordHash nvarchar(max) = '';


DECLARE @userId uniqueidentifier = NEWID();
DECLARE @adminId uniqueidentifier;
DECLARE @designerId uniqueidentifier;
DECLARE @memberID uniqueidentifier;

SET @adminId = (
    SELECT [r].[Id]
    FROM [dbo].[AspNetRoles] [r]
    WHERE [r].[Name] = 'Admin');

SET @designerId = (
    SELECT [r].[Id]
    FROM [dbo].[AspNetRoles] [r]
    WHERE [r].[Name] = 'Game Designer');

SET @memberID = (
    SELECT [r].[Id]
    FROM [dbo].[AspNetRoles] [r]
    WHERE [r].[Name] = 'Member');


INSERT INTO [dbo].[AspNetUsers] VALUES (@userId, NULL, 1, @passwordHash, '1514DE22-B493-4F86-8A6D-1AE834980051', NULL, 0, 0, NULL, 0, 0, @username);
INSERT INTO [dbo].[AspNetUserRoles] VALUES (@userId, @adminId);
INSERT INTO [dbo].[AspNetUserRoles] VALUES (@userId, @designerId);
INSERT INTO [dbo].[AspNetUserRoles] VALUES (@userId, @memberID);
GO

