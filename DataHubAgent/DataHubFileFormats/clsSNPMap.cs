//Index;   Name;     Chromosome; Position;  GenTrain Score  SNP     ILMN Strand     Customer Strand     NormID
//1		10080_COIII	      0			0	        0.7349	    [T/C]       BOT             TOP                 0
//2	    10668_COIII	      0	        0	        0.7701	    [A/G]       TOP             BOT                 0

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DataHub.DataHubAgent
{
    public class clsSNPMap
    {
        // Public properties
        public List<clsSNPMapDataRow> Rows { get; set; }
        
        // Private properties
        private string Filename { get; set; }
        private List<string> RawRows { get; set; }  // used for test BSON serialize / deserialize
        private List<string> ColumnHeading { get; set; }

        public clsSNPMap() { }
        public clsSNPMap(string Filename, bool RetainText)
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

        // Methods
        /// <summary>
        /// Returns a SNPMap class filled from the string array. 
        /// Optionally retains the raw text in the object for later testing BSON serialization
        /// </summary>
        /// <param name="lines">String array where each element is a line of the text file. Assumes full file is presented.</param>
        /// <param name="RetainText">True retains the raw test lines passed in parameter 'lines'</param>
        /// <returns></returns>
        public clsSNPMap LoadFile(string[] lines, bool RetainText)
        {
            if (RetainText)
            {
                // Save a copy of the raw text for later testing BSON serialization accuracy
                if (RawRows == null)
                    RawRows = new List<string>(lines.Count());
                RawRows.AddRange(lines);
            }

            // Grab the line of column headings
            string[] fields = lines[0].Split("\t".ToCharArray());
            this.ColumnHeading = new List<string>(fields.Count());
            this.ColumnHeading.AddRange(fields);


            this.Rows = new List<clsSNPMapDataRow>(lines.Count());
            for (int i = 1; i < lines.Count(); i++)
            {
                string[] items = lines[i].Split("\t".ToCharArray());
                var dr = new clsSNPMapDataRow(items);
                this.Rows.Add(dr);
            }
            return this;
        }

        public void WriteBSON(string Filename)
        {
            if (File.Exists(Filename)) File.Delete(Filename);
            var bson = this.ToString();
            File.WriteAllText(Filename, bson);
            //var lines = from x in Rows select x.ToString();
            //File.WriteAllLines(Filename, lines
            
        }


        //public List<string> DisplayHeaders()
        //{
        //    var items = new List<string>();
        //    items.Add("Filename = " + Filename);
        //    return items;
        //}
        //public List<string> DisplayRows()
        //{
        //    var items = new List<string>();
        //    string heading = "";
        //    for (int i = 0; i < ColumnHeading.Count; i++)
        //    {
        //        heading += ColumnHeading[i] + " ";
        //    }
        //    items.Add(heading);

        //    for (int i = 0; i < Rows.Count; i++)
        //    {
        //        items.Add(Rows[i].ToString());
        //    }
        //    return items;
        //}

        public class clsSNPMapDataRow
        {
            public clsSNPMapDataRow() { }
            public clsSNPMapDataRow(string[] Items)
            {
                Index = int.Parse(Items[0]);
                SNP_Name = Items[1];
                Chromosome = Items[2];
                Position = int.Parse(Items[3]);
                Gentrain_Score = decimal.Parse(Items[4]);
                SNP = Items[5];
                ILMN_Strand = Items[6];
                Customer_Strand = Items[7];
                NormID = int.Parse(Items[8]);
            }

            public int Index { get; set; }
            public string SNP_Name { get; set; }
            public string Chromosome { get; set; }
            public int Position { get; set; }
            public decimal Gentrain_Score { get; set; }
            public string SNP { get; set; }
            public string ILMN_Strand { get; set; }
            public string Customer_Strand { get; set; }
            public int NormID { get; set; }

            // Serialize the row as JSON as the default string representation
            public override string ToString()
            {
                string json = JsonConvert.SerializeObject(this, Formatting.Indented);
                return json;
            }
        }
        //    Index Name    Chromosome Position    GenTrain Score  SNP ILMN Strand Customer Strand NormID
        //1	10080_COIII	0	0	0.7349	[T/C]    BOT TOP 0
    }
}