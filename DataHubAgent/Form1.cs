using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using System;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;

namespace DataHub.DataHubAgent
{
    public partial class Form1 : Form
    {
        private static ILogger _logger { get; set; }
        private static IConfigurationRoot _configuration;
        private static IServiceProvider _serviceProvider;
        private static DataHubAgent.Models.DataHubMongo.IMongoService mongoConn;

        public string strInitialLocalDirectory { get; set; }
        public string strArchiveLocalDirectory { get; set; }

        public bool IsListening { get; set; }

        private AWSManagers.clsS3Manager S3Bucket;
        
        public FileSystemWatcher fsw;

        public Form1(ILogger logger, IConfigurationRoot config, IServiceProvider services)
        {
            _logger = logger;
            _configuration = config;
            _serviceProvider = services;

            InitializeComponent();
        }

        // File --> Open, temporarily reset the folder to watch
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        // File --> Exit
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // About popup
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 box = new AboutBox1();
            box.ShowDialog();
        }

        // Initialize file system watcher from configuration
        private void Form1_Load(object sender, EventArgs e)
        {
            GetCustomersFromSQLExpress();

            InitializeConfig();

            GetStuffFromMongo();

            InitializeFSW();

            StopWatching();
            
            // clear the on-screen log
            richTextBox1.Text = "";
        }

        private void GetStuffFromMongo()
        {
            mongoConn = new Models.DataHubMongo.MongoHelper();
            mongoConn.Initialize(_logger, _configuration.GetConnectionString("MongoDbContext"));

            // do stuff
            mongoConn.DoWork("restaurants");
        }

        private void InitializeFSW()
        {
            // Create a new FileSystemWatcher and set its properties- https://msdn.microsoft.com/en-us/library/system.io.filesystemwatcher(v=vs.110).aspx
            fsw = new FileSystemWatcher();

            fsw.Path = strInitialLocalDirectory;
            fsw.Filter = _configuration["File:FileWatcherFilter"];  // only care about JSON files coming through, may have to modify for BSON
            // set events
            fsw.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            // https://msdn.microsoft.com/en-us/library/system.io.notifyfilters(v=vs.110).aspx 
            fsw.Changed += new FileSystemEventHandler(OnChanged);
            fsw.Created += new FileSystemEventHandler(OnChanged);
            // fsw.Deleted += new FileSystemEventHandler(OnChanged);
            // fsw.Renamed += new RenamedEventHandler(OnRenamed);
            _logger.Trace("INFO: Established file system watcher in PAUSED state with filter {0}", fsw.Filter);

        }

        private void GetCustomersFromSQLExpress()
        {
            _logger.Trace("using {0}", _configuration.GetConnectionString("CoreDbContext"));

            using (var db = new DataHubAgent.DataHubCoreContext())
            {
                //db.Customer.Add(new Customer { Url = "http://blogs.msdn.com/adonet" });
                //var count = db.SaveChanges();
                //Console.WriteLine("{0} records saved to database", count);

                //var customer = await _context.Customers
                //.SingleOrDefaultAsync(m => m.Id == id);
                //if (customer == null)
                //{
                //    return null;
                //}
                //return customer;

                foreach (var cust in db.Customer)
                {
                    _logger.Debug("Name {0}", cust.Name);
                }
            }

            var db2 = _serviceProvider.GetService<DataHubCoreContext>();
            foreach (var rec in db2.Customer)
            {
                _logger.Debug("Description {0}", rec.Description);
            }
        }

        public void InitializeConfig()
        {
            // read in configuration file settings for default targets
            // strInitialLocalDirectory = ConfigurationManager.AppSettings["InitialLocalDirectory"];
            strInitialLocalDirectory = _configuration["File:InitialLocalDirectory"];
            _logger.Trace("INFO: Setting initial watch directory to {0}", strInitialLocalDirectory);
            strArchiveLocalDirectory = _configuration["File:ArchiveLocalDirectory"];
            _logger.Trace("INFO: Setting archive watch directory to {0}", strArchiveLocalDirectory);

            this.textBox1.Text = strInitialLocalDirectory;

            string startS3 = _configuration["AWS:InitialS3Bucket"];
            string endS3 = _configuration["AWS:ArchiveS3Bucket"];
            _logger.Trace("INFO: Initializing S3 client for profile {0}", _configuration["AWSProfileName"]);

            if (_configuration["AWS:AWSEnabled"] == "true")
            {
                S3Bucket = new AWSManagers.clsS3Manager(_logger, startS3, endS3);
                this.textBox2.Text = S3Bucket.bucketName;
                _logger.Trace("INFO: Setting target S3 bucket to {0}", S3Bucket.bucketName);
            } else {
                _logger.Trace("Not using AWS");
            }
        }

