//Index Name    ID Gender  Plate Well    Group Parent1 Parent2 Replicate   SentrixPosition
//1	219AEC4416	219AEC4416 Unknown 98706	A1					201031700012_R01C01
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace DataHub.DataHubAgent
{
    public class clsSampleMap
    {
        // Private properties
        private string Filename { get; set; }
        private List<string> RawRows { get; set; }  // used for test BSON serialize / deserialize
        private List<string> ColumnHeading { get; set; }

        public List<clsSampleMapDataRow> Rows { get; set; }
        public clsSampleMap() { }
        public clsSampleMap(string Filename, bool RetainText)
        {
            this.Filename = Filename;
            if (!File.Exists(Filename))
                throw new FileNotFoundException(Filename);
            var lines = File.ReadAllLines(Filename);
            LoadFile(lines, RetainText);
        }


        public void LoadFile(string Filename, bool RetainText)
        {
            if (!File.Exists(Filename))
                throw new FileNotFoundException(Filename);

            this.Filename = Filename;
            var lines = File.ReadAllLines(Filename);
            LoadFile(lines, RetainText);
        }

    public void LoadFile(string[] lines, bool RetainText)
        {
            if (RetainText)
            {
                // Save a copy of the raw text for later testing BSON serialization accuracy
                if (RawRows == null)
                    RawRows = new List<string>(lines.Count());
                RawRows.AddRange(lines);
            }

            // Split the text line by tabs
            string[] fields = lines[0].Split("\t".ToCharArray());

            // First line is the column headings
            this.ColumnHeading = new List<string>();
            this.ColumnHeading.AddRange(fields);

            // Remaining rows are data
            this.Rows = new List<clsSampleMapDataRow>();
            for (int i = 1; i < lines.Count(); i++)
            {
                string[] items = lines[i].Split("\t".ToCharArray());
                var dr = new clsSampleMapDataRow(items);
                this.Rows.Add(dr);
            }
            return;
        }
        public void WriteBSON(string Filename)
        {
            if (File.Exists(Filename)) File.Delete(Filename);
            string BSON = this.ToString();
            File.WriteAllText(Filename, BSON);
        }

        public override string ToString()
        {
            string json = JsonConvert.SerializeObject(this, Formatting.Indented);
            return json;
        }
    }
}