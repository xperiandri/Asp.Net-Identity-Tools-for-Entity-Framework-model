CREATE TABLE [dbo].[Users]
(
    [UserId]               UNIQUEIDENTIFIER       NOT NULL DEFAULT NEWID(),
    [UserName]             [dbo].[Name]           NOT NULL,
    [Email]                [dbo].[Email]          NULL,
    [EmailConfirmed]       [dbo].[Flag]           NOT NULL,
    [PasswordHash]         NVARCHAR (100)         NULL,
    [SecurityStamp]        NVARCHAR (100)         NULL,
    [PhoneNumber]          [dbo].[Phone]          NULL,
    [PhoneNumberConfirmed] [dbo].[Flag]           NOT NULL,
    [TwoFactorEnabled]     [dbo].[Flag]           NOT NULL,
    [LockoutEndDateUtc]    DATETIME               NULL,
    [LockoutEnabled]       [dbo].[Flag]           NOT NULL,
    [AccessFailedCount]    INT                    NOT NULL,

    CONSTRAINT [PK_Users_UserID] PRIMARY KEY CLUSTERED ([UserId] ASC),
    CONSTRAINT [UK_Users_UserName] UNIQUE NONCLUSTERED ([UserName] ASC)
);
