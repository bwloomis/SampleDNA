using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// custom
using NLog;

namespace DataHub.SendMailService
{
    public interface IEmailService
    {
        // only used in console apps
        void Initialize(ILogger logger);

        Task SendEmailAsync(string email, string subject, string message);
    }

    public class SendMailService : IEmailService
    {
        // private readonly IConsoleLogger _consoleLogger;
        private ILogger _logger;

        // use ctor in ASP .NET for DI
        public SendMailService()
        {
            // _logger = logger;
        }

        // use this method for construction/DI in console applications
        public void Initialize(ILogger logger)
        {
            _logger = logger;
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            _logger.Debug("made it here");

            return null;
        }
    }
}
