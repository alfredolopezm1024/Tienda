using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Entity.DTO
{
    public class LoginDTO
    {
        public string correo { get; set; }
        public string password { get; set; }
    }
}
