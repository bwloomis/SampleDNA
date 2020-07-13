using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DataHub.DataHubAgent
{
    public class clsSupplementalDataRow
    {
        public string Study { get; set; }
        public string Index { get; set; }
        public string SampleID { get; set; }
        public string BarCode { get; set; }
        public string AnimalID { get; set; }
        public string RegistrationNumber { get; set; }
        public string CallRate { get; set; }
        public string Other_Name { get; set; }
        public string Sire { get; set; }
        public string Dam { get; set; }
        public string Comment { get; set; }
        public string Sample_Source { get; set; }
        public string SamplePlate { get; set; }
        public string WellOnDNAPlate { get; set; }
        public string AMP_Plate { get; set; }
        public string WellInAMPPlate { get; set; }
        public string PCErrorRate { get; set; }
        public string PPCErrorRate { get; set; }
        public string ArrayInfoSentrixIDPosition { get; set; }
        public string ProbOfZChr_Heterozygosity { get; set; }
        public string SexEstimate { get; set; }
        public string SexOnSubmission { get; set; }
        public string Comparison { get; set; }

        private clsMongoDB_Helper mongoHelper = new clsMongoDB_Helper();

        public clsSupplementalDataRow() { }

        public clsSupplementalDataRow(string DataLine)
        {
            string[] fields = DataLine.Split("\t".ToCharArray());
            this.Study = fields[0];
            this.Index = fields[1];
            this.SampleID = fields[2];
            this.BarCode = fields[3];
            this.AnimalID = fields[4];
            this.RegistrationNumber = fields[5];
            this.CallRate = fields[6];
            this.Other_Name = fields[7];
            this.Sire = fields[8];
            this.Dam = fields[9];
            this.Comment = fields[10];
            this.Sample_Source = fields[11];
            this.SamplePlate = fields[12];
            this.WellOnDNAPlate = fields[13];
            this.AMP_Plate = fields[14];
            this.WellInAMPPlate = fields[15];
            this.PCErrorRate = fields[16];
            this.PPCErrorRate = fields[17];
            this.ArrayInfoSentrixIDPosition = fields[18];
            this.ProbOfZChr_Heterozygosity = fields[19];
            this.SexEstimate = fields[20];
            this.SexOnSubmission = fields[21];
            this.Comparison = fields[22];
        }

        public List<string> Headings()
        {
            var result = new List<string>(22);
            var headings = @"Study;Index;Sample ID;BarCode;AnimalID;" +
                "RegistrationNumber;Call Rate;Other_Name;Sire;Dam;" +
                "Comment;Sample_Source;Sample Plate;Well on DNA plate;" +
                "AMP_Plate;Well in AMP Plate;PC Error Rate; PPC Error Rate;" +
                "Array Info.Sentrix ID/ Position;Prob of ZChr_Heterozygosity" +
                "Sex Estimate;Sex Estimate;Sex on Submission;Comparison";
            result.AddRange(headings.Split(";".ToCharArray()));
            return result;
        }

        public override string ToString()
        {
            string json = JsonConvert.SerializeObject(this, Formatting.Indented);
            return json;
        }

        public JObject ToBSON()
        {
            var result = JObject.Parse(this.ToString());
            return result;
        }

        public List<string> PropertyNames()
        {
            var result = mongoHelper.PropertyNames(this);
            return result;
        }
        public List<string> PropertyValues()
        {
            var result = mongoHelper.PropertyValues(this);
            return result;
        }
    }
}

//Study    Index            Sample ID   BarCode     AnimalID    RegistrationNumber     Call Rate  Other_Name  Sire Dam Comment Sample_Source  Sample Plate  Well on DNA plate   AMP_Plate Well in AMP Plate   PC Error Rate PPC Error Rate  Array Info.Sentrix   ID/Position Prob of ZChr_Heterozygosity Sex Estimate    Sex on Submission Comparison
//2884539  2884539_2884539  219AEC4416  1132351114  219AEC4416  SH_12_MG219_H4_RUN_FC  0.9977;                               Blood Tube     98706;        A1                  22380;A1                                                  201031700012_R01C01  0.3507;M NA  Incorrect
