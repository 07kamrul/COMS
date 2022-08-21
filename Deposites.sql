CREATE TABLE [dbo].[Deposites](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [MemberId] [int] NOT NULL,
    [AmountId] [int] NOT NULL,
    [DepositeDate] [date] NOT NULL,
    [CreatedBy] [int] NOT NULL,
    [CreationDate] [date] NOT NULL,
    [ModifiedBy] [int] NULL,
    [ModificationDate] [date] NULL,
    [IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Deposites] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Deposites] ADD  CONSTRAINT [DF_Deposite_CreatedDate]  DEFAULT (getdate()) FOR [CreationDate]
GO

ALTER TABLE [dbo].[Deposites] ADD  CONSTRAINT [DF_Deposite_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
