using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;
using Newtonsoft.Json;

namespace DataHub.DataHubAgent
{
    public class clsFinalReport
    {
        public string GSGTVersion { get; set; }
        public DateTime ProcessingDate { get; set; }
        public string Content { get; set; }
        public int NumSNPs { get; set; }
        public int TotalSNPs { get; set; }
        public int SampleIndex { get; set; }  // This is sample number from original FinalReport file before split into individual samples
        public int NumSamples { get; set; }
        public int TotalSamples { get; set; }
        public List<string> DataHeadings { get; set; }
        public List<clsFinalReportSNP> SNP { get; set; }

        // Private properties
        public string Filename { get; set; }
        private List<string> RawRows { get; set; }  // used for test BSON serialize / deserialize

        public clsFinalReport()
        {
            DataHeadings = new List<string>(20);
            SNP = new List<clsFinalReportSNP>(70000);
            DataHeadings = new List<string>(20);
        }

        public clsFinalReport(string Filename, bool RetainText)
        {
            if (!File.Exists(Filename))
                throw new FileNotFoundException(Filename);

            this.Filename = Filename;
            DataHeadings = new List<string>(20);
            SNP = new List<clsFinalReportSNP>(70000);
            DataHeadings = new List<string>(20);

            var lines = File.ReadAllLines(Filename);
            LoadFile(lines, RetainText);
        }

        /// <summary>
        /// Scan folder of individual FinalReport txt files and outputs associated BSON file for each
        /// </summary>
        /// <param name="Singles_Directory"></param>
        /// <param name="BSON_Directory"></param>
        //public void SinglesToBSON(string Singles_Directory, string BSON_Directory)
        //{
        //    if (Directory.Exists(Singles_Directory) == false)
        //        throw new DirectoryNotFoundException(Singles_Directory);

        //    if (Directory.Exists(BSON_Directory) == false)
        //        throw new DirectoryNotFoundException(BSON_Directory);

        //    var singles = Directory.GetFiles(Singles_Directory);
        //    for (int i = 0; i < singles.Length; i++)
        //    {
        //        var finalReport = new clsFinalReport(singles[i],false);

        //        var birdSample = new clsBirdSample();
        //        birdSample.FinalReport = finalReport;
        //        birdSample.LoadFile(finalReport, finalReport.GetSampleIDByIndex(0));
        //        var BSON = birdSample.GetCMD();
        //        var outFilename = Path.Combine(BSON_Directory, new FileInfo(finalReport.Filename).Name);
        //        if (File.Exists(outFilename))
        //            File.Delete(outFilename);
        //        File.WriteAllText(outFilename, BSON);
        //    }
        //}

        public override string ToString()
        {
            string json = JsonConvert.SerializeObject(this, Formatting.Indented);
            return json;
        }


        /// <summary>
        /// Loads a FinalReport.txt file, assumes enough memory (file was split if needed)
        /// </summary>
        /// <param name="lines"></param>
        /// <param name="RetainText"></param>
        public void LoadFile(string Filename, bool RetainText)
        {
            if (!File.Exists(Filename))
                throw new FileNotFoundException(Filename);

            this.Filename = Filename;
            var lines = File.ReadAllLines(Filename);
            LoadFile(lines, RetainText);
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
            bool inDataSection = false;
            for (int i = 0; i <= lines.GetUpperBound(0); i++)
            {
                string fullLine = lines[i];
                string[] fields = lines[i].Split("\t".ToCharArray());
                if (inDataSection)
                {
                    var snpRow = new clsFinalReportSNP(fields);
                    this.SNP.Add(snpRow);
                }
                else
                {
                    if (fields[0].StartsWith("GSGT Version")) { this.GSGTVersion = fields[1].Trim(); }
                    if (fields[0].StartsWith("Processing Date")) { this.ProcessingDate = DateTime.Parse(fields[1].Trim()); }
                    if (fields[0].StartsWith("Content")) { this.Content = fields[2].Trim(); }
                    if (fields[0].StartsWith("Num SNPs")) { this.NumSNPs = int.Parse(fields[1].Trim()); }
                    if (fields[0].StartsWith("Total SNPs")) { this.TotalSNPs = int.Parse(fields[1].Trim()); }
                    if (fields[0].StartsWith("Num Samples")) { this.NumSamples = int.Parse(fields[1].Trim()); }
                    if (fields[0].StartsWith("Total Samples")) { this.TotalSamples = int.Parse(fields[1].Trim()); }
                    if (fields[0].StartsWith("SampleIndex")) { this.SampleIndex = int.Parse(fields[1].Trim()); }
                    if (fields[0].StartsWith("Filename")) { this.Filename = fields[1].Trim(); }

                    if (fields[0].StartsWith("[Data]"))
                    {
                        if (this.SampleIndex <= 0 & this.NumSamples != 1)
                            throw new Exception("FinalReport File Must Be Split Into Single Sample Files");

                        // Get the column headings for the data section
                        i += 1;
                        string[] dataHeading = lines[i].Split("\t".ToCharArray());
                        this.DataHeadings.AddRange(dataHeading);
                        inDataSection = true;
                    }
                }
            }
        }

