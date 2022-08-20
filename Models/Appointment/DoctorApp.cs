using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class DoctorApp
    {
        public int appId { get; set; }

        public string reason { get; set; } = string.Empty;

        public DateTime appDate { get; set; }
          
        public string? time { get; set; }
        // public DateTime time { get; set; }

        public int patientRefId { get; set; }

        public int doctorRefId { get; set; }
        
        public bool isComplete {get; set;}
        
        public bool isCancell { get; set; }

        public bool isAccepted { get; set; }

        public GetPatient? patient { get; set; }

    }
}