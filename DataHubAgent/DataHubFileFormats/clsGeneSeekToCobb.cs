using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace DataHub.DataHubAgent
{
    public class clsGeneSeekToCobb
    {
        public string Filename { get; set; }

        public string GetGeneSeekFileName(string Title, string Wildcard)
        {
            var OFD = new OpenFileDialog();
            OFD.Title = Title;
            OFD.ValidateNames = true;
            if (Wildcard.Trim().Length == 0)
            {
                OFD.Filter = "*.*";
            } else
            {
                OFD.Filter = Wildcard + " file|*" + Wildcard + "*.*|All Files|*.*";
            }
            OFD.FilterIndex = 0;
            OFD.AddExtension = false;
            OFD.CheckFileExists = true;
            OFD.Multiselect = false;
            OFD.ReadOnlyChecked = true;
            OFD.RestoreDirectory = false;
            OFD.ShowDialog();
            return OFD.FileName;
        }

        public string[] LoadFileLines(string FQFilename)
        {
            var fileLines = File.ReadAllLines(FQFilename);
            return fileLines;
        }
    }
}       