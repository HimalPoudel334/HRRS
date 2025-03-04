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

CREATE TABLE [HospitalType] (
    [SN] int NOT NULL IDENTITY,
    [HOSP_CODE] nvarchar(max) NOT NULL,
    [HOSP_TYPE] nvarchar(max) NOT NULL,
    [ACTIVE] bit NOT NULL,
    CONSTRAINT [PK_HospitalType] PRIMARY KEY ([SN])
);

CREATE TABLE [Provinces] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Provinces] PRIMARY KEY ([Id])
);

CREATE TABLE [UserRoles] (
    [Id] int NOT NULL IDENTITY,
    [Title] nvarchar(max) NOT NULL,
    [BedCount] int NULL,
    CONSTRAINT [PK_UserRoles] PRIMARY KEY ([Id])
);

CREATE TABLE [Parichheds] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [SerialNo] nvarchar(max) NOT NULL,
    [AnusuchiId] int NOT NULL,
    CONSTRAINT [PK_Parichheds] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Parichheds_Anusuchis_AnusuchiId] FOREIGN KEY ([AnusuchiId]) REFERENCES [Anusuchis] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [Districts] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [ProvinceId] int NOT NULL,
    CONSTRAINT [PK_Districts] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Districts_Provinces_ProvinceId] FOREIGN KEY ([ProvinceId]) REFERENCES [Provinces] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [SubParichheds] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [SerialNo] nvarchar(max) NOT NULL,
    [ParichhedId] int NOT NULL,
    CONSTRAINT [PK_SubParichheds] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SubParichheds_Parichheds_ParichhedId] FOREIGN KEY ([ParichhedId]) REFERENCES [Parichheds] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [LocalLevels] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [DistrictId] int NOT NULL,
    CONSTRAINT [PK_LocalLevels] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_LocalLevels_Districts_DistrictId] FOREIGN KEY ([DistrictId]) REFERENCES [Districts] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [SubSubParichheds] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [SerialNo] nvarchar(max) NOT NULL,
    [SubParichhedId] int NOT NULL,
    CONSTRAINT [PK_SubSubParichheds] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SubSubParichheds_SubParichheds_SubParichhedId] FOREIGN KEY ([SubParichhedId]) REFERENCES [SubParichheds] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [HealthFacilities] (
    [Id] int NOT NULL IDENTITY,
    [FacilityName] nvarchar(max) NOT NULL,
    [FacilityTypeId] int NOT NULL,
    [PanNumber] nvarchar(450) NOT NULL,
    [BedCount] int NOT NULL,
    [SpecialistCount] int NOT NULL,
    [AvailableServices] nvarchar(max) NOT NULL,
    [DistrictId] int NOT NULL,
    [LocalLevelId] int NOT NULL,
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
    CONSTRAINT [FK_HealthFacilities_Districts_DistrictId] FOREIGN KEY ([DistrictId]) REFERENCES [Districts] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_HealthFacilities_HospitalType_FacilityTypeId] FOREIGN KEY ([FacilityTypeId]) REFERENCES [HospitalType] ([SN]) ON DELETE CASCADE,
    CONSTRAINT [FK_HealthFacilities_LocalLevels_LocalLevelId] FOREIGN KEY ([LocalLevelId]) REFERENCES [LocalLevels] ([Id])
);

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
    CONSTRAINT [FK_TempHealthFacilities_LocalLevels_LocalLevelId] FOREIGN KEY ([LocalLevelId]) REFERENCES [LocalLevels] ([Id])
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

