CREATE TABLE [dbo].[UserClaims]
(
    [UserId]     UNIQUEIDENTIFIER      NOT NULL,
    [ClaimID]    UNIQUEIDENTIFIER      NOT NULL DEFAULT NEWID(),
    [ClaimType]  NVARCHAR (MAX)        NULL,
    [ClaimValue] NVARCHAR (MAX)        NULL,

    CONSTRAINT [PK_UserClaims_ClaimID] PRIMARY KEY CLUSTERED ([ClaimID] ASC),
    CONSTRAINT [FK_UserClaims_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([UserId]) ON DELETE CASCADE
);

GO
CREATE NONCLUSTERED INDEX [IX_UserClaims_UserID]
    ON [dbo].[UserClaims] ([UserId] ASC);