using ChatApp.Application.Abstraction.Clock;
using ChatApp.Application.Abstraction.Data;
using ChatApp.Domain.Abstraction;
using ChatApp.Domain.Chats;
using ChatApp.Domain.Messages;
using ChatApp.Domain.Users;
using ChatApp.Infrastructure.Clock;
using ChatApp.Infrastructure.Data;
using ChatApp.Infrastructure.Outbox;
using ChatApp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace ChatApp.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            services.AddTransient<IDateTimeProvider, DateTimeProvider>();

            AddPersistence(services, configuration);

            AddBackgroundJobs(services, configuration);

            return services;
        }

        private static void AddPersistence(
            IServiceCollection services,
            IConfiguration configuration
        )
        {
            string connectionString =
                configuration.GetConnectionString("Database")
                ?? throw new ArgumentNullException(nameof(configuration));

            services.AddDbContext<ApplicationDbContext>(options =>
                options
                    .UseNpgsql(connectionString, options => options.EnableRetryOnFailure())
                    .UseSnakeCaseNamingConvention()
            );

            services.AddScoped<IChatRepository, ChatRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();

            services.AddSingleton<ISqlConnectionFactory>(_ => new SqlConnectionFactory(
                connectionString
            ));

            services.AddScoped<IUnitOfWork>(serviceProvider =>
                serviceProvider.GetRequiredService<ApplicationDbContext>()
            );
        }

        private static void AddBackgroundJobs(
            IServiceCollection services,
            IConfiguration configuration
        )
        {
            services.Configure<OutboxOptions>(configuration.GetSection("Outbox"));

            services.AddQuartz();

            services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);

            services.ConfigureOptions<ProcessOutboxMessagesJobSetup>();
        }
    }
}
