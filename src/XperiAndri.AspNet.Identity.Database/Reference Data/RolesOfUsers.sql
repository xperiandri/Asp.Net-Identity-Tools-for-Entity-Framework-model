MERGE INTO [dbo].[RolesOfUsers] AS Target
USING (VALUES

    (N'cad9d619-f270-444e-883f-181bdfc249a3', N'22c7a242-7810-4152-8088-f96aeb27b0d2')

) AS Source ([UserID], [RoleID])
ON Target.[UserId] = Source.[UserID] AND Target.[RoleId] = Source.[RoleID]
WHEN NOT MATCHED BY TARGET THEN
-- Insert new rows
INSERT ([UserId], [RoleId])
VALUES ([UserID], [RoleID]);
GO