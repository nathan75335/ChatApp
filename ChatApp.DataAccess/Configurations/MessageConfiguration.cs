using ChatApp.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatApp.DataAccess.Configurations;

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.Sender)
            .WithMany(x => x.MessagesSenders)
            .HasForeignKey(x => x.SenderId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Receiver)
           .WithMany(x => x.MessagesReceivers)
           .HasForeignKey(x => x.ReceiverId)
           .OnDelete(DeleteBehavior.NoAction);

        builder.Property(x => x.Text)
            .IsRequired();
        builder.Property(x => x.MessageStatus)
            .IsRequired(false);

        builder.ToTable("Messages");
    }
}
