using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.DTO
{
    public class ClienteDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Direccion { get; set; }
        public string correo { get; set; }
    }
}
