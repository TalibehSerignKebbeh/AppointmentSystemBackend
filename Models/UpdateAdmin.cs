using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Models
{
    public class UpdateAdmin
    {
        [Key]
        public int userId { get; set; }

        [Required(ErrorMessage ="username is required")]
        public string? username { get; set; }

        [Required(ErrorMessage ="password is required")]
        public string? password { get; set; }

       [Required(ErrorMessage ="fullname is required")]
        public string? fullName { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage ="email is required")]
        public string? email { get; set; }

         [Required(ErrorMessage ="telephone is required")]
        [DataType(DataType.PhoneNumber)]
        public string? telephone { get; set; }


         [Required(ErrorMessage ="role is required")]
        public string? role { get; set; } 


        [Required(ErrorMessage ="gender is required")]
        public string? gender { get; set; }

        [Required(ErrorMessage ="address is required")]
        public string? address { get; set; }

         public string imageName { get; set; } = string.Empty;

        public bool isApproved { get; set; }
        public IFormFile? ImageFile { get; set; }



    }
}