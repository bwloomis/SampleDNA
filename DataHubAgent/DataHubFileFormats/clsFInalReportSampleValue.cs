using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataHub.DataHubAgent
{
    public class clsFinalReportSampleValue
    {
        public string SNPName { get; set; }
        public char Allele1_Forward { get; set; }
        public char Allele2_Forward { get; set; }
        public char Allele1_Top { get; set; }
        public char Allele2_Top { get; set; }
        public char Allele1_AB { get; set; }
        public char Allele2_AB { get; set; }
        public decimal GC_Score { get; set; }
        public decimal X { get; set; }
        public decimal Y { get; set; }
        public string SNPValue { get; set; }

        public override string ToString()
        {
            string result = "JSON string";
            return result;
        }

        public string ToStringVerbose()
        {
            string result = 
                "SNPName:" + SNPName + " " +
                "Allele1_Forward:" + Allele1_Forward + " " +
                "Allele2_Forward:" + Allele2_Forward + " " +
                "Allele1_Top:" + Allele1_Top + " " +
                "Allele2_Top:" + Allele2_Top + " " +
                "Allele1_AB:" + Allele1_AB + " " +
                "Allele2_AB:" + Allele2_AB + " " +
                "GC_Score:" + GC_Score + " " +
                "X:" + X + " " +
                "Y:" + Y;
            return result;
        }
    }

}
