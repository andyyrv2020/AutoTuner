USE [aspnet-AutoTuner-e05d2182-a428-4aed-86fa-8fa058f1614c]
GO

/****** Object: Table [dbo].[PerformanceHistories] Script Date: 11/17/2025 6:49:44 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PerformanceHistories] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [CarId]       INT           NOT NULL,
    [OldPower]    INT           NOT NULL,
    [NewPower]    INT           NOT NULL,
    [OldTorque]   INT           NOT NULL,
    [NewTorque]   INT           NOT NULL,
    [DateApplied] DATETIME2 (7) NOT NULL
);


GO
CREATE NONCLUSTERED INDEX [IX_PerformanceHistories_CarId]
    ON [dbo].[PerformanceHistories]([CarId] ASC);


GO
ALTER TABLE [dbo].[PerformanceHistories]
    ADD CONSTRAINT [PK_PerformanceHistories] PRIMARY KEY CLUSTERED ([Id] ASC);


GO
ALTER TABLE [dbo].[PerformanceHistories]
    ADD CONSTRAINT [FK_PerformanceHistories_Cars_CarId] FOREIGN KEY ([CarId]) REFERENCES [dbo].[Cars] ([Id]) ON DELETE CASCADE;


