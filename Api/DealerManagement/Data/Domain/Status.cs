using Base.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace Data.Domain
{
    [Table("Status", Schema = "dbo")]
    public class Status
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Order Order { get; set; }
    }

    public class StatusConfiguration : IEntityTypeConfiguration<Status>
    {
        public void Configure(EntityTypeBuilder<Status> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
            builder.HasOne(e => e.Order).WithOne(e => e.Status).HasForeignKey<Order>(e => e.StatusId).IsRequired();
        }
    }
}
