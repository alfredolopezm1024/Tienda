using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.DTO
{
    public class LoginResponseDTO
    {
        public int Id { get; set; }
        public string Correo { get; set; }
        public string Token { get; set; }
    }
}
