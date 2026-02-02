using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Entity.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Direccion {  get; set; }
        public string correo { get; set; }
        public string password { get; set; }
    }
}
