
--Migrations for Mapdandas table

ALTER TABLE [Mapdandas] ADD [FormType] int NOT NULL DEFAULT 0;

ALTER TABLE [Mapdandas] ADD [Col5] nvarchar(max) NULL;

ALTER TABLE [Mapdandas] ADD [Col6] nvarchar(max) NULL;

ALTER TABLE [Mapdandas] ADD [Col7] nvarchar(max) NULL;

ALTER TABLE [Mapdandas] ADD [Col8] nvarchar(max) NULL;

ALTER TABLE [Mapdandas] ADD [Col9] nvarchar(max) NULL;

ALTER TABLE [Mapdandas] ADD [IsCol5Active] bit NOT NULL DEFAULT CAST(0 AS bit);

ALTER TABLE [Mapdandas] ADD [IsCol6Active] bit NOT NULL DEFAULT CAST(0 AS bit);

ALTER TABLE [Mapdandas] ADD [IsCol7Active] bit NOT NULL DEFAULT CAST(0 AS bit);

ALTER TABLE [Mapdandas] ADD [IsCol8Active] bit NOT NULL DEFAULT CAST(0 AS bit);

ALTER TABLE [Mapdandas] ADD [IsCol9Active] bit NOT NULL DEFAULT CAST(0 AS bit);

