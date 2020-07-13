using MongoDB.Bson;
using MongoDB.Driver;
using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace DataHub.DataHubAgent
{
    public class clsCobbDB
    {
        public MongoClient client { get; set; }

        public IMongoDatabase database { get; set; }

        // BWL - added other methods to use config file
        public void Connect()
        {
            string connect = ConnectionString;
            logger.Trace("INFO: Connection string:{0}", connect);
            if (connect == "") throw new Exception("No connection string");

            var newClient = new MongoClient(connect);
            logger.Trace("INFO: Connected to MongoDB:{0}", connect);
            client = newClient;
        }

        public void Connect(string Host)
        {
            string connect = string.Concat(getAppConfigString("MongoDBPrefix"), Host );
            logger.Trace("INFO: Connection string:{0}", connect);
            if (connect == "") throw new Exception("No connection string");
            ConnectionString = connect;

            var newClient = new MongoClient(connect);
            logger.Trace("INFO: Connected to MongoDB:{0}", connect);
            client = newClient;
        }

        public static Logger logger;
        public clsCobbDB()
        {
            logger = LogManager.GetLogger("thisapp");
        }

        // end BWL

        public void Connect(string Host, int Port)
        {
            string connect = "mongodb://" + Host + ":" + Port.ToString();
            logger.Trace("INFO: Connection string:{0}", connect);
            ConnectionString = connect;

            var newClient = new MongoClient(connect);
            logger.Trace("INFO: Connected to MongoDB:{0}", connect);
            client = newClient;
        }

        public IMongoDatabase GetDatabase(string name)
        {
            var result = client.GetDatabase(name);
            return result;
        }

        public void GetCollection(string name)
        { }

        public List<string> Show_Collection_Names()
        {
            var result = new List<string>(100);
            try
            {
                foreach (var item in database.ListCollectionsAsync().Result.ToListAsync<BsonDocument>().Result)
                {
                    var collectionName = item.Values.ToString();
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

            return result;
        }

        // BWL adds below
        private string connString = "";
        public string ConnectionString
        {
            get
            {
                if (connString == "")
                {
                    // try to set the connection string
                    connString = string.Concat(getAppConfigString("MongoDBPrefix"),
                        getAppConfigString("MongoDBHostURL"));    // "mongodb://localhost";
                }
                return this.connString;
            }
            set
            {
                this.connString = value;
            }
        }

        private string connDatabase = "";
        public string ConnectedDatabase
        {
            get
            {
                if (connDatabase == "")
                {
                    connDatabase = getAppConfigString("MongoDBRepository");
                }
                return this.connDatabase;
            }
            set
            {
                this.connDatabase = value;
            }
        }

        private string connCollection = "";
        public string ConnectedCollection
        {
            get
            {
                if (connCollection == "")
                {
                    connCollection = getAppConfigString("MongoDBCollection");
                }
                return this.connCollection;
            }
            set
            {
                this.connCollection = value;
            }
        }

        public MongoClient mongoConn = null;
        public IMongoDatabase mongoDatabase = null;

        // BWL - old functions from my side
        //public Boolean Connect()
        //{
        //    try
        //    {
        //        // TODO: add logging
        //        mongoConn = new MongoClient(this.ConnectionString); // set connection string from app.config file
        //        mongoDatabase = mongoConn.GetDatabase(this.ConnectedDatabase);
        //        return true;
        //    }
        //    catch (Exception e)
        //    {
        //        return false;
        //    }
        //}

        // TODO: need to implement finalize()
        public Boolean Disconnect()
        {
            logger.Trace("INFO: Disconnecting from MongoDB:{0}", ConnectionString);
            return false;
        }

        public int DocumentCount;
        public async void GetDocumentCount(string collection)
        {
            if (collection != "")
                ConnectedCollection = collection;

            var count = mongoDatabase.GetCollection<BsonDocument>(ConnectedCollection).Count(new BsonDocument());
            // var count = await mongoDatabase.GetCollection<BsonDocument>(collection).CountAsync(new BsonDocument());
            // TODO: make these async calls, using await

            bool b = int.TryParse(Newtonsoft.Json.JsonConvert.ToString(count), out DocumentCount);
            // return DocumentCount;  can't return INT in asynch
        }

        // simple calls to CM
        public string getDBConfigurationString(string key)
        {
            string s = "";

            // return first instance of a configuration file setting
            ConnectionStringSettingsCollection sc = ConfigurationManager.ConnectionStrings;
            if (sc != null)
                foreach (ConnectionStringSettings str in sc)
                {
                    if (str.ProviderName == key)
                    {
                        s = str.ConnectionString;
                        break;
                    }
                }

            return s;
        }

        public string getAppConfigString(string key)
        {
            string s = "";

            // return first instance of a configuration file setting for this key
            var sc = ConfigurationManager.AppSettings;
            if (sc[key] != null)
                s = sc[key];

            return s;
        }
    }
}