        /// <summary>
        /// A single FinalReport.txt file may be too big to fit into memory. Use this to split into individual files if needed.
        /// </summary>
        /// <param name="SourceFilename"></param>
        /// <param name="DestinationFolder"></param>
        public void SplitIntoSingleSampleFiles(string SourceFilename, string DestinationFolder)
        {
            if (System.IO.File.Exists(SourceFilename) == false)
                throw new System.IO.FileNotFoundException(SourceFilename);

            if (System.IO.Directory.Exists(DestinationFolder) == false)
                throw new System.IO.DirectoryNotFoundException(DestinationFolder);

            this.Filename = SourceFilename;

            var HeaderLines = new List<string>(10);
            var DataHeader = new List<string>(2);
            var SampleLines = new List<string>(63000);
            var currentSampleID = "";
            bool inDataSection = false;
            int SampleIndex = 1;

            using (StreamReader sr = new StreamReader(SourceFilename))
            {
                while (sr.Peek() >= 0)
                {
                    var line = sr.ReadLine();
                    if (inDataSection == false)
                    {
                        if (line.StartsWith("[Data]"))
                        {
                            DataHeader.Add(line);
                            inDataSection = true;
                            var colHeadings = sr.ReadLine();
                            DataHeader.Add(colHeadings);
                            var firstDataLine = sr.ReadLine();
                            SampleLines.Add(firstDataLine);
                            string[] fields = firstDataLine.Split("\t".ToCharArray());
                            currentSampleID = fields[1];
                        }
                        else
                        {
                            HeaderLines.Add(line);
                        }
                    }
                    else
                    {
                        string[] fields = line.Split("\t".ToCharArray());
                        if (currentSampleID == fields[1]) SampleLines.Add(line);
                        else
                        {
                            if (SampleLines.Count > 0)
                            {
                                var outFileName = System.IO.Path.Combine(DestinationFolder, string.Format("{0}_Sample_{1}.txt", this.Filename, currentSampleID));
                                if (File.Exists(outFileName)) File.Delete(outFileName);
                                File.WriteAllLines(outFileName, HeaderLines.ToArray());
                                File.AppendAllText(outFileName, "SampleIndex\t" + SampleIndex.ToString() + "\r");
                                File.AppendAllText(outFileName, "Filename\t" + Filename + "\r");
                                File.AppendAllLines(outFileName, DataHeader.ToArray());
                                File.AppendAllLines(outFileName, SampleLines.ToArray());
                                SampleLines = new List<string>(63000);
                                currentSampleID = fields[1];
                                SampleIndex += 1;
                            }
                        }
                    }
                }
                if (SampleLines.Count > 0)
                {
                    // should be placed into a method and called 
                    var outFileName = System.IO.Path.Combine(DestinationFolder, string.Format("{0}_Sample_{1}.txt", this.Filename, currentSampleID));
                    if (File.Exists(outFileName)) File.Delete(outFileName);
                    File.WriteAllLines(outFileName, HeaderLines.ToArray());
                    File.AppendAllText(outFileName, "SampleIndex\t" + SampleIndex.ToString() + "\r");
                    File.AppendAllText(outFileName, "Filename\t" + Filename + "\r");
                    File.AppendAllLines(outFileName, DataHeader.ToArray());
                    File.AppendAllLines(outFileName, SampleLines.ToArray());
                    HeaderLines.Clear();
                    SampleLines.Clear();
                    currentSampleID = "";
                }
            }
        }

        /// <summary>
        /// Use this method to get the Header section
        /// </summary>
        /// <returns>List<<string>></returns>
        private List<string> HeaderSection()
        {
            var items = new List<string>();
            items.Add("GSGT Version = " + GSGTVersion);
            items.Add("Processing Date = " + ProcessingDate.ToString());
            items.Add("Content = " + Content);
            items.Add("# SNPs = " + NumSNPs);
            items.Add("Total SNPs = " + TotalSNPs);
            items.Add("# Samples = " + NumSamples);
            items.Add("Total Samples = " + TotalSamples);
            items.Add("Filename = " + Filename);
            return items;
        }

        /// <summary>
        /// Create a single BSON file for the clsFinalReport
        /// </summary>
        /// <param name="Filename"></param>
        public void WriteBSON(string Filename)
        {
            if (File.Exists(Filename)) File.Delete(Filename);
            string BSON = this.ToString();
            File.WriteAllText(Filename, BSON);
        }

        //public List<string> GetSampleNameList()
        //{
        //    var result = new List<string>(Sample.Count);
        //    for (int i = 1; i < Sample.Count; i++)
        //    {
        //        result.Add(Sample[i].SampleID);
        //    }
        //    return result;
        //}

        //public List<string> GetSampleStringList(int Index)
        //{
        //    var result = new List<string>(63000);
        //    result.Add("Sample ID = " + Sample[Index].SampleID);

        //    for (int i = 0; i < Sample[Index].SNP.Count(); i++)
        //    {
        //        result.Add(Sample[Index].SNP[i].ToString());
        //    }
        //    return result;
        //}

