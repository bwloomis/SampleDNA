using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using DataHub.DataHubAgent;

namespace DataHub
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void btnBrowseGSLIMS_Click(object sender, EventArgs e)
        {
            var fn = DoOpenFile("Select GeneSeek LIMS DNA Report file", "Report");
            this.txtGSLIMS_Filename.Text = fn;
            this.txtDNAReportBSONFilename.Text = fn.Replace(".csv", ".bson");
        }

        private void tpDNAReport_Click(object sender, EventArgs e)
        {

        }

        private void btnCreateBSONFile_Click(object sender, EventArgs e)
        {
            if (!File.Exists(this.txtGSLIMS_Filename.Text))
                throw new FileNotFoundException(this.txtGSLIMS_Filename.Text);

            var dna = new clsDNAReport();
            dna.LoadFile(this.txtGSLIMS_Filename.Text);
            dna.WriteBSON(this.txtDNAReportBSONFilename.Text);
            MessageBox.Show("Create BSON File completed");
        }

        private void btnDNAReportBSON_File_Click(object sender, EventArgs e)
        {

        }

        private string DoOpenFile(string Prompt, string SearchString)
        {
            var ofd = new OpenFileDialog();
            ofd.Title = Prompt;
            ofd.FileName = "*" + SearchString + "*";
            ofd.ShowDialog();
            var result = ofd.FileName;
            return result;
        }

        private void btnSNPMapLIMS_Click(object sender, EventArgs e)
        {
            var fn = DoOpenFile("Select GeneSeek LIMS SNP Map file", "SNP");
            this.txtSNPMapLIMS.Text = fn;
            this.txtSNPMapOut.Text = fn.Replace(".txt", ".bson");
        }

        private void btnSNPMapOut_Click(object sender, EventArgs e)
        {
            var fn = DoOpenFile("Select GeneSeek LIMS SNP Map BSON file", "SNP");
            this.txtSNPMapOut.Text = fn;
        }

        private void btnSNPMapBSON_Click(object sender, EventArgs e)
        {
            if (!File.Exists(this.txtSNPMapLIMS.Text))
                throw new FileNotFoundException(this.txtSNPMapLIMS.Text);

            var dna = new clsSNPMap(this.txtSNPMapLIMS.Text, true);
            dna.WriteBSON(this.txtSNPMapOut.Text);
            MessageBox.Show("Create BSON File completed");
        }

        private void btnSampleMapSelectLIMS_Click(object sender, EventArgs e)
        {
            var fn = DoOpenFile("Select GeneSeek LIMS Sample Map file", "Sample");
            this.txtSampleMapLIMS.Text = fn;
            this.txtSampleMapBSON.Text = fn.Replace(".txt", ".bson");
        }

        private void btnSampleMapSelectBSON_Click(object sender, EventArgs e)
        {
            var fn = DoOpenFile("Select GeneSeek LIMS Sample Map BSON file", "Sample");
            this.txtSampleMapBSON.Text = fn;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!File.Exists(this.txtSampleMapLIMS.Text))
                throw new FileNotFoundException(this.txtSampleMapLIMS.Text);

            var sampleMap = new clsSampleMap(this.txtSampleMapLIMS.Text, true);
            sampleMap.WriteBSON(this.txtSampleMapBSON.Text);
            MessageBox.Show("Create BSON File completed");
        }

        private void btnLocusSummaryLIMS_Click(object sender, EventArgs e)
        {
            var fn = DoOpenFile("Select GeneSeek LIMS Locus Summary file", "Locus");
            this.txtLocusSummaryLIMS.Text = fn;
            this.txtLocusSummaryBSON.Text = fn.Replace(".csv", ".bson");
        }

        private void btnLocusSummaryBSON_Click(object sender, EventArgs e)
        {
            var fn = DoOpenFile("Select GeneSeek LIMS Locus Summary BSON file", "Locus");
            this.txtLocusSummaryBSON.Text = fn;

        }

        private void btnLocusSummaryCreateFile_Click(object sender, EventArgs e)
        {
            if (!File.Exists(this.txtLocusSummaryLIMS.Text))
                throw new FileNotFoundException(this.txtLocusSummaryLIMS.Text);

            var locusSummary = new clsLocusSummary(this.txtLocusSummaryLIMS.Text, true);
            locusSummary.WriteBSON(this.txtLocusSummaryBSON.Text);
            MessageBox.Show("Create BSON File completed");
        }

        private void Form2_Load(object sender, EventArgs e)
        {
        }

        private void btnFinalReportLIMSSelect_Click(object sender, EventArgs e)
        {
            var fn = DoOpenFile("Select GeneSeek LIMS FinalReport file", "Final");
            this.txtFinalReportLIMS.Text = fn;
            this.txtFinalReportSplitFolder.Text = Path.GetDirectoryName(this.txtFinalReportLIMS.Text);
            this.txtFinalReportBSON.Text = Path.GetDirectoryName(this.txtFinalReportLIMS.Text);
        }

        private void btnFinalReportSplitFolderSelect_Click(object sender, EventArgs e)
        {
        }

        private void btnFinalReportBSONFolderSelect_Click(object sender, EventArgs e)
        {

        }

        private void btnFinalReportSplitFiles_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            var finalReport = new clsFinalReport();
            finalReport.SplitIntoSingleSampleFiles(this.txtFinalReportLIMS.Text, this.txtFinalReportSplitFolder.Text);
            this.Cursor = Cursors.Default;
            MessageBox.Show("Split completed");
        }

        private void btnFinalReportCreateBSON_Click(object sender, EventArgs e)
        {
            if (!File.Exists(this.txtFinalReportLIMS.Text))
                throw new FileNotFoundException(this.txtLocusSummaryLIMS.Text);

            var finalReport = new clsFinalReport(this.txtFinalReportLIMS.Text, true);
            var outFilename = finalReport.Filename.Replace(".txt", ".bson");

            finalReport.WriteBSON(outFilename);
            MessageBox.Show("Create BSON File completed");

        }

        private void btnSupplemental_Click(object sender, EventArgs e)
        {
            clsSupplemental supplemental = new clsSupplemental(@"C:\samples\_SH_12_MG219_H4_RUN_FC_244638_HGC60KV03_20161122_Supplemental\SH_12_MG219_H4_RUN_FC_244638_HGC60KV03_20161122_Supplemental.txt", true);
            var json = supplemental.ToString();
            var headings = supplemental.ColumnHeadings();
            var properties = supplemental.Rows[0].PropertyNames();
            var values = supplemental.Rows[0].PropertyValues();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
