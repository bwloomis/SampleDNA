using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.IO;

namespace DataHub.DataHubAgent
{
    /// <summary>
    /// Read LIMS Final Report Maxtrix AB file. 
    /// Assumed must be split into individual samples files before saved into MongoDB
    /// </summary>
    public class clsFinalReportMatrixAB
    {
        public string GSGTVersion { get; set; }
        public DateTime ProcessingDate { get; set; }
        public string Content { get; set; }
        public int NumSNPs { get; set; }
        public int TotalSNPs { get; set; }
        public int SampleIndex { get; set; }
        public int NumSamples { get; set; }
        public int TotalSamples { get; set; }
        public List<clsFinalReportMatrixAB_SNP> SNP { get; set; }
        private string Filename { get; set; }
        private string SampleName { get; set; }
        private List<string> SampleID { get; set; }
        //private List<clsFinalReportMatrixAB_SNPRow> SNPRow { get; set; }
        private List<string> RawRows { get; set; }  // used for test BSON serialize / deserialize

        public clsFinalReportMatrixAB()
        {
            //this.SampleID = new List<string>(700);
            this.SNP = new List<clsFinalReportMatrixAB_SNP>(63000);
        }
        public clsFinalReportMatrixAB(string Filename, bool RetainText)
        {
            if (!File.Exists(Filename))
                throw new FileNotFoundException(Filename);

            this.Filename = Filename;
            this.SNP = new List<clsFinalReportMatrixAB_SNP>(63000);

            var lines = File.ReadAllLines(Filename);
            LoadFile(lines, RetainText);
        }

        public override string ToString()
        {
            string json = JsonConvert.SerializeObject(this, Formatting.Indented);
            return json;
        }

        public void LoadFile(string Filename, bool RetainText)
        {
            if (!File.Exists(Filename))
                throw new FileNotFoundException(Filename);

            this.Filename = Filename;
            var lines = File.ReadAllLines(Filename);
            LoadFile(lines, RetainText);
        }
        public void WriteBSON(string Filename)
        {
            if (File.Exists(Filename)) File.Delete(Filename);
            string BSON = this.ToString();
            File.WriteAllText(Filename, BSON);
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

            int DataRowIndex = -1;
            bool inDataSection = false;

            for (int i = 0; i <= lines.GetUpperBound(0); i++)
            {
                string fullLine = lines[i];
                if (inDataSection)
                {
                    if (DataRowIndex == -1)
                    {
                        // Assume split file with only one sample
                        string[] fields = lines[i].Split("\t".ToCharArray());
                        SampleName = fields[1];

                        //this.SampleID = new List<string>(700);
                        //SampleID.AddRange(fields);
                    }
                    else
                    {
                        clsFinalReportMatrixAB_SNP row = new clsFinalReportMatrixAB_SNP(fullLine, SampleName);
                        this.SNP.Add(row);
                    }
                    DataRowIndex += 1;
                }
                else
                {
                    string[] fields = lines[i].Split("\t".ToCharArray());
                    if (fields[0].StartsWith("[Header]")) { inDataSection = false; }
                    if (fields[0].StartsWith("GSGT Version")) { this.GSGTVersion = fields[1].Trim(); }
                    if (fields[0].StartsWith("Processing Date")) { this.ProcessingDate = DateTime.Parse(fields[1].Trim()); }
                    if (fields[0].StartsWith("Content")) { this.Content = fields[2].Trim(); }
                    if (fields[0].StartsWith("Num SNPs")) { this.NumSNPs = int.Parse(fields[1].Trim()); }
                    if (fields[0].StartsWith("Total SNPs")) { this.TotalSNPs = int.Parse(fields[1].Trim()); }
                    if (fields[0].StartsWith("Num Samples")) { this.NumSamples = int.Parse(fields[1].Trim()); }
                    if (fields[0].StartsWith("Total Samples")) { this.TotalSamples = int.Parse(fields[1].Trim()); }
                    if (fields[0].StartsWith("[Data]")) { inDataSection = true; }
                }
            }
            return;
        }

