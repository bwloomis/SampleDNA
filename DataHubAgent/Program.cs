using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using System;
using System.IO;
using System.Windows.Forms;

namespace DataHub.DataHubAgent
{
    static class Program
    {
        public static Logger logger { get; set; }
        public static IConfigurationRoot Configuration;
        public static IServiceProvider serviceProvider;

        public static void InitializeConfiguration()
        {
            // get the configuration settings
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
        }

        public static void InitializeLogging()
        {
            logger = LogManager.GetLogger(Configuration["GeneSeek:LogFileApplicationName"]);
        }

        public static void InitializeServiceDI()
        {
            // set up dependency injection
            serviceProvider = new ServiceCollection()
                // .AddSingleton<IEmailService, DataHub.SendMailService.SendMailService>()
                .AddDbContext<DataHubCoreContext>(options => options.UseSqlServer(Configuration.GetConnectionString("CoreDbContext")))
                .BuildServiceProvider();

            // add logging to email component
            // serviceProvider.GetService<IEmailService>().Initialize(logger);
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            InitializeConfiguration();

            InitializeLogging();

            logger.Debug("beginning DI");
            InitializeServiceDI();

            // print current working directory
            logger.Debug("Starting in directory {0}", Directory.GetCurrentDirectory());

            // determine if we are in development or production
            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            logger.Debug("RUNNING IN {0} MODE", environment);

            Application.Run(new Form1(logger, Configuration, serviceProvider));
        }
    }
}
