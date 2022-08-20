using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Models
{
    public class Patient
    {
        
        [Required (ErrorMessage ="username is required")]
        public string? username { get; set; }


        [Required(ErrorMessage ="password is required")]
        public string? password { get; set; }

        [Required(ErrorMessage ="confirm password is required")]
        public string? confirmpassword { get; set; }


        [Required(ErrorMessage ="fullname is required")]
        public string? fullName { get; set; }

        [Required(ErrorMessage ="email is required")]
        [DataType(DataType.EmailAddress)]
        
        public string? email { get; set; }

        public string imageName { get; set; }=string.Empty;
        

        [Required(ErrorMessage ="role is required")]

        [DefaultValue("patient")]
        public string? role { get; set; } 

        [Required(ErrorMessage ="telephone is required")]
        [DataType(DataType.PhoneNumber)]
        public string? telephone { get; set; }
        public string? address { get; set; }


        [Required(ErrorMessage ="gender is required")]
        public string? gender { get; set; }

        [DefaultValue(true)]
        public bool termsAccepted { get; set; }

        

        
        
    }
}