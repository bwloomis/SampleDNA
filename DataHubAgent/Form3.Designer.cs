namespace DataHub
{
    partial class Form3
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabCobb = new System.Windows.Forms.TabControl();
            this.tpDNAReport = new System.Windows.Forms.TabPage();
            this.btnCreateBSONFile = new System.Windows.Forms.Button();
            this.txtDNAReportBSONFilename = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnDNAReportBSON_File = new System.Windows.Forms.Button();
            this.txtGSLIMS_Filename = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnDNAReport_Select = new System.Windows.Forms.Button();
            this.tpSNP_Map = new System.Windows.Forms.TabPage();
            this.btnSNPMapBSON = new System.Windows.Forms.Button();
            this.txtSNPMapOut = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSNPMapOut = new System.Windows.Forms.Button();
            this.txtSNPMapLIMS = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSNPMapLIMS = new System.Windows.Forms.Button();
            this.tpSampleMap = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.txtSampleMapBSON = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSampleMapSelectBSON = new System.Windows.Forms.Button();
            this.txtSampleMapLIMS = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnSampleMapSelectLIMS = new System.Windows.Forms.Button();
            this.tpLocusSummary = new System.Windows.Forms.TabPage();
            this.btnLocusSummaryCreateFile = new System.Windows.Forms.Button();
            this.txtLocusSummaryBSON = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnLocusSummaryBSON = new System.Windows.Forms.Button();
            this.txtLocusSummaryLIMS = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnLocusSummaryLIMS = new System.Windows.Forms.Button();
            this.tpFinalReport = new System.Windows.Forms.TabPage();
            this.txtFinalReportBSON = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.btnFinalReportBSONFolderSelect = new System.Windows.Forms.Button();
            this.btnFinalReportSplitFiles = new System.Windows.Forms.Button();
            this.btnFinalReportCreateBSON = new System.Windows.Forms.Button();
            this.txtFinalReportSplitFolder = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnFinalReportSplitFolderSelect = new System.Windows.Forms.Button();
            this.txtFinalReportLIMS = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.btnFinalReportLIMSSelect = new System.Windows.Forms.Button();
            this.tpSupplemental = new System.Windows.Forms.TabPage();
            this.btnSupplemental = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.tabCobb.SuspendLayout();
            this.tpDNAReport.SuspendLayout();
            this.tpSNP_Map.SuspendLayout();
            this.tpSampleMap.SuspendLayout();
            this.tpLocusSummary.SuspendLayout();
            this.tpFinalReport.SuspendLayout();
            this.tpSupplemental.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(521, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.tabCobb);
            this.panel1.Location = new System.Drawing.Point(12, 50);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(497, 390);
            this.panel1.TabIndex = 1;
            // 
            // tabCobb
            // 
            this.tabCobb.Controls.Add(this.tpDNAReport);
            this.tabCobb.Controls.Add(this.tpSNP_Map);
            this.tabCobb.Controls.Add(this.tpSampleMap);
            this.tabCobb.Controls.Add(this.tpLocusSummary);
            this.tabCobb.Controls.Add(this.tpFinalReport);
            this.tabCobb.Controls.Add(this.tpSupplemental);
            this.tabCobb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabCobb.Location = new System.Drawing.Point(0, 0);
            this.tabCobb.Name = "tabCobb";
            this.tabCobb.SelectedIndex = 0;
            this.tabCobb.Size = new System.Drawing.Size(497, 390);
            this.tabCobb.TabIndex = 0;
            // 
            // tpDNAReport
            // 
            this.tpDNAReport.Controls.Add(this.button2);
            this.tpDNAReport.Controls.Add(this.btnCreateBSONFile);
            this.tpDNAReport.Controls.Add(this.txtDNAReportBSONFilename);
            this.tpDNAReport.Controls.Add(this.label1);
            this.tpDNAReport.Controls.Add(this.btnDNAReportBSON_File);
            this.tpDNAReport.Controls.Add(this.txtGSLIMS_Filename);
            this.tpDNAReport.Controls.Add(this.label2);
            this.tpDNAReport.Controls.Add(this.btnDNAReport_Select);
            this.tpDNAReport.Location = new System.Drawing.Point(4, 22);
            this.tpDNAReport.Name = "tpDNAReport";
            this.tpDNAReport.Padding = new System.Windows.Forms.Padding(3);
            this.tpDNAReport.Size = new System.Drawing.Size(489, 364);
            this.tpDNAReport.TabIndex = 0;
            this.tpDNAReport.Text = "DNA Report";
            this.tpDNAReport.UseVisualStyleBackColor = true;
            this.tpDNAReport.Click += new System.EventHandler(this.tpDNAReport_Click);
            // 
            // btnCreateBSONFile
            // 
            this.btnCreateBSONFile.Location = new System.Drawing.Point(7, 78);
            this.btnCreateBSONFile.Name = "btnCreateBSONFile";
            this.btnCreateBSONFile.Size = new System.Drawing.Size(111, 23);
            this.btnCreateBSONFile.TabIndex = 7;
            this.btnCreateBSONFile.Text = "Create BSON File";
            this.btnCreateBSONFile.UseVisualStyleBackColor = true;
            this.btnCreateBSONFile.Click += new System.EventHandler(this.btnCreateBSONFile_Click);
            // 
            // txtDNAReportBSONFilename
            // 
            this.txtDNAReportBSONFilename.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDNAReportBSONFilename.Location = new System.Drawing.Point(115, 42);
            this.txtDNAReportBSONFilename.Name = "txtDNAReportBSONFilename";
            this.txtDNAReportBSONFilename.Size = new System.Drawing.Size(285, 20);
            this.txtDNAReportBSONFilename.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "BSON Output File";
            // 
            // btnDNAReportBSON_File
            // 
            this.btnDNAReportBSON_File.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDNAReportBSON_File.Location = new System.Drawing.Point(406, 42);
            this.btnDNAReportBSON_File.Name = "btnDNAReportBSON_File";
            this.btnDNAReportBSON_File.Size = new System.Drawing.Size(75, 23);
            this.btnDNAReportBSON_File.TabIndex = 4;
            this.btnDNAReportBSON_File.Text = "Select";
            this.btnDNAReportBSON_File.UseVisualStyleBackColor = true;
            this.btnDNAReportBSON_File.Click += new System.EventHandler(this.btnDNAReportBSON_File_Click);
            // 
            // txtGSLIMS_Filename
            // 
            this.txtGSLIMS_Filename.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGSLIMS_Filename.Location = new System.Drawing.Point(115, 10);
            this.txtGSLIMS_Filename.Name = "txtGSLIMS_Filename";
            this.txtGSLIMS_Filename.Size = new System.Drawing.Size(285, 20);
            this.txtGSLIMS_Filename.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "GeneSeek LIMS File";
            // 
            // btnDNAReport_Select
            // 
            this.btnDNAReport_Select.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDNAReport_Select.Location = new System.Drawing.Point(406, 10);
            this.btnDNAReport_Select.Name = "btnDNAReport_Select";
            this.btnDNAReport_Select.Size = new System.Drawing.Size(75, 23);
            this.btnDNAReport_Select.TabIndex = 0;
            this.btnDNAReport_Select.Text = "Select";
            this.btnDNAReport_Select.UseVisualStyleBackColor = true;
            this.btnDNAReport_Select.Click += new System.EventHandler(this.btnBrowseGSLIMS_Click);
            // 
            // tpSNP_Map
            // 
            this.tpSNP_Map.Controls.Add(this.btnSNPMapBSON);
            this.tpSNP_Map.Controls.Add(this.txtSNPMapOut);
            this.tpSNP_Map.Controls.Add(this.label3);
            this.tpSNP_Map.Controls.Add(this.btnSNPMapOut);
            this.tpSNP_Map.Controls.Add(this.txtSNPMapLIMS);
            this.tpSNP_Map.Controls.Add(this.label4);
            this.tpSNP_Map.Controls.Add(this.btnSNPMapLIMS);
            this.tpSNP_Map.Location = new System.Drawing.Point(4, 22);
            this.tpSNP_Map.Name = "tpSNP_Map";
            this.tpSNP_Map.Padding = new System.Windows.Forms.Padding(3);
            this.tpSNP_Map.Size = new System.Drawing.Size(489, 364);
            this.tpSNP_Map.TabIndex = 1;
            this.tpSNP_Map.Text = "SNP Map";
            this.tpSNP_Map.UseVisualStyleBackColor = true;
            // 
            // btnSNPMapBSON
            // 
            this.btnSNPMapBSON.Location = new System.Drawing.Point(9, 79);
            this.btnSNPMapBSON.Name = "btnSNPMapBSON";
            this.btnSNPMapBSON.Size = new System.Drawing.Size(111, 23);
            this.btnSNPMapBSON.TabIndex = 14;
            this.btnSNPMapBSON.Text = "Create BSON File";
            this.btnSNPMapBSON.UseVisualStyleBackColor = true;
            this.btnSNPMapBSON.Click += new System.EventHandler(this.btnSNPMapBSON_Click);
            // 
            // txtSNPMapOut
            // 
            this.txtSNPMapOut.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSNPMapOut.Location = new System.Drawing.Point(117, 43);
            this.txtSNPMapOut.Name = "txtSNPMapOut";
            this.txtSNPMapOut.Size = new System.Drawing.Size(285, 20);
            this.txtSNPMapOut.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "BSON Output File";
            // 
            // btnSNPMapOut
            // 
            this.btnSNPMapOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSNPMapOut.Location = new System.Drawing.Point(408, 43);
            this.btnSNPMapOut.Name = "btnSNPMapOut";
            this.btnSNPMapOut.Size = new System.Drawing.Size(75, 23);
            this.btnSNPMapOut.TabIndex = 11;
            this.btnSNPMapOut.Text = "Select";
            this.btnSNPMapOut.UseVisualStyleBackColor = true;
            this.btnSNPMapOut.Click += new System.EventHandler(this.btnSNPMapOut_Click);
            // 
            // txtSNPMapLIMS
            // 
            this.txtSNPMapLIMS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSNPMapLIMS.Location = new System.Drawing.Point(117, 11);
            this.txtSNPMapLIMS.Name = "txtSNPMapLIMS";
            this.txtSNPMapLIMS.Size = new System.Drawing.Size(285, 20);
            this.txtSNPMapLIMS.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "GeneSeek LIMS File";
            // 
            // btnSNPMapLIMS
            // 
            this.btnSNPMapLIMS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSNPMapLIMS.Location = new System.Drawing.Point(408, 11);
            this.btnSNPMapLIMS.Name = "btnSNPMapLIMS";
            this.btnSNPMapLIMS.Size = new System.Drawing.Size(75, 23);
            this.btnSNPMapLIMS.TabIndex = 8;
            this.btnSNPMapLIMS.Text = "Select";
            this.btnSNPMapLIMS.UseVisualStyleBackColor = true;
            this.btnSNPMapLIMS.Click += new System.EventHandler(this.btnSNPMapLIMS_Click);
            // 
            // tpSampleMap
            // 
            this.tpSampleMap.Controls.Add(this.button1);
            this.tpSampleMap.Controls.Add(this.txtSampleMapBSON);
            this.tpSampleMap.Controls.Add(this.label5);
            this.tpSampleMap.Controls.Add(this.btnSampleMapSelectBSON);
            this.tpSampleMap.Controls.Add(this.txtSampleMapLIMS);
            this.tpSampleMap.Controls.Add(this.label6);
            this.tpSampleMap.Controls.Add(this.btnSampleMapSelectLIMS);
            this.tpSampleMap.Location = new System.Drawing.Point(4, 22);
            this.tpSampleMap.Name = "tpSampleMap";
            this.tpSampleMap.Size = new System.Drawing.Size(489, 364);
            this.tpSampleMap.TabIndex = 2;
            this.tpSampleMap.Text = "Sample Map";
            this.tpSampleMap.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(7, 81);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(111, 23);
            this.button1.TabIndex = 14;
            this.button1.Text = "Create BSON File";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtSampleMapBSON
            // 
            this.txtSampleMapBSON.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSampleMapBSON.Location = new System.Drawing.Point(115, 45);
            this.txtSampleMapBSON.Name = "txtSampleMapBSON";
            this.txtSampleMapBSON.Size = new System.Drawing.Size(285, 20);
            this.txtSampleMapBSON.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "BSON Output File";
            // 
            // btnSampleMapSelectBSON
            // 
            this.btnSampleMapSelectBSON.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSampleMapSelectBSON.Location = new System.Drawing.Point(406, 45);
            this.btnSampleMapSelectBSON.Name = "btnSampleMapSelectBSON";
            this.btnSampleMapSelectBSON.Size = new System.Drawing.Size(75, 23);
            this.btnSampleMapSelectBSON.TabIndex = 11;
            this.btnSampleMapSelectBSON.Text = "Select";
            this.btnSampleMapSelectBSON.UseVisualStyleBackColor = true;
            this.btnSampleMapSelectBSON.Click += new System.EventHandler(this.btnSampleMapSelectBSON_Click);
            // 
            // txtSampleMapLIMS
            // 
            this.txtSampleMapLIMS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSampleMapLIMS.Location = new System.Drawing.Point(115, 13);
            this.txtSampleMapLIMS.Name = "txtSampleMapLIMS";
            this.txtSampleMapLIMS.Size = new System.Drawing.Size(285, 20);
            this.txtSampleMapLIMS.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 18);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(105, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "GeneSeek LIMS File";
            // 
            // btnSampleMapSelectLIMS
            // 
            this.btnSampleMapSelectLIMS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSampleMapSelectLIMS.Location = new System.Drawing.Point(406, 13);
            this.btnSampleMapSelectLIMS.Name = "btnSampleMapSelectLIMS";
            this.btnSampleMapSelectLIMS.Size = new System.Drawing.Size(75, 23);
            this.btnSampleMapSelectLIMS.TabIndex = 8;
            this.btnSampleMapSelectLIMS.Text = "Select";
            this.btnSampleMapSelectLIMS.UseVisualStyleBackColor = true;
            this.btnSampleMapSelectLIMS.Click += new System.EventHandler(this.btnSampleMapSelectLIMS_Click);
            // 
            // tpLocusSummary
            // 
            this.tpLocusSummary.Controls.Add(this.btnLocusSummaryCreateFile);
            this.tpLocusSummary.Controls.Add(this.txtLocusSummaryBSON);
            this.tpLocusSummary.Controls.Add(this.label7);
            this.tpLocusSummary.Controls.Add(this.btnLocusSummaryBSON);
            this.tpLocusSummary.Controls.Add(this.txtLocusSummaryLIMS);
            this.tpLocusSummary.Controls.Add(this.label8);
            this.tpLocusSummary.Controls.Add(this.btnLocusSummaryLIMS);
            this.tpLocusSummary.Location = new System.Drawing.Point(4, 22);
            this.tpLocusSummary.Name = "tpLocusSummary";
            this.tpLocusSummary.Size = new System.Drawing.Size(489, 364);
            this.tpLocusSummary.TabIndex = 3;
            this.tpLocusSummary.Text = "Locus Summary";
            this.tpLocusSummary.UseVisualStyleBackColor = true;
            // 
            // btnLocusSummaryCreateFile
            // 
            this.btnLocusSummaryCreateFile.Location = new System.Drawing.Point(11, 83);
            this.btnLocusSummaryCreateFile.Name = "btnLocusSummaryCreateFile";
            this.btnLocusSummaryCreateFile.Size = new System.Drawing.Size(111, 23);
            this.btnLocusSummaryCreateFile.TabIndex = 21;
            this.btnLocusSummaryCreateFile.Text = "Create BSON File";
            this.btnLocusSummaryCreateFile.UseVisualStyleBackColor = true;
            this.btnLocusSummaryCreateFile.Click += new System.EventHandler(this.btnLocusSummaryCreateFile_Click);
            // 
            // txtLocusSummaryBSON
            // 
            this.txtLocusSummaryBSON.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLocusSummaryBSON.Location = new System.Drawing.Point(119, 47);
            this.txtLocusSummaryBSON.Name = "txtLocusSummaryBSON";
            this.txtLocusSummaryBSON.Size = new System.Drawing.Size(285, 20);
            this.txtLocusSummaryBSON.TabIndex = 20;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 52);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(91, 13);
            this.label7.TabIndex = 19;
            this.label7.Text = "BSON Output File";
            // 
            // btnLocusSummaryBSON
            // 
            this.btnLocusSummaryBSON.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLocusSummaryBSON.Location = new System.Drawing.Point(410, 47);
            this.btnLocusSummaryBSON.Name = "btnLocusSummaryBSON";
            this.btnLocusSummaryBSON.Size = new System.Drawing.Size(75, 23);
            this.btnLocusSummaryBSON.TabIndex = 18;
            this.btnLocusSummaryBSON.Text = "Select";
            this.btnLocusSummaryBSON.UseVisualStyleBackColor = true;
            this.btnLocusSummaryBSON.Click += new System.EventHandler(this.btnLocusSummaryBSON_Click);
            // 
            // txtLocusSummaryLIMS
            // 
            this.txtLocusSummaryLIMS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLocusSummaryLIMS.Location = new System.Drawing.Point(119, 15);
            this.txtLocusSummaryLIMS.Name = "txtLocusSummaryLIMS";
            this.txtLocusSummaryLIMS.Size = new System.Drawing.Size(285, 20);
            this.txtLocusSummaryLIMS.TabIndex = 17;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 20);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(105, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "GeneSeek LIMS File";
            // 
            // btnLocusSummaryLIMS
            // 
            this.btnLocusSummaryLIMS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLocusSummaryLIMS.Location = new System.Drawing.Point(410, 15);
            this.btnLocusSummaryLIMS.Name = "btnLocusSummaryLIMS";
            this.btnLocusSummaryLIMS.Size = new System.Drawing.Size(75, 23);
            this.btnLocusSummaryLIMS.TabIndex = 15;
            this.btnLocusSummaryLIMS.Text = "Select";
            this.btnLocusSummaryLIMS.UseVisualStyleBackColor = true;
            this.btnLocusSummaryLIMS.Click += new System.EventHandler(this.btnLocusSummaryLIMS_Click);
            // 
            // tpFinalReport
            // 
            this.tpFinalReport.Controls.Add(this.txtFinalReportBSON);
            this.tpFinalReport.Controls.Add(this.label11);
            this.tpFinalReport.Controls.Add(this.btnFinalReportBSONFolderSelect);
            this.tpFinalReport.Controls.Add(this.btnFinalReportSplitFiles);
            this.tpFinalReport.Controls.Add(this.btnFinalReportCreateBSON);
            this.tpFinalReport.Controls.Add(this.txtFinalReportSplitFolder);
            this.tpFinalReport.Controls.Add(this.label9);
            this.tpFinalReport.Controls.Add(this.btnFinalReportSplitFolderSelect);
            this.tpFinalReport.Controls.Add(this.txtFinalReportLIMS);
            this.tpFinalReport.Controls.Add(this.label10);
            this.tpFinalReport.Controls.Add(this.btnFinalReportLIMSSelect);
            this.tpFinalReport.Location = new System.Drawing.Point(4, 22);
            this.tpFinalReport.Name = "tpFinalReport";
            this.tpFinalReport.Size = new System.Drawing.Size(489, 364);
            this.tpFinalReport.TabIndex = 4;
            this.tpFinalReport.Text = "Final Report";
            this.tpFinalReport.UseVisualStyleBackColor = true;
            // 
            // txtFinalReportBSON
            // 
            this.txtFinalReportBSON.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFinalReportBSON.Location = new System.Drawing.Point(116, 83);
            this.txtFinalReportBSON.Name = "txtFinalReportBSON";
            this.txtFinalReportBSON.Size = new System.Drawing.Size(285, 20);
            this.txtFinalReportBSON.TabIndex = 18;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(17, 88);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(93, 13);
            this.label11.TabIndex = 17;
            this.label11.Text = "BSON Files Folder";
            // 
            // btnFinalReportBSONFolderSelect
            // 
            this.btnFinalReportBSONFolderSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFinalReportBSONFolderSelect.Location = new System.Drawing.Point(407, 83);
            this.btnFinalReportBSONFolderSelect.Name = "btnFinalReportBSONFolderSelect";
            this.btnFinalReportBSONFolderSelect.Size = new System.Drawing.Size(75, 23);
            this.btnFinalReportBSONFolderSelect.TabIndex = 16;
            this.btnFinalReportBSONFolderSelect.Text = "Select";
            this.btnFinalReportBSONFolderSelect.UseVisualStyleBackColor = true;
            this.btnFinalReportBSONFolderSelect.Click += new System.EventHandler(this.btnFinalReportBSONFolderSelect_Click);
            // 
            // btnFinalReportSplitFiles
            // 
            this.btnFinalReportSplitFiles.Location = new System.Drawing.Point(116, 118);
            this.btnFinalReportSplitFiles.Name = "btnFinalReportSplitFiles";
            this.btnFinalReportSplitFiles.Size = new System.Drawing.Size(111, 23);
            this.btnFinalReportSplitFiles.TabIndex = 15;
            this.btnFinalReportSplitFiles.Text = "Split Into Files";
            this.btnFinalReportSplitFiles.UseVisualStyleBackColor = true;
            this.btnFinalReportSplitFiles.Click += new System.EventHandler(this.btnFinalReportSplitFiles_Click);
            // 
            // btnFinalReportCreateBSON
            // 
            this.btnFinalReportCreateBSON.Location = new System.Drawing.Point(290, 118);
            this.btnFinalReportCreateBSON.Name = "btnFinalReportCreateBSON";
            this.btnFinalReportCreateBSON.Size = new System.Drawing.Size(111, 23);
            this.btnFinalReportCreateBSON.TabIndex = 14;
            this.btnFinalReportCreateBSON.Text = "Create BSON File(s)";
            this.btnFinalReportCreateBSON.UseVisualStyleBackColor = true;
            this.btnFinalReportCreateBSON.Click += new System.EventHandler(this.btnFinalReportCreateBSON_Click);
            // 
            // txtFinalReportSplitFolder
            // 
            this.txtFinalReportSplitFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFinalReportSplitFolder.Location = new System.Drawing.Point(116, 47);
            this.txtFinalReportSplitFolder.Name = "txtFinalReportSplitFolder";
            this.txtFinalReportSplitFolder.Size = new System.Drawing.Size(285, 20);
            this.txtFinalReportSplitFolder.TabIndex = 13;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(27, 52);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(83, 13);
            this.label9.TabIndex = 12;
            this.label9.Text = "Split Files Folder";
            // 
            // btnFinalReportSplitFolderSelect
            // 
            this.btnFinalReportSplitFolderSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFinalReportSplitFolderSelect.Location = new System.Drawing.Point(407, 47);
            this.btnFinalReportSplitFolderSelect.Name = "btnFinalReportSplitFolderSelect";
            this.btnFinalReportSplitFolderSelect.Size = new System.Drawing.Size(75, 23);
            this.btnFinalReportSplitFolderSelect.TabIndex = 11;
            this.btnFinalReportSplitFolderSelect.Text = "Select";
            this.btnFinalReportSplitFolderSelect.UseVisualStyleBackColor = true;
            this.btnFinalReportSplitFolderSelect.Click += new System.EventHandler(this.btnFinalReportSplitFolderSelect_Click);
            // 
            // txtFinalReportLIMS
            // 
            this.txtFinalReportLIMS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFinalReportLIMS.Location = new System.Drawing.Point(116, 15);
            this.txtFinalReportLIMS.Name = "txtFinalReportLIMS";
            this.txtFinalReportLIMS.Size = new System.Drawing.Size(285, 20);
            this.txtFinalReportLIMS.TabIndex = 10;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(5, 20);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(105, 13);
            this.label10.TabIndex = 9;
            this.label10.Text = "GeneSeek LIMS File";
            // 
            // btnFinalReportLIMSSelect
            // 
            this.btnFinalReportLIMSSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFinalReportLIMSSelect.Location = new System.Drawing.Point(407, 15);
            this.btnFinalReportLIMSSelect.Name = "btnFinalReportLIMSSelect";
            this.btnFinalReportLIMSSelect.Size = new System.Drawing.Size(75, 23);
            this.btnFinalReportLIMSSelect.TabIndex = 8;
            this.btnFinalReportLIMSSelect.Text = "Select";
            this.btnFinalReportLIMSSelect.UseVisualStyleBackColor = true;
            this.btnFinalReportLIMSSelect.Click += new System.EventHandler(this.btnFinalReportLIMSSelect_Click);
            // 
            // tpSupplemental
            // 
            this.tpSupplemental.Controls.Add(this.btnSupplemental);
            this.tpSupplemental.Location = new System.Drawing.Point(4, 22);
            this.tpSupplemental.Name = "tpSupplemental";
            this.tpSupplemental.Size = new System.Drawing.Size(489, 364);
            this.tpSupplemental.TabIndex = 5;
            this.tpSupplemental.Text = "Supplemental";
            this.tpSupplemental.UseVisualStyleBackColor = true;
            // 
            // btnSupplemental
            // 
            this.btnSupplemental.Location = new System.Drawing.Point(13, 16);
            this.btnSupplemental.Name = "btnSupplemental";
            this.btnSupplemental.Size = new System.Drawing.Size(111, 23);
            this.btnSupplemental.TabIndex = 16;
            this.btnSupplemental.Text = "Test";
            this.btnSupplemental.UseVisualStyleBackColor = true;
            this.btnSupplemental.Click += new System.EventHandler(this.btnSupplemental_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(405, 256);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 8;
            this.button2.Text = "Form 1";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 483);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form3";
            this.Text = "GeneSeek - Cobb Database Prototype Test System";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.panel1.ResumeLayout(false);
            this.tabCobb.ResumeLayout(false);
            this.tpDNAReport.ResumeLayout(false);
            this.tpDNAReport.PerformLayout();
            this.tpSNP_Map.ResumeLayout(false);
            this.tpSNP_Map.PerformLayout();
            this.tpSampleMap.ResumeLayout(false);
            this.tpSampleMap.PerformLayout();
            this.tpLocusSummary.ResumeLayout(false);
            this.tpLocusSummary.PerformLayout();
            this.tpFinalReport.ResumeLayout(false);
            this.tpFinalReport.PerformLayout();
            this.tpSupplemental.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabCobb;
        private System.Windows.Forms.TabPage tpDNAReport;
        private System.Windows.Forms.TabPage tpSNP_Map;
        private System.Windows.Forms.TextBox txtDNAReportBSONFilename;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDNAReportBSON_File;
        private System.Windows.Forms.TextBox txtGSLIMS_Filename;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnDNAReport_Select;
        private System.Windows.Forms.Button btnCreateBSONFile;
        private System.Windows.Forms.Button btnSNPMapBSON;
        private System.Windows.Forms.TextBox txtSNPMapOut;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSNPMapOut;
        private System.Windows.Forms.TextBox txtSNPMapLIMS;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSNPMapLIMS;
        private System.Windows.Forms.TabPage tpSampleMap;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtSampleMapBSON;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnSampleMapSelectBSON;
        private System.Windows.Forms.TextBox txtSampleMapLIMS;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnSampleMapSelectLIMS;
        private System.Windows.Forms.TabPage tpLocusSummary;
        private System.Windows.Forms.Button btnLocusSummaryCreateFile;
        private System.Windows.Forms.TextBox txtLocusSummaryBSON;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnLocusSummaryBSON;
        private System.Windows.Forms.TextBox txtLocusSummaryLIMS;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnLocusSummaryLIMS;
        private System.Windows.Forms.TabPage tpFinalReport;
        private System.Windows.Forms.TextBox txtFinalReportBSON;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnFinalReportBSONFolderSelect;
        private System.Windows.Forms.Button btnFinalReportSplitFiles;
        private System.Windows.Forms.Button btnFinalReportCreateBSON;
        private System.Windows.Forms.TextBox txtFinalReportSplitFolder;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnFinalReportSplitFolderSelect;
        private System.Windows.Forms.TextBox txtFinalReportLIMS;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnFinalReportLIMSSelect;
        private System.Windows.Forms.TabPage tpSupplemental;
        private System.Windows.Forms.Button btnSupplemental;
        private System.Windows.Forms.Button button2;
    }
}