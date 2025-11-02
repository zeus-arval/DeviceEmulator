using DeviceEmulator.ApplicationLayer;
using Microsoft.Extensions.Logging;
using ReactiveUI;

namespace DeviceEmulator.UserInterface
{
    public abstract class FrameworkModelCore : ReactiveObject
    {
        protected readonly ILogger Logger;
        protected readonly ApplicationContext Context;

        protected FrameworkModelCore(ApplicationContext context)
        {
            var typeName = GetType().Name;

            Logger = context.LoggerFactory.CreateLocalLogger(nameof(typeName));
            Context = context;
        }
    }
}
