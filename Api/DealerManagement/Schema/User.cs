using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schema
{

    public class UserRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int RoleId { get; set; }
    }

    public class UserResponse
    {
        public int UserNumber { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int RoleId { get; set; }

        public virtual List<AddressResponse> Addresses { get; set; }
        public virtual List<AccountResponse> Accounts { get; set; }
    }
}
