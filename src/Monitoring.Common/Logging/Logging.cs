using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;
using Serilog.Sinks.MSSqlServer;

namespace Monitoring.Common.Logging;

public static class Logging
{
    public static Action<HostBuilderContext, LoggerConfiguration> ConfigureLogger =>
      (context, loggerConfiguration) =>
      {
          var env = context.HostingEnvironment;
          loggerConfiguration.MinimumLevel.Information()
          .Enrich.FromLogContext()
          .Enrich.WithProperty("ApplicationName", env.ApplicationName)
          .Enrich.WithExceptionDetails()
          .WriteTo.MSSqlServer(context.Configuration["ConnectionStrings:Product"], new MSSqlServerSinkOptions()
          {
              AutoCreateSqlTable = true,
              TableName = "Logs",
              SchemaName = "log",
              BatchPostingLimit = 100,
              BatchPeriod = new TimeSpan(0, 0, 10)
          })
          .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName ?? "Development")

          .WriteTo.Console();
          if (context.HostingEnvironment.IsDevelopment())
          {
              loggerConfiguration.MinimumLevel.Override("Product", LogEventLevel.Debug);
          }

          var elasticUrl = context.Configuration.GetValue<string>("ElasticConfiguration:Uri");
          if (!string.IsNullOrEmpty(elasticUrl))
          {
              loggerConfiguration.WriteTo.Elasticsearch(
              new ElasticsearchSinkOptions(new Uri(elasticUrl))
            {
                AutoRegisterTemplate = true,
                AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv8,
                IndexFormat =
                  $"{context.Configuration["ApplicationName"]}-logs-{context.HostingEnvironment.EnvironmentName?.ToLower().Replace(".", "-")}-{DateTime.Now:yyyy-MM}",
                MinimumLogEventLevel = LogEventLevel.Debug
            });
          }

      };
    
    public static Action<HostBuilderContext, LoggerConfiguration> ConfigureMicrosoftLogger =>
      (context, loggerConfiguration) =>
      {
          var env = context.HostingEnvironment;
          loggerConfiguration.MinimumLevel.Information()
          .Enrich.FromLogContext()
          .Enrich.WithProperty("ApplicationName", env.ApplicationName)
          .Enrich.WithExceptionDetails()
          .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
          .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
          .WriteTo.MSSqlServer(context.Configuration["ConnectionStrings:Product"], new MSSqlServerSinkOptions()
          {
              AutoCreateSqlTable = true,
              TableName = "Microsoft_Logs",
              SchemaName = "log",
              BatchPostingLimit = 100,
              BatchPeriod = new TimeSpan(0, 0, 10)
          })
          .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName ?? "Development")

          .WriteTo.Console();
          if (context.HostingEnvironment.IsDevelopment())
          {
              loggerConfiguration.MinimumLevel.Override("Product", LogEventLevel.Debug);
          }

          var elasticUrl = context.Configuration.GetValue<string>("ElasticConfiguration:Uri");
          if (!string.IsNullOrEmpty(elasticUrl))
          {
              loggerConfiguration.WriteTo.Elasticsearch(
              new ElasticsearchSinkOptions(new Uri(elasticUrl))
            {
                AutoRegisterTemplate = true,
                AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv8,
                IndexFormat =
                  $"{context.Configuration["ApplicationName"]}-microsoft-logs-{context.HostingEnvironment.EnvironmentName?.ToLower().Replace(".", "-")}-{DateTime.Now:yyyy-MM}",
                MinimumLogEventLevel = LogEventLevel.Debug
            });
          }

      };
}