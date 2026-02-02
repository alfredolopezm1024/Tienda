using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.DTO
{
    public class OrderRequestDTO
    {
        public int ClienteId { get; set; }
        public List<OrderItem> ItemList { get; set; }
    }
}
