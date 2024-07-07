using ChatApp.Domain.Messages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatApp.Infrastructure.Configurations
{
    internal sealed class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.ToTable("messages");

            builder.HasKey(message => message.Id);

            builder
                .Property(message => message.Id)
                .HasConversion(id => id.Value, value => new MessageId(value));

            builder
                .Property(message => message.Content)
                .IsRequired()
                .HasMaxLength(500)
                .HasConversion(content => content.Value, value => new Content(value));

            builder
                .Property(message => message.SentAt)
                .IsRequired();

            builder
                .Property(message => message.UserId)
                .IsRequired();

            builder
                .HasOne(message => message.User)
                .WithMany()
                .HasForeignKey(message => message.UserId);

            builder
                .Property(message => message.ChatId)
                .IsRequired();

            builder
                .HasOne(message => message.Chat)
                .WithMany(chat => chat.Messages)
                .HasForeignKey(message => message.ChatId);
        }
    }
}
