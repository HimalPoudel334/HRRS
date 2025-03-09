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

CREATE TABLE [BedCount] (
    [Id] int NOT NULL IDENTITY,
    [Count] int NOT NULL,
    CONSTRAINT [PK_BedCount] PRIMARY KEY ([Id])
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

CREATE TABLE [SubmissionTypes] (
    [Id] int NOT NULL IDENTITY,
    [Title] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_SubmissionTypes] PRIMARY KEY ([Id])
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

CREATE TABLE [MapdandaTables] (
    [Id] int NOT NULL IDENTITY,
    [TableName] nvarchar(max) NULL,
    [TableNumber] int NOT NULL,
    [AnusuchiId] int NOT NULL,
    [ParichhedId] int NULL,
    [SubParichhedId] int NULL,
    [Description] nvarchar(max) NULL,
    [Note] nvarchar(max) NULL,
    [FormType] int NOT NULL,
    CONSTRAINT [PK_MapdandaTables] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_MapdandaTables_Anusuchis_AnusuchiId] FOREIGN KEY ([AnusuchiId]) REFERENCES [Anusuchis] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_MapdandaTables_Parichheds_ParichhedId] FOREIGN KEY ([ParichhedId]) REFERENCES [Parichheds] ([Id]),
    CONSTRAINT [FK_MapdandaTables_SubParichheds_SubParichhedId] FOREIGN KEY ([SubParichhedId]) REFERENCES [SubParichheds] ([Id])
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
    [ProvinceId] int NOT NULL,
    [DistrictId] int NOT NULL,
    [LocalLevelId] int NOT NULL,
    [WardNumber] int NOT NULL,
    [Tole] nvarchar(max) NOT NULL,
    [Longitude] float NULL,
    [Latitude] float NULL,
    [FilePath] nvarchar(max) NULL,
    [DateOfInspection] nvarchar(max) NULL,
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
    CONSTRAINT [FK_HealthFacilities_Districts_DistrictId] FOREIGN KEY ([DistrictId]) REFERENCES [Districts] ([Id]),
    CONSTRAINT [FK_HealthFacilities_HospitalType_FacilityTypeId] FOREIGN KEY ([FacilityTypeId]) REFERENCES [HospitalType] ([SN]) ON DELETE CASCADE,
    CONSTRAINT [FK_HealthFacilities_LocalLevels_LocalLevelId] FOREIGN KEY ([LocalLevelId]) REFERENCES [LocalLevels] ([Id]),
    CONSTRAINT [FK_HealthFacilities_Provinces_ProvinceId] FOREIGN KEY ([ProvinceId]) REFERENCES [Provinces] ([Id])
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
    [Longitude] float NULL,
    [Latitude] float NULL,
    [FilePath] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [MobileNumber] nvarchar(max) NULL,
    [Email] nvarchar(max) NULL,
    [ProvinceId] int NOT NULL,
    CONSTRAINT [PK_TempHealthFacilities] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TempHealthFacilities_Districts_DistrictId] FOREIGN KEY ([DistrictId]) REFERENCES [Districts] ([Id]),
    CONSTRAINT [FK_TempHealthFacilities_HospitalType_FacilityTypeId] FOREIGN KEY ([FacilityTypeId]) REFERENCES [HospitalType] ([SN]) ON DELETE CASCADE,
    CONSTRAINT [FK_TempHealthFacilities_LocalLevels_LocalLevelId] FOREIGN KEY ([LocalLevelId]) REFERENCES [LocalLevels] ([Id]),
    CONSTRAINT [FK_TempHealthFacilities_Provinces_ProvinceId] FOREIGN KEY ([ProvinceId]) REFERENCES [Provinces] ([Id])
);

