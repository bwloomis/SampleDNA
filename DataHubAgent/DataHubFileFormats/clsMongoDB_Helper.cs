using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace DataHub.DataHubAgent
{
    public class clsMongoDB_Helper
    {
        public List<string> PropertyNames(object item)
        {
            var bson =  JObject.Parse(item.ToString());
            var result = new List<string>(bson.Count);
            foreach (var x in bson)
                result.Add(x.Key);
            return result;
        }
        public List<string> PropertyValues(object item)
        {
            var bson = JObject.Parse(item.ToString());
            var result = new List<string>(bson.Count);
            foreach (var x in bson)
                result.Add(x.Value.ToString());
            return result;
        }

        public void WriteBSON(string Filename, object item)
        {
            if (File.Exists(Filename)) File.Delete(Filename);
            string BSON = item.ToString();
            File.WriteAllText(Filename, BSON);
        }
        public string[] GetFileLines(string Filename)
        {
            if (!File.Exists(Filename))
                throw new FileNotFoundException(Filename);
            var lines = File.ReadAllLines(Filename);
            return lines;
        }

    }
}
