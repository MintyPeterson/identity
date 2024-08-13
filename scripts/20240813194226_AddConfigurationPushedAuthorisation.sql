BEGIN TRANSACTION;
GO

ALTER TABLE [Clients] ADD [PushedAuthorizationLifetime] int NULL;
GO

ALTER TABLE [Clients] ADD [RequirePushedAuthorization] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240813194226_AddConfigurationPushedAuthorisation', N'8.0.7');
GO

COMMIT;
GO

