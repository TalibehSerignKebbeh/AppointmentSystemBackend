using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using GetModels;
namespace Models;
    public class AddHospital
    {
        public string? name { get; set; }
        public string? location { get; set; }
        public string? region { get; set; }
        public string? district { get; set; }

        public string? mapKey { get; set; }
        public string? category { get; set; }
        public string? imageName { get; set; }    

        [NotMapped]
        public IFormFile? ImageFile { get; set; }

    }
