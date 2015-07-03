CREATE TABLE [dbo].[UserLogins]
(
    [UserId]        UNIQUEIDENTIFIER            NOT NULL,
    [LoginProvider] NVARCHAR (128) NOT NULL,
    [ProviderKey]   NVARCHAR (128) NOT NULL,

    CONSTRAINT [PK_UserLogins_UserID_LoginProvider_ProviderKey] PRIMARY KEY CLUSTERED ([UserId] ASC, [LoginProvider] ASC, [ProviderKey] ASC),
    CONSTRAINT [FK_UserLogins_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([UserId]) ON DELETE CASCADE
);

GO
CREATE NONCLUSTERED INDEX [IX_UserLogins_UserID]
    ON [dbo].[UserLogins] ([UserId] ASC);