        //private List<clsFinalReportSampleValue> GetSampleValues(int Index)
        //{
        //    var result = new List<clsFinalReportSampleValue>();
        //var result = new List<clsSampleValue>(SNP.Count);
        //for (int i = 0; i < SNP.Count; i++)
        //{
        //    var item = new clsSampleValue();
        //    item.SNPName = SNP[i].SNPName;
        //    item.SNPValue = SNP[i].Cell[Index];
        //    result.Add(item);
        //}
        //    return result;
        //}
        
        //public clsFinalReport GetSampleWithHeader(int Index)
        //{
        //    var result = new clsFinalReport();
        //    result.Content = this.Content;
        //    result.Filename = this.Filename;
        //    result.GSGTVersion = this.GSGTVersion;
        //    result.NumSamples = this.NumSamples;
        //    result.NumSNPs = this.NumSNPs;
        //    result.ProcessingDate = this.ProcessingDate;
        //    result.TotalSamples = this.TotalSamples;
        //    result.TotalSNPs = this.TotalSNPs;
        //    result.DataHeadings = this.DataHeadings;
        //    result.SNP.Clear();
        //    result.SNP.Add(this.SNP[Index]);
        //    return result;
        //}

        //public int GetIndexBySampleID(string SampleID)
        //{
        //    int result = this.Sample.FindIndex(x => x.SampleID == SampleID);
        //    return result;
        //}

        //public string GetSampleIDByIndex(int Index)
        //{
        //    string result = "";
        //    result = Sample[Index].SampleID;
        //    return result;
        //}

        //public string GetCMD(int Index)
        //{  // Works for manually copy an paste into MongoDB command line
        //    var result = new StringBuilder(130000);
        //    result.Append("db.BirdSample.insert({");
        //    result.Append(string.Format("SampleID : '{0}',\r", this.Sample[Index].SampleID));
        //    result.Append(string.Format("Content : '{0}',\r", this.Content));
        //    result.Append(string.Format("Filename : '{0}',\r", this.Filename));
        //    result.Append(string.Format("GSGTVersion : '{0}',\r", this.GSGTVersion));
        //    result.Append(string.Format("NumSamples : {0},\r", this.NumSamples));
        //    result.Append(string.Format("NumSNPs : {0},\r", this.NumSNPs));
        //    result.Append(string.Format("ProcessingDate : ISODate('{0}'),\r",
        //        this.ProcessingDate.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'")));
        //    result.Append(string.Format("TotalSamples : {0},\r", this.TotalSamples));
        //    result.Append(string.Format("TotalSNPs : {0},\r", this.TotalSNPs));
        //    result.Append("SNPs : [\r");
        //    var maxValue = Sample[Index].SNP.Count;
        //    //maxValue = 3; //test
        //    for (int j = 0; j < maxValue; j++)
        //    {
        //        result.Append(Sample[Index].SNP[j].GetCMD(4));
        //        if (j + 1 < maxValue) result.Append(",");
        //        result.Append("\r");
        //    }
        //    result.Append("]\r");
        //    result.Append("});\r");
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
        //        result.Append(string.Format("    {{\"SNP\" : \"{0}\", \"Value\" : \"{1}\"}}",
        //            sample.SampleValue[j].SNPName,
        //            sample.SampleValue[j].SNPValue.ToString()));
        //        if (j + 1 < sample.SampleValue.Count) result.Append(",");
        //        result.Append("\r");
        //    }
        //    result.Append("]\r");
        //    result.Append("}\r");
        //    return result.ToString();
        }

    }
//}
//    public class clsFinalReportSNP
//    {
//        public string SampleID { get; set; }
//        public List<clsFinalReportDataRow> SNP { get; set; }

//        public clsFinalReportSNP()
//        {
//            SNP = new List<clsFinalReportDataRow>(63000);
//        }

//        public override string ToString()
//        {
//            return string.Format("Sample: {0} with {1} SNPs", SampleID, SNP.Count());
//        }

//        public string GetCMD()
//        {
//            StringBuilder sb = new StringBuilder(6300000);
//            sb.Append("{ \r");
//            sb.Append("SampleID : '" + SampleID + "', \r");

//            sb.Append("    SNPs : [ /r");
//            for (int i = 0; i < SNP.Count; i++)
//            {
//                sb.Append(SNP[i].GetCMD(4));
//                if ((i + 1) < SNP.Count)
//                    sb.Append(",/r");
//            }
//            sb.Append("]/r");

//            return sb.ToString();
//        }
//    }
//}


//[Header]
//GSGT Version    1.9.4
//Processing Date 11/22/2016 3:11 PM
//Content     GGP_C3_B.bpm
//Num SNPs	62732
//Total SNPs  62831
//Num Samples 51
//Total Samples   51
//[Data]
//SNP Name    Sample ID   Allele1 - Forward Allele2 - Forward Allele1 - Top Allele2 - Top Allele1 - AB Allele2 - AB GC Score X   Y
//10080_COIII	38AAY7849 G   G G   G B   B	0.7048	0.027	1.315
//10668_COIII	38AAY7849 C   C G   G B   B	0.7672	0.027	0.996