CREATE TABLE [Users] (
    [UserId] bigint NOT NULL IDENTITY,
    [UserName] nvarchar(max) NOT NULL,
    [Password] nvarchar(max) NOT NULL,
    [UserType] nvarchar(max) NOT NULL,
    [HealthFacilityId] int NULL,
    [RoleId] int NULL,
    [IsFirstLogin] bit NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([UserId]),
    CONSTRAINT [FK_Users_HealthFacilities_HealthFacilityId] FOREIGN KEY ([HealthFacilityId]) REFERENCES [HealthFacilities] ([Id]),
    CONSTRAINT [FK_Users_UserRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [UserRoles] ([Id])
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
    [IsNewEntry] bit NOT NULL,
    CONSTRAINT [PK_MasterStandardEntries] PRIMARY KEY ([SubmissionCode]),
    CONSTRAINT [FK_MasterStandardEntries_HealthFacilities_HealthFacilityId] FOREIGN KEY ([HealthFacilityId]) REFERENCES [HealthFacilities] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_MasterStandardEntries_Users_ApprovedById] FOREIGN KEY ([ApprovedById]) REFERENCES [Users] ([UserId]),
    CONSTRAINT [FK_MasterStandardEntries_Users_RejectedById] FOREIGN KEY ([RejectedById]) REFERENCES [Users] ([UserId])
);

CREATE TABLE [RegistrationRequests] (
    [Id] int NOT NULL IDENTITY,
    [HealthFacilityId] int NOT NULL,
    [Status] int NOT NULL,
    [HandledById] bigint NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [Remarks] nvarchar(max) NULL,
    CONSTRAINT [PK_RegistrationRequests] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_RegistrationRequests_TempHealthFacilities_HealthFacilityId] FOREIGN KEY ([HealthFacilityId]) REFERENCES [TempHealthFacilities] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_RegistrationRequests_Users_HandledById] FOREIGN KEY ([HandledById]) REFERENCES [Users] ([UserId])
);

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

CREATE TABLE [HospitalStandardEntrys] (
    [Id] int NOT NULL IDENTITY,
    [Remarks] nvarchar(max) NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedAt] datetime2 NULL,
    [MasterStandardEntrySubmissionCode] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_HospitalStandardEntrys] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_HospitalStandardEntrys_MasterStandardEntries_MasterStandardEntrySubmissionCode] FOREIGN KEY ([MasterStandardEntrySubmissionCode]) REFERENCES [MasterStandardEntries] ([SubmissionCode]) ON DELETE CASCADE
);

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

CREATE INDEX [IX_Approvals_CreatedById] ON [Approvals] ([CreatedById]);

CREATE INDEX [IX_Approvals_EntryId] ON [Approvals] ([EntryId]);

CREATE INDEX [IX_Districts_ProvinceId] ON [Districts] ([ProvinceId]);

CREATE INDEX [IX_HealthFacilities_DistrictId] ON [HealthFacilities] ([DistrictId]);

CREATE INDEX [IX_HealthFacilities_FacilityTypeId] ON [HealthFacilities] ([FacilityTypeId]);

CREATE INDEX [IX_HealthFacilities_LocalLevelId] ON [HealthFacilities] ([LocalLevelId]);

CREATE UNIQUE INDEX [IX_HealthFacilities_PanNumber] ON [HealthFacilities] ([PanNumber]);

CREATE INDEX [IX_HospitalStandardEntrys_MasterStandardEntrySubmissionCode] ON [HospitalStandardEntrys] ([MasterStandardEntrySubmissionCode]);

CREATE INDEX [IX_HospitalStandards_MapdandaId] ON [HospitalStandards] ([MapdandaId]);

CREATE INDEX [IX_HospitalStandards_StandardEntryId] ON [HospitalStandards] ([StandardEntryId]);

CREATE INDEX [IX_LocalLevels_DistrictId] ON [LocalLevels] ([DistrictId]);

CREATE INDEX [IX_Mapdandas_AnusuchiId] ON [Mapdandas] ([AnusuchiId]);

CREATE INDEX [IX_Mapdandas_ParichhedId] ON [Mapdandas] ([ParichhedId]);

CREATE INDEX [IX_Mapdandas_SubParichhedId] ON [Mapdandas] ([SubParichhedId]);

CREATE INDEX [IX_Mapdandas_SubSubParichhedId] ON [Mapdandas] ([SubSubParichhedId]);

CREATE INDEX [IX_MasterStandardEntries_ApprovedById] ON [MasterStandardEntries] ([ApprovedById]);

