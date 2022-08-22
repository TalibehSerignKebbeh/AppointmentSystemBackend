using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
namespace Models
{
    public class Admin
    {
        public string? fullName { get; set; }
        public string? role { get; set; }

        
        [Required]
        public string? username { get; set; }

        [Required]
        public string? password { get; set; }
        [Required]
       
        public string? imageName { get; set; } = string.Empty;

        [Required]

         [DataType(DataType.EmailAddress)]
        public string? email { get; set; }

        public string? address { get; set; }

         [DataType(DataType.PhoneNumber)]
        public string? telephone { get; set; } = string.Empty;
        public string? gender { get; set; }

  

        public IFormFile? ImageFile { get; set; }






    }
}