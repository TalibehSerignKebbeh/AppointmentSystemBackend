using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class GetAppPatient
    {
             public int userId { get; set; }
        public string? fullName { get; set; }
        public string? username { get; set; }
        public string? role { get; set; }
        public string? imageName { get; set; } = string.Empty;
        public string? email { get; set; }
        public string? telephone { get; set; } = string.Empty;
        public bool isApproved { get; set; }
        public string? gender { get; set; }
        public string? address { get; set; }
    }
}