CREATE INDEX [IX_MasterStandardEntries_HealthFacilityId] ON [MasterStandardEntries] ([HealthFacilityId]);

CREATE INDEX [IX_MasterStandardEntries_RejectedById] ON [MasterStandardEntries] ([RejectedById]);

CREATE INDEX [IX_Parichheds_AnusuchiId] ON [Parichheds] ([AnusuchiId]);

CREATE INDEX [IX_RegistrationRequests_HandledById] ON [RegistrationRequests] ([HandledById]);

CREATE INDEX [IX_RegistrationRequests_HealthFacilityId] ON [RegistrationRequests] ([HealthFacilityId]);

CREATE INDEX [IX_SubMapdandas_MapdandaId] ON [SubMapdandas] ([MapdandaId]);

CREATE INDEX [IX_SubParichheds_ParichhedId] ON [SubParichheds] ([ParichhedId]);

CREATE INDEX [IX_SubSubParichheds_SubParichhedId] ON [SubSubParichheds] ([SubParichhedId]);

CREATE INDEX [IX_TempHealthFacilities_DistrictId] ON [TempHealthFacilities] ([DistrictId]);

CREATE INDEX [IX_TempHealthFacilities_FacilityTypeId] ON [TempHealthFacilities] ([FacilityTypeId]);

CREATE INDEX [IX_TempHealthFacilities_LocalLevelId] ON [TempHealthFacilities] ([LocalLevelId]);

CREATE INDEX [IX_Users_HealthFacilityId] ON [Users] ([HealthFacilityId]);

CREATE INDEX [IX_Users_RoleId] ON [Users] ([RoleId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250302104758_InitalCreate', N'9.0.1');

EXEC sp_rename N'[MasterStandardEntries].[SubmissionType]', N'SubmissionTypeId', 'COLUMN';

ALTER TABLE [TempHealthFacilities] ADD [FilePath] nvarchar(max) NULL;

ALTER TABLE [TempHealthFacilities] ADD [Latitude] float NULL;

ALTER TABLE [TempHealthFacilities] ADD [Longitude] float NULL;

ALTER TABLE [HealthFacilities] ADD [FilePath] nvarchar(max) NULL;

ALTER TABLE [HealthFacilities] ADD [Latitude] float NULL;

ALTER TABLE [HealthFacilities] ADD [Longitude] float NULL;

CREATE TABLE [SubmissionTypes] (
    [Id] int NOT NULL IDENTITY,
    [Title] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_SubmissionTypes] PRIMARY KEY ([Id])
);

CREATE INDEX [IX_MasterStandardEntries_SubmissionTypeId] ON [MasterStandardEntries] ([SubmissionTypeId]);

ALTER TABLE [MasterStandardEntries] ADD CONSTRAINT [FK_MasterStandardEntries_SubmissionTypes_SubmissionTypeId] FOREIGN KEY ([SubmissionTypeId]) REFERENCES [SubmissionTypes] ([Id]) ON DELETE CASCADE;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250303042450_AddMappingTable', N'9.0.1');

ALTER TABLE [TempHealthFacilities] ADD [Email] nvarchar(max) NOT NULL DEFAULT N'';

ALTER TABLE [TempHealthFacilities] ADD [MobileNumber] nvarchar(max) NOT NULL DEFAULT N'';

ALTER TABLE [TempHealthFacilities] ADD [PhoneNumber] nvarchar(max) NOT NULL DEFAULT N'';

ALTER TABLE [TempHealthFacilities] ADD [ProvinceId] int NOT NULL DEFAULT 0;

ALTER TABLE [HealthFacilities] ADD [ProvinceId] int NOT NULL DEFAULT 0;

CREATE INDEX [IX_TempHealthFacilities_ProvinceId] ON [TempHealthFacilities] ([ProvinceId]);

CREATE INDEX [IX_HealthFacilities_ProvinceId] ON [HealthFacilities] ([ProvinceId]);

ALTER TABLE [HealthFacilities] ADD CONSTRAINT [FK_HealthFacilities_Provinces_ProvinceId] FOREIGN KEY ([ProvinceId]) REFERENCES [Provinces] ([Id]);

ALTER TABLE [TempHealthFacilities] ADD CONSTRAINT [FK_TempHealthFacilities_Provinces_ProvinceId] FOREIGN KEY ([ProvinceId]) REFERENCES [Provinces] ([Id]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250303052927_AddProvinceInFacility', N'9.0.1');

DECLARE @var sysname;
SELECT @var = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[TempHealthFacilities]') AND [c].[name] = N'PhoneNumber');
IF @var IS NOT NULL EXEC(N'ALTER TABLE [TempHealthFacilities] DROP CONSTRAINT [' + @var + '];');
ALTER TABLE [TempHealthFacilities] ALTER COLUMN [PhoneNumber] nvarchar(max) NULL;

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[TempHealthFacilities]') AND [c].[name] = N'MobileNumber');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [TempHealthFacilities] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [TempHealthFacilities] ALTER COLUMN [MobileNumber] nvarchar(max) NULL;

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[TempHealthFacilities]') AND [c].[name] = N'Email');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [TempHealthFacilities] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [TempHealthFacilities] ALTER COLUMN [Email] nvarchar(max) NULL;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250303060713_MakeFieldNulls', N'9.0.1');

ALTER TABLE [Mapdandas] ADD [TableNumber] int NOT NULL DEFAULT 0;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250303083158_AddTableNumber', N'9.0.1');

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[HealthFacilities]') AND [c].[name] = N'DateOfInspection');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [HealthFacilities] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [HealthFacilities] ALTER COLUMN [DateOfInspection] nvarchar(max) NULL;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250303093041_MakeDateOfInsNull', N'9.0.1');

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[HospitalStandards]') AND [c].[name] = N'IsApproved');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [HospitalStandards] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [HospitalStandards] ALTER COLUMN [IsApproved] bit NULL;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250304054051_MakeIsApprovedNull', N'9.0.1');