CREATE TABLE [AnusuchiMappings] (
    [Id] int NOT NULL IDENTITY,
    [FacilityTypeId] int NOT NULL,
    [BedCountId] int NOT NULL,
    [SubmissionTypeId] int NOT NULL,
    [RoleId] int NOT NULL,
    CONSTRAINT [PK_AnusuchiMappings] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AnusuchiMappings_BedCount_BedCountId] FOREIGN KEY ([BedCountId]) REFERENCES [BedCount] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AnusuchiMappings_HospitalType_FacilityTypeId] FOREIGN KEY ([FacilityTypeId]) REFERENCES [HospitalType] ([SN]) ON DELETE CASCADE,
    CONSTRAINT [FK_AnusuchiMappings_SubmissionTypes_SubmissionTypeId] FOREIGN KEY ([SubmissionTypeId]) REFERENCES [SubmissionTypes] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AnusuchiMappings_UserRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [UserRoles] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [Mapdandas] (
    [Id] int NOT NULL IDENTITY,
    [OrderNo] int NOT NULL,
    [SerialNumber] nvarchar(max) NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [Parimaad] nvarchar(max) NULL,
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
    [FormType] int NOT NULL,
    [MapdandaTableId] int NOT NULL,
    [IsGroup] bit NOT NULL,
    [IsSubGroup] bit NOT NULL,
    [IsSection] bit NOT NULL,
    [HasGroup] bit NOT NULL,
    CONSTRAINT [PK_Mapdandas] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Mapdandas_MapdandaTables_MapdandaTableId] FOREIGN KEY ([MapdandaTableId]) REFERENCES [MapdandaTables] ([Id]) ON DELETE CASCADE,
);

CREATE TABLE [Users] (
    [UserId] bigint NOT NULL IDENTITY,
    [UserName] nvarchar(max) NOT NULL,
    [Password] nvarchar(max) NOT NULL,
    [UserType] nvarchar(max) NOT NULL,
    [HealthFacilityId] int NULL,
    [RoleId] int NULL,
    [IsFirstLogin] bit NOT NULL,
    [ProvinceId] int NOT NULL,
    [DistrictId] int NOT NULL,
    [FacilityTypeId] int NOT NULL,
    [Post] nvarchar(max) NULL,
    [FullName] nvarchar(max) NULL,
    [MobileNumber] nvarchar(max) NULL,
    [FacilityMobileNumber] nvarchar(max) NULL,
    [TelephoneNumber] nvarchar(max) NULL,
    [FacilityEmail] nvarchar(max) NULL,
    [PersonalEmail] nvarchar(max) NULL,
    [Remarks] nvarchar(max) NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([UserId]),
    CONSTRAINT [FK_Users_Districts_DistrictId] FOREIGN KEY ([DistrictId]) REFERENCES [Districts] ([Id]),
    CONSTRAINT [FK_Users_HealthFacilities_HealthFacilityId] FOREIGN KEY ([HealthFacilityId]) REFERENCES [HealthFacilities] ([Id]),
    CONSTRAINT [FK_Users_HospitalType_FacilityTypeId] FOREIGN KEY ([FacilityTypeId]) REFERENCES [HospitalType] ([SN]) ON DELETE CASCADE,
    CONSTRAINT [FK_Users_Provinces_ProvinceId] FOREIGN KEY ([ProvinceId]) REFERENCES [Provinces] ([Id]),
    CONSTRAINT [FK_Users_UserRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [UserRoles] ([Id])
);

CREATE TABLE [AnusuchiMapdandaTableMappings] (
    [Id] int NOT NULL IDENTITY,
    [AnusuchiMappingId] int NOT NULL,
    [AnusuchiId] int NOT NULL,
    [ParichhedId] int NULL,
    CONSTRAINT [PK_AnusuchiMapdandaTableMappings] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AnusuchiMapdandaTableMappings_AnusuchiMappings_AnusuchiMappingId] FOREIGN KEY ([AnusuchiMappingId]) REFERENCES [AnusuchiMappings] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AnusuchiMapdandaTableMappings_Anusuchis_AnusuchiId] FOREIGN KEY ([AnusuchiId]) REFERENCES [Anusuchis] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AnusuchiMapdandaTableMappings_Parichheds_ParichhedId] FOREIGN KEY ([ParichhedId]) REFERENCES [Parichheds] ([Id])
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
    [SubmissionTypeId] int NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedAt] datetime2 NULL,
    [IsNewEntry] bit NOT NULL,
    CONSTRAINT [PK_MasterStandardEntries] PRIMARY KEY ([SubmissionCode]),
    CONSTRAINT [FK_MasterStandardEntries_HealthFacilities_HealthFacilityId] FOREIGN KEY ([HealthFacilityId]) REFERENCES [HealthFacilities] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_MasterStandardEntries_SubmissionTypes_SubmissionTypeId] FOREIGN KEY ([SubmissionTypeId]) REFERENCES [SubmissionTypes] ([Id]) ON DELETE CASCADE,
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
    [IsApproved] bit NULL,
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

