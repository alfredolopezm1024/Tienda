using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entity.Models
{
    public class ClienteArticulo
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public int ArticuloId { get; set; }
        public int Cantidad { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        [ForeignKey("ClienteId")]
        public virtual Cliente Cliente { get; set; }
        [ForeignKey("ArticuloId")]
        public virtual Articulo Articulo { get; set; }
    }
}

