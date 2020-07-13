using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace DataHub.DataHubAgent
{
    public class clsLocusSummary
    {
        public string LocusReportOn { get; set; }
        public string LocusReportIn { get; set; }
        public int NumLOCI { get; set; }
        public int NumDNAs { get; set; }
        public string ProjectName { get; set; }
        public string GenCallVersion { get; set; }
        public decimal LowGenCallScoreCutoff { get; set; }
        public List<string> ColumnHeading { get; set; }

        public List<clsLocusDataRow> Rows { get; set; }


        private string Filename { get; set; }
        private List<string> RawRows { get; set; }  // used for test BSON serialize / deserialize


        public clsLocusSummary(string Filename, bool RetainText)
        {
            this.Filename = Filename;
            if (!File.Exists(Filename))
                throw new FileNotFoundException(Filename);
            var lines = File.ReadAllLines(Filename);
            LoadFile(lines, RetainText);
        }

        public void WriteBSON(string Filename)
        {
            if (File.Exists(Filename)) File.Delete(Filename);
            string BSON = this.ToString();
            File.WriteAllText(Filename, BSON);
        }

        public clsLocusSummary LoadFile(string[] lines, bool RetainText)
        {
            if (RetainText)
            {
                // Save a copy of the raw text for later testing BSON serialization accuracy
                if (RawRows == null)
                    RawRows = new List<string>(lines.Count());
                RawRows.AddRange(lines);
            }

            // Process first line
            var firstLine = ParseFirstLine(lines[0]);
            this.LocusReportOn = firstLine[0];
            this.LocusReportIn = firstLine[1];

            // Process second line
            var secondLine = ParseSecondLine(lines[1]);
            this.NumLOCI = int.Parse(secondLine[0]);
            this.NumDNAs = int.Parse(secondLine[1]);
            this.ProjectName = secondLine[2];
            this.GenCallVersion = secondLine[3];
            this.LowGenCallScoreCutoff = decimal.Parse(secondLine[4]);

            // Process third line
            this.ColumnHeading = new List<string>();
            var items = lines[2].Split(",".ToCharArray());
            this.ColumnHeading.AddRange(items);

            // Process data lines
            this.Rows = new List<clsLocusDataRow>();
            for (int i = 3; i < lines.Count(); i++)
            {
                if (lines[i].Trim().Length > 0)
                {
                    var newRow = new clsLocusDataRow();
                    newRow.Load(lines[i]);
                    this.Rows.Add(newRow);
                }
            }
            return this;
        }

        private string[] ParseFirstLine(string FirstLine)
        {
            var result = new string[2];
            int workIN = FirstLine.IndexOf(" in ");
            result[1] = FirstLine.Substring(workIN + 4);
            result[0] = FirstLine.Substring("Locus Summary on ".Length, FirstLine.Length - result[1].Length - "Locus Summary on ".Length - 4);
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


        public class clsLocusDataRow
        {
            public int RowID { get; set; }
            public string Locus_Name { get; set; }
            public string Illumicode_Name { get; set; }
            public int Num_NoCalls { get; set; }
            public int Num_Calls { get; set; }
            public decimal Call_Freq { get; set; }
            public decimal AA_Freq { get; set; }
            public decimal AB_Freq { get; set; }
            public decimal BB_Freq { get; set; }
            public decimal Minor_Freq { get; set; }
            public decimal Gentrain_Score { get; set; }
            public decimal Pct50_GC_Score { get; set; }
            public decimal Pct10_GC_Score { get; set; }
            public decimal Het_Excess_Freq { get; set; }
            public decimal ChiTest_P100 { get; set; }
            public decimal Cluster_Sep { get; set; }
            public decimal AA_T_Mean { get; set; }
            public decimal AA_T_Std { get; set; }
            public decimal AB_T_Mean { get; set; }
            public decimal AB_T_Std { get; set; }
            public decimal BB_T_Mean { get; set; }
            public decimal BB_T_Std { get; set; }
            public decimal AA_R_Mean { get; set; }
            public decimal AA_R_Std { get; set; }
            public decimal AB_R_Mean { get; set; }
            public decimal AB_R_Std { get; set; }
            public decimal BB_R_Mean { get; set; }
            public decimal BB_R_Std { get; set; }
            public string PlusMinusStrand { get; set; }


            private string Filename { get; set; }
            private List<string> RawRows { get; set; }  // used for test BSON serialize / deserialize

            public void WriteBSON(string Filename)
            {
                if (File.Exists(Filename)) File.Delete(Filename);
                string BSON = this.ToString();
                File.WriteAllText(Filename, BSON);
            }


            public clsLocusDataRow Load(string DataRow)
            {

                if (DataRow == "") return null;

                string[] items = DataRow.Split(",".ToCharArray());
                int index = 0;
                RowID = int.Parse(items[index]); index += 1;
                Locus_Name = items[index]; index += 1;
                Illumicode_Name = items[index]; index += 1;
                Num_NoCalls = int.Parse(items[index]); index += 1;
                Num_Calls = int.Parse(items[index]); index += 1;
                Call_Freq = decimal.Parse(items[index]); index += 1;
                AA_Freq = decimal.Parse(items[index]); index += 1;
                AB_Freq = decimal.Parse(items[index]); index += 1;
                BB_Freq = decimal.Parse(items[index]); index += 1;
                Minor_Freq = decimal.Parse(items[index]); index += 1;
                Gentrain_Score = decimal.Parse(items[index]); index += 1;
                Pct50_GC_Score = decimal.Parse(items[index]); index += 1;
                Pct10_GC_Score = decimal.Parse(items[index]); index += 1;
                Het_Excess_Freq = decimal.Parse(items[index]); index += 1;
                ChiTest_P100 = decimal.Parse(items[index]); index += 1;
                Cluster_Sep = decimal.Parse(items[index]); index += 1;
                AA_T_Mean = decimal.Parse(items[index]); index += 1;
                AA_T_Std = decimal.Parse(items[index]); index += 1;
                AB_T_Mean = decimal.Parse(items[index]); index += 1;
                AB_T_Std = decimal.Parse(items[index]); index += 1;
                BB_T_Mean = decimal.Parse(items[index]); index += 1;
                BB_T_Std = decimal.Parse(items[index]); index += 1;
                AA_R_Mean = decimal.Parse(items[index]); index += 1;
                AA_R_Std = decimal.Parse(items[index]); index += 1;
                AB_R_Mean = decimal.Parse(items[index]); index += 1;
                AB_R_Std = decimal.Parse(items[index]); index += 1;
                BB_R_Mean = decimal.Parse(items[index]); index += 1;
                BB_R_Std = decimal.Parse(items[index]); index += 1;
                PlusMinusStrand = items[index];
                return this;
            }
            public override string ToString()
            {
                string json = JsonConvert.SerializeObject(this, Formatting.Indented);
                return json;
            }
            public string ToStringVerbose()
            {
                string result =
                    RowID.ToString() + " " +
                    Locus_Name.ToString() + " " +
                    Illumicode_Name.ToString() + " " +
                    Num_NoCalls.ToString() + " " +
                    Num_Calls.ToString() + " " +
                    Call_Freq.ToString() + " " +
                    AA_Freq.ToString() + " " +
                    AB_Freq.ToString() + " " +
                    BB_Freq.ToString() + " " +
                    Minor_Freq.ToString() + " " +
                    Gentrain_Score.ToString() + " " +
                    Pct50_GC_Score.ToString() + " " +
                    Pct10_GC_Score.ToString() + " " +
                    Het_Excess_Freq.ToString() + " " +
                    ChiTest_P100.ToString() + " " +
                    Cluster_Sep.ToString() + " " +
                    AA_T_Mean.ToString() + " " +
                    AA_T_Std.ToString() + " " +
                    AB_T_Mean.ToString() + " " +
                    AB_T_Std.ToString() + " " +
                    BB_T_Mean.ToString() + " " +
                    BB_T_Std.ToString() + " " +
                    AA_R_Mean.ToString() + " " +
                    AA_R_Std.ToString() + " " +
                    AB_R_Mean.ToString() + " " +
                    AB_R_Std.ToString() + " " +
                    BB_R_Mean.ToString() + " " +
                    BB_R_Std.ToString() + " " +
                    PlusMinusStrand.ToString();
                return result;
            }
        }
        public override string ToString()
        {
            string json = JsonConvert.SerializeObject(this, Formatting.Indented);
            return json;
        }
    }
    //    Locus Summary on Q:\Illumina Project Data 2016\_Cobb_Vantress Projects in Progress\Cobb_Vantress_244638_HGC60KV03_20161122\Cobb_Vantress_244638_HGC60KV03_20161122\Cobb_Vantress_244638_HGC60KV03_20161122_LocusSummary.csv
    //,# LOCI = 62732,# DNAs = 639,ProjectName = Cobb_Vantress_244638_HGC60KV03_20161122_LocusSummary,GenCall Version = 6.3.0,Low GenCall Score Cutoff = 0.0500
    //Row,Locus_Name,Illumicode_Name,#No_Calls,#Calls,Call_Freq,A/A_Freq,A/B_Freq,B/B_Freq,Minor_Freq,Gentrain_Score,50%_GC_Score,10%_GC_Score,Het_Excess_Freq,ChiTest_P100,Cluster_Sep,AA_T_Mean,AA_T_Std,AB_T_Mean,AB_T_Std,BB_T_Mean,BB_T_Std,AA_R_Mean,AA_R_Std,AB_R_Mean,AB_R_Std,BB_R_Mean,BB_R_Std,Plus/Minus Strand
    //1,10080_COIII,27645310,2,637,0.997,0.179,0.000,0.821,0.179,0.7349,0.7048,0.7048,-0.9999,0.0000,0.7568,0.016,0.013,0.285,0.022,0.990,0.009,1.624,0.147,0.834,0.100,1.263,0.100,TOP

}
