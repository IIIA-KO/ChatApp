using ChatApp.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatApp.Infrastructure.Configurations
{
    internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.HasKey(user => user.Id);

            builder
                .Property(user => user.Id)
                .HasConversion(id => id.Value, value => new UserId(value));

            builder
                .Property(user => user.UserName)
                .IsRequired()
                .HasMaxLength(50)
                .HasConversion(username => username.Value, value => new UserName(value));

            builder
                .HasMany(user => user.CreatedChats)
                .WithOne(chat => chat.Creator)
                .HasForeignKey(chat => chat.CreatorId)
                .IsRequired();

            builder
                .HasMany(user => user.Chats)
                .WithMany(chat => chat.Participants)
                .UsingEntity(j => j.ToTable("chat_participants"));

            builder.HasIndex(user => user.UserName).IsUnique();
        }
    }
}
