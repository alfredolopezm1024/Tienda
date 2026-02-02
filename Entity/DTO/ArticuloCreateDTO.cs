using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.DTO
{
    public class ArticuloCreateDTO
    {
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public int TiendaId { get; set; }
        public string ImagenBase64 { get; set; }
    }
}
