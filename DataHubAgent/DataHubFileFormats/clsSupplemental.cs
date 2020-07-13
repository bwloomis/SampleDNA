using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace DataHub.DataHubAgent
{
    public class clsSupplemental
    {
        private string Filename { get; set; }
        public List<clsSupplementalDataRow> Rows { get; set; }
        private List<string> RawRows { get; set; }  // used for test BSON serialize / deserialize
        private clsMongoDB_Helper mongoDB_Helper = new clsMongoDB_Helper();
        public clsSupplemental() { }
        public clsSupplemental(string Filename, bool RetainText)
        {
            this.Filename = Filename;
            var lines = mongoDB_Helper.GetFileLines(Filename);
            LoadFile(lines, RetainText);
        }
        public clsSupplemental LoadFile(string[] lines, bool RetainText)
        {
            if (RetainText)
            {
                // Save a copy of the raw text for later testing BSON serialization accuracy
                if (RawRows == null)
                    RawRows = new List<string>(lines.Count());
                RawRows.AddRange(lines);
            }

            this.Rows = new List<clsSupplementalDataRow>(1000);
            for (int i = 1; i < lines.Count(); i++)
            {
                var dataLine = new clsSupplementalDataRow(lines[i]);
                this.Rows.Add(dataLine);
            }
            return this;
        }
        public void WriteBSON(string Filename)
        {
            mongoDB_Helper.WriteBSON(Filename, this);
        }
        public clsSupplemental LoadBSON(string BSON_Filename)
        {
            if (!File.Exists(BSON_Filename))
                throw new FileNotFoundException(BSON_Filename);

            JObject BSON = JObject.Parse(File.ReadAllText(BSON_Filename));

            var serializer = new JsonSerializer();
            clsSupplemental result =
                (clsSupplemental)serializer.Deserialize(
                    new JTokenReader(BSON), typeof(clsSupplemental));

            return result;
        }
        public void WriteLIMS_File(string LIMS_Filename)
        {
            if (File.Exists(LIMS_Filename))
                File.Delete(LIMS_Filename);

            var lines = new List<string>(Rows.Count + 1);

            var columnHeadingLine = string.Join("\t",this.ColumnHeadings());
            lines.Add(columnHeadingLine);

            for (int i = 0; i < Rows.Count; i++)
            {
                var dataLine = string.Join("\t", Rows[i]);
                lines.Add(dataLine);
            }
            File.WriteAllLines(LIMS_Filename,lines);
        }
        public List<string> ColumnHeadings()
        {
            var result = this.Rows[0].Headings();
            return result;
        }
        public override string ToString()
        {
            string json = JsonConvert.SerializeObject(this, Formatting.Indented);
            return json;
        }
    }
}

//Study Index   Sample ID   BarCode AnimalID    RegistrationNumber Call Rate Other_Name  Sire Dam Comment Sample_Source   Sample Plate    Well on DNA plate   AMP_Plate Well in AMP Plate   PC Error Rate PPC Error Rate  Array Info.Sentrix ID/Position Prob of ZChr_Heterozygosity Sex Estimate    Sex on Submission Comparison
//2884539	2884539_2884539 219AEC4416  1132351114	219AEC4416 SH_12_MG219_H4_RUN_FC	0.9977					Blood Tube	98706	A1  22380	A1          201031700012_R01C01 0.3507	M NA  Incorrect
