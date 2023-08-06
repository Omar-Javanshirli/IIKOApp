using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C._Domain.Dtos.RequestDto
{
    public  class LoginRequestDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
