﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Table("Deposites")]
    public class Deposites : BaseEntity
    {
        public int MemberId { get; set; }
        public virtual Members? Members { get; set; }
        public int AmountId { get; set; }
        //public virtual Amounts Amounts { get; set; }
        public DateTime DepositeDate { get; set; }
        public bool IsVerified { get; set; }
        public int VerifiedBy { get; set; }
        public DateTime? VerificationDate { get; set; }
        public List<Attachments> Attachments { get; set; }
    }
}
