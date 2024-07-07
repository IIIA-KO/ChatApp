using ChatApp.Application.Abstraction.Data;
using ChatApp.Infrastructure;
using ChatApp.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Testcontainers.PostgreSql;

namespace ChatApp.Application.IntegrationTests.Infrastructure
{
    public class IntegrationTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
            .WithImage("postgres:latest")
            .WithDatabase("chatapp")
            .WithUsername("postgres")
            .WithPassword("postgres")
            .Build();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                services.RemoveAll(typeof(DbContextOptions<ApplicationDbContext>));

                string connectionString =
                    $"{this._dbContainer.GetConnectionString()};Pooling=False";

                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseNpgsql(connectionString).UseSnakeCaseNamingConvention()
                );

                services.RemoveAll(typeof(ISqlConnectionFactory));

                services.AddSingleton<ISqlConnectionFactory>(_ => new SqlConnectionFactory(
                    connectionString
                ));
            });
        }

        public async Task InitializeAsync()
        {
            await this._dbContainer.StartAsync();
        }

        public new async Task DisposeAsync()
        {
            await this._dbContainer.StopAsync();
        }
    }
}
