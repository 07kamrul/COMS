CREATE TABLE [dbo].[Members](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[FatherName] [nvarchar](50) NULL,
	[MotherName] [nvarchar](50) NULL,
	[JoiningDate] [date] NOT NULL,
	[DateOfBirth] [date] NOT NULL,
	[Nid] [bigint] NOT NULL,
	[Phone] [int] NOT NULL,
	[Email] [nvarchar](50) NULL,
	[Gender] [nvarchar](50) NULL,
	[Nationality] [nvarchar](50) NULL,
    [ProjectId] [int] NOT NULL,
    [NumberOfAccount] [int] NOT NULL,
    [TotalAmounts] [numeric](18,2) NOT NULL,
	[PresentAddress] [nvarchar](250) NULL,
	[PermanentAddress] [nvarchar](250) NULL,
	[MaritalStatus] [nvarchar](50) NULL,
	[Religion] [nvarchar](50) NULL,
	[Occupation] [nvarchar](50) NULL,
	[Designation] [nvarchar](50) NULL,
	[Company] [nvarchar](50) NULL,
	[CreatedBy] [int] NOT NULL,
	[CreationDate] [date] NOT NULL,
	[ModifiedBy] [int] NULL,
	[ModificationDate] [date] NULL,
	[IsVerified] [bit] NOT NULL,
	[VerifiedBy] [int] NULL,
	[VerificationDate] [date] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Members] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO



