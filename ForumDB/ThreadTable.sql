USE [ForumDB]
GO

/****** Object:  Table [dbo].[Threads]    Script Date: 20.03.2018 23:31:23 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Threads](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[TextDescription] [nvarchar](max) NOT NULL,
	[TopicId] [int] NULL,
	[CreatedAt] [date] NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[UserName] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[Threads]  WITH CHECK ADD FOREIGN KEY([TopicId])
REFERENCES [dbo].[Topics] ([Id])
GO

ALTER TABLE [dbo].[Threads]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO


