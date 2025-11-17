USE [aspnet-AutoTuner-e05d2182-a428-4aed-86fa-8fa058f1614c]
GO

/****** Object: Table [dbo].[Cars] Script Date: 11/17/2025 6:49:34 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Cars] (
    [Id]            INT            IDENTITY (1, 1) NOT NULL,
    [UserId]        NVARCHAR (450) NOT NULL,
    [Brand]         NVARCHAR (100) NOT NULL,
    [Model]         NVARCHAR (100) NOT NULL,
    [Year]          INT            NOT NULL,
    [EngineType]    NVARCHAR (100) NOT NULL,
    [HorsePower]    INT            NOT NULL,
    [Torque]        INT            NOT NULL,
    [Weight]        INT            NOT NULL,
    [Drivetrain]    NVARCHAR (100) NOT NULL,
    [DrivingStyle]  INT            NOT NULL,
    [ZeroToHundred] FLOAT (53)     NOT NULL
);


GO
CREATE NONCLUSTERED INDEX [IX_Cars_UserId]
    ON [dbo].[Cars]([UserId] ASC);


GO
ALTER TABLE [dbo].[Cars]
    ADD CONSTRAINT [PK_Cars] PRIMARY KEY CLUSTERED ([Id] ASC);


GO
ALTER TABLE [dbo].[Cars]
    ADD CONSTRAINT [FK_Cars_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE;