        //public List<string> GetLocusList()
        //{
        //    var result = new List<string>(63000);
        //    for (int i = 0; i < SNP.Count(); i++)
        //    {
        //        result.Add(SNP[i].SNPName);
        //    }
        //    return result;
        //}

        //private List<clsFinalReportSampleValue> GetSampleValues(int Index)
        //{
        //    var result = new List<clsFinalReportSampleValue>(SNP.Count);
        //    for (int i = 0; i < SNP.Count; i++)
        //    {
        //        var item = new clsFinalReportSampleValue();
        //        item.SNPName = SNP[i].SNPName;
        //        item.SNPValue = SNP[i].Cell[Index];
        //        result.Add(item);
        //    }
        //    return result;
        //}


        //public clsSample GetSample(int Index)
        //{
        //    var result = new clsSample();
        //    result.Content = this.Content;
        //    result.Filename = this.Filename;
        //    result.GSGTVersion = this.GSGTVersion;
        //    result.NumSamples = this.NumSamples;
        //    result.NumSNPs = this.NumSNPs;
        //    result.ProcessingDate = this.ProcessingDate;
        //    result.TotalSamples = this.TotalSamples;
        //    result.TotalSNPs = this.TotalSNPs;
        //    //result.SampleValue = this.GetSampleValues(Index);
        //    result.SampleID = this.SampleID[Index];
        //    return result;
        //}

        //public int GetSampleIDByIndex(string SampleName)
        //{
        //    int result = -1;
        //    result = SampleID.IndexOf(SampleName);
        //    return result;
        //}

        //public string GetSampleIDByName(int Index)
        //{
        //    string result = "";
        //    result = SampleID[Index];
        //    return result;
        //}

        //public string GetCMD(int Index)
        //{  // Works for manually copy an paste into MongoDB command line
        //    var result = new StringBuilder(130000);
        //    result.Append("{");
        //    result.Append(string.Format("SampleID : '{0}',\r", this.SampleID[Index]));
        //    result.Append(string.Format("Content : '{0}',\r", this.Content));
        //    result.Append(string.Format("Filename : '{0}',\r", this.Filename));
        //    result.Append(string.Format("GSGTVersion : '{0}',\r", this.GSGTVersion));
        //    result.Append(string.Format("NumSamples : {0},\r", this.NumSamples));
        //    result.Append(string.Format("NumSNPs : {0},\r", this.NumSNPs));
        //    result.Append(string.Format("ProcessingDate : ISODate('{0}'),\r",
        //        this.ProcessingDate.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'")));
        //    result.Append(string.Format("TotalSamples : {0},\r", this.TotalSamples));
        //    result.Append(string.Format("TotalSNPs : {0},\r", this.TotalSNPs));
        //    result.Append("values : [\r");
        //    var sample = this.GetSample(Index);
        //    var maxValue = sample.SampleValue.Count;
        //    maxValue = 3; //test
        //    //for (int j = 0; j < maxValue; j++)
        //    //{
        //    //    result.Append(string.Format("    {{Locus : '{0}', Value : '{1}'}}", 
        //    //        sample.SampleValue[j].LocusName,  
        //    //        sample.SampleValue[j].LocusValue.ToString()));
        //    //    if (j + 1 < maxValue) result.Append(",");
        //    //    result.Append("\r");
        //    //}
        //    result.Append("]\r");
        //    result.Append("}\r");
        //    return result.ToString();
        //}



