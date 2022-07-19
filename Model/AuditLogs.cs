using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Table("AuditLogs")]
    public class AuditLogs
    {
        [Key]
        public long Id { get; set; }
        
        [Required, MaxLength(100)]
        public string UserID{ get; set; }

        [Required]
        public DateTime EventDateUTC { get; set; }

        [Required, MaxLength(1)]
        public string EventType{ get; set; }

        [Required, MaxLength(100)]
        public string TableName { get; set; }

        [Required, MaxLength(100)]
        public string RecordID { get; set; }

        [Required, MaxLength(100)]
        public string ColumnName { get; set; }

        public string OriginalValue { get; set; }
        public string NewValue { get; set; }
    }
}
