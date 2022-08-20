using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using appointmentApi.Models.Entities;
namespace ResponseMessages
{
    public class LoginResponse
    {
        public User? user { get; set; }
        public string? token { get; set; }
    }
}