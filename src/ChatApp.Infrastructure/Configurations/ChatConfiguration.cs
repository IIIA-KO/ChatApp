using ChatApp.Domain.Chats;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatApp.Infrastructure.Configurations
{
    internal sealed class ChatConfiguration : IEntityTypeConfiguration<Chat>
    {
        public void Configure(EntityTypeBuilder<Chat> builder)
        {
            builder.ToTable("chats");

            builder.HasKey(chat => chat.Id);

            builder
                .Property(chat => chat.Id)
                .HasConversion(id => id.Value, value => new ChatId(value));

            builder
                .Property(chat => chat.ChatName)
                .IsRequired()
                .HasMaxLength(100)
                .HasConversion(chatName => chatName.Value, value => new ChatName(value));

            builder.Property(chat => chat.CreatorId).IsRequired();

            builder
                .HasOne(chat => chat.Creator)
                .WithMany(user => user.CreatedChats)
                .HasForeignKey(chat => chat.CreatorId);

            builder
                .HasMany(chat => chat.Participants)
                .WithMany(user => user.Chats)
                .UsingEntity(j => j.ToTable("chat_participants"));

            builder
                .HasMany(chat => chat.Messages)
                .WithOne(message => message.Chat)
                .HasForeignKey(message => message.ChatId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