        //public string GetJSON(int Index)
        //{
        //    var result = new StringBuilder(130000);
        //    result.Append("{");
        //    result.Append(string.Format("\"SampleID\" : \"{0}\",\r", this.SampleID[Index]));
        //    result.Append(string.Format("\"Content\" : \"{0}\",\r", this.Content));
        //    result.Append(string.Format("\"Filename\" : \"{0}\",\r", this.Filename));
        //    result.Append(string.Format("\"GSGTVersion\" : \"{0}\",\r", this.GSGTVersion));
        //    result.Append(string.Format("\"NumSamples\" : {0},\r", this.NumSamples));
        //    result.Append(string.Format("\"NumSNPs\" : {0},\r", this.NumSNPs));
        //    result.Append(string.Format("\"ProcessingDate\" : {0},\r",
        //        this.ProcessingDate.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'")));
        //    result.Append(string.Format("\"TotalSamples\" : {0},\r", this.TotalSamples));
        //    result.Append(string.Format("\"TotalSNPs\" : {0},\r", this.TotalSNPs));
        //    result.Append("\"values\" : [\r");
        //    var sample = this.GetSample(Index);
        //    for (int j = 0; j < sample.SampleValue.Count; j++)
        //    {
        //        result.Append(string.Format("    {{\"Locus\" : \"{0}\", \"Value\" : \"{1}\"}}",
        //            sample.SampleValue[j].LocusName,
        //            sample.SampleValue[j].LocusValue.ToString()));
        //        if (j + 1 < sample.SampleValue.Count) result.Append(",");
        //        result.Append("\r");
        //    }
        //    result.Append("]\r");
        //    result.Append("}\r");
        //    return result.ToString();
        //}

        //    public clsLocusCountProfile GetLocusCountProfile()
        //    {
        //        var result = new clsLocusCountProfile();
        //        for (int i = 0; i < SNP.Count(); i++)
        //        {
        //            bool emptyRow = true;
        //            if (SNP[i].Cell[1] == clsFinalReportAB_SNPRow.MatrixValue.__)
        //            {

        //                for (int j = 1; j < SNP[i].Cell.Count(); j++)
        //                {
        //                    if (SNP[i].Cell[j] != clsFinalReportAB_SNPRow.MatrixValue.__)
        //                    {
        //                        emptyRow = false;
        //                        break;
        //                    }
        //                }

        //            }
        //            else
        //            {
        //                emptyRow = false;
        //            }
        //            if (emptyRow)
        //            { result.BlankLocusCount += 1; }
        //            else
        //            { result.CompletedLocusCount += 1; }

        //            result.TotalLocusCount += 1;
        //        }

        //        return result;
        //    }
        //}

        //public class clsLocusCountProfile
        //{
        //    public int TotalLocusCount { get; set; }
        //    public int BlankLocusCount { get; set; }

        //    public int CompletedLocusCount { get; set; }

        //    public override string ToString()
        //    {
        //        var result = string.Format(
        //            "{0} Completed Rows and {1} Blank Rows for {2} Total Rows",
        //            CompletedLocusCount,
        //            BlankLocusCount,
        //            TotalLocusCount);
        //        return result;
        //    }
        //}

        //public class clsSample
        //{
        //    public string GSGTVersion { get; set; }
        //    public DateTime ProcessingDate { get; set; }
        //    public string Content { get; set; }
        //    public int NumSNPs { get; set; }
        //    public int TotalSNPs { get; set; }
        //    public int NumSamples { get; set; }
        //    public int TotalSamples { get; set; }
        //    public string Filename { get; set; }
        //    public string SampleID { get; set; }
        //    public List<clsFinalReportSampleValue> SampleValue { get; set; }
        //}



        //private class clsFinalReportAB_SNPRow
        //{
        //    public clsFinalReportAB_SNPRow() { }
        //    public clsFinalReportAB_SNPRow(string DataLine, List<string> Columns)
        //    {
        //        string[] fields = DataLine.Split("\t".ToCharArray());
        //        Cell = new List<CellValue>(fields.Length);
        //        for (int i = 0; i < fields.Length; i++)
        //        {
        //            var item = new CellValue { Name = Columns[i], Value = fields[i] };
        //            this.Cell.Add(item);
        //        }
        //    }
        //    public string SNPName { get; set; }
        //    public List<CellValue> Cell { get; set; }

