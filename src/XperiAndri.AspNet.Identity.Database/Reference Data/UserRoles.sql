MERGE INTO [dbo].[UserRoles] AS Target
USING (VALUES

    (N'22c7a242-7810-4152-8088-f96aeb27b0d2', N'Administrator'),
    (N'f77ebb1c-afbd-4ff1-906f-c567fa6cded0', N'Moderator')

) AS Source ([RoleID], [Name])
ON Target.[RoleId] = Source.[RoleID]
-- Update matched rows
WHEN MATCHED THEN
UPDATE SET [Name] = Source.[Name]
-- Insert new rows
WHEN NOT MATCHED BY TARGET THEN
INSERT ([RoleId], [Name])
VALUES ([RoleID], [Name])
-- Delete rows that are in the target but not the source
WHEN NOT MATCHED BY SOURCE THEN
DELETE;
GO