using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models;
public class AddUser
{
    [Required]
    public string? fullName { get; set; }

    [Required]
    public string? username { get; set; }

    [Required]
    public string? password { get; set; }
    [Required]
    public string? role { get; set; }

    public string? imageName { get; set; } = string.Empty;

    [Required]

    [DataType(DataType.EmailAddress)]
    public string? email { get; set; }
    public string? address { get; set; }

    [DataType(DataType.PhoneNumber)]
    public string? telephone { get; set; } = string.Empty;

    [DefaultValue(false)]
    public bool isApproved { get; set; }

    public string? gender { get; set; }

      [NotMapped]
        public IFormFile? ImageFile { get; set; }

}