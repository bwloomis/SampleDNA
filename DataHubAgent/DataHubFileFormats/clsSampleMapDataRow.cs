using Newtonsoft.Json;

namespace DataHub.DataHubAgent
{
    public class clsSampleMapDataRow
    {
        public clsSampleMapDataRow()
        { }
        public clsSampleMapDataRow(string[] Items)
        {
            Load(Items);
        }
        public int Index { get; set; }
        public string Name { get; set; }
        public string ID { get; set; }
        public string Gender { get; set; }
        public string Plate { get; set; }
        public string Well { get; set; }
        public string Group { get; set; }
        public string Parent1 { get; set; }
        public string Parent2 { get; set; }
        public string Replicate { get; set; }
        public string SentrixPosition { get; set; }

        public void Load(string[] dataRow)
        {
            this.Index = int.Parse(dataRow[0]);
            this.Name = dataRow[1];
            this.ID = dataRow[2];
            this.Gender = dataRow[3];
            this.Plate = dataRow[4];
            this.Well = dataRow[5];
            this.Group = dataRow[6];
            this.Parent1 = dataRow[7];
            this.Parent2 = dataRow[8];
            this.Replicate = dataRow[9];
            this.SentrixPosition = dataRow[10];
        }

        public override string ToString()
        {
            string json = JsonConvert.SerializeObject(this, Formatting.Indented);
            return json;
        }
    }
}
