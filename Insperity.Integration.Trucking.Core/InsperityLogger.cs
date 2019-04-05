using System;
using System.Threading.Tasks;
using Insperity.Infrastructure.Log4net;

namespace Insperity.Integration.Trucking.Core
{
    public class InsperityLogger : ILogger
    {
        private readonly Logger _logger;

        public InsperityLogger() : this(new Logger(""))
        { }

        public InsperityLogger(Logger logger)
        {
            _logger = logger;
        }

        public async Task LogMessage(object message)
        {
            //_logger.Log("", AppLogLevel.StandardTrace, message); //Commented out for demo purposes
            Console.WriteLine($"Handled {message}.");
            await Task.CompletedTask;
        }

        public async Task LogError(Exception e)
        {
            //_logger.Log("", AppLogLevel.Error, e); //Commented out for demo purposes
            Console.WriteLine(e.Message);
            await Task.CompletedTask;
        }
    }
}
