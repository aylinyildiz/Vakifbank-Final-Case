using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schema
{
    public class AddressRequest
    {
        public int UserId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string PostalCode { get; set; }
    }

    public class AddressResponse
    {
        public int UserId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string PostalCode { get; set; }
        public string Name { get; set; }
    }
}
