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
    [Table("Product", Schema = "dbo")]
    public class Product :BaseModel
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
    }
    public class ProductConfigruration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.InsertUserId).IsRequired();
            builder.Property(x => x.UpdateUserId).IsRequired().HasDefaultValue(0);
            builder.Property(x => x.InsertDate).IsRequired();
            builder.Property(x => x.UpdateDate).IsRequired(false);
            builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);

            builder.Property(x => x.StockQuantity).IsRequired(true);
            builder.Property(x => x.Price).IsRequired(true);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
        }
    }
}
