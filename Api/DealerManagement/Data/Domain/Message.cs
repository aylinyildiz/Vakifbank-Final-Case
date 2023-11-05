using Base.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Domain
{
    [Table("Message", Schema = "dbo")]
    public class Message :BaseModel
    {
        public string Content { get; set; }
        public DateTime Date { get; set; }

        public int SenderId { get; set; }
        public User SenderUser { get; set; }
        public int ReceiverId { get; set; }
        public User ReceiverUser { get; set; }
    }

    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.Property(x => x.InsertUserId).IsRequired();
            builder.Property(x => x.UpdateUserId).IsRequired().HasDefaultValue(0);
            builder.Property(x => x.InsertDate).IsRequired();
            builder.Property(x => x.UpdateDate).IsRequired(false);
            builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);

            builder.Property(x => x.Content).HasMaxLength(300).IsRequired(true);
            
            builder.Property(x => x.SenderId).IsRequired();
            builder.Property(x => x.ReceiverId).IsRequired();

            builder.HasOne(x => x.SenderUser)
            .WithMany(x => x.Messages1)
            .HasForeignKey(x => x.SenderId)
            .IsRequired(true).OnDelete(DeleteBehavior.ClientNoAction);

            builder.HasOne(x => x.ReceiverUser)
         .WithMany(x => x.Messages2)
         .HasForeignKey(x => x.ReceiverId)
         .IsRequired(true).OnDelete(DeleteBehavior.ClientNoAction);

        }
    }

}
