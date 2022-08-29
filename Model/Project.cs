using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Table("Projects")]
    public class Project : BaseEntity
    {
        public string ProjectName { get; set; }
        public string ProjectSize { get; set; }
        public string Location { get; set; }
        public int NumberOfShare { get; set; }
        public int TotalCost { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
