CREATE TABLE [dbo].[Threads] (
    [Id]              INT            IDENTITY (1, 1) NOT NULL,
    [Name]            NVARCHAR (50)  NOT NULL,
    [TextDescription] NVARCHAR (MAX) NOT NULL,
    [TopicId]         INT            NULL,
    [CreatedAt]       DATE           NULL,
    [UserId]          NVARCHAR (128) NOT NULL,
    [UserName]        NVARCHAR (50)  NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([TopicId]) REFERENCES [dbo].[Topics] ([Id]),
    FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id])
);

