using Monitoring.Api;
using Monitoring.Application;
using Monitoring.Common.Logging;
using Monitoring.Infrastructure;
using Monitoring.Infrastructure.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.
//builder.Services.AddHealthChecks().Services.AddDbContext<ApplicationDbContext>();

builder.Services
  .AddApplicationServices(builder.Configuration)
  .AddInfrastructureServices(builder.Configuration)
  .AddApiServices(builder.Configuration);

builder.Host.UseSerilog(Logging.ConfigureLogger);
builder.Host.UseSerilog(Logging.ConfigureMicrosoftLogger);

var app = builder.Build();

app.AddCorrelationIdMiddleware();
app.UseApiServices();
app.UseSerilogRequestLogging();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
  await app.InitialiseDatabaseAsync();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
