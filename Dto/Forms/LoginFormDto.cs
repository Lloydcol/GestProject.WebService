using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestProject.WebService.Dto.Forms
{
    public class LoginFormDto
    {
        public string Email { get; set; }
        public string PlainPassword { get; set; }
    }
}
