using System.ComponentModel.DataAnnotations;
namespace Models
{
    public class AuthModel
    {
        [Required (ErrorMessage ="email or username is required")]
        public string? loginKey { get; set; }
        [Required (ErrorMessage ="password is required")]
        public string? password { get; set; }
        
    }
}