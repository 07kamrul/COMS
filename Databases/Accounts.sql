CREATE TABLE [dbo].[Accounts](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [MemberId] [int] NOT NULL,
    [ProjectId] [int] NOT NULL,
    [OpaningDate] [date] NOT NULL,
    [ClosingDate] [date] NOT NULL,
    [DueAmounts] [numeric](18,2) NOT NULL,
    [PayableAmounts] [numeric](18,2) NOT NULL,
    [TotalAmounts] [numeric](18,2) NOT NULL,
    [IsVerified] [bit] NOT NULL,
    [VerifiedBy] [int] NULL,
    [VerificationDate] [date] NULL,
    [CreatedBy] [int] NOT NULL,
    [CreationDate] [date] NOT NULL,
    [ModifiedBy] [int] NULL,
    [ModificationDate] [date] NULL,
    [IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Accounts] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Accounts] ADD  CONSTRAINT [DF_Accounts_CreatedDate]  DEFAULT (getdate()) FOR [CreationDate]
GO

ALTER TABLE [dbo].[Accounts] ADD  CONSTRAINT [DF_Accounts_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
