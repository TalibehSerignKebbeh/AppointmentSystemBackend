using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class UpdateHospital
    {
         public int hospitalId { get; set; }
        public string? name { get; set; }
        public string? location { get; set; }
        public string? region { get; set; }
        public string? district { get; set; }

        public string? mapKey { get; set; }
        public string? category { get; set; }
        public string? imageName { get; set; }
        public bool isDeleted { get;  set; }
        public IFormFile? ImageFile { get; set; }
    }
}