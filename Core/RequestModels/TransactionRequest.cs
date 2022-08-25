using System;

namespace Core.RequestModels
{
    public class TransactionRequest
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int ProjectId { get; set; }
        public int InstallmentNo { get; set; }
        public int DueAmounts { get; set; }
        public int PayableAmounts { get; set; }
        public int TransactionAmounts { get; set; }
        public int TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }
        public int VerifiedBy { get; set; }
        public DateTime? VerificationDate { get; set; }
        public AttachmentRequest Attachment { get; set; }
    }
}
