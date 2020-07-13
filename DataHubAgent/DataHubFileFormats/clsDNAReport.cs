using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace DataHub.DataHubAgent
{
    public class clsDNAReport
    {
        public string DNAReportOn { get; set; }
        public string DNAReportIn { get; set; }
        public int NumLOCI { get; set; }
        public int NumDNAs { get; set; }
        public string ProjectName { get; set; }
        public string GenCallVersion { get; set; }
        public decimal LowGenCallScoreCutoff { get; set; }
        public string Filename { get; set; }
        public List<string> ColumnHeading { get; set; }
        public List<string> BSON_Heading { get; set; }
        public List<clsDNAReportDataRow> Rows { get; set; }

        public clsDNAReport()
        {
            ColumnHeading = new List<string>(12);
            BSON_Heading = new List<string>(12);
            Rows = new List<clsDNAReportDataRow>(50000);
        }

        public void WriteBSON(string Filename)
        {
            if (File.Exists(Filename)) File.Delete(Filename);
            string BSON = this.ToString();
            File.WriteAllText(Filename,BSON);
        }

        public override string ToString()
        {
            string json = JsonConvert.SerializeObject(this, Formatting.Indented);
            return json;
        }

        //public string GetBSON()
        //{
        //    var result = new StringBuilder(5000000);

        //    result.Append("{ \r" +
        //        "'DNAReportOn' : '" + DNAReportOn + "', \r" +
        //        "'DNAReportIn' : '" + DNAReportIn + "', \r" +
        //        "'NumLOCI' : " + NumLOCI.ToString() + ", \r" +
        //        "'NumDNAs' : " + NumDNAs.ToString() + ", \r" +
        //        "'ProjectName' : '" + ProjectName + "', \r" +
        //        "'GenCallVersion' : '" + GenCallVersion + "', \r" +
        //        "'LowGenCallScoreCutoff' : " + LowGenCallScoreCutoff.ToString("0.0000") + ", \r" +
        //        "'Filename' : '" + Filename + "'");
        //    if (Rows.Count > 0) {
        //        result.Append(", \rDNA : [ \r");
        //        for (int i = 0; i < Rows.Count - 1; i++)
        //        {
        //            result.Append(Rows[i].GetBSON(BSON_Heading) +", \r");
        //        }
        //        result.Append(Rows[Rows.Count - 2].GetBSON(BSON_Heading) + "]\r}\r");
        //    }
        //    return result.ToString();
        //}

        // Overload to allow a Filename parameter to load the file
        public void LoadFile(string Filename)
        {
            if (File.Exists(Filename) == false)
                throw new FileNotFoundException(Filename);

            string[] lines = File.ReadAllLines(Filename);
            var result = new clsDNAReport();
            result.LoadFile(lines);
        }
        public void LoadFile(string[] lines) {
            
            // Process first line
            var firstLine = ParseFirstLine(lines[0]);
            this.DNAReportOn = firstLine[0];
            this.DNAReportIn = firstLine[1];

            // Process second line
            var secondLine = ParseSecondLine(lines[1]);
            this.NumLOCI = int.Parse(secondLine[0]);
            this.NumDNAs = int.Parse(secondLine[1]);
            this.ProjectName = secondLine[2];
            this.GenCallVersion = secondLine[3];
            this.LowGenCallScoreCutoff = decimal.Parse(secondLine[4]);

            // Process third line
            ColumnHeading.Clear();
            var items = lines[2].Split(",".ToCharArray());
            this.ColumnHeading.AddRange(items);

            BSON_Heading.Clear();
            for (int i = 0; i < ColumnHeading.Count; i++)
            {
                BSON_Heading.Add(ColumnHeading[i]
                    .Replace("#", "Num")
                    .Replace(" ", "_")
                    .Replace("/", "_")
                    .Replace("10%", "TenPct")
                    .Replace("50%", "FiftyPct")
                    .Replace("0_1", "ZeroOrOne")                    );
            }

            // Process data lines
            this.Rows = new List<clsDNAReportDataRow>();
            for (int i = 3; i < lines.Count(); i++)
            {
                var newRow = new clsDNAReportDataRow();
                newRow.Load(lines[i]);
                this.Rows.Add(newRow);
            }

            return;
        }
        private string[] ParseFirstLine(string FirstLine)
        {
            var result = new string[2];
            int workIN = FirstLine.IndexOf(" in ");
            result[1] = FirstLine.Substring(workIN+4);
            result[0] = FirstLine.Substring("DNA Report on ".Length, FirstLine.Length - result[1].Length - "DNA Report on ".Length-4);
            return result;
        }
        
        private string[] ParseSecondLine(string SecondLine)
        {
            var result = new string[5];
            var items = SecondLine.Split(",".ToCharArray());
            result[0] = items[1].Split("=".ToCharArray())[1].Trim(); // NumLOCI
            result[1] = items[2].Split("=".ToCharArray())[1].Trim(); // NumDNAs
            result[2] = items[3].Split("=".ToCharArray())[1].Trim(); // ProjectName
            result[3] = items[4].Split("=".ToCharArray())[1].Trim(); // GenCall Version
            result[4] = items[5].Split("=".ToCharArray())[1].Trim(); // Low GenCall Score Cutoff
            return result;
        }

        //public List<string> HeaderForList()
        //{
        //    var items = new List<string>();
        //    items.Add("DNA Report On = " + DNAReportOn);
        //    items.Add("DNA Report In = " + DNAReportIn);
        //    items.Add("# LOCI = " + NumLOCI.ToString());
        //    items.Add("# DNAs = " + NumDNAs.ToString());
        //    items.Add("Project Name = " + ProjectName);
        //    items.Add("Gene Call Version = " + GenCallVersion);
        //    items.Add("Low GenCall Score Cutoff = " + LowGenCallScoreCutoff);
        //    items.Add("Filename = " + Filename);
        //    return items;
        //}
        //public List<string> DetailForList()
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
    }
    

    //DNA Report on Q:\Illumina Project Data 2016\_Cobb_Vantress Projects in Progress\Cobb_Vantress_244637_HGC60KV03_20161122\Cobb_Vantress_244637_HGC60KV03_20161122\Cobb_Vantress_244637_HGC60KV03_20161122_DNAReport.csv
    //,# LOCI = 62732,# DNAs = 78,ProjectName = Cobb_Vantress_244637_HGC60KV03_20161122_DNAReport,GenCall Version = 6.3.0,Low GenCall Score Cutoff = 0.0500
    //Row,DNA_ID,#No_Calls,#Calls,Call_Rate,A/A_Freq,A/B_Freq,B/B_Freq,Minor_Freq,50%_GC_Score,10%_GC_Score,0/1
    //1,219AEC4083,1770,60962,0.9718,0.3539,0.2669,0.3792,0.4874,0.8639,0.6921,1

    public class clsDNAReportDataRow {
        public int RowID { get; set; }
        public string DNA_ID { get; set; }
        public int Num_NoCalls { get; set; }
        public int Num_Calls { get; set; }
        public decimal Call_Rate { get; set; }
        public decimal AA_Freq { get; set; }
        public decimal AB_Freq { get; set; }
        public decimal BB_Freq { get; set; }
        public decimal Minor_Freq {get;set;}
        public decimal Pct50_GC_Score { get; set; }
        public decimal Pct10_GC_Score { get; set; }
        public int ZeroOrOne { get; set; }

        public clsDNAReportDataRow() { }

        public clsDNAReportDataRow Load(string DataRow)
        {
            string[] items = DataRow.Split(",".ToCharArray());
            RowID = int.Parse(items[0]);
            DNA_ID = items[1];
            Num_NoCalls = int.Parse(items[2]);
            Num_Calls = int.Parse(items[3]);
            Call_Rate = decimal.Parse(items[4]);
            AA_Freq = decimal.Parse(items[5]);
            AB_Freq = decimal.Parse(items[6]);
            BB_Freq = decimal.Parse(items[7]);
            Minor_Freq = decimal.Parse(items[8]);
            Pct50_GC_Score = decimal.Parse(items[9]);
            Pct10_GC_Score = decimal.Parse(items[10]);
            ZeroOrOne = int.Parse(items[11]);
            return this;
        }

        public override string ToString()
        {
            string result =
                RowID.ToString() + " " + 
                DNA_ID.ToString() + " " +
                Num_NoCalls.ToString() + " " +
                Num_Calls.ToString() + " " +
                Call_Rate.ToString() + " " +
                AA_Freq.ToString() + " " +
                AB_Freq.ToString() + " " +
                BB_Freq.ToString() + " " +
                Minor_Freq.ToString() + " " +
                Pct50_GC_Score.ToString() + " " +
                Pct10_GC_Score.ToString() + " " +
                ZeroOrOne.ToString();
            return result;
        }

        //public string GetBSON(List<string> Headings)
        //{
        //    if (Headings.Count == 0)
        //        throw new InvalidDataException("Column Heading List Has No Values");

        //    string result = "{ " +
        //        "'" + Headings[0] + "' : " + RowID.ToString() + ",\r" +
        //        "'" + Headings[1] + "' : '" + DNA_ID.ToString() + "',\r" +
        //        "'" + Headings[2] + "' : " + Num_NoCalls.ToString() + ",\r" +
        //        "'" + Headings[3] + "' : " + Num_Calls.ToString() + ",\r " +
        //        "'" + Headings[4] + "' : " + Num_Calls.ToString() + ",\r" +
        //        "'" + Headings[5] + "' : " + AA_Freq.ToString("0.0000") + ",\r" +
        //        "'" + Headings[6] + "' : " + AB_Freq.ToString("0.0000") + ",\r" +
        //        "'" + Headings[7] + "' : " + BB_Freq.ToString("0.0000") + ",\r" +
        //        "'" + Headings[8] + "' : " + Minor_Freq.ToString("0.0000") + ",\r" +
        //        "'" + Headings[9] + "' : " + Pct50_GC_Score.ToString("0.0000") + ",\r" +
        //        "'" + Headings[10] + "' : " + Pct10_GC_Score.ToString("0.0000") + ",\r" +
        //        "'" + Headings[11] + "' : " + ZeroOrOne.ToString() + " }\r";
        //    return result;
        //}
    }
}

