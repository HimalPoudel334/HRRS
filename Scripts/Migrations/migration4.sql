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
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250227064228_Initial'
)
BEGIN
    CREATE TABLE [Anusuchis] (
        [Id] int NOT NULL IDENTITY,
        [SerialNo] nvarchar(max) NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [DafaNo] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Anusuchis] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250227064228_Initial'
)
BEGIN
    CREATE TABLE [HospitalType] (
        [SN] int NOT NULL IDENTITY,
        [HOSP_CODE] nvarchar(max) NOT NULL,
        [HOSP_TYPE] nvarchar(max) NOT NULL,
        [ACTIVE] bit NOT NULL,
        CONSTRAINT [PK_HospitalType] PRIMARY KEY ([SN])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250227064228_Initial'
)
BEGIN
    CREATE TABLE [UserRoles] (
        [Id] int NOT NULL IDENTITY,
        [Title] nvarchar(max) NOT NULL,
        [BedCount] int NULL,
        CONSTRAINT [PK_UserRoles] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250227064228_Initial'
)
BEGIN
    CREATE TABLE [Parichheds] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [SerialNo] nvarchar(max) NOT NULL,
        [AnusuchiId] int NOT NULL,
        CONSTRAINT [PK_Parichheds] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Parichheds_Anusuchis_AnusuchiId] FOREIGN KEY ([AnusuchiId]) REFERENCES [Anusuchis] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250227064228_Initial'
)
BEGIN
    CREATE TABLE [HealthFacilities] (
        [Id] int NOT NULL IDENTITY,
        [FacilityName] nvarchar(max) NOT NULL,
        [FacilityTypeId] int NOT NULL,
        [PanNumber] nvarchar(450) NOT NULL,
        [BedCount] int NOT NULL,
        [SpecialistCount] int NOT NULL,
        [AvailableServices] nvarchar(max) NOT NULL,
        [District] nvarchar(max) NOT NULL,
        [LocalLevel] nvarchar(max) NOT NULL,
        [WardNumber] int NOT NULL,
        [Tole] nvarchar(max) NOT NULL,
        [DateOfInspection] nvarchar(max) NOT NULL,
        [FacilityEmail] nvarchar(max) NULL,
        [FacilityPhoneNumber] nvarchar(max) NULL,
        [FacilityHeadName] nvarchar(max) NULL,
        [FacilityHeadPhone] nvarchar(max) NULL,
        [FacilityHeadEmail] nvarchar(max) NULL,
        [ExecutiveHeadName] nvarchar(max) NULL,
        [ExecutiveHeadMobile] nvarchar(max) NULL,
        [ExecutiveHeadEmail] nvarchar(max) NULL,
        [PermissionReceivedDate] nvarchar(max) NULL,
        [LastRenewedDate] nvarchar(max) NULL,
        [ApporvingAuthority] nvarchar(max) NULL,
        [RenewingAuthority] nvarchar(max) NULL,
        [ApprovalValidityTill] nvarchar(max) NULL,
        [RenewalValidityTill] nvarchar(max) NULL,
        [UpgradeDate] nvarchar(max) NULL,
        [UpgradingAuthority] nvarchar(max) NULL,
        [IsLetterOfIntent] bit NULL,
        [IsExecutionPermission] bit NULL,
        [IsRenewal] bit NULL,
        [IsUpgrade] bit NULL,
        [IsServiceExtension] bit NULL,
        [IsBranchExtension] bit NULL,
        [IsRelocation] bit NULL,
        [Others] nvarchar(max) NULL,
        [ApplicationSubmittedDate] nvarchar(max) NULL,
        [ApplicationSubmittedAuthority] nvarchar(max) NULL,
        CONSTRAINT [PK_HealthFacilities] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_HealthFacilities_HospitalType_FacilityTypeId] FOREIGN KEY ([FacilityTypeId]) REFERENCES [HospitalType] ([SN]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250227064228_Initial'
)
BEGIN
    CREATE TABLE [SubParichheds] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [SerialNo] nvarchar(max) NOT NULL,
        [ParichhedId] int NOT NULL,
        CONSTRAINT [PK_SubParichheds] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_SubParichheds_Parichheds_ParichhedId] FOREIGN KEY ([ParichhedId]) REFERENCES [Parichheds] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250227064228_Initial'
)
BEGIN
    CREATE TABLE [Users] (
        [UserId] bigint NOT NULL IDENTITY,
        [UserName] nvarchar(max) NOT NULL,
        [Password] nvarchar(max) NOT NULL,
        [UserType] nvarchar(max) NOT NULL,
        [HealthFacilityId] int NULL,
        [RoleId] int NULL,
        CONSTRAINT [PK_Users] PRIMARY KEY ([UserId]),
        CONSTRAINT [FK_Users_HealthFacilities_HealthFacilityId] FOREIGN KEY ([HealthFacilityId]) REFERENCES [HealthFacilities] ([Id]),
        CONSTRAINT [FK_Users_UserRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [UserRoles] ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250227064228_Initial'
)
BEGIN
    CREATE TABLE [SubSubParichheds] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [SerialNo] nvarchar(max) NOT NULL,
        [SubParichhedId] int NOT NULL,
        CONSTRAINT [PK_SubSubParichheds] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_SubSubParichheds_SubParichheds_SubParichhedId] FOREIGN KEY ([SubParichhedId]) REFERENCES [SubParichheds] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250227064228_Initial'
)
BEGIN
    CREATE TABLE [MasterStandardEntries] (
        [SubmissionCode] uniqueidentifier NOT NULL,
        [BedCount] int NOT NULL,
        [HealthFacilityId] int NOT NULL,
        [EntryStatus] int NOT NULL,
        [Remarks] nvarchar(max) NULL,
        [ApprovedById] bigint NULL,
        [RejectedById] bigint NULL,
        [SubmissionType] int NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [UpdatedAt] datetime2 NULL,
        CONSTRAINT [PK_MasterStandardEntries] PRIMARY KEY ([SubmissionCode]),
        CONSTRAINT [FK_MasterStandardEntries_HealthFacilities_HealthFacilityId] FOREIGN KEY ([HealthFacilityId]) REFERENCES [HealthFacilities] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_MasterStandardEntries_Users_ApprovedById] FOREIGN KEY ([ApprovedById]) REFERENCES [Users] ([UserId]),
        CONSTRAINT [FK_MasterStandardEntries_Users_RejectedById] FOREIGN KEY ([RejectedById]) REFERENCES [Users] ([UserId])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250227064228_Initial'
)
BEGIN
    CREATE TABLE [Mapdandas] (
        [Id] int NOT NULL IDENTITY,
        [SerialNumber] nvarchar(max) NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [Parimaad] nvarchar(max) NULL,
        [Group] nvarchar(max) NULL,
        [IsAvailableDivided] bit NOT NULL,
        [Is25Active] bit NOT NULL,
        [Is50Active] bit NOT NULL,
        [Is100Active] bit NOT NULL,
        [Is200Active] bit NOT NULL,
        [IsCol5Active] bit NOT NULL,
        [IsCol6Active] bit NOT NULL,
        [IsCol7Active] bit NOT NULL,
        [IsCol8Active] bit NOT NULL,
        [IsCol9Active] bit NOT NULL,
        [Value25] nvarchar(max) NULL,
        [Value50] nvarchar(max) NULL,
        [Value100] nvarchar(max) NULL,
        [Value200] nvarchar(max) NULL,
        [Col5] nvarchar(max) NULL,
        [Col6] nvarchar(max) NULL,
        [Col7] nvarchar(max) NULL,
        [Col8] nvarchar(max) NULL,
        [Col9] nvarchar(max) NULL,
        [Status] bit NOT NULL,
        [AnusuchiId] int NOT NULL,
        [ParichhedId] int NULL,
        [SubParichhedId] int NULL,
        [SubSubParichhedId] int NULL,
        [FormType] int NOT NULL,
        CONSTRAINT [PK_Mapdandas] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Mapdandas_Anusuchis_AnusuchiId] FOREIGN KEY ([AnusuchiId]) REFERENCES [Anusuchis] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Mapdandas_Parichheds_ParichhedId] FOREIGN KEY ([ParichhedId]) REFERENCES [Parichheds] ([Id]),
        CONSTRAINT [FK_Mapdandas_SubParichheds_SubParichhedId] FOREIGN KEY ([SubParichhedId]) REFERENCES [SubParichheds] ([Id]),
        CONSTRAINT [FK_Mapdandas_SubSubParichheds_SubSubParichhedId] FOREIGN KEY ([SubSubParichhedId]) REFERENCES [SubSubParichheds] ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250227064228_Initial'
)
BEGIN
    CREATE TABLE [Approvals] (
        [Id] bigint NOT NULL IDENTITY,
        [Status] nvarchar(max) NOT NULL,
        [Remarks] nvarchar(max) NOT NULL,
        [CreatedById] bigint NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        [EntryId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_Approvals] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Approvals_MasterStandardEntries_EntryId] FOREIGN KEY ([EntryId]) REFERENCES [MasterStandardEntries] ([SubmissionCode]) ON DELETE CASCADE,
        CONSTRAINT [FK_Approvals_Users_CreatedById] FOREIGN KEY ([CreatedById]) REFERENCES [Users] ([UserId]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250227064228_Initial'
)
BEGIN
    CREATE TABLE [HospitalStandardEntrys] (
        [Id] int NOT NULL IDENTITY,
        [Remarks] nvarchar(max) NULL,
        [CreatedAt] datetime2 NOT NULL,
        [UpdatedAt] datetime2 NULL,
        [MasterStandardEntrySubmissionCode] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_HospitalStandardEntrys] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_HospitalStandardEntrys_MasterStandardEntries_MasterStandardEntrySubmissionCode] FOREIGN KEY ([MasterStandardEntrySubmissionCode]) REFERENCES [MasterStandardEntries] ([SubmissionCode]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250227064228_Initial'
)
BEGIN
    CREATE TABLE [SubMapdandas] (
        [Id] int NOT NULL IDENTITY,
        [SerialNumber] nvarchar(max) NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [Parimaad] nvarchar(max) NULL,
        [MapdandaId] int NOT NULL,
        [Status] bit NOT NULL,
        CONSTRAINT [PK_SubMapdandas] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_SubMapdandas_Mapdandas_MapdandaId] FOREIGN KEY ([MapdandaId]) REFERENCES [Mapdandas] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250227064228_Initial'
)
BEGIN
    CREATE TABLE [HospitalStandards] (
        [Id] int NOT NULL IDENTITY,
        [HealthFacilityId] int NOT NULL,
        [MapdandaId] int NOT NULL,
        [IsAvailable] bit NULL,
        [IsApproved] bit NOT NULL,
        [Remarks] nvarchar(max) NULL,
        [FilePath] nvarchar(max) NULL,
        [FiscalYear] nvarchar(max) NULL,
        [CreatedAt] datetime2 NOT NULL,
        [UpdatedAt] datetime2 NULL,
        [Status] bit NOT NULL,
        [StandardEntryId] int NOT NULL,
        CONSTRAINT [PK_HospitalStandards] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_HospitalStandards_HospitalStandardEntrys_StandardEntryId] FOREIGN KEY ([StandardEntryId]) REFERENCES [HospitalStandardEntrys] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_HospitalStandards_Mapdandas_MapdandaId] FOREIGN KEY ([MapdandaId]) REFERENCES [Mapdandas] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250227064228_Initial'
)
BEGIN
    CREATE INDEX [IX_Approvals_CreatedById] ON [Approvals] ([CreatedById]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250227064228_Initial'
)
BEGIN
    CREATE INDEX [IX_Approvals_EntryId] ON [Approvals] ([EntryId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250227064228_Initial'
)
BEGIN
    CREATE INDEX [IX_HealthFacilities_FacilityTypeId] ON [HealthFacilities] ([FacilityTypeId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250227064228_Initial'
)
BEGIN
    CREATE UNIQUE INDEX [IX_HealthFacilities_PanNumber] ON [HealthFacilities] ([PanNumber]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250227064228_Initial'
)
BEGIN
    CREATE INDEX [IX_HospitalStandardEntrys_MasterStandardEntrySubmissionCode] ON [HospitalStandardEntrys] ([MasterStandardEntrySubmissionCode]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250227064228_Initial'
)
BEGIN
    CREATE INDEX [IX_HospitalStandards_MapdandaId] ON [HospitalStandards] ([MapdandaId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250227064228_Initial'
)
BEGIN
    CREATE INDEX [IX_HospitalStandards_StandardEntryId] ON [HospitalStandards] ([StandardEntryId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250227064228_Initial'
)
BEGIN
    CREATE INDEX [IX_Mapdandas_AnusuchiId] ON [Mapdandas] ([AnusuchiId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250227064228_Initial'
)
BEGIN
    CREATE INDEX [IX_Mapdandas_ParichhedId] ON [Mapdandas] ([ParichhedId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250227064228_Initial'
)
BEGIN
    CREATE INDEX [IX_Mapdandas_SubParichhedId] ON [Mapdandas] ([SubParichhedId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250227064228_Initial'
)
BEGIN
    CREATE INDEX [IX_Mapdandas_SubSubParichhedId] ON [Mapdandas] ([SubSubParichhedId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250227064228_Initial'
)
BEGIN
    CREATE INDEX [IX_MasterStandardEntries_ApprovedById] ON [MasterStandardEntries] ([ApprovedById]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250227064228_Initial'
)
BEGIN
    CREATE INDEX [IX_MasterStandardEntries_HealthFacilityId] ON [MasterStandardEntries] ([HealthFacilityId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250227064228_Initial'
)
BEGIN
    CREATE INDEX [IX_MasterStandardEntries_RejectedById] ON [MasterStandardEntries] ([RejectedById]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250227064228_Initial'
)
BEGIN
    CREATE INDEX [IX_Parichheds_AnusuchiId] ON [Parichheds] ([AnusuchiId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250227064228_Initial'
)
BEGIN
    CREATE INDEX [IX_SubMapdandas_MapdandaId] ON [SubMapdandas] ([MapdandaId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250227064228_Initial'
)
BEGIN
    CREATE INDEX [IX_SubParichheds_ParichhedId] ON [SubParichheds] ([ParichhedId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250227064228_Initial'
)
BEGIN
    CREATE INDEX [IX_SubSubParichheds_SubParichhedId] ON [SubSubParichheds] ([SubParichhedId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250227064228_Initial'
)
BEGIN
    CREATE INDEX [IX_Users_HealthFacilityId] ON [Users] ([HealthFacilityId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250227064228_Initial'
)
BEGIN
    CREATE INDEX [IX_Users_RoleId] ON [Users] ([RoleId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250227064228_Initial'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250227064228_Initial', N'9.0.1');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250227085140_AddFacilityAddress'
)
BEGIN
    DECLARE @var sysname;
    SELECT @var = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[HealthFacilities]') AND [c].[name] = N'District');
    IF @var IS NOT NULL EXEC(N'ALTER TABLE [HealthFacilities] DROP CONSTRAINT [' + @var + '];');
    ALTER TABLE [HealthFacilities] DROP COLUMN [District];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250227085140_AddFacilityAddress'
)
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[HealthFacilities]') AND [c].[name] = N'LocalLevel');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [HealthFacilities] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [HealthFacilities] DROP COLUMN [LocalLevel];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250227085140_AddFacilityAddress'
)
BEGIN
    ALTER TABLE [HealthFacilities] ADD [DistrictId] int NOT NULL DEFAULT 0;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250227085140_AddFacilityAddress'
)
BEGIN
    ALTER TABLE [HealthFacilities] ADD [LocalLevelId] int NOT NULL DEFAULT 0;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250227085140_AddFacilityAddress'
)
BEGIN
    CREATE TABLE [Provinces] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Provinces] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250227085140_AddFacilityAddress'
)
BEGIN
    CREATE TABLE [Districts] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [ProvinceId] int NOT NULL,
        CONSTRAINT [PK_Districts] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Districts_Provinces_ProvinceId] FOREIGN KEY ([ProvinceId]) REFERENCES [Provinces] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250227085140_AddFacilityAddress'
)
BEGIN
    CREATE TABLE [LocalLevels] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [DistrictId] int NOT NULL,
        CONSTRAINT [PK_LocalLevels] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_LocalLevels_Districts_DistrictId] FOREIGN KEY ([DistrictId]) REFERENCES [Districts] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250227085140_AddFacilityAddress'
)
BEGIN
    CREATE INDEX [IX_HealthFacilities_DistrictId] ON [HealthFacilities] ([DistrictId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250227085140_AddFacilityAddress'
)
BEGIN
    CREATE INDEX [IX_HealthFacilities_LocalLevelId] ON [HealthFacilities] ([LocalLevelId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250227085140_AddFacilityAddress'
)
BEGIN
    CREATE INDEX [IX_Districts_ProvinceId] ON [Districts] ([ProvinceId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250227085140_AddFacilityAddress'
)
BEGIN
    CREATE INDEX [IX_LocalLevels_DistrictId] ON [LocalLevels] ([DistrictId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250227085140_AddFacilityAddress'
)
BEGIN
    ALTER TABLE [HealthFacilities] ADD CONSTRAINT [FK_HealthFacilities_Districts_DistrictId] FOREIGN KEY ([DistrictId]) REFERENCES [Districts] ([Id]) ON DELETE CASCADE;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250227085140_AddFacilityAddress'
)
BEGIN
    ALTER TABLE [HealthFacilities] ADD CONSTRAINT [FK_HealthFacilities_LocalLevels_LocalLevelId] FOREIGN KEY ([LocalLevelId]) REFERENCES [LocalLevels] ([Id]) ON DELETE CASCADE;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250227085140_AddFacilityAddress'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250227085140_AddFacilityAddress', N'9.0.1');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250301092919_AddNewEntryCol'
)
BEGIN
    ALTER TABLE [MasterStandardEntries] ADD [IsNewEntry] bit NOT NULL DEFAULT CAST(0 AS bit);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250301092919_AddNewEntryCol'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250301092919_AddNewEntryCol', N'9.0.1');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250302101342_AddRegisReq'
)
BEGIN
    ALTER TABLE [HealthFacilities] DROP CONSTRAINT [FK_HealthFacilities_Districts_DistrictId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250302101342_AddRegisReq'
)
BEGIN
    ALTER TABLE [HealthFacilities] DROP CONSTRAINT [FK_HealthFacilities_HospitalType_FacilityTypeId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250302101342_AddRegisReq'
)
BEGIN
    ALTER TABLE [HealthFacilities] DROP CONSTRAINT [FK_HealthFacilities_LocalLevels_LocalLevelId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250302101342_AddRegisReq'
)
BEGIN
    ALTER TABLE [MasterStandardEntries] DROP CONSTRAINT [FK_MasterStandardEntries_HealthFacilities_HealthFacilityId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250302101342_AddRegisReq'
)
BEGIN
    ALTER TABLE [Users] DROP CONSTRAINT [FK_Users_HealthFacilities_HealthFacilityId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250302101342_AddRegisReq'
)
BEGIN
    ALTER TABLE [HealthFacilities] DROP CONSTRAINT [PK_HealthFacilities];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250302101342_AddRegisReq'
)
BEGIN
    EXEC sp_rename N'[HealthFacilities]', N'HealthFacility', 'OBJECT';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250302101342_AddRegisReq'
)
BEGIN
    EXEC sp_rename N'[HealthFacility].[IX_HealthFacilities_PanNumber]', N'IX_HealthFacility_PanNumber', 'INDEX';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250302101342_AddRegisReq'
)
BEGIN
    EXEC sp_rename N'[HealthFacility].[IX_HealthFacilities_LocalLevelId]', N'IX_HealthFacility_LocalLevelId', 'INDEX';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250302101342_AddRegisReq'
)
BEGIN
    EXEC sp_rename N'[HealthFacility].[IX_HealthFacilities_FacilityTypeId]', N'IX_HealthFacility_FacilityTypeId', 'INDEX';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250302101342_AddRegisReq'
)
BEGIN
    EXEC sp_rename N'[HealthFacility].[IX_HealthFacilities_DistrictId]', N'IX_HealthFacility_DistrictId', 'INDEX';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250302101342_AddRegisReq'
)
BEGIN
    ALTER TABLE [Users] ADD [IsFirstLogin] bit NOT NULL DEFAULT CAST(0 AS bit);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250302101342_AddRegisReq'
)
BEGIN
    ALTER TABLE [HealthFacility] ADD CONSTRAINT [PK_HealthFacility] PRIMARY KEY ([Id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250302101342_AddRegisReq'
)
BEGIN
    CREATE TABLE [RegistrationRequests] (
        [Id] int NOT NULL IDENTITY,
        [HealthFacilityId] int NOT NULL,
        [Status] int NOT NULL,
        [HandledById] bigint NULL,
        [CreatedAt] datetime2 NOT NULL,
        [UpdatedAt] datetime2 NOT NULL,
        [Remarks] nvarchar(max) NULL,
        CONSTRAINT [PK_RegistrationRequests] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_RegistrationRequests_HealthFacility_HealthFacilityId] FOREIGN KEY ([HealthFacilityId]) REFERENCES [HealthFacility] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_RegistrationRequests_Users_HandledById] FOREIGN KEY ([HandledById]) REFERENCES [Users] ([UserId])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250302101342_AddRegisReq'
)
BEGIN
    CREATE INDEX [IX_RegistrationRequests_HandledById] ON [RegistrationRequests] ([HandledById]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250302101342_AddRegisReq'
)
BEGIN
    CREATE INDEX [IX_RegistrationRequests_HealthFacilityId] ON [RegistrationRequests] ([HealthFacilityId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250302101342_AddRegisReq'
)
BEGIN
    ALTER TABLE [HealthFacility] ADD CONSTRAINT [FK_HealthFacility_Districts_DistrictId] FOREIGN KEY ([DistrictId]) REFERENCES [Districts] ([Id]) ON DELETE CASCADE;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250302101342_AddRegisReq'
)
BEGIN
    ALTER TABLE [HealthFacility] ADD CONSTRAINT [FK_HealthFacility_HospitalType_FacilityTypeId] FOREIGN KEY ([FacilityTypeId]) REFERENCES [HospitalType] ([SN]) ON DELETE CASCADE;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250302101342_AddRegisReq'
)
BEGIN
    ALTER TABLE [HealthFacility] ADD CONSTRAINT [FK_HealthFacility_LocalLevels_LocalLevelId] FOREIGN KEY ([LocalLevelId]) REFERENCES [LocalLevels] ([Id]) ON DELETE CASCADE;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250302101342_AddRegisReq'
)
BEGIN
    ALTER TABLE [MasterStandardEntries] ADD CONSTRAINT [FK_MasterStandardEntries_HealthFacility_HealthFacilityId] FOREIGN KEY ([HealthFacilityId]) REFERENCES [HealthFacility] ([Id]) ON DELETE CASCADE;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250302101342_AddRegisReq'
)
BEGIN
    ALTER TABLE [Users] ADD CONSTRAINT [FK_Users_HealthFacility_HealthFacilityId] FOREIGN KEY ([HealthFacilityId]) REFERENCES [HealthFacility] ([Id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250302101342_AddRegisReq'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250302101342_AddRegisReq', N'9.0.1');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250302103950_AddTempHealthFacility'
)
BEGIN
    ALTER TABLE [HealthFacility] DROP CONSTRAINT [FK_HealthFacility_Districts_DistrictId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250302103950_AddTempHealthFacility'
)
BEGIN
    ALTER TABLE [HealthFacility] DROP CONSTRAINT [FK_HealthFacility_HospitalType_FacilityTypeId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250302103950_AddTempHealthFacility'
)
BEGIN
    ALTER TABLE [HealthFacility] DROP CONSTRAINT [FK_HealthFacility_LocalLevels_LocalLevelId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250302103950_AddTempHealthFacility'
)
BEGIN
    ALTER TABLE [MasterStandardEntries] DROP CONSTRAINT [FK_MasterStandardEntries_HealthFacility_HealthFacilityId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250302103950_AddTempHealthFacility'
)
BEGIN
    ALTER TABLE [RegistrationRequests] DROP CONSTRAINT [FK_RegistrationRequests_HealthFacility_HealthFacilityId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250302103950_AddTempHealthFacility'
)
BEGIN
    ALTER TABLE [Users] DROP CONSTRAINT [FK_Users_HealthFacility_HealthFacilityId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250302103950_AddTempHealthFacility'
)
BEGIN
    ALTER TABLE [HealthFacility] DROP CONSTRAINT [PK_HealthFacility];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250302103950_AddTempHealthFacility'
)
BEGIN
    EXEC sp_rename N'[HealthFacility]', N'HealthFacilities', 'OBJECT';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250302103950_AddTempHealthFacility'
)
BEGIN
    EXEC sp_rename N'[HealthFacilities].[IX_HealthFacility_PanNumber]', N'IX_HealthFacilities_PanNumber', 'INDEX';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250302103950_AddTempHealthFacility'
)
BEGIN
    EXEC sp_rename N'[HealthFacilities].[IX_HealthFacility_LocalLevelId]', N'IX_HealthFacilities_LocalLevelId', 'INDEX';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250302103950_AddTempHealthFacility'
)
BEGIN
    EXEC sp_rename N'[HealthFacilities].[IX_HealthFacility_FacilityTypeId]', N'IX_HealthFacilities_FacilityTypeId', 'INDEX';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250302103950_AddTempHealthFacility'
)
BEGIN
    EXEC sp_rename N'[HealthFacilities].[IX_HealthFacility_DistrictId]', N'IX_HealthFacilities_DistrictId', 'INDEX';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250302103950_AddTempHealthFacility'
)
BEGIN
    ALTER TABLE [HealthFacilities] ADD CONSTRAINT [PK_HealthFacilities] PRIMARY KEY ([Id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250302103950_AddTempHealthFacility'
)
BEGIN
    CREATE TABLE [TempHealthFacilities] (
        [Id] int NOT NULL IDENTITY,
        [FacilityName] nvarchar(max) NOT NULL,
        [FacilityTypeId] int NOT NULL,
        [PanNumber] nvarchar(max) NOT NULL,
        [BedCount] int NOT NULL,
        [SpecialistCount] int NOT NULL,
        [AvailableServices] nvarchar(max) NOT NULL,
        [DistrictId] int NOT NULL,
        [LocalLevelId] int NOT NULL,
        [WardNumber] int NOT NULL,
        [Tole] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_TempHealthFacilities] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_TempHealthFacilities_Districts_DistrictId] FOREIGN KEY ([DistrictId]) REFERENCES [Districts] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_TempHealthFacilities_HospitalType_FacilityTypeId] FOREIGN KEY ([FacilityTypeId]) REFERENCES [HospitalType] ([SN]) ON DELETE CASCADE,
        CONSTRAINT [FK_TempHealthFacilities_LocalLevels_LocalLevelId] FOREIGN KEY ([LocalLevelId]) REFERENCES [LocalLevels] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250302103950_AddTempHealthFacility'
)
BEGIN
    CREATE INDEX [IX_TempHealthFacilities_DistrictId] ON [TempHealthFacilities] ([DistrictId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250302103950_AddTempHealthFacility'
)
BEGIN
    CREATE INDEX [IX_TempHealthFacilities_FacilityTypeId] ON [TempHealthFacilities] ([FacilityTypeId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250302103950_AddTempHealthFacility'
)
BEGIN
    CREATE INDEX [IX_TempHealthFacilities_LocalLevelId] ON [TempHealthFacilities] ([LocalLevelId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250302103950_AddTempHealthFacility'
)
BEGIN
    ALTER TABLE [HealthFacilities] ADD CONSTRAINT [FK_HealthFacilities_Districts_DistrictId] FOREIGN KEY ([DistrictId]) REFERENCES [Districts] ([Id]) ON DELETE CASCADE;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250302103950_AddTempHealthFacility'
)
BEGIN
    ALTER TABLE [HealthFacilities] ADD CONSTRAINT [FK_HealthFacilities_HospitalType_FacilityTypeId] FOREIGN KEY ([FacilityTypeId]) REFERENCES [HospitalType] ([SN]) ON DELETE CASCADE;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250302103950_AddTempHealthFacility'
)
BEGIN
    ALTER TABLE [HealthFacilities] ADD CONSTRAINT [FK_HealthFacilities_LocalLevels_LocalLevelId] FOREIGN KEY ([LocalLevelId]) REFERENCES [LocalLevels] ([Id]) ON DELETE CASCADE;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250302103950_AddTempHealthFacility'
)
BEGIN
    ALTER TABLE [MasterStandardEntries] ADD CONSTRAINT [FK_MasterStandardEntries_HealthFacilities_HealthFacilityId] FOREIGN KEY ([HealthFacilityId]) REFERENCES [HealthFacilities] ([Id]) ON DELETE CASCADE;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250302103950_AddTempHealthFacility'
)
BEGIN
    ALTER TABLE [RegistrationRequests] ADD CONSTRAINT [FK_RegistrationRequests_TempHealthFacilities_HealthFacilityId] FOREIGN KEY ([HealthFacilityId]) REFERENCES [TempHealthFacilities] ([Id]) ON DELETE CASCADE;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250302103950_AddTempHealthFacility'
)
BEGIN
    ALTER TABLE [Users] ADD CONSTRAINT [FK_Users_HealthFacilities_HealthFacilityId] FOREIGN KEY ([HealthFacilityId]) REFERENCES [HealthFacilities] ([Id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250302103950_AddTempHealthFacility'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250302103950_AddTempHealthFacility', N'9.0.1');
END;

COMMIT;
GO

