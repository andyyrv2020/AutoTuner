USE [aspnet-AutoTuner-e05d2182-a428-4aed-86fa-8fa058f1614c]
GO

/****** Object: Table [dbo].[Workshops] Script Date: 11/17/2025 6:50:08 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Workshops] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [Name]           NVARCHAR (150) NOT NULL,
    [City]           NVARCHAR (100) NOT NULL,
    [Address]        NVARCHAR (200) NOT NULL,
    [Latitude]       FLOAT (53)     NOT NULL,
    [Longitude]      FLOAT (53)     NOT NULL,
    [Specialization] NVARCHAR (150) NULL
);