        //    public override string ToString()
        //    {
        //        string result = SNPName;
        //        for (int i = 0; i < Cell.Count(); i++)
        //        {
        //            result += " " + Cell[i];
        //        }
        //        return result;
        //    }
        //    public string ToString(StringBuilder sb)
        //    {
        //        sb.Clear();
        //        sb.Append(SNPName);
        //        for (int i = 0; i < Cell.Count(); i++)
        //        {
        //            sb.Append(" " + Cell[i]);
        //        }
        //        return sb.ToString();
        //    }
        //}

        //public class CellValue
        //{
        //    // Matrix column heading in the file
        //    public string Name { get; set; }
        //    public string Value { get; set; }
        //}

    }
}
//[Header]
//GSGT Version    1.9.4
//Processing Date 11/22/2016 4:36 PM
//Content     GGP_C3_B.bpm
//Num SNPs	62732
//Total SNPs  62831
//Num Samples 639
//Total Samples   645
//[Data]
//	219AEC4416	219AEC4125	219AEC3874	219AEC3873	219AEC3839	219AEC4912	219AEC4135	219AEC4617	219AEC3923	219AEC4122	219AEC3783	219AEC4628	219AEC4047	219AEC3909	219AEC4287	219AEC3977	219AEC4316	219AEC3825	219AEC4884	219AEC4417	219AEC3907	219AEC4446	219AEC4787	219AEC4107	219AEC3840	219AEC4387	219AEC3833	219AEC3854	219AEC5655	219AEC4350	219AEC5094	219AEC5069	219AEC4157	219AEC3847	219AEC5734	219AEC5431	219AEC4399	219AEC4112	219AEC3964	219AEC5295	219AEC5051	219AEC4073	219AEC4370	219AEC3952	219AEC4958	219AEC4988	219AEC3798	219AEC5567	219AEC3971	219AEC4327	219AEC5461	219AEC3948	219AEC4656	219AEC3933	219AEC4531	219AEC3989	219AEC3915	219AEC5035	219AEC5650	219AEC3984	219AEC4635	219AEC3823	219AEC4736	219AEC5541	219AEC5809	219AEC4908	219AEC4959	219AEC5307	219AEC5772	219AEC5525	219AEC5640	219AEC4757	219AEC4882	219AEC5437	219AEC4881	219AEC5543	219AEC4379	219AEC5217	219AEC5290	219AEC3979	219AEC4766	219AEC5224	219AEC3808	219AEC3814	219AEC3882	219AEC4889	219AEC4834	219AEC3807	219AEC5526	219AEC5445	219AEC3852	219AEC4329	219AEC4079	219AEC5074	219AEC5592	219AEC4962	219AEC4938	219AEC4934	219AEC4703	219AEC4931	219AEC3781	219AEC5538	219AEC4768	219AEC5070	219AEC4413	219AEC4405	219AEC3975	219AEC4124	219AEC4655	219AEC4590	219AEC4970	219AEC5549	219AEC4883	219AEC4937	219AEC4837	219AEC4907	219AEC3811	219AEC5536	219AEC5297	219AEC5658	219AEC5287	219AEC4634	219AEC4623	219AEC5076	219AEC5470	219AEC4915	219AEC5412	219AEC5578	219AEC4886	219AEC5317	219AEC5299	219AEC4659	219AEC4342	219AEC5391	219AEC5450	219AEC4630	219AEC4916	219AEC4512	219AEC5306	219AEC4495	219AEC3959	219AEC3867	219AEC4839	219AEC4955	219AEC4382	219AEC5816	219AEC5180	219AEC5178	219AEC3877	219AEC4863	219AEC3869	219AEC4354	219AEC4404	219AEC4390	219AEC3857	219AEC3815	219AEC3896	219AEC3809	219AEC3881	219AEC4505	219AEC4388	219AEC4415	219AEC3930	219AEC5309	219AEC3796	219AEC5634	219AEC3932	219AEC4517	219AEC3806	219AEC5667	219AEC3828	219AEC3779	219AEC5181	219AEC5439	219AEC5052	219AEC4954	219AEC3855	219AEC4072	219AEC4362	219AEC4117	219AEC4285	219AEC4925	219AEC5623	219AEC3917	219AEC4366	219AEC4396	219AEC4332	219AEC4304	219AEC3810	219AEC3826	219AEC4352	219AEC5219	219AEC4385	219AEC3985	219AEC4410	219AEC4406	219AEC4126	219AEC5377	219AEC5641	219AEC4237	219AEC4286	219AEC4315	219AEC4526	219AEC3784	219AEC4089	219AEC3982	219AEC4622	219AEC3870	219AEC4520	219AEC4145	219AEC4376	219AEC4602	219AEC5405	219AEC4942	219AEC5468	219AEC5199	219AEC5458	219AEC3925	219AEC4509	219AEC4499	219AEC4136	219AEC5081	219AEC4746	219AEC5026	219AEC4631	219AEC5513	219AEC4516	219AEC4596	219AEC4127	219AEC5676	219AEC4550	219AEC4948	219AEC5725	219AEC4769	219AEC4900	219AEC4905	219AEC5057	219AEC5292	219AEC4515	219AEC3919	219AEC4372	219AEC5454	219AEC4334	219AEC5208	219AEC4872	219AEC5195	219AEC5294	219AEC5034	219AEC4973	219AEC4846	219AEC4094	219AEC3883	219AEC5629	219AEC4756	219AEC5401	219AEC5084	219AEC5730	219AEC4733	219AEC5316	219AEC5378	219AEC3929	219AEC3850	219AEC4419	219AEC3868	219AEC4074	219AEC3892	219AEC3945	219AEC5573	219AEC4394	219AEC4401	219AEC4088	219AEC4105	219AEC4541	219AEC3782	219AEC4391	219AEC5811	219AEC3897	219AEC3888	219AEC3905	219AEC3957	219AEC3788	219AEC4490	219AEC3992	219AEC5320	219AEC3885	219AEC3965	219AEC3889	219AEC4498	219AEC4085	219AEC3941	219AEC3943	219AEC4084	219AEC5568	219AEC5668	219AEC5064	219AEC5368	219AEC4644	219AEC5071	219AEC4849	219AEC4906	219AEC5534	219AEC4836	219AEC5565	219AEC5519	219AEC4765	219AEC5202	219AEC5390	219AEC5550	219AEC5557	219AEC4845	219AEC5571	219AEC4957	219AEC5665	219AEC5449	219AEC5171	219AEC5798	219AEC3950	219AEC3838	219AEC3908	219AEC3903	219AEC4150	219AEC5569	219AEC4629	219AEC5636	219AEC4542	219AEC4284	219AEC4092	219AEC4491	219AEC3797	219AEC5446	219AEC5827	219AEC4835	219AEC5581	219AEC5532	219AEC5073	219AEC5044	219AEC5149	219AEC5254	219AEC5460	219AEC5654	219AEC4857	219AEC5375	219AEC4781	219AEC4639	219AEC5083	219AEC5038	219AEC4508	219AEC4604	219AEC4381	219AEC5079	219AEC5530	219AEC5075	219AEC5066	219AEC5196	219AEC5422	219AEC4501	219AEC4718	219AEC5664	219AEC4936	219AEC5814	219AEC5800	219AEC5388	219AEC3924	219AEC4141	219AEC4968	219AEC3928	219AEC5404	219AEC4324	219AEC3772	219AEC4847	219AEC5808	219AEC4502	219AEC5638	219AEC4528	219AEC4909	219AEC4364	219AEC3851	219AEC4108	219AEC4398	219AEC4815	219AEC4983	219AEC4770	219AEC3910	219AEC5572	219AEC5190	219AEC4764	219AEC5822	219AEC5176	219AEC4870	219AEC5666	219AEC5204	219AEC4926	219AEC5216	219AEC5305	219AEC4879	219AEC4918	219AEC4944	219AEC4935	219AEC4865	219AEC3956	219AEC4511	219AEC4075	219AEC4091	219AEC3954	219AEC4383	219AEC4913	219AEC4378	219AEC5322	219AEC4375	219AEC4880	219AEC4121	219AEC5448	219AEC4551	219AEC3863	219AEC4312	219AEC5522	219AEC5207	219AEC4513	219AEC4099	219AEC4153	219AEC4518	219AEC3978	219AEC3780	219AEC4420	219AEC3835	219AEC5182	219AEC4504	219AEC4510	219AEC3836	219AEC4296	219AEC4291	219AEC4302	219AEC4110	219AEC5041	219AEC3786	219AEC3813	219AEC4119	219AEC4175	219AEC4323	219AEC5563	219AEC4336	219AEC4493	219AEC3880	219AEC3804	219AEC3953	219AEC3988	219AEC4374	219AEC4549	219AEC4864	219AEC5179	219AEC4852	219AEC4838	219AEC5474	219AEC4760	219AEC4393	219AEC5220	219AEC5593	219AEC5206	219AEC5329	219AEC5226	219AEC5440	219AEC5017	219AEC5194	219AEC4832	219AEC5263	219AEC5040	219AEC5173	219AEC4646	219AEC4754	219AEC5089	219AEC5042	219AEC4554	219AEC4901	219AEC4331	219AEC4384	219AEC4540	219AEC3958	219AEC5724	219AEC4871	219AEC5346	219AEC5200	219AEC5799	219AEC4719	219AEC4914	219AEC4573	219AEC4692	219AEC5103	219AEC3962	219AEC5496	219AEC4603	219AEC4960	219AEC5399	219AEC5810	219AEC4763	219AEC4877	219AEC4917	219AEC5757	219AEC5312	219AEC5298	219AEC4965	219AEC4910	219AEC4673	219AEC4941	219AEC5558	219AEC4627	219AEC4831	219AEC4964	219AEC5712	219AEC5771	219AEC5302	219AEC4791	219AEC5308	219AEC3968	219AEC4775	219AEC5818	219AEC5817	219AEC5602	219AEC5813	219AEC5675	219AEC5078	219AEC5426	219AEC4885	219AEC4848	219AEC5469	219AEC5365	219AEC5222	219AEC5090	219AEC4314	219AEC4716	219AEC5583	219AEC5669	219AEC5379	219AEC4464	219AEC5815	219AEC5726	219AEC5509	219AEC5732	219AEC5289	219AEC5618	219AEC5047	219AEC5756	219AEC5184	219AEC5369	219AEC5193	219AEC4772	219AEC5019	219AEC5535	219AEC4899	219AEC5432	219AEC4333	219AEC4087	219AEC4090	219AEC4045	219AEC3967	219AEC5831	219AEC4039	219AEC5442	219AEC4403	219AEC4747	219AEC5471	219AEC5705	219AEC3846	219AEC4805	219AEC5315	219AEC5420	219AEC4878	219AEC4898	219AEC4789	219AEC5223	219AEC4654	219AEC5088	219AEC5417	219AEC5456	219AEC5680	219AEC4874	219AEC5105	219AEC5085	219AEC4855	219AEC5324	219AEC5607	219AEC5419	219AEC4833	219AEC4734	219AEC5429	219AEC4840	219AEC5301	219AEC4320	219AEC4589	219AEC4330	219AEC4897	219AEC5791	219AEC4945	219AEC5357	219AEC4053	219AEC5203	219AEC5591	219AEC4106	219AEC4986	219AEC5101	219AEC4120	219AEC4755	219AEC4861	219AEC4956	219AEC4097	219AEC4408	219AEC5444	219AEC5653	219AEC5795	219AEC4866	219AEC4844	219AEC4341	219AEC5643	219AEC4767	219AEC3876	219AEC3767	219AEC3900	219AEC4337	219AEC5736	219AEC3927	219AEC4489	219AEC3832	219AEC4020	219AEC4101	219AEC4355	219AEC3816	219AEC4697	219AEC3848	219AEC4492	219AEC4292	219AEC4467	219AEC4128	219AEC5595	219AEC4130	219AEC4497	219AEC4326	219AEC4448	219AEC4893	219AEC4569	219AEC4649
//10080_COIII BB  BB AA  BB BB  BB BB  BB BB  BB BB  AA BB  BB AA  AA BB  BB BB  BB BB  BB BB  BB BB  BB BB  BB BB  BB AA  BB BB  AA BB  BB AA  BB BB  BB BB  BB BB  BB BB  BB AA  BB BB  BB BB  BB BB  AA AA  BB AA  BB BB  BB BB  BB BB  BB BB  BB BB  BB AA  BB BB  BB BB  AA BB  BB BB  BB BB  AA BB  BB BB  BB BB  BB BB  BB BB  BB BB  BB BB  BB BB  AA BB  BB BB  AA BB  BB AA  BB BB  BB AA  BB BB  BB BB  BB BB  BB BB  AA BB  BB BB  BB BB  BB BB  BB BB  BB BB  BB BB  AA BB  BB BB  BB BB  AA BB  BB BB  AA AA  BB BB  BB BB  BB BB  BB BB  BB BB  BB BB  AA AA  BB BB  BB BB  BB BB  BB AA  BB BB  BB AA  BB BB  BB BB  BB BB  BB BB  BB BB  BB BB  BB AA  AA BB  BB BB  AA BB  BB BB  BB BB  BB BB  BB BB  AA BB  BB BB  BB AA  BB AA  BB BB  BB BB  AA BB  BB BB  BB BB  BB BB  AA BB  BB BB  BB BB  BB BB  BB AA  BB BB  AA BB  BB BB  BB BB  AA AA  AA BB  BB BB  BB BB  AA BB  AA BB  BB BB  BB BB  BB BB  BB BB  BB BB  BB AA  BB AA  BB AA  AA BB  BB BB  BB BB  AA AA  BB BB  BB BB  BB AA  BB BB  BB BB  AA AA  BB BB  BB BB  BB BB  BB BB  BB BB  BB BB  BB BB  BB BB  BB BB  AA BB  BB BB  AA BB  BB BB  BB BB  BB AA  BB BB  BB AA  BB BB  BB BB  BB BB  BB AA  BB BB  AA BB  BB BB  BB BB  BB BB  BB BB  AA BB  BB BB  BB BB  BB AA  BB BB  BB BB  BB BB  BB BB  BB BB  BB BB  BB AA  AA BB  BB BB  BB BB  BB BB  BB BB  BB BB  BB BB  AA BB  AA BB  BB AA  BB BB  BB BB  AA BB  AA BB  BB BB  BB BB  BB BB  AA BB  BB BB  BB BB  BB BB  AA BB  BB BB  BB BB  BB BB  BB BB  BB BB  BB BB  BB AA  AA AA  BB BB  BB BB  AA BB  BB BB  BB BB  BB BB  BB AA  BB BB  BB BB  BB BB  BB BB  BB AA  AA BB  BB BB  BB BB  BB BB  BB BB  BB BB  AA AA  BB BB  AA BB  BB BB  BB BB  BB BB  BB AA  AA BB  BB BB  BB AA  BB BB  BB AA  BB BB  BB AA  BB AA  BB BB  AA BB  BB AA  BB BB  BB BB  BB BB  BB BB  BB BB  BB BB  BB BB  BB AA  BB	--	BB BB  AA BB  BB BB  BB AA  AA BB  BB BB  BB BB  BB BB  BB AA  BB BB  BB AA  AA BB  AA BB  BB BB  BB BB  BB BB  AA BB  BB BB  BB	--	BB BB  AA BB  BB BB  BB BB  BB BB  BB AA  BB BB  BB BB  AA BB  AA BB  BB AA  BB BB  BB BB  BB AA  AA BB  BB BB  BB BB  BB BB  BB BB  BB BB  AA BB  BB BB  BB AA  BB BB  BB BB  BB BB  BB AA  BB BB  BB BB  BB AA  BB BB  BB BB  BB BB  BB AA  BB BB  BB AA  BB BB  BB BB  BB AA  BB BB  BB AA  AA AA  BB BB  BB BB  BB BB  BB BB  AA AA
//1