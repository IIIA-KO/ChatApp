using ChatApp.Infrastructure;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ChatApp.Application.IntegrationTests.Infrastructure
{
    public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>
    {
        private readonly IServiceScope _scope;
        protected readonly ISender Sender;
        protected readonly ApplicationDbContext DbContext;

        protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
        {
            this._scope = factory.Services.CreateScope();

            this.Sender = this._scope.ServiceProvider.GetRequiredService<ISender>();
            this.DbContext = this._scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        }
    }
}
