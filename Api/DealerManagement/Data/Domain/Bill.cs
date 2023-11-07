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
    [Table("Bill", Schema = "dbo")]
    public class Bill : BaseModel
    {
        public DateTime BillDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Currency { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public List<Order> Orders { get; set; }
    }

    public class BillConfiguration : IEntityTypeConfiguration<Bill>
    {
        public void Configure(EntityTypeBuilder<Bill> builder)
        {
            builder.Property(x => x.InsertUserId).IsRequired();
            builder.Property(x => x.UpdateUserId).IsRequired().HasDefaultValue(0);
            builder.Property(x => x.InsertDate).IsRequired();
            builder.Property(x => x.UpdateDate).IsRequired(false);
            builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);
            builder.Property(x => x.BillDate).IsRequired();
            builder.Property(x => x.TotalAmount).IsRequired();
            builder.Property(x => x.Currency).IsRequired();
            builder.Property(x => x.UserId).IsRequired();

            builder.HasOne(x => x.Address)
                .WithMany()
                .HasForeignKey(x => x.AddressId)
                .IsRequired(true).OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.User)
                .WithMany(x=>x.Bills)
                .HasForeignKey(x => x.UserId)
                 .IsRequired(true).OnDelete(DeleteBehavior.NoAction);
        }
    }

}
