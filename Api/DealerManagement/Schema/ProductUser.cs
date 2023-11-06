using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schema
{
    public class ProductUserRequest
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public ProductRequest Product { get; set; }

        public int UserId { get; set; }
        public UserRequest User { get; set; }
    }
    public class ProductUserResponse
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public ProductResponse Product { get; set; }

        public int UserId { get; set; }
        public UserResponse User { get; set; }
    }
}
