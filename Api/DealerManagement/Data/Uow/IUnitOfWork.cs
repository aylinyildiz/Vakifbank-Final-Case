using Data.Domain;
using Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Uow
{
    public interface IUnitOfWork
    {
        void Complete();
        void CompleteTransaction();

        IGenericRepository<Account> AccountRepository { get; }
        IGenericRepository<AccountTransaction> AccountTransactionRepository { get; }
        IGenericRepository<Address> AddressRepository { get; }
        IGenericRepository<Bill> BillRepository { get; }
        IGenericRepository<Card> CardRepository { get; }
        IGenericRepository<Eft> EftRepository { get; }
        IGenericRepository<Message> MessageRepository { get; }
        IGenericRepository<Order> OrderRepository { get; }
        IGenericRepository<Product> ProductRepository { get; }
        IGenericRepository<Report> ReportRepository { get; }
        IGenericRepository<User> UserRepository { get; }

    }
}
