CREATE TABLE [dbo].[AttachmentTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](250) NOT NULL,
	[IsActive] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreationDate] [date] NOT NULL,
	[ModifiedBy] [int] NULL,
	[ModificationDate] [date] NULL,
 CONSTRAINT [PK_AttachmentType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[AttachmentTypes] ADD  CONSTRAINT [DF_AttachmentType_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO

ALTER TABLE [dbo].[AttachmentTypes] ADD  CONSTRAINT [DF_AttachmentType_CreationDate]  DEFAULT (getdate()) FOR [CreationDate]
GO

