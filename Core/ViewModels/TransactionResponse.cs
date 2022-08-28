using System;

namespace Core.ViewModels
{
    public class TransactionResponse
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int ProjectId { get; set; }
        public int InstallmentNo { get; set; }
        public double DueAmounts { get; set; }
        public double PayableAmounts { get; set; }
        public double TransactionAmounts { get; set; }
        public int TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }
        public int VerifiedBy { get; set; }
        public DateTime? VerificationDate { get; set; }
        public AttachmentResponse Attachment { get; set; }
    }
}
