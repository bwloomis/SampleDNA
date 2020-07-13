using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace DataHub.DataHubAgent
{
    public class clsFinalReportSNP
    {
        public string SNPName { get; set; }
        public string SampleID { get; set; }
        public char Allele1_Forward { get; set; }
        public char Allele2_Forward { get; set; }
        public char Allele1_Top { get; set; }
        public char Allele2_Top { get; set; }
        public char Allele1_AB { get; set; }
        public char Allele2_AB { get; set; }

        public decimal? GC_Score { get; set; }
        public decimal? X { get; set; }
        public decimal? Y { get; set; }

        public clsFinalReportSNP(string[] items) {
            Load(items);
        }
        public void Load(string[] DataRow)
        {
            this.SNPName = DataRow[0];
            this.SampleID = DataRow[1];
            this.Allele1_Forward = DataRow[2][0];
            this.Allele2_Forward = DataRow[3][0];
            this.Allele1_Top = DataRow[4][0];
            this.Allele2_Top = DataRow[5][0];
            this.Allele1_AB = DataRow[6][0];
            this.Allele2_AB = DataRow[7][0];
            if (DataRow[8] == "NaN") this.GC_Score = null; else this.GC_Score = Convert.ToDecimal(DataRow[8]);
            if (DataRow[9] == "NaN") this.X = null; else this.X = Convert.ToDecimal(DataRow[9]);
            if (DataRow[10] == "NaN") this.Y = null; else this.Y = Convert.ToDecimal(DataRow[10]);
        }

        public override string ToString()
        {
            string json = JsonConvert.SerializeObject(this, Formatting.Indented);
            return json;
        }

        //public string GetCMD(int indent)
        //{
        //    string result = "{ ".PadLeft(indent) +
        //         "SNP_Name : '".PadLeft(indent) + SNPName.ToString() + "', " +
        //        "SampleID : '".PadLeft(indent) + SampleID.ToString() + "', " +
        //        "Allele1_Forward : '".PadLeft(indent) + Allele1_Forward.ToString() + "', " +
        //        "Allele2_Forward : '".PadLeft(indent) + Allele2_Forward.ToString() + "', " +
        //        "Allele1_Top : '".PadLeft(indent) + Allele1_Top.ToString() + "', " +
        //        "Allele2_Top : '".PadLeft(indent) + Allele2_Top.ToString() + "', " +
        //        "Allele1_AB : '".PadLeft(indent) + Allele1_AB.ToString() + "', " +
        //        "Allele2_AB : '".PadLeft(indent) + Allele2_AB.ToString() + "', " +
        //        "GC_Score : ".PadLeft(indent) + GC_Score.ToString() + ", " +
        //        "X : ".PadLeft(indent) + X.ToString() + ", " +
        //        "Y : ".PadLeft(indent) + Y.ToString() +
        //        " }".PadLeft(indent);
        //    return result;
        //}
    }

}

