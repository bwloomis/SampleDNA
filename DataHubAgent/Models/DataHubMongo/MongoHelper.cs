using MongoDB.Bson;
using MongoDB.Driver;
using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace DataHub.DataHubAgent.Models.DataHubMongo
{
    public interface IMongoService
    {
        void Initialize(ILogger logger, string ConnectionString);
        void Connect();
        Boolean Disconnect();

        Task DoWork(string collection);
    }

    class MongoHelper : IMongoService
    {
        private MongoClient _client { get; set; }
        private IMongoDatabase _database { get; set; }
        private ILogger _logger { get; set; }
        public string _connString { get; private set; }

        Task IMongoService.DoWork(string collection)
        {
            Show_Collection_Names();

            foreach (var x in Show_Collection_Names())
            {
                _logger.Debug("stuff in mongo {0}", x.ToBson().ToString());
            }
            // throw new NotImplementedException();
            return null;
        }

        void IMongoService.Initialize(ILogger logger, string ConnectionString)
        {
            _logger = logger;
            _connString = ConnectionString;

            try
            {
                Connect();
            } catch (MongoClientException mce)
            {
                _logger.Debug("could not connect with cs = {0} {1}", _connString, mce.Message);
            }
        }

        public void Connect()
        {
            // _logger.Trace("Mongo: Connection string: {0}", _connString);
            if (_connString == "")
                throw new Exception("No connection string");
            try
            {
                _client = new MongoClient(_connString);
                _logger.Trace("Mongo: Connected to MongoDB: {0}", _connString);        
            } catch (MongoAuthenticationException mae)
            {
                _logger.Debug("could not connect with cs = {0} {1}", _connString, mae.Message);
            } catch (Exception e)
            {
                _logger.Debug("General error {0} Inner: {1}", e.Message, e.InnerException.ToString());
            }
        }
        
        //public void Connect(string Host, int Port)
        //{
        //    string connect = "mongodb://" + Host + ":" + Port.ToString();
        //    _logger.Trace("INFO: Connection string:{0}", connect);
        //    _connString = connect;

        //    var newClient = new MongoClient(connect);
        //    _logger.Trace("INFO: Connected to MongoDB:{0}", connect);
        //    _client = newClient;
        //}

        public IMongoDatabase GetDatabase(string name)
        {
            var result = _client.GetDatabase(name);
            return result;
        }

        public List<string> Show_Collection_Names()
        {
            _logger.Debug("found {0} docs", GetDocumentCount("test"));

            var result = new List<string>(100);
            try
            {
                foreach (var item in _database.ListCollectionsAsync().Result.ToListAsync<BsonDocument>().Result)
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

        // TODO: need to implement finalize()
        public Boolean Disconnect()
        {
            _logger.Trace("INFO: Disconnecting from MongoDB:{0}", _connString);
            return false;
        }

        public int GetDocumentCount(string collection)
        {
            int DocumentCount;

            _database = _client.GetDatabase(collection);

            var count = _database.GetCollection<BsonDocument>(collection).Count(new BsonDocument());
            // var count = await mongoDatabase.GetCollection<BsonDocument>(collection).CountAsync(new BsonDocument());
            // TODO: make these async calls, using await

            bool b = int.TryParse(Newtonsoft.Json.JsonConvert.ToString(count), out DocumentCount);
            return DocumentCount;  // can't return INT in asynch
        }
    }
}


        
      
   