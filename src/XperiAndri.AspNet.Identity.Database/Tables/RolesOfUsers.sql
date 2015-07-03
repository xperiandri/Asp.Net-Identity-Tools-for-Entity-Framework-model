CREATE TABLE [dbo].[RolesOfUsers]
(
    [UserId] UNIQUEIDENTIFIER NOT NULL,
    [RoleId] UNIQUEIDENTIFIER NOT NULL,

    CONSTRAINT [PK_RolesOfUsers_UserID_RoleID] PRIMARY KEY CLUSTERED ([UserId] ASC, [RoleId] ASC),
    CONSTRAINT [FK_RolesOfUsers_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([UserId]) ON DELETE CASCADE,
    CONSTRAINT [FK_RolesOfUsers_UserRole] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[UserRoles] ([RoleId]) ON DELETE CASCADE
);

GO
CREATE NONCLUSTERED INDEX [IX_RolesOfUsers_UserID]
    ON [dbo].[RolesOfUsers] ([UserId] ASC);

GO
CREATE NONCLUSTERED INDEX [IX_RolesOfUsers_RoleID]
    ON [dbo].[RolesOfUsers] ([RoleId] ASC);
