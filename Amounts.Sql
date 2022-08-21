CREATE TABLE [dbo].[Amounts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MemberId] [int] NOT NULL,
	[Amount] [int] NOT NULL,
	[AmountDate] [date] NOT NULL,
	[DipositeId] [int] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreationDate] [date] NOT NULL,
	[ModifiedBy] [int] NULL,
	[ModificationDate] [date] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Amounts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Amounts] ADD  CONSTRAINT [DF_Amounts_CreatedDate]  DEFAULT (getdate()) FOR [CreationDate]
GO

ALTER TABLE [dbo].[Amounts] ADD  CONSTRAINT [DF_Amounts_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
