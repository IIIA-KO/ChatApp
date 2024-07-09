using ChatApp.Domain.Chats;
using ChatApp.Domain.UserChats;
using ChatApp.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatApp.Infrastructure.Configurations
{
    internal sealed class UserChatConfiguration : IEntityTypeConfiguration<UserChat>
    {
        public void Configure(EntityTypeBuilder<UserChat> builder)
        {
            builder.ToTable("users_chats");

            builder.HasKey(uc => new { uc.UserId, uc.ChatId });

            builder
                .Property(uc => uc.UserId)
                .HasConversion(userId => userId.Value, value => new UserId(value));

            builder
                .Property(uc => uc.ChatId)
                .HasConversion(chatId => chatId.Value, value => new ChatId(value));

            builder
                .HasOne(uc => uc.User)
                .WithMany()
                .HasForeignKey(uc => uc.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(uc => uc.Chat)
                .WithMany()
                .HasForeignKey(uc => uc.ChatId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
