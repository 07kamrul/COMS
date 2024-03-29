CREATE TABLE [dbo].[Transactions](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [MemberId] [int] NOT NULL,
    [ProjectId] [int] NOT NULL,
    [AccountId] [int] NOT NULL,
    [InstallmentNo] [int] NOT NULL,
    [DueAmounts] [numeric](18,2) NOT NULL,
    [PayableAmounts] [numeric](18,2) NOT NULL,
    [TransactionAmounts] [numeric](18,2) NOT NULL,
    [TransactionType] [int] NOT NULL,
    [TransactionDate] [date] NOT NULL,
    [IsVerified] [bit] NOT NULL,
    [VerifiedBy] [int] NULL,
    [VerificationDate] [date] NULL,
    [CreatedBy] [int] NOT NULL,
    [CreationDate] [date] NOT NULL,
    [ModifiedBy] [int] NULL,
    [ModificationDate] [date] NULL,
    [IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Transactions] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Transactions] ADD  CONSTRAINT [DF_Transactions_CreatedDate]  DEFAULT (getdate()) FOR [CreationDate]
GO

ALTER TABLE [dbo].[Transactions] ADD  CONSTRAINT [DF_Transactions_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
