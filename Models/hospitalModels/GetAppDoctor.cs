using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models;

    public class GetAppDoctor
{
     public int userId { get; set; }
        public string? fullName { get; set; }

        public string? username { get; set; }
         public string? role { get; set; }= string.Empty;
        public string? imageName { get; set; } = string.Empty;
        public string dept { get; set; } = string.Empty;
        public string? specialisation { get; set; }
        public string? email { get; set; }= string.Empty;
        public string? gender { get; set; }= string.Empty;
        public string? telephone { get; set; } = string.Empty;
        public string? address { get; set; }
        public bool isApproved { get; set; }
        public bool isActive { get; set; }


}