CREATE INDEX [IX_AnusuchiMapdandaTableMappings_AnusuchiId] ON [AnusuchiMapdandaTableMappings] ([AnusuchiId]);

CREATE INDEX [IX_AnusuchiMapdandaTableMappings_AnusuchiMappingId] ON [AnusuchiMapdandaTableMappings] ([AnusuchiMappingId]);

CREATE INDEX [IX_AnusuchiMapdandaTableMappings_ParichhedId] ON [AnusuchiMapdandaTableMappings] ([ParichhedId]);

CREATE INDEX [IX_AnusuchiMappings_BedCountId] ON [AnusuchiMappings] ([BedCountId]);

CREATE INDEX [IX_AnusuchiMappings_FacilityTypeId] ON [AnusuchiMappings] ([FacilityTypeId]);

CREATE INDEX [IX_AnusuchiMappings_MapdandaTableId] ON [AnusuchiMappings] ([MapdandaTableId]);

CREATE INDEX [IX_AnusuchiMappings_RoleId] ON [AnusuchiMappings] ([RoleId]);

CREATE INDEX [IX_AnusuchiMappings_SubmissionTypeId] ON [AnusuchiMappings] ([SubmissionTypeId]);

CREATE INDEX [IX_Districts_ProvinceId] ON [Districts] ([ProvinceId]);

CREATE INDEX [IX_HealthFacilities_DistrictId] ON [HealthFacilities] ([DistrictId]);

CREATE INDEX [IX_HealthFacilities_FacilityTypeId] ON [HealthFacilities] ([FacilityTypeId]);

CREATE INDEX [IX_HealthFacilities_LocalLevelId] ON [HealthFacilities] ([LocalLevelId]);

CREATE UNIQUE INDEX [IX_HealthFacilities_PanNumber] ON [HealthFacilities] ([PanNumber]);

CREATE INDEX [IX_HealthFacilities_ProvinceId] ON [HealthFacilities] ([ProvinceId]);

CREATE INDEX [IX_HospitalStandardEntrys_MasterStandardEntrySubmissionCode] ON [HospitalStandardEntrys] ([MasterStandardEntrySubmissionCode]);

CREATE INDEX [IX_HospitalStandards_MapdandaId] ON [HospitalStandards] ([MapdandaId]);

CREATE INDEX [IX_HospitalStandards_StandardEntryId] ON [HospitalStandards] ([StandardEntryId]);

CREATE INDEX [IX_LocalLevels_DistrictId] ON [LocalLevels] ([DistrictId]);

CREATE INDEX [IX_Mapdandas_MapdandaTableId] ON [Mapdandas] ([MapdandaTableId]);

CREATE INDEX [IX_Mapdandas_SubParichhedId] ON [Mapdandas] ([SubParichhedId]);

CREATE INDEX [IX_Mapdandas_SubSubParichhedId] ON [Mapdandas] ([SubSubParichhedId]);

CREATE INDEX [IX_MapdandaTables_AnusuchiId] ON [MapdandaTables] ([AnusuchiId]);

CREATE INDEX [IX_MapdandaTables_ParichhedId] ON [MapdandaTables] ([ParichhedId]);

CREATE INDEX [IX_MapdandaTables_SubParichhedId] ON [MapdandaTables] ([SubParichhedId]);

CREATE INDEX [IX_MasterStandardEntries_ApprovedById] ON [MasterStandardEntries] ([ApprovedById]);

CREATE INDEX [IX_MasterStandardEntries_HealthFacilityId] ON [MasterStandardEntries] ([HealthFacilityId]);

CREATE INDEX [IX_MasterStandardEntries_RejectedById] ON [MasterStandardEntries] ([RejectedById]);

CREATE INDEX [IX_MasterStandardEntries_SubmissionTypeId] ON [MasterStandardEntries] ([SubmissionTypeId]);

CREATE INDEX [IX_Parichheds_AnusuchiId] ON [Parichheds] ([AnusuchiId]);

CREATE INDEX [IX_RegistrationRequests_HandledById] ON [RegistrationRequests] ([HandledById]);

CREATE INDEX [IX_RegistrationRequests_HealthFacilityId] ON [RegistrationRequests] ([HealthFacilityId]);

CREATE INDEX [IX_SubMapdandas_MapdandaId] ON [SubMapdandas] ([MapdandaId]);

