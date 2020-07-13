using Newtonsoft.Json;

namespace DataHub.DataHubAgent
{
    public class clsFinalReportMatrixAB_SNP
    {
        public string SNPName { get; set; }

        public string Value { get; set; }

        public clsFinalReportMatrixAB_SNP() { }
        public clsFinalReportMatrixAB_SNP(string SNPName, string Value)
        {
            this.SNPName = SNPName;
            this.Value = Value;
        }
        public clsFinalReportMatrixAB_SNP(string Line)
        {
            string[] fields = Line.Split("\t".ToCharArray());
            this.SNPName = fields[0];
            this.Value = fields[1];

        }
        public override string ToString()
        {
            string json = JsonConvert.SerializeObject(this, Formatting.Indented);
            return json;
        }

    }
}

