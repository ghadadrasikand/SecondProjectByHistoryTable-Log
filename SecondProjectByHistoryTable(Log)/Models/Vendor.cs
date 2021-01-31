using SecondProjectByHistoryTable_Log_.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecondProjectEFCoreAttributes.Models
{
    public class Vendor
    {
        public Vendor()
        {
            Tags = new HashSet<Tag>();
        }
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(128)]
        public string Name { get; set; }
        [Required]
        [StringLength(128)]
        public string Title { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public ICollection<Tag> Tags { get; set; }
    }
}
