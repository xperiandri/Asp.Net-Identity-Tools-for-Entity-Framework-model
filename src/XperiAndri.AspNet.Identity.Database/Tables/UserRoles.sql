CREATE TABLE [dbo].[UserRoles]
(
    [RoleId] UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
    [Name]   [dbo].[Name]     NOT NULL,

    CONSTRAINT [PK_UserRole_RoleID] PRIMARY KEY CLUSTERED ([RoleId] ASC),
    CONSTRAINT [UK_UserRole_Name] UNIQUE NONCLUSTERED ([Name] ASC)
);
