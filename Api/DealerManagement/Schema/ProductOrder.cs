using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schema
{
    public class ProductOrderRequest
    {
        public int ProductCount { get; set; } = 0;
        public int ProductId { get; set; }
    }

    public class ProductOrderResponse
    {
        public int ProductCount { get; set; } = 0;
        public int ProductId { get; set; }
        public ProductResponse Product { get; set; }

        public int OrderId { get; set; }
        public OrderResponse Order { get; set; }
    }
}
