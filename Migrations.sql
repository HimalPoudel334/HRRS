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
CREATE TABLE [Anusuchis] (
    [Id] int NOT NULL IDENTITY,
    [SerialNo] nvarchar(max) NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [DafaNo] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Anusuchis] PRIMARY KEY ([Id])
);

CREATE TABLE [HealthFacilities] (
    [Id] int NOT NULL IDENTITY,
    [FacilityName] nvarchar(max) NOT NULL,
    [FacilityType] nvarchar(max) NOT NULL,
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
    CONSTRAINT [PK_HealthFacilities] PRIMARY KEY ([Id])
);

CREATE TABLE [Parichheds] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [SerialNo] nvarchar(max) NOT NULL,
    [AnusuchiId] int NOT NULL,
    CONSTRAINT [PK_Parichheds] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Parichheds_Anusuchis_AnusuchiId] FOREIGN KEY ([AnusuchiId]) REFERENCES [Anusuchis] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [MasterStandardEntries] (
    [SubmissionCode] uniqueidentifier NOT NULL,
    [HealthFacilityId] int NOT NULL,
    [EntryStatus] int NOT NULL,
    [Remarks] nvarchar(max) NULL,
    [SubmissionType] int NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedAt] datetime2 NULL,
    CONSTRAINT [PK_MasterStandardEntries] PRIMARY KEY ([SubmissionCode]),
    CONSTRAINT [FK_MasterStandardEntries_HealthFacilities_HealthFacilityId] FOREIGN KEY ([HealthFacilityId]) REFERENCES [HealthFacilities] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [Users] (
    [UserId] bigint NOT NULL IDENTITY,
    [UserName] nvarchar(max) NOT NULL,
    [Password] nvarchar(max) NOT NULL,
    [UserType] nvarchar(max) NOT NULL,
    [HealthFacilityId] int NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([UserId]),
    CONSTRAINT [FK_Users_HealthFacilities_HealthFacilityId] FOREIGN KEY ([HealthFacilityId]) REFERENCES [HealthFacilities] ([Id])
);

CREATE TABLE [SubParichheds] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [SerialNo] nvarchar(max) NOT NULL,
    [ParichhedId] int NOT NULL,
    CONSTRAINT [PK_SubParichheds] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SubParichheds_Parichheds_ParichhedId] FOREIGN KEY ([ParichhedId]) REFERENCES [Parichheds] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [HospitalStandardEntrys] (
    [Id] int NOT NULL IDENTITY,
    [Remarks] nvarchar(max) NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedAt] datetime2 NULL,
    [Status] int NOT NULL,
    [SubmissionType] int NOT NULL,
    [MasterStandardEntrySubmissionCode] uniqueidentifier NOT NULL,
    [StandardEntryMasterId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_HospitalStandardEntrys] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_HospitalStandardEntrys_MasterStandardEntries_MasterStandardEntrySubmissionCode] FOREIGN KEY ([MasterStandardEntrySubmissionCode]) REFERENCES [MasterStandardEntries] ([SubmissionCode]) ON DELETE CASCADE
);

CREATE TABLE [SubSubParichheds] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [SerialNo] nvarchar(max) NOT NULL,
    [SubParichhedId] int NOT NULL,
    CONSTRAINT [PK_SubSubParichheds] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SubSubParichheds_SubParichheds_SubParichhedId] FOREIGN KEY ([SubParichhedId]) REFERENCES [SubParichheds] ([Id]) ON DELETE CASCADE
);

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
    [Value25] nvarchar(max) NULL,
    [Value50] nvarchar(max) NULL,
    [Value100] nvarchar(max) NULL,
    [Value200] nvarchar(max) NULL,
    [Status] bit NOT NULL,
    [AnusuchiId] int NOT NULL,
    [ParichhedId] int NULL,
    [SubParichhedId] int NULL,
    [SubSubParichhedId] int NULL,
    CONSTRAINT [PK_Mapdandas] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Mapdandas_Anusuchis_AnusuchiId] FOREIGN KEY ([AnusuchiId]) REFERENCES [Anusuchis] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Mapdandas_Parichheds_ParichhedId] FOREIGN KEY ([ParichhedId]) REFERENCES [Parichheds] ([Id]),
    CONSTRAINT [FK_Mapdandas_SubParichheds_SubParichhedId] FOREIGN KEY ([SubParichhedId]) REFERENCES [SubParichheds] ([Id]),
    CONSTRAINT [FK_Mapdandas_SubSubParichheds_SubSubParichhedId] FOREIGN KEY ([SubSubParichhedId]) REFERENCES [SubSubParichheds] ([Id])
);

CREATE TABLE [HospitalStandards] (
    [Id] int NOT NULL IDENTITY,
    [HealthFacilityId] int NOT NULL,
    [MapdandaId] int NOT NULL,
    [IsAvailable] bit NULL,
    [Has25] bit NULL,
    [Has50] bit NULL,
    [Has100] bit NULL,
    [Has200] bit NULL,
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

CREATE UNIQUE INDEX [IX_HealthFacilities_PanNumber] ON [HealthFacilities] ([PanNumber]);

CREATE INDEX [IX_HospitalStandardEntrys_MasterStandardEntrySubmissionCode] ON [HospitalStandardEntrys] ([MasterStandardEntrySubmissionCode]);

CREATE INDEX [IX_HospitalStandards_MapdandaId] ON [HospitalStandards] ([MapdandaId]);

CREATE INDEX [IX_HospitalStandards_StandardEntryId] ON [HospitalStandards] ([StandardEntryId]);

CREATE INDEX [IX_Mapdandas_AnusuchiId] ON [Mapdandas] ([AnusuchiId]);

CREATE INDEX [IX_Mapdandas_ParichhedId] ON [Mapdandas] ([ParichhedId]);

CREATE INDEX [IX_Mapdandas_SubParichhedId] ON [Mapdandas] ([SubParichhedId]);

CREATE INDEX [IX_Mapdandas_SubSubParichhedId] ON [Mapdandas] ([SubSubParichhedId]);

CREATE INDEX [IX_MasterStandardEntries_HealthFacilityId] ON [MasterStandardEntries] ([HealthFacilityId]);

CREATE INDEX [IX_Parichheds_AnusuchiId] ON [Parichheds] ([AnusuchiId]);

CREATE INDEX [IX_SubMapdandas_MapdandaId] ON [SubMapdandas] ([MapdandaId]);

CREATE INDEX [IX_SubParichheds_ParichhedId] ON [SubParichheds] ([ParichhedId]);

CREATE INDEX [IX_SubSubParichheds_SubParichhedId] ON [SubSubParichheds] ([SubParichhedId]);

CREATE INDEX [IX_Users_HealthFacilityId] ON [Users] ([HealthFacilityId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250216103634_InitialCreate', N'9.0.1');

DECLARE @var sysname;
SELECT @var = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[HospitalStandardEntrys]') AND [c].[name] = N'StandardEntryMasterId');
IF @var IS NOT NULL EXEC(N'ALTER TABLE [HospitalStandardEntrys] DROP CONSTRAINT [' + @var + '];');
ALTER TABLE [HospitalStandardEntrys] DROP COLUMN [StandardEntryMasterId];

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250216132800_RemoveCol', N'9.0.1');

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250216162043_UploadTo25', N'9.0.1');

COMMIT;
GO

