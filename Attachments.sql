CREATE TABLE [dbo].[Attachments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MemberId] [int] NOT NULL,
	[AttachmentTypeId] [int] NOT NULL,
	[FileName] [nvarchar](250) NOT NULL,
	[FileGUID] [nvarchar](50) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreationDate] [date] NOT NULL,
	[ModifiedBy] [int] NULL,
	[ModificationDate] [date] NULL,
 CONSTRAINT [PK_Attachment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Attachments] ADD  CONSTRAINT [DF_Attachment_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO

ALTER TABLE [dbo].[Attachments] ADD  CONSTRAINT [DF_Attachment_CreationDate]  DEFAULT (getdate()) FOR [CreationDate]
GO

