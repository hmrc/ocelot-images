using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ImageAPI.Models
{
    public class ImagePatch
    {
        public int Id { get; set; }       
        public string ImageName { get; set; }    
        public string Placeholder { get; set; }
        public bool Approved { get; set; }   
        public int? ApprovedByPID { get; set; }
        public DateTime? ApprovedDate { get; set; }
    }
}