CREATE INDEX [IX_SubParichheds_ParichhedId] ON [SubParichheds] ([ParichhedId]);

CREATE INDEX [IX_SubSubParichheds_SubParichhedId] ON [SubSubParichheds] ([SubParichhedId]);

CREATE INDEX [IX_TempHealthFacilities_DistrictId] ON [TempHealthFacilities] ([DistrictId]);

CREATE INDEX [IX_TempHealthFacilities_FacilityTypeId] ON [TempHealthFacilities] ([FacilityTypeId]);

CREATE INDEX [IX_TempHealthFacilities_LocalLevelId] ON [TempHealthFacilities] ([LocalLevelId]);

CREATE INDEX [IX_TempHealthFacilities_ProvinceId] ON [TempHealthFacilities] ([ProvinceId]);

CREATE INDEX [IX_Users_DistrictId] ON [Users] ([DistrictId]);

CREATE INDEX [IX_Users_FacilityTypeId] ON [Users] ([FacilityTypeId]);

CREATE INDEX [IX_Users_HealthFacilityId] ON [Users] ([HealthFacilityId]);

CREATE INDEX [IX_Users_ProvinceId] ON [Users] ([ProvinceId]);

CREATE INDEX [IX_Users_RoleId] ON [Users] ([RoleId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250307115538_InitialCreate', N'9.0.1');

COMMIT;
GO

select * from locallevels

select * from MapdandaTables

truncate table MapdandaTables
select scope_identity()

sp_help MapdandaTables

select * from Anusuchis

sp_help users

INSERT INTO Users 
(UserName, Password, UserType, HealthFacilityId, RoleId, IsFirstLogin, ProvinceId, DistrictId, FacilityTypeId, Post, FullName, MobileNumber, FacilityMobileNumber, TelephoneNumber, FacilityEmail, PersonalEmail, Remarks)
VALUES
('Administrator', '$2a$12$B2E8SlntjeWl4VpxV08Mcuh0OAnFh8tBdfK9fnw10Zpb1yK5H5Wqq', 'SuperAdmin', NULL, NULL, 0, 3, 28, 1, 'SuperAdmin', NULL, NULL, NULL, NULL, NULL, NULL, NULL);



alter table mapdandas drop constraint FK_Mapdandas_MapdandaTables_MapdandaTableId


select * from AnusuchiMapdandaTableMappings
select SCOPE_IDENTITY()

select * from AnusuchiMappings

select * from MapdandaTables

select * from Anusuchis

select * from Mapdandas



truncate table AnusuchiMapdandaTableMappings
INSERT INTO AnusuchiMapdandaTableMappings (AnusuchiMappingId, AnusuchiId, ParichhedId)
VALUES
(1, 17, NULL),
(1, 3, NULL),
(1, 1, NULL),
(1, 4, NULL),
(1, 5, NULL),
(2, 17, NULL),
(2, 3, NULL),
(2, 4, NULL),
(2, 5, NULL),
(3, 17, NULL),
(3, 3, NULL),
(3, 2, NULL),
(3, 4, NULL),
(3, 5, NULL),
(4, 17, NULL),
(4, 3, NULL),
(4, 4, NULL),
(4, 5, NULL),
(5, 17, NULL),
(5, 3, NULL),
(5, 4, NULL),
(5, 5, NULL),
(6, 17, NULL),
(6, 3, NULL),
(6, 4, NULL),
(6, 5, NULL),
(7, 17, NULL),
(7, 3, NULL),
(7, 4, NULL),
(7, 5, NULL),
(8, 17, NULL),
(8, 3, NULL),
(8, 1, NULL),
(8, 4, NULL),
(8, 5, NULL),
(9, 17, NULL),
(9, 3, NULL),
(9, 4, NULL),
(9, 5, NULL),
(10, 17, NULL),
(10, 3, NULL),
(10, 2, NULL),
(10, 4, NULL),
(10, 5, NULL),
(11, 17, NULL),
(11, 3, NULL),
(11, 1, NULL),
(11, 4, NULL),
(11, 5, NULL),
(12, 17, NULL),
(12, 3, NULL),
(12, 1, NULL),
(12, 4, NULL),
(12, 5, NULL),
(13, 17, NULL),
(13, 3, NULL),
(13, 1, NULL),
(13, 4, NULL),
(13, 5, NULL),
(14, 17, NULL),
(14, 3, NULL),
(14, 4, NULL),
(14, 5, NULL),
(15, 17, NULL),
(15, 3, NULL),
(15, 2, NULL),
(15, 4, NULL),
(15, 5, NULL),
(16, 17, NULL),
(16, 3, NULL),
(16, 4, NULL),
(16, 5, NULL),
(17, 17, NULL),
(17, 3, NULL),
(17, 4, NULL),
(17, 5, NULL),
(18, 17, NULL),
(18, 3, NULL),
(18, 4, NULL),
(18, 5, NULL),
(19, 17, NULL),
(19, 3, NULL),
(19, 4, NULL),
(19, 5, NULL),
(20, 17, NULL),
(20, 3, NULL),
(20, 1, NULL),
(20, 4, NULL),
(20, 5, NULL),
(21, 17, NULL),
(21, 3, NULL),
(21, 4, NULL),
(21, 5, NULL),
(22, 17, NULL),
(22, 3, NULL),
(22, 2, NULL),
(22, 4, NULL),
(22, 5, NULL),
(23, 17, NULL),
(23, 3, NULL),
(23, 1, NULL),
(23, 4, NULL),
(23, 5, NULL),
(24, 17, NULL),
(24, 3, NULL),
(24, 1, NULL),
(24, 4, NULL),
(24, 5, NULL),
(25, 17, NULL),
(25, 3, NULL),
(25, 1, NULL),
(25, 4, NULL),
(25, 5, 33),
(26, 17, NULL),
(26, 3, NULL),
(26, 4, NULL),
(26, 5, 33),
(27, 17, NULL),
(27, 3, NULL),
(27, 2, NULL),
(27, 4, NULL),
(27, 5, 33),
(28, 17, NULL),
(28, 3, NULL),
(28, 4, NULL),
(28, 5, 33),
(29, 17, NULL),
(29, 3, NULL),
(29, 4, NULL),
(29, 5, 33),
(30, 17, NULL),
(30, 3, NULL),
(30, 4, NULL),
(30, 5, 33),
(31, 17, NULL),
(31, 3, NULL),
(31, 1, NULL),
(31, 4, NULL),
(31, 7, NULL),
(32, 17, NULL),
(32, 3, NULL),
(32, 4, NULL),
(32, 7, NULL),
(33, 17, NULL),
(33, 3, NULL),
(33, 2, NULL),
(33, 4, NULL),
(33, 7, NULL),
(34, 17, NULL),
(34, 3, NULL),
(34, 4, NULL),
(34, 7, NULL),
(35, 17, NULL),
(35, 3, NULL),
(35, 4, NULL),
(35, 7, NULL),
(36, 17, NULL),
(36, 3, NULL),
(36, 4, NULL),
(36, 7, NULL),
(37, 17, NULL),
(37, 3, NULL),
(37, 1, NULL),
(37, 4, NULL),
(37, 6, NULL),
(38, 17, NULL),
(38, 3, NULL),
(38, 4, NULL),
(38, 6, NULL),
(39, 17, NULL),
(39, 3, NULL),
(39, 2, NULL),
(39, 4, NULL),
(39, 6, NULL),
(40, 17, NULL),
(40, 3, NULL),
(40, 4, NULL),
(40, 6, NULL),
(41, 17, NULL),
(41, 3, NULL),
(41, 4, NULL),
(41, 6, NULL),
(42, 17, NULL),
(42, 3, NULL),
(42, 4, NULL),
(42, 6, NULL),
(43, 17, NULL),
(43, 3, NULL),
(43, 1, NULL),
(43, 4, NULL),
(43, 8, NULL),
(44, 17, NULL),
(44, 3, NULL),
(44, 4, NULL),
(44, 8, NULL),
(45, 17, NULL),
(45, 3, NULL),
(45, 2, NULL),
(45, 4, NULL),
(45, 8, NULL),
(46, 17, NULL),
(46, 3, NULL),
(46, 4, NULL),
(46, 8, NULL),
(47, 17, NULL),
(47, 3, NULL),
(47, 4, NULL),
(47, 8, NULL),
(48, 17, NULL),
(48, 3, NULL),
(48, 4, NULL),
(48, 8, NULL),
(49, 17, NULL),
(49, 3, NULL),
(49, 1, NULL),
(49, 4, NULL),
(49, 5, 27),
(50, 17, NULL),
(50, 3, NULL),
(50, 4, NULL),
(50, 5, 27),
(51, 17, NULL),
(51, 3, NULL),
(51, 2, NULL),
(51, 4, NULL),
(51, 5, 27),
(52, 17, NULL),
(52, 3, NULL),
(52, 4, NULL),
(52, 5, 27),
(53, 17, NULL),
(53, 3, NULL),
(53, 4, NULL),
(53, 5, 27),
(54, 17, NULL),
(54, 3, NULL),
(54, 4, NULL),
(54, 5, 27),
(55, 17, NULL),
(55, 3, NULL),
(55, 1, NULL),
(55, 4, NULL),
(55, 11, NULL),
(56, 17, NULL),
(56, 3, NULL),
(56, 4, NULL),
(56, 11, NULL),
(57, 17, NULL),
(57, 3, NULL),
(57, 2, NULL),
(57, 4, NULL),
(57, 11, NULL),
(58, 17, NULL),
(58, 3, NULL),
(58, 4, NULL),
(58, 11, NULL),
(59, 17, NULL),
(59, 3, NULL),
(59, 4, NULL),
(59, 11, NULL),
(60, 17, NULL),
(60, 3, NULL),
(60, 4, NULL),
(60, 11, NULL),
(61, 17, NULL),
(61, 3, NULL),
(61, 1, NULL),
(61, 15, NULL),
(62, 17, NULL),
(62, 3, NULL),
(62, 15, NULL),
(63, 17, NULL),
(63, 3, NULL),
(63, 2, NULL),
(63, 15, NULL),
(64, 17, NULL),
(64, 3, NULL),
(64, 1, NULL),
(64, 15, NULL),
(65, 17, NULL),
(65, 3, NULL),
(65, 1, NULL),
(65, 15, NULL),
(66, 17, NULL),
(66, 3, NULL),
(66, 1, NULL),
(66, 15, NULL),
(67, 17, NULL),
(67, 3, NULL),
(67, 1, NULL),
(67, 13, NULL),
(68, 17, NULL),
(68, 3, NULL),
(68, 13, NULL),
(69, 17, NULL),
(69, 3, NULL),
(69, 2, NULL),
(69, 13, NULL),
(70, 17, NULL),
(70, 3, NULL),
(70, 1, NULL),
(70, 13, NULL),
(71, 17, NULL),
(71, 3, NULL),
(71, 1, NULL),
(71, 13, NULL),
(72, 17, NULL),
(72, 3, NULL),
(72, 1, NULL),
(72, 13, NULL),
(73, 17, NULL),
(73, 3, NULL),
(73, 1, NULL),
(73, 10, NULL),
(74, 17, NULL),
(74, 3, NULL),
(74, 10, NULL),
(75, 17, NULL),
(75, 3, NULL),
(75, 2, NULL),
(75, 10, NULL),
(76, 17, NULL),
(76, 3, NULL),
(76, 1, NULL),
(76, 10, NULL),
(77, 17, NULL),
(77, 3, NULL),
(77, 1, NULL),
(77, 10, NULL),
(78, 17, NULL),
(78, 3, NULL),
(78, 1, NULL),
(78, 10, NULL),
(79, 17, NULL),
(79, 3, NULL),
(79, 1, NULL),
(78, 17, NULL),
(78, 3, NULL),
(79, 17, NULL),
(79, 3, NULL),
(79, 2, NULL),
(80, 17, NULL),
(80, 3, NULL),
(80, 2, NULL),
(81, 17, NULL),
(81, 3, NULL),
(81, 2, NULL),
(82, 17, NULL),
(82, 3, NULL),
(82, 1, NULL),
(83, 17, NULL),
(83, 3, NULL),
(83, 1, NULL),
(84, 17, NULL),
(84, 3, NULL),
(84, 1, NULL),
(85, 17, NULL),
(85, 3, NULL),
(85, 2, NULL),
(86, 17, NULL),
(86, 3, NULL),
(86, 2, NULL),
(87, 17, NULL),
(87, 3, NULL),
(87, 2, NULL),
(88, 17, NULL),
(88, 3, NULL),
(88, 1, NULL),
(89, 17, NULL),
(89, 3, NULL),
(89, 1, NULL),
(90, 17, NULL),
(90, 3, NULL),
(90, 1, NULL),
(91, 17, NULL),
(91, 3, NULL),
(91, 1, NULL),
(91, 5, 28),
(92, 17, NULL),
(92, 3, NULL),
(92, 5, 28),
(93, 17, NULL),
(93, 3, NULL),
(93, 2, NULL),
(93, 5, 28),
(94, 17, NULL),
(94, 3, NULL),
(94, 1, NULL),
(94, 5, 28),
(95, 17, NULL),
(95, 3, NULL),
(95, 1, NULL),
(95, 5, 28),
(96, 17, NULL),
(96, 3, NULL),
(96, 1, NULL),
(96, 5, 28),
(97, 17, NULL),
(97, 3, NULL),
(97, 1, NULL),
(97, 5, 29),
(98, 17, NULL),
(98, 3, NULL),
(98, 5, 29),
(99, 17, NULL),
(99, 3, NULL),
(99, 2, NULL),
(99, 5, 29),
(100, 17, NULL),
(100, 3, NULL),
(100, 1, NULL),
(100, 5, 29),
(101, 17, NULL),
(101, 3, NULL),
(101, 2, NULL),
(101, 5, 29),
(102, 17, NULL),
(102, 3, NULL),
(102, 1, NULL),
(102, 5, 29),
(103, 17, NULL),
(103, 3, NULL),
(103, 1, NULL),
(104, 17, NULL),
(104, 3, NULL),
(105, 17, NULL),
(105, 3, NULL),
(105, 2, NULL),
(106, 17, NULL),
(106, 3, NULL),
(106, 1, NULL),
(107, 17, NULL),
(107, 3, NULL),
(107, 1, NULL),
(108, 17, NULL),
(108, 3, NULL),
(108, 1, NULL),
(109, 17, NULL),
(109, 3, NULL),
(109, 1, NULL),
(110, 17, NULL),
(110, 3, NULL),
(111, 17, NULL),
(111, 3, NULL),
(111, 2, NULL),
(112, 17, NULL),
(112, 3, NULL),
(112, 1, NULL),
(113, 17, NULL),
(113, 3, NULL),
(113, 1, NULL),
(114, 17, NULL),
(114, 3, NULL),
(114, 1, NULL),
(115, 17, NULL),
(115, 3, NULL),
(115, 1, NULL),
(115, 4, NULL),
(115, 12, NULL),
(116, 17, NULL),
(116, 3, NULL),
(116, 4, NULL),
(116, 12, NULL),
(117, 17, NULL),
(117, 3, NULL),
(117, 2, NULL),
(117, 4, NULL),
(117, 12, NULL),
(118, 17, NULL),
(118, 3, NULL),
(118, 1, NULL),
(118, 4, NULL),
(118, 12, NULL),
(119, 17, NULL),
(119, 3, NULL),
(119, 1, NULL),
(119, 4, NULL),
(119, 12, NULL),
(120, 17, NULL),
(120, 3, NULL),
(120, 1, NULL),
(120, 4, NULL),
(120, 12, NULL),
(121, 17, NULL),
(121, 3, NULL),
(121, 1, NULL),
(121, 15, NULL),
(122, 17, NULL),
(122, 3, NULL),
(122, 15, NULL),
(123, 17, NULL),
(123, 3, NULL),
(123, 2, NULL),
(123, 15, NULL),
(124, 17, NULL),
(124, 3, NULL),
(124, 1, NULL),
(124, 15, NULL),
(125, 17, NULL),
(125, 3, NULL),
(125, 1, NULL),
(126, 15, NULL),
(126, 17, NULL),
(126, 3, NULL),
(126, 1, NULL),
(126, 15, NULL),
(127, 17, NULL),
(127, 3, NULL),
(127, 1, NULL),
(127, 15, NULL),
(128, 17, NULL),
(128, 3, NULL),
(128, 15, NULL),
(129, 17, NULL),
(129, 3, NULL),
(129, 2, NULL),
(129, 15, NULL),
(130, 17, NULL),
(130, 3, NULL),
(130, 1, NULL),
(130, 15, NULL),
(131, 17, NULL),
(131, 3, NULL),
(131, 1, NULL),
(131, 15, NULL),
(132, 17, NULL),
(132, 3, NULL),
(132, 1, NULL),
(132, 15, NULL),
(133, 17, NULL),
(133, 3, NULL),
(133, 1, NULL),
(133, 15, NULL),
(134, 17, NULL),
(134, 3, NULL),
(134, 15, NULL),
(135, 17, NULL),
(135, 3, NULL),
(135, 2, NULL),
(135, 15, NULL),
(136, 17, NULL),
(136, 3, NULL),
(136, 1, NULL),
(136, 15, NULL),
(137, 17, NULL),
(137, 3, NULL),
(137, 1, NULL),
(137, 15, NULL),
(138, 17, NULL),
(138, 3, NULL),
(138, 1, NULL),
(138, 15, NULL),
(139, 17, NULL),
(139, 3, NULL),
(139, 1, NULL),
(139, 15, NULL),
(140, 17, NULL),
(140, 3, NULL),
(140, 15, NULL),
(141, 17, NULL),
(141, 3, NULL),
(141, 2, NULL),
(141, 15, NULL),
(142, 17, NULL),
(142, 3, NULL),
(142, 1, NULL),
(142, 15, NULL),
(143, 17, NULL),
(143, 3, NULL),
(143, 1, NULL),
(143, 15, NULL),
(144, 17, NULL),
(144, 3, NULL),
(144, 1, NULL),
(144, 15, NULL),
(145, 17, NULL),
(145, 3, NULL),
(145, 1, NULL),
(145, 15, NULL),
(146, 17, NULL),
(146, 3, NULL),
(146, 15, NULL),
(147, 17, NULL),
(147, 3, NULL),
(147, 2, NULL),
(147, 15, NULL),
(148, 17, NULL),
(148, 3, NULL),
(148, 1, NULL),
(148, 15, NULL),
(149, 17, NULL),
(149, 3, NULL),
(149, 1, NULL),
(149, 15, NULL),
(150, 17, NULL),
(150, 3, NULL),
(150, 1, NULL),
(150, 15, NULL),
(151, 17, NULL),
(151, 3, NULL),
(151, 1, NULL),
(151, 15, NULL),
(152, 17, NULL),
(152, 3, NULL),
(152, 15, NULL),
(153, 17, NULL),
(153, 3, NULL),
(153, 2, NULL),
(153, 15, NULL),
(154, 17, NULL),
(154, 3, NULL),
(154, 1, NULL),
(154, 15, NULL),
(155, 17, NULL),
(155, 3, NULL),
(155, 1, NULL),
(155, 15, NULL),
(156, 17, NULL),
(156, 3, NULL),
(156, 1, NULL),
(156, 15, NULL),
(157, 17, NULL),
(157, 31, NULL),
(157, 1, NULL),
(157, 15, NULL),
(158, 17, NULL),
(158, 3, NULL),
(158, 14, NULL),
(159, 17, NULL),
(159, 3, NULL),
(159, 2, NULL),
(159, 14, NULL),
(160, 17, NULL),
(160, 3, NULL),
(160, 1, NULL),
(160, 14, NULL),
(161, 17, NULL),
(161, 3, NULL),
(161, 1, NULL),
(161, 14, NULL),
(162, 17, NULL),
(162, 3, NULL),
(162, 1, NULL),
(162, 14, NULL),
(163, 17, NULL),
(163, 31, NULL),
(163, 1, NULL),
(163, 15, NULL),
(164, 17, NULL),
(164, 3, NULL),
(164, 14, NULL),
(165, 17, NULL),
(165, 3, NULL),
(165, 2, NULL),
(165, 14, NULL),
(166, 17, NULL),
(166, 3, NULL),
(166, 1, NULL),
(166, 14, NULL),
(167, 17, NULL),
(167, 3, NULL),
(167, 1, NULL),
(167, 14, NULL),
(168, 17, NULL),
(168, 3, NULL),
(168, 1, NULL),
(168, 14, NULL),
(169, 17, NULL),
(169, 3, NULL),
(169, 1, NULL),
(169, 15, NULL),
(170, 17, NULL),
(170, 3, NULL),
(170, 15, NULL),
(171, 17, NULL),
(171, 3, NULL),
(171, 2, NULL),
(171, 15, NULL),
(172, 17, NULL),
(172, 3, NULL),
(172, 1, NULL),
(172, 15, NULL),
(173, 17, NULL),
(173, 3, NULL),
(173, 1, NULL),
(173, 15, NULL),
(174, 17, NULL),
(174, 3, NULL),
(174, 1, NULL),
(174, 15, NULL);


