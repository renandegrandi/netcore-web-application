using KestrelWebApplication.Core.Config;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.Configure<ExampleConfig>(builder.Configuration.GetSection(ExampleConfig.Key));

var app = builder.Build();

app.UseAuthorization();

app.MapControllers();

app.Run();
