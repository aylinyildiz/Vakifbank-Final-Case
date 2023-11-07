using Base.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace Data.Domain
{
    [Table("User", Schema = "dbo")]
    public class User : BaseModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime LastActivityDate { get; set; }
        public int RoleId { get; set; }
        public int Profit { get; set; }
        public Role Role { get; set; }
        public List<ProductUser> ProductUsers { get; set; }
        public List<Bill> Bills { get; set; }
        public List<Order> Orders { get; set; }
        public List<Message> Messages1 { get; set; }
        public List<Message> Messages2 { get; set; }
        public virtual List<Address> Addresses { get; set; }
        public virtual List<Account> Accounts { get; set; }
    }

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.InsertUserId).IsRequired();
            builder.Property(x => x.UpdateUserId).IsRequired().HasDefaultValue(0);
            builder.Property(x => x.InsertDate).IsRequired();
            builder.Property(x => x.UpdateDate).IsRequired(false);
            builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);

            builder.Property(x => x.Email).IsRequired().HasMaxLength(50);
            builder.Property(x => x.LastActivityDate).IsRequired();
            builder.Property(x => x.Password).IsRequired().HasMaxLength(50);
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(50);

            builder.HasIndex(x => x.Email).IsUnique(true);

            builder.HasOne(u => u.Role).WithMany(x => x.Users).HasForeignKey(u => u.RoleId).IsRequired().OnDelete(DeleteBehavior.NoAction);


            builder.HasMany(x => x.Accounts).WithOne(x => x.User).HasForeignKey(x => x.UserId).IsRequired(true);

            builder.HasMany(x => x.Addresses)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
            .IsRequired(true);

            builder.HasMany(x => x.ProductUsers).WithOne(x => x.User).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);


        }

    }
}
