using Microsoft.Extensions.DependencyInjection;

namespace DeviceEmulator.App
{
    public class DIInitializer
    {
        public static ServiceCollection InitializeContainer()
        {
            var services = new ServiceCollection();

            //services.Configure()
            //services.AddLogging(builder => builder.Add)

            return services;
        }
    }
}

