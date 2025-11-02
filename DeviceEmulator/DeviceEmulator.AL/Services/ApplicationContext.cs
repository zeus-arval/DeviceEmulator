using System.Net.Sockets;
using DeviceEmulator.ApplicationLayer.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DeviceEmulator.ApplicationLayer
{
    public class ApplicationContext(IServiceProvider serviceProvider)
    {
        // TODO implement next clients
        //public RestApiClient RestApiClient { get; set; }
        //public TCPClient TcpClient { get; set; }
        //public COMPortClient ComPortClient { get; set; }
        public ILocalLoggerFactory LoggerFactory { get; init; } = serviceProvider.GetRequiredService<ILocalLoggerFactory>();
        public IServiceProvider ServiceProvider { get; init; } = serviceProvider;
    }
}
