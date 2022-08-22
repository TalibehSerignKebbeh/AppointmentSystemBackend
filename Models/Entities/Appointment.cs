using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using appointmentApi.Models.Entities;
using Models;

namespace appointmentApi.Models
{
    public class Appointment
    {
        [Key]
        public int appId { get; set; }

        public string reason { get; set; } = string.Empty;
        public string secret { get; set; } = string.Empty;


        [DataType(DataType.Date)]
        public DateTime appDate { get; set; }
          
          [DataType(DataType.Time)]
        public string? time { get; set; }
        // public DateTime time { get; set; }

        [ForeignKey("User")]
        public int patientRefId { get; set; }

        [ForeignKey("User")]
        public int doctorRefId { get; set; }

        [ForeignKey("Hospital")]
        public int hospitalRefId { get; set; }
        
        [DefaultValue(false)]
        public bool isComplete {get; set;}
        
        [DefaultValue(false)]
        public bool isCancell { get; set; }

        [DefaultValue(false)]
        public bool isAccepted { get; set; }
        public bool isDeleted { get; set; }

        [NotMapped]
        public GetAppPatient? patient { get; set; }
         [NotMapped]
        public GetAppDoctor? doctor { get; set; }

        [NotMapped]
        public Hospital? hospital { get; set; }
       
    }
}