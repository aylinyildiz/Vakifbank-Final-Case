using Base.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Domain
{
    [Table("Account", Schema = "dbo")]
    public class Account :BaseModel
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public string Name { get; set; }
        public int AccountNumber { get; set; }
        public string IBAN { get; set; }
        public decimal Balance { get; set; }
        public decimal Limit { get; set; }
        public DateTime OpenDate { get; set; }
        public DateTime? CloseDate { get; set; }

        public int? DealerId { get; set; }

        public virtual List<Eft> Eft { get; set; }
        public virtual List<AccountTransaction> AccountTransactions { get; set; }

        public int? CardId { get; set; }
        public virtual Card Card { get; set; }
    }

    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.Property(x => x.InsertUserId).IsRequired();
            builder.Property(x => x.UpdateUserId).IsRequired().HasDefaultValue(0);
            builder.Property(x => x.InsertDate).IsRequired();
            builder.Property(x => x.UpdateDate).IsRequired(false);
            builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);

            builder.Property(x => x.UserId).IsRequired(true);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.AccountNumber).IsRequired(true);
            builder.Property(x => x.IBAN).IsRequired().HasMaxLength(34);
            builder.Property(x => x.Balance).IsRequired().HasPrecision(18, 2).HasDefaultValue(0);
            builder.Property(x => x.OpenDate).IsRequired();
            builder.Property(x => x.CloseDate).IsRequired(false);

            builder.HasIndex(x => x.UserId);
            builder.HasIndex(x => x.AccountNumber).IsUnique(true);

            builder.HasMany(x => x.Eft)
                .WithOne(x => x.Account)
                .HasForeignKey(x => x.AccountId)
                .IsRequired(true);

            builder.HasMany(x => x.AccountTransactions)
                .WithOne(x => x.Account)
                .HasForeignKey(x => x.AccountId)
                .IsRequired(true);

            builder.HasOne(x => x.Card)
                .WithOne(x => x.Account)
                .HasForeignKey<Card>().IsRequired(true);
        }
    }
}
