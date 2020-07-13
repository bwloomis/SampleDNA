using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataHub.DataHubAgent
{
    public class clsBirdSample
    {
        // Public fields
        public long RecordID { get; set; } //long
        public string SampleID { get; set; } // char(10)
        public string RegID { get; set; } // char(24)
        public string BirdID { get; set; } // char(2)
        public string ChickenID { get; set; } // ? estimated char(20)
        public string DamChickenID { get; set; } // ? estimated char(20)
        public string DamBlupID { get; set; } // ? estimated char(20)
        public string SireChickenID { get; set; } // ? estimated char(20)
        public string SireBlupID { get; set; } // ? estimated char(20)
        public char Gender { get; set; } // char(1)
        public char CalculatedGender { get; set; } // char(1)
        public char ReportedGender { get; set; } // char(1)
        public double CallRate { get; set; }
        public string Farm { get; set; } // char(2)
        public string Line { get; set; } // char(3)
        public int MatingGroup { get; set; } // char(5)
        public string PlateVersion { get; set; } // char(6)
        public string PlateWell { get; set; } // char(3)
        public string PlatePosition { get; set; } // char(6)
        public char Usable { get; set; } // char(1)
        public char Primary { get; set; } // char(1)
        public string SampleType { get; set; } // char(16), example: 'blood tube'
        public string GSGTVersion { get; set; }
        public DateTime ProcessingDate { get; set; }
        public string Content { get; set; }
        public int NumSNPs { get; set; }
        public int TotalSNPs { get; set; }
        public int NumSamples { get; set; }
        public int TotalSamples { get; set; }
        public string Filename { get; set; }   // varchar(255)



        // Public field arrays
        public List<string> SNPName { get; set; }  // char(12)
        public List<char> Allele1_Forward { get; set; } // char(1)
        public List<char> Allele2_Forward { get; set; } // char(1)
        public List<char> Allele1_Top { get; set; } // char(1)
        public List<char> Allele2_Top { get; set; } // char(1)
        public List<char> Allele1_AB { get; set; } // char(1)
        public List<char> Allele2_AB { get; set; } // char(1)
        public List<decimal?> GC_Score { get; set; } // char(1)
        public List<decimal?> X { get; set; } // char(1)
        public List<decimal?> Y { get; set; } // char(1)

        // Reference to other documents in the LMS output if needed
        public clsDNAReport DNAReport { get; set; }
        public clsFinalReport FinalReport { get; set; }
        public clsFinalReportMatrixAB FinalReportMatrixAB { get; set; }
        public clsSampleMap SampleMap { get; set; }
        public clsSNPMap SNPMap { get; set; }
        public clsSupplemental Supplemental { get; set; }

        public clsBirdSample()
        {
            int maxSize = 63000;
            SNPName = new List<string>(maxSize);
            Allele1_Forward = new List<char>(maxSize);
            Allele2_Forward = new List<char>(maxSize);
            Allele1_Top = new List<char>(maxSize);
            Allele2_Top = new List<char>(maxSize);
            Allele1_AB = new List<char>(maxSize);
            Allele2_AB = new List<char>(maxSize);
            GC_Score = new List<decimal?>(maxSize);
            X = new List<decimal?>(maxSize);
            Y = new List<decimal?>(maxSize);
        }

        public void LoadFile(clsFinalReport FinalReport, string SampleID)
        {
            // Mock values we don't have yet
            Random random = new Random();
            this.RecordID = RandomExtensionMethods.NextLong(random);
            this.SampleID = SampleID;
            this.RegID = RandomExtensionMethods.RandomString("", 24, 24, random);
            this.BirdID = RandomExtensionMethods.RandomString("", 2, 2, random);
            this.ChickenID = RandomExtensionMethods.RandomString("", 20, 20, random);
            this.DamChickenID = RandomExtensionMethods.RandomString("", 20, 20, random);
            this.DamBlupID = RandomExtensionMethods.RandomString("", 24, 24, random);
            this.SireChickenID = RandomExtensionMethods.RandomString("", 20, 20, random);
            this.SireBlupID = RandomExtensionMethods.RandomString("", 24, 24, random);
            this.Gender = RandomExtensionMethods.RandomString("MF", 1, 1, random)[0];
            this.CalculatedGender = RandomExtensionMethods.RandomString("MF", 1, 1, random)[0];
            this.ReportedGender = RandomExtensionMethods.RandomString("MF", 1, 1, random)[0];
            this.CallRate = RandomExtensionMethods.RandomDoubleFraction(5,random);
            this.Farm = RandomExtensionMethods.RandomString("", 2, 2, random);
            this.Line = RandomExtensionMethods.RandomString("", 3, 3, random);
            this.MatingGroup = RandomExtensionMethods.RandomInt(1, 99999, random);
            this.PlateVersion = RandomExtensionMethods.RandomString("", 6, 6, random);
            this.PlateWell = RandomExtensionMethods.RandomString("", 3, 3, random);
            this.Usable = RandomExtensionMethods.RandomString("YN ", 1, 1, random)[0];
            this.Primary = RandomExtensionMethods.RandomString("YN ", 1, 1, random)[0];
            this.SampleType = RandomExtensionMethods.RandomString("", 16, 16, random);
            this.GSGTVersion = FinalReport.GSGTVersion;
            this.ProcessingDate = FinalReport.ProcessingDate;
            this.Content = FinalReport.Content;
            this.NumSamples = FinalReport.NumSamples;
            this.NumSNPs = FinalReport.NumSNPs;
            this.TotalSamples = FinalReport.NumSamples;
            this.TotalSNPs = FinalReport.NumSNPs;
            this.Filename = FinalReport.Filename;

            //int index = 0; //FinalReport.GetIndexBySampleID(SampleID);

            // Fill genomic arrays for specified sample
            for (int i = 0; i < FinalReport.SNP.Count; i++)
            {
                var snp = FinalReport.SNP[i];
                this.SNPName.Add(snp.SNPName);
                this.Allele1_Forward.Add(snp.Allele1_Forward);
                this.Allele2_Forward.Add(snp.Allele2_Forward);
                this.Allele1_Top.Add(snp.Allele1_Top);
                this.Allele2_Top.Add(snp.Allele2_Top);
                this.Allele1_AB.Add(snp.Allele1_AB);
                this.Allele2_AB.Add(snp.Allele2_AB);
                this.GC_Score.Add(snp.GC_Score);
                this.X.Add(snp.X);
                this.Y.Add(snp.X);
            }
        }
        public string GetCMD()
        {  // Works for manually copy an paste into MongoDB command line
            var result = new StringBuilder(130000);
            result.Append("db.BirdSample.insert({\r");
            result.Append(string.Format("RecordID : '{0}',\r", this.RecordID));
            result.Append(string.Format("SampleID : '{0}',\r", this.SampleID));
            result.Append(string.Format("BirdID : '{0}',\r", this.BirdID));
            result.Append(string.Format("ChickenID : '{0}',\r", this.ChickenID));
            result.Append(string.Format("DamChickenID : '{0}',\r", this.DamChickenID));
            result.Append(string.Format("DamBlupID : '{0}',\r", this.DamBlupID));
            result.Append(string.Format("SireChickenID : '{0}',\r", this.SireChickenID));
            result.Append(string.Format("SireBlupID : '{0}',\r", this.SireBlupID));
            result.Append(string.Format("Gender : '{0}',\r", this.Gender));
            result.Append(string.Format("CalculatedGender : '{0}',\r", this.CalculatedGender));
            result.Append(string.Format("ReportedGender : '{0}',\r", this.ReportedGender));
            result.Append(string.Format("CallRate : '{0}',\r", this.CallRate));
            result.Append(string.Format("Farm : '{0}',\r", this.Farm));
            result.Append(string.Format("Line : '{0}',\r", this.Line));
            result.Append(string.Format("MatingGroup : '{0}',\r", this.MatingGroup));
            result.Append(string.Format("PlateVersion : '{0}',\r", this.PlateVersion));
            result.Append(string.Format("PlateWell : '{0}',\r", this.PlateWell));
            result.Append(string.Format("PlatePosition : '{0}',\r", this.PlatePosition));
            result.Append(string.Format("Usable : '{0}',\r", this.Usable));
            result.Append(string.Format("Primary : '{0}',\r", this.Primary));
            result.Append(string.Format("SampleType : '{0}',\r", this.SampleType));
            result.Append(string.Format("Content : '{0}',\r", this.Content));
            result.Append(string.Format("Filename : '{0}',\r", this.Filename));
            result.Append(string.Format("GSGTVersion : '{0}',\r", this.GSGTVersion));
            result.Append(string.Format("NumSamples : {0},\r", this.NumSamples));
            result.Append(string.Format("NumSNPs : {0},\r", this.NumSNPs));
            result.Append(string.Format("ProcessingDate : ISODate('{0}'),\r",
                this.ProcessingDate.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'")));
            result.Append(string.Format("TotalSamples : {0},\r", this.TotalSamples));
            result.Append(string.Format("TotalSNPs : {0},\r", this.TotalSNPs));
            var maxValue = this.SNPName.Count;
            //maxValue = 3; //test


            result.Append("SNPName : [ \r");
            for (int i = 0; i < maxValue; i++)
            {
                result.Append("    '" + SNPName[i] + "'");
                if (i + 1 < maxValue) result.Append(",\r");
            }
            result.Append("],\r");

            result.Append("Allele1_Forward : [ \r");
            for (int i = 0; i < maxValue; i++)
            {
                result.Append("    '" + Allele1_Forward[i] + "'");
                if (i + 1 < maxValue) result.Append(",\r");
            }
            result.Append("],\r");

            result.Append("Allele2_Forward : [ \r");
            for (int i = 0; i < maxValue; i++)
            {
                result.Append("    '" + Allele2_Forward[i] + "'");
                if (i + 1 < maxValue) result.Append(",\r");
            }
            result.Append("],\r");

            result.Append("Allele1_Top : [ \r");
            for (int i = 0; i < maxValue; i++)
            {
                result.Append("    '" + Allele1_Top[i] + "'");
                if (i + 1 < maxValue) result.Append(",\r");
            }
            result.Append("],\r");

            result.Append("Allele2_Top : [ \r");
            for (int i = 0; i < maxValue; i++)
            {
                result.Append("    '" + Allele2_Top[i] + "'");
                if (i + 1 < maxValue) result.Append(",\r");
            }
            result.Append("],\r");
            result.Append("Allele1_AB : [ \r");
            for (int i = 0; i < maxValue; i++)
            {
                result.Append("    '" + Allele1_AB[i] + "'");
                if (i + 1 < maxValue) result.Append(",\r");
            }
            result.Append("],\r");

            result.Append("Allele2_AB : [ \r");
            for (int i = 0; i < maxValue; i++)
            {
                result.Append("    '" + Allele2_AB[i] + "'");
                if (i + 1 < maxValue) result.Append(",\r");
            }
            result.Append("],\r");

            result.Append("GC_Score : [ \r");
            for (int i = 0; i < maxValue; i++)
            {
                result.Append("    '" + GC_Score[i] + "'");
                if (i + 1 < maxValue) result.Append(",\r");
            }
            result.Append("],\r");

            result.Append("X : [ \r");
            for (int i = 0; i < maxValue; i++)
            {
                result.Append("    '" + X[i] + "'");
                if (i + 1 < maxValue) result.Append(",\r");
            }
            result.Append("],\r");

            result.Append("Y : [ \r");
            for (int i = 0; i < maxValue; i++)
            {
                result.Append("    '" + Y[i] + "'");
                if (i + 1 < maxValue) result.Append(",\r");
            }
            result.Append("]\r");

            result.Append("});\r");
            return result.ToString();
        }

    }

}
