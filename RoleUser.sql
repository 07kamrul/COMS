CREATE TABLE [dbo].[RoleUser](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreationDate] [date] NOT NULL,
	[ModifiedBy] [int] NULL,
	[ModificationDate] [date] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_RoleUser] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[RoleUser] ADD  CONSTRAINT [DF_RoleUser_CreatedDate]  DEFAULT (getdate()) FOR [CreationDate]
GO

ALTER TABLE [dbo].[RoleUser] ADD  CONSTRAINT [DF_RoleUser_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
