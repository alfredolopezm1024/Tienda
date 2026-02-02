using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entity.Models
{
    public class TiendaArticulo
    {
        public int TiendaId { get; set; }
        public int ArticuloId { get; set; }
        [Required]
        public DateTime Fecha { get; set; } = DateTime.Now;
        [ForeignKey("TiendaId")]
        public virtual Tienda Tienda { get; set; }
        [ForeignKey("ArticuloId")]
        public virtual Articulo Articulo { get; set; }
    }
}
