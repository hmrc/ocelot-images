using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ImageAPI.Models
{
    public class Image
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ImageName { get; set; }
        [Required]
        public int Placeholder { get; set; }
        public bool Approved { get; set; }         
        [StringLength(60, MinimumLength = 5)]
        [Required]
        public string Description { get; set; }
        public string Path { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime? UploadedDate { get; set; } = DateTime.UtcNow;       
        public int? ApprovedByPID { get; set; }      
        public int? UploadedByPID { get; set; }
        public DateTime? ApprovedDate { get; set; }    
        
    }
}
