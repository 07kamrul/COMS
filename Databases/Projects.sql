CREATE TABLE [dbo].[Projects](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProjectName] [nvarchar](100) NOT NULL,
	[ProjectSize] [nvarchar](100) NOT NULL,
	[Location] [nvarchar](100) NOT NULL,
	[NumberOfShare] [int] NOT NULL,
	[TotalCost] [int] NOT NULL,
	[StartDate] [date] NOT NULL,
	[EndDate] [date] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreationDate] [date] NOT NULL,
	[ModifiedBy] [int] NULL,
	[ModificationDate] [date] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Projects] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Projects] ADD  CONSTRAINT [DF_Projects_CreatedDate]  DEFAULT (getdate()) FOR [CreationDate]
GO

ALTER TABLE [dbo].[Projects] ADD  CONSTRAINT [DF_Projects_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
