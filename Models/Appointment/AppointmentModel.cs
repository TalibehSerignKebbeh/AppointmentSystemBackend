using System.ComponentModel.DataAnnotations;
namespace Models
{
    public class AppointmentModel
    {
         [Required(ErrorMessage ="Please tell us the reason for your appointment")]
         public string? reason { get; set; } 

         [DataType(DataType.DateTime)]
         [Required(ErrorMessage ="date field is required")]
          public DateTime appDate { get; set; }

        [DataType(DataType.Time)]
         [Required(ErrorMessage ="time field is required")]
        public string? time { get; set; }

        [Required(ErrorMessage = "patient is required")]
        public int patientRefId { get; set; }
        public int doctorRefId { get; set; }

    }
}