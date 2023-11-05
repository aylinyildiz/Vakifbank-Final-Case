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
    [Table("Report", Schema = "dbo")]
    public class Report : BaseModel
    {
        public string ReportType { get; set; } //aylık, günlük,yıllık,haftalık
        public int UserId { get; set; }
        public DateTime ReportDate { get; set; }
        public decimal TotalOrderAmount { get; set; }
        public int ProductStockReportId { get; set; } ///?
    }

    public class ReportConfigruration : IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> builder)
        {
            builder.Property(x => x.InsertUserId).IsRequired();
            builder.Property(x => x.UpdateUserId).IsRequired().HasDefaultValue(0);
            builder.Property(x => x.InsertDate).IsRequired();
            builder.Property(x => x.UpdateDate).IsRequired(false);
            builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);

            builder.Property(x => x.ReportType).HasMaxLength(50).IsRequired();

            builder.Property(x => x.ReportDate).IsRequired();
            builder.Property(x => x.ReportDate).IsRequired();
         
        }
    }
}
