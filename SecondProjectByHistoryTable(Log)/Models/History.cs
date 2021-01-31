using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SecondProjectByHistoryTable_Log_.Models
{
    public class History
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int VendorId { get; set; }
        [Required]
        public string JsonResult { get; set; }
        [Required]
        [StringLength(128)]
        public string Operation { get; set; }
    }
}
