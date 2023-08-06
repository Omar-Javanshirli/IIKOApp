using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C._Domain.Dtos.RequestDto
{
    public class AuthenticateRequestDto
    {
        [Required]
        public string SyetemName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
