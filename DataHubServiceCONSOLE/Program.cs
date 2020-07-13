using System;
using System.Threading.Tasks;
using System.IO;

// custom
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using NLog;
using DataHub.SendMailService;

namespace DataHub.DataHubService
{
    class Program
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
            //serviceProvider = new ServiceCollection()
            //    .AddSingleton<IEmailService, DataHub.SendMailService.SendMailService>()
            //    .BuildServiceProvider();
          
            //// add logging to email component
            //serviceProvider.GetService<IEmailService>().Initialize(logger);
        }

        static void Main(string[] args)
        {
            InitializeConfiguration();

            InitializeLogging();

            InitializeServiceDI();

            // print current working directory
            logger.Debug("Starting in directory {0}", Directory.GetCurrentDirectory());

            // send an email...
            // var email = serviceProvider.GetService<IEmailService>().SendEmailAsync("hi", "bar", "mess");
            
            // determine if we are in development or production
            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            logger.Debug("RUNNING IN {0} MODE", environment);

            // TODO: abstract the MySQL stuff into dbContext, then DI, then do this call!
            // using MySql.Data.MySqlClient;
            // using System.Data.Common;
            //MySqlConnection connection = new MySqlConnection
            //{
            //    ConnectionString = "Server=localhost;User Id=root;Password=P@ssword;Database=chix"
            //};
            //connection.Open();

            //// read something
            //MySqlCommand command = new MySqlCommand("SELECT * FROM chix.jobs;", connection);

            //using (DbDataReader reader = command.ExecuteReader())
            //{
            //    System.Console.WriteLine("Category Id\t\tName\t\tLast Update");
            //    while (reader.Read())
            //    {
            //        string row = $"{reader["idJobs"]}\t\t{reader["jobName"]}\t\t{reader["jobScheduledTime"]}\t\t{reader["jobParameters"]}";
            //        System.Console.WriteLine(row);
            //    }
            //}

            //connection.Close();

            Console.WriteLine($"option1 = {Configuration["GeneSeek:Description"]}");
            Console.ReadLine();
        }
    }
}