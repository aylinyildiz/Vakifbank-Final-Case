using Data.Context;
using Data.Domain;
using Data.Repository;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Uow
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DealerDbContext dbContext;

        public UnitOfWork(DealerDbContext dbContext)
        {
            this.dbContext = dbContext;

            UserRepository = new GenericRepository<User>(dbContext);
            AddressRepository = new GenericRepository<Address>(dbContext);
            AccountTransactionRepository = new GenericRepository<AccountTransaction>(dbContext);
            AccountRepository = new GenericRepository<Account>(dbContext);
            CardRepository = new GenericRepository<Card>(dbContext);
            EftRepository = new GenericRepository<Eft>(dbContext);
            BillRepository = new GenericRepository<Bill>(dbContext);
            MessageRepository = new GenericRepository<Message>(dbContext);
            OrderRepository = new GenericRepository<Order>(dbContext);
            ProductRepository = new GenericRepository<Product>(dbContext);
            ReportRepository = new GenericRepository<Report>(dbContext);
        }

        public void Complete()
        {
            dbContext.SaveChanges();
        }

        public void CompleteTransaction()
        {
            using (var transaction = dbContext.Database.BeginTransaction())
            {
                try
                {
                    dbContext.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Log.Error("CompleteTransactionError", ex);
                }
            }
        }


        public IGenericRepository<User> UserRepository { get; private set; }
        public IGenericRepository<Account> AccountRepository { get; private set; }
        public IGenericRepository<AccountTransaction> AccountTransactionRepository { get; private set; }
        public IGenericRepository<Address> AddressRepository { get; private set; }
        public IGenericRepository<Card> CardRepository { get; private set; }
        public IGenericRepository<Eft> EftRepository { get; private set; }
        public IGenericRepository<Bill> BillRepository { get; private set; }
        public IGenericRepository<Message> MessageRepository { get; private set; }
        public IGenericRepository<Order> OrderRepository { get; private set; }
        public IGenericRepository<Product> ProductRepository { get; private set; }
        public IGenericRepository<Report> ReportRepository { get; private set; }
    }
}
