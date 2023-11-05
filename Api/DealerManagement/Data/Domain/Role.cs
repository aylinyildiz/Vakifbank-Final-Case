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
    [Table("Role", Schema = "dbo")]
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }

        public List<User> Users { get; set; }
    }

    public class RoleConfigruration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.Property(x => x.RoleName).HasMaxLength(50).IsRequired();
            builder.HasMany(x => x.Users)
                .WithOne(r => r.Role)
                .HasForeignKey(u => u.RoleId)
                .IsRequired();

        }
    }
}
