using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SecondProjectByHistoryTable_Log_.DTOs.Histories
{
    public class HistoryDTO
    {
        [Required]
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
