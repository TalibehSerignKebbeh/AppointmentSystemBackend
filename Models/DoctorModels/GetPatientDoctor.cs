using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class GetPatientDoctor
    {
         [Key]
        public int userId { get; set; }
        public string? username { get; set; }
         public string? role { get; set; }= string.Empty;
        public string? imageName { get; set; } = string.Empty;
        public string dept { get; set; } = string.Empty;
        public string? specialisation { get; set; }
        public string? email { get; set; }= string.Empty;
        public string? gender { get; set; }= string.Empty;
        public string? telephone { get; set; } = string.Empty;
        public bool isApproved { get; set; }
    }
}