        // start the local FileWatcher
        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartWatching();
        }

        // stops the local file watcher
        private void endToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StopWatching();
         }

        // FileSystemWatcher start and stop actions
        public void StartWatching()
        {
            IsListening = true;
            button1.Image = Image.FromFile(".//Graphics//Start-52.png");

            if (fsw != null)
            {
                fsw.EnableRaisingEvents = true;
                // textBox1.BackColor = Color.DarkGray;
                _logger.Trace("INFO: Established file system watcher in LISTENING state");
            }
        }

        public void StopWatching()
        {
            IsListening = false;
            button1.Image = Image.FromFile(".//Graphics//Stop-52.png");
            this.Refresh();

            if (fsw != null)
            {
                fsw.EnableRaisingEvents = false;
                // textBox1.BackColor = Color.WhiteSmoke;
                _logger.Trace("INFO: Established file system watcher in PAUSED state");
            }
        }

        // FileSystemWatcher Event Handlers
        private void OnChanged(object source, FileSystemEventArgs e)
        {
            //Copies file to another directory
            _logger.Trace("INFO: Found a file change for {0} & event {1}", e.FullPath, e.ChangeType);

            switch (e.ChangeType)
            {
                case WatcherChangeTypes.Created:
                    {
                        // move the file to archive and process it
                        // https://msdn.microsoft.com/en-us/library/cc148994.aspx
                        string fileName = e.Name;
                        string sourceFile = e.FullPath;
                        string destFile = System.IO.Path.Combine(strArchiveLocalDirectory, fileName);
                        _logger.Trace("INFO: Target file is {0}", destFile);

                        if (!System.IO.Directory.Exists(strArchiveLocalDirectory))
                        {
                            System.IO.Directory.CreateDirectory(strArchiveLocalDirectory);
                        }

                        if (System.IO.File.Exists(sourceFile))
                        {
                            if (!System.IO.File.Exists(destFile))
                            {
                                if (_configuration["AWS:AWSEnabled"] == "true")
                                {
                                    // process the file (copy to S3 initially)
                                    if (S3Bucket == null)
                                    {
                                        string startS3 = _configuration["AWS:InitialS3Bucket"];
                                        string endS3 = _configuration["AWS:ArchiveS3Bucket"];
                                        _logger.Trace("INFO: Initializing S3 client for profile {0}", _configuration["AWSProfileName"]);

                                        S3Bucket = new AWSManagers.clsS3Manager(_logger, startS3, endS3);
                                    }
                                        
                                    string destName = Path.GetFileName(sourceFile);
                                    S3Bucket.UploadFile(sourceFile, destName);
                                }

                                string log = string.Concat(destFile, " @ ", DateTime.Now.ToString());
                                UpdateOnScreenLog(log);
                                
                                // move the file to archive directory
                                System.IO.File.Move(sourceFile, destFile);
                                _logger.Trace("INFO: File copied {0} to {1}", sourceFile, destFile);
                            }
                            else
                                _logger.Trace("ERROR: Target file exists {0}", destFile);
                        }
                        break;
                    }
                case WatcherChangeTypes.Changed:
                    break;
                default:
                    break;
            }
        }

        public void UpdateOnScreenLog(string text)
        {
            if (InvokeRequired)
                Invoke(new Action<string>(UpdateOnScreenLog), text);
            else
            {
                //richTextBox1.Text = string.Concat(richTextBox1.Text, Environment.NewLine, log);
                richTextBox1.AppendText(string.Concat(text, Environment.NewLine));
            }
        }

        private void OnRenamed(object source, FileSystemEventArgs e)
        {
            _logger.Trace("INFO: Unhandled rename change for {0} & event {1}", e.FullPath, e.ChangeType);
        }

        // start/stop watching
        private void button1_Click(object sender, EventArgs e)
        {
            if(IsListening)
            {
                StopWatching();
            } else
            {
                StartWatching();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // TODO: pass through the logger, services, config as needed - may not need once collapsed
            Form next = new Form3();
            next.ShowDialog();
        }
    }
}