ALTER TABLE [Users] ADD [DistrictId] int NOT NULL DEFAULT 0;

ALTER TABLE [Users] ADD [FacilityEmail] nvarchar(max) NULL;

ALTER TABLE [Users] ADD [FacilityMobileNumber] nvarchar(max) NULL;

ALTER TABLE [Users] ADD [FacilityTypeId] int NOT NULL DEFAULT 0;

ALTER TABLE [Users] ADD [FullName] nvarchar(max) NULL;

ALTER TABLE [Users] ADD [MobileNumber] nvarchar(max) NULL;

ALTER TABLE [Users] ADD [PersonalEmail] nvarchar(max) NULL;

ALTER TABLE [Users] ADD [Post] nvarchar(max) NOT NULL DEFAULT N'';

ALTER TABLE [Users] ADD [ProvinceId] int NOT NULL DEFAULT 0;

ALTER TABLE [Users] ADD [Remarks] nvarchar(max) NULL;

ALTER TABLE [Users] ADD [TelephoneNumber] nvarchar(max) NULL;

CREATE INDEX [IX_Users_DistrictId] ON [Users] ([DistrictId]);

CREATE INDEX [IX_Users_FacilityTypeId] ON [Users] ([FacilityTypeId]);

CREATE INDEX [IX_Users_ProvinceId] ON [Users] ([ProvinceId]);

ALTER TABLE [Users] ADD CONSTRAINT [FK_Users_Districts_DistrictId] FOREIGN KEY ([DistrictId]) REFERENCES [Districts] ([Id]);

ALTER TABLE [Users] ADD CONSTRAINT [FK_Users_HospitalType_FacilityTypeId] FOREIGN KEY ([FacilityTypeId]) REFERENCES [HospitalType] ([SN]) ON DELETE CASCADE;

ALTER TABLE [Users] ADD CONSTRAINT [FK_Users_Provinces_ProvinceId] FOREIGN KEY ([ProvinceId]) REFERENCES [Provinces] ([Id]) ON DELETE CASCADE;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250304071623_AddMoreColsInUser', N'9.0.1');

COMMIT;
GO

