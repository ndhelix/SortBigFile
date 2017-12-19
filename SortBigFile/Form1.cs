using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SortBigFile
{
    public partial class Form1 : Form
    {
        const int MinTextLength = 3;
        const int MaxTextLength = 400;
        const int MaxInt = 1000000;
        public Form1()
        {
            InitializeComponent();
        }

        static Random _random = new Random();


        string duplicate;
        int duplicatecandidatepercent = 10;
        int useduplicatepercent = 10;

        
        private void btnSort_Click( object sender, EventArgs e )
        {
            lblStatus.Text = "Sorting...";
            Application.DoEvents();
            DateTime start = DateTime.Now;

            string filename = txtFile.Text;
            if (!File.Exists( filename ))
            {
                MessageBox.Show("File does not exist: " + filename );
                return;
            }

            Sorter sorter = new Sorter();
            long size = new System.IO.FileInfo( filename ).Length;
            string sortedfilename = Path.Combine( Path.GetDirectoryName( filename ),
                Path.GetFileNameWithoutExtension( filename ) + "_sorted" + Path.GetExtension( filename ) );
            sorter.SortFile( filename, sortedfilename );

            var timepass = DateTime.Now - start;
            lblStatus.Text = string.Format( "{0:0.0} GB file sorted in {1}. ", (decimal)size/1024/1024/1024, timepass.ToString( @"hh\:mm\:ss" ) );
        }

        private void btnSelectFile_Click( object sender, EventArgs e )
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                 txtFile.Text = openFileDialog1.FileName;
            }


        }
    }
}
