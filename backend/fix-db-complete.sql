-- Fix: Add missing ProfilePicture column to Users (IF NOT EXISTS)
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Users') AND name = 'ProfilePicture')
BEGIN
    ALTER TABLE Users ADD ProfilePicture nvarchar(max) NULL;
    PRINT 'Added ProfilePicture column to Users';
END
ELSE
    PRINT 'ProfilePicture column already exists';

-- Fix: Add missing IsRecurring column to Expenses (IF NOT EXISTS)
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Expenses') AND name = 'IsRecurring')
BEGIN
    ALTER TABLE Expenses ADD IsRecurring bit NOT NULL DEFAULT 0;
    PRINT 'Added IsRecurring column to Expenses';
END
ELSE
    PRINT 'IsRecurring column already exists';

-- Fix: Create PasswordResetTokens table (IF NOT EXISTS)
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'PasswordResetTokens')
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
    PRINT 'Created PasswordResetTokens table';
END
ELSE
    PRINT 'PasswordResetTokens table already exists';

-- Fix: Create ActivityLogs table (IF NOT EXISTS)
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'ActivityLogs')
BEGIN
    CREATE TABLE ActivityLogs (
        ActivityLogId int NOT NULL IDENTITY,
        UserId int NOT NULL,
        Action nvarchar(max) NOT NULL,
        Details nvarchar(max) NULL,
        Timestamp datetime2 NOT NULL,
        CONSTRAINT PK_ActivityLogs PRIMARY KEY (ActivityLogId),
        CONSTRAINT FK_ActivityLogs_Users_UserId FOREIGN KEY (UserId) REFERENCES Users(UserId) ON DELETE CASCADE
    );
    CREATE INDEX IX_ActivityLogs_UserId ON ActivityLogs(UserId);
    PRINT 'Created ActivityLogs table';
END
ELSE
    PRINT 'ActivityLogs table already exists';

-- Fix: Create Budgets table (IF NOT EXISTS)
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'Budgets')
BEGIN
    CREATE TABLE Budgets (
        BudgetId int NOT NULL IDENTITY,
        UserId int NOT NULL,
        Category nvarchar(max) NOT NULL,
        MonthlyLimit decimal(18,2) NOT NULL,
        Month int NOT NULL,
        Year int NOT NULL,
        CreatedDate datetime2 NOT NULL,
        CONSTRAINT PK_Budgets PRIMARY KEY (BudgetId),
        CONSTRAINT FK_Budgets_Users_UserId FOREIGN KEY (UserId) REFERENCES Users(UserId) ON DELETE CASCADE
    );
    CREATE INDEX IX_Budgets_UserId ON Budgets(UserId);
    PRINT 'Created Budgets table';
END
ELSE
    PRINT 'Budgets table already exists';

-- Reset EF Migrations History so EF knows the current state
-- This allows dotnet ef database update to work going forward
PRINT 'Done! All missing tables and columns have been added.';
