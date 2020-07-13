using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

// my includes
using MySql.Data.MySqlClient;
using System.Data.Common;
using System.Text;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.IO;

namespace CobbWebAPICoreDB.Controllers
{
    [Route("api/[controller]")]
    public class Jobs : Controller
    {

        private string WriteReaderToJSON(DbDataReader reader)
        {
            int rowCount = 0;
            StringBuilder sb = new StringBuilder();

            if (reader == null || reader.FieldCount == 0) return null;

            sb.Append(@"{""Rows"":[");

            while (reader.Read())
            {
                sb.Append("{");
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    sb.Append("\"" + reader.GetName(i) + "\":");
                    sb.Append("\"" + reader[i] + "\"");
                    sb.Append(i == (reader.FieldCount-1) ? "" : ",");
                }
                sb.Append("},");
                rowCount++;
            }

            if (rowCount > 0)
            {
                int index = sb.ToString().LastIndexOf(",");
                sb.Remove(index, 1);    // remove trailing comma
            }
            sb.Append("]}");

            return sb.ToString();
        }

        static public IConfiguration Configuration { get; set; }
        static public ConfigurationBuilder configurationBuilder;
        private string GetConnectionString()
        {
            // TODO: dependency injection to create this whole class... https://docs.microsoft.com/en-us/aspnet/web-api/overview/advanced/dependency-injection

            // get MySQL connection string
            if(configurationBuilder == null)
                configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("config.json");
            // configurationBuilder.AddInMemoryCollection(DefaultConfigurationStrings);
            Configuration = configurationBuilder.Build();
            return Configuration["AppConfiguration:MySQLConnection"];
            // s = Configuration.Get("AppConfiguration:MySQLConnection");
        }

        static public MySqlConnection connection { get; set; }
        public bool OpenConnection()
        {
            connection = new MySqlConnection(GetConnectionString());
            connection.Open();
            return true;
        }

        //// GET: api/jobs
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    if (OpenConnection())
        //    {
        //        // read something
        //        MySqlCommand command = new MySqlCommand("SELECT * FROM chix.jobs;", connection);
        //        DbDataReader reader = command.ExecuteReader();
        //        string json = WriteReaderToJSON(reader);

        //        connection.Close();
        //        // return new string[] { "value1", "value2", $"Hello {Configuration["Profile:UserName"]}", json, s };
        //        return new string[] { json };
        //    }
        //    else
        //        return null;
        //}

        // GET: api/jobs
        [HttpGet]
        public string Get()
        {
            if (OpenConnection())
            {
                // read something
                MySqlCommand command = new MySqlCommand("SELECT * FROM chix.jobs;", connection);
                DbDataReader reader = command.ExecuteReader();
                string json = WriteReaderToJSON(reader);

                connection.Close();
                // return new string[] { "value1", "value2", $"Hello {Configuration["Profile:UserName"]}", json, s };
                return json;
            }
            else
                return null;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

