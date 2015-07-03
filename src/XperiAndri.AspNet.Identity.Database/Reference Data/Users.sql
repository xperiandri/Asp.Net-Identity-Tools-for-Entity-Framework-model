MERGE INTO [dbo].[Users] AS Target
USING (VALUES

    (N'cad9d619-f270-444e-883f-181bdfc249a3', N'admin', N'admin@example.com', 0, N'ACe+kHUdH61ms8NbkXSCXyV34CEP7tjfj93JrtlKRPfShGurFdAujQrmbVA7J9MDbg==',
        N'9771f91d-b4a0-45e0-8971-899b907c5863', NULL, 0, 0, NULL, 0, 0),
    (N'85245dac-2c43-49bc-bdfe-2f49a8de9d7b', N'demo', N'demo@example.com', 0, N'ACe+kHUdH61ms8NbkXSCXyV34CEP7tjfj93JrtlKRPfShGurFdAujQrmbVA7J9MDbg==',
        N'9771f91d-b4a0-45e0-8971-899b907c5863', NULL, 0, 0, NULL, 0, 0)

) AS Source (
    [UserID],
    [UserName],
    [Email],
    [EmailConfirmed],
    [PasswordHash],
    [SecurityStamp],
    [PhoneNumber],
    [PhoneNumberConfirmed],
    [TwoFactorEnabled],
    [LockoutEndDateUtc],
    [LockoutEnabled],
    [AccessFailedCount]
)
ON Target.[UserId] = Source.[UserID]
-- Update matched rows
WHEN MATCHED THEN
UPDATE SET
    [UserName] = Source.[UserName],
    [Email] = Source.[Email],
    [EmailConfirmed] = Source.[EmailConfirmed],
    [PasswordHash] = Source.[PasswordHash],
    [SecurityStamp] = Source.[SecurityStamp],
    [PhoneNumber] = Source.[PhoneNumber],
    [PhoneNumberConfirmed] = Source.[PhoneNumberConfirmed],
    [TwoFactorEnabled] = Source.[TwoFactorEnabled],
    [LockoutEndDateUtc] = Source.[LockoutEndDateUtc],
    [LockoutEnabled] = Source.[LockoutEnabled],
    [AccessFailedCount] = Source.[AccessFailedCount]
-- Insert new rows
WHEN NOT MATCHED BY TARGET THEN
INSERT (
    [UserId],
    [UserName],
    [Email],
    [EmailConfirmed],
    [PasswordHash],
    [SecurityStamp],
    [PhoneNumber],
    [PhoneNumberConfirmed],
    [TwoFactorEnabled],
    [LockoutEndDateUtc],
    [LockoutEnabled],
    [AccessFailedCount]
)
VALUES (
    [UserID],
    [UserName],
    [Email],
    [EmailConfirmed],
    [PasswordHash],
    [SecurityStamp],
    [PhoneNumber],
    [PhoneNumberConfirmed],
    [TwoFactorEnabled],
    [LockoutEndDateUtc],
    [LockoutEnabled],
    [AccessFailedCount]
);
GO
