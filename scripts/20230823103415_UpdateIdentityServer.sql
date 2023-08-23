BEGIN TRANSACTION;
GO

ALTER TABLE [Clients] ADD [DPoPClockSkew] time NOT NULL DEFAULT '00:00:00';
GO

ALTER TABLE [Clients] ADD [DPoPValidationMode] int NOT NULL DEFAULT 0;
GO

ALTER TABLE [Clients] ADD [InitiateLoginUri] nvarchar(2000) NULL;
GO

ALTER TABLE [Clients] ADD [RequireDPoP] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230823103415_UpdateIdentityServer', N'6.0.8');
GO

COMMIT;
GO

