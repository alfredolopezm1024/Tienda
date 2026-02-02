using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entity.Models
{
    public class Articulo
    {
        [Key]
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descripcion {  get; set; }
        public decimal Precio { get; set; }
        public byte[] Imagen {  get; set; }
        public int stock { get; set; }
    }
}
