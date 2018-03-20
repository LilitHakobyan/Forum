CREATE TABLE [dbo].[Posts] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [Text]     NVARCHAR (50)  NOT NULL,
    [ThreadId] INT            NOT NULL,
    [UserId]   NVARCHAR (128) NOT NULL,
    [UserName] NVARCHAR (50)  NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([ThreadId]) REFERENCES [dbo].[Threads] ([Id]),
    FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id])
);

