using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Account
{
    public class NewUserDto
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required string Token { get; set; }
    }
}