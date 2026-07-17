-- Fix: Add ProfilePicture column to Users table
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Users') AND name = 'ProfilePicture')
BEGIN
    ALTER TABLE Users ADD ProfilePicture nvarchar(max) NULL;
END
GO

-- Fix: Create PasswordResetTokens table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'PasswordResetTokens')
BEGIN
    CREATE TABLE PasswordResetTokens (
        Id int NOT NULL IDENTITY,
        UserId int NOT NULL,
        Token nvarchar(max) NOT NULL,
        ExpiresAt datetime2 NOT NULL,
        IsUsed bit NOT NULL,
        CreatedAt datetime2 NOT NULL,
        CONSTRAINT PK_PasswordResetTokens PRIMARY KEY (Id)
    );
END
GO

-- Fix: Reset EF migration history so it matches current model
-- Remove any partial/wrong migration entries
DELETE FROM [__EFMigrationsHistory];
INSERT INTO [__EFMigrationsHistory] (MigrationId, ProductVersion)
VALUES ('20260709122855_AddExpenseTable', '6.0.36');
INSERT INTO [__EFMigrationsHistory] (MigrationId, ProductVersion)
VALUES ('20260716052404_TestMigration', '6.0.36');
GO
