using Data.Domain;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    public class DealerDbContext : DbContext
    {
        public DealerDbContext(DbContextOptions<DealerDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AccountTransactionConfigruration());
            modelBuilder.ApplyConfiguration(new AccountConfigruration());
            modelBuilder.ApplyConfiguration(new AddressConfigruration());
            modelBuilder.ApplyConfiguration(new BillConfigruration());
            modelBuilder.ApplyConfiguration(new CardConfigruration());
            modelBuilder.ApplyConfiguration(new EftTransactionConfigruration());
            modelBuilder.ApplyConfiguration(new MessageConfigruration());
            modelBuilder.ApplyConfiguration(new OrderConfigruration());
            modelBuilder.ApplyConfiguration(new ProductConfigruration());
            modelBuilder.ApplyConfiguration(new ReportConfigruration());
            modelBuilder.ApplyConfiguration(new RoleConfigruration());
            modelBuilder.ApplyConfiguration(new StatusConfigruration());
            modelBuilder.ApplyConfiguration(new UserConfigruration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
