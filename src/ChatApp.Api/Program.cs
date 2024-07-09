using ChatApp.Api.Extensions;
using ChatApp.Api.SignalR;
using ChatApp.Application;
using ChatApp.Infrastructure;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddSignalR();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.ApplyMigrations();
}

app.UseHttpsRedirection();

app.UseCustomExceptionHandler();

app.UseAuthorization();

app.MapControllers();

app.MapHub<ChatHub>("/chathub");

await app.RunAsync();

public partial class Program;
