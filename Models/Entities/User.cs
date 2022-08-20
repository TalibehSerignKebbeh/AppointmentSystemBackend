using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
namespace appointmentApi.Models.Entities
{
    public class User
    {
        [Key]
        public int userId { get; set; }
        public string? fullName { get; set; }
        public string? username { get; set; }

        //  [DataType(DataType.Password)]
        public string? password { get; set; }

        [DataType(DataType.Text)]
        public string? role { get; set; }
        public string? imageName { get; set; } = string.Empty;
        public string? dept { get; set; } 
        public string? specialisation { get; set; }
         [DataType(DataType.EmailAddress)]
         [EmailAddress]
        public string? email { get; set; }
        public string? gender { get; set; }
        public string? address { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string? telephone { get; set; }
        public DateTime dateRegistered { get; set; } 
        public DateTime lastModified { get; set; } 
        public DateTime dateDeleted { get; set; } 

        [ForeignKey("Hospital")]
        public int hospitalRefId { get; set; }

        
        [DefaultValue(false)]
        public bool isDeleted { get; set; }
        
        [DefaultValue(false)]
        public bool isApproved { get; set; }
        [DefaultValue(false)]
        public bool isActive { get; set; }
        public bool acceptTerms { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }

    }
}