using Base.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Domain
{
    [Table("Order", Schema = "dbo")]
    public class Order :BaseModel
    {
        public DateTime OrderDate { get; set; }
        public string PaymentOption { get; set; }
        public int StatusId { get; set; }
        public Status Status { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
      
        public List<ProductOrder> ProductOrders { get; set; }
    }

    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(x => x.InsertUserId).IsRequired();
            builder.Property(x => x.UpdateUserId).IsRequired().HasDefaultValue(0);
            builder.Property(x => x.InsertDate).IsRequired();
            builder.Property(x => x.UpdateDate).IsRequired(false);
            builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);

            builder.Property(x => x.StatusId).IsRequired(true);
            builder.Property(x => x.PaymentOption).IsRequired().HasMaxLength(50);

            builder.HasOne(x => x.User).WithMany(x => x.Orders).HasForeignKey(x => x.UserId).IsRequired(true);

            builder.HasMany(x => x.ProductOrders).WithOne(x => x.Order).HasForeignKey(x => x.OrderId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(o => o.Status).WithMany(s => s.Order).HasForeignKey(o => o.StatusId);
        }
    }
}
