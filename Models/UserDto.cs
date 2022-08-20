using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
namespace appointmentApi.Models
{
    public class Models
    {
        public string? fullName { get; set; }
        
        [Required]
        public string? username { get; set; }

        [Required]
        public string? password { get; set; }
        [Required]
        [DefaultValue("admin")]
        public string? role { get; set; }
       
        public string? imageName { get; set; } = string.Empty;

        [Required]

         [DataType(DataType.EmailAddress)]
        public string? email { get; set; }

         [DataType(DataType.PhoneNumber)]
        public string? telephone { get; set; } = string.Empty;
        public string? gender { get; set; }
        public string? address { get; set; }


        [DefaultValue(false)]
        public bool isApproved { get; set; }

        public bool termsAccepted { get; set; }

        
        
        
        
        
    }
}