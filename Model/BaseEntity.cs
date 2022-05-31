using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Model
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            CreationDate = DateTime.Now;
            IsActive = true;
            CreatedBy = 1;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id {get; set;}
        public DateTime CreationDate { get; set;}
        public DateTime? ModificationDate { get; set;}
        public int? CreatedBy { get; set;}
        public int? ModifiedBy { get; set;}
        public bool IsActive { get; set;}
    }
}
