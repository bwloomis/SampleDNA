using System;
using System.Collections.Generic;
using System.Text;

// custom
//using NLog;
using Microsoft.Extensions.Logging;

namespace DataHubServiceCONSOLE
{
    //public class ConsoleLogger // : ILogger
    //{
    //    public Logger logger { get; set; }

    //    public ConsoleLogger()  // could take ILoggerFactory from Microsoft.Extensions.Logging.Console, but we're using NLog
    //    {
    //        logger = LogManager.GetLogger("thisapp");
    //    }
    //}

    public interface IConsoleLogger
    {
        // ideally does more than wraps the log file
        // void DoThing(int number);

        void Debug(string s);
    }

    public class ConsoleLogger : IConsoleLogger
    {
        private readonly ILogger<ConsoleLogger> _logger;

        public ConsoleLogger(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ConsoleLogger>();
        }

        public void Debug(string s)
        {
            _logger.LogDebug(s);
        }

        // public void DoThing(int num) { _logger.Trace(); }
    }
}
