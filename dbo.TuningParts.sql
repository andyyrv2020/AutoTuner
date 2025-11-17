USE [aspnet-AutoTuner-e05d2182-a428-4aed-86fa-8fa058f1614c]
GO

/****** Object: Table [dbo].[TuningParts] Script Date: 11/17/2025 6:50:03 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TuningParts] (
    [Id]                  INT             IDENTITY (1, 1) NOT NULL,
    [Name]                NVARCHAR (150)  NOT NULL,
    [Category]            NVARCHAR (100)  NOT NULL,
    [Description]         NVARCHAR (500)  NULL,
    [PowerGain]           INT             NOT NULL,
    [TorqueGain]          INT             NOT NULL,
    [EfficiencyImpact]    FLOAT (53)      NOT NULL,
    [Cost]                DECIMAL (18, 2) NOT NULL,
    [RecommendedForStyle] INT             NOT NULL,
    [IsSafetyCritical]    BIT             NOT NULL
);


