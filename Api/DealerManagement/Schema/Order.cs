using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schema
{
    public class OrderRequest
    {
        public DateTime OrderDate { get; set; }
        public string PaymentOption { get; set; }
        public int StatusId { get; set; }
        public int UserId { get; set; }

        public List<ProductOrderRequest> ProductOrders { get; set; }
    }

    public class OrderResponse
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string PaymentOption { get; set; }
        public string StatusName { get; set; }
        public string UserName { get; set; }
        public List<ProductOrderResponse> ProductOrders { get; set; }
    }
}
