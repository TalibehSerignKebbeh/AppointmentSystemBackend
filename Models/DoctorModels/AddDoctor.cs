using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class AddDoctor
    {
        [Required(ErrorMessage ="username is required")]
        public string? username { get; set; }

        [Required(ErrorMessage ="password is required")]
        [Compare("confirmpassword")]
        public string? password { get; set; }

        [Required(ErrorMessage ="confirm password is required")]
        public string? confirmpassword { get; set; }


       [Required(ErrorMessage ="fullname is required")]
        public string? fullName { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage ="email is required")]
        public string? email { get; set; }
        

         [Required(ErrorMessage ="telephone is required")]
        [DataType(DataType.PhoneNumber)]
        public string? telephone { get; set; }

        [Required(ErrorMessage ="department is required")]
         public string? dept { get; set; }
         
        [Required(ErrorMessage ="specialisation is required")]
         public string? specialisation { get; set; }

        [Required(ErrorMessage ="gender is required")]
        public string? gender { get; set; }

        [Required(ErrorMessage ="address is required")]
        public string? address { get; set; }

        [Required(ErrorMessage ="hospital is required")]

        public int hospitalRefId { get; set; }


           [NotMapped]
        public IFormFile? ImageFile { get; set; }
        
    }
}