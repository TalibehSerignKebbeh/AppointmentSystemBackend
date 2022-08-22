using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using Models;
namespace appointmentApi.Models.Entities;
    public class Hospital
    {
        public int hospitalId { get; set; }
        public string? name { get; set; }
        public string? location { get; set; }
        public string? region { get; set; }
        public string? district { get; set; }

        public string? mapKey { get; set; }
        public string? category { get; set; }
        public string? imageName { get; set; }

        public virtual ICollection<GetDoctor>? doctors { get; set; }

        public virtual ICollection<Appointment>? appointments { get; set; }

        [DefaultValue(false)]
        public bool isDeleted { get;  set; }

    }
