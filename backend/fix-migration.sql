IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260709122855_AddExpenseTable')
BEGIN
    CREATE TABLE [Users] (
        [UserId] int NOT NULL IDENTITY,
        [FullName] nvarchar(max) NOT NULL,
        [Email] nvarchar(max) NOT NULL,
        [PasswordHash] nvarchar(max) NOT NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [DateOfBirth] datetime2 NULL,
        [Gender] nvarchar(max) NULL,
        [CreatedDate] datetime2 NOT NULL,
        CONSTRAINT [PK_Users] PRIMARY KEY ([UserId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260709122855_AddExpenseTable')
BEGIN
    CREATE TABLE [Expenses] (
        [ExpenseId] int NOT NULL IDENTITY,
        [UserId] int NOT NULL,
        [Category] nvarchar(max) NOT NULL,
        [Amount] decimal(18,2) NOT NULL,
        [ExpenseDate] datetime2 NOT NULL,
        [Description] nvarchar(max) NULL,
        [CreatedDate] datetime2 NOT NULL,
        CONSTRAINT [PK_Expenses] PRIMARY KEY ([ExpenseId]),
        CONSTRAINT [FK_Expenses_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([UserId]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260709122855_AddExpenseTable')
BEGIN
    CREATE INDEX [IX_Expenses_UserId] ON [Expenses] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260709122855_AddExpenseTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260709122855_AddExpenseTable', N'6.0.36');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260709130928_AddIncomeTable')
BEGIN
    CREATE TABLE [Incomes] (
        [IncomeId] int NOT NULL IDENTITY,
        [UserId] int NOT NULL,
        [Source] nvarchar(max) NOT NULL,
        [Amount] decimal(18,2) NOT NULL,
        [IncomeDate] datetime2 NOT NULL,
        [Description] nvarchar(max) NULL,
        [CreatedDate] datetime2 NOT NULL,
        CONSTRAINT [PK_Incomes] PRIMARY KEY ([IncomeId]),
        CONSTRAINT [FK_Incomes_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([UserId]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260709130928_AddIncomeTable')
BEGIN
    CREATE INDEX [IX_Incomes_UserId] ON [Incomes] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260709130928_AddIncomeTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260709130928_AddIncomeTable', N'6.0.36');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260710065150_AddHealthcareTable')
BEGIN
    CREATE TABLE [Healthcares] (
        [HealthcareId] int NOT NULL IDENTITY,
        [UserId] int NOT NULL,
        [HospitalName] nvarchar(max) NOT NULL,
        [DoctorName] nvarchar(max) NOT NULL,
        [VisitDate] datetime2 NOT NULL,
        [Diagnosis] nvarchar(max) NOT NULL,
        [ConsultationFee] decimal(18,2) NOT NULL,
        [Notes] nvarchar(max) NULL,
        CONSTRAINT [PK_Healthcares] PRIMARY KEY ([HealthcareId]),
        CONSTRAINT [FK_Healthcares_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([UserId]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260710065150_AddHealthcareTable')
BEGIN
    CREATE INDEX [IX_Healthcares_UserId] ON [Healthcares] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260710065150_AddHealthcareTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260710065150_AddHealthcareTable', N'6.0.36');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260710070559_AddMedicineTable')
BEGIN
    CREATE TABLE [Medicines] (
        [MedicineId] int NOT NULL IDENTITY,
        [HealthcareId] int NOT NULL,
        [MedicineName] nvarchar(max) NOT NULL,
        [Dosage] nvarchar(max) NULL,
        [DurationInDays] int NOT NULL,
        [Price] decimal(18,2) NOT NULL,
        CONSTRAINT [PK_Medicines] PRIMARY KEY ([MedicineId]),
        CONSTRAINT [FK_Medicines_Healthcares_HealthcareId] FOREIGN KEY ([HealthcareId]) REFERENCES [Healthcares] ([HealthcareId]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260710070559_AddMedicineTable')
BEGIN
    CREATE INDEX [IX_Medicines_HealthcareId] ON [Medicines] ([HealthcareId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260710070559_AddMedicineTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260710070559_AddMedicineTable', N'6.0.36');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260710074111_AddHealthcareAndMedicineTables')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260710074111_AddHealthcareAndMedicineTables', N'6.0.36');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260710081841_AddAppointmentTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260710081841_AddAppointmentTable', N'6.0.36');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260710082658_UpdateMedicineTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260710082658_UpdateMedicineTable', N'6.0.36');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260710083134_UpdateMedicineStructure')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260710083134_UpdateMedicineStructure', N'6.0.36');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260710102215_InitialCreate')
BEGIN
    CREATE TABLE [Users] (
        [UserId] int NOT NULL IDENTITY,
        [FullName] nvarchar(max) NOT NULL,
        [Email] nvarchar(max) NOT NULL,
        [PasswordHash] nvarchar(max) NOT NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [DateOfBirth] datetime2 NULL,
        [Gender] nvarchar(max) NULL,
        [CreatedDate] datetime2 NOT NULL,
        CONSTRAINT [PK_Users] PRIMARY KEY ([UserId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260710102215_InitialCreate')
BEGIN
    CREATE TABLE [Expenses] (
        [ExpenseId] int NOT NULL IDENTITY,
        [UserId] int NOT NULL,
        [Category] nvarchar(max) NOT NULL,
        [Amount] decimal(18,2) NOT NULL,
        [ExpenseDate] datetime2 NOT NULL,
        [Description] nvarchar(max) NULL,
        [CreatedDate] datetime2 NOT NULL,
        CONSTRAINT [PK_Expenses] PRIMARY KEY ([ExpenseId]),
        CONSTRAINT [FK_Expenses_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([UserId]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260710102215_InitialCreate')
BEGIN
    CREATE TABLE [Healthcares] (
        [HealthcareId] int NOT NULL IDENTITY,
        [UserId] int NOT NULL,
        [HospitalName] nvarchar(max) NOT NULL,
        [DoctorName] nvarchar(max) NOT NULL,
        [VisitDate] datetime2 NOT NULL,
        [Diagnosis] nvarchar(max) NOT NULL,
        [ConsultationFee] decimal(18,2) NOT NULL,
        [Notes] nvarchar(max) NULL,
        CONSTRAINT [PK_Healthcares] PRIMARY KEY ([HealthcareId]),
        CONSTRAINT [FK_Healthcares_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([UserId]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260710102215_InitialCreate')
BEGIN
    CREATE TABLE [Incomes] (
        [IncomeId] int NOT NULL IDENTITY,
        [UserId] int NOT NULL,
        [Source] nvarchar(max) NOT NULL,
        [Amount] decimal(18,2) NOT NULL,
        [IncomeDate] datetime2 NOT NULL,
        [Description] nvarchar(max) NULL,
        [CreatedDate] datetime2 NOT NULL,
        CONSTRAINT [PK_Incomes] PRIMARY KEY ([IncomeId]),
        CONSTRAINT [FK_Incomes_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([UserId]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260710102215_InitialCreate')
BEGIN
    CREATE TABLE [Medicines] (
        [MedicineId] int NOT NULL IDENTITY,
        [HealthcareId] int NOT NULL,
        [MedicineName] nvarchar(max) NOT NULL,
        [Dosage] nvarchar(max) NULL,
        [DurationInDays] int NOT NULL,
        [Price] decimal(18,2) NOT NULL,
        CONSTRAINT [PK_Medicines] PRIMARY KEY ([MedicineId]),
        CONSTRAINT [FK_Medicines_Healthcares_HealthcareId] FOREIGN KEY ([HealthcareId]) REFERENCES [Healthcares] ([HealthcareId]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260710102215_InitialCreate')
BEGIN
    CREATE INDEX [IX_Expenses_UserId] ON [Expenses] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260710102215_InitialCreate')
BEGIN
    CREATE INDEX [IX_Healthcares_UserId] ON [Healthcares] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260710102215_InitialCreate')
BEGIN
    CREATE INDEX [IX_Incomes_UserId] ON [Incomes] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260710102215_InitialCreate')
BEGIN
    CREATE INDEX [IX_Medicines_HealthcareId] ON [Medicines] ([HealthcareId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260710102215_InitialCreate')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260710102215_InitialCreate', N'6.0.36');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260716052404_TestMigration')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Medicines]') AND [c].[name] = N'Dosage');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Medicines] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [Medicines] DROP COLUMN [Dosage];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260716052404_TestMigration')
BEGIN
    ALTER TABLE [Medicines] ADD [AfternoonDose] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260716052404_TestMigration')
BEGIN
    ALTER TABLE [Medicines] ADD [MorningDose] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260716052404_TestMigration')
BEGIN
    ALTER TABLE [Medicines] ADD [NightDose] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260716052404_TestMigration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260716052404_TestMigration', N'6.0.36');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260717105532_AddPasswordResetAndProfilePicture')
BEGIN
    ALTER TABLE [Users] ADD [ProfilePicture] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260717105532_AddPasswordResetAndProfilePicture')
BEGIN
    ALTER TABLE [Expenses] ADD [IsRecurring] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260717105532_AddPasswordResetAndProfilePicture')
BEGIN
    CREATE TABLE [ActivityLogs] (
        [ActivityLogId] int NOT NULL IDENTITY,
        [UserId] int NOT NULL,
        [Action] nvarchar(max) NOT NULL,
        [Details] nvarchar(max) NULL,
        [Timestamp] datetime2 NOT NULL,
        CONSTRAINT [PK_ActivityLogs] PRIMARY KEY ([ActivityLogId]),
        CONSTRAINT [FK_ActivityLogs_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([UserId]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260717105532_AddPasswordResetAndProfilePicture')
BEGIN
    CREATE TABLE [Budgets] (
        [BudgetId] int NOT NULL IDENTITY,
        [UserId] int NOT NULL,
        [Category] nvarchar(max) NOT NULL,
        [MonthlyLimit] decimal(18,2) NOT NULL,
        [Month] int NOT NULL,
        [Year] int NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        CONSTRAINT [PK_Budgets] PRIMARY KEY ([BudgetId]),
        CONSTRAINT [FK_Budgets_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([UserId]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260717105532_AddPasswordResetAndProfilePicture')
BEGIN
    CREATE TABLE [PasswordResetTokens] (
        [Id] int NOT NULL IDENTITY,
        [UserId] int NOT NULL,
        [Token] nvarchar(max) NOT NULL,
        [ExpiresAt] datetime2 NOT NULL,
        [IsUsed] bit NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        CONSTRAINT [PK_PasswordResetTokens] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260717105532_AddPasswordResetAndProfilePicture')
BEGIN
    CREATE INDEX [IX_ActivityLogs_UserId] ON [ActivityLogs] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260717105532_AddPasswordResetAndProfilePicture')
BEGIN
    CREATE INDEX [IX_Budgets_UserId] ON [Budgets] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260717105532_AddPasswordResetAndProfilePicture')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260717105532_AddPasswordResetAndProfilePicture', N'6.0.36');
END;
GO

COMMIT;
GO

