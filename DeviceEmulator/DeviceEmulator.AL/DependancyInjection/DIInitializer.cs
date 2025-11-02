using DeviceEmulator.ApplicationLayer;
using DeviceEmulator.ApplicationLayer.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Extensions.Logging;
using Serilog.Formatting;
using ILogger = Serilog.ILogger;

namespace DeviceEmulator.App
{
    public static class DIInitializer
    {
        public static ServiceCollection InitializeContainer()
        {
            var services = new ServiceCollection();

            var configuration = BuildConfiguration();
            var serilogLogger = InitializeSerilogLogger(configuration);

            services.AddSingleton<IConfiguration>(configuration);
            services.AddSingleton<ILocalLoggerFactory>((_) => new LocalLoggerFactory(new SerilogLoggerProvider(serilogLogger, false)));
            services.AddLogging(builder =>
            {
                builder
                    .ClearProviders()
                    .AddConfiguration(configuration.GetSection("Logging"))
                    .AddSerilog(serilogLogger, true)
                    .AddDebug();
            });
            services.AddSingleton<ApplicationContext>();

            return services;
        }

        private static ILogger InitializeSerilogLogger(IConfigurationRoot configuration)
        {
            var loggingDir = configuration["Logging:LogDirectory"];
            var loggingFile = loggingDir.TrimEnd('/') + "/DeviceEmulator.log";
            ITextFormatter formatter = new Serilog.Formatting.Display.MessageTemplateTextFormatter(
                "{Timestamp:yyyy-MM-dd HH:mm:ss.fffffff zzz} {PaddedLevel} {SourceContextMin}: {Message}{NewLine}{Exception}",
                System.Globalization.CultureInfo.InvariantCulture);
            var serilogConf = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Async(conf => conf
                    .File(
                        path: loggingFile,
                        formatter: formatter,
                        fileSizeLimitBytes: 10 * 1024 * 1024,
                        retainedFileCountLimit: null,
                        rollingInterval: RollingInterval.Day,
                        rollOnFileSizeLimit: true));

            return serilogConf.CreateLogger();
        }


        private static IConfigurationRoot BuildConfiguration()
        {
            return new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
        }
    }
}

