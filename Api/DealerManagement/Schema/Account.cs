using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schema
{
    public class AccountRequest
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string CurrencyCode { get; set; }
    }

    public class AccountResponse
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public int AccountNumber { get; set; }
        public string IBAN { get; set; }
        public decimal Balance { get; set; }
        public string CurrencyCode { get; set; }
        public DateTime OpenDate { get; set; }
        public DateTime? CloseDate { get; set; }
        public int? CardId { get; set; }

        public virtual List<EftResponse> EftTransactions { get; set; }
        public virtual List<AccountTransactionResponse> AccountTransactions { get; set; }
    }
}
