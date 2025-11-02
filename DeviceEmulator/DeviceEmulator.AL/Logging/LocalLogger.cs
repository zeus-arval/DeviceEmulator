using Microsoft.Extensions.Logging;

namespace DeviceEmulator.ApplicationLayer.Logging
{
    public interface ILocalLoggerFactory
    {
        ILocalLogger CreateLocalLogger(string name);
    }

    public class LocalLoggerFactory(ILoggerProvider localLoggerProvider) : ILocalLoggerFactory
    {
        private readonly ILoggerProvider _localLoggerProvider = localLoggerProvider;

        public ILocalLogger CreateLocalLogger(string name)
        {
            return new LocalLogger(_localLoggerProvider.CreateLogger(name));
        }
    }

    public static class LocalLoggerExtensions
    {
        public static ILocalLogger CreateLocalLogger<T>(this ILocalLoggerFactory factory)
        {
            return factory.CreateLocalLogger(typeof(T).Name);
        }
    }

    public interface ILocalLogger : ILogger
    {
    }

    public class LocalLogger(ILogger logger) : ILocalLogger
    {
        private readonly ILogger _logger = logger;

        public IDisposable BeginScope<TState>(TState state)
        {
            return _logger.BeginScope(state);
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return _logger.IsEnabled(logLevel);
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception,
            Func<TState, Exception, string> formatter)
        {
            _logger.Log(logLevel, eventId, state, exception, formatter);
        }
    }